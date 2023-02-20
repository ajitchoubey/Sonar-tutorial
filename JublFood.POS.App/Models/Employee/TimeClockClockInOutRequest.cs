using System;
using System.ComponentModel.DataAnnotations;

namespace JublFood.POS.App.Models.Employee
{
    public class TimeClockClockInOutRequest
    {
        [Required]
        public string LocationCode { get; set; }

        [Required]
        public string EmployeeCode { get; set; }

        [Required]
        public string PositionCode { get; set; }

        [Required]
        public DateTime SystemDate { get; set; }

        [Required]
        public int PositionShiftNumber { get; set; }

        [Required]
        public int DateShiftNumber { get; set; }

        
        public decimal TillStartingAmount { get; set; }

        
        public float BeginOdometer { get; set; }

        
        public float EndOdometer { get; set; }

        
        public string Source { get; set; }

        public string AddedBy { get; set; }
    }
}
