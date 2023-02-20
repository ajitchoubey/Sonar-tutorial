using System.ComponentModel.DataAnnotations;

namespace JublFood.POS.App.Models.Order
{
    public class InsertAuditTrailRequest
    {
        [Required]
        public string LocationCode { get; set; }

        [Required]
        public int MenuItemID { get; set; }

        [Required]
        public bool AccessDenied { get; set; }

        [Required]
        public string OriginalValue { get; set; }

        [Required]
        public string NewValue { get; set; }

        [Required]
        public string EmployeeCode { get; set; }

        [Required]
        public int ActionID { get; set; }

        [Required]
        public string AddedPositionCode { get; set; }

        [Required]
        public string Source { get; set; }

    }
}
