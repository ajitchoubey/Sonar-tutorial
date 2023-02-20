using System;
using System.ComponentModel.DataAnnotations;

namespace JublFood.POS.App.Models.Order
{
    public class GetOrderRequest
    {
        public String LocationCode { get; set; }
        [Required]
        public int Language_Code { get; set; }
        [Required]
        public DateTime Order_Date { get; set; }
        [Required]
        public string Address_Format { get; set; }
        [Required]
        public string Order_Type_String { get; set; }
        [Required]
        public bool Show_Order_Takers_Orders_Only { get; set; }
        [Required]
        public string Employee_Code { get; set; }
        [Required]
        public bool View_All_Orders { get; set; }
    }
}
