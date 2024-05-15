using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Assignment_3.Models
{
    public class EmployeeSalaryModel
    {
        [Required]
        public double FixedSalary { get; set; }

        [Required]
        [Display(Name = "Present Days")]
        public int PresentDays { get; set; }

        [Required]
        [Display(Name = "Leave Days")]
        public int LeaveDays { get; set; }

        [Required]
        [Display(Name = "Overtime Hours")]
        public int OvertimeHours { get; set; }

        public double BasicSalary { get; set; }
        public double LeaveDeduction { get; set; }
        public double OvertimeEarnings { get; set; }
        public double TotalSalary { get; set; }
    }
}