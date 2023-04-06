using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using Newtonsoft.Json;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using GestioConfig.Models;

namespace GestioConfig.Pages.ProductsPages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AllProducts : ContentPage
	{

        public AllProducts()
        {
            InitializeComponent();


        }
        private async void btndress_Clicked(object sender, EventArgs e)
        {
            var request = new HttpRequestMessage();
            request.RequestUri = new Uri("http://192.168.94.84:80/gestioconfig/product.php?category_id=1");
            request.Method = HttpMethod.Get;
            request.Headers.Add("Accept", "application/json");
            var client = new HttpClient();
            HttpResponseMessage response = await client.SendAsync(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                string content = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<List<Products>>(content);
                Console.WriteLine("Le résultat est "+result);
                await Navigation.PushAsync(new ShowProductCategory(result));
            }
        }

        private async void btnskirt_Clicked(object sender, EventArgs e)
        {
            var skirtrequest = new HttpRequestMessage();
            skirtrequest.RequestUri = new Uri("http://192.168.94.84:80/gestioconfig/product.php?category_id=2");
            skirtrequest.Method = HttpMethod.Get;
            skirtrequest.Headers.Add("Accept", "application/json");
            var skirtclient = new HttpClient();
            HttpResponseMessage skirtresponse = await skirtclient.SendAsync(skirtrequest);
            if (skirtresponse.StatusCode == HttpStatusCode.OK)
            {
                string skirtcontent = await skirtresponse.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<List<Products>>(skirtcontent);
                await Navigation.PushAsync(new ShowProductCategory(result));
            }
        }

        }
}