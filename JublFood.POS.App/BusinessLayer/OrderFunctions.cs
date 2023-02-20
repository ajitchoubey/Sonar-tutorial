using JublFood.POS.App.API;
using JublFood.POS.App.Cache;
using JublFood.POS.App.Class;
using JublFood.POS.App.Models;
using JublFood.POS.App.Models.Cart;
using JublFood.POS.App.Models.Customer;
using JublFood.POS.App.Models.Order;
using JublFood.POS.App.Models.Payment;
using JublFood.POS.App.Models.Catalog;
using JublFood.POS.App.Models.Employee;
using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Jublfood.AppLogger;

namespace JublFood.POS.App.BusinessLayer
{
    public static class OrderFunctions
    {
        public static string Open_farm;
       
        
        public static UC_FunctionList UC_FunctionList = new UC_FunctionList();

        public static void LoadOrderDetails(long orderNumber, DateTime orderDate, bool blnModify, bool blnHistory,
                                        bool blnRefresh, bool blnCashOutScreen, string customerPhoneNumber)
        {
            bool blnLoginSuccessful = false;
            decimal curOrigDelFee;
            int selectedLineNumber = 0;
            EmployeeResult oldLoginEmployee;
                
            Session.originalresponsePayment = new ResponsePayment();
            Customer customer = new Customer();
            if (!blnHistory && blnModify)
                Session.pblnModifyingOrder = true;

            OriginalOrderInfo originalOrderInfos = new OriginalOrderInfo();
            originalOrderInfos = APILayer.LoadOriginalOrderInfo(SystemSettings.LocationCodes.LocationCode, orderDate, orderNumber, blnHistory);
           
            if (!blnCashOutScreen)
            {
                if (blnModify &&
                    ((originalOrderInfos.cartHeader.Order_Type_Code == "D" && ((originalOrderInfos.cartHeader.Order_Status_Code >= 3) ||
                    (originalOrderInfos.cartHeader.Order_Status_Code < 3 && originalOrderInfos.cartHeader.Pay_Now))) ||
                    (originalOrderInfos.cartHeader.Order_Type_Code != "D" && originalOrderInfos.cartHeader.Pay_Now))

                )
                {
                    #region blnModify and delivery condition
                    DialogResult result = CustomMessageBox.Show(APILayer.GetCatalogText(LanguageConstant.cintMSGAlreadyRouted), CustomMessageBox.Buttons.YesNo,CustomMessageBox.Icon.Question);
                    switch (result)
                    {
                        case DialogResult.Yes:
                            if (SystemSettings.settings.pblnReqPasswordCompletedOrders)
                            {
                              

                                if (!string.IsNullOrEmpty(Session.CurrentEmployee.LoginDetail.EmployeeCode) && Session.CurrentEmployee.LoginDetail.blnReqPasswordCompletedOrders == true) //Session.CurrentEmployee.LoginDetail.blnReqPasswordCompletedOrders
                                {
                                    if (SystemSettings.settings.pblnRequirePasswordForSpecialAccess)
                                    {
                                        if (EmployeeFunctions.MatchEmployeePassword())
                                        {
                                            blnLoginSuccessful = true;
                                        }
                                        else
                                        {
                                            blnLoginSuccessful = false;
                                        }
                                    }
                                    else
                                    {
                                        oldLoginEmployee = Session.CurrentEmployee;

                                        frmLogin frm = new frmLogin();
                                        frm.SpecialAccess = true;
                                        frm.Text = APILayer.GetCatalogText(LanguageConstant.cintSpecialAccess);
                                        frm.RequirePassword = true;
                                        frm.ShowDialog();

                                        if (Session.CurrentEmployee != null && Session.CurrentEmployee.LoginDetail != null)
                                        {
                                            if (!string.IsNullOrEmpty(Session.CurrentEmployee.LoginDetail.EmployeeCode) && Session.CurrentEmployee.LoginDetail.blnReqPasswordCompletedOrders == true) //Session.CurrentEmployee.LoginDetail.blnReqPasswordCompletedOrders
                                            {
                                                blnLoginSuccessful = true;
                                            }
                                            else
                                            {
                                                blnLoginSuccessful = false;
                                                CustomMessageBox.Show(APILayer.GetCatalogText(LanguageConstant.cintMSGInsufficientPrivileges), CustomMessageBox.Buttons.OK, CustomMessageBox.Icon.Warning);
                                                Session.pblnModifyingOrder = false;
                                                //return;
                                            }
                                        }
                                        else
                                            blnLoginSuccessful = false;
                                        Session.CurrentEmployee=oldLoginEmployee;
                                    }

                                    if (!blnLoginSuccessful)
                                    {
                                        originalOrderInfos = new OriginalOrderInfo();
                                        Session.cart = (new Cart().GetCart());
                                        Session.originalcart = (new Cart().GetCart());
                                        Session.pblnModifyingOrder = false;
                                        return;
                                    }
                                }
                                
                            }
                            else
                            {
                                if (!string.IsNullOrEmpty(Session.CurrentEmployee.LoginDetail.EmployeeCode) && !Session.CurrentEmployee.LoginDetail.blnReqPasswordCompletedOrders) //Session.CurrentEmployee.LoginDetail.blnReqPasswordCompletedOrders
                                {
                                    oldLoginEmployee=Session.CurrentEmployee;
                                    frmLogin frm = new frmLogin();
                                    frm.SpecialAccess = true;
                                    frm.Text = APILayer.GetCatalogText(LanguageConstant.cintSpecialAccess);
                                    frm.RequirePassword = true;
                                    frm.ShowDialog();

                                    if (Session.CurrentEmployee != null && Session.CurrentEmployee.LoginDetail != null)
                                    {
                                        if (!string.IsNullOrEmpty(Session.CurrentEmployee.LoginDetail.EmployeeCode) && Session.CurrentEmployee.LoginDetail.blnReqPasswordCompletedOrders == true) //Session.CurrentEmployee.LoginDetail.blnReqPasswordCompletedOrders
                                        {
                                            blnLoginSuccessful = true;
                                        }
                                        else
                                        {
                                            blnLoginSuccessful = false;
                                            CustomMessageBox.Show(APILayer.GetCatalogText(LanguageConstant.cintMSGInsufficientPrivileges), CustomMessageBox.Buttons.OK, CustomMessageBox.Icon.Warning);
                                            Session.pblnModifyingOrder = false;
                                            //return;
                                        }
                                    }
                                    else
                                        blnLoginSuccessful = false;
                                    Session.CurrentEmployee = oldLoginEmployee;

                                    if (!blnLoginSuccessful)
                                    {
                                        originalOrderInfos = new OriginalOrderInfo();
                                        Session.cart = (new Cart().GetCart());
                                        Session.originalcart = (new Cart().GetCart());
                                        Session.pblnModifyingOrder = false;
                                        return;
                                    }
                                }

                            }
                            break;
                        case DialogResult.No:
                            originalOrderInfos = new OriginalOrderInfo();
                            Session.cart = (new Cart().GetCart());
                            Session.originalcart = (new Cart().GetCart());
                            Session.pblnModifyingOrder = false;
                            return;
                        case DialogResult.Cancel:
                            originalOrderInfos = new OriginalOrderInfo();
                            Session.cart = (new Cart().GetCart());
                            Session.originalcart = (new Cart().GetCart());
                            Session.pblnModifyingOrder = false;
                            return;
                    }

                }
                #endregion 
                else
                {
                    #region blnModify
                    if (blnModify)
                    {
                        if (!string.IsNullOrEmpty(Session.CurrentEmployee.LoginDetail.EmployeeCode) && !Session.CurrentEmployee.LoginDetail.blnModifyOpenOrders) //Session.CurrentEmployee.LoginDetail.blnModifyOpenOrders
                        {
                            oldLoginEmployee = Session.CurrentEmployee;
                            frmLogin frm = new frmLogin();
                            frm.SpecialAccess = true;
                            frm.Text = APILayer.GetCatalogText(LanguageConstant.cintSpecialAccess);
                            frm.RequirePassword = true;
                            frm.ShowDialog();

                            if (Session.CurrentEmployee != null && Session.CurrentEmployee.LoginDetail != null)
                            {
                                if (!string.IsNullOrEmpty(Session.CurrentEmployee.LoginDetail.EmployeeCode) && Session.CurrentEmployee.LoginDetail.blnModifyOpenOrders) //Session.CurrentEmployee.LoginDetail.blnModifyOpenOrders
                                {
                                    blnLoginSuccessful = true;
                                }
                                else
                                {
                                    blnLoginSuccessful = false;
                                    CustomMessageBox.Show(APILayer.GetCatalogText(LanguageConstant.cintMSGInsufficientPrivileges), CustomMessageBox.Buttons.OK, CustomMessageBox.Icon.Warning);
                                    Session.pblnModifyingOrder = false;
                                    //return;
                                }
                            }
                            else
                                blnLoginSuccessful = false;
                            Session.CurrentEmployee=oldLoginEmployee;
                            if (!blnLoginSuccessful)
                            {
                                originalOrderInfos = new OriginalOrderInfo();
                                Session.cart = (new Cart().GetCart());
                                Session.originalcart = (new Cart().GetCart());
                                return;
                            }
                        }
                        else
                        {
                            blnLoginSuccessful = true;
                        }
                    }
                    #endregion 
                    else
                    {
                        blnLoginSuccessful = true;
                    }
                }

            }


                          
            Session.originalcart = ConvertOriginalCart(originalOrderInfos, customerPhoneNumber);

            if (!blnHistory && Session.RemakeOrder == false)
            {

                //ConvertOriginalCartToMainCart(Session.originalcart, ref selectedLineNumber);
                CartFunctions.GenerateOrderCart(true, null,true);
                CartFunctions.FillCartToCustomer();

            }


            if (!blnHistory && !Session.RemakeOrder)
            {
                curOrigDelFee = originalOrderInfos.cartHeader.Delivery_Fee;
                POSUpdateOrderModifyingStatus(Session._LocationCode, orderNumber, orderDate, Session.CurrentEmployee.LoginDetail.EmployeeCode, Session.pblnModifyingOrder);

            }
            if (!blnHistory && !Session.RemakeOrder)
            {
                //CustomerFunction.FillAddressScreen();
                Session.cart.cartHeader.Current_Sequence = GetOrderLineMaxSequence();
                Session.originalcart.cartHeader.Current_Sequence = GetOrderLineMaxSequence();
            }


            LoadOrderFormatOrderScreen(blnHistory);
            // LoadOrderDisplayOrderScreen(null, blnHistory, blnModify, 0, 0);
        }


