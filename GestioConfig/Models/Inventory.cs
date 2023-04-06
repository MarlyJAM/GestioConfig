using System;
using System.Collections.Generic;
using System.Text;

namespace GestioConfig.Models
{
    public class Product
    {
        public int id { get; set; }
        public string reference { get; set; }
        public string name { get; set; }
        public decimal price { get; set; }
        public int quantity { get; set; }
        public int stock { get; set; }
    }

    public class Inventory
    {
        public int id { get; set; }
        public string name { get; set; }
        public DateTime date_added { get; set; }
        public List<Product> products { get; set; }
    }
}
