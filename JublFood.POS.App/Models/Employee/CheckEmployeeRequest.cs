using System;
using System.ComponentModel.DataAnnotations;

namespace JublFood.POS.App.Models.Employee
{
    public class CheckEmployeeRequest
    {
        [Required]
        public string LocationCode { get; set; }

        [Required]
        public DateTime SystemDate { get; set; }

        [Required]
        public string UserId { get; set; }

        public string PositionCode { get; set; }
    }
}
