using GestioConfig.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GestioConfig.Pages.InventoryPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ShowContentInventory : ContentPage
    {
        public Inventory inventory;
        public List<Product> products;
        public ShowContentInventory(Inventory result)
        {
            InitializeComponent();
            inventory = result;
            products = inventory.products;
            txtdate.BindingContext = inventory;
            txtNameInventory.BindingContext = inventory;
            listProducts.ItemsSource = products;
            /*txtName.BindingContext = products;
            txtPrice.BindingContext = products;
            txtReference.BindingContext = products;
            txtStock.BindingContext = products;
            txtQuantity.BindingContext = products; */
        }

        private void listProducts_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {

        }
    }
}