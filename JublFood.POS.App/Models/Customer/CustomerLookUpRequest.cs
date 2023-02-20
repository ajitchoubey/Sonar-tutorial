using System.ComponentModel.DataAnnotations;

namespace JublFood.POS.App.Models.Customer
{
    public class CustomerLookUpRequest
    {
        [Required]
        public string LocationCode { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        public string PhoneNumberExt { get; set; }
    }
}