        public static Cart ConvertOriginalCart(OriginalOrderInfo orderInfo, string customerPhoneNumber = "")
        {
            //Session.originalcart = new Cart().GetCart();
            Cart localcart = (new Cart().GetCart());
            localcart.cartHeader = new CartHeader();
            localcart.Customer = new Customer();
            localcart.cartItems = new List<CartItem>();
            localcart.orderReasons = new List<OrderReason>();
            localcart.itemCombos = new List<ItemCombo>();


            List<CartItem> cartItems = new List<CartItem>();
            List<OrderPayment> orgOrderPayments = new List<OrderPayment>();
            List<OrderCreditCard> orgorderCreditCards = new List<OrderCreditCard>();
            Customer customer = new Customer();
            OrderUDT orderUDT = new OrderUDT();
            List<ItemUDT> itemUDTs = new List<ItemUDT>();
            ItemUDT itemUDT = new ItemUDT();
            CatalogMenuItems catalogMenu = new CatalogMenuItems();
            CatalogPOSComboMealItems catalogCombo  = new CatalogPOSComboMealItems();
            //Session.comboMenuItems
            localcart.cartHeader = orderInfo.cartHeader;

            orgOrderPayments = orderInfo.orderPayments;
            orgorderCreditCards = orderInfo.orderCreditCards;
            localcart.orderReasons = orderInfo.orderReasons;
            localcart.orderPayments = orgOrderPayments;
            localcart.orderCreditCards = orgorderCreditCards;
            localcart.orderAdditionalCharges = orderInfo.orderAdditionalCharges;
            Session.originalresponsePayment.payment = orgOrderPayments;
            Session.originalresponsePayment.orderCreditCard = orgorderCreditCards;

     

            if (orderInfo != null && orderInfo.cartHeader != null && orderInfo.cartHeader.Customer_Code != 0 && !string.IsNullOrEmpty(customerPhoneNumber))
            {
                customer = CustomerFunction.GetCustomer(orderInfo.cartHeader.LocationCode, customerPhoneNumber, "");
            }
            else
            {
                LoadDefaultCustomer(ref customer, orderInfo.cartHeader.LocationCode,orderInfo.cartHeader.Customer_Name);
            }
            localcart.Customer = customer;
            localcart.cartHeader.ctlAddressCity = localcart.Customer.City;

            if (SystemSettings.settings.pbytTaxStructure < 4)
            {
                if (!localcart.Customer.Tax_Exempt1 && !localcart.Customer.Tax_Exempt2)
                {
                    if (localcart.Customer.TaxRate1 > 0)
                    {
                        localcart.cartHeader.TaxRate1 = localcart.Customer.TaxRate1;
                    }
                    else
                    {
                        if (localcart.cartHeader.Order_Type_Code == "C" || localcart.cartHeader.Order_Type_Code == "P")
                        {
                            localcart.cartHeader.TaxRate1 = SystemSettings.settings.psngCarryOutTax_Rate1;
                        }
                        else
                        {
                            localcart.cartHeader.TaxRate1 = SystemSettings.settings.psngTax_Rate1;
                        }
                    }

                    if (localcart.Customer.TaxRate2 > 0)
                    {
                        localcart.cartHeader.TaxRate2 = customer.TaxRate2;
                    }
                    else
                    {
                        if (localcart.cartHeader.Order_Type_Code == "C" || localcart.cartHeader.Order_Type_Code == "P")
                        {
                            localcart.cartHeader.TaxRate2 = SystemSettings.settings.psngCarryOutTax_Rate2;
                        }
                        else
                        {
                            localcart.cartHeader.TaxRate2 = SystemSettings.settings.psngTax_Rate2;
                        }
                    }
                }

                orderUDT = new OrderUDT();
            }
            else
            {
                orderUDT = orderInfo.orderUDT;
                itemUDT = new ItemUDT();
                itemUDTs = orderInfo.itemUDTs;

            }
            localcart.orderUDT = orderUDT;

            List<ItemCombo> itemCombos = new List<ItemCombo>();
            localcart.itemCombos = orderInfo.itemCombos;
            if (localcart.itemCombos != null)
            {
                foreach (ItemCombo itemCombo in localcart.itemCombos)
                {
                    itemCombo.Combo_Description = itemCombo.Combo_Size_Description + " " + itemCombo.Combo_Menu_Description;
                }
            }
            //attributeGroups----
            List<ItemAttributeGroup> attributeGroups = new List<ItemAttributeGroup>();
            ItemAttributeGroup itemAttributeGroup = new ItemAttributeGroup();
            attributeGroups = orderInfo.itemAttributeGroups;


            //itemAttributes----------
            List<ItemAttribute> itemAttributes = new List<ItemAttribute>();
            ItemAttribute itemAttribute = new ItemAttribute();
            itemAttributes = orderInfo.itemAttributes;

            List<ItemOptionGroup> itemOptionGroups = new List<ItemOptionGroup>();
            ItemOptionGroup itemOptionGroup = new ItemOptionGroup();
            itemOptionGroups = orderInfo.itemOptionGroups;

            List<ItemOption> itemOptions = new List<ItemOption>();
            ItemOption itemOption = new ItemOption();
            itemOptions = orderInfo.itemOptions;

            List<ItemReason> itemReasons = new List<ItemReason>();
            ItemReason itemReason = new ItemReason();
            itemReasons = orderInfo.itemReasons;


            List<ItemSpecialtyPizza> itemSpecialtyPizzas = new List<ItemSpecialtyPizza>();
            ItemSpecialtyPizza itemSpecialtyPizza = new ItemSpecialtyPizza();
            itemSpecialtyPizzas = orderInfo.itemSpecialtyPizzas;


            //List<OrderReason> orderReasons = new List<OrderReason>();
            //OrderReason orderReason = new OrderReason();
            //orderReasons = orderInfo.orderReasons;


            //foreach (var orderreson in orderReasons)
            //{
            //    //localcart.orderReason.CartId = localcart.cartHeader.CartId;
            //    localcart.orderReason.Location_Code = orderreson.Location_Code;
            //    localcart.orderReason.Order_Number = orderreson.Order_Number;
            //    localcart.orderReason.Order_Date = orderreson.Order_Date;
            //    localcart.orderReason.Reason_Sequence = orderreson.Reason_Sequence;
            //    localcart.orderReason.Reason_Group_Code = orderreson.Reason_Group_Code;
            //    localcart.orderReason.Reason_ID = orderreson.Reason_ID;
            //    localcart.orderReason.Other_Information = orderreson.Other_Information;
            //    localcart.orderReason.Deleted = orderreson.Deleted;
            //    localcart.orderReason.Added_By = orderreson.Added_By;
            //    localcart.orderReason.Reason_Description = orderreson.Reason_Description;

            //}


            if (!Session.blnCheckLine)
            {
                cartItems = orderInfo.cartItems;
                foreach (var cartitemvalue in cartItems)
                {
                    CartItem cartItem = new CartItem();


                    cartItem.CartId = cartitemvalue.CartId;
                    cartItem.Location_Code = cartitemvalue.Location_Code;
                    cartItem.Order_Number = cartitemvalue.Order_Number;
                    cartItem.Order_Date = cartitemvalue.Order_Date;
                    cartItem.Line_Number = cartitemvalue.Line_Number;
                    cartItem.Sequence = cartitemvalue.Sequence;
                    cartItem.Order_Line_Status_Code = cartitemvalue.Order_Line_Status_Code;
                    cartItem.Oven_Time = cartitemvalue.Oven_Time;
                    cartItem.Quantity = cartitemvalue.Quantity;
                    cartItem.Combo_Menu_Code = cartitemvalue.Combo_Menu_Code;
                    cartItem.Combo_Size_Code = cartitemvalue.Combo_Size_Code;
                    cartItem.Combo_Group = cartitemvalue.Combo_Group;

                    cartItem.Menu_Type_ID = cartitemvalue.Menu_Type_ID;
                    cartItem.Menu_Category_Code = cartitemvalue.Menu_Category_Code;
                    cartItem.Menu_Code = cartitemvalue.Menu_Code;
                    cartItem.Size_Code = cartitemvalue.Size_Code;
                    cartItem.Instruction_String = cartitemvalue.Instruction_String;
                    cartItem.Coupon_Code = cartitemvalue.Coupon_Code;
                    cartItem.Coupon_Description = cartitemvalue.Coupon_Description;
                    cartItem.Coupon_Taxable = cartitemvalue.Coupon_Taxable;
                    cartItem.Coupon_Type_Code = cartitemvalue.Coupon_Type_Code;
                    cartItem.Coupon_Adjustment = cartitemvalue.Coupon_Adjustment;
                    cartItem.Coupon_Amount = cartitemvalue.Coupon_Amount; //condition 
                    cartItem.Coupon_Min_Price = cartitemvalue.Coupon_Min_Price; //condition
                    cartItem.Price = cartitemvalue.Price;   //condition
                    cartItem.SubTotal = cartitemvalue.Price;
                    cartItem.Menu_Price = cartitemvalue.Menu_Price;
                    cartItem.Menu_Price2 = cartitemvalue.Menu_Price2;
                    cartItem.Bottle_Deposit = cartitemvalue.Bottle_Deposit;
                    cartItem.Deleted = cartitemvalue.Deleted;
                    cartItem.Topping_Codes = cartitemvalue.Topping_Codes;
                    cartItem.Topping_Descriptions = cartitemvalue.Topping_Descriptions;
                    cartItem.Topping_String = cartitemvalue.Topping_String;
                    cartItem.Calculate_IFC = cartitemvalue.Calculate_IFC;
                    cartItem.Doubles_Group = cartitemvalue.Doubles_Group;
                    cartItem.Order_Saved_Time = cartitemvalue.Order_Saved_Time;
                    cartItem.Modifying = cartitemvalue.Modifying;
                    cartItem.Order_Coupon_Amount = cartitemvalue.Order_Coupon_Amount;
                    cartItem.Order_Line_Coupon_Amount = cartitemvalue.Order_Line_Coupon_Amount;
                    cartItem.Order_Line_No_Tax_Discount = cartitemvalue.Order_Line_No_Tax_Discount;
                    cartItem.Order_Line_Tax_Discount = cartitemvalue.Order_Line_Tax_Discount;
                    cartItem.Order_No_Tax_Discount = cartitemvalue.Order_No_Tax_Discount;
                    cartItem.Order_Tax_Discount = cartitemvalue.Order_Tax_Discount;
                    cartItem.Credit_Discount = cartitemvalue.Credit_Discount;
                    cartItem.Order_Line_Taxable_Sale1 = cartitemvalue.Order_Line_Taxable_Sale1;
                    cartItem.Order_Line_Taxable_Sale2 = cartitemvalue.Order_Line_Taxable_Sale2;
                    cartItem.Order_Line_Non_Taxable_Sale = cartitemvalue.Order_Line_Non_Taxable_Sale;
                    cartItem.Order_Line_Tax1 = cartitemvalue.Order_Line_Tax1;
                    cartItem.Order_Line_Tax2 = cartitemvalue.Order_Line_Tax2;
                    cartItem.Order_Line_Total = cartitemvalue.Order_Line_Total;
                    cartItem.Added_By = cartitemvalue.Added_By;
                    cartItem.Menu_Description = cartitemvalue.Menu_Description;
                    cartItem.Size_Description = cartitemvalue.Size_Description;
                    cartItem.Pizza = cartitemvalue.Pizza;
                    cartItem.Menu_Item_Taxable = cartitemvalue.Menu_Item_Taxable;
                    cartItem.Prepared = cartitemvalue.Prepared;
                    cartItem.Kitchen_Device_Count = cartitemvalue.Kitchen_Device_Count;
                    cartItem.Item_Modified = false;
                    cartItem.New_Item = false;
                    cartItem.Orig_Menu_Code = cartitemvalue.Orig_Menu_Code;
                    cartItem.Prompt_For_Size = cartitemvalue.Prompt_For_Size;
                    cartItem.Combo_Discount = cartitemvalue.Combo_Discount;
                    cartItem.Combo_Item_Number = cartitemvalue.Combo_Item_Number;
                    cartItem.Combo_MaxAmount = cartitemvalue.Combo_MaxAmount;
                    cartItem.Combo_MinAmount = cartitemvalue.Combo_MinAmount;
                    cartItem.Size_Chosen = true;
                    cartItem.Menu_Item_Choosen = true;
                    cartItem.Order_Line_Complete = true;
                    cartItem.Base_Price = cartitemvalue.Base_Price;
                    cartItem.Base_Price2 = cartitemvalue.Base_Price2;
                    cartItem.Manual = cartitemvalue.Manual;
                    cartItem.Topping_Group_Code = cartitemvalue.Topping_Group_Code;
                    cartItem.Topping_Group_Price_By_Number = cartitemvalue.Topping_Group_Price_By_Number;
                    cartItem.Topping_Count = cartitemvalue.Topping_Count;
                    cartItem.Doubles_Bypassed = cartitemvalue.Doubles_Bypassed;
                    cartItem.NumberOfSizes = cartitemvalue.NumberOfSizes;
                    cartItem.NumberOfAttributes = cartitemvalue.NumberOfAttributes;
                    cartItem.NumberOfOptions = cartitemvalue.NumberOfOptions;
                    cartItem.Price_By_Weight = cartitemvalue.Price_By_Weight;
                    cartItem.Tare_Weight = cartitemvalue.Tare_Weight;
                    cartItem.Menu_Item_Type_Code = cartitemvalue.Menu_Item_Type_Code;
                    cartItem.Open_Value_Card = cartitemvalue.Open_Value_Card;
                    cartItem.Min_Amount_Open_Value_Card = cartitemvalue.Min_Amount_Open_Value_Card;
                    cartItem.Max_Amount_Open_Value_Card = cartitemvalue.Max_Amount_Open_Value_Card;
                    cartItem.Print_Nutritional_Label = cartitemvalue.Print_Nutritional_Label;
                    cartItem.Description = (cartitemvalue.Size_Description == "." || cartitemvalue.Size_Description == "-" ? "" : cartitemvalue.Size_Description + " ") + cartitemvalue.Menu_Description;
                    cartItem.PromptDoubles = false;
                    if (cartitemvalue.MenuItemType != null) cartItem.MenuItemType = cartitemvalue.MenuItemType;

                    ////manish 
                    catalogMenu = null;
                    if (Session.menuItems != null && Session.menuItems.Count > 0)
                    {
                        catalogMenu = Session.menuItems.Find(x => x.Menu_Code == cartitemvalue.Menu_Code);
                        if (catalogMenu != null)
                        {
                            cartItem.MenuItemType = catalogMenu.MenuItemType;
                        }
                    }
                    
                    if(catalogMenu == null)
                    {
                        List<CatalogMenuItems> currentMenuItems = APILayer.GetMenuItems(cartitemvalue.Menu_Category_Code, localcart.cartHeader.Order_Type_Code);
                        if (Session.menuItems == null) Session.menuItems = new List<CatalogMenuItems>();
                        CartFunctions.RemoveItemsOrderTypeFromMenuItems(localcart.cartHeader.Order_Type_Code);
                        foreach (CatalogMenuItems catalogMenuItems in currentMenuItems)
                        {
                            if (!Session.menuItems.Any(z => z.Menu_Code == catalogMenuItems.Menu_Code)) Session.menuItems.Add(catalogMenuItems);
                        }

                        catalogMenu = Session.menuItems.Find(x => x.Menu_Code == cartitemvalue.Menu_Code);
                        if (catalogMenu != null)
                        {
                            cartItem.MenuItemType = catalogMenu.MenuItemType;
                        }
                    }

                    if (!string.IsNullOrEmpty(cartitemvalue.Combo_Menu_Code) && !string.IsNullOrEmpty(cartitemvalue.Combo_Size_Code))
                    {
                        catalogCombo = null;
                        if (Session.comboMenuItems != null && Session.comboMenuItems.Count > 0)
                        {
                            catalogCombo = Session.comboMenuItems.Find(x => x.Combo_Menu_Code == cartitemvalue.Combo_Menu_Code && x.Combo_Size_Code == cartitemvalue.Combo_Size_Code && x.Menu_Code == cartitemvalue.Menu_Code && x.Item_Number == cartitemvalue.Combo_Item_Number);
                        }

                        if (catalogCombo == null)
                        {

                            List<CatalogPOSComboMealItems> currentMenuCombos = APILayer.GetComboMealItems(cartitemvalue.Combo_Menu_Code, cartitemvalue.Combo_Size_Code);

                            if (Session.comboMenuItems == null) Session.comboMenuItems = new List<CatalogPOSComboMealItems>();
                            foreach (CatalogPOSComboMealItems catalogPOSCombo in currentMenuCombos)
                            {
                                if (!Session.comboMenuItems.Any(z => z.Combo_Menu_Code == catalogPOSCombo.Combo_Menu_Code && z.Combo_Size_Code == catalogPOSCombo.Combo_Size_Code && z.Menu_Code == catalogPOSCombo.Menu_Code && z.Item_Number == catalogPOSCombo.Item_Number)) Session.comboMenuItems.Add(catalogPOSCombo);
                            }
                        }
                    }

                    if (attributeGroups.Count > 0)
                    {
                        var attributeGroupscount = attributeGroups.Where(item => item.Sequence == cartItem.Sequence).Where(item => item.Line_Number == cartItem.Line_Number).ToList();
                        cartItem.itemAttributeGroups = new List<ItemAttributeGroup>();
                        foreach (var attributeGroupsvalue in attributeGroupscount)
                        {
                            cartItem.itemAttributeGroups.Add(attributeGroupsvalue);
                        }


                    }
                    if (itemAttributes.Count > 0)
                    {
                        var itemAttributescount = itemAttributes.Where(item => item.Sequence == cartItem.Sequence).Where(item => item.Line_Number == cartItem.Line_Number).ToList();
                        cartItem.itemAttributes = new List<ItemAttribute>();
                        foreach (var itemAttributevalue in itemAttributescount)
                        {
                            cartItem.itemAttributes.Add(itemAttributevalue);
                        }

                    }
                    //itemCombo=itemCombos.Find(item => item.Combo_Menu_Code == cartItem.Combo_Menu_Code)
                    if (itemOptionGroups.Count > 0)
                    {
                        var itemOptionGroupscount = itemOptionGroups.Where(item => item.Sequence == cartItem.Sequence).Where(item => item.Line_Number == cartItem.Line_Number).ToList();
                        cartItem.itemOptionGroups = new List<ItemOptionGroup>();
                        foreach (var itemoptiongroup in itemOptionGroupscount)
                        {
                            cartItem.itemOptionGroups.Add(itemoptiongroup);
                        }

                    }



                    if (itemOptions.Count > 0)
                    {

                        var itemOptionscount = itemOptions.Where(item => item.Sequence == cartItem.Sequence).Where(item => item.Line_Number == cartItem.Line_Number).ToList();
                        if (cartItem.itemOptions == null) cartItem.itemOptions = new List<ItemOption>();
                        foreach (var itemsoptions in itemOptionscount)
                        {
                            cartItem.itemOptions.Add(itemsoptions);
                        }
                        if (itemOptionscount.Count > 0)
                        {
                            if (!Convert.ToBoolean(cartItem.MenuItemType))
                            {
                                bool isNonVegTopping = false;
                                List<CatalogToppings> catalogToppings = APILayer.GetToppings(cartItem.Menu_Code, cartItem.itemOptionGroups[0].Option_Group_Code);
                                foreach (ItemOption itemOption1 in cartItem.itemOptions)
                                { 
                                    isNonVegTopping = Convert.ToBoolean(catalogToppings.Find(x => x.Menu_Code == itemOption1.Menu_Code).MenuItemType);
                                    if(isNonVegTopping) break;
                                }

                                if (isNonVegTopping)
                                    cartItem.MenuItemType = true;
                            }
                        }

                    }
                    if (itemReasons.Count > 0)
                    {
                        var itemReasoncount = itemReasons.Where(item => item.Sequence == cartItem.Sequence).Where(item => item.Line_Number == cartItem.Line_Number).ToList();
                        cartItem.itemReasons = new List<ItemReason>();
                        foreach (var reason in itemReasoncount)
                        {
                            cartItem.itemReasons.Add(reason);
                        }

                    }
                    if (itemSpecialtyPizzas.Count > 0)
                    {
                        var itemOptionscouitemSpecialtyPizzascount = itemSpecialtyPizzas.Where(item => item.Sequence == cartItem.Sequence).Where(item => item.Line_Number == cartItem.Line_Number).ToList();
                        cartItem.itemSpecialtyPizzas = new List<ItemSpecialtyPizza>();
                        foreach (var specialtyPizza in itemOptionscouitemSpecialtyPizzascount)
                        {
                            cartItem.itemSpecialtyPizzas.Add(specialtyPizza);
                        }
                        if(itemOptionscouitemSpecialtyPizzascount != null && itemOptionscouitemSpecialtyPizzascount.Count>0)
                        {
                            cartItem.Specialty_Pizza = true;
                            cartItem.Specialty_Pizza_Code = itemOptionscouitemSpecialtyPizzascount[0].Specialty_Pizza_Code;
                        }

                    }
                    if (itemUDTs.Count > 0)
                    {
                        cartItem.itemUDT = new ItemUDT();
                        itemUDT = itemUDTs.First(item => item.Sequence == cartItem.Sequence && item.Line_Number == cartItem.Line_Number);
                        cartItem.itemUDT = itemUDT;
                       if(itemUDT!=null)
                        {
                            var ODCTextValue = SystemSettings.GetSettingValue("ODC_Change_Tax_Rate", Session._LocationCode);
                            if (itemUDT.Tax_4_Rate == Convert.ToDouble(ODCTextValue) && localcart.cartHeader.Order_Type_Code == "P")
                              localcart.cartHeader.ODC_Tax = true;
                            else
                                localcart.cartHeader.ODC_Tax = false;
                        }
                    }

                    localcart.cartItems.Add(cartItem);
                }

            }
            if(Session.RemakeOrder)
            {

                localcart.cartHeader.SubTotal = localcart.cartHeader.SubTotal + localcart.cartHeader.Order_Line_Adjustments - localcart.cartHeader.Delivery_Fee;
                localcart.cartHeader.Order_Line_Adjustments = 0;
                localcart.cartHeader.Delivery_Fee = 0;
            }

            return localcart;
        }


