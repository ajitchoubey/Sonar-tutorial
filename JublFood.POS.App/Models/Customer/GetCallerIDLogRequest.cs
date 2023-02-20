using System.ComponentModel.DataAnnotations;

namespace JublFood.POS.App.Models.Customer
{
    public class GetCallerIDLogRequest
    {
        [Required]
        public string LocationCode { get; set; }

        [Required]
        public bool blnToday { get; set; }
    }
}
