using System.ComponentModel.DataAnnotations;

namespace JublFood.POS.App.Models.Employee
{
    public class TimeClockConfirmationRequest
    {
        [Required]
        public string LocationCode { get; set; }

        [Required]
        public int WorkStationID { get; set; }

        [Required]
        public int LanguageCode { get; set; }

        [Required]
        public string EmployeeCode { get; set; }

        [Required]
        public string Source { get; set; }
    }
}
