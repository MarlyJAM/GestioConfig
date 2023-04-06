using GestioConfig.Models;
using GestioConfig.Pages.ProductsPages;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GestioConfig.Pages.InventoryPages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AllInventories : ContentPage
    {
        List<Inventory> results = new List<Inventory>();
        public Inventory selectedInventory;
        public Inventory result;

        private async void listInventories_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            selectedInventory = (Inventory)listInventories.SelectedItem;
            var request = new HttpRequestMessage();
            request.RequestUri = new Uri($"http://192.168.94.84:80/gestioconfig/inventory.php?id={selectedInventory.id}");
            request.Method = HttpMethod.Get;
            request.Headers.Add("Accept", "application/json");
            var client = new HttpClient();
            HttpResponseMessage response = await client.SendAsync(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                string content = await response.Content.ReadAsStringAsync();
                result = JsonConvert.DeserializeObject<Inventory>(content);
                Console.WriteLine("Le résultat est " + result);
                await Navigation.PushAsync(new ShowContentInventory(result));
            }

            
        }

        

        private async void ShowInventory_Clicked(object sender, EventArgs e)
        {
            var request = new HttpRequestMessage();
            request.RequestUri = new Uri("http://192.168.94.84:80/gestioconfig/inventory.php");
            request.Method = HttpMethod.Get;
            request.Headers.Add("Accept", "application/json");
            var client = new HttpClient();
            HttpResponseMessage response = await client.SendAsync(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                listInventories.IsVisible = true;
                ShowInventory.IsVisible = false;
                string contents = await response.Content.ReadAsStringAsync();
                results = JsonConvert.DeserializeObject<List<Inventory>>(contents);
                Console.WriteLine("Le résultat est " + results);
                listInventories.ItemsSource = results;
            }



        }
        public AllInventories()
        {
            InitializeComponent();
            
        }

        

    }
}