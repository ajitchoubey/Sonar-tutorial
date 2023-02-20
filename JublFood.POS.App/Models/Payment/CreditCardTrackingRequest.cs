using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JublFood.POS.App.Models.Cart;
using JublFood.POS.App.Models.Customer;

namespace JublFood.POS.App.Models.Payment
{
    public class CreditCardTrackingRequest
    {
        public CartHeader cartHeader { get; set; }
        public JublFood.POS.App.Models.Customer.Customer Customer { get; set; }
        public int Order_Pay_Type_Code { get; set; }
        public decimal Amount_Tendered { get; set; }
        public int Credit_Card_Code { get; set; }
        public int Credit_Card_ID { get; set; }
        public string Added_by { get; set; }

    }
}