        public static void ItemOptionForCart(ref CartItem cartItem, ref List<ItemOption> itemOptions)
        {
            if (itemOptions.Count > 0)
            {
                foreach (ItemOption itemOption1 in itemOptions)
                {

                    if (itemOption1.Order_Date == cartItem.Order_Date && itemOption1.Order_Number == cartItem.Order_Number &&
                        itemOption1.Line_Number == cartItem.Line_Number && itemOption1.Sequence == cartItem.Sequence)
                    {
                        ItemOption itemOption = new ItemOption();
                        itemOption.Location_Code = itemOption1.Location_Code;
                        itemOption.Order_Number = itemOption1.Order_Number;
                        itemOption.Order_Date = itemOption1.Order_Date;
                        itemOption.Line_Number = itemOption1.Line_Number;
                        itemOption.Sequence = itemOption1.Sequence;
                        itemOption.Menu_Option_Group_Code = itemOption1.Menu_Option_Group_Code;
                        itemOption.Menu_Code = itemOption1.Menu_Code;
                        itemOption.Size_Code = itemOption1.Size_Code;
                        itemOption.Pizza_Part = itemOption1.Pizza_Part;
                        itemOption.Amount_Code = itemOption1.Amount_Code;
                        cartItem.itemOptions.Add(itemOption);
                    }
                }


            }

        }

