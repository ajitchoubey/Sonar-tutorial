using System;
using System.ComponentModel.DataAnnotations;

namespace JublFood.POS.App.Models.Employee
{
    public class EmployeeLoginRequest
    {
        [Required]
        public string LocationCode { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        public string EmployeeCode { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public DateTime SystemDate { get; set; }

        [Required]
        public string Source { get; set; }
    }
}
