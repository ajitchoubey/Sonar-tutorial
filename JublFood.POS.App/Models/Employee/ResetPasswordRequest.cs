using System.ComponentModel.DataAnnotations;

namespace JublFood.POS.App.Models.Employee
{
    public class ResetPasswordRequest
    {
        [Required]
        public string LocationCode { get; set; }

        [Required]
        public string LoginEmployeeCode { get; set; }

        [Required]
        public string EmployeeCode { get; set; }

        [Required]
        public string EventType { get; set; }

        [Required]
        public string Source { get; set; }
    }
}
