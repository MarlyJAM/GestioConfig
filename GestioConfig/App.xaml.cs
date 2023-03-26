using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using GestioConfig;
using GestioConfig.Pages;


namespace GestioConfig
{
    public partial class App : Application
    {
        public static MasterDetailPage MAsterDet { get; set; }
        public App()
        {
            InitializeComponent();
            MainPage = MainPage =new NavigationPage(new GestioConfig.Pages.LoginPage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
