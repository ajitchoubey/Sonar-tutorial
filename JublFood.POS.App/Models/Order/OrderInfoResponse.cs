using JublFood.POS.App.Models.Cart;
using System.Collections.Generic;

namespace JublFood.POS.App.Models.Order
{
    public class OrderInfoResponse
    {
        public string ResponseStatus { get; set; }
        public string ResponseStatusCode { get; set; }
        public CartHeader cartHeader { get; set; }
        public List<CartItem> cartItems { get; set; }
        public List<OrderPayment> orderPayments { get; set; }
        public OrderUDT orderUDT { get; set; }
        public List<ItemUDT> itemUDTs { get; set; }
        public List<OrderCreditCard> orderCreditCards { get; set; }

        public List<ItemCombo> itemCombos { get; set; }

        public List<ItemAttribute> itemAttributes { get; set; }
        public List<ItemOption> itemOptions { get; set; }
        public List<ItemSpecialtyPizza> itemSpecialtyPizzas { get; set; }

        public List<ItemReason> itemReasons { get; set; }
        public List<OrderReason> orderReasons { get; set; }
        public List<ItemAttributeGroup> itemAttributeGroups { get; set; }
        public List<ItemOptionGroup> itemOptionGroups { get; set; }
        public List<OrderAdditionalCharge> orderAdditionalCharges { get; set; }
    }
}
