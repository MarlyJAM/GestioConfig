using GestioConfig.Models;
using GestioConfig.Pages.InventoryPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GestioConfig.Pages.CommandePages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ShowCommande : ContentPage
    {
        public Commande commande;
        public List<Product> products;
        public ShowCommande(Commande result)
        {
            InitializeComponent();
            commande = result;
            products = commande.products;
            listProducts.ItemsSource= products;
            txtCustomerMail.BindingContext = commande;
            txtCustomerName.BindingContext = commande;
            txtReference.BindingContext = commande;
            txtDate.BindingContext = commande;
            txtTotal.BindingContext = commande;
           
          
        }
        private async void btnDelete_Clicked(object sender, EventArgs e)
        {
            await DisplayAlert("Warning", "Voulez vous vraiment supprimer cette commande?", "OK");
            Uri RequestUri = new Uri($"http://192.168.94.84:80/gestioconfig/inventory.php?id={commande.id}");
            var client = new HttpClient();
            var response = await client.DeleteAsync(RequestUri);
            if (response.StatusCode == HttpStatusCode.OK)
            {

                await DisplayAlert("Data", "Commande was deleted ", "OK");
                await Navigation.PushAsync(new AllCommandes());
            }
            else
            {
                await DisplayAlert("Data", "Error ", "OK");
            }
        }

        private async void btnUpdate_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new UpdateCommande(commande));
        }
    }
}