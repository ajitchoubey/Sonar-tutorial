using JublFood.POS.App.Models.Order;
using System;
using System.Collections.Generic;

namespace JublFood.POS.App.Models.Payment
{
    public class ResponsePayment
    {
        public string cart_id { get; set; }
        public Boolean Response_Status { get; set; }
        public string Response_Message { get; set; }
        public List<OrderPayment> payment { get; set; }
        public List<OrderCreditCard> orderCreditCard { get; set; }
        public List<OrderGiftCard> OrderGiftCards { get; set; }
        // public Paymentresponse payment { get; set; }
        public string Cheque_Infomation { get; set; }
        public int Transaction_Number { get; set; }

    }
}
