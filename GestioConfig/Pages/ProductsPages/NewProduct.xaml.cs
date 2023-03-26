using GestioConfig.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using GestioConfig.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Security.Cryptography.X509Certificates;

namespace GestioConfig.Pages.ProductsPages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class NewProduct : ContentPage
	{
        private int selectedCategoryId;
        public Category selectedCategory;
        public  NewProduct ()
		{
			InitializeComponent ();
         }

        private async void newproduct_Clicked(object sender, EventArgs e)
        {
            newproduct.IsVisible = false;
            txtPrice.IsVisible = true;
            txtQuantity.IsVisible = true;
            txtName.IsVisible = true;
            txtDescription.IsVisible = true;
            btnCreate.IsVisible = true;
            picker.IsVisible = true;
            var request = new HttpRequestMessage();
            request.RequestUri = new Uri("http://192.168.102.84:80/gestioconfig/category.php");
            request.Method = HttpMethod.Get;
            request.Headers.Add("Accept", "application/json");
            var client = new HttpClient();
            HttpResponseMessage response = await client.SendAsync(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                string content = await response.Content.ReadAsStringAsync();
                var categories = JsonConvert.DeserializeObject<List<Category>>(content);
                Console.WriteLine("Le résultat est " + categories);
                picker.ItemsSource=categories;
            }
           
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

        private async void btnCreate_Clicked(object sender, EventArgs e)
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
                name = txtName.Text,
                description = txtDescription.Text,
                quantity = Int32.Parse((txtQuantity.Text).ToString()),
                price = Decimal.Parse(txtPrice.Text),
                category_id = selectedCategoryId,

            };
            Uri RequestUri = new Uri("http://192.168.102.84:80/gestioconfig/product.php");
            var client = new HttpClient();
            var json = JsonConvert.SerializeObject(p);
            var contentJson = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PostAsync(RequestUri, contentJson);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                await DisplayAlert("Data", "Product was added ", "OK");
                await Navigation.PushAsync(new AllProducts());
            }
            else
            {
                await DisplayAlert("Data", "Error ", "OK");
                txtName.Text = "";
                txtDescription.Text = "";
                txtQuantity.Text = "";
                txtPrice.Text = "";
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

    }
}