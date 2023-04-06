using GestioConfig.Models;
using GestioConfig.Pages.ProductsPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GestioConfig.Pages.InventoryPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ShowContentInventory : ContentPage
    {
        public Inventory inventory;
        public List<Product> products;
        public ShowContentInventory(Inventory result)
        {
            InitializeComponent();
            inventory = result;
            products = inventory.products;
            txtdate.BindingContext = inventory;
            txtNameInventory.BindingContext = inventory;
            listProducts.ItemsSource = products;
            /*txtName.BindingContext = products;
            txtPrice.BindingContext = products;
            txtReference.BindingContext = products;
            txtStock.BindingContext = products;
            txtQuantity.BindingContext = products; */
        }

        private async void btnDelete_Clicked(object sender, EventArgs e)
        {
            await DisplayAlert("Warning", "Voulez vous vraiment supprimer cet inventaire?", "OK");
            Uri RequestUri = new Uri($"http://192.168.94.84:80/gestioconfig/inventory.php?id={inventory.id}");
            var client = new HttpClient();
            var response = await client.DeleteAsync(RequestUri);
            if (response.StatusCode == HttpStatusCode.OK)
            {

                await DisplayAlert("Data", "Inventory was deleted ", "OK");
                await Navigation.PushAsync(new AllInventories());
            }
            else
            {
                await DisplayAlert("Data", "Error ", "OK");
            }
        }
    }
}