        public static void ConvertOriginalCartToMainCart(Cart originalCart, ref int selectedLineNumber)
        {

            CartFunctions.CheckCart();

            List<CatalogOptionGroups> catalogOptionGroups = null;
            Session.cart = originalCart;
            Session.cart.orderUDT = originalCart.orderUDT;
            Cart cartLocal = (new Cart().GetCart());
            cartLocal.cartHeader = originalCart.cartHeader;
            cartLocal.Customer = Session.cart.Customer;
            cartLocal.itemCombos = Session.cart.itemCombos;
            cartLocal.orderPayments = originalCart.orderPayments;
            cartLocal.orderCreditCards = originalCart.orderCreditCards;
            if (Session.cart.itemCombos == null)
            {
                cartLocal.itemCombos = new List<ItemCombo>();
            }

            cartLocal.orderReasons = originalCart.orderReasons;

            CartItem item = originalCart.cartItems[0];
            cartLocal.orderUDT = originalCart.orderUDT;
            CartItem cartItemLocal = new CartItem();

            cartItemLocal.Location_Code = Session._LocationCode;
            cartItemLocal.Order_Date = Session.SystemDate;
            cartItemLocal.Line_Number = 1;
            cartItemLocal.Sequence = 1;
            cartItemLocal.Menu_Type_ID = item.Menu_Type_ID;//Session._MenuTypeID;
            cartItemLocal.Menu_Category_Code = item.Menu_Category_Code; //Session.selectedMenuItem.Menu_Category_Code;
            cartItemLocal.Combo_Menu_Code = item.Combo_Menu_Code;
            cartItemLocal.Combo_Size_Code = item.Combo_Size_Code;
            cartItemLocal.Menu_Code = item.Menu_Code; //Session.selectedMenuItem.Menu_Code;
            cartItemLocal.Menu_Description = item.Menu_Description;//Session.selectedMenuItem.Order_Description;
            cartItemLocal.Prepared = item.Prepared; //Convert.ToBoolean(Session.selectedMenuItem.Prepared_YN);
            cartItemLocal.Menu_Item_Taxable = item.Menu_Item_Taxable;//Convert.ToInt32(Session.selectedMenuItem.Taxable);
            cartItemLocal.Pizza = item.Pizza; //Convert.ToBoolean(Session.selectedMenuItem.Pizza_YN);
            cartItemLocal.Size_Code = item.Size_Code;
            cartItemLocal.Size_Description = item.Size_Description;
            cartItemLocal.Quantity = 1;
            cartItemLocal.Added_By = Session.CurrentEmployee.LoginDetail.EmployeeCode;
            cartItemLocal.Kitchen_Device_Count = item.Kitchen_Device_Count; //Session.selectedMenuItem.Kitchen_Device_Count;
            cartItemLocal.Orig_Menu_Code = item.Orig_Menu_Code;//Session.selectedMenuItem.Orig_Menu_Code;
            cartItemLocal.Menu_Price = item.Menu_Price;
            cartItemLocal.Price = item.Price;
            cartItemLocal.Order_Line_Total = item.Order_Line_Total;
            cartItemLocal.Action = "A";
            cartItemLocal.Prompt_For_Size = item.Prompt_For_Size;//Session.selectedMenuItem.Prompt_For_Size;
            cartItemLocal.Order_Line_Complete = false;
            if (cartItemLocal.Kitchen_Device_Count > 0)
                cartItemLocal.Order_Line_Status_Code = 1;
            else
                cartItemLocal.Order_Line_Status_Code = 2;
            cartItemLocal.Base_Price = item.Menu_Price; //cartItemLocal.Menu_Price;
            cartItemLocal.Base_Price2 = item.Menu_Price2;//cartItemLocal.Menu_Price2;
            cartItemLocal.NumberOfSizes = item.NumberOfSizes; //Session.selectedMenuItem.NumberOfSizes;
            cartItemLocal.NumberOfAttributes = item.NumberOfAttributes; //Session.selectedMenuItem.NumberOfAttributes;
            cartItemLocal.NumberOfOptions = item.NumberOfOptions;//Session.selectedMenuItem.NumberOfOptions;
            cartItemLocal.Menu_Item_Type_Code = item.Menu_Item_Type_Code;//Session.selectedMenuItem.Menu_Item_Type_Code;
            cartItemLocal.Print_Nutritional_Label = item.Print_Nutritional_Label;//Convert.ToBoolean(Session.selectedMenuItem.Print_Nutritional_Label);
            cartItemLocal.Specialty_Pizza = item.Specialty_Pizza;//Convert.ToBoolean(Session.selectedMenuItem.Specialty_Pizza);
            cartItemLocal.Specialty_Pizza_Code = item.Specialty_Pizza_Code;//Session.selectedMenuItem.Specialty_Pizza_Code;
            cartItemLocal.Topping_String = item.Topping_String;
            cartItemLocal.Instruction_String = item.Instruction_String;
            cartItemLocal.Coupon_Code = item.Coupon_Code;
            cartItemLocal.Topping_Codes = item.Topping_Codes;
            cartItemLocal.Topping_Descriptions = item.Topping_Descriptions;
            cartItemLocal.Coupon_Description = item.Coupon_Description;
            cartItemLocal.Description = item.Menu_Description;

            if (item.itemAttributeGroups != null && item.itemAttributeGroups.Count > 0)
            {
                cartItemLocal.itemAttributeGroups = item.itemAttributeGroups;
            }
            if (item.itemAttributes != null && item.itemAttributes.Count > 0)
            {
                cartItemLocal.itemAttributes = item.itemAttributes;
            }
            if (item.itemGiftCards != null && item.itemGiftCards.Count > 0)
            {
                cartItemLocal.itemGiftCards = item.itemGiftCards;
            }
            if (item.itemOptionGroups != null && item.itemOptionGroups.Count > 0)
            {
                cartItemLocal.itemOptionGroups = item.itemOptionGroups;
            }
            if (item.itemOptions != null && item.itemOptions.Count > 0)
            {
                cartItemLocal.itemOptions = item.itemOptions;
            }
            if (item.itemReasons != null && item.itemReasons.Count > 0)
            {
                cartItemLocal.itemReasons = item.itemReasons;
            }
            if (item.itemSpecialtyPizzas != null && item.itemSpecialtyPizzas.Count > 0)
            {
                cartItemLocal.itemSpecialtyPizzas = item.itemSpecialtyPizzas;
            }
            if (item.itemUDT != null)
            {
                cartItemLocal.itemUDT = item.itemUDT;
            }
            cartLocal.cartItems.Add(cartItemLocal);


            //public static void ConvertOriginalCartToMainCart(Cart originalCart, ref int selectedLineNumber)
            //{

            //    CartFunctions.CheckCart();

            //    List<CatalogOptionGroups> catalogOptionGroups = null;
            //    Session.cart = originalCart;
            //    Session.cart.orderUDT = originalCart.orderUDT;
            //    Cart cartLocal = (new Cart().GetCart());
            //    cartLocal.cartHeader = originalCart.cartHeader;
            //    cartLocal.Customer = Session.cart.Customer;
            //    cartLocal.itemCombos = Session.cart.itemCombos;
            //    if(Session.cart.itemCombos==null)
            //    {
            //        cartLocal.itemCombos = new List<ItemCombo>();
            //    }

            //    cartLocal.orderReason = originalCart.orderReason;

            //    CartItem item = originalCart.cartItems[0];
            //    cartLocal.orderUDT = originalCart.orderUDT;
            //    CartItem cartItemLocal = new CartItem();

            //    cartItemLocal.Location_Code = Session._LocationCode;
            //    cartItemLocal.Order_Date = Session.SystemDate;
            //    cartItemLocal.Line_Number = 1;
            //    cartItemLocal.Sequence = 1;
            //    cartItemLocal.Menu_Type_ID = item.Menu_Type_ID;//Session._MenuTypeID;
            //    cartItemLocal.Menu_Category_Code = item.Menu_Category_Code; //Session.selectedMenuItem.Menu_Category_Code;
            //    cartItemLocal.Combo_Menu_Code = item.Combo_Menu_Code;
            //    cartItemLocal.Combo_Size_Code = item.Combo_Size_Code;
            //    cartItemLocal.Menu_Code = item.Menu_Code; //Session.selectedMenuItem.Menu_Code;
            //    cartItemLocal.Menu_Description = item.Menu_Description;//Session.selectedMenuItem.Order_Description;
            //    cartItemLocal.Prepared = item.Prepared; //Convert.ToBoolean(Session.selectedMenuItem.Prepared_YN);
            //    cartItemLocal.Menu_Item_Taxable = item.Menu_Item_Taxable;//Convert.ToInt32(Session.selectedMenuItem.Taxable);
            //    cartItemLocal.Pizza = item.Pizza; //Convert.ToBoolean(Session.selectedMenuItem.Pizza_YN);
            //    cartItemLocal.Size_Code = item.Size_Code;
            //    cartItemLocal.Size_Description = item.Size_Description;
            //    cartItemLocal.Quantity = 1;
            //    cartItemLocal.Added_By = Session.CurrentEmployee.LoginDetail.EmployeeCode;
            //    cartItemLocal.Kitchen_Device_Count = item.Kitchen_Device_Count; //Session.selectedMenuItem.Kitchen_Device_Count;
            //    cartItemLocal.Orig_Menu_Code = item.Orig_Menu_Code;//Session.selectedMenuItem.Orig_Menu_Code;
            //    cartItemLocal.Menu_Price = item.Menu_Price;
            //    cartItemLocal.Price = item.Price;
            //    cartItemLocal.Order_Line_Total = item.Order_Line_Total;
            //    cartItemLocal.Action = "A";
            //    cartItemLocal.Prompt_For_Size = item.Prompt_For_Size;//Session.selectedMenuItem.Prompt_For_Size;
            //    cartItemLocal.Order_Line_Complete = false;
            //    if (cartItemLocal.Kitchen_Device_Count > 0)
            //        cartItemLocal.Order_Line_Status_Code = 1;
            //    else
            //        cartItemLocal.Order_Line_Status_Code = 2;
            //    cartItemLocal.Base_Price = item.Menu_Price; //cartItemLocal.Menu_Price;
            //    cartItemLocal.Base_Price2 = item.Menu_Price2;//cartItemLocal.Menu_Price2;
            //    cartItemLocal.NumberOfSizes = item.NumberOfSizes; //Session.selectedMenuItem.NumberOfSizes;
            //    cartItemLocal.NumberOfAttributes = item.NumberOfAttributes; //Session.selectedMenuItem.NumberOfAttributes;
            //    cartItemLocal.NumberOfOptions = item.NumberOfOptions;//Session.selectedMenuItem.NumberOfOptions;
            //    cartItemLocal.Menu_Item_Type_Code = item.Menu_Item_Type_Code;//Session.selectedMenuItem.Menu_Item_Type_Code;
            //    cartItemLocal.Print_Nutritional_Label = item.Print_Nutritional_Label;//Convert.ToBoolean(Session.selectedMenuItem.Print_Nutritional_Label);
            //    cartItemLocal.Specialty_Pizza = item.Specialty_Pizza;//Convert.ToBoolean(Session.selectedMenuItem.Specialty_Pizza);
            //    cartItemLocal.Specialty_Pizza_Code = item.Specialty_Pizza_Code;//Session.selectedMenuItem.Specialty_Pizza_Code;
            //    cartItemLocal.Topping_String = item.Topping_String;
            //    cartItemLocal.Instruction_String = item.Instruction_String;
            //    cartItemLocal.Coupon_Code = item.Coupon_Code;
            //    cartItemLocal.Topping_Codes = item.Topping_Codes;
            //    cartItemLocal.Topping_Descriptions = item.Topping_Descriptions;
            //    cartItemLocal.Coupon_Description = item.Coupon_Description;
            //    cartItemLocal.Description = item.Menu_Description;

            //    if (item.itemAttributeGroups != null && item.itemAttributeGroups.Count > 0)
            //    {
            //        cartItemLocal.itemAttributeGroups = item.itemAttributeGroups;
            //    }
            //    if (item.itemAttributes != null && item.itemAttributes.Count > 0)
            //    {
            //        cartItemLocal.itemAttributes = item.itemAttributes;
            //    }
            //    if (item.itemGiftCards != null && item.itemGiftCards.Count > 0)
            //    {
            //        cartItemLocal.itemGiftCards = item.itemGiftCards;
            //    }
            //    if (item.itemOptionGroups != null && item.itemOptionGroups.Count > 0)
            //    {
            //        cartItemLocal.itemOptionGroups = item.itemOptionGroups;
            //    }
            //    if (item.itemOptions != null && item.itemOptions.Count > 0)
            //    {
            //        cartItemLocal.itemOptions = item.itemOptions;
            //    }
            //    if (item.itemReasons != null && item.itemReasons.Count > 0)
            //    {
            //        cartItemLocal.itemReasons = item.itemReasons;
            //    }
            //    if (item.itemSpecialtyPizzas != null && item.itemSpecialtyPizzas.Count > 0)
            //    {
            //        cartItemLocal.itemSpecialtyPizzas = item.itemSpecialtyPizzas;
            //    }
            //    if (item.itemUDT != null)
            //    {
            //        cartItemLocal.itemUDT = item.itemUDT;
            //    }
            //    cartLocal.cartItems.Add(cartItemLocal);            


            //    selectedLineNumber = -1;

            //    Session.cart = APILayer.Add2Cart(cartLocal);


            //    CartFunctions.CheckCart();
            //    cartLocal = (new Cart().GetCart());
            //    cartLocal.cartHeader = Session.cart.cartHeader;
            //    cartLocal.Customer = Session.cart.Customer;

            //    for (int i = 1; i <= originalCart.cartItems.Count - 1; i++)
            //    {
            //        item = originalCart.cartItems[i];

            //        cartItemLocal = new CartItem();

            //        cartItemLocal.Location_Code = Session._LocationCode;
            //        cartItemLocal.Order_Date = Session.SystemDate;
            //        cartItemLocal.Line_Number = 1;
            //        cartItemLocal.Sequence = 1;
            //        cartItemLocal.Menu_Type_ID = item.Menu_Type_ID;//Session._MenuTypeID;
            //        cartItemLocal.Menu_Category_Code = item.Menu_Category_Code; //Session.selectedMenuItem.Menu_Category_Code;
            //        cartItemLocal.Combo_Menu_Code = item.Combo_Menu_Code;
            //        cartItemLocal.Combo_Size_Code = item.Combo_Size_Code;
            //        cartItemLocal.Menu_Code = item.Menu_Code; //Session.selectedMenuItem.Menu_Code;
            //        cartItemLocal.Menu_Description = item.Menu_Description;//Session.selectedMenuItem.Order_Description;
            //        cartItemLocal.Prepared = item.Prepared; //Convert.ToBoolean(Session.selectedMenuItem.Prepared_YN);
            //        cartItemLocal.Menu_Item_Taxable = item.Menu_Item_Taxable;//Convert.ToInt32(Session.selectedMenuItem.Taxable);
            //        cartItemLocal.Pizza = item.Pizza; //Convert.ToBoolean(Session.selectedMenuItem.Pizza_YN);
            //        cartItemLocal.Size_Code = item.Size_Code;
            //        cartItemLocal.Size_Description = item.Size_Description;
            //        cartItemLocal.Quantity = 1;
            //        cartItemLocal.Added_By = Session.CurrentEmployee.LoginDetail.EmployeeCode;
            //        cartItemLocal.Kitchen_Device_Count = item.Kitchen_Device_Count; //Session.selectedMenuItem.Kitchen_Device_Count;
            //        cartItemLocal.Orig_Menu_Code = item.Orig_Menu_Code;//Session.selectedMenuItem.Orig_Menu_Code;
            //        cartItemLocal.Menu_Price = item.Menu_Price;
            //        cartItemLocal.Price = item.Price;
            //        cartItemLocal.Order_Line_Total = item.Order_Line_Total;
            //        cartItemLocal.Action = "A";
            //        cartItemLocal.Prompt_For_Size = item.Prompt_For_Size;//Session.selectedMenuItem.Prompt_For_Size;
            //        cartItemLocal.Order_Line_Complete = false;
            //        if (cartItemLocal.Kitchen_Device_Count > 0)
            //            cartItemLocal.Order_Line_Status_Code = 1;
            //        else
            //            cartItemLocal.Order_Line_Status_Code = 2;
            //        cartItemLocal.Base_Price = item.Menu_Price; //cartItemLocal.Menu_Price;
            //        cartItemLocal.Base_Price2 = item.Menu_Price2;//cartItemLocal.Menu_Price2;
            //        cartItemLocal.NumberOfSizes = item.NumberOfSizes; //Session.selectedMenuItem.NumberOfSizes;
            //        cartItemLocal.NumberOfAttributes = item.NumberOfAttributes; //Session.selectedMenuItem.NumberOfAttributes;
            //        cartItemLocal.NumberOfOptions = item.NumberOfOptions;//Session.selectedMenuItem.NumberOfOptions;
            //        cartItemLocal.Menu_Item_Type_Code = item.Menu_Item_Type_Code;//Session.selectedMenuItem.Menu_Item_Type_Code;
            //        cartItemLocal.Print_Nutritional_Label = item.Print_Nutritional_Label;//Convert.ToBoolean(Session.selectedMenuItem.Print_Nutritional_Label);
            //        cartItemLocal.Specialty_Pizza = item.Specialty_Pizza;//Convert.ToBoolean(Session.selectedMenuItem.Specialty_Pizza);
            //        cartItemLocal.Specialty_Pizza_Code = item.Specialty_Pizza_Code;//Session.selectedMenuItem.Specialty_Pizza_Code;
            //        cartItemLocal.Topping_String = item.Topping_String;
            //        cartItemLocal.Instruction_String = item.Instruction_String;
            //        cartItemLocal.Coupon_Code = item.Coupon_Code;
            //        cartItemLocal.Topping_Codes = item.Topping_Codes;
            //        cartItemLocal.Topping_Descriptions = item.Topping_Descriptions;
            //        cartItemLocal.Coupon_Description = item.Coupon_Description;
            //        cartItemLocal.Description = item.Menu_Description;

            //        if (item.itemAttributeGroups != null && item.itemAttributeGroups.Count > 0)
            //        {
            //            cartItemLocal.itemAttributeGroups = item.itemAttributeGroups;
            //        }
            //        if (item.itemAttributes != null && item.itemAttributes.Count > 0)
            //        {
            //            cartItemLocal.itemAttributes = item.itemAttributes;
            //        }
            //        if (item.itemGiftCards != null && item.itemGiftCards.Count > 0)
            //        {
            //            cartItemLocal.itemGiftCards = item.itemGiftCards;
            //        }
            //        if (item.itemOptionGroups != null && item.itemOptionGroups.Count > 0)
            //        {
            //            cartItemLocal.itemOptionGroups = item.itemOptionGroups;
            //        }
            //        if (item.itemOptions != null && item.itemOptions.Count > 0)
            //        {
            //            cartItemLocal.itemOptions = item.itemOptions;
            //        }
            //        if (item.itemReasons != null && item.itemReasons.Count > 0)
            //        {
            //            cartItemLocal.itemReasons = item.itemReasons;
            //        }
            //        if (item.itemSpecialtyPizzas != null && item.itemSpecialtyPizzas.Count > 0)
            //        {
            //            cartItemLocal.itemSpecialtyPizzas = item.itemSpecialtyPizzas;
            //        }
            //        if (item.itemUDT != null)
            //        {
            //            cartItemLocal.itemUDT = item.itemUDT;
            //        }
            //        cartLocal.cartItems.Add(cartItemLocal);


            //        selectedLineNumber = -1;

            //        //Session.cart = APILayer.Add2CartOrderModify(cartLocal);

            //    }
            //    Session.cart = APILayer.Add2Cart(cartLocal);
            //    Session.selectedOrderType = Session.cart.cartHeader.Order_Type_Code;
            //}

           



        }

