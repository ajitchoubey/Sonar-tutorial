using System.ComponentModel.DataAnnotations;

namespace JublFood.POS.App.Models.Employee
{
    public class EmployeeSpecialAccessRequest
    {
        [Required]
        public string LocationCode { get; set; }

        [Required]
        public string EmployeeCode { get; set; }

        [Required]
        public int MenuItemId { get; set; }

        [Required]
        public string Source { get; set; }

    }
}
