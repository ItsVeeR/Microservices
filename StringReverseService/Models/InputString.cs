using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StringReverseService.Models
{
    public class InputString
    {
        
        public Guid Id { get; set; }

        [Required]
        public string InputValue { get; set; }

        public DateTime RequestedOn { get; set; }
    }
}