        public static void LoadDefaultCustomer(ref Customer customer, string locationCode,string customername)
        {
            customer.Name = customername;
            customer.Location_Code = locationCode;
            customer.Customer_Code = 0;
            customer.Street_Code = 0;
            customer.Accept_Cash = true;
            customer.Accept_Credit_Cards = true;
            customer.Accept_Gift_Cards = true;
            customer.Accept_Checks = true;
            customer.Accept_Charge_Account = true;
            customer.Finance_Charge_Rate = 0;
            customer.Set_Discount = 0;
            customer.Phone_Number = "";
            customer.Phone_Ext = "";
            customer.Company_Name ="";
            customer.Street_Number = ""; 
            customer.Street_Code = 0;
            customer.Cross_Street_Code = 0;
            customer.Suite = "";
            customer.Address_Line_2 = "";
            customer.Address_Line_3 = "";
            customer.Address_Line_4 = "";
            customer.Mailing_Address = "";
            customer.Postal_Code = "";
            customer.Plus4 = "";
            customer.Delivery_Point_Code = "";
            customer.Walk_Sequence = "";
            customer.Address_Type = "";
            customer.Set_Discount =0;
            customer.Tax_Exempt1 = false;
            customer.Tax_ID1 = "";
            customer.Tax_Exempt2 = false;
            customer.Tax_ID2 = "";
            customer.Tax_ID3 = "";
            customer.Tax_Exempt4 = false;
            customer.Tax_ID4 = "";
            customer.Accept_Checks = false;
            customer.Accept_Credit_Cards = false;
            customer.Accept_Gift_Cards = false;
            customer.Accept_Charge_Account = false;
            customer.Accept_Cash = true;
            customer.Finance_Charge_Rate = 0;
            customer.Credit_Limit = 0;
            customer.Credit = 0;
            customer.Payment_Terms = 0;
            customer.First_Order_Date = DateTime.MinValue;
            customer.Last_Order_Date = DateTime.MinValue;
            customer.Added_By = Session.CurrentEmployee.LoginDetail.EmployeeCode; ;
            customer.Comments = "";
            customer.DriverComments = "";
            customer.DriverCommentsAddUpdateDelete = false; 
            customer.Manager_Notes = "";
            customer.Customer_City_Code = 1;
            customer.Customer_Street_Name = "";
            customer.HotelorCollege = false;
            customer.City = "";
            customer.Region = "";
            customer.TaxRate1 = 0;
            customer.TaxRate2 = 0;
            customer.Cross_Street = "";
            customer.NoteAddUpdateDelete = false;
            customer.gstin_number ="";
            customer.date_of_birth =DateTime.MinValue;
            customer.anniversary_date = DateTime.MinValue;


        }
        public static int GetOrderLineMaxSequence()
        {
            int sequence = 0;

            foreach (var item in Session.originalcart.cartItems)
            {
                sequence = item.Sequence;
            }
            return sequence;
        }

