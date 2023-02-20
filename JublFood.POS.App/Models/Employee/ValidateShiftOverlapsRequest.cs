using System;
using System.ComponentModel.DataAnnotations;

namespace JublFood.POS.App.Models.Employee
{
    public class ValidateShiftOverlapsRequest
    {
        [Required]
        public string LocationCode { get; set; }

        [Required]
        public string EmployeeCode { get; set; }
        
        [Required]
        public DateTime TimeIn { get; set; }

        [Required]
        public DateTime TimeOut { get; set; }

        [Required]
        public DateTime ProcessingDate { get; set; }

        [Required]
        public int Shift { get; set; }

        [Required]
        public string Source { get; set; }
    }
}
