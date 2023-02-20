using System.ComponentModel.DataAnnotations;

namespace JublFood.POS.App.Models.Employee
{
    public class GetTextRequest
    {
        [Required]
        public string LocationCode { get; set; }

        [Required]
        public int LanguageCode { get; set; }

        [Required]
        public int KeyField { get; set; }
    }
}
