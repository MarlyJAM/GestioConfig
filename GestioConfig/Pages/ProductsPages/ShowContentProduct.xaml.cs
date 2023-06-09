﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using GestioConfig.Models;
using Newtonsoft.Json;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GestioConfig.Pages.ProductsPages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ShowContentProduct : ContentPage
	{
        private int selectedCategoryId;
        public Category selectedCategory;
        public Products products;
        public ShowContentProduct (Models.Products selectedProduct, List<Category> categories)
		{
			InitializeComponent ();
            products = selectedProduct;
			BindingContext=selectedProduct;
            int Id = selectedProduct.id;

            picker.ItemsSource = categories;
			
			
        }
        private async void picker_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedCategory = picker.SelectedItem as Category;
            if (selectedCategory != null)
            {
                selectedCategoryId = selectedCategory.id;
                Console.WriteLine("Selected category ID: " + selectedCategoryId);
            }
            else
            {
                await DisplayAlert("Erreur", "Veuillez sélectionner une catégorie.", "OK");
                Console.WriteLine("Aucune catégorie sélectionnée.");
            }
        }


        private async void btnUpdate_Clicked(object sender, EventArgs e)
        {
            if (selectedCategoryId == 0)
            {
                await DisplayAlert("Erreur", "Veuillez sélectionner une catégorie.", "OK");
                return;
            }
            if (!IsInteger(txtQuantity.Text))
            {
                await DisplayAlert("Erreur", "Veuillez entrer une quantité valide.", "OK");
                return;
            }
            if (!ValidatePrice(txtPrice.Text))
            {
                return;
            }
            Products p = new Products
            {
                id=products.id,
                name = txtName.Text,
                description = txtDescription.Text,
                quantity = Int32.Parse((txtQuantity.Text).ToString()),
                price = Decimal.Parse(txtPrice.Text),
                category_id = selectedCategoryId,

            };
            Uri RequestUri = new Uri($"http://192.168.94.84:80/gestioconfig/product.php?id={products.id}");
            var client = new HttpClient();
            var json = JsonConvert.SerializeObject(p);
            var contentJson = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PutAsync(RequestUri, contentJson);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                Console.WriteLine("L' identifiant est :" +""+ products.id +"La quantité est :"+""+ p.quantity+""+"Les variable json sont :"+json) ;
                await DisplayAlert("Data", "Product was updated ", "OK");
                await Navigation.PushAsync(new AllProducts());
            }
            else
            {
                await DisplayAlert("Data", "Error ", "OK");
            }
        }
        private bool IsInteger(string input)
        {
            int number;
            return Int32.TryParse(input, out number);
        }
        private bool ValidatePrice(string priceStr)
        {
            bool isValid = decimal.TryParse(priceStr, out decimal price);
            if (!isValid)
            {
                DisplayAlert("Erreur", "Le prix doit être un nombre décimal valide", "OK");
            }
            return isValid;
        }

        private async void btnDelete_Clicked(object sender, EventArgs e)
        {
            if (selectedCategoryId == 0)
            {
                await DisplayAlert("Erreur", "Veuillez sélectionner une catégorie.", "OK");
                return;
            }
            if (!IsInteger(txtQuantity.Text))
            {
                await DisplayAlert("Erreur", "Veuillez entrer une quantité valide.", "OK");
                return;
            }
            if (!ValidatePrice(txtPrice.Text))
            {
                return;
            }
            await DisplayAlert("Warning", "Voulez vous vraiment supprimer ce produit?", "OK");
            Uri RequestUri = new Uri($"http://192.168.94.84:80/gestioconfig/product.php?id={products.id}");
            var client = new HttpClient();
            var response = await client.DeleteAsync(RequestUri);
            if (response.StatusCode == HttpStatusCode.OK)
            {

                await DisplayAlert("Data", "Product was deleted ", "OK");
                await Navigation.PushAsync(new AllProducts());
            }
            else
            {
                await DisplayAlert("Data", "Error ", "OK");
            }
        }
    }
}