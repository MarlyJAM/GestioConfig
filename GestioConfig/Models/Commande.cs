using System;
using System.Collections.Generic;
using System.Text;

namespace GestioConfig.Models
{
    public class Commande
    {
        public int id { get; set; }
        public List<Product> products { get; set; }
        public string reference { get; set; }
        public string customer_name { get; set; }
        public string customer_email { get; set; }
        public decimal total_amount { get; set; }
        public DateTime created_at { get; set; }
    }
}
