using System;
using System.ComponentModel.DataAnnotations;

namespace JublFood.POS.App.Models.Employee
{
    public class UpdateClockInAndOutDataBiometricRequest
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
        public int DateShiftNumber { get; set; }

        [Required]
        public string PunchType { get; set; }

        [Required]
        public string Source { get; set; }
    }
}
