using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using GestioConfig.Models;
using Newtonsoft.Json;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static  GestioConfig.Models.LoginResponse;
using static Xamarin.Essentials.AppleSignInAuthenticator;

namespace GestioConfig.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private async void  Button_Clicked(object sender, EventArgs e)
        {
           
            var email = Email.Text;
            var password = Password.Text;
            var loginData = new { email = email, password = password };
            var jsonData = JsonConvert.SerializeObject(loginData);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            Console.WriteLine("Montre moi le : "+ " " + email + " "  + password + " " + jsonData + " " + content);
            var httpClient = new HttpClient();
            try
            {
                var response = await httpClient.PostAsync("http://192.168.102.84:80/gestioconfig/login.php", content);
                response.EnsureSuccessStatusCode();
               var responseContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine("Montre moi le : " + responseContent);
                var result = JsonConvert.DeserializeObject<LoginResponse>(responseContent);
                if (result.success)
                {
                    await Navigation.PushAsync(new HomePage());
                }
                else
                {
                  
                    await DisplayAlert("Message", result.message, "OK");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }


        }
    }
}