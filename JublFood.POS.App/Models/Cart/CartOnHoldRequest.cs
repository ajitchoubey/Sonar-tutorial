using System;

namespace JublFood.POS.App.Models.Cart
{
    public class CartOnHoldRequest
    {
        public string CartId { get; set; }
        public DateTime Time { get; set; }
        public string CustomerName { get; set; }
        public string CustomerNumber { get; set; }
        public decimal OrderAmount { get; set; }
        public string OrderTaker { get; set; }
        public string Terminal { get; set; }
        public int IsActive { get; set; }
        public DateTime OrderDate { get; set; }
    }
}
