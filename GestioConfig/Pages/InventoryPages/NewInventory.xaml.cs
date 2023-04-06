using GestioConfig.Models;
using GestioConfig.Pages.ProductsPages;
using Newtonsoft.Json;
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
	public partial class NewInventory : ContentPage
	{
		public NewInventory ()
		{
			InitializeComponent ();
		}

        private void newinventory_Clicked(object sender, EventArgs e)
        {
            newinventory.IsVisible = false;
            txtName.IsVisible = true;
            btnCreate.IsVisible = true;
        }
        private async void btnCreate_Clicked(object sender, EventArgs e)
        {
            if (txtName.Text == "")
            {
                await DisplayAlert("Erreur", "Veuillez écrire quelque chose.", "OK");
                return;
            }
           
            Inventory i = new Inventory
            {
                name = txtName.Text

            };
            Uri RequestUri = new Uri("http://192.168.94.84:80/gestioconfig/inventory.php");
            var client = new HttpClient();
            var json = JsonConvert.SerializeObject(i);
            var contentJson = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PostAsync(RequestUri, contentJson);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                await DisplayAlert("Data", "Inventory was added ", "OK");
                await Navigation.PushAsync(new AllInventories());
            }
            else
            {
                await DisplayAlert("Data", "Error ", "OK");
                txtName.Text = "";
                
            }
        }
    }
}