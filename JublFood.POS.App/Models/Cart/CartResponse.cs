using JublFood.POS.App.Models.Order;
using System;
using System.Collections.Generic;

namespace JublFood.POS.App.Models.Cart
{
    public class CartResponse
    {
        public String ResponseCode { get; set; }
        public String ResponseMessage { get; set; }        
        public CartHeader cartHeader { get; set; }
        public JublFood.POS.App.Models.Customer.Customer Customer { get; set; }
        public List<CartItem> cartItems { get; set; }
        public List<OrderGiftCard> orderGiftCards { get; set; }
        //public List<OrderPayment> orderPayments { get; set; }
        public OrderUDT orderUDT { get; set; }

        public List<ItemCombo> itemCombos { get; set; }
        public List<OrderPayment> orderPayments { get; set; }
        public List<OrderCreditCard> orderCreditCards { get; set; }
        public List<OrderReason> orderReasons { get; set; }

        public List<OrderAdditionalCharge> orderAdditionalCharges { get; set; }

    }
}
