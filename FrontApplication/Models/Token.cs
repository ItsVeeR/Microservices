using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FrontApplication.Models
{
    public class Token
    {
        public string SecretKey { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
    }
}