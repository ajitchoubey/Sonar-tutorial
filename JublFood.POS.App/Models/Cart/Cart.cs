using JublFood.POS.App.Cache;
using JublFood.POS.App.Models.Order;
using System;
using System.Collections.Generic;

namespace JublFood.POS.App.Models.Cart
{
    public class Cart
    {        
        public CartHeader cartHeader { get; set; }
        public JublFood.POS.App.Models.Customer.Customer Customer { get; set; }
        public List<CartItem> cartItems { get; set; }
        public List<OrderGiftCard> orderGiftCards { get; set; }
        //public List<OrderPayment> orderPayments { get; set; }
        public OrderUDT orderUDT { get; set; }
        public List<OrderReason> orderReasons { get; set; }
        public List<ItemCombo> itemCombos { get; set; }
        public List<OrderPayment> orderPayments { get; set; }
        public List<OrderCreditCard> orderCreditCards { get; set; }
        public List<OrderAdditionalCharge> orderAdditionalCharges { get; set; }

        public Cart GetCart()
        {
            Cart cart = new Cart();

            CartHeader cartHeader = new CartHeader();
            JublFood.POS.App.Models.Customer.Customer customer = new JublFood.POS.App.Models.Customer.Customer();
            List<CartItem> cartItems = new List<CartItem>();
            List<OrderGiftCard> orderGiftCards = new List<OrderGiftCard>();
           
            OrderUDT orderUDTs = new OrderUDT();
            OrderReason orderReason = new OrderReason();
            List<OrderReason> orderReasons = new List<OrderReason>();
            List<ItemCombo> itemCombos = new List<ItemCombo>();
            List<OrderPayment> orderpayments = new List<OrderPayment>();
            List<OrderCreditCard> orderCreditCards = new List<OrderCreditCard>();
            OrderPayment orderpayment = new OrderPayment();
            OrderCreditCard orderCreditCard = new OrderCreditCard();
            List<ItemAttributeGroup> itemAttributeGroups = new List<ItemAttributeGroup>();
            List<ItemAttribute> itemAttributes = new List<ItemAttribute>();
            ItemCombo itemCombo = new ItemCombo();
            List<OrderAdditionalCharge> orderAdditionalCharges = new List<OrderAdditionalCharge>();
            List<ItemGiftCard> itemGiftCards = new List<ItemGiftCard>();
            List<ItemOptionGroup> itemOptionGroups = new List<ItemOptionGroup>();
            List<ItemOption> itemOptions = new List<ItemOption>();
            List<ItemReason> itemReasons = new List<ItemReason>();
            List<ItemSpecialtyPizza> itemSpecialityPizzas = new List<ItemSpecialtyPizza>();
            ItemUDT itemUDT = new ItemUDT();
            CartItem cartItem = new CartItem();


            cartItem.itemAttributeGroups = itemAttributeGroups;
            cartItem.itemAttributes = itemAttributes;            
            cartItem.itemGiftCards = itemGiftCards;
            cartItem.itemOptionGroups = itemOptionGroups;
            cartItem.itemOptions = itemOptions;
            cartItem.itemReasons = itemReasons;
            cartItem.itemSpecialtyPizzas = itemSpecialityPizzas;
            cartItem.itemUDT = itemUDT;

            //cartItems.Add(cartItem);

            cart.cartHeader = cartHeader;
            cart.Customer = customer;
            cart.cartItems = cartItems;
            cart.orderGiftCards = orderGiftCards;
            
            cart.orderUDT = orderUDTs;
            cart.orderReasons = orderReasons;
            cart.itemCombos = itemCombos;
            cart.orderPayments = orderpayments;
            cart.orderCreditCards = orderCreditCards;
            cart.orderAdditionalCharges = orderAdditionalCharges;

            return cart;
        }

