using System;
using System.Collections.Generic;

namespace JublFood.POS.App.Models.Cart
{
    public class CartOnHoldResponse
    {
        public String ResponseCode { get; set; }
        public String ResponseMessage { get; set; }

        public List<CartOnHoldModel> cartOnHolds { get; set; }
    }

    public class CartOnHoldModel
    {
        public int Id { get; set; }
        public string CartId { get; set; }
        public DateTime Time { get; set; }
        public string CustomerName { get; set; }
        public string CustomerNumber { get; set; }
        public decimal OrderAmount { get; set; }
        public string OrderTaker { get; set; }
        public string Terminal { get; set; }
        public string IsActive { get; set; }
    }

}