        public static void LoadOrderFormatOrderScreen(bool blnHistory)
        {
            if (!blnHistory)
            {
                frmCustomer frmcustomer = new frmCustomer();

                // CustomerFunction.Set_Required_Fields(Session.originalcart.cartHeader.Order_Type_Code, Session.originalcart.Customer.Address_Type);
                //CustomerFunction.UpdatePhoneNumber(Session.originalcart.Customer.Phone_Number, Session.originalcart.Customer, Session.originalcart.cartHeader);
                if (!string.IsNullOrEmpty(Session.originalcart.Customer.Name))
                {
                    frmcustomer.txtName.Text = Session.originalcart.Customer.Name;
                }
                if (!string.IsNullOrEmpty(Session.originalcart.cartHeader.Customer_Room))
                {
                    frmcustomer.txtSuite.Text = Session.originalcart.cartHeader.Customer_Room;
                }
                frmcustomer.txtNotes_Instore.Visible = false;

                if (SystemSettings.WorkStationSettings.pblnAttached_Card_Reader == true)
                {

                    //If frmOrder.msrCreditCardReader Is Nothing Then
                    //Call frmOrder.OpenMSR
                    //Else
                    //frmOrder.msrCreditCardReader.DeviceEnabled = True
                    //frmOrder.msrCreditCardReader.AutoDisable = True
                    //frmOrder.msrCreditCardReader.DataEventEnabled = True
                    //frmOrder.msrCreditCardReader.CardType = CreditCard

                }

            }

        }
        public static void LoadOrderDisplayOrderScreen(UC_Customer_OrderMenu UC_Customer_OrderMenu, bool blnHistory, bool blnModify, int intCurrentBookmark, int intDoublesGroup)
        {
            Session.OrderReqField = new OrderReqField();


            if (!blnHistory)
            {

                Session.OrderReqField.btn_Minus = false;
                Session.OrderReqField.btn_Plus = false;

                if (blnModify)
                {
                    UC_Customer_OrderMenu.ConvertExittoCancel();
                    Session.pblnModifyingOrder = true;
                    Session.pcurOriginalTotal = Session.originalcart.cartHeader.Total;
                    Session.pblnCashingOutAfter = false;
                    Session.pblnOrderModifications = false;
                    Session.OrderReqField.Text = APILayer.GetCatalogText(LanguageConstant.cintModifyOrders) + "-" + Session.originalcart.cartHeader.Order_Number;

                    Cash_Out_Buttons(false, UC_Customer_OrderMenu);
                    Set_Buttons(false, UC_Customer_OrderMenu);
                    if (Session.cart.cartHeader.Order_Type_Code == "D")
                    {
                        if (Session.cart.cartHeader.Delivery_Fee > 0 || Session.cart.cartHeader.Delivery_Fee_Removed == true)
                        {
                            //Call frmOrder.ShowDeliveryFeeButton
                        }
                        else
                        {
                            //Call frmOrder.HideDeliveryFeeButton
                        }
                    }
                    else
                    {
                        //Call frmOrder.HideDeliveryFeeButton

                    }
                }
            }

        }