        public Cart GetCartWithDefaultValues()
        {
            Cart cart = new Cart();

            CartHeader cartHeader = new CartHeader();
            JublFood.POS.App.Models.Customer.Customer customer = new JublFood.POS.App.Models.Customer.Customer();
            List<CartItem> cartItems = new List<CartItem>();
            List<OrderGiftCard> orderGiftCards = new List<OrderGiftCard>();
            List<OrderPayment> orderPayments = new List<OrderPayment>();
            OrderUDT orderUDTs = new OrderUDT();
            OrderReason orderReason = new OrderReason();
            List<ItemCombo> itemCombos = new List<ItemCombo>();

            List<OrderReason> orderReasons = new List<OrderReason>();
            List<ItemAttributeGroup> itemAttributeGroups = new List<ItemAttributeGroup>();
            List<ItemAttribute> itemAttributes = new List<ItemAttribute>();
            ItemCombo itemCombo = new ItemCombo();
            List<ItemGiftCard> itemGiftCards = new List<ItemGiftCard>();
            List<ItemOptionGroup> itemOptionGroups = new List<ItemOptionGroup>();
            List<ItemOption> itemOptions = new List<ItemOption>();
            List<ItemReason> itemReasons = new List<ItemReason>();
            List<ItemSpecialtyPizza> itemSpecialityPizzas = new List<ItemSpecialtyPizza>();
            ItemUDT itemUDT = new ItemUDT();
            CartItem cartItem = new CartItem();

            cartItem.itemAttributeGroups = itemAttributeGroups;
            cartItem.itemAttributes = itemAttributes;
            cartItem.itemGiftCards = itemGiftCards;
            cartItem.itemOptionGroups = itemOptionGroups;
            cartItem.itemOptions = itemOptions;
            cartItem.itemReasons = itemReasons;
            cartItem.itemSpecialtyPizzas = itemSpecialityPizzas;
            cartItem.itemUDT = itemUDT;

            //cartItems.Add(cartItem);

            cart.cartHeader = FillCartHeaderDefault();
            cart.Customer = customer;
            cart.cartItems = cartItems;
            cart.orderGiftCards = orderGiftCards;
            //cartResponse.orderPayments = orderPayments;
            cart.orderUDT = orderUDTs;
            cart.orderReasons = orderReasons;
            cart.itemCombos = itemCombos;

            return cart;
        }

        public CartHeader FillCartHeaderDefault()
        {
            CartHeader localCartHeader = new CartHeader();
            localCartHeader.CartId = Session.cart.cartHeader.CartId;
            localCartHeader.LocationCode = Session._LocationCode;
            localCartHeader.Order_Date = Session.cart.cartHeader.Order_Date == DateTime.MinValue ? Session.SystemDate : Session.cart.cartHeader.Order_Date;
            localCartHeader.ctlAddressCity = ""; //Session.ctlAddressCity;
            localCartHeader.Actual_Order_Date = Convert.ToDateTime(SystemSettings.settings.pdtmServerDateTime);
            localCartHeader.Customer_Code = 0;
            localCartHeader.Customer_Name = "";
            localCartHeader.Computer_Name = Session.ComputerName;
            localCartHeader.Order_Status_Code = 1; // TO DO 
            localCartHeader.Order_Taker_ID = Session.CurrentEmployee.LoginDetail.EmployeeCode;
            localCartHeader.Order_Taker_Shift = Convert.ToString(Session.CurrentEmployee.LoginDetail.DateShiftNumber); //TO DO
            localCartHeader.Order_Time = 0;
            localCartHeader.Order_Type_Code = Session.selectedOrderType;
            localCartHeader.Added_By = Session.CurrentEmployee.LoginDetail.EmployeeCode;
            localCartHeader.Workstation_ID = Session._WorkStationID;
            localCartHeader.Modifying = "";
            localCartHeader.Customer_Room = "";
            localCartHeader.Comments = "";
            localCartHeader.Coupon_Code = "";
            localCartHeader.Driver_ID = "";
            localCartHeader.Driver_Shift = "";
            localCartHeader.Credit_Card_Name = "";
            localCartHeader.Tent_Number = "";
            localCartHeader.Secure_Coupon_ID = "";
            localCartHeader.ROI_Customer = "";
            localCartHeader.Instruction = "";
            localCartHeader.Types = "";
            localCartHeader.Device_Type = "";
            localCartHeader.Platform = "";
            localCartHeader.Browser = "";
            localCartHeader.Payment_Gateway = "";
            localCartHeader.CustomField1 = "";
            localCartHeader.CustomField2 = "";
            localCartHeader.CustomField3 = "";
            localCartHeader.CustomField4 = "";
            localCartHeader.CustomField5 = "";
            localCartHeader.CustomField7 = "";
            localCartHeader.CustomField8 = "";
            localCartHeader.CustomField9 = "";
            localCartHeader.CustomField10 = "";
            localCartHeader.OTS_Number = "";
            localCartHeader.Delayed_Date = Session.cart.cartHeader.Delayed_Date;
            localCartHeader.Delayed_Same_Day = Session.cart.cartHeader.Delayed_Same_Day;
            localCartHeader.Kitchen_Display_Time = Session.cart.cartHeader.Kitchen_Display_Time;
            localCartHeader.SubTotal = 0;
            localCartHeader.Final_Total = 0;
            return localCartHeader;
        }

    }
}