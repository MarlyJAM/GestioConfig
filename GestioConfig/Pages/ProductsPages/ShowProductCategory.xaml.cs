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
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;

namespace GestioConfig.Pages.ProductsPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ShowProductCategory : ContentPage
    {
        public Products selectedProduct;
        public ShowProductCategory(List<Products> result)
        {
            InitializeComponent();
            listProducts.ItemsSource = result;
        }

        private async void listProducts_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            selectedProduct = (Products)listProducts.SelectedItem;
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
                await Navigation.PushAsync(new ShowContentProduct(selectedProduct, categories));
            }
           
        }
    }
}