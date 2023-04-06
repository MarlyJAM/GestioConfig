using GestioConfig.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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
           
          
        }
    }
}