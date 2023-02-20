using System.ComponentModel.DataAnnotations;

namespace JublFood.POS.App.Models.Customer
{
    public class StreetLookUpRequest
    {
        [Required]
        public string LocationCode { get; set; }

        [Required]
        public string StreetName { get; set; }
    }
}
