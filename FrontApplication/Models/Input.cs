using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FrontApplication.Models
{
    public class Input
    {
        public Guid Id { get; set; }
         
        public string InputValue { get; set; }

        public DateTime RequestedOn { get; set; }
    }
}