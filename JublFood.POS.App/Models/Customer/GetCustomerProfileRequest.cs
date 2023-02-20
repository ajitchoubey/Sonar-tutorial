using System.ComponentModel.DataAnnotations;

namespace JublFood.POS.App.Models.Customer
{
    public class GetCustomerProfileRequest
    {
        [Required]
        public string LocationCode { get; set; }

        [Required]
        public string CustomerCode { get; set; }
    }
}
