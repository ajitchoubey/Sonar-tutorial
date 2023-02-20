using System.ComponentModel.DataAnnotations;

namespace JublFood.POS.App.Models
{
    public class CheckRecordRequest
    {
        [Required]
        public string LocationCode { get; set; }

        [Required]
        public int WorkstationID { get; set; }

        [Required]
        public int Flag { get; set; }

        [Required]
        public string UserID { get; set; }

        [Required]
        public string Source { get; set; }
    }
}
