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

namespace GestioConfig.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Master : ContentPage
	{
		public Master ()
		{
			InitializeComponent ();
		}

        private async void BtnInventory_Clicked(object sender, EventArgs e)
        {
            App.MAsterDet.IsPresented = false;
            await App.MAsterDet.Detail.Navigation.PushAsync(new InventoryPage());
        }

        private async void BtnProducts_Clicked(object sender, EventArgs e)
        {
            App.MAsterDet.IsPresented = false;
            await App.MAsterDet.Detail.Navigation.PushAsync(new ProductPage());
        }

        private async void BtnCommandes_Clicked(object sender, EventArgs e)
        {
            App.MAsterDet.IsPresented = false;
            await App.MAsterDet.Detail.Navigation.PushAsync(new CommandePage());
        }
    }
}