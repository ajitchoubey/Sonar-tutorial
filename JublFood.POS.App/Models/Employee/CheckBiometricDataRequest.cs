using System;
using System.ComponentModel.DataAnnotations;

namespace JublFood.POS.App.Models.Employee
{
    public class CheckBiometricDataRequest
    {
        [Required]
        public string LocationCode { get; set; }

        [Required]
        public string EmployeeCode { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        public DateTime POSDate { get; set; }

        [Required]
        public bool FormTimeClock { get; set; }

        [Required]
        public string Source { get; set; }
    }
}
