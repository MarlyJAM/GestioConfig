using System;
using System.Collections.Generic;
using System.Text;

namespace GestioConfig.Models
{
   public class Products
    {
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public decimal price { get; set; }
        public int quantity { get; set; }
        public int category_id { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
    }
}
