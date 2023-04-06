using GestioConfig.Models;
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
using GestioConfig.Pages.InventoryPages;

namespace GestioConfig.Pages.CommandePages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AllCommandes : ContentPage
    {
        List<Commande> results = new List<Commande>();
        public Commande selectedCommande;
        public Commande result;
        public AllCommandes()
        {
            InitializeComponent();
        }

        private async void ShowCommande_Clicked(object sender, EventArgs e)
        {
            var request = new HttpRequestMessage();
            request.RequestUri = new Uri("http://192.168.94.84:80/gestioconfig/commande.php");
            request.Method = HttpMethod.Get;
            request.Headers.Add("Accept", "application/json");
            var client = new HttpClient();
            HttpResponseMessage response = await client.SendAsync(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                listCommandes.IsVisible = true;
                ShowCommande.IsVisible = false;
                string contents = await response.Content.ReadAsStringAsync();
                results = JsonConvert.DeserializeObject<List<Commande>>(contents);
                Console.WriteLine("Le résultat est " + results);
                listCommandes.ItemsSource = results;
            }


        }

        private async void listCommandes_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            selectedCommande = (Commande)listCommandes.SelectedItem;
            var request = new HttpRequestMessage();
            request.RequestUri = new Uri($"http://192.168.94.84:80/gestioconfig/commande.php?id={selectedCommande.id}");
            request.Method = HttpMethod.Get;
            request.Headers.Add("Accept", "application/json");
            var client = new HttpClient();
            HttpResponseMessage response = await client.SendAsync(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                string content = await response.Content.ReadAsStringAsync();
                result= JsonConvert.DeserializeObject<Commande>(content);
                Console.WriteLine("Le résultat est " + result);
                await Navigation.PushAsync(new ShowCommande(result));
            }
        }
    }
}