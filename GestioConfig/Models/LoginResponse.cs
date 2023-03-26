using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace GestioConfig.Models
{
    public class LoginResponse
    {
        public bool success { get; set; }
        public string message { get; set; }
        public string token { get; set; }
    }

}
