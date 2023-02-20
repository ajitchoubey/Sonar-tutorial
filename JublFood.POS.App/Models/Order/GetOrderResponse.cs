using System;
using System.Collections.Generic;

namespace JublFood.POS.App.Models.Order
{
    public class GetOrderResponse
    {
        public string ResponseStatus { get; set; }
        public string ResponseStatusCode { get; set; }

        public List<GetOrders> getOrders { get; set; }

    }

    public class GetOrders
    {
        public string BeingModified { get; set; }
        public long Order_Number { get; set; }

        public string Actual_Order_Date { get; set; }
        public string Actual_Order_Time { get; set; }
        public string OrderStatus { get; set; }

        public string Phone_Number { get; set; }

        public string Tent_Number { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Order_Taker { get; set; }
        public decimal Price { get; set; }
        public string Driver { get; set; }
        public DateTime? Route_Time { get; set; }
        public DateTime? Return_Time { get; set; }
        public string OpenStatus { get; set; }
        public DateTime Order_Date { get; set; }
        public string Order_Number_Search { get; set; }
        public string ROI_Customer { get; set; }
        public bool AggregatorGSTCalculation { get; set; }
    }
}
