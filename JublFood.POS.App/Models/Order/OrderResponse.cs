using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JublFood.Order.Models
{
    public class OrderResponse
    {
        public string ResponseStatus { get; set; }
        public string ResponseStatusCode { get; set; }

        public OrderResponseData orderResponseData { get; set; }
       
    }

    public class OrderResponseData
    {
        public string Location_Code { get; set; }
        public DateTime Order_Date { get; set; }
        public long Order_Number { get; set; }
        public decimal Final_Total { get; set; }
        public DateTime Order_Saved_Time { get; set; }
        public int Estimated_Time_Begin { get; set; }
        public int Estimated_Time_End { get; set; }
        public DateTime Actual_Order_Date { get; set; }
        public int Order_Status_Code { get; set; }
        public string Tent_Number { get; set; }
        public int Delayed_Order { get; set; }

    }
}