        public static void Cash_Out_Buttons(bool blnCashOut, UC_Customer_OrderMenu UC_Customer_OrderMenu)
        {
            UC_Customer_OrderMenu.cmdTimedOrders.Visible = !blnCashOut;
            UC_Customer_OrderMenu.cmdFunctions.Visible = !blnCashOut;
            UC_FunctionList.cmdGiveCredit.Visible = !blnCashOut;

            UC_Customer_OrderMenu.cmdTimeClock.Visible = !blnCashOut;

            UC_Customer_OrderMenu.cmdHistory.Visible = !blnCashOut;
            UC_Customer_OrderMenu.cmdDelivery_Info.Visible = !blnCashOut;
            Session.OrderReqField.btn_Instructions = !blnCashOut;
            Session.OrderReqField.btn_Up = !blnCashOut;
            Session.OrderReqField.btn_Down = !blnCashOut;
            Session.OrderReqField.btn_Plus = !blnCashOut;
            Session.OrderReqField.btn_Minus = !blnCashOut;
            UC_Customer_OrderMenu.cmdInformation.Visible = !blnCashOut;
            Session.OrderReqField.btn_Coupons = !blnCashOut;
            Session.OrderReqField.btn_Quantity = !blnCashOut;


            if (blnCashOut)
            {

            }
            if (Convert.ToBoolean(SystemSettings.settings.pbytDoublesPricingOn) == true)
            {
                if (SystemSettings.settings.pbytDoublesPricingOn == 2 || SystemSettings.settings.pbytDoublesPricingOn == 3)
                {
                    switch (SystemSettings.settings.pbytDoublesPricingOn)
                    {
                        case 0:
                            //' cmdDoublesPricing.Visible = True
                            //'cmdAdditionalPricing.Visible = False '
                            break;
                        case 1:
                            // cmdDoublesPricing.Visible = False
                            //'cmdAdditionalPricing.Visible = True
                            break;

                    }
                }
            }


        }
        public static void Set_Buttons(bool blnStart_Order, UC_Customer_OrderMenu UC_Customer_OrderMenu)
        {
            int intIndex;

            UC_Customer_OrderMenu.cmdTimeClock.Enabled = true;
            UC_Customer_OrderMenu.cmdLogin.Enabled = true;
            UC_Customer_OrderMenu.cmdInformation.Enabled = true;

            if (Session.cart.cartHeader == null)
            {

            }

            if (Session.cart.cartHeader.Delivery_Fee > 0)
            {
                PaymentFunctions.PayButtonEnabled(false);
                PaymentFunctions.PayButtonEnabled(false);

                if (!string.IsNullOrEmpty(Session.CurrentEmployee.LoginDetail.EmployeeCode))
                {
                    PaymentFunctions.EnableOrderTypes(true);
                }
                else
                {
                    PaymentFunctions.EnableOrderTypes(false);
                }
                Session.OrderReqField.btn_Instructions = false;
                UC_Customer_OrderMenu.cmdFunctions.Enabled = true;
                UC_FunctionList.cmdUseCredit.Enabled = false;
                UC_Customer_OrderMenu.cmdOrderCoupons.Enabled = false;
                Session.OrderReqField.btn_Quantity = false;
                Session.OrderReqField.btn_Coupons = false;
                UC_Customer_OrderMenu.cmdSearch.Enabled = false;
                if (SystemSettings.settings.pblnTrainingMode == true)
                {
                    UC_Customer_OrderMenu.cmdChangeOrders.Enabled = false;
                }
                else if (Session.cart.cartHeader.Order_Date == DateTime.MinValue)
                {
                    UC_Customer_OrderMenu.cmdChangeOrders.Enabled = true;
                }
                else
                {
                    UC_Customer_OrderMenu.cmdChangeOrders.Enabled = false;
                }
                SetButtonsPlusMinus(false, UC_Customer_OrderMenu);
                if (Session.cart.cartHeader == null)
                {
                    SetButtonsDineIn(UC_Customer_OrderMenu);
                }

            }
            else
            {
               
                if (Session.cart.cartHeader.Final_Total > 0)
                {
                    if (Session.cart.cartHeader.Order_Type_Code == "D" && Session.cart.cartHeader.Delivery_Fee > 0)
                    {
                        PaymentFunctions.PayButtonEnabled(false);
                    }
                    else
                    {
                        PaymentFunctions.PayButtonEnabled(true);
                    }
                    if (Session.cart.cartHeader.Final_Total <= 0)
                    {

                        PaymentFunctions.PayButtonEnabled(false);
                        if (string.IsNullOrEmpty(Session.CurrentEmployee.LoginDetail.EmployeeCode))
                        {
                            PaymentFunctions.EnableOrderTypes(true);
                        }
                        else
                        {
                            PaymentFunctions.EnableOrderTypes(false);

                        }

                    }
                    Session.OrderReqField.btn_Instructions = false;
                    UC_Customer_OrderMenu.cmdFunctions.Enabled = true;
                    Session.OrderReqField.btn_Coupons = false;
                    Session.OrderReqField.btn_Quantity = false;
                    UC_Customer_OrderMenu.cmdOrderCoupons.Enabled = false;
                    UC_Customer_OrderMenu.cmdTimeClock.Enabled = false;
                    UC_Customer_OrderMenu.cmdSearch.Enabled = true;
                    UC_Customer_OrderMenu.cmdLogin.Enabled = false;
                    UC_Customer_OrderMenu.cmdDelivery_Info.Text = "Customer";
                    UC_Customer_OrderMenu.cmdTimedOrders.Enabled = false;
                    UC_Customer_OrderMenu.cmdChangeOrders.Enabled = false;
                    SetButtonsPlusMinus(false, UC_Customer_OrderMenu);
                    if (Session.cart.cartHeader != null)
                    {
                        SetButtonsDineIn(UC_Customer_OrderMenu);
                    }

                }
                else
                {
                    
                        if (Session.cart.cartHeader.Final_Total > 0)
                        {
                            if (Session.cart.cartHeader.Order_Type_Code == "D" && Session.cart.cartHeader.Delivery_Fee > 0)
                            {
                                PaymentFunctions.PayButtonEnabled(false);
                            }
                            else
                            {
                                PaymentFunctions.PayButtonEnabled(true);
                            }
                            if (Session.cart.cartHeader.Final_Total <= 0)
                            {

                                PaymentFunctions.PayButtonEnabled(false);
                                if (string.IsNullOrEmpty(Session.CurrentEmployee.LoginDetail.EmployeeCode))
                                {
                                    PaymentFunctions.EnableOrderTypes(true);
                                }
                                else
                                {
                                    PaymentFunctions.EnableOrderTypes(false);

                                }


                            Session.OrderReqField.btn_Instructions = false;
                            UC_Customer_OrderMenu.cmdFunctions.Enabled = true;
                            Session.OrderReqField.btn_Coupons = false;
                            Session.OrderReqField.btn_Quantity = false;
                            UC_Customer_OrderMenu.cmdOrderCoupons.Enabled = false;
                            UC_Customer_OrderMenu.cmdTimeClock.Enabled = false;
                            UC_Customer_OrderMenu.cmdSearch.Enabled = true;
                            UC_Customer_OrderMenu.cmdLogin.Enabled = false;
                            UC_Customer_OrderMenu.cmdDelivery_Info.Enabled = false;
                            UC_Customer_OrderMenu.cmdTimedOrders.Enabled = false;
                            SetButtonsPlusMinus(false, UC_Customer_OrderMenu);
                            if (Session.cart.cartHeader != null)
                            {
                                SetButtonsDineIn(UC_Customer_OrderMenu);
                            }

                        }
                    }

                }
            }




        }
        public static void SetButtonsPlusMinus(bool blnEnabled, UC_Customer_OrderMenu UC_Customer_OrderMenu)
        {
            Session.OrderReqField.btn_Plus = blnEnabled;
            Session.OrderReqField.btn_Plus = blnEnabled;
        }

        public static void SetButtonsDineIn(UC_Customer_OrderMenu UC_Customer_OrderMenu)
        {
            if (Session.cart.cartHeader.Customer_Code > 0)
            {
                if (Session.pblnModifyingOrder)
                {
                    UC_Customer_OrderMenu.cmdHistory.Enabled = false;
                }
                else
                {
                    UC_Customer_OrderMenu.cmdHistory.Enabled = true;
                }

                Session.pcurCreditBalanceHolding = Session.cart.Customer.Credit;
                if (Session.cart.Customer.Credit > 0 || Session.pcurCreditBalanceHolding > 0)
                {

                }

                else
                {
                    UC_Customer_OrderMenu.cmdHistory.Enabled = false;
                    UC_FunctionList.cmdGiveCredit.Enabled = false;

                }

                UC_Customer_OrderMenu.cmdTimedOrders.Enabled = false;
                UC_FunctionList.cmdTaxExempt.Enabled = true;
            }
            else
            {
                if (Session.cart.Customer.Phone_Number.Length > 10 || Session.cart.Customer.Customer_Code > 0)
                {
                    UC_FunctionList.cmdGiveCredit.Enabled = true;
                }
                else
                {
                    UC_FunctionList.cmdGiveCredit.Enabled = false;

                }

                UC_Customer_OrderMenu.cmdDelivery_Info.Enabled = true;
                UC_Customer_OrderMenu.cmdHistory.Enabled = true;
                UC_Customer_OrderMenu.cmdTimedOrders.Enabled = false;
                UC_FunctionList.cmdTaxExempt.Enabled = false;


            }
        }
        public static void POSUpdateOrderModifyingStatus(string Location_Code, long Order_Number, DateTime Order_Date, string Employee_Code, bool Order_Status)
        {
            try
            {

                UpdateOrderStatusRequest updateOrderStatusRequest = new UpdateOrderStatusRequest();
                updateOrderStatusRequest.Employee_Code = Employee_Code;
                updateOrderStatusRequest.Location_Code = Session._LocationCode;
                updateOrderStatusRequest.Order_Number = Order_Number;
                updateOrderStatusRequest.Order_Date = Order_Date;
                updateOrderStatusRequest.Order_Status = Order_Status;
                int result = APILayer.UpdateOrderStatus(updateOrderStatusRequest);
            }
            catch (Exception ex)
            {
                Logger.Trace("ERROR", "OrderFunctions-POSUpdateOrderModifyingStatus(): " + ex.Message, ex, true);
            }
        }

        public static bool AbandonOrder()
        {
            bool OrderAbandoned = false;
            bool blnLoginSuccessful = false;
            bool EmployeeAbandonOrder = false;

            EmployeeAbandonOrder = Session.CurrentEmployee.LoginDetail.blnAbandonOrder;
            if (Session.cart != null)
            {
                Session.cart.cartHeader.Added_By = Session.CurrentEmployee.LoginDetail.EmployeeCode;
            }
            if (SystemSettings.GetSettingValue("PasswordAbandonOrder", Session._LocationCode) == "1")
            {
                if (!String.IsNullOrEmpty(Session.CurrentEmployee.LoginDetail.EmployeeCode) && EmployeeAbandonOrder)
                {
                    if (SystemSettings.GetSettingValue("RequirePasswordForSpecialAccess", Session._LocationCode) == "1")
                    {
                        if (!EmployeeFunctions.MatchEmployeePassword())
                            blnLoginSuccessful = false;
                        else
                            blnLoginSuccessful = true;
                    }
                    else
                    {
                        blnLoginSuccessful = true;
                    }
                }
                else
                {
                    //TO DO -- Special Access
                }
            }
            else
            {
                if (!String.IsNullOrEmpty(Session.CurrentEmployee.LoginDetail.EmployeeCode) && !EmployeeAbandonOrder)
                {
                    //TO DO -- Special Access
                }
                else
                {
                    blnLoginSuccessful = true;
                }
            }


            if (blnLoginSuccessful && !SystemSettings.settings.pblnTrainingMode)
            {
                OrderRequest orderRequest = new OrderRequest();
                if (Session.cart != null && Session.CartInitiated)
                {
                    Cart cartLocal = (new Cart().GetCart());

                    cartLocal.cartHeader = Session.cart.cartHeader;
                    cartLocal.cartHeader.Delayed_Order = cartLocal.cartHeader.Delayed_Order;
                    cartLocal.cartHeader.Actual_Order_Date = Convert.ToDateTime(SystemSettings.settings.pdtmServerDateTime);
                    cartLocal.cartHeader.Pay_Now = false;
                    cartLocal.cartHeader.Order_Saved = Session.SystemDate;
                    cartLocal.cartHeader.Closed_Order_Time = new DateTime(1899, 12, 30);
                    cartLocal.cartHeader.Order_Time = UserFunctions.OrderTimeinSeconds();
                    cartLocal.cartHeader.Order_Status_Code = 101;
                    cartLocal.cartHeader.Action = "M";
                    CartFunctions.UpdateCustomer(cartLocal);
                    Session.cart = APILayer.Add2Cart(cartLocal);


                    orderRequest.locationCode = Session.cart.cartHeader.LocationCode;
                    orderRequest.Order_Date = Session.cart.cartHeader.Order_Date;
                    orderRequest.cartId = Session.cart.cartHeader.CartId;
                    orderRequest.modifyOrder = false;
                    orderRequest.newOrderTime = UserFunctions.OrderTimeinSeconds();

                    if (APILayer.PushOrder(orderRequest))
                    {
                        OrderAbandoned = true;

                        if ((SystemSettings.GetSettingValue("CartOnHold", Session._LocationCode) == "1"))
                        {                           
                            CartOnHoldRequest cartOnHoldRequest = new CartOnHoldRequest();
                            cartOnHoldRequest.CartId = Session.cart.cartHeader.CartId;
                            cartOnHoldRequest.Time = DateTime.Now;
                            cartOnHoldRequest.CustomerName = Session.cart.Customer.Name;
                            cartOnHoldRequest.CustomerNumber = Session.cart.Customer.Phone_Number;
                            cartOnHoldRequest.OrderAmount = Convert.ToDecimal(Session.cart.cartHeader.Total);
                            cartOnHoldRequest.OrderTaker = Session.CurrentEmployee.LoginDetail.FirstName + " " + Session.CurrentEmployee.LoginDetail.LastName;
                            cartOnHoldRequest.Terminal = Session.ComputerName;
                            cartOnHoldRequest.IsActive = 0;

                            APILayer.UpdateCartOnHold(cartOnHoldRequest);
                        }
                        //Session.currentOrderResponse.Order_Number;
                        //TO DO
                        //Call HandleAbandonCancelTransactions(True)


                        //TO DO -- Delete Caller ID Call
                        //'If they change the lines then mark the previous line as unanswered
                        //    If p_intCurrentCallerIDLine<> 0 Then
                        //       Call pobjBackoCallerID.DeleteCall(OrderCollection.Location_Code, p_intCurrentCallerIDLine, p_strCurrentCallerIDPhoneNumber)
                        //    End If


                        //TO DO -- Audit

                        //Abanded Order Printing
                        PrintFunctions.PrintAbandedOrder(Session._LocationCode, Session._WorkStationID, Session.currentOrderResponse.Order_Date, Session.currentOrderResponse.Order_Number, Session.CurrentEmployee.LoginDetail.EmployeeCode);

                    }
                }
            }
            return OrderAbandoned;
        }

