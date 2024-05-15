using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Assignment_3.Models
{
    public class EmployeeModel
    {
        public int EmployeeId { get; set; }

        [Required]
        public string EmpName { get; set; }

        [Required]
        public int Age { get; set; }
    }
}