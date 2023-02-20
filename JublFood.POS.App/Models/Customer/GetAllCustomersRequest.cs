using System.ComponentModel.DataAnnotations;

namespace JublFood.POS.App.Models.Customer
{
    public class GetAllCustomersRequest
    {
        [Required]
        public string LocationCode { get; set; }
    }
}
