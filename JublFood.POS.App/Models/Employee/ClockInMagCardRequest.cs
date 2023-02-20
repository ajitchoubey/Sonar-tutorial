using System.ComponentModel.DataAnnotations;

namespace JublFood.POS.App.Models.Employee
{
    public class ClockInMagCardRequest
    {
        [Required]
        public string LocationCode { get; set; }

        [Required]
        public string Track1Data { get; set; }

        [Required]
        public string Track2Data { get; set; }

        [Required]
        public string Track3Data { get; set; }

        [Required]
        public string Source { get; set; }
    }
}
