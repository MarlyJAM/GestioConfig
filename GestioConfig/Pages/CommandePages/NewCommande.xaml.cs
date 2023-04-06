using GestioConfig.Models;
using GestioConfig.Pages.CommandePages;
using GestioConfig.Pages.InventoryPages;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;
using Entry = Xamarin.Forms.Entry;
using Picker = Xamarin.Forms.Picker;

namespace GestioConfig.Pages.CommandePages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class NewCommande : ContentPage
	{
        private int selectedProductId;
        private Products selectedProduct;
        private Entry entry;
        private Picker picker;
        private List<Products> products = new List<Products>();
        private List<Product> selectedProducts = new List<Product>();
        public NewCommande ()
		{
            InitializeComponent();
        }
        /* Charger les produits pour les utiliser dans le picker*/
        private async void Charger_Clicked(object sender, EventArgs e)
        {
            txtReference.IsVisible = true;
            txtCustomer_mail.IsVisible = true;
            txtCustomer_name.IsVisible = true;
            addProduct.IsVisible=true;
            txtcommande.IsVisible=false;
            create.IsVisible = true;
            Charger.IsVisible=false;

            var request = new HttpRequestMessage();
            request.RequestUri = new Uri("http://192.168.94.84:80/gestioconfig/product.php");
            request.Method = HttpMethod.Get;
            request.Headers.Add("Accept", "application/json");
            var client = new HttpClient();
            HttpResponseMessage response = await client.SendAsync(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                string content = await response.Content.ReadAsStringAsync();
                products = JsonConvert.DeserializeObject<List<Products>>(content);
                Console.WriteLine("Le résultat est " + products);
                
            }
        }
        /* Méthode pour ajouter un créer les champs où seront choisi les produits commandés */
        private void addProduct_Clicked(object sender, EventArgs e)
        {
            picker = new Picker { Title = "Select a product", TitleColor = Color.Red };
            picker.ItemsSource=products;
            picker.IsVisible = true;
            picker.ItemDisplayBinding = new Binding("name");
            picker.SelectedIndexChanged += OnPickerSelectedIndexChanged;
            entry = new Entry { Placeholder = "Quantity" };
            entry.IsVisible = true;
            Button myButton = new Button();
            myButton.Text = "add product";
            myButton.Clicked += OnButtonClicked;

            StackLayout.Children.Add(picker);
            StackLayout.Children.Add(entry);
            StackLayout.Children.Add(myButton);

        }
        /* méthode pour récupérer l'id de l'élément sélectionné dans le picker*/
        private async void OnPickerSelectedIndexChanged(object sender, EventArgs e)
        {
            selectedProduct = picker.SelectedItem as Products;
            if (selectedProduct != null)
            {
                selectedProductId = selectedProduct.id;
                Console.WriteLine("Selected category ID: " + selectedProductId);
            }
            else
            {
                await DisplayAlert("Erreur", "Veuillez sélectionner une catégorie.", "OK");
                Console.WriteLine("Aucune catégorie sélectionnée.");
            }

        }
        
        /* Méthode pour ajouter un produit dans la liste qui sera utilisé plus tart pour la création de la commande */
        private async void OnButtonClicked(object sender, EventArgs e)
        {
            if (!IsInteger(entry.Text))
            {
                await DisplayAlert("Erreur", "Veuillez entrer une quantité valide.", "OK");
                return;
            }
            var quantity = Int32.Parse((entry.Text).ToString());
            Product p = new Product
            {
                id=selectedProductId,
                quantity = quantity

            };
            selectedProducts.Add(p);
            entry.Text = "";
            picker.SelectedItem = null;
        }
        /* Méthode pour créer une commande */
        private async void create_Clicked(object sender, EventArgs e)
        {
            if (txtReference.Text == "")
            {
                await DisplayAlert("Erreur", "Veuillez écrire quelque chose.", "OK");
                return;
            }
            if (txtCustomer_mail.Text == "")
            {
                await DisplayAlert("Erreur", "Veuillez écrire quelque chose.", "OK");
                return;
            }
            if (txtCustomer_name.Text == "")
            {
                await DisplayAlert("Erreur", "Veuillez écrire quelque chose.", "OK");
                return;
            }
            if (txtReference.Text == "")
            {
                await DisplayAlert("Erreur", "Veuillez écrire quelque chose.", "OK");
                return;
            }

            Commande c = new Commande
            {
                reference = txtReference.Text,
                customer_email=txtCustomer_mail.Text,
                customer_name=txtCustomer_name.Text,
                products = selectedProducts

            };
            Uri RequestUri = new Uri("http://192.168.94.84:80/gestioconfig/commande.php");
            var client = new HttpClient();
            var json = JsonConvert.SerializeObject(c);
            var contentJson = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PostAsync(RequestUri, contentJson);
            
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine("Montre moi le : " + responseContent);
                var result = JsonConvert.DeserializeObject<LoginResponse>(responseContent);
                await DisplayAlert("Data", result.message, "OK");
                Console.WriteLine("Selected category ID: " + contentJson);
            }
            else
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine("Montre moi le : " + responseContent);
                var result = JsonConvert.DeserializeObject<LoginResponse>(responseContent);
                await DisplayAlert("Data", result.message, "OK");
                txtReference.Text = "";
                Console.WriteLine("Selected category ID: " + contentJson);
            }
        }
        private bool IsInteger(string input)
        {
            int number;
            return Int32.TryParse(input, out number);
        }
    }
}