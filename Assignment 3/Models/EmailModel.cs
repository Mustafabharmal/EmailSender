using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Assignment_3.Models
{
    public class EmailModel
    {
        [Required]
        [EmailAddress]
        public string ToEmail { get; set; }

        [EmailAddress]
        public string CCEmail { get; set; }

        [EmailAddress]
        public string BCCEmail { get; set; }

        [Required]
        public string Subject { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        public string Body { get; set; }
    }
}