        //public static string UpsellReminder()
        //{
        //    string Notes;            
        //    Notes = Session.UpsellReminder;
        //    return Notes;
        //}
        public static bool HandleAddressControlData()
        {
            Int32 lngStreetCode;
            //bool blnCustomerCodeChanged = false;
            bool HandleAddressControlData = false;
            //CartFunctions.FillCartToCustomer(); //Commented by Vikas Saraswat 23/12/2020
            if (Session.cart.Customer.Street_Code != 0)
            {
                lngStreetCode = Session.cart.Customer.Street_Code;
            }
            else
            {
                lngStreetCode = 0;
            }


            if (NewStreet_OutofArea(lngStreetCode))
            {
                //Call ctlAddress.ChooseNewStreet;
                HandleAddressControlData = false;
                return false;
            }

            //Start : Commented by Vikas Saraswat 23/12/2020
            //frmCustomer objcust = new frmCustomer();
            //Session.cart.Customer.Customer_Street_Name = objcust.txtStreet.ToString().Trim();


            ////Already cart header has customer details
            //if (objcust.tabControl_Notes.SelectedTab.Text != Session.cart.Customer.Comments)
            //{
            //    if (objcust.tabControl_Notes.SelectedTab == null)
            //    {
            //        //Session.cart.Customer.NoteAddUpdateDelete = 3;
            //    }
            //    else
            //    {
            //        if (Session.cart.Customer.Comments == null)
            //        {
            //            //Session.cart.Customer.NoteAddUpdateDelete = 1;
            //        }
            //        else
            //        {
            //            //Session.cart.Customer.NoteAddUpdateDelete = 2;
            //        }
            //    }
            //}
            //END : Commented by Vikas Saraswat 23/12/2020

            //        CustomerCollection.Comments = Trim$(ctlCallNotes.InstoreComments)

            //        If AppFields.pblnSaveDriverComment Then
            //    If ctlCallNotes.Comments<> CustomerCollection.DriverComments Then
            //        If ctlCallNotes.Comments = vbNullString Then
            //            CustomerCollection.DriverCommentsAddUpdateDelete = 3
            //        Else
            //            If CustomerCollection.DriverComments = vbNullString Then
            //                CustomerCollection.DriverCommentsAddUpdateDelete = 1
            //            Else
            //                CustomerCollection.DriverCommentsAddUpdateDelete = 2
            //            End If
            //        End If
            //    End If


            //    CustomerCollection.DriverComments = Trim$(ctlCallNotes.Comments)
            //Else
            //    OrderCollection.Comments = Trim$(ctlCallNotes.Comments)
            //End If


            //'If this is a modify order and the customer has been changed, clear out the credit for the order
            //If Not OrigOrderCollection Is Nothing Then
            //    If OrigOrderCollection.Customer_Code<> OrderCollection.Customer_Code Then
            //        'Don't bother recal if there wasn't credit applied on original order
            //        If OrigOrderCollection.Credit > 0 Then
            //            blnCustomerCodeChanged = True
            //        End If
            //    End If
            //End If


            //'mlngPreviousCustomerCode > 0 b/c we don't need to check if there is no previous customer
            //If Not blnCustomerCodeChanged And ctlAddress.PreviousCustomerCode<> CustomerCollection.Customer_Code _
            //    And ctlAddress.PreviousCustomerCode > 0 _
            //Then
            //    blnCustomerCodeChanged = True
            //End If


            //If blnCustomerCodeChanged Then
            //    OrderCollection.Credit = 0


            //    Call OrderClass.CalcOrderPrice(OrderCollection, OrderLineCollection, CustomerCollection, TicketCollection, _
            //                                    OrderLineOptionCollection, OrderLineComboCollection, OrderUDTCollection, _
            //                                    OrderLineUDTCollection, False)

            //    Call RefreshTicketTotals


            //    Call RefreshTicket
            //End If
            HandleAddressControlData = true;
            return HandleAddressControlData;

        }

        public static bool NewStreet_OutofArea(Int32 StreetCode)
        {
            bool blnDoIt = false;
            bool blnAddStreet = false;
            //CartFunctions.FillCartToCustomer(); //Commented by Vikas Saraswat 23/12/2020
            //if (Session.cart.Customer.Location_Code.Length == 0)
            //    {
            //    Session.cart.cartHeader.LocationCode = SystemSettings.settings.pstrDefault_Location_Code;
            //    }

            //if (Session.cart.Customer.Location_Code.Length == 0)
            //{
            //    Session.cart.Customer.Location_Code = SystemSettings.settings.pstrDefault_Location_Code;
            //}

            Form frmObj = Application.OpenForms["frmCustomer"];
            if (frmObj != null)
            {
                frmCustomer obj1 = (frmCustomer)frmObj;

                if (Session.cart.Customer.Street_Code == 0 && obj1.txtStreet.Text != " ")
                {
                    //to do add new form frmAddNewStreet



                    if (blnAddStreet)
                    {
                        blnDoIt = Security_Prompt("InsertStreet");
                    }
                    if (!blnAddStreet)
                    {
                        return false;
                    }
                    else
                    {
                        blnDoIt = false;
                    }
                }

                else
                {
                    if (Session.cart.Customer.Street_Code == 0 && obj1.txtStreet.Text == null && (Session.cart.cartHeader.Order_Type_Code == "P" || Session.cart.cartHeader.Order_Type_Code == "C" && obj1.tdbmPhone_Number.Text.Length >= Session.MaxPhoneDigits))
                    {
                        //Session.cart.Customer.Street_Code = Get_None_Street_Code();
                        blnDoIt = true;
                    }
                }
                if (!blnDoIt)
                {
                    if (Session.cart.Customer.Street_Code == 0 && obj1.txtStreet.Text == null)
                    {
                        blnDoIt = true;
                    }
                    else
                    {
                        //if (Check_Out_Of_Area)
                        //{
                        //    blnDoIt = Security_Prompt("OutOfArea");
                        //}
                        //else 
                        //{
                        //    blnDoIt = true;
                        //}
                    }
                }

                if (Session.cart.cartHeader.Order_Type_Code == "P" || (Session.cart.cartHeader.Order_Type_Code == "C" && obj1.tdbmPhone_Number.Text.Length >= Session.MaxPhoneDigits))
                {
                    //Session.cart.Customer.Street_Code = Get_None_Street_Code();
                    blnDoIt = true;
                }
                else
                {
                    blnDoIt = true;
                }
            }

            return blnDoIt;
        }

        public static bool Security_Prompt(string strSpecialAccess)
        {

            bool blnSpecialAccessType = false;
            bool blnLoginSuccessful = false;
            bool blnSecurityOK = false; ;


            if (strSpecialAccess == "InsertStreet")
            {
                blnSpecialAccessType = Session.CurrentEmployee.LoginDetail.blnAddNewStreet;
            }
            if (strSpecialAccess == "OutOfArea")
            {
                blnSpecialAccessType = Session.CurrentEmployee.LoginDetail.blnAllowOutOfArea;
            }

            if (!blnSpecialAccessType)
            {
                if (Session.CurrentEmployee.LoginDetail.EmployeeCode != null && blnSpecialAccessType)
                {
                    if (SystemSettings.settings.pblnRequirePasswordForSpecialAccess)
                    {
                        if (EmployeeFunctions.MatchEmployeePassword())
                        {
                            blnLoginSuccessful = true;
                        }
                        else
                        {
                            blnLoginSuccessful = false;
                        }
                        blnLoginSuccessful = true;
                    }
                }
                if (strSpecialAccess == "OutOfArea")
                {
                    CustomMessageBox.Show(APILayer.GetCatalogText(LanguageConstant.cintMSGOutofDeliveryArea), CustomMessageBox.Buttons.OK);
                }
                frmLogin objlogin = new frmLogin();
                objlogin.RequireSpecialAccess = true;
                CustomMessageBox.Show(APILayer.GetCatalogText(LanguageConstant.cintMSGOutofDeliveryArea), CustomMessageBox.Buttons.OK);
                objlogin.RequirePassword = true;
                objlogin.ShowDialog();

            }

            if (strSpecialAccess == "InsertStreet")
            {
                blnSpecialAccessType = Session.CurrentEmployee.LoginDetail.blnAddNewStreet;
            }
            if (strSpecialAccess == "OutOfArea")
            {
                blnSpecialAccessType = Session.CurrentEmployee.LoginDetail.blnAllowOutOfArea;
            }

            if (Session.CurrentEmployee.LoginDetail.EmployeeCode != null && blnSpecialAccessType)
            {
                blnLoginSuccessful = true;
            }
            else
            {
                blnLoginSuccessful = false;
            }

            if (blnLoginSuccessful)
            {
                if (strSpecialAccess == "InsertStreet")
                {
                    //To do Insert street from OMS
                }
                if (strSpecialAccess == "AllowOutOfArea")
                {
                    CustomMessageBox.Show(APILayer.GetCatalogText(LanguageConstant.cintMSGOutofDeliveryArea), CustomMessageBox.Buttons.OK);

                }
                blnSecurityOK = true;

            }


            return blnSecurityOK;
        }
    }
}