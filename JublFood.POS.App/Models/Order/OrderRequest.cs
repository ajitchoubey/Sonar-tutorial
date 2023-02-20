using System;
using System.Collections.Generic;

namespace JublFood.POS.App.Models.Order
{
    public class OrderRequest
    {
        public string locationCode { get; set; }
        public DateTime Order_Date { get; set; }
        public string cartId { get; set; }
        public bool modifyOrder { get; set; }
        public int newOrderTime { get; set; }
    }
}
