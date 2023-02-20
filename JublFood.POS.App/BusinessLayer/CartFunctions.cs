using Jublfood.AppLogger;
using JublFood.POS.App.API;
using JublFood.POS.App.Cache;
using JublFood.POS.App.Class;
using JublFood.POS.App.Models.Cart;
using JublFood.POS.App.Models.Catalog;
using JublFood.POS.App.Models.Customer;
using JublFood.POS.App.Models.Employee;
using JublFood.POS.App.Models.Order;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace JublFood.POS.App.BusinessLayer
{
    public static class CartFunctions
    {
        public static void GetCart(string ParentMenu, string btnName, ref int selectedLineNumber)
        {
            CheckCart();
            EmployeeResult oldLoginEmployee;
            if (ParentMenu == "MenuItems")
            {
                Session.selectedMenuItem = Session.menuItems.Find(x => x.Menu_Code == btnName);

                Cart cartLocal = (new Cart().GetCart());

                if (Convert.ToBoolean(Session.selectedMenuItem.Combo_Meal))
                {
                    Session.ProcessingCombo = true;
                    Session.CurrentComboItem = 0;
                    Session.ComboGroup = Session.ComboGroup + 1;
                    Session.CurrentComboGroup = Session.ComboGroup;

                    ItemCombo itemCombo = new ItemCombo();
                    itemCombo.CartId = Session.cart.cartHeader.CartId;
                    itemCombo.Location_Code = Session._LocationCode;
                    itemCombo.Order_Date = Session.SystemDate;
                    //itemCombo.Order_Number = OrderCollection.Order_Number
                    itemCombo.Combo_Menu_Code = Session.selectedMenuItem.Menu_Code;
                    itemCombo.Combo_Menu_Description = Session.selectedMenuItem.Order_Description;
                    itemCombo.Combo_Quantity = 1;
                    itemCombo.Combo_Menu_Category_Code = Session.selectedMenuItem.Menu_Category_Code;
                    itemCombo.Combo_Menu_Type_ID = Session._MenuTypeID;
                    itemCombo.Prompt_For_Size = Session.selectedMenuItem.Prompt_For_Size;
                    itemCombo.Combo_Group = Session.CurrentComboGroup;
                    itemCombo.Combo_Size_Code = "";
                    itemCombo.Combo_Size_Description = "";
                    itemCombo.Action = "A";


                    cartLocal.itemCombos.Add(itemCombo);
                }
                else
                {
                    CartItem cartItemLocal = new CartItem();

                    cartItemLocal.Location_Code = Session._LocationCode;
                    cartItemLocal.Order_Date = Session.SystemDate;
                    cartItemLocal.Line_Number = 1;
                    cartItemLocal.Sequence = 1;
                    cartItemLocal.Menu_Type_ID = Session._MenuTypeID;
                    cartItemLocal.Menu_Category_Code = Session.selectedMenuItem.Menu_Category_Code;
                    cartItemLocal.Combo_Menu_Code = "";
                    cartItemLocal.Combo_Size_Code = "";
                    cartItemLocal.Menu_Code = Session.selectedMenuItem.Menu_Code;
                    cartItemLocal.Menu_Description = Session.selectedMenuItem.Order_Description;
                    cartItemLocal.Prepared = Convert.ToBoolean(Session.selectedMenuItem.Prepared_YN);
                    cartItemLocal.Menu_Item_Taxable = Convert.ToInt32(Session.selectedMenuItem.Taxable);
                    cartItemLocal.Pizza = Convert.ToBoolean(Session.selectedMenuItem.Pizza_YN);
                    cartItemLocal.Size_Code = "";
                    cartItemLocal.Size_Description = "";
                    cartItemLocal.Quantity = 1;
                    cartItemLocal.Added_By = Session.CurrentEmployee.LoginDetail.EmployeeCode;
                    cartItemLocal.Kitchen_Device_Count = Session.selectedMenuItem.Kitchen_Device_Count;
                    cartItemLocal.Orig_Menu_Code = Session.selectedMenuItem.Orig_Menu_Code;
                    cartItemLocal.Menu_Price = 0;
                    cartItemLocal.Price = 0;
                    cartItemLocal.Order_Line_Total = 0;
                    cartItemLocal.Action = "A";
                    cartItemLocal.Prompt_For_Size = Session.selectedMenuItem.Prompt_For_Size;
                    cartItemLocal.Order_Line_Complete = false;
                    if (cartItemLocal.Kitchen_Device_Count > 0)
                        cartItemLocal.Order_Line_Status_Code = 1;
                    else
                        cartItemLocal.Order_Line_Status_Code = 2;
                    cartItemLocal.Base_Price = cartItemLocal.Menu_Price;
                    cartItemLocal.Base_Price2 = cartItemLocal.Menu_Price2;
                    cartItemLocal.NumberOfSizes = Session.selectedMenuItem.NumberOfSizes;
                    cartItemLocal.NumberOfAttributes = Session.selectedMenuItem.NumberOfAttributes;
                    cartItemLocal.NumberOfOptions = Session.selectedMenuItem.NumberOfOptions;
                    cartItemLocal.Menu_Item_Type_Code = Session.selectedMenuItem.Menu_Item_Type_Code;
                    cartItemLocal.Print_Nutritional_Label = Convert.ToBoolean(Session.selectedMenuItem.Print_Nutritional_Label);
                    cartItemLocal.Specialty_Pizza = Convert.ToBoolean(Session.selectedMenuItem.Specialty_Pizza);
                    cartItemLocal.Specialty_Pizza_Code = Session.selectedMenuItem.Specialty_Pizza_Code;
                    cartItemLocal.Topping_String = "";
                    cartItemLocal.Instruction_String = "";
                    cartItemLocal.Coupon_Code = "";
                    cartItemLocal.Topping_Codes = "";
                    cartItemLocal.Topping_Descriptions = "";
                    cartItemLocal.Coupon_Description = "";
                    cartItemLocal.New_Item = true;
                    cartItemLocal.Item_Modified = false;


                    cartItemLocal.MenuItemType = Session.selectedMenuItem.MenuItemType;

                    cartLocal.cartItems.Add(cartItemLocal);

                }

                cartLocal.Customer = Session.cart.Customer;

                cartLocal.cartHeader.CartId = Session.cart.cartHeader.CartId;
                cartLocal.cartHeader.LocationCode = Session._LocationCode;
                cartLocal.cartHeader.Order_Date = Session.cart.cartHeader.Order_Date == DateTime.MinValue ? Session.SystemDate : Session.cart.cartHeader.Order_Date;
                cartLocal.cartHeader.ctlAddressCity = cartLocal.Customer.City.Trim(); //Session.ctlAddressCity;
                cartLocal.cartHeader.Actual_Order_Date = Convert.ToDateTime(SystemSettings.settings.pdtmServerDateTime);
                cartLocal.cartHeader.Customer_Code = cartLocal.Customer.Customer_Code;
                cartLocal.cartHeader.Customer_Name = cartLocal.Customer.Name;
                cartLocal.cartHeader.Computer_Name = Session.ComputerName;
                cartLocal.cartHeader.Order_Status_Code = 1;
                cartLocal.cartHeader.Order_Taker_ID = Session.CurrentEmployee.LoginDetail.EmployeeCode;
                cartLocal.cartHeader.Order_Taker_Shift = Convert.ToString(Session.CurrentEmployee.LoginDetail.DateShiftNumber);
                cartLocal.cartHeader.Order_Time = 0;
                cartLocal.cartHeader.Order_Type_Code = Session.selectedOrderType;
                cartLocal.cartHeader.Added_By = Session.CurrentEmployee.LoginDetail.EmployeeCode;
                cartLocal.cartHeader.Workstation_ID = Session._WorkStationID;
                cartLocal.cartHeader.Modifying = "";
                cartLocal.cartHeader.Customer_Room = "";
                cartLocal.cartHeader.Comments = string.Empty;
                cartLocal.cartHeader.Coupon_Code = "";
                cartLocal.cartHeader.Driver_ID = "";
                cartLocal.cartHeader.Driver_Shift = "";
                cartLocal.cartHeader.Credit_Card_Name = "";
                cartLocal.cartHeader.Tent_Number = "";
                cartLocal.cartHeader.Secure_Coupon_ID = "";
                cartLocal.cartHeader.ROI_Customer = Session.selectedSourceName;
                cartLocal.cartHeader.Instruction = "";
                cartLocal.cartHeader.Types = "";
                cartLocal.cartHeader.Device_Type = "";
                cartLocal.cartHeader.Platform = "";
                cartLocal.cartHeader.Browser = "";
                cartLocal.cartHeader.Payment_Gateway = "";
                cartLocal.cartHeader.CustomField1 = "";
                cartLocal.cartHeader.CustomField2 = "";
                cartLocal.cartHeader.CustomField3 = "";
                cartLocal.cartHeader.CustomField4 = "";
                cartLocal.cartHeader.CustomField5 = "";
                cartLocal.cartHeader.CustomField7 = "";
                cartLocal.cartHeader.CustomField8 = "";
                cartLocal.cartHeader.CustomField9 = "";
                cartLocal.cartHeader.CustomField10 = "";
                cartLocal.cartHeader.OTS_Number = "";
                cartLocal.cartHeader.Delayed_Date = Session.cart.cartHeader.Delayed_Date;
                cartLocal.cartHeader.Delayed_Same_Day = Session.cart.cartHeader.Delayed_Same_Day;
                cartLocal.cartHeader.Kitchen_Display_Time = Session.cart.cartHeader.Kitchen_Display_Time;
                cartLocal.cartHeader.Delayed_Order = Session.cart.cartHeader.Delayed_Order;
                cartLocal.cartHeader.ODC_Tax = Session.ODC_Tax;
                //cart.cartHeader.SubTotal = cartResponse.cartItems[0].Price;
                //cart.cartHeader.Final_Total = cartResponse.cartItems[0].Order_Line_Total;


                selectedLineNumber = -1;

                //CartFunctions.UpdateCustomer(cartLocal);

                Session.cart = APILayer.Add2Cart(cartLocal);
            }
            else if (ParentMenu == "MenuItemSizes")
            {
                if (Session.ProcessingCombo && Session.CurrentComboItem > 0)
                {
                    ItemCombo itemCombo = Session.cart.itemCombos.Find(x => x.Combo_Group == Session.CurrentComboGroup);
                    if (itemCombo == null) return;
                    Session.selectedComboMenuItemSizes = Session.comboMenuItemSizes.Find(x => x.Combo_Menu_Code == itemCombo.Combo_Menu_Code && x.Combo_Size_Code == itemCombo.Combo_Size_Code && x.Size_Code == btnName && x.Menu_Code == Session.selectedComboMenuItem.Menu_Code && x.Item_Number == Session.CurrentComboItem);
                }
                else
                {
                    Session.selectedMenuItemSizes = Session.menuItemSizes.Find(x => x.Size_Code == btnName && x.Menu_Code == Session.selectedMenuItem.Menu_Code);
                }


                int LocLineNumber = selectedLineNumber;
                Cart cartLocal = (new Cart().GetCart());

                CartItem cartItemLocal = new CartItem();

                if (selectedLineNumber > -1)
                    cartItemLocal = Session.cart.cartItems.Find(x => x.Line_Number == LocLineNumber);
                else
                    cartItemLocal = Session.cart.cartItems[Session.cart.cartItems.Count - 1];

                if (cartItemLocal.Combo_Group > 0)
                {
                    cartItemLocal.Size_Code = Session.selectedComboMenuItemSizes.Size_Code;
                    cartItemLocal.Size_Description = Session.selectedComboMenuItemSizes.Description;
                    cartItemLocal.Menu_Price = Session.selectedComboMenuItemSizes.Price;
                    cartItemLocal.Menu_Price2 = Session.selectedComboMenuItemSizes.Price2;
                    cartItemLocal.Price_By_Weight = Session.selectedComboMenuItemSizes.Price_By_Weight;
                    cartItemLocal.Tare_Weight = Session.selectedComboMenuItemSizes.Tare_Weight;
                }
                else
                {
                    cartItemLocal.Size_Code = Session.selectedMenuItemSizes.Size_Code;
                    cartItemLocal.Size_Description = Session.selectedMenuItemSizes.Description;
                    cartItemLocal.Price = Convert.ToDecimal(Session.selectedMenuItemSizes.Price);
                    cartItemLocal.Base_Price = Convert.ToDecimal(Session.selectedMenuItemSizes.Price);
                    cartItemLocal.Menu_Price = Convert.ToDecimal(Session.selectedMenuItemSizes.Price);
                    cartItemLocal.Menu_Price2 = Convert.ToDecimal(Session.selectedMenuItemSizes.Price2);
                    cartItemLocal.Bottle_Deposit = Convert.ToDecimal(Session.selectedMenuItemSizes.Bottle_Deposit);
                    cartItemLocal.Price_By_Weight = Session.selectedMenuItemSizes.Price_By_Weight;
                    cartItemLocal.Open_Value_Card = Session.selectedMenuItemSizes.Open_Value_Card;
                    cartItemLocal.Min_Amount_Open_Value_Card = Convert.ToDecimal(Session.selectedMenuItemSizes.Min_Amount_Open_Value_Card);
                    cartItemLocal.Max_Amount_Open_Value_Card = Convert.ToDecimal(Session.selectedMenuItemSizes.Max_Amount_Open_Value_Card);
                    cartItemLocal.Tare_Weight = Convert.ToSingle(Session.selectedMenuItemSizes.Tare_Weight);
                }
                cartItemLocal.Base_Price = cartItemLocal.Menu_Price;
                cartItemLocal.Base_Price2 = cartItemLocal.Menu_Price2;
                cartItemLocal.Action = "M";

                cartLocal.cartItems.Add(cartItemLocal);


                bool pblnPromptDoubles = false;
                if (pblnPromptDoubles)
                {
                    if (APILayer.GetPromptForDoublesStatus(Session.cart.cartItems))
                    {
                        if (CustomMessageBox.Show(APILayer.GetCatalogText(LanguageConstant.cintUseDoublesPricing), CustomMessageBox.Buttons.YesNo, CustomMessageBox.Icon.Question) == DialogResult.No)
                            cartItemLocal.PromptDoubles = true;
                    }
                }


                cartLocal.Customer = Session.cart.Customer;

                cartLocal.cartHeader.CartId = Session.cart.cartHeader.CartId;
                if (Session.cart.cartHeader.ctlAddressCity != cartLocal.Customer.City.Trim())
                {
                    cartLocal.cartHeader = Session.cart.cartHeader;
                    cartLocal.cartHeader.Action = "M";
                    cartLocal.cartHeader.ctlAddressCity = cartLocal.Customer.City.Trim();
                }

                CartFunctions.UpdateCustomer(cartLocal);
                Session.cart = APILayer.Add2Cart(cartLocal);

                CouponFunctions.EDVUpsellRecalculationOnItemAdd();
            }
            else if (ParentMenu == "MenuCategories")
            {
                int LocalLineNumber = UserFunctions.ActualLineNumber(Session.cart, Session.SelectedLineNumber);

                if (!String.IsNullOrEmpty(Session.cart.cartHeader.CartId))
                {
                    CartResponse cartResponse = APILayer.GetCart(Session.cart.cartHeader.CartId);

                    if (cartResponse != null && cartResponse.cartItems != null)
                    {
                        if (cartResponse.cartItems.Count > 0 && LocalLineNumber > 0)
                        {
                            string description = "";
                            if (Session.ProcessingCombo && Session.CurrentComboItem > 0)
                            {
                                if (Session.selectedComboMenuItem != null && Session.selectedComboMenuItemSizes != null)
                                {
                                    CartItem item = Session.cart.cartItems.Find(x => x.Menu_Code == Session.selectedComboMenuItem.Menu_Code && x.Description.Contains(Session.selectedComboMenuItemSizes.Description));
                                    if (item != null) description = item.Description;
                                }
                            }
                            else
                            {
                                if (Session.selectedMenuItem != null && Session.selectedMenuItemSizes != null)
                                {
                                    CartItem item = Session.cart.cartItems.Find(x => x.Menu_Code == Session.selectedMenuItem.Menu_Code && x.Description.Contains(Session.selectedMenuItemSizes.Description));
                                    if (item != null) description = item.Description;
                                }
                            }

                            CartItem item1 = Session.cart.cartItems.Find(x => x.Description == description && x.Line_Number == LocalLineNumber);
                            if (item1 != null) Session.cart.cartItems.Find(x => x.Description == description && x.Line_Number == LocalLineNumber).MenuItemType = cartResponse.cartItems.Find(x => x.Description == description && x.Line_Number == LocalLineNumber).MenuItemType;
                        }
                    }
                }
            }
            else if (ParentMenu == "OrderCoupons")
            {
                Cart cartLocal = (new Cart().GetCart());
                if (btnName != "D")
                {
                    Session.selectedOrderCoupon = Session.OrderCoupons.Find(x => x.Coupon_Code == btnName);
                    cartLocal.Customer = Session.cart.Customer;
                    cartLocal.cartHeader = Session.cart.cartHeader;
                    cartLocal.cartHeader.Coupon_Code = Session.selectedOrderCoupon.Coupon_Code;
                    cartLocal.cartHeader.Coupon_Taxable = Session.selectedOrderCoupon.Taxable;
                    cartLocal.cartHeader.Coupon_Total = Session.selectedOrderCoupon.Amount;
                    cartLocal.cartHeader.Coupon_Type_Code = Session.selectedOrderCoupon.Coupon_Type_Code;
                    cartLocal.cartHeader.Coupon_Adjustment = Session.selectedOrderCoupon.Adjustment;
                    cartLocal.cartHeader.Coupon_Amount = Session.selectedOrderCoupon.Amount;
                }
                else
                {
                    //Session.selectedOrderCoupon = Session.OrderCoupons.Find(x => x.Coupon_Code == btnName);
                    cartLocal.Customer = Session.cart.Customer;
                    cartLocal.cartHeader = Session.cart.cartHeader;
                    cartLocal.cartHeader.Coupon_Code = "";
                    //cartLocal.cartHeader.Coupon_Taxable = Session.selectedOrderCoupon.Taxable;
                    cartLocal.cartHeader.Coupon_Total = 0;
                    cartLocal.cartHeader.Coupon_Type_Code = 0;
                    //cartLocal.cartHeader.Coupon_Adjustment = Session.selectedOrderCoupon.Adjustment;
                    cartLocal.cartHeader.Coupon_Amount = 0;


                }

                bool blnLoginSuccessful = false;

                if (cartLocal.cartHeader.Coupon_Type_Code == Constants.VariablePrice || cartLocal.cartHeader.Coupon_Type_Code == Constants.VariableDiscount)
                {
                    if (Session.selectedOrderCoupon.Protect_Coupon)
                    {
                        if (!String.IsNullOrEmpty(Session.CurrentEmployee.LoginDetail.EmployeeCode) && Session.CurrentEmployee.LoginDetail.blnVariableCoupons)
                        {
                            //if (SystemSettings.settings.pblnRequirePasswordForSpecialAccess)
                            //{
                            //    if (!EmployeeFunctions.MatchEmployeePassword())
                            //    {
                            //        cartLocal.cartHeader.Coupon_Code = string.Empty;
                            //        return;
                            //    }
                            //}

                            if (SystemSettings.settings.pblnRequirePasswordForSpecialAccess)
                            {
                                if (EmployeeFunctions.MatchEmployeePassword())
                                {
                                    blnLoginSuccessful = true;
                                }
                                else
                                {
                                    cartLocal.cartHeader.Coupon_Code = string.Empty;
                                    blnLoginSuccessful = false;
                                }
                            }
                            else
                                blnLoginSuccessful = true;
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
                                if (!string.IsNullOrEmpty(Session.CurrentEmployee.LoginDetail.EmployeeCode) && Session.CurrentEmployee.LoginDetail.blnVariableCoupons)
                                {
                                    blnLoginSuccessful = true;
                                }
                                else
                                {
                                    blnLoginSuccessful = false;
                                    cartLocal.cartHeader.Coupon_Code = string.Empty;
                                    CustomMessageBox.Show(APILayer.GetCatalogText(LanguageConstant.cintMSGInsufficientPrivileges), CustomMessageBox.Buttons.OK, CustomMessageBox.Icon.Warning);
                                    //return;
                                }
                            }
                            else
                                blnLoginSuccessful = false;
                            Session.CurrentEmployee = oldLoginEmployee;
                        }
                    }

                    if (blnLoginSuccessful)
                    {
                        frmKeyBoardNumeric obj = new frmKeyBoardNumeric();
                        obj.Text = Session.selectedOrderCoupon.Description;
                        obj.ShowDialog();


                        if (Convert.ToDecimal(obj.txt_Input.Text) > (Session.cart.cartHeader.SubTotal + Session.cart.cartHeader.Order_Adjustments))
                        {
                            cartLocal.cartHeader.Coupon_Code = string.Empty;

                            GetTextRequest getTextRequest = new GetTextRequest();
                            getTextRequest.LocationCode = Session._LocationCode;
                            getTextRequest.LanguageCode = Constants.LanguageCode;
                            getTextRequest.KeyField = Constants.InvalidCouponAmount;

                            CustomMessageBox.Show(APILayer.GetText(APILayer.CallType.POST, getTextRequest), CustomMessageBox.Buttons.OK, CustomMessageBox.Icon.Warning);
                            return;
                        }

                        List<CatalogReasons> lstCatalogReasons = new List<CatalogReasons>();
                        lstCatalogReasons = APILayer.GetCatalogReasons(SystemSettings.settings.pstrDefault_Location_Code, Convert.ToInt32(Session.CurrentEmployee.LoginDetail.LanguageCode), Convert.ToInt32(enumReasonGroupID.Coupon));

                        if (lstCatalogReasons != null && lstCatalogReasons.Count > 0)
                        {
                            frmReason objfrmReason = new frmReason(Convert.ToInt32(enumReasonGroupID.Coupon));
                            objfrmReason.SelectedLineNumber = selectedLineNumber;
                            //objfrmReason.HighlightItemReason(selectedLineNumber);
                            objfrmReason.ShowDialog();
                        }

                        cartLocal.cartHeader.Coupon_Amount = Convert.ToDecimal(obj.txt_Input.Text);
                    }
                }
                //cartLocal.cartHeader.Order_Coupon_Total = Session.selectedOrderCoupon.
                //cartLocal.cartHeader.Order_Line_Coupon_Total = Session.selectedOrderCoupon.Coupon_Code;

                cartLocal.cartHeader.Action = "M";
                //CartFunctions.UpdateCustomer(cartLocal);
                Session.cart = APILayer.Add2Cart(cartLocal);

            }
            else if (ParentMenu == "OrderLineCoupons")
            {
                //List<CatalogMenuItemEDVCoupon> catalogMenuItemEDVCouponsList = new List<CatalogMenuItemEDVCoupon>();
                bool blnLoginSuccessful = false;
                //EmployeeResult oldEmployeeResult;
                decimal InputVariableCouponAmount;
                int LocLineNumber = selectedLineNumber;
                CatalogCoupons selectedOrderLineCoupon = null;
                Cart cartLocal = (new Cart().GetCart());
                CartItem cartItemLocal = new CartItem();
                bool MultipleLineCouponApplied = false;
                string CurrentEDVCoupon = string.Empty;

                if (selectedLineNumber > -1)
                    cartItemLocal = Session.cart.cartItems.Find(x => x.Line_Number == LocLineNumber);
                else
                    cartItemLocal = Session.cart.cartItems[Session.cart.cartItems.Count - 1];


                if (!string.IsNullOrEmpty(cartItemLocal.Coupon_Code) && cartItemLocal.isEDVCoupon && cartItemLocal.Coupon_Code != btnName)
                    CurrentEDVCoupon = cartItemLocal.Coupon_Code;



                if (btnName == "D")
                {
                    if (CouponFunctions.GetAppliedEDVCoupons().Count > 0)
                    {
                        string CurrentCouponCode = cartItemLocal.Coupon_Code;
                        foreach (CartItem cartItem in Session.cart.cartItems)
                        {
                            if (cartItem.Coupon_Code == CurrentCouponCode)
                            {
                                cartItem.Coupon_Code = "";
                                cartItem.Coupon_Description = "";
                                cartItem.Coupon_Type_Code = 0;
                                cartItem.Coupon_Taxable = false;
                                cartItem.Coupon_Adjustment = false;
                                cartItem.Coupon_Min_Price = 0;
                                cartItem.Coupon_Amount = 0;
                                cartItem.Order_Line_No_Tax_Discount = 0;
                                cartItem.Order_Line_Coupon_Amount = 0;
                                cartItem.Order_Line_Tax_Discount = 0;
                                cartItem.isEDVCoupon = false;
                                cartItem.isUpsellCoupon = false;
                                cartItem.Adjustment = 0;

                                MultipleLineCouponApplied = true;
                            }
                        }
                    }
                    else
                    {
                        cartItemLocal.Coupon_Code = "";
                        cartItemLocal.Coupon_Description = "";
                        cartItemLocal.Coupon_Type_Code = 0;
                        cartItemLocal.Coupon_Taxable = false;
                        cartItemLocal.Coupon_Adjustment = false;
                        cartItemLocal.Coupon_Min_Price = 0;
                        cartItemLocal.Coupon_Amount = 0;
                        cartItemLocal.Order_Line_No_Tax_Discount = 0;
                        cartItemLocal.Order_Line_Coupon_Amount = 0;
                        cartItemLocal.Order_Line_Tax_Discount = 0;
                        cartItemLocal.isEDVCoupon = false;
                        cartItemLocal.isUpsellCoupon = false;
                        cartItemLocal.Adjustment = 0;

                        cartLocal.Customer = Session.cart.Customer;
                        cartLocal.cartHeader = Session.cart.cartHeader;
                    }

                }
                else
                {
                    MultipleLineCouponApplied = CouponFunctions.ApplyOrderLineCoupon(btnName);

                    if (!MultipleLineCouponApplied)
                    {
                        selectedOrderLineCoupon = Session.orderLineCoupons.Find(x => x.Coupon_Code == btnName);

                        cartItemLocal.Coupon_Code = btnName;
                        cartItemLocal.Coupon_Description = selectedOrderLineCoupon.Description;
                        cartItemLocal.Coupon_Type_Code = selectedOrderLineCoupon.Coupon_Type_Code;
                        cartItemLocal.Coupon_Taxable = selectedOrderLineCoupon.Taxable;
                        cartItemLocal.Coupon_Adjustment = selectedOrderLineCoupon.Adjustment;
                        cartItemLocal.Coupon_Min_Price = selectedOrderLineCoupon.Min_Price;
                        cartItemLocal.Coupon_Amount = selectedOrderLineCoupon.Amount;
                        cartItemLocal.isEDVCoupon = false;
                        cartItemLocal.isUpsellCoupon = false;

                        if (!string.IsNullOrEmpty(CurrentEDVCoupon))
                        {
                            foreach (CartItem cartItem in Session.cart.cartItems)
                            {
                                if (cartItem.Coupon_Code == CurrentEDVCoupon)
                                {
                                    cartItem.Coupon_Code = "";
                                    cartItem.Coupon_Description = "";
                                    cartItem.Coupon_Type_Code = 0;
                                    cartItem.Coupon_Taxable = false;
                                    cartItem.Coupon_Adjustment = false;
                                    cartItem.Coupon_Min_Price = 0;
                                    cartItem.Coupon_Amount = 0;
                                    cartItem.Order_Line_No_Tax_Discount = 0;
                                    cartItem.Order_Line_Coupon_Amount = 0;
                                    cartItem.Order_Line_Tax_Discount = 0;
                                    cartItem.isEDVCoupon = false;
                                    cartItem.isUpsellCoupon = false;
                                    cartItem.Adjustment = 0;

                                    MultipleLineCouponApplied = true;
                                }
                            }
                        }
                    }
                }


                cartLocal.Customer = Session.cart.Customer;
                cartLocal.cartHeader = Session.cart.cartHeader;
                //cartLocal.cartHeader.aapplyCoupon = btnName;

                if (cartItemLocal.Coupon_Type_Code == Constants.VariablePrice || cartItemLocal.Coupon_Type_Code == Constants.VariableDiscount)
                {
                    oldLoginEmployee = Session.CurrentEmployee;
                    if (!String.IsNullOrEmpty(Session.CurrentEmployee.LoginDetail.EmployeeCode) && Session.CurrentEmployee.LoginDetail.blnVariableCoupons)
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
                            blnLoginSuccessful = true;
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
                            if (!string.IsNullOrEmpty(Session.CurrentEmployee.LoginDetail.EmployeeCode) && Session.CurrentEmployee.LoginDetail.blnVariableCoupons)
                            {
                                blnLoginSuccessful = true;
                            }
                            else
                            {
                                blnLoginSuccessful = false;
                                CustomMessageBox.Show(APILayer.GetCatalogText(LanguageConstant.cintMSGInsufficientPrivileges), CustomMessageBox.Buttons.OK, CustomMessageBox.Icon.Warning);
                            }
                        }
                        else
                            blnLoginSuccessful = false;

                        Session.CurrentEmployee = oldLoginEmployee;
                    }

                    if (blnLoginSuccessful)
                        cartLocal.cartHeader.Secure_Coupon_ID = Session.CurrentEmployee.LoginDetail.EmployeeCode;

                    //Session.CurrentEmployee = oldLoginEmployee;

                    if (blnLoginSuccessful)
                    {
                        InputVariableCouponAmount = 0;
                        frmKeyBoardNumeric obj = new frmKeyBoardNumeric();
                        obj.Text = selectedOrderLineCoupon.Description;
                        obj.ShowDialog();
                        InputVariableCouponAmount = Convert.ToDecimal(obj.txt_Input.Text);

                        if (InputVariableCouponAmount > cartItemLocal.Price && cartItemLocal.Coupon_Type_Code == Constants.VariableDiscount)
                        {
                            CustomMessageBox.Show(APILayer.GetCatalogText(LanguageConstant.cintMSGInvalidCouponAmount), CustomMessageBox.Buttons.OK, CustomMessageBox.Icon.Warning);
                            return;
                        }
                        else
                        {
                            cartItemLocal.Coupon_Amount = InputVariableCouponAmount;
                        }

                        List<CatalogReasons> lstCatalogReasons = new List<CatalogReasons>();
                        lstCatalogReasons = APILayer.GetCatalogReasons(SystemSettings.settings.pstrDefault_Location_Code, Convert.ToInt32(Session.CurrentEmployee.LoginDetail.LanguageCode), Convert.ToInt32(enumReasonGroupID.Coupon));

                        if (lstCatalogReasons != null && lstCatalogReasons.Count > 0)
                        {
                            frmReason objfrmReason = new frmReason(Convert.ToInt32(enumReasonGroupID.Coupon));
                            objfrmReason.SelectedLineNumber = selectedLineNumber;
                            //objfrmReason.HighlightItemReason(selectedLineNumber);
                            objfrmReason.ShowDialog();

                            ItemReason itemReason = null;
                            if (cartItemLocal.itemReasons != null)
                                itemReason = cartItemLocal.itemReasons.Find(x => x.Reason_Group_Code == Convert.ToInt32(enumReasonGroupID.Coupon) && x.Line_Number == LocLineNumber);

                            if (itemReason == null)
                            {
                                cartItemLocal.Coupon_Code = "";
                                cartItemLocal.Coupon_Description = "";
                                cartItemLocal.Coupon_Type_Code = 0;
                                cartItemLocal.Coupon_Taxable = false;
                                cartItemLocal.Coupon_Adjustment = false;
                                cartItemLocal.Coupon_Min_Price = 0;
                                cartItemLocal.Coupon_Amount = 0;
                            }

                        }
                    }
                    else
                    {
                        cartItemLocal.Coupon_Code = "";
                        cartItemLocal.Coupon_Description = "";
                        cartItemLocal.Coupon_Type_Code = 0;
                        cartItemLocal.Coupon_Taxable = false;
                        cartItemLocal.Coupon_Adjustment = false;
                        cartItemLocal.Coupon_Min_Price = 0;
                        cartItemLocal.Coupon_Amount = 0;
                    }
                }
                else if (selectedOrderLineCoupon != null && selectedOrderLineCoupon.Protect_Coupon)
                {
                    oldLoginEmployee = Session.CurrentEmployee;
                    if (!String.IsNullOrEmpty(Session.CurrentEmployee.LoginDetail.EmployeeCode) && Session.CurrentEmployee.LoginDetail.blnUseProtectedCoupon)
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
                            blnLoginSuccessful = true;
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
                            if (!string.IsNullOrEmpty(Session.CurrentEmployee.LoginDetail.EmployeeCode) && Session.CurrentEmployee.LoginDetail.blnUseProtectedCoupon)
                            {
                                blnLoginSuccessful = true;
                            }
                            else
                            {
                                blnLoginSuccessful = false;
                                CustomMessageBox.Show(APILayer.GetCatalogText(LanguageConstant.cintMSGInsufficientPrivileges), CustomMessageBox.Buttons.OK, CustomMessageBox.Icon.Warning);
                            }
                        }
                        else
                            blnLoginSuccessful = false;

                        Session.CurrentEmployee = oldLoginEmployee;
                    }

                    if (blnLoginSuccessful)
                        cartLocal.cartHeader.Secure_Coupon_ID = Session.CurrentEmployee.LoginDetail.EmployeeCode;

                    //Session.CurrentEmployee  = oldLoginEmployee;

                    if (blnLoginSuccessful)
                    {
                        cartItemLocal.Coupon_Amount = selectedOrderLineCoupon.Amount;
                    }
                    else
                    {
                        cartItemLocal.Coupon_Code = "";
                        cartItemLocal.Coupon_Description = "";
                        cartItemLocal.Coupon_Type_Code = 0;
                        cartItemLocal.Coupon_Taxable = false;
                        cartItemLocal.Coupon_Adjustment = false;
                        cartItemLocal.Coupon_Min_Price = 0;
                        cartItemLocal.Coupon_Amount = 0;
                    }
                }

                if (MultipleLineCouponApplied)
                {
                    cartLocal.cartItems = Session.cart.cartItems;
                    foreach (CartItem cartItem1 in cartLocal.cartItems)
                        cartItem1.Action = "M";
                }
                else
                {
                    cartItemLocal.Action = "M";
                    cartLocal.cartItems.Add(cartItemLocal);
                }

                cartLocal.cartHeader.Action = "M";
                CartFunctions.UpdateCustomer(cartLocal);
                Session.cart = APILayer.Add2Cart(cartLocal);
            }
        }

        public static void CheckCart()
        {
            if (Session.cart == null)
            {
                Session.cart = (new Cart()).GetCart();
            }
        }

        public static CartItem GetCurrentCartItem(int selectedLineNumber)
        {
            CartItem cartItemLocal = null;
            if (Session.cart.cartItems.Count > 0)
            {
                if (selectedLineNumber > -1)
                    cartItemLocal = Session.cart.cartItems.Find(x => x.Line_Number == selectedLineNumber);
                else
                    cartItemLocal = Session.cart.cartItems[Session.cart.cartItems.Count - 1];
            }

            return cartItemLocal;
        }

        public static void FillCustomerToCart(ref Cart cart)
        {
            CheckCart();

            if (Session.cart.Customer != null)
            {
                cart.Customer.CartId = String.IsNullOrEmpty(cart.cartHeader.CartId) ? "" : cart.cartHeader.CartId;
                cart.Customer.Location_Code = String.IsNullOrEmpty(Session.cart.Customer.Location_Code) ? "" : Session.cart.Customer.Location_Code;
                cart.Customer.Phone_Number = String.IsNullOrEmpty(Session.cart.Customer.Phone_Number) ? "" : Session.cart.Customer.Phone_Number;
                cart.Customer.Phone_Ext = String.IsNullOrEmpty(Session.cart.Customer.Phone_Ext) ? "" : Session.cart.Customer.Phone_Ext;
                cart.Customer.Customer_Code = Convert.ToInt32(Session.cart.Customer.Customer_Code);
                cart.Customer.Name = String.IsNullOrEmpty(Session.cart.Customer.Name) ? "" : Session.cart.Customer.Name;
                cart.Customer.Company_Name = String.IsNullOrEmpty(Session.cart.Customer.Company_Name) ? "" : Session.cart.Customer.Company_Name;
                cart.Customer.Street_Number = String.IsNullOrEmpty(Session.cart.Customer.Street_Number) ? "" : Session.cart.Customer.Street_Number;
                cart.Customer.Street_Code = Session.cart.Customer.Street_Code == 0 ? 1 : Session.cart.Customer.Street_Code;
                cart.Customer.Cross_Street_Code = Session.cart.Customer.Cross_Street_Code;
                cart.Customer.Suite = String.IsNullOrEmpty(Session.cart.Customer.Suite) ? "" : Session.cart.Customer.Suite;
                cart.Customer.Address_Line_2 = String.IsNullOrEmpty(Session.cart.Customer.Address_Line_2) ? "" : Session.cart.Customer.Address_Line_2;
                cart.Customer.Address_Line_3 = String.IsNullOrEmpty(Session.cart.Customer.Address_Line_3) ? "" : Session.cart.Customer.Address_Line_3;
                cart.Customer.Address_Line_4 = String.IsNullOrEmpty(Session.cart.Customer.Address_Line_4) ? "" : Session.cart.Customer.Address_Line_4;
                cart.Customer.Mailing_Address = "";
                cart.Customer.Postal_Code = String.IsNullOrEmpty(Session.cart.Customer.Postal_Code) ? "" : Session.cart.Customer.Postal_Code;
                cart.Customer.Plus4 = "";
                cart.Customer.Cart = "";
                cart.Customer.Delivery_Point_Code = "";
                cart.Customer.Walk_Sequence = "";
                cart.Customer.Address_Type = String.IsNullOrEmpty(Session.cart.Customer.Address_Type) ? "" : Session.cart.Customer.Address_Type;
                cart.Customer.Set_Discount = 0;
                cart.Customer.Tax_Exempt1 = Session.cart.Customer.Tax_Exempt1;
                cart.Customer.Tax_ID1 = String.IsNullOrEmpty(Session.cart.Customer.Tax_ID1) ? "" : Session.cart.Customer.Tax_ID1;
                cart.Customer.Tax_Exempt2 = Session.cart.Customer.Tax_Exempt2;
                cart.Customer.Tax_ID2 = String.IsNullOrEmpty(Session.cart.Customer.Tax_ID2) ? "" : Session.cart.Customer.Tax_ID2;
                cart.Customer.Tax_Exempt3 = Session.cart.Customer.Tax_Exempt3;
                cart.Customer.Tax_ID3 = String.IsNullOrEmpty(Session.cart.Customer.Tax_ID3) ? "" : Session.cart.Customer.Tax_ID3;
                cart.Customer.Tax_Exempt4 = Session.cart.Customer.Tax_Exempt4;
                cart.Customer.Tax_ID4 = String.IsNullOrEmpty(Session.cart.Customer.Tax_ID4) ? "" : Session.cart.Customer.Tax_ID4;
                cart.Customer.Accept_Checks = false;
                cart.Customer.Accept_Credit_Cards = false;
                cart.Customer.Accept_Gift_Cards = false;
                cart.Customer.Accept_Charge_Account = false;
                cart.Customer.Accept_Cash = false;
                cart.Customer.Finance_Charge_Rate = Session.cart.Customer.Finance_Charge_Rate;
                cart.Customer.Credit_Limit = 0;
                cart.Customer.Credit = 0;
                cart.Customer.Payment_Terms = 0;
                cart.Customer.First_Order_Date = Session.cart.Customer.First_Order_Date == Convert.ToDateTime("01-01-0001") ? new DateTime(1899, 12, 30) : Session.cart.Customer.First_Order_Date;
                cart.Customer.Last_Order_Date = Session.cart.Customer.Last_Order_Date == Convert.ToDateTime("01-01-0001") ? new DateTime(1899, 12, 30) : Session.cart.Customer.Last_Order_Date;
                cart.Customer.Added_By = String.IsNullOrEmpty(Session.cart.Customer.Added_By) ? "" : Session.cart.Customer.Added_By;
                cart.Customer.Comments = String.IsNullOrEmpty(Session.cart.Customer.Comments) ? "" : Session.cart.Customer.Comments;
                cart.Customer.DriverComments = String.IsNullOrEmpty(Session.cart.Customer.DriverComments) ? "" : Session.cart.Customer.DriverComments;
                cart.Customer.DriverCommentsAddUpdateDelete = false;
                cart.Customer.Manager_Notes = String.IsNullOrEmpty(Session.cart.Customer.Manager_Notes) ? "" : Session.cart.Customer.Manager_Notes;
                cart.Customer.Customer_City_Code = Session.cart.Customer.Customer_City_Code;
                cart.Customer.Customer_Street_Name = Session.cart.Customer.Customer_Street_Name;
                cart.Customer.HotelorCollege = false;
                cart.Customer.City = String.IsNullOrEmpty(Session.cart.Customer.City) ? "" : Session.cart.Customer.City;
                cart.Customer.Region = String.IsNullOrEmpty(Session.cart.Customer.Region) ? "" : Session.cart.Customer.Region;
                cart.Customer.TaxRate1 = 0;
                cart.Customer.TaxRate2 = 0;
                cart.Customer.Cross_Street = "";
                cart.Customer.NoteAddUpdateDelete = false;
                cart.Customer.gstin_number = String.IsNullOrEmpty(Session.cart.Customer.gstin_number) ? "" : Session.cart.Customer.gstin_number;
                cart.Customer.date_of_birth = Session.cart.Customer.date_of_birth == Convert.ToDateTime("01-01-0001") ? new DateTime(1899, 12, 30) : Session.cart.Customer.date_of_birth;
                cart.Customer.anniversary_date = Session.cart.Customer.anniversary_date == Convert.ToDateTime("01-01-0001") ? new DateTime(1899, 12, 30) : Session.cart.Customer.anniversary_date;
                cart.Customer.Action = "M";

            }
        }

        public static bool OrderTypeChange()
        {
            if (Session.cart != null && Session.cart.cartHeader.CartId != null && Session.cart.cartHeader.CartId != "")
            {
                Session.cart.cartHeader.Order_Type_Code = Session.selectedOrderType;
                Cart cartLocal = (new Cart().GetCart());
                cartLocal.cartHeader = Session.cart.cartHeader;
                cartLocal.cartHeader.CartId = Session.cart.cartHeader.CartId;
                cartLocal.cartHeader.Action = "M";
                CartFunctions.UpdateCustomer(cartLocal);
                Session.cart = APILayer.Add2Cart(cartLocal);

                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool QuantityChange(bool Replace, int Qty, int lselectedLineNumber, bool removeFromDict = true)
        {
            if (Session.cart != null && Session.cart.cartItems.Count > 0)
            {
                bool CallCartAPI = false;
                float NewQuantity = 0;
                Cart cartLocal = (new Cart().GetCart());
                CartItem cartItemLocal = new CartItem();
                List<string> EDVCoupons = CouponFunctions.GetAppliedEDVCoupons();
                List<string> UpsellCoupons = CouponFunctions.GetAppliedUpsellCoupons();

                if (lselectedLineNumber > -1)
                    cartItemLocal = Session.cart.cartItems.Find(x => x.Line_Number == lselectedLineNumber);
                else
                    cartItemLocal = Session.cart.cartItems[Session.cart.cartItems.Count - 1];

                NewQuantity = cartItemLocal.Quantity;

                if (Replace)
                    NewQuantity = Qty < 0 ? 0 : Qty;
                else
                    NewQuantity = NewQuantity + (Qty);

                if (removeFromDict)
                {
                    if (NewQuantity <= 0)
                    {
                        if (CustomMessageBox.Show(cartItemLocal.Description + " " + APILayer.GetCatalogText(LanguageConstant.cintMSGRemoveItemFromOrder), CustomMessageBox.Buttons.YesNo, CustomMessageBox.Icon.Question) == DialogResult.No)
                        {
                            return false;
                        }
                    }
                }

                cartItemLocal.Quantity = NewQuantity;
                cartItemLocal.Action = cartItemLocal.Quantity <= 0 ? "D" : "M";

                if (EDVCoupons.Count > 0 || UpsellCoupons.Count > 0)
                {
                    foreach (CartItem cartItem in Session.cart.cartItems)
                    {
                        if (cartItem.isEDVCoupon || cartItem.isUpsellCoupon)
                        {
                            cartItem.Coupon_Code = "";
                            cartItem.Coupon_Description = "";
                            cartItem.Coupon_Amount = 0;

                            cartItem.Coupon_Adjustment = false;
                            cartItem.Coupon_Min_Price = 0;
                            cartItem.Coupon_Taxable = false;
                            cartItem.Order_Line_No_Tax_Discount = 0;
                            cartItem.Order_Line_Coupon_Amount = 0;
                            cartItem.Order_Line_Tax_Discount = 0;
                            cartItem.Adjustment = 0;
                            cartItem.isEDVCoupon = false;
                            cartItem.isUpsellCoupon = false;

                            Session.cart.cartHeader.aapplyCoupon = "";
                        }
                    }

                    List<CatalogCoupons> currentCatalogCoupons = CouponFunctions.GetCouponsForCurrentCartItems();

                    if (currentCatalogCoupons != null && currentCatalogCoupons.Count > 0)
                    {
                        foreach (string EDV in EDVCoupons)
                        {
                            if (currentCatalogCoupons.FindIndex(x => x.Coupon_Code == EDV) > -1) CouponFunctions.ApplyOrderLineCoupon(EDV);

                            cartLocal.cartItems = Session.cart.cartItems;
                            foreach (CartItem cartItem1 in cartLocal.cartItems)
                                cartItem1.Action = cartItem1.Action != "D" ? "M" : cartItem1.Action;

                            cartLocal.Customer = Session.cart.Customer;
                            cartLocal.cartHeader = Session.cart.cartHeader;
                            cartLocal.cartHeader.Action = "M";
                            CartFunctions.UpdateCustomer(cartLocal);
                            Session.cart = APILayer.Add2Cart(cartLocal);

                            CallCartAPI = true;
                        }

                        foreach (string Upsell in UpsellCoupons)
                        {
                            if (currentCatalogCoupons.FindIndex(x => x.Coupon_Code == Upsell) > -1) CouponFunctions.ApplyOrderLineCoupon(Upsell);

                            cartLocal.cartItems = Session.cart.cartItems;
                            foreach (CartItem cartItem1 in cartLocal.cartItems)
                                cartItem1.Action = cartItem1.Action != "D" ? "M" : cartItem1.Action;

                            cartLocal.Customer = Session.cart.Customer;
                            cartLocal.cartHeader = Session.cart.cartHeader;
                            cartLocal.cartHeader.Action = "M";
                            CartFunctions.UpdateCustomer(cartLocal);
                            Session.cart = APILayer.Add2Cart(cartLocal);

                            CallCartAPI = true;
                        }
                    }

                    if (!CallCartAPI)
                    {
                        cartLocal.cartItems = Session.cart.cartItems;
                        foreach (CartItem cartItem1 in cartLocal.cartItems)
                            cartItem1.Action = cartItem1.Action != "D" ? "M" : cartItem1.Action;

                        cartLocal.Customer = Session.cart.Customer;
                        cartLocal.cartHeader = Session.cart.cartHeader;
                        cartLocal.cartHeader.Action = "M";
                        CartFunctions.UpdateCustomer(cartLocal);
                        Session.cart = APILayer.Add2Cart(cartLocal);
                    }
                }
                else
                {
                    cartLocal.cartItems.Add(cartItemLocal);
                    cartLocal.cartHeader.CartId = Session.cart.cartHeader.CartId;
                    CartFunctions.UpdateCustomer(cartLocal);
                    Session.cart = APILayer.Add2Cart(cartLocal);
                }

                if (Session.cart.cartHeader.Order_Adjustments <= 0 && Session.cart.cartHeader.Order_Coupon_Total <= 0 && !String.IsNullOrEmpty(Session.cart.cartHeader.Coupon_Code))
                {
                    Cart cartLocal1 = (new Cart().GetCart());
                    cartLocal1.Customer = Session.cart.Customer;
                    cartLocal1.cartHeader = Session.cart.cartHeader;
                    cartLocal1.cartHeader.Action = "M";
                    cartLocal1.cartHeader.Coupon_Code = "";
                    Session.cart = APILayer.Add2Cart(cartLocal1);
                }




                if (Session.cart.cartItems.Count > 0)
                {
                    if (Session.selectedMenuItems != null)
                    {
                        foreach (KeyValuePair<string, Nullable<Boolean>> item in Session.selectedMenuItems)
                        {
                            CartItem cartItem = Session.cart.cartItems.Where(x => x.Menu_Code == item.Key).FirstOrDefault();
                            if (cartItem != null)
                            {
                                if (string.IsNullOrEmpty(cartItem.Menu_Code))
                                {
                                    Session.selectedMenuItems.Remove(item.Key);
                                    break;
                                }
                            }
                            else
                            {
                                Session.selectedMenuItems.Remove(item.Key);
                                break;
                            }
                        }
                    }
                }
                else
                {
                    Session.selectedMenuItems = new Dictionary<string, bool?>();
                }

                return true;
            }
            else
            {
                return false;
            }
        }

        public static void AddReasonTocart(CartItem cartItems)
        {
            Cart cartLocal = (new Cart().GetCart());
            cartLocal.cartItems.Add(cartItems);

            cartLocal.cartHeader = Session.cart.cartHeader;
            CartFunctions.UpdateCustomer(cartLocal);
            Session.cart = APILayer.Add2Cart(cartLocal);
        }

        public static void GenerateTicketCollection(DataTable dtCart)
        {
            Session.TicketCollection = new List<TicketFields>();
            if (dtCart != null && dtCart.Rows.Count > 0)
            {
                foreach (DataRow dr in dtCart.Rows)
                {
                    TicketFields ticketField = new TicketFields();
                    ticketField.LineType = Convert.ToString(dr["LineType"]);
                    ticketField.ItemCode = Convert.ToString(dr["Menu_Code"]);
                    ticketField.Quantity = Convert.ToString(dr["Qty"]);
                    ticketField.ItemDescription = Convert.ToString(dr["Item"]);
                    ticketField.Price = Convert.ToString(dr["Price"]);
                    ticketField.DoublesGroup = string.Empty;
                    ticketField.OrderLine_Line_Number = Convert.ToInt32(Convert.ToString(dr["Line_Number"]));
                    ticketField.Group_Code = string.Empty;
                    ticketField.Combo_Group = 0;
                    ticketField.Combo_Item_Number = 0;
                    ticketField.Line_Complete = true;
                    ticketField.Order_Line_Adjustments = string.Empty;

                    Session.TicketCollection.Add(ticketField);
                }
            }
        }

        public static bool ToppingChosen(string strButtonCaption, string strButtonTag, bool blnSelected, string strAmount, string strItemPart, bool blnDefaultTopping, string strDefaultAmount, int ButtonIndex, Color ButtonColor, int intChosen, int SelectedLineNumber)
        {
            if (Session.cart != null && Session.cart.cartItems.Count > 0)
            {
                int index = -1;
                ItemOption itemOption = null;
                Cart cartLocal = (new Cart().GetCart());

                SelectedLineNumber = UserFunctions.ActualLineNumber(Session.cart, SelectedLineNumber);
                Session.SelectedLineNumber = SelectedLineNumber;

                CartItem curretCartItem = null;
                if (Session.ProcessingCombo && Session.CurrentComboItem > 0)
                    curretCartItem = Session.cart.cartItems.Find(x => x.Menu_Code == Session.selectedComboMenuItem.Menu_Code && x.Size_Code == Session.selectedComboMenuItemSizes.Size_Code && x.Line_Number == (SelectedLineNumber > 0 ? SelectedLineNumber : x.Line_Number));
                else
                    curretCartItem = Session.cart.cartItems.Find(x => x.Menu_Code == Session.selectedMenuItem.Menu_Code && x.Size_Code == Session.selectedMenuItemSizes.Size_Code && x.Line_Number == (SelectedLineNumber > 0 ? SelectedLineNumber : x.Line_Number));


                if (blnSelected && !blnDefaultTopping && strItemPart == "W")
                {
                    if (Session.currentToppingCollection.pizzaToppings.Count > 0)
                    {
                        index = -1;
                        index = Session.currentToppingCollection.pizzaToppings.FindIndex(x => x.ButtonIndex == ButtonIndex && (x.ItemPart == "1" || x.ItemPart == "2") && x.DefaultTopping == "");
                        if (index > -1)
                            Session.currentToppingCollection.pizzaToppings.RemoveAt(index);
                    }

                    if (curretCartItem.itemOptions != null && curretCartItem.itemOptions.Count > 0)
                    {
                        index = -1;
                        index = curretCartItem.itemOptions.FindIndex(x => x.Menu_Option_Group_Code == Session.currentToppingCollection.currentCatalogOptionGroups.Menu_Option_Group_Code && x.Menu_Code == strButtonTag && (x.Pizza_Part == "1" || x.Pizza_Part == "2"));
                        if (index > -1)
                            curretCartItem.itemOptions.RemoveAt(index);
                    }
                }

                if (blnSelected || blnDefaultTopping)
                {
                    if (Session.currentToppingCollection.pizzaToppings.Count > 0)
                    {
                        index = -1;
                        index = Session.currentToppingCollection.pizzaToppings.FindIndex(x => x.ButtonIndex == ButtonIndex && x.ItemPart == strItemPart && x.DefaultTopping == "");
                        if (index > -1)
                            Session.currentToppingCollection.pizzaToppings.RemoveAt(index);
                    }

                    if (curretCartItem.itemOptions != null && curretCartItem.itemOptions.Count > 0)
                    {
                        index = -1;
                        index = curretCartItem.itemOptions.FindIndex(x => x.Menu_Option_Group_Code == Session.currentToppingCollection.currentCatalogOptionGroups.Menu_Option_Group_Code && x.Menu_Code == strButtonTag && x.Pizza_Part == strItemPart && x.Default_Topping == "");
                        if (index > -1)
                            curretCartItem.itemOptions.RemoveAt(index);
                    }


                    if (blnSelected)
                    {
                        if (curretCartItem.itemOptions == null)
                            curretCartItem.itemOptions = new List<ItemOption>();

                        itemOption = new ItemOption();
                        itemOption.CartId = Session.cart.cartHeader.CartId;
                        Topping ClickedTopping = Session.currentToppingCollection.currentToppings.Find(x => x.Menu_Code == strButtonTag);

                        PizzaTopping pizzaTopping = new PizzaTopping();
                        pizzaTopping.ButtonIndex = ButtonIndex;
                        pizzaTopping.ButtonColor = ButtonColor;
                        pizzaTopping.ItemPart = strItemPart;
                        pizzaTopping.DefaultTopping = "";
                        pizzaTopping.ButtonCaption = strButtonCaption;
                        pizzaTopping.KitchenDisplayOrder = Convert.ToInt32(ClickedTopping.Kitchen_Display_Order);
                        pizzaTopping.ItemPartInt = strItemPart == "W" ? 0 : Convert.ToInt32(strItemPart);
                        pizzaTopping.ToppingCode = ClickedTopping.Topping_Code;
                        Session.currentToppingCollection.pizzaToppings.Add(pizzaTopping);

                        if (ButtonColor == Session.ToppingSizeLightColor)
                            itemOption.Amount_Code = UserTypes.cstrLight_Code;
                        else if (ButtonColor == Session.ToppingSizeExtraColor)
                            itemOption.Amount_Code = UserTypes.cstrExtra_Code;
                        else if (ButtonColor == Session.ToppingSizeDoubleColor)
                            itemOption.Amount_Code = UserTypes.cstrDouble_Code;
                        else if (ButtonColor == Session.ToppingSizeTripleColor)
                            itemOption.Amount_Code = UserTypes.cstrTriple_Code;
                        else if (ButtonColor == Session.ToppingColor || ButtonColor == Color.Black || ButtonColor == Color.Gray)
                            itemOption.Amount_Code = "-";
                        else
                            itemOption.Amount_Code = "";

                        itemOption.Kitchen_Display_Order = Convert.ToInt32(ClickedTopping.Kitchen_Display_Order);
                        itemOption.Topping_group = Session.currentToppingCollection.currentCatalogOptionGroups.Topping_Group;
                        itemOption.Default_Topping = pizzaTopping.DefaultTopping;
                        itemOption.Pizza_Part = pizzaTopping.ItemPart;
                        itemOption.Description = ClickedTopping.Topping_Code;
                        itemOption.Size_Code = ClickedTopping.Size_Code;
                        itemOption.Menu_Description = strButtonCaption;
                        itemOption.Menu_Code = strButtonTag;
                        itemOption.Menu_Option_Group_Code = Session.currentToppingCollection.currentCatalogOptionGroups.Menu_Option_Group_Code;
                        itemOption.Index = pizzaTopping.ButtonIndex;
                        itemOption.Line_Number = curretCartItem.Line_Number;
                        itemOption.Location_Code = curretCartItem.Location_Code;
                        itemOption.Order_Date = curretCartItem.Order_Date;
                        itemOption.Order_Number = curretCartItem.Order_Number;
                        itemOption.Sequence = curretCartItem.Sequence;
                        itemOption.Added_By = Session.CurrentEmployee.LoginDetail.EmployeeCode;
                        //itemOption.MenuItemType = Session.cartToppings.
                        itemOption.Group_Sort_Order = 99;
                        itemOption.Default_Amount_Code = strDefaultAmount;

                        curretCartItem.itemOptions.Add(itemOption);

                    }
                }
                else
                {
                    if (Session.currentToppingCollection.pizzaToppings.Count > 0)
                    {
                        index = -1;
                        index = Session.currentToppingCollection.pizzaToppings.FindIndex(x => x.ButtonIndex == ButtonIndex && x.ItemPart == strItemPart && x.DefaultTopping == "");
                        if (index > -1)
                            Session.currentToppingCollection.pizzaToppings.RemoveAt(index);
                    }

                    if (curretCartItem.itemOptions != null && curretCartItem.itemOptions.Count > 0)
                    {
                        index = -1;
                        index = curretCartItem.itemOptions.FindIndex(x => x.Menu_Option_Group_Code == Session.currentToppingCollection.currentCatalogOptionGroups.Menu_Option_Group_Code && x.Menu_Code == strButtonTag && x.Pizza_Part == strItemPart && x.Default_Topping == "");
                        if (index > -1)
                            curretCartItem.itemOptions.RemoveAt(index);
                    }
                }

                foreach (ItemOptionGroup itemOptionGroup in curretCartItem.itemOptionGroups)
                {
                    if (itemOption != null)
                    {
                        if (itemOptionGroup.Option_Group_Code == Session.currentToppingCollection.currentCatalogOptionGroups.Menu_Option_Group_Code)
                        {
                            //TO DO Combo
                            //If pblnProcessingCombo Then
                            //   If OrderLine.Combo_Prompt_Options Then
                            //        If Val(lblSelectedValue.Caption) >= Val(lblMinValue.Caption) Then
                            //            OrderLineOptionGroup.Option_Group_Complete = True
                            //        Else
                            //            OrderLineOptionGroup.Option_Group_Complete = False
                            //        End If
                            //    Else
                            //        OrderLineOptionGroup.Option_Group_Complete = True
                            //    End If
                            //Else
                            //TO DO with intChosen corrected
                            //if (intChosen >= Session.currentToppingCollection.currentCatalogOptionGroups.Min_To_Choose)
                            itemOptionGroup.Option_Group_Complete = true;
                            //else
                            //    itemOptionGroup.Option_Group_Complete = false;
                            //End If

                            break;
                        }
                    }
                }

                curretCartItem.Topping_String = Create_Topping_Strings(Session.currentToppingCollection.pizzaToppings);
                //Session.Topping_String = curretCartItem.Topping_String;

                HandleToppings(ref curretCartItem);

                curretCartItem.Action = "M";
                cartLocal.cartItems.Add(curretCartItem);
                cartLocal.cartHeader = Session.cart.cartHeader;
                CartFunctions.UpdateCustomer(cartLocal);
                Session.cart = APILayer.Add2Cart(cartLocal);

                return true;
            }
            else
                return false;

        }

        public static List<PizzaTopping> GetPizzaToppings(List<Topping> Toppings, int SelectedLineNumber, string SpecialtyPizzaCode = "")
        {

            try
            {
                int i = 0;
                List<PizzaTopping> pizzaToppings = new List<PizzaTopping>();
                int LocalLineNumber = SelectedLineNumber;
                SelectedLineNumber = UserFunctions.ActualLineNumber(Session.cart, SelectedLineNumber);

                foreach (Topping _catalogToppings in Toppings)
                {
                    PizzaTopping pizzaTopping = new PizzaTopping();
                    pizzaTopping.ButtonIndex = i;
                    pizzaTopping.ButtonColor = _catalogToppings.Default ? Session.ToppingSizeSingleColor : Session.ToppingColor;
                    pizzaTopping.ItemPart = "W";
                    if (SpecialtyPizzaCode == "")
                        pizzaTopping.DefaultTopping = (Session.ProcessingCombo && Session.CurrentComboItem > 0) ? Session.selectedComboMenuItem.Specialty_Pizza_Code : (Session.selectedMenuItem.Specialty_Pizza_Code == "" ? "-D-" : Session.selectedMenuItem.Specialty_Pizza_Code);
                    else
                        pizzaTopping.DefaultTopping = SpecialtyPizzaCode;
                    pizzaTopping.ButtonCaption = _catalogToppings.Order_Description;
                    pizzaTopping.KitchenDisplayOrder = Convert.ToInt32(_catalogToppings.Kitchen_Display_Order);
                    pizzaTopping.ItemPartInt = 0;
                    pizzaTopping.ToppingCode = _catalogToppings.Topping_Code;
                    pizzaToppings.Add(pizzaTopping);

                    i = i + 1;
                }

                if (Session.cart != null && Session.cart.cartItems != null && Session.cart.cartItems.Count > 0 && Session.selectedMenuItemSizes != null)
                {
                    CartItem cartItem = null;
                    if (Session.ProcessingCombo && Session.CurrentComboItem > 0)
                        cartItem = Session.cart.cartItems.Find(x => x.Menu_Code == Session.selectedComboMenuItem.Menu_Code && x.Size_Code == Session.selectedComboMenuItemSizes.Size_Code && x.Line_Number == (SelectedLineNumber > 0 ? SelectedLineNumber : x.Line_Number));
                    else
                        cartItem = Session.cart.cartItems.Find(x => x.Menu_Code == Session.selectedMenuItem.Menu_Code && x.Size_Code == Session.selectedMenuItemSizes.Size_Code && x.Line_Number == (SelectedLineNumber > 0 ? SelectedLineNumber : x.Line_Number));

                    if (LocalLineNumber == -1 && cartItem != null) cartItem = null;

                    if (cartItem != null)
                    {
                        List<ItemOption> itemOptions = cartItem.itemOptions;

                        if (itemOptions != null && itemOptions.Count > 0)
                        {
                            foreach (ItemOption itemOption in itemOptions)
                            {
                                if (itemOption.Default_Topping == "" || (cartItem.Specialty_Pizza == false && itemOption.Pizza_Part != "W" && !String.IsNullOrEmpty(itemOption.Default_Topping) && itemOption.Default_Topping != cartItem.Specialty_Pizza_Code))
                                {
                                    PizzaTopping pizzaTopping = new PizzaTopping();
                                    pizzaTopping.ButtonIndex = pizzaToppings.Find(x => x.ButtonCaption == itemOption.Menu_Description).ButtonIndex; ;
                                    pizzaTopping.ButtonColor = UserFunctions.GetColorbyAmountCode(itemOption.Amount_Code);
                                    pizzaTopping.ItemPart = itemOption.Pizza_Part;
                                    pizzaTopping.DefaultTopping = itemOption.Default_Topping;
                                    pizzaTopping.ButtonCaption = itemOption.Menu_Description;
                                    pizzaTopping.KitchenDisplayOrder = itemOption.Kitchen_Display_Order;
                                    pizzaTopping.ItemPartInt = itemOption.Pizza_Part == "W" ? 0 : Convert.ToInt32(itemOption.Pizza_Part);
                                    pizzaTopping.ToppingCode = itemOption.Description;

                                    pizzaToppings.Add(pizzaTopping);
                                }
                            }
                        }
                    }
                }
                return pizzaToppings;
            }
            catch (Exception ex)
            {
                Logger.Trace("ERROR", "CartFunction-GetPizzaToppings(): " + ex.Message, ex, true);
                return new List<PizzaTopping>();
            }

        }

        public static string Create_Topping_Strings(List<PizzaTopping> pizzaToppings)
        {
            string strTopping_Code;
            int intRow;
            string strSpecialty_Code;
            int bytCount;
            string strPizzaPart;
            string strWhole;
            string str1stHalf;
            string str2ndHalf;
            string strCurrentToppingString;

            strWhole = "";
            str1stHalf = "";
            str2ndHalf = "";


            if (pizzaToppings.Count > 0)
                pizzaToppings = pizzaToppings.OrderBy(x => x.ItemPart).ToList().OrderBy(x => x.KitchenDisplayOrder).ToList();

            for (bytCount = 0; bytCount <= 2; bytCount++)
            {
                if (bytCount == 0)
                    strPizzaPart = "W";
                else
                    strPizzaPart = Convert.ToString(bytCount);


                strSpecialty_Code = "";

                foreach (PizzaTopping pizzaTopping in pizzaToppings)
                {
                    if (pizzaTopping.ItemPart == strPizzaPart)
                    {

                        if (strSpecialty_Code == "" && pizzaTopping.DefaultTopping != "" && pizzaTopping.DefaultTopping != "-D-")
                        {
                            strSpecialty_Code = pizzaTopping.DefaultTopping;

                            if (strPizzaPart == "W")
                                strWhole = "(" + strSpecialty_Code + ")" + strWhole;
                            else if (strPizzaPart == "1")
                                str1stHalf = "(" + strSpecialty_Code + ")" + str1stHalf;
                            else if (strPizzaPart == "2")
                                str2ndHalf = "(" + strSpecialty_Code + ")" + str2ndHalf;

                        }
                        else if (pizzaTopping.DefaultTopping == "")
                        {
                            if (pizzaTopping.ButtonColor == Session.ToppingSizeLightColor)
                            {
                                strTopping_Code = UserTypes.cstrLight_Code + pizzaTopping.ToppingCode;
                                strTopping_Code = strTopping_Code + new string(' ', strTopping_Code.Length);
                            }
                            else if (pizzaTopping.ButtonColor == Session.ToppingSizeSingleColor)
                            {
                                strTopping_Code = "" + pizzaTopping.ToppingCode;
                                strTopping_Code = strTopping_Code + new string(' ', strTopping_Code.Length);
                            }
                            else if (pizzaTopping.ButtonColor == Session.ToppingSizeExtraColor)
                            {
                                strTopping_Code = UserTypes.cstrExtra_Code + pizzaTopping.ToppingCode;
                                strTopping_Code = strTopping_Code + new string(' ', strTopping_Code.Length);
                            }
                            else if (pizzaTopping.ButtonColor == Session.ToppingSizeDoubleColor)
                            {
                                strTopping_Code = UserTypes.cstrDouble_Code + pizzaTopping.ToppingCode;
                                strTopping_Code = strTopping_Code + new string(' ', strTopping_Code.Length);
                            }
                            else if (pizzaTopping.ButtonColor == Session.ToppingSizeTripleColor)
                            {
                                strTopping_Code = UserTypes.cstrTriple_Code + pizzaTopping.ToppingCode;
                                strTopping_Code = strTopping_Code + new string(' ', strTopping_Code.Length);
                            }
                            else
                            {
                                strTopping_Code = "-" + pizzaTopping.ToppingCode;
                                strTopping_Code = strTopping_Code + new string(' ', strTopping_Code.Length);
                            }

                            if (strPizzaPart == "W")
                                strWhole = strWhole + strTopping_Code;
                            else if (strPizzaPart == "1")
                                str1stHalf = str1stHalf + strTopping_Code;
                            else if (strPizzaPart == "2")
                                str2ndHalf = str2ndHalf + strTopping_Code;
                        }
                    }
                }
            }

            strCurrentToppingString = strWhole.Replace(" ", "") + "/" + str1stHalf.Replace(" ", "") + "/" + str2ndHalf.Replace(" ", "");

            if (strCurrentToppingString.Trim() == "//")
                strCurrentToppingString = "";

            if (strCurrentToppingString.Length > 0)
            {
                do
                {
                    strCurrentToppingString = strCurrentToppingString.Substring(0, strCurrentToppingString.Length - 1);
                }
                while (strCurrentToppingString.EndsWith("/"));
            }

            return strCurrentToppingString;

        }

        public static int GetCurrentToppingIndex(DataGridView dataGridView)
        {
            int index = -1;

            for (int i = 0; i <= dataGridView.Rows.Count - 1; i++)
            {
                if (Convert.ToString(dataGridView.Rows[i].Cells["Menu_Code"].Value) == Session.selectedMenuItem.Menu_Code && Convert.ToString(dataGridView.Rows[i].Cells["Size_Code"].Value) == Session.selectedMenuItemSizes.Size_Code && Convert.ToString(dataGridView.Rows[i].Cells["LineType"].Value) == "O")
                    index = i;
            }

            return index;
        }

        public static void FillCartToCustomer()
        {
            //CheckCart();

            if (Session.cart != null)
            {
                //Session.cart.Customer.CartId = String.IsNullOrEmpty(Session.cart.cartHeader.CartId) ? "" : Session.cart.cartHeader.CartId;
                Session.cart.cartHeader.CartId = String.IsNullOrEmpty(Session.cart.Customer.CartId) ? "" : Session.cart.Customer.CartId;
                //Session.cart.Customer.Location_Code = String.IsNullOrEmpty(Session.cart.Customer.Location_Code) ? "" : Session.cart.Customer.Location_Code;
                Session.cart.Customer.Location_Code = String.IsNullOrEmpty(Session.cart.Customer.Location_Code) ? "" : Session.cart.Customer.Location_Code;
                //Session.cart.Customer.Phone_Number = String.IsNullOrEmpty(Session.cart.Customer.Phone_Number) ? "" : Session.cart.Customer.Phone_Number;
                Session.cart.Customer.Phone_Number = String.IsNullOrEmpty(Session.cart.Customer.Phone_Number) ? "" : Session.cart.Customer.Phone_Number;

                //Session.cart.Customer.Phone_Ext = String.IsNullOrEmpty(Session.cart.Customer.Phone_Ext) ? "" : Session.cart.Customer.Phone_Ext;
                Session.cart.Customer.Phone_Ext = String.IsNullOrEmpty(Session.cart.Customer.Phone_Ext) ? "" : Session.cart.Customer.Phone_Ext;

                //Session.cart.Customer.Customer_Code = Convert.ToInt32(Session.cart.Customer.Customer_Code);
                Session.cart.Customer.Customer_Code = Session.cart.Customer.Customer_Code;

                //Session.cart.Customer.Name = String.IsNullOrEmpty(Session.cart.Customer.Name) ? "" : Session.cart.Customer.Name;
                Session.cart.Customer.Name = String.IsNullOrEmpty(Session.cart.Customer.Name) ? "" : Session.cart.Customer.Name;

                //Session.cart.Customer.Company_Name = String.IsNullOrEmpty(Session.cart.Customer.Company_Name) ? "" : Session.cart.Customer.Company_Name;
                Session.cart.Customer.Company_Name = String.IsNullOrEmpty(Session.cart.Customer.Company_Name) ? "" : Session.cart.Customer.Company_Name;

                //Session.cart.Customer.Street_Number = String.IsNullOrEmpty(Session.cart.Customer.Street_Number) ? "" : Session.cart.Customer.Street_Number;
                Session.cart.Customer.Street_Number = String.IsNullOrEmpty(Session.cart.Customer.Street_Number) ? "" : Session.cart.Customer.Street_Number;

                //Session.cart.Customer.Street_Code = Session.cart.Customer.Street_Code == 0 ? 1 : Session.cart.Customer.Street_Code;
                Session.cart.Customer.Street_Code = Session.cart.Customer.Street_Code == 0 ? 1 : Session.cart.Customer.Street_Code;

                //Session.cart.Customer.Cross_Street_Code = Session.cart.Customer.Cross_Street_Code;
                Session.cart.Customer.Cross_Street_Code = Session.cart.Customer.Cross_Street_Code;

                //Session.cart.Customer.Suite = String.IsNullOrEmpty(Session.cart.Customer.Suite) ? "" : Session.cart.Customer.Suite;
                Session.cart.Customer.Suite = String.IsNullOrEmpty(Session.cart.Customer.Suite) ? "" : Session.cart.Customer.Suite;

                //Session.cart.Customer.Address_Line_2 = String.IsNullOrEmpty(Session.cart.Customer.Address_Line_2) ? "" : Session.cart.Customer.Address_Line_2;
                Session.cart.Customer.Address_Line_2 = String.IsNullOrEmpty(Session.cart.Customer.Address_Line_2) ? "" : Session.cart.Customer.Address_Line_2;

                //Session.cart.Customer.Address_Line_3 = String.IsNullOrEmpty(Session.cart.Customer.Address_Line_3) ? "" : Session.cart.Customer.Address_Line_3;
                Session.cart.Customer.Address_Line_3 = String.IsNullOrEmpty(Session.cart.Customer.Address_Line_3) ? "" : Session.cart.Customer.Address_Line_3;

                //Session.cart.Customer.Address_Line_4 = String.IsNullOrEmpty(Session.cart.Customer.Address_Line_4) ? "" : Session.cart.Customer.Address_Line_4;
                Session.cart.Customer.Address_Line_4 = String.IsNullOrEmpty(Session.cart.Customer.Address_Line_4) ? "" : Session.cart.Customer.Address_Line_4;

                //Session.cart.Customer.Mailing_Address = "";
                //Session.cart.Customer.Postal_Code = String.IsNullOrEmpty(Session.cart.Customer.Postal_Code) ? "" : Session.cart.Customer.Postal_Code;
                Session.cart.Customer.Postal_Code = String.IsNullOrEmpty(Session.cart.Customer.Postal_Code) ? "" : Session.cart.Customer.Postal_Code;

                //Session.cart.Customer.Plus4 = "";
                //Session.cart.Customer.Cart = "";
                //Session.cart.Customer.Delivery_Point_Code = "";
                //Session.cart.Customer.Walk_Sequence = "";
                //Session.cart.Customer.Address_Type = String.IsNullOrEmpty(Session.cart.Customer.Address_Type) ? "" : Session.cart.Customer.Address_Type;
                Session.cart.Customer.Address_Type = String.IsNullOrEmpty(Session.cart.Customer.Address_Type) ? "" : Session.cart.Customer.Address_Type;

                //Session.cart.Customer.Set_Discount = 0;
                //Session.cart.Customer.Tax_Exempt1 = Session.cart.Customer.Tax_Exempt1;
                Session.cart.Customer.Tax_Exempt1 = Session.cart.Customer.Tax_Exempt1;

                //Session.cart.Customer.Tax_ID1 = String.IsNullOrEmpty(Session.cart.Customer.Tax_ID1) ? "" : Session.cart.Customer.Tax_ID1;
                Session.cart.Customer.Tax_ID1 = String.IsNullOrEmpty(Session.cart.Customer.Tax_ID1) ? "" : Session.cart.Customer.Tax_ID1;

                //Session.cart.Customer.Tax_Exempt2 = Session.cart.Customer.Tax_Exempt2;
                Session.cart.Customer.Tax_Exempt2 = Session.cart.Customer.Tax_Exempt2;

                //Session.cart.Customer.Tax_ID2 = String.IsNullOrEmpty(Session.cart.Customer.Tax_ID2) ? "" : Session.cart.Customer.Tax_ID2;
                Session.cart.Customer.Tax_ID2 = String.IsNullOrEmpty(Session.cart.Customer.Tax_ID2) ? "" : Session.cart.Customer.Tax_ID2;

                //Session.cart.Customer.Tax_Exempt3 = Session.cart.Customer.Tax_Exempt3;
                Session.cart.Customer.Tax_Exempt3 = Session.cart.Customer.Tax_Exempt3;

                //Session.cart.Customer.Tax_ID3 = String.IsNullOrEmpty(Session.cart.Customer.Tax_ID3) ? "" : Session.cart.Customer.Tax_ID3;
                Session.cart.Customer.Tax_ID3 = String.IsNullOrEmpty(Session.cart.Customer.Tax_ID3) ? "" : Session.cart.Customer.Tax_ID3;

                //Session.cart.Customer.Tax_Exempt4 = Session.cart.Customer.Tax_Exempt4;
                Session.cart.Customer.Tax_Exempt4 = Session.cart.Customer.Tax_Exempt4;

                //Session.cart.Customer.Tax_ID4 = String.IsNullOrEmpty(Session.cart.Customer.Tax_ID4) ? "" : Session.cart.Customer.Tax_ID4;
                Session.cart.Customer.Tax_ID4 = String.IsNullOrEmpty(Session.cart.Customer.Tax_ID4) ? "" : Session.cart.Customer.Tax_ID4;

                //Session.cart.Customer.Accept_Checks = false;
                //Session.cart.Customer.Accept_Credit_Cards = false;
                //Session.cart.Customer.Accept_Gift_Cards = false;
                //Session.cart.Customer.Accept_Charge_Account = false;
                //Session.cart.Customer.Accept_Cash = false;
                //Session.cart.Customer.Finance_Charge_Rate = Session.cart.Customer.Finance_Charge_Rate;
                Session.cart.Customer.Finance_Charge_Rate = Session.cart.Customer.Finance_Charge_Rate;

                //Session.cart.Customer.Credit_Limit = 0;
                //Session.cart.Customer.Credit = 0;
                //Session.cart.Customer.Payment_Terms = 0;
                //Session.cart.Customer.First_Order_Date = Session.cart.Customer.First_Order_Date == Convert.ToDateTime("01-01-0001") ? new DateTime(1899, 12, 30) : Session.cart.Customer.First_Order_Date;
                Session.cart.Customer.First_Order_Date = Session.cart.Customer.First_Order_Date == Convert.ToDateTime("01-01-0001") ? new DateTime(1899, 12, 30) : Session.cart.Customer.First_Order_Date;

                //Session.cart.Customer.Last_Order_Date = Session.cart.Customer.Last_Order_Date == Convert.ToDateTime("01-01-0001") ? new DateTime(1899, 12, 30) : Session.cart.Customer.Last_Order_Date;
                Session.cart.Customer.Last_Order_Date = Session.cart.Customer.Last_Order_Date == Convert.ToDateTime("01-01-0001") ? new DateTime(1899, 12, 30) : Session.cart.Customer.Last_Order_Date;

                //Session.cart.Customer.Added_By = String.IsNullOrEmpty(Session.cart.Customer.Added_By) ? "" : Session.cart.Customer.Added_By;
                Session.cart.Customer.Added_By = String.IsNullOrEmpty(Session.cart.Customer.Added_By) ? "" : Session.cart.Customer.Added_By;

                //Session.cart.Customer.Comments = String.IsNullOrEmpty(Session.cart.Customer.Comments) ? "" : Session.cart.Customer.Comments;
                Session.cart.Customer.Comments = String.IsNullOrEmpty(Session.cart.Customer.Comments) ? "" : Session.cart.Customer.Comments;

                //Session.cart.Customer.DriverComments = String.IsNullOrEmpty(Session.cart.Customer.DriverComments) ? "" : Session.cart.Customer.DriverComments;
                Session.cart.Customer.DriverComments = String.IsNullOrEmpty(Session.cart.Customer.DriverComments) ? "" : Session.cart.Customer.DriverComments;

                //Session.cart.Customer.DriverCommentsAddUpdateDelete = false;
                //Session.cart.Customer.Manager_Notes = String.IsNullOrEmpty(Session.cart.Customer.Manager_Notes) ? "" : Session.cart.Customer.Manager_Notes;
                Session.cart.Customer.Manager_Notes = String.IsNullOrEmpty(Session.cart.Customer.Manager_Notes) ? "" : Session.cart.Customer.Manager_Notes;

                //Session.cart.Customer.Customer_City_Code = 0;
                //Session.cart.Customer.Customer_Street_Name = "";
                //Session.cart.Customer.HotelorCollege = false;
                //Session.cart.Customer.City = String.IsNullOrEmpty(Session.cart.Customer.City) ? "" : Session.cart.Customer.City;
                Session.cart.Customer.City = String.IsNullOrEmpty(Session.cart.Customer.City) ? "" : Session.cart.Customer.City;

                //Session.cart.Customer.Region = String.IsNullOrEmpty(Session.cart.Customer.Region) ? "" : Session.cart.Customer.Region;
                Session.cart.Customer.Region = String.IsNullOrEmpty(Session.cart.Customer.Region) ? "" : Session.cart.Customer.Region;

                //Session.cart.Customer.TaxRate1 = 0;
                //Session.cart.Customer.TaxRate2 = 0;
                //Session.cart.Customer.Cross_Street = "";
                //Session.cart.Customer.NoteAddUpdateDelete = false;
                //Session.cart.Customer.gstin_number_number = String.IsNullOrEmpty(Session.cart.Customer.gstin_number) ? "" : Session.cart.Customer.gstin_number;
                Session.cart.Customer.gstin_number = String.IsNullOrEmpty(Session.cart.Customer.gstin_number) ? "" : Session.cart.Customer.gstin_number;

                //Session.cart.Customer.date_of_birth = Session.cart.Customer.date_of_birth == Convert.ToDateTime("01-01-0001") ? new DateTime(1899, 12, 30) : Session.cart.Customer.date_of_birth;
                Session.cart.Customer.date_of_birth = Session.cart.Customer.date_of_birth == Convert.ToDateTime("01-01-0001") ? new DateTime(1899, 12, 30) : Session.cart.Customer.date_of_birth;

                //Session.cart.Customer.anniversary_date = Session.cart.Customer.anniversary_date == Convert.ToDateTime("01-01-0001") ? new DateTime(1899, 12, 30) : Session.cart.Customer.anniversary_date;
                Session.cart.Customer.anniversary_date = Session.cart.Customer.anniversary_date == Convert.ToDateTime("01-01-0001") ? new DateTime(1899, 12, 30) : Session.cart.Customer.anniversary_date;

                //Session.cart.Customer.Action = "M";

                //Profile
                GetCustomerProfileRequest getCustomerProfileRequest = new GetCustomerProfileRequest();
                getCustomerProfileRequest.CustomerCode = Session.cart.Customer.Customer_Code.ToString();//Session.CurrentEmployee.LoginDetail.
                getCustomerProfileRequest.LocationCode = Session._LocationCode;// Session.CurrentEmployee.LoginDetail.

                GetCustomerProfileResponse getCustomerProfileResponse = APILayer.GetCustomerProfile(APILayer.CallType.POST, getCustomerProfileRequest);

                if (getCustomerProfileResponse != null && getCustomerProfileResponse.Result != null && getCustomerProfileResponse.Result.CustomerProfile != null)
                {
                    Session.CustomerProfileCollection = new GetCustomerProfile();
                    //Session.cart.Customer.First_Order_Date = getCustomerProfileResponse.Result.CustomerProfile.FirstOrderDate;
                    //Session.cart.Customer.Last_Order_Date = getCustomerProfileResponse.Result.CustomerProfile.LastOrderDate;
                    Session.CustomerProfileCollection.CreditLimit = getCustomerProfileResponse.Result.CustomerProfile.CreditLimit;
                    Session.CustomerProfileCollection.ARBalance = getCustomerProfileResponse.Result.CustomerProfile.ARBalance;
                    Session.CustomerProfileCollection.InStoreCredit = getCustomerProfileResponse.Result.CustomerProfile.InStoreCredit;
                    Session.CustomerProfileCollection.LastVoidAmount = getCustomerProfileResponse.Result.CustomerProfile.LastVoidAmount;
                    Session.CustomerProfileCollection.LastVoidCount = getCustomerProfileResponse.Result.CustomerProfile.LastVoidCount;
                    Session.CustomerProfileCollection.LastBadAmount = getCustomerProfileResponse.Result.CustomerProfile.LastBadAmount;
                    Session.CustomerProfileCollection.LastBadCount = getCustomerProfileResponse.Result.CustomerProfile.LastBadCount;
                    Session.CustomerProfileCollection.LastLateAmount = getCustomerProfileResponse.Result.CustomerProfile.LastLateAmount;
                    Session.CustomerProfileCollection.LastLateCount = getCustomerProfileResponse.Result.CustomerProfile.LastLateCount;
                    Session.CustomerProfileCollection.LastOrdersAmount = getCustomerProfileResponse.Result.CustomerProfile.LastOrdersAmount;
                    Session.CustomerProfileCollection.LastOrdersCount = getCustomerProfileResponse.Result.CustomerProfile.LastOrdersCount;
                    Session.CustomerProfileCollection.LastAverage = getCustomerProfileResponse.Result.CustomerProfile.LastAverage;
                    Session.CustomerProfileCollection.YTDVoidAmount = getCustomerProfileResponse.Result.CustomerProfile.YTDVoidAmount;
                    Session.CustomerProfileCollection.YTDVoidCount = getCustomerProfileResponse.Result.CustomerProfile.YTDVoidCount;
                    Session.CustomerProfileCollection.YTDBadAmount = getCustomerProfileResponse.Result.CustomerProfile.YTDBadAmount;
                    Session.CustomerProfileCollection.YTDBadCount = getCustomerProfileResponse.Result.CustomerProfile.YTDBadCount;
                    Session.CustomerProfileCollection.YTDLateAmount = getCustomerProfileResponse.Result.CustomerProfile.YTDLateAmount;
                    Session.CustomerProfileCollection.YTDLateCount = getCustomerProfileResponse.Result.CustomerProfile.YTDLateCount;
                    Session.CustomerProfileCollection.YTDOrdersAmount = getCustomerProfileResponse.Result.CustomerProfile.YTDOrdersAmount;
                    Session.CustomerProfileCollection.YTDAverage = getCustomerProfileResponse.Result.CustomerProfile.YTDAverage;
                    Session.CustomerProfileCollection.YTDOrdersCount = getCustomerProfileResponse.Result.CustomerProfile.YTDOrdersCount;
                    Session.CustomerProfileCollection.OrdersPerMonth = getCustomerProfileResponse.Result.CustomerProfile.OrdersPerMonth;
                    Session.CustomerProfileCollection.Elapsed = getCustomerProfileResponse.Result.CustomerProfile.Elapsed;
                    Session.CustomerProfileCollection.LastOrderDate = getCustomerProfileResponse.Result.CustomerProfile.LastOrderDate;
                    Session.CustomerProfileCollection.FirstOrderDate = getCustomerProfileResponse.Result.CustomerProfile.FirstOrderDate;
                }
            }
        }

        public static void PopulateDefaultToppings(ref Cart cart, int SelectedLineNumber, string SelectedComboMenuItem_MenuCode, string SelectedMenuItem_MenuCode,
                                                    string SelectedComboMenuItem_SpecialtyPizzaCode, string SelectedMenuItem_SpecialtyPizzaCode)
        {
            int i = 0;
            bool IsSpecialtyPizza = false;

            CatalogMenuItems menuItem = Session.AllCatalogMenuItems.Find(x => x.Menu_Code == ((Session.ProcessingCombo && Session.CurrentComboItem > 0) ? SelectedComboMenuItem_MenuCode : SelectedMenuItem_MenuCode));
            if (menuItem != null)
                IsSpecialtyPizza = Convert.ToBoolean(menuItem.Specialty_Pizza);

            SelectedLineNumber = UserFunctions.ActualLineNumber(cart, SelectedLineNumber);

            if (Session.currentToppingCollection != null && Session.currentToppingCollection.currentToppings != null)
            {
                foreach (Topping _Toppings in Session.currentToppingCollection.currentToppings)
                {
                    if (_Toppings.Default)
                    {
                        Color ButtonColor = _Toppings.Default ? Session.ToppingSizeSingleColor : Session.ToppingColor;

                        CartItem curretCartItem = null;
                        if (Session.ProcessingCombo && Session.CurrentComboItem > 0)
                            curretCartItem = cart.cartItems.Find(x => x.Menu_Code == SelectedComboMenuItem_MenuCode && x.Line_Number == (SelectedLineNumber > 0 ? SelectedLineNumber : x.Line_Number));
                        else
                            curretCartItem = cart.cartItems.Find(x => x.Menu_Code == SelectedMenuItem_MenuCode && x.Line_Number == (SelectedLineNumber > 0 ? SelectedLineNumber : x.Line_Number));



                        //if (IsSpecialtyPizza)
                        //{

                        if (curretCartItem.itemOptions != null && curretCartItem.itemOptions.Count > 0)
                        {
                            int index = -1;
                            index = curretCartItem.itemOptions.FindIndex(x => x.Menu_Option_Group_Code == Session.currentToppingCollection.currentCatalogOptionGroups.Menu_Option_Group_Code && x.Menu_Code == _Toppings.Menu_Code && x.Pizza_Part == "W" && x.Default_Topping == SelectedMenuItem_SpecialtyPizzaCode);
                            if (index > -1)
                                curretCartItem.itemOptions.RemoveAt(index);
                        }

                        if (curretCartItem.itemOptions == null)
                            curretCartItem.itemOptions = new List<ItemOption>();

                        ItemOption itemOption = new ItemOption();
                        itemOption.CartId = cart.cartHeader.CartId;

                        if (ButtonColor == Session.ToppingSizeLightColor)
                            itemOption.Amount_Code = UserTypes.cstrLight_Code;
                        else if (ButtonColor == Session.ToppingSizeExtraColor)
                            itemOption.Amount_Code = UserTypes.cstrExtra_Code;
                        else if (ButtonColor == Session.ToppingSizeDoubleColor)
                            itemOption.Amount_Code = UserTypes.cstrDouble_Code;
                        else if (ButtonColor == Session.ToppingSizeTripleColor)
                            itemOption.Amount_Code = UserTypes.cstrTriple_Code;
                        else if (ButtonColor == Session.ToppingColor || ButtonColor == Color.Black)
                            itemOption.Amount_Code = "-";
                        else
                            itemOption.Amount_Code = "";

                        itemOption.Kitchen_Display_Order = Convert.ToInt32(_Toppings.Kitchen_Display_Order);
                        itemOption.Topping_group = Session.currentToppingCollection.currentCatalogOptionGroups.Topping_Group;
                        itemOption.Default_Topping = (String.IsNullOrEmpty(_Toppings.Default_Item) ? ((Session.ProcessingCombo && Session.CurrentComboItem > 0) ? SelectedComboMenuItem_SpecialtyPizzaCode : SelectedMenuItem_SpecialtyPizzaCode) : _Toppings.Default_Item);
                        itemOption.Pizza_Part = "W";
                        itemOption.Description = _Toppings.Topping_Code;
                        itemOption.Size_Code = _Toppings.Size_Code;
                        itemOption.Menu_Description = _Toppings.Order_Description;
                        itemOption.Menu_Code = _Toppings.Menu_Code;
                        itemOption.Menu_Option_Group_Code = Session.currentToppingCollection.currentCatalogOptionGroups.Menu_Option_Group_Code;
                        itemOption.Index = i;
                        itemOption.Line_Number = curretCartItem.Line_Number;
                        itemOption.Location_Code = curretCartItem.Location_Code;
                        itemOption.Order_Date = curretCartItem.Order_Date;
                        itemOption.Order_Number = curretCartItem.Order_Number;
                        itemOption.Sequence = curretCartItem.Sequence;
                        itemOption.Added_By = Session.CurrentEmployee.LoginDetail.EmployeeCode;
                        itemOption.Group_Sort_Order = 99;
                        itemOption.Default_Amount_Code = _Toppings.Amount_Code;

                        curretCartItem.itemOptions.Add(itemOption);
                        //}
                    }

                    i = i + 1;
                }
            }
        }

        private static void HandleToppings(ref CartItem cartItem)
        {
            try
            {
                bool blnFirstSpecialtyToppingH1;
                bool blnFirstSpecialtyToppingH2;
                bool blnFirstSpecialtyToppingWhole;
                string strPart;
                string strWholeDescriptionString = "";
                string strFirstHalfDescriptionString = "";
                string strSecondHalfDescriptionString = "";
                string strWholeCodeString = "";
                string strFirstHalfCodeString = "";
                string strSecondHalfCodeString = "";
                string strWholeTopCodeString = "";
                string strFirstTopCodeString = "";
                string strSecondTopCodeString = "";

                cartItem.Topping_Codes = "";
                cartItem.Topping_Descriptions = "";

                string currentCartItem = cartItem.Menu_Code;
                string specialtyPizzaDescriptionWhole = string.Empty;
                string specialtyPizzaDescription1stHalf = string.Empty;
                string specialtyPizzaDescription2ndHalf = string.Empty;

                if (cartItem != null && cartItem.itemSpecialtyPizzas != null && cartItem.itemSpecialtyPizzas.Count > 0)
                {
                    ItemSpecialtyPizza itemSpecialtyPizza1stHalf = cartItem.itemSpecialtyPizzas.Find(x => x.Pizza_Part == "1");
                    if (itemSpecialtyPizza1stHalf != null)
                        specialtyPizzaDescription1stHalf = Session.AllCatalogMenuItems.Find(x => x.Menu_Code == itemSpecialtyPizza1stHalf.Menu_Code).Description;

                    ItemSpecialtyPizza itemSpecialtyPizza2ndHalf = cartItem.itemSpecialtyPizzas.Find(x => x.Pizza_Part == "2");
                    if (itemSpecialtyPizza2ndHalf != null)
                        specialtyPizzaDescription2ndHalf = Session.AllCatalogMenuItems.Find(x => x.Menu_Code == itemSpecialtyPizza2ndHalf.Menu_Code).Description;
                }

                CatalogMenuItems catalogMenu = Session.AllCatalogMenuItems.Find(x => x.Menu_Code == currentCartItem);
                if (catalogMenu != null)
                {
                    if (catalogMenu.Specialty_Pizza == true)
                        specialtyPizzaDescriptionWhole = catalogMenu.Description;
                    else
                        specialtyPizzaDescriptionWhole = "";
                }

                if (cartItem.itemAttributes != null && cartItem.itemAttributes.Count > 0)
                {
                    foreach (ItemAttribute itemAttribute in cartItem.itemAttributes)
                    {
                        strWholeDescriptionString = strWholeDescriptionString + itemAttribute.Attribute_Description + ",";

                        strWholeCodeString = strWholeCodeString + itemAttribute.Attribute_Code + ",";
                    }
                }


                if (cartItem.itemOptions != null && cartItem.itemOptions.Count > 0)
                {
                    blnFirstSpecialtyToppingH1 = true;
                    blnFirstSpecialtyToppingH2 = true;
                    blnFirstSpecialtyToppingWhole = true;

                    List<ItemOption> itemOptionsTemp = cartItem.itemOptions = cartItem.itemOptions.OrderBy(x => x.Pizza_Part).ToList().OrderBy(x => x.Group_Sort_Order).ToList().OrderBy(x => x.Kitchen_Display_Order).ToList();

                    foreach (ItemOption itemOptionTemp in itemOptionsTemp)
                    {
                        if (itemOptionTemp.Default_Topping != "-D-") //'Is it default?
                        {
                            if (itemOptionTemp.Pizza_Part == "1" || itemOptionTemp.Pizza_Part == "2") //'Is it on half?
                                strPart = "[H" + itemOptionTemp.Pizza_Part + "]";
                            else
                                strPart = "";


                            if (itemOptionTemp.Default_Topping != "")
                            {
                                if (itemOptionTemp.Pizza_Part == "1" && blnFirstSpecialtyToppingH1 || itemOptionTemp.Pizza_Part == "2" && blnFirstSpecialtyToppingH2 || itemOptionTemp.Pizza_Part == "W" && blnFirstSpecialtyToppingWhole)
                                {
                                    switch (itemOptionTemp.Pizza_Part)
                                    {
                                        case "1":
                                            strFirstHalfDescriptionString = strPart + " (" + specialtyPizzaDescription1stHalf + ")," + strFirstHalfDescriptionString;
                                            break;
                                        case "2":
                                            strSecondHalfDescriptionString = strPart + " (" + specialtyPizzaDescription2ndHalf + ")," + strSecondHalfDescriptionString;
                                            break;
                                        case "W":
                                            strWholeDescriptionString = strPart + " (" + specialtyPizzaDescriptionWhole + ")," + strWholeDescriptionString;
                                            break;
                                    }
                                }
                            }
                            else
                            {
                                switch (itemOptionTemp.Pizza_Part)
                                {
                                    case "1":
                                        switch (itemOptionTemp.Amount_Code)
                                        {
                                            case "-":
                                                strFirstHalfDescriptionString = strFirstHalfDescriptionString + strPart + "--" + itemOptionTemp.Menu_Description + ",";
                                                break;
                                            case UserTypes.cstrLight_Code:
                                                strFirstHalfDescriptionString = strFirstHalfDescriptionString + (strPart + " " + APILayer.GetCatalogText(LanguageConstant.cintLight) + " " + itemOptionTemp.Menu_Description + ",").TrimStart();
                                                break;
                                            case "": //' single
                                                strFirstHalfDescriptionString = strFirstHalfDescriptionString + (strPart + " " + itemOptionTemp.Menu_Description + ",").TrimStart();
                                                break;
                                            case UserTypes.cstrExtra_Code:
                                                strFirstHalfDescriptionString = strFirstHalfDescriptionString + (strPart + " " + APILayer.GetCatalogText(LanguageConstant.cintExtra) + " " + itemOptionTemp.Menu_Description + ",").TrimStart();
                                                break;
                                            case UserTypes.cstrDouble_Code:
                                                strFirstHalfDescriptionString = strFirstHalfDescriptionString + (strPart + " " + APILayer.GetCatalogText(LanguageConstant.cintDouble) + " " + itemOptionTemp.Menu_Description + ",").TrimStart();
                                                break;
                                            case UserTypes.cstrTriple_Code:
                                                strFirstHalfDescriptionString = strFirstHalfDescriptionString + (strPart + " " + APILayer.GetCatalogText(LanguageConstant.cintTriple) + " " + itemOptionTemp.Menu_Description + ",").TrimStart();
                                                break;
                                        }
                                        break;
                                    case "2":
                                        switch (itemOptionTemp.Amount_Code)
                                        {
                                            case "-":
                                                strSecondHalfDescriptionString = strSecondHalfDescriptionString + strPart + "--" + itemOptionTemp.Menu_Description + ",";
                                                break;
                                            case UserTypes.cstrLight_Code:
                                                strSecondHalfDescriptionString = strSecondHalfDescriptionString + (strPart + " " + APILayer.GetCatalogText(LanguageConstant.cintLight) + " " + itemOptionTemp.Menu_Description + ",").TrimStart();
                                                break;
                                            case "": //' single
                                                strSecondHalfDescriptionString = strSecondHalfDescriptionString + (strPart + " " + itemOptionTemp.Menu_Description + ",").TrimStart();
                                                break;
                                            case UserTypes.cstrExtra_Code:
                                                strSecondHalfDescriptionString = strSecondHalfDescriptionString + (strPart + " " + APILayer.GetCatalogText(LanguageConstant.cintExtra) + " " + itemOptionTemp.Menu_Description + ",").TrimStart();
                                                break;
                                            case UserTypes.cstrDouble_Code:
                                                strSecondHalfDescriptionString = strSecondHalfDescriptionString + (strPart + " " + APILayer.GetCatalogText(LanguageConstant.cintDouble) + " " + itemOptionTemp.Menu_Description + ",").TrimStart();
                                                break;
                                            case UserTypes.cstrTriple_Code:
                                                strSecondHalfDescriptionString = strSecondHalfDescriptionString + (strPart + " " + APILayer.GetCatalogText(LanguageConstant.cintTriple) + " " + itemOptionTemp.Menu_Description + ",").TrimStart();
                                                break;
                                        }
                                        break;
                                    case "W":
                                        switch (itemOptionTemp.Amount_Code)
                                        {
                                            case "-":
                                                strWholeDescriptionString = strWholeDescriptionString + strPart + "--" + itemOptionTemp.Menu_Description + ",";
                                                break;
                                            case UserTypes.cstrLight_Code:
                                                strWholeDescriptionString = strWholeDescriptionString + (strPart + " " + APILayer.GetCatalogText(LanguageConstant.cintLight) + " " + itemOptionTemp.Menu_Description + ",").TrimStart();
                                                break;
                                            case "": //' single
                                                strWholeDescriptionString = strWholeDescriptionString + (strPart + " " + itemOptionTemp.Menu_Description + ",").TrimStart();
                                                break;
                                            case UserTypes.cstrExtra_Code:
                                                strWholeDescriptionString = strWholeDescriptionString + (strPart + " " + APILayer.GetCatalogText(LanguageConstant.cintExtra) + " " + itemOptionTemp.Menu_Description + ",").TrimStart();
                                                break;
                                            case UserTypes.cstrDouble_Code:
                                                strWholeDescriptionString = strWholeDescriptionString + (strPart + " " + APILayer.GetCatalogText(LanguageConstant.cintDouble) + " " + itemOptionTemp.Menu_Description + ",").TrimStart();
                                                break;
                                            case UserTypes.cstrTriple_Code:
                                                strWholeDescriptionString = strWholeDescriptionString + (strPart + " " + APILayer.GetCatalogText(LanguageConstant.cintTriple) + " " + itemOptionTemp.Menu_Description + ",").TrimStart();
                                                break;
                                        }
                                        break;
                                }
                            }

                        }


                        if (itemOptionTemp.Default_Topping == "")
                        {
                            if (!cartItem.Pizza || !itemOptionTemp.Topping_group)
                            {
                                switch (itemOptionTemp.Pizza_Part)
                                {
                                    case "1":
                                        strFirstHalfCodeString = strFirstHalfCodeString + itemOptionTemp.Amount_Code + itemOptionTemp.Menu_Description + ",";
                                        if (itemOptionTemp.Topping_group)
                                            strFirstTopCodeString = strFirstTopCodeString + itemOptionTemp.Amount_Code + itemOptionTemp.Menu_Description + ",";
                                        break;
                                    case "2":
                                        strSecondHalfCodeString = strSecondHalfCodeString + itemOptionTemp.Amount_Code + itemOptionTemp.Menu_Description + ",";
                                        if (itemOptionTemp.Topping_group)
                                            strSecondTopCodeString = strSecondTopCodeString + itemOptionTemp.Amount_Code + itemOptionTemp.Menu_Description + ",";
                                        break;
                                    case "W":
                                        strWholeCodeString = strWholeCodeString + itemOptionTemp.Amount_Code + itemOptionTemp.Menu_Description + ",";
                                        if (itemOptionTemp.Topping_group)
                                            strWholeTopCodeString = strWholeTopCodeString + itemOptionTemp.Amount_Code + itemOptionTemp.Menu_Description + ",";
                                        break;
                                }
                            }
                            else
                            {
                                switch (itemOptionTemp.Pizza_Part)
                                {
                                    case "1":
                                        strFirstHalfCodeString = strFirstHalfCodeString + itemOptionTemp.Amount_Code + itemOptionTemp.Description;
                                        if (itemOptionTemp.Topping_group)
                                            strFirstTopCodeString = strFirstTopCodeString + itemOptionTemp.Amount_Code + itemOptionTemp.Description;
                                        break;
                                    case "2":
                                        strSecondHalfCodeString = strSecondHalfCodeString + itemOptionTemp.Amount_Code + itemOptionTemp.Description;
                                        if (itemOptionTemp.Topping_group)
                                            strSecondTopCodeString = strSecondTopCodeString + itemOptionTemp.Amount_Code + itemOptionTemp.Description;
                                        break;
                                    case "W":
                                        strWholeCodeString = strWholeCodeString + itemOptionTemp.Amount_Code + itemOptionTemp.Description;
                                        if (itemOptionTemp.Topping_group)
                                            strWholeTopCodeString = strWholeTopCodeString + itemOptionTemp.Amount_Code + itemOptionTemp.Description;
                                        break;
                                }

                            }

                        }
                        else if (itemOptionTemp.Default_Topping != "-D-")
                        {
                            if (itemOptionTemp.Pizza_Part == "1" && blnFirstSpecialtyToppingH1)
                            {
                                strFirstHalfCodeString = "(" + itemOptionTemp.Default_Topping + ")" + strFirstHalfCodeString;
                                strFirstTopCodeString = "(" + itemOptionTemp.Default_Topping + ")" + strFirstTopCodeString;
                            }
                            else if (itemOptionTemp.Pizza_Part == "2" && blnFirstSpecialtyToppingH2)
                            {
                                strSecondHalfCodeString = "(" + itemOptionTemp.Default_Topping + ")" + strSecondHalfCodeString;
                                strSecondTopCodeString = "(" + itemOptionTemp.Default_Topping + ")" + strSecondTopCodeString;
                            }
                            else if (itemOptionTemp.Pizza_Part == "W" && blnFirstSpecialtyToppingWhole)
                            {
                                strWholeCodeString = "(" + itemOptionTemp.Default_Topping + ")" + strWholeCodeString;
                                strWholeTopCodeString = "(" + itemOptionTemp.Default_Topping + ")" + strWholeTopCodeString;
                            }

                        }


                        if (itemOptionTemp.Default_Topping != "-D-" && itemOptionTemp.Default_Topping != "")
                        {
                            if (itemOptionTemp.Pizza_Part == "1")
                                blnFirstSpecialtyToppingH1 = false;
                            else if (itemOptionTemp.Pizza_Part == "2")
                                blnFirstSpecialtyToppingH2 = false;
                            else if (itemOptionTemp.Pizza_Part == "W")
                                blnFirstSpecialtyToppingWhole = false;
                        }
                    }
                }


                cartItem.Topping_Codes = strWholeCodeString.Replace(" ", "") + "/" + strFirstHalfCodeString.Replace(" ", "") + "/" + strSecondHalfCodeString.Replace(" ", "");

                cartItem.Topping_String = strWholeTopCodeString.Replace(" ", "") + "/" + strFirstTopCodeString.Replace(" ", "") + "/" + strSecondTopCodeString.Replace(" ", "");

                if (cartItem.Topping_Codes.Trim() == "//")
                    cartItem.Topping_Codes = "";

                if (cartItem.Topping_String.Trim() == "//")
                    cartItem.Topping_String = "";


                while (cartItem.Topping_Codes.EndsWith("/"))
                {
                    cartItem.Topping_Codes = cartItem.Topping_Codes.Substring(0, cartItem.Topping_Codes.Length - 1);
                }

                while (cartItem.Topping_String.EndsWith("/"))
                {
                    cartItem.Topping_String = cartItem.Topping_String.Substring(0, cartItem.Topping_String.Length - 1);
                }


                cartItem.Topping_Descriptions = strWholeDescriptionString + "," + strFirstHalfDescriptionString + "," + strSecondHalfDescriptionString;

                cartItem.Topping_Descriptions = cartItem.Topping_Descriptions.Replace(",,", ",");


                if (cartItem.Topping_Descriptions.Length != 0)
                {
                    while (cartItem.Topping_Descriptions.EndsWith(","))
                    {
                        cartItem.Topping_Descriptions = cartItem.Topping_Descriptions.Substring(0, cartItem.Topping_Descriptions.Length - 1);
                    }

                    cartItem.Topping_Descriptions = UserFunctions.LineBreak(cartItem.Topping_Descriptions);
                }

                if (cartItem.Topping_Codes.Length != 0)
                {
                    while (cartItem.Topping_Codes.EndsWith(","))
                    {
                        cartItem.Topping_Codes = cartItem.Topping_Codes.Substring(0, cartItem.Topping_Codes.Length - 1);
                    }

                    cartItem.Topping_Codes = UserFunctions.LineBreak(cartItem.Topping_Codes);
                }


                if (cartItem.Doubles_Group == 0)
                    cartItem.Price = cartItem.Menu_Price * Convert.ToDecimal(cartItem.Quantity);


            }
            catch (Exception ex)
            {
                Logger.Trace("ERROR", "CartFunctions-HandleToppings(): " + ex.Message, ex, true);
            }
        }

        public static List<Topping> GetToppingsFromCatalogToppings(List<CatalogToppings> catalogToppings, int SelectedLineNumber, Cart cart,
                                                           string SelectedComboMenuItem_MenuCode, string selectedComboMenuItemSizes_SizeCode,
                                                           string selectedMenuItem_MenuCode, string SelectedMenuItemSizes_SizeCode)
        {
            bool ItemOptionAvailable = false;
            List<ItemOption> itemOptions = null; ;
            ItemOption itemOption = null; ;

            SelectedLineNumber = UserFunctions.ActualLineNumber(cart, SelectedLineNumber);

            CartItem cartItem = null;
            if (Session.ProcessingCombo && Session.CurrentComboItem > 0)
                cartItem = cart.cartItems.Find(x => x.Menu_Code == SelectedComboMenuItem_MenuCode && x.Size_Code == selectedComboMenuItemSizes_SizeCode && x.Line_Number == (SelectedLineNumber > 0 ? SelectedLineNumber : x.Line_Number));
            else
                cartItem = cart.cartItems.Find(x => x.Menu_Code == selectedMenuItem_MenuCode && x.Size_Code == SelectedMenuItemSizes_SizeCode && x.Line_Number == (SelectedLineNumber > 0 ? SelectedLineNumber : x.Line_Number));

            if (cartItem != null)
            {
                itemOptions = cartItem.itemOptions;
                if (itemOptions != null && itemOptions.Count > 0)
                {
                    ItemOptionAvailable = true;
                }
            }

            List<Topping> toppings = new List<Topping>();

            if (catalogToppings != null && catalogToppings.Count > 0)
            {
                foreach (CatalogToppings _catalogTopping in catalogToppings)
                {
                    Topping topping = new Topping();
                    topping.Menu_Code = _catalogTopping.Menu_Code;
                    topping.Size_Code = _catalogTopping.Size_Code;
                    topping.Order_Description = _catalogTopping.Order_Description;
                    topping.Topping_Code = _catalogTopping.Topping_Code;
                    topping.Menu_Item_Image = _catalogTopping.Menu_Item_Image;
                    topping.Text_Color = _catalogTopping.Text_Color;
                    topping.Kitchen_Display_Order = _catalogTopping.Kitchen_Display_Order;
                    topping.Amount_Code = _catalogTopping.Amount_Code;
                    topping.Default_Item = _catalogTopping.Default_Item;
                    topping.Item_Part = _catalogTopping.Item_Part;
                    topping.Default = _catalogTopping.Default;
                    topping.MenuItemType = _catalogTopping.MenuItemType;


                    if (ItemOptionAvailable)
                    {
                        itemOption = itemOptions.Find(x => x.Menu_Code == _catalogTopping.Menu_Code && x.Default_Topping != "" && x.Pizza_Part == "W" && x.Amount_Code != "-");
                        if (itemOption != null)
                        {
                            topping.WholeDefault = true;
                            topping.WholeDefaultAmount = itemOption.Amount_Code;
                        }
                        else
                        {
                            topping.WholeDefault = _catalogTopping.Default ? true : false;
                            topping.WholeDefaultAmount = _catalogTopping.Default ? _catalogTopping.Amount_Code : "";
                        }

                        itemOption = itemOptions.Find(x => x.Menu_Code == _catalogTopping.Menu_Code && x.Default_Topping != "" && x.Pizza_Part == "1");
                        if (itemOption != null)
                        {
                            topping.FirstHalfDefault = true;
                            topping.FirstHalfDefaultAmount = itemOption.Amount_Code;
                        }
                        else
                        {
                            topping.FirstHalfDefault = false;
                            topping.FirstHalfDefaultAmount = "";
                        }

                        itemOption = itemOptions.Find(x => x.Menu_Code == _catalogTopping.Menu_Code && x.Default_Topping != "" && x.Pizza_Part == "2");
                        if (itemOption != null)
                        {
                            topping.SecondHalfDefault = true;
                            topping.SecondHalfDefaultAmount = itemOption.Default_Amount_Code;
                        }
                        else
                        {
                            topping.SecondHalfDefault = false;
                            topping.SecondHalfDefaultAmount = "";
                        }

                        itemOption = itemOptions.Find(x => x.Menu_Code == _catalogTopping.Menu_Code && x.Default_Topping == "" && x.Pizza_Part == "W");
                        if (itemOption != null)
                        {
                            topping.SelectedOnWhole = true;
                            topping.WholeSelectedAmount = itemOption.Amount_Code;
                        }
                        else
                        {
                            topping.SelectedOnWhole = false;
                            topping.WholeSelectedAmount = "";
                        }

                        itemOption = itemOptions.Find(x => x.Menu_Code == _catalogTopping.Menu_Code && x.Default_Topping == "" && x.Pizza_Part == "1");
                        if (itemOption != null)
                        {
                            topping.SelectedOnFirstHalf = true;
                            topping.FirstHalfSelectedAmount = itemOption.Amount_Code;
                        }
                        else
                        {
                            topping.SelectedOnFirstHalf = false;
                            topping.FirstHalfSelectedAmount = "";
                        }

                        itemOption = itemOptions.Find(x => x.Menu_Code == _catalogTopping.Menu_Code && x.Default_Topping == "" && x.Pizza_Part == "2");
                        if (itemOption != null)
                        {
                            topping.SelectedOnSecondHalf = true;
                            topping.SecondHalfSelectedAmount = itemOption.Amount_Code;
                        }
                        else
                        {
                            topping.SelectedOnSecondHalf = false;
                            topping.SecondHalfSelectedAmount = "";
                        }

                        itemOption = itemOptions.Find(x => x.Menu_Code == _catalogTopping.Menu_Code && x.Default_Topping == "" && x.Pizza_Part == "W" && x.Amount_Code == "-");
                        if (itemOption != null)
                            topping.RemovedFromWhole = true;
                        else
                            topping.RemovedFromWhole = false;
                    }
                    else
                    {
                        topping.WholeDefault = _catalogTopping.Default ? true : false;
                        topping.WholeDefaultAmount = _catalogTopping.Default ? _catalogTopping.Amount_Code : "";
                        topping.FirstHalfDefault = false;
                        topping.FirstHalfDefaultAmount = "";
                        topping.SecondHalfDefault = false;
                        topping.SecondHalfDefaultAmount = "";
                        topping.SelectedOnWhole = false;
                        topping.WholeSelectedAmount = "";
                        topping.SelectedOnFirstHalf = false;
                        topping.FirstHalfSelectedAmount = "";
                        topping.SelectedOnSecondHalf = false;
                        topping.SecondHalfSelectedAmount = "";
                        topping.RemovedFromWhole = false;
                    }

                    toppings.Add(topping);
                }
            }

            return toppings;
        }

        public static void ToppingSelectionCompleted(CartItem curretCartItem)
        {
            Cart cartLocal = (new Cart().GetCart());

            curretCartItem.Topping_String = Create_Topping_Strings(Session.currentToppingCollection.pizzaToppings);

            HandleToppings(ref curretCartItem);

            curretCartItem.Action = "M";
            cartLocal.cartItems.Add(curretCartItem);
            cartLocal.cartHeader = Session.cart.cartHeader;
            CartFunctions.UpdateCustomer(cartLocal);
            Session.cart = APILayer.Add2Cart(cartLocal);
        }

        public static bool QuantityChangeCombo(bool Replace, int Qty, int currentComboGroup)
        {
            double NewQuantity = 0;
            CheckCart();
            Cart cartLocal = (new Cart().GetCart());

            ItemCombo itemCombo = Session.cart.itemCombos.Find(x => x.Combo_Group == currentComboGroup);

            if (itemCombo == null)
                return false;

            itemCombo.Combo_Price = itemCombo.Combo_Price / Convert.ToDecimal(itemCombo.Combo_Quantity);
            NewQuantity = itemCombo.Combo_Quantity;
            if (Replace)
                NewQuantity = Qty < 0 ? 0 : Qty;
            else
                NewQuantity = NewQuantity + (Qty);

            if (NewQuantity <= 0)
            {
                if (CustomMessageBox.Show(itemCombo.Combo_Description + " " + APILayer.GetCatalogText(LanguageConstant.cintMSGRemoveItemFromOrder), CustomMessageBox.Buttons.YesNo, CustomMessageBox.Icon.Question) == DialogResult.No)
                {
                    return false;
                }
            }

            itemCombo.Combo_Quantity = NewQuantity;
            itemCombo.Combo_Price = itemCombo.Combo_Price * Convert.ToDecimal(itemCombo.Combo_Quantity);
            itemCombo.Action = itemCombo.Combo_Quantity <= 0 ? "D" : "M";
            cartLocal.itemCombos.Add(itemCombo);

            foreach (CartItem cartItem in Session.cart.cartItems)
            {
                if (cartItem.Combo_Group == currentComboGroup)
                {
                    if (Replace)
                        cartItem.Quantity = Qty < 0 ? 0 : Qty;
                    else
                        cartItem.Quantity = cartItem.Quantity + (Qty);
                    cartItem.Action = cartItem.Quantity <= 0 ? "D" : "M";
                    cartLocal.cartItems.Add(cartItem);
                }
            }

            cartLocal.cartHeader.CartId = Session.cart.cartHeader.CartId;
            CartFunctions.UpdateCustomer(cartLocal);
            Session.cart = APILayer.Add2Cart(cartLocal);

            return true;

        }

        public static void GetCartUpsell(CatalogUpsellMenu catalogUpsellMenu, int quantity)
        {
            CartFunctions.CheckCart();
            Cart cartLocal = (new Cart().GetCart());
            CartItem cartItemLocal = new CartItem();
            cartItemLocal.Location_Code = catalogUpsellMenu.location_code;
            cartItemLocal.Order_Date = Session.SystemDate;
            cartItemLocal.Line_Number = 1;
            cartItemLocal.Sequence = 1;
            cartItemLocal.Size_Code = catalogUpsellMenu.Size_Code;
            cartItemLocal.Action = "A";
            cartItemLocal.Price = catalogUpsellMenu.price;
            cartItemLocal.Base_Price = catalogUpsellMenu.price;
            cartItemLocal.Menu_Price = catalogUpsellMenu.price;
            cartItemLocal.Menu_Price2 = catalogUpsellMenu.price;
            cartItemLocal.Menu_Description = catalogUpsellMenu.order_description;
            cartItemLocal.Menu_Code = catalogUpsellMenu.Menu_Code;
            cartItemLocal.Menu_Category_Code = catalogUpsellMenu.Menu_Category_Code;
            cartItemLocal.Size_Description = catalogUpsellMenu.description;
            cartItemLocal.Quantity = quantity;
            cartItemLocal.Added_By = Session.CurrentEmployee.LoginDetail.EmployeeCode;
            cartItemLocal.Bottle_Deposit = Session.selectedMenuItemSizes.Bottle_Deposit;
            cartItemLocal.Price_By_Weight = Session.selectedMenuItemSizes.Price_By_Weight;
            cartItemLocal.Open_Value_Card = Session.selectedMenuItemSizes.Open_Value_Card;
            cartItemLocal.Min_Amount_Open_Value_Card = Convert.ToDecimal(Session.selectedMenuItemSizes.Min_Amount_Open_Value_Card);
            cartItemLocal.Max_Amount_Open_Value_Card = Convert.ToDecimal(Session.selectedMenuItemSizes.Max_Amount_Open_Value_Card);
            cartItemLocal.Tare_Weight = Convert.ToSingle(Session.selectedMenuItemSizes.Tare_Weight);

            cartItemLocal.Combo_Menu_Code = "";
            cartItemLocal.Combo_Size_Code = "";
            cartItemLocal.Coupon_Code = "";
            cartItemLocal.Coupon_Description = "";
            cartItemLocal.Instruction_String = "";
            cartItemLocal.Orig_Menu_Code = "";
            cartItemLocal.Topping_Codes = "";
            cartItemLocal.Topping_Descriptions = "";
            cartItemLocal.Topping_String = "";
            cartItemLocal.MenuItemType = catalogUpsellMenu.MenuItemType;

            cartLocal.cartItems.Add(cartItemLocal);


            cartLocal.cartHeader = Session.cart.cartHeader;
            CartFunctions.UpdateCustomer(cartLocal);
            Session.cart = APILayer.Add2Cart(cartLocal);

        }



        public static void GenerateOrderCart(bool blnReplace, List<int> Indices, bool FromModify = false)
        {
            try
            {
                bool newCart = false;
                int cartItemNumber = 0;
                int ItemCounter = 0;

                CartFunctions.CheckCart();
                Cart cartLocal = (new Cart().GetCart());

                if (blnReplace == true)
                {
                    if (Session.cart.cartItems != null)
                    {
                        foreach (CartItem cartItem in Session.cart.cartItems)
                        {
                            CartItem cartItemlocal = new CartItem();
                            cartItemlocal = cartItem;
                            cartItemlocal.Action = "D";
                            cartLocal.cartItems.Add(cartItemlocal);
                        }
                    }

                    if (Session.cart.itemCombos != null && Session.cart.itemCombos.Count > 0)
                    {
                        foreach (ItemCombo itemCombo in Session.cart.itemCombos)
                        {
                            ItemCombo itemComboLocal = new ItemCombo();
                            itemComboLocal = itemCombo;
                            itemComboLocal.Action = "D";
                            cartLocal.itemCombos.Add(itemComboLocal);
                        }
                    }
                }

                if (!(Session.cart != null && Session.cart.cartHeader != null && !String.IsNullOrEmpty(Session.cart.cartHeader.CartId)))
                {
                    newCart = true;
                    CreateCart(Session.originalcart, ref cartItemNumber, Indices, Session.pblnModifyingOrder);
                }
                //if (Session.pblnModifyingOrder)
                //{
                //    Session.cart.Customer = Session.originalcart.Customer;
                //}

                if (cartItemNumber < 0) return;

                foreach (CartItem cartItemOrignal in Session.originalcart.cartItems)
                {
                    CartItem cartItem = new CartItem();
                    cartItem = cartItemOrignal;
                    //Session.menuItems                    
                    if ((Indices != null && Indices.Count > 0 && !Indices.Contains(ItemCounter)) || (Indices == null || Indices.Count <= 0))
                    {
                        if (cartItem.Menu_Description.Trim() != APILayer.GetCatalogText(LanguageConstant.cintNA).Trim())
                        {
                            if (!FromModify)
                            {
                                cartItem.Sequence = 1;
                                cartItem.Added_By = Session.CurrentEmployee.LoginDetail.EmployeeCode;
                                cartItem.Coupon_Adjustment = false;
                                cartItem.Coupon_Amount = 0;
                                cartItem.Coupon_Code = "";
                                cartItem.Coupon_Description = "";
                                cartItem.Coupon_Min_Price = 0;
                                cartItem.Coupon_Taxable = false;
                                cartItem.Coupon_Type_Code = 0;
                                cartItem.Order_Number = 0;
                                cartItem.Line_Number = (cartLocal.cartItems != null && cartLocal.cartItems.Count > 0) ? (cartLocal.cartItems.Max(x => x.Line_Number) + 1) : cartItem.Line_Number;

                                if (cartItem.itemAttributeGroups != null && cartItem.itemAttributeGroups.Count > 0)
                                    foreach (ItemAttributeGroup item in cartItem.itemAttributeGroups)
                                        item.Order_Number = 0;

                                if (cartItem.itemAttributes != null && cartItem.itemAttributes.Count > 0)
                                    foreach (ItemAttribute item in cartItem.itemAttributes)
                                        item.Order_Number = 0;

                                if (cartItem.itemGiftCards != null && cartItem.itemGiftCards.Count > 0)
                                    foreach (ItemGiftCard item in cartItem.itemGiftCards)
                                        item.Order_Number = 0;

                                if (cartItem.itemOptionGroups != null && cartItem.itemOptionGroups.Count > 0)
                                    foreach (ItemOptionGroup item in cartItem.itemOptionGroups)
                                        item.Order_Number = 0;

                                if (cartItem.itemOptions != null && cartItem.itemOptions.Count > 0)
                                    foreach (ItemOption item in cartItem.itemOptions)
                                    {
                                        item.Order_Number = 0;
                                        item.Added_By = Session.CurrentEmployee.LoginDetail.EmployeeCode;
                                    }

                                if (cartItem.itemReasons != null && cartItem.itemReasons.Count > 0)
                                    foreach (ItemReason item in cartItem.itemReasons)
                                    {
                                        item.Order_Number = 0;
                                        item.Added_By = Session.CurrentEmployee.LoginDetail.EmployeeCode;
                                    }

                                if (cartItem.itemSpecialtyPizzas != null && cartItem.itemSpecialtyPizzas.Count > 0)
                                    foreach (ItemSpecialtyPizza item in cartItem.itemSpecialtyPizzas)
                                    {
                                        item.Order_Number = 0;
                                        item.Added_By = Session.CurrentEmployee.LoginDetail.EmployeeCode;
                                    }

                                CatalogMenuItemSizes catalogMenuItemSizes = Session.AllCatalogMenuItemSizes.Find(x => x.Menu_Code == cartItem.Menu_Code && x.Size_Code == cartItem.Size_Code);
                                if (catalogMenuItemSizes != null)
                                {
                                    cartItem.Base_Price = catalogMenuItemSizes.Price;
                                    //cartItem.Base_Price2 = catalogMenuItemSizes.Price2;
                                    //cartItem.Menu_Price = catalogMenuItemSizes.Price;
                                    //cartItem.Menu_Price2 = catalogMenuItemSizes.Price2;
                                }
                                else
                                {
                                    cartItem.Base_Price = 0;
                                    //cartItem.Base_Price2 = 0;
                                    //cartItem.Menu_Price = 0;
                                    //cartItem.Menu_Price2 = 0;
                                }
                            }

                            CouponFunctions.SetEDVUpsellFlagtoCartItemModify(ref cartItem);


                            if (newCart == true && ItemCounter == cartItemNumber)
                            {
                                cartItem.Action = "M";
                                cartItem.CartId = Session.cart.cartHeader.CartId;
                            }
                            else
                            {
                                cartItem.Action = "A";
                            }

                            cartLocal.cartItems.Add(cartItem);

                            if (cartItem.Combo_Group > 0)
                            {
                                Session.ProcessingCombo = true;
                                Session.CurrentComboGroup = cartItem.Combo_Group;
                                Session.CurrentComboItem = cartItem.Combo_Item_Number;
                            }
                            else
                            {
                                Session.ProcessingCombo = false;
                                Session.CurrentComboGroup = 0;
                                Session.CurrentComboItem = 0;
                            }
                            //Default Toppings
                            List<CatalogOptionGroups> catalogOptionGroups = APILayer.GetOptionGroups(cartItem.Menu_Code);
                            if (catalogOptionGroups != null && catalogOptionGroups.Count > 0)
                            {
                                CatalogOptionGroups currentCatalogOptionGroups = catalogOptionGroups[0]; //Only one Option Group considered

                                if (currentCatalogOptionGroups.Topping_Group)
                                {
                                    Session.currentToppingCollection = new UserTypes.ToppingCollection();
                                    Session.currentToppingCollection.currentCatalogOptionGroups = currentCatalogOptionGroups;
                                    Session.currentToppingCollection.currentToppings = CartFunctions.GetToppingsFromCatalogToppings(APILayer.GetToppings(cartItem.Menu_Code, currentCatalogOptionGroups.Menu_Option_Group_Code), cartItem.Line_Number, cartLocal, cartItem.Menu_Code, cartItem.Size_Code, cartItem.Menu_Code, cartItem.Size_Code);
                                    string itemSpecialtyPizzasCode = string.Empty;
                                    if (cartItem.itemSpecialtyPizzas != null && cartItem.itemSpecialtyPizzas.Count > 0)
                                    {
                                        itemSpecialtyPizzasCode = cartItem.itemSpecialtyPizzas[0].Specialty_Pizza_Code;
                                    }
                                    else
                                    {
                                        itemSpecialtyPizzasCode = "";
                                    }

                                    CartFunctions.PopulateDefaultToppings(ref cartLocal, cartItem.Line_Number, cartItem.Menu_Code, cartItem.Menu_Code, itemSpecialtyPizzasCode, itemSpecialtyPizzasCode);
                                }
                            }
                            Session.currentToppingSizeCode = "";

                            Session.ProcessingCombo = false;
                            Session.CurrentComboGroup = 0;
                            Session.CurrentComboItem = 0;

                        }
                    }
                    ItemCounter++;
                }

                foreach (CartItem _cartItem in cartLocal.cartItems)
                {
                    List<ItemOption> localItemOptions = new List<ItemOption>();
                    if (_cartItem.itemOptions != null && _cartItem.itemOptions.Count > 0)
                    {
                        foreach (ItemOption itemOption in _cartItem.itemOptions)
                        {
                            ItemOption TempItemOption = new ItemOption();
                            TempItemOption = itemOption;
                            TempItemOption.CartId = Session.cart.cartHeader.CartId;
                            localItemOptions.Add(TempItemOption);
                        }
                        _cartItem.itemOptions = localItemOptions;
                    }
                    else
                    {
                        _cartItem.itemOptions = new List<ItemOption>();
                    }


                    if (_cartItem.itemReasons != null && _cartItem.itemReasons.Count > 0)
                    {
                        foreach (ItemReason itemReason in _cartItem.itemReasons)
                        {
                            itemReason.CartId = Session.cart.cartHeader.CartId;
                        }
                    }
                }

                if (Session.originalcart.itemCombos != null && Session.originalcart.itemCombos.Count > 0)
                {
                    if (cartLocal.itemCombos == null) cartLocal.itemCombos = new List<ItemCombo>();
                    foreach (ItemCombo itemCombo in Session.originalcart.itemCombos)
                    {
                        ItemCombo itemComboLocal = new ItemCombo();
                        itemComboLocal = itemCombo;
                        itemComboLocal.CartId = Session.cart.cartHeader.CartId;
                        itemComboLocal.Action = "A";
                        cartLocal.itemCombos.Add(itemComboLocal);
                    }
                }

                //cartLocal.itemCombos = Session.originalcart.itemCombos;

                //foreach (ItemCombo itemCombo in cartLocal.itemCombos)
                //{
                //    itemCombo.CartId = Session.cart.cartHeader.CartId;
                //    itemCombo.Action = "A";
                //}

                cartLocal.Customer = Session.cart.Customer;

                cartLocal.cartHeader = Session.cart.cartHeader;

                ComboFunctions.FillComboPropertiestoCartItemHistoryModify(ref cartLocal);
                if (FromModify)
                {
                    cartLocal.orderPayments = Session.originalcart.orderPayments;
                    cartLocal.orderCreditCards = Session.originalcart.orderCreditCards;
                    cartLocal.orderUDT = Session.originalcart.orderUDT;
                }

                else
                {
                    cartLocal.orderPayments = new List<OrderPayment>();
                    cartLocal.orderCreditCards = new List<OrderCreditCard>();
                }
                CartFunctions.UpdateCustomer(cartLocal);

                Session.cart = APILayer.Add2Cart(cartLocal);

                //UnComment for Hustle.Net 5.2
                Session.ODC_Tax = Session.cart.cartHeader.ODC_Tax;

                Session.AggregatorGSTValue = 0;
                if (FromModify)
                {
                    if (Session.originalcart != null && Session.originalcart.orderAdditionalCharges != null && Session.originalcart.orderAdditionalCharges.Count > 0)
                    {
                        int ChargeID = Convert.ToInt32(Convert.ToString(SystemSettings.GetSettingValue("AggregatorGSTChargeID", Session.originalcart.cartHeader.LocationCode)));
                        OrderAdditionalCharge orderAdditionalCharge = Session.originalcart.orderAdditionalCharges.Find(x => x.Charge_Id == ChargeID);
                        if (orderAdditionalCharge != null)
                            Session.AggregatorGSTValue = orderAdditionalCharge.Amount;
                    }
				}

                if(Session.cart != null && Session.cart.cartItems != null && Session.cart.cartItems.Count >0)
                {
                    Session.LineNumbersFromHistory = new List<int>();
                    foreach (CartItem cartItem in Session.cart.cartItems)
                        Session.LineNumbersFromHistory.Add(cartItem.Line_Number);
                }

            }
            catch (Exception ex)
            {
                Logger.Trace("ERROR", "CartFunctions-GenerateOrderCart(): " + ex.Message, ex, true);
            }
        }

        public static void CreateCart(Cart originalCart, ref int cartItemNumber, List<int> Indices, bool FromModify = false)
        {

            CartFunctions.CheckCart();
            Cart cartLocal = (new Cart().GetCart());


            if (FromModify)
            {
                cartLocal.Customer = originalCart.Customer;
                cartLocal.cartHeader = originalCart.cartHeader;
                cartLocal.orderUDT = originalCart.orderUDT;
                cartLocal.cartHeader.Modifying = "1";                
            }
            else
            {
                FillCustomerToCart(ref cartLocal);
                FillCartHeaderDefault(ref cartLocal);
            }
            cartLocal.cartHeader.Order_Type_Code = originalCart.cartHeader.Order_Type_Code;

            // cartLocal.Customer = originalCart.Customer;

            CartItem cartItemLocal = new CartItem();

            int availbleIndex = GetFirstAvailableItemIndex(originalCart, Indices);
            cartItemNumber = availbleIndex;


            if (availbleIndex >= 0)
            {

                cartItemLocal = originalCart.cartItems[availbleIndex];

                cartItemLocal.Location_Code = Session._LocationCode;
                cartItemLocal.Order_Date = Session.SystemDate;

                if (!FromModify)
                {
                    cartItemLocal.Line_Number = 1;
                    cartItemLocal.Sequence = 1;
                    cartItemLocal.Added_By = Session.CurrentEmployee.LoginDetail.EmployeeCode;
                    cartItemLocal.Order_Number = 0;

                    if (cartItemLocal.itemAttributeGroups != null && cartItemLocal.itemAttributeGroups.Count > 0)
                        foreach (ItemAttributeGroup item in cartItemLocal.itemAttributeGroups)
                            item.Order_Number = 0;

                    if (cartItemLocal.itemAttributes != null && cartItemLocal.itemAttributes.Count > 0)
                        foreach (ItemAttribute item in cartItemLocal.itemAttributes)
                            item.Order_Number = 0;

                    if (cartItemLocal.itemGiftCards != null && cartItemLocal.itemGiftCards.Count > 0)
                        foreach (ItemGiftCard item in cartItemLocal.itemGiftCards)
                            item.Order_Number = 0;

                    if (cartItemLocal.itemOptionGroups != null && cartItemLocal.itemOptionGroups.Count > 0)
                        foreach (ItemOptionGroup item in cartItemLocal.itemOptionGroups)
                            item.Order_Number = 0;

                    if (cartItemLocal.itemOptions != null && cartItemLocal.itemOptions.Count > 0)
                        foreach (ItemOption item in cartItemLocal.itemOptions)
                        {
                            item.Order_Number = 0;
                            item.Added_By = Session.CurrentEmployee.LoginDetail.EmployeeCode;
                        }

                    if (cartItemLocal.itemReasons != null && cartItemLocal.itemReasons.Count > 0)
                        foreach (ItemReason item in cartItemLocal.itemReasons)
                        {
                            item.Order_Number = 0;
                            item.Added_By = Session.CurrentEmployee.LoginDetail.EmployeeCode;
                        }

                    if (cartItemLocal.itemSpecialtyPizzas != null && cartItemLocal.itemSpecialtyPizzas.Count > 0)
                        foreach (ItemSpecialtyPizza item in cartItemLocal.itemSpecialtyPizzas)
                        {
                            item.Order_Number = 0;
                            item.Added_By = Session.CurrentEmployee.LoginDetail.EmployeeCode;
                        }

                    CatalogMenuItemSizes catalogMenuItemSizes = Session.AllCatalogMenuItemSizes.Find(x => x.Menu_Code == cartItemLocal.Menu_Code && x.Size_Code == cartItemLocal.Size_Code);
                    if (catalogMenuItemSizes != null)
                    {
                        cartItemLocal.Base_Price = catalogMenuItemSizes.Price;
                        //cartItemLocal.Base_Price2 = catalogMenuItemSizes.Price2;
                        //cartItemLocal.Menu_Price = catalogMenuItemSizes.Price;
                        //cartItemLocal.Menu_Price2 = catalogMenuItemSizes.Price2;
                    }
                    else
                    {
                        cartItemLocal.Base_Price = 0;
                        //cartItemLocal.Base_Price2 = 0;
                        //cartItemLocal.Menu_Price = 0;
                        //cartItemLocal.Menu_Price2 = 0;
                    }



                }   //if (cartItemLocal.Menu_Description.Trim() == APILayer.GetCatalogText(LanguageConstant.cintNA).Trim())
                    //    cartItemLocal.Action = "D";
                    //else
                cartItemLocal.Action = "A";


                cartLocal.cartItems.Add(cartItemLocal);

                // CartFunctions.UpdateCustomer(cartLocal);
                Session.cart = APILayer.Add2Cart(cartLocal);

                Session.selectedOrderType = cartLocal.cartHeader.Order_Type_Code;
            }
        }

        //public static void FillCustomerToCartLocal(ref Cart cart)
        //{
        //    if (Session.cart.Customer != null)
        //    {
        //        cart.Customer.Location_Code = String.IsNullOrEmpty(Session.cart.Customer.Location_Code) ? "" : Session.cart.Customer.Location_Code;
        //        cart.Customer.Phone_Number = String.IsNullOrEmpty(Session.cart.Customer.Phone_Number) ? "" : Session.cart.Customer.Phone_Number;
        //        cart.Customer.Phone_Ext = String.IsNullOrEmpty(Session.cart.Customer.Phone_Ext) ? "" : Session.cart.Customer.Phone_Ext;
        //        cart.Customer.Customer_Code = Convert.ToInt32(Session.cart.Customer.Customer_Code);
        //        cart.Customer.Name = String.IsNullOrEmpty(Session.cart.Customer.Name) ? "" : Session.cart.Customer.Name;
        //        cart.Customer.Company_Name = String.IsNullOrEmpty(Session.cart.Customer.Company_Name) ? "" : Session.cart.Customer.Company_Name;
        //        cart.Customer.Street_Number = String.IsNullOrEmpty(Session.cart.Customer.Street_Number) ? "" : Session.cart.Customer.Street_Number;
        //        cart.Customer.Street_Code = Session.cart.Customer.Street_Code == 0 ? 1 : Session.cart.Customer.Street_Code;
        //        cart.Customer.Cross_Street_Code = Session.cart.Customer.Cross_Street_Code;
        //        cart.Customer.Suite = String.IsNullOrEmpty(Session.cart.Customer.Suite) ? "" : Session.cart.Customer.Suite;
        //        cart.Customer.Address_Line_2 = String.IsNullOrEmpty(Session.cart.Customer.Address_Line_2) ? "" : Session.cart.Customer.Address_Line_2;
        //        cart.Customer.Address_Line_3 = String.IsNullOrEmpty(Session.cart.Customer.Address_Line_3) ? "" : Session.cart.Customer.Address_Line_3;
        //        cart.Customer.Address_Line_4 = String.IsNullOrEmpty(Session.cart.Customer.Address_Line_4) ? "" : Session.cart.Customer.Address_Line_4;
        //        cart.Customer.Mailing_Address = "";
        //        cart.Customer.Postal_Code = String.IsNullOrEmpty(Session.cart.Customer.Postal_Code) ? "" : Session.cart.Customer.Postal_Code;
        //        cart.Customer.Plus4 = "";
        //        cart.Customer.Cart = "";
        //        cart.Customer.Delivery_Point_Code = "";
        //        cart.Customer.Walk_Sequence = "";
        //        cart.Customer.Address_Type = String.IsNullOrEmpty(Session.cart.Customer.Address_Type) ? "" : Session.cart.Customer.Address_Type;
        //        cart.Customer.Set_Discount = 0;
        //        cart.Customer.Tax_Exempt1 = Session.cart.Customer.Tax_Exempt1;
        //        cart.Customer.Tax_ID1 = String.IsNullOrEmpty(Session.cart.Customer.Tax_ID1) ? "" : Session.cart.Customer.Tax_ID1;
        //        cart.Customer.Tax_Exempt2 = Session.cart.Customer.Tax_Exempt2;
        //        cart.Customer.Tax_ID2 = String.IsNullOrEmpty(Session.cart.Customer.Tax_ID2) ? "" : Session.cart.Customer.Tax_ID2;
        //        cart.Customer.Tax_Exempt3 = Session.cart.Customer.Tax_Exempt3;
        //        cart.Customer.Tax_ID3 = String.IsNullOrEmpty(Session.cart.Customer.Tax_ID3) ? "" : Session.cart.Customer.Tax_ID3;
        //        cart.Customer.Tax_Exempt4 = Session.cart.Customer.Tax_Exempt4;
        //        cart.Customer.Tax_ID4 = String.IsNullOrEmpty(Session.cart.Customer.Tax_ID4) ? "" : Session.cart.Customer.Tax_ID4;
        //        cart.Customer.Accept_Checks = false;
        //        cart.Customer.Accept_Credit_Cards = false;
        //        cart.Customer.Accept_Gift_Cards = false;
        //        cart.Customer.Accept_Charge_Account = false;
        //        cart.Customer.Accept_Cash = false;
        //        cart.Customer.Finance_Charge_Rate = Session.cart.Customer.Finance_Charge_Rate;
        //        cart.Customer.Credit_Limit = 0;
        //        cart.Customer.Credit = 0;
        //        cart.Customer.Payment_Terms = 0;
        //        cart.Customer.First_Order_Date = Session.cart.Customer.First_Order_Date == Convert.ToDateTime("01-01-0001") ? new DateTime(1899, 12, 30) : Session.cart.Customer.First_Order_Date;
        //        cart.Customer.Last_Order_Date = Session.cart.Customer.Last_Order_Date == Convert.ToDateTime("01-01-0001") ? new DateTime(1899, 12, 30) : Session.cart.Customer.Last_Order_Date;
        //        cart.Customer.Added_By = String.IsNullOrEmpty(Session.cart.Customer.Added_By) ? "" : Session.cart.Customer.Added_By;
        //        cart.Customer.Comments = String.IsNullOrEmpty(Session.cart.Customer.Comments) ? "" : Session.cart.Customer.Comments;
        //        cart.Customer.DriverComments = String.IsNullOrEmpty(Session.cart.Customer.DriverComments) ? "" : Session.cart.Customer.DriverComments;
        //        cart.Customer.DriverCommentsAddUpdateDelete = false;
        //        cart.Customer.Manager_Notes = String.IsNullOrEmpty(Session.cart.Customer.Manager_Notes) ? "" : Session.cart.Customer.Manager_Notes;
        //        cart.Customer.Customer_City_Code = 0;
        //        cart.Customer.Customer_Street_Name = "";
        //        cart.Customer.HotelorCollege = false;
        //        cart.Customer.City = String.IsNullOrEmpty(Session.cart.Customer.City) ? "" : Session.cart.Customer.City;
        //        cart.Customer.Region = String.IsNullOrEmpty(Session.cart.Customer.Region) ? "" : Session.cart.Customer.Region;
        //        cart.Customer.TaxRate1 = 0;
        //        cart.Customer.TaxRate2 = 0;
        //        cart.Customer.Cross_Street = "";
        //        cart.Customer.NoteAddUpdateDelete = false;
        //        cart.Customer.gstin_number = String.IsNullOrEmpty(Session.cart.Customer.gstin_number) ? "" : Session.cart.Customer.gstin_number;
        //        cart.Customer.date_of_birth = Session.cart.Customer.date_of_birth == Convert.ToDateTime("01-01-0001") ? new DateTime(1899, 12, 30) : Session.cart.Customer.date_of_birth;
        //        cart.Customer.anniversary_date = Session.cart.Customer.anniversary_date == Convert.ToDateTime("01-01-0001") ? new DateTime(1899, 12, 30) : Session.cart.Customer.anniversary_date;
        //        cart.Customer.Action = "M";
        //    }
        //}

        public static void FillCartHeaderDefault(ref Cart cartLocal)
        {
            cartLocal.cartHeader.CartId = Session.cart.cartHeader.CartId;
            cartLocal.cartHeader.LocationCode = Session._LocationCode;
            cartLocal.cartHeader.Order_Date = Session.cart.cartHeader.Order_Date == DateTime.MinValue ? Session.SystemDate : Session.cart.cartHeader.Order_Date;
            cartLocal.cartHeader.ctlAddressCity = cartLocal.Customer.City.Trim();
            cartLocal.cartHeader.Actual_Order_Date = Convert.ToDateTime(SystemSettings.settings.pdtmServerDateTime);
            cartLocal.cartHeader.Customer_Code = cartLocal.Customer.Customer_Code;
            cartLocal.cartHeader.Customer_Name = cartLocal.Customer.Name;
            cartLocal.cartHeader.Computer_Name = Session.ComputerName;
            cartLocal.cartHeader.Order_Status_Code = 1;
            cartLocal.cartHeader.Order_Taker_ID = Session.CurrentEmployee.LoginDetail.EmployeeCode;
            cartLocal.cartHeader.Order_Taker_Shift = Convert.ToString(Session.CurrentEmployee.LoginDetail.DateShiftNumber);
            cartLocal.cartHeader.Order_Time = 0;
            cartLocal.cartHeader.Order_Type_Code = Session.selectedOrderType;
            cartLocal.cartHeader.Added_By = Session.CurrentEmployee.LoginDetail.EmployeeCode;
            cartLocal.cartHeader.Workstation_ID = Session._WorkStationID;
            cartLocal.cartHeader.Modifying = "";
            cartLocal.cartHeader.Customer_Room = "";
            cartLocal.cartHeader.Comments = string.Empty;
            cartLocal.cartHeader.Coupon_Code = "";
            cartLocal.cartHeader.Driver_ID = "";
            cartLocal.cartHeader.Driver_Shift = "";
            cartLocal.cartHeader.Credit_Card_Name = "";
            cartLocal.cartHeader.Tent_Number = "";
            cartLocal.cartHeader.Secure_Coupon_ID = "";
            cartLocal.cartHeader.ROI_Customer = "";
            cartLocal.cartHeader.Instruction = "";
            cartLocal.cartHeader.Types = "";
            cartLocal.cartHeader.Device_Type = "";
            cartLocal.cartHeader.Platform = "";
            cartLocal.cartHeader.Browser = "";
            cartLocal.cartHeader.Payment_Gateway = "";
            cartLocal.cartHeader.CustomField1 = "";
            cartLocal.cartHeader.CustomField2 = "";
            cartLocal.cartHeader.CustomField3 = "";
            cartLocal.cartHeader.CustomField4 = "";
            cartLocal.cartHeader.CustomField5 = "";
            cartLocal.cartHeader.CustomField7 = "";
            cartLocal.cartHeader.CustomField8 = "";
            cartLocal.cartHeader.CustomField9 = "";
            cartLocal.cartHeader.CustomField10 = "";
            cartLocal.cartHeader.OTS_Number = "";
            cartLocal.cartHeader.Delayed_Date = Session.cart.cartHeader.Delayed_Date;
            cartLocal.cartHeader.Delayed_Same_Day = Session.cart.cartHeader.Delayed_Same_Day;
            cartLocal.cartHeader.Kitchen_Display_Time = Session.cart.cartHeader.Kitchen_Display_Time;
            cartLocal.cartHeader.Delivery_Fee = 0;
            cartLocal.cartHeader.Route_Time = Session.cart.cartHeader.Route_Time;
        }

        public static void MenuItemsCheck(string Menu_Code, string Menu_Catagory_Code)
        {
            Session.selectedMenuItem = Session.menuItems.Find(x => x.Menu_Code == Menu_Code);

            if (Session.selectedMenuItem == null)
            {
                List<CatalogMenuItems> currentMenuItems = APILayer.GetMenuItems(Menu_Catagory_Code, Session.selectedOrderType);
                RemoveItemsOrderTypeFromMenuItems(Session.selectedOrderType);
                foreach (CatalogMenuItems catalogMenuItems in currentMenuItems)
                {
                    if (!Session.menuItems.Any(z => z.Menu_Code == catalogMenuItems.Menu_Code)) Session.menuItems.Add(catalogMenuItems);
                }

                Session.selectedMenuItem = Session.menuItems.Find(x => x.Menu_Code == Menu_Code);
            }
        }

        public static void MenuItemSizesCheck(string Menu_Code, string Size_Code)
        {
            Session.selectedMenuItemSizes = Session.menuItemSizes.Find(x => x.Menu_Code == Menu_Code && x.Size_Code == Size_Code);

            if (Session.selectedMenuItemSizes == null)
            {
                List<CatalogMenuItemSizes> CurrentMenuItemSizes = APILayer.GetMenuItemSizes(Menu_Code, Session.selectedOrderType);
                CartFunctions.RemoveItemSizesOrderTypeFromMenuItemSizes(Session.selectedOrderType);
                foreach (CatalogMenuItemSizes catalogMenuItemSizes in CurrentMenuItemSizes)
                {
                    if (!Session.menuItemSizes.Any(z => z.Menu_Code == catalogMenuItemSizes.Menu_Code && z.Size_Code == catalogMenuItemSizes.Size_Code)) Session.menuItemSizes.Add(catalogMenuItemSizes);
                }

                Session.selectedMenuItemSizes = Session.menuItemSizes.Find(x => x.Menu_Code == Menu_Code && x.Size_Code == Size_Code);
            }
        }

        public static void GetCartRemake(string btnName)
        {
            CheckCart();
            LoginResult oldLoginEmployee;
            Cart cartLocal = (new Cart().GetCart());
            if (btnName != "D")
            {
                Session.selectedOrderCoupon = Session.OrderCoupons.First(x => x.Coupon_Code == btnName);
                cartLocal.Customer = Session.cart.Customer;
                cartLocal.cartHeader = Session.cart.cartHeader;
                cartLocal.cartHeader.Coupon_Code = Session.selectedOrderCoupon.Coupon_Code;
                cartLocal.cartHeader.Coupon_Taxable = Session.selectedOrderCoupon.Taxable;
                cartLocal.cartHeader.Coupon_Total = Session.selectedOrderCoupon.Amount;
                cartLocal.cartHeader.Coupon_Type_Code = Session.selectedOrderCoupon.Coupon_Type_Code;
                cartLocal.cartHeader.Coupon_Adjustment = Session.selectedOrderCoupon.Adjustment;
                cartLocal.cartHeader.Coupon_Amount = Session.selectedOrderCoupon.Amount;
            }
            else
            {
                //Session.selectedOrderCoupon = Session.OrderCoupons.First(x => x.Coupon_Code == btnName);
                cartLocal.Customer = Session.cart.Customer;
                cartLocal.cartHeader = Session.cart.cartHeader;
                cartLocal.cartHeader.Coupon_Code = null;
                //cartLocal.cartHeader.Coupon_Taxable = Session.selectedOrderCoupon.Taxable;
                cartLocal.cartHeader.Coupon_Total = 0;
                cartLocal.cartHeader.Coupon_Type_Code = 0;
                //cartLocal.cartHeader.Coupon_Adjustment = Session.selectedOrderCoupon.Adjustment;
                cartLocal.cartHeader.Coupon_Amount = 0;
            }

            if (cartLocal.cartHeader.Coupon_Type_Code == Constants.VariablePrice || cartLocal.cartHeader.Coupon_Type_Code == Constants.VariableDiscount)
            {
                if (Session.selectedOrderCoupon.Protect_Coupon)
                {
                    if (!String.IsNullOrEmpty(Session.CurrentEmployee.LoginDetail.EmployeeCode) && Session.RemakeOrder == false)
                    {

                        frmPassword frmPassword = new frmPassword();
                        frmPassword.ShowDialog();
                        EmployeeLoginRequest loginRequest = new EmployeeLoginRequest();

                        loginRequest.UserId = Session.UserID;
                        loginRequest.Password = frmPassword.TypedPassword;
                        loginRequest.LocationCode = Session._LocationCode;

                        loginRequest.SystemDate = Session.SystemDate;
                        loginRequest.Source = Constants.Source;

                        loginRequest.EmployeeCode = Session.UserID;

                        int status = APILayer.ValidateLogin(APILayer.CallType.POST, loginRequest);
                        if (status == 0)

                            if (!EmployeeFunctions.MatchEmployeePassword())

                            {
                                CustomMessageBox.Show(MessageConstant.InvalidLogin, CustomMessageBox.Buttons.OK, CustomMessageBox.Icon.Warning);
                                return;
                            }
                    }
                    else
                    {
                        //return;
                    }
                }
                if (Session.RemakeOrder)
                {
                    cartLocal.cartHeader.Coupon_Amount = Convert.ToDecimal(Session.cart.cartHeader.SubTotal) - Convert.ToDecimal(Session.cart.cartHeader.Delivery_Fee);
                }
                else
                {
                    cartLocal.cartHeader.Coupon_Amount = Convert.ToDecimal(Session.cart.cartHeader.SubTotal);

                }
            }

            cartLocal.cartHeader.Action = "M";
            CartFunctions.UpdateCustomer(cartLocal);

            Session.cart = APILayer.Add2Cart(cartLocal);




        }
        public static bool ODCTaxChange()
        {
            if (Session.cart != null && Session.cart.cartHeader.CartId != null && Session.cart.cartHeader.CartId != "")
            {
                Session.cart.cartHeader.ODC_Tax = Session.ODC_Tax;
                Cart cartLocal = (new Cart().GetCart());
                cartLocal.cartHeader = Session.cart.cartHeader;
                cartLocal.cartHeader.CartId = Session.cart.cartHeader.CartId;
                cartLocal.cartHeader.Action = "M";
                CartFunctions.UpdateCustomer(cartLocal);
                Session.cart = APILayer.Add2Cart(cartLocal);

                return true;
            }
            else
            {
                return false;
            }
        }

        public static void UpdateCustomer(Cart cartLocal)
        {
            cartLocal.Customer = Session.cart.Customer;
            cartLocal.Customer.Action = "M";

            cartLocal.cartHeader = string.IsNullOrEmpty(Session.cart.cartHeader.LocationCode) ? cartLocal.cartHeader : Session.cart.cartHeader;
            cartLocal.cartHeader.Customer_Code = Session.cart.Customer.Customer_Code;
            cartLocal.cartHeader.Customer_Name = Session.cart.Customer.Name;
            cartLocal.cartHeader.Action = "M";
            cartLocal.cartHeader.ROI_Customer = Session.selectedSourceName;
        }

        public static void RemoveItemsOrderTypeFromMenuItems(string Order_Type_Code)
        {
            if (Session.menuItems != null && Session.menuItems.Count > 0)
            {
                Session.menuItems.RemoveAll(x => x.Order_Type_Code != Order_Type_Code);
            }
        }

        public static List<int> ItemsToExcludeFromCart()
        {
            if (Session.originalcart == null || Session.originalcart.cartItems.Count <= 0 || Session.cart == null) return (new List<int>());

            List<int> Indexes = new List<int>();

            List<CatalogMenuItems> menu_Items = new List<CatalogMenuItems>();

            List<string> distinctItemCategory = Session.originalcart.cartItems.Select(x => x.Menu_Category_Code).Distinct().ToList();

            string OrderType;
            //= (Convert.ToString(Session.originalcart.cartHeader.Order_Type_Code) == "") ? Session.originalcart.cartHeader.Order_Type_Code : Session.orderPayTypeCodes;

            if (Session.originalcart.cartHeader.Order_Type_Code != "")
            {
                OrderType = Session.originalcart.cartHeader.Order_Type_Code;
            }
            else
            {
                OrderType = Session.selectedOrderType;
            }

            foreach (string itemCategory in distinctItemCategory)
            {
                List<CatalogMenuItems> currentMenuItems = APILayer.GetMenuItems(itemCategory, OrderType);

                foreach (CatalogMenuItems MenuItems in currentMenuItems)
                {
                    if (!menu_Items.Any(z => z.Menu_Code == MenuItems.Menu_Code)) menu_Items.Add(MenuItems);
                }
            }

            if (menu_Items == null || menu_Items.Count <= 0) return (new List<int>());

            //foreach (CartItem cartItem in cart.cartItems)
            for (int i = 0; i <= Session.originalcart.cartItems.Count - 1; i++)
            {
                if (Session.originalcart.cartItems[i].Combo_Group <= 0)
                {
                    int index = -1;
                    index = menu_Items.FindIndex(
                        delegate (CatalogMenuItems mi)
                        {
                            return mi.Menu_Code == Session.originalcart.cartItems[i].Menu_Code && (mi.Enabled == false || mi.EnabledInv == false);
                        }
                        );

                    if (index == -1)
                    {
                        List<CatalogMenuItemSizes> menu_Item_Sizes = APILayer.GetMenuItemSizes(Session.originalcart.cartItems[i].Menu_Code, Session.cart.cartHeader.Order_Type_Code);
                        index = menu_Item_Sizes.FindIndex(
                        delegate (CatalogMenuItemSizes mis)
                        {
                            return mis.Menu_Code == Session.originalcart.cartItems[i].Menu_Code && mis.Size_Code == Session.originalcart.cartItems[i].Size_Code && ( mis.Enabled == false || mis.EnabledInv == false);
                        }
                        );
                    }

                    if (index > -1) Indexes.Add(i);
                }

            }

            if (Session.originalcart.itemCombos != null && Session.originalcart.itemCombos.Count > 0)
            {
                for (int j = 0; j <= Session.originalcart.itemCombos.Count - 1; j++)
                {
                    int indexCombo = -1;
                    indexCombo = menu_Items.FindIndex(
                        delegate (CatalogMenuItems mi)
                        {
                            return mi.Menu_Code == Session.originalcart.itemCombos[j].Combo_Menu_Code && ( mi.Enabled == false || mi.EnabledInv == false);
                        }
                        );

                    if (indexCombo == -1)
                    {
                        List<CatalogMenuItemSizes> menu_Item_Sizes = APILayer.GetMenuItemSizes(Session.originalcart.itemCombos[j].Combo_Menu_Code, Session.cart.cartHeader.Order_Type_Code);
                        indexCombo = menu_Item_Sizes.FindIndex(
                        delegate (CatalogMenuItemSizes mis)
                        {
                            return mis.Menu_Code == Session.originalcart.itemCombos[j].Combo_Menu_Code && mis.Size_Code == Session.originalcart.itemCombos[j].Combo_Size_Code && ( mis.Enabled == false || mis.EnabledInv == false);
                        }
                        );
                    }

                    if (indexCombo > -1)
                    {
                        //ComboIndices.Add(j);

                        for (int k = 0; k <= Session.originalcart.cartItems.Count - 1; k++)
                        {
                            if (Session.originalcart.itemCombos[j].Combo_Menu_Code == Session.originalcart.cartItems[k].Combo_Menu_Code && Session.originalcart.itemCombos[j].Combo_Size_Code == Session.originalcart.cartItems[k].Combo_Size_Code)
                                Indexes.Add(k);
                        }

                    }
                }
            }

            return Indexes;
        }

        public static void RemoveItemSizesOrderTypeFromMenuItemSizes(string Order_Type_Code)
        {
            if (Session.menuItemSizes != null && Session.menuItemSizes.Count > 0)
            {
                Session.menuItemSizes.RemoveAll(x => x.Order_Type_Code != Order_Type_Code);
            }
        }

        public static void SpecialtyItemChosen(string strButtonCaption, string strButtonTag, bool blnSelected, string strItemPart, int SelectedLineNumber = 0)
        {
            int index = 0;
            string strTempMenuCode;
            string strTempMenuDescription;
            string strTempSpecialtyCode;
            ItemSpecialtyPizza itemSpecialityPizza;
            string pstrPizzaPart;
            string[] strPizzaHalves = new string[2]; //TO DO - Reconsider
            bool blnSpecialtyPizzaOnHalf = false; //TO DO - Reconsider
            string pstrOriginalPizzaMenuCode = ""; //TO DO - Reconsider
            string pstrOriginalPizzaMenuDescription; //TO DO - Reconsider
            decimal pcurOriginalPizzaPrice; //TO DO - Reconsider
            Cart cartLocal = (new Cart().GetCart());
            Form frmObj = Application.OpenForms["frmOrder"];

            SelectedLineNumber = UserFunctions.ActualLineNumber(Session.cart, SelectedLineNumber);
            Session.SelectedLineNumber = SelectedLineNumber;

            CatalogSpecialtyPizzas currentSpecialtyPizza = Session.SpecialtyPizzasList.Find(x => x.Menu_Code == strButtonTag);

            CartItem currentCartItem = Session.cart.cartItems.Find(x => x.Line_Number == (SelectedLineNumber > 0 ? SelectedLineNumber : x.Line_Number));

            if (strItemPart != "W" && blnSelected)
            {
                if (currentCartItem.itemSpecialtyPizzas != null && currentCartItem.itemSpecialtyPizzas.Count > 0)
                {
                    itemSpecialityPizza = currentCartItem.itemSpecialtyPizzas.Find(x => x.Pizza_Part == "W");

                    if (itemSpecialityPizza != null)
                    {
                        strTempMenuCode = itemSpecialityPizza.Menu_Code;
                        strTempMenuDescription = currentCartItem.Menu_Description;
                        strTempSpecialtyCode = itemSpecialityPizza.Specialty_Pizza_Code;

                        //'Remove the specialty from the whole
                        SpecialtyItemChosen(currentCartItem.Menu_Description, strTempMenuCode, false, "W", SelectedLineNumber);

                        Session.SpecialtyItems.SpecialtyCodeWhole = "";

                        currentCartItem = Session.cart.cartItems.Find(x => x.Line_Number == (SelectedLineNumber > 0 ? SelectedLineNumber : x.Line_Number));

                        //'Now place the specialty on the other half
                        SpecialtyItemChosen(strTempMenuDescription, strTempMenuCode, true, (strItemPart == "1" ? "2" : "1"), SelectedLineNumber);

                        if (strItemPart == "1")
                            Session.SpecialtyItems.SpecialtyCode2ndHalf = strTempSpecialtyCode;
                        else
                            Session.SpecialtyItems.SpecialtyCode1stHalf = strTempSpecialtyCode;

                        currentCartItem = Session.cart.cartItems.Find(x => x.Line_Number == (SelectedLineNumber > 0 ? SelectedLineNumber : x.Line_Number));

                        //return;
                    }
                }
            }
            else if (strItemPart == "W" && blnSelected)
            {
                if (currentCartItem.itemSpecialtyPizzas != null && currentCartItem.itemSpecialtyPizzas.Count > 0)
                {
                    itemSpecialityPizza = currentCartItem.itemSpecialtyPizzas.Find(x => x.Pizza_Part == "1");

                    if (itemSpecialityPizza != null)
                    {
                        strTempMenuCode = itemSpecialityPizza.Menu_Code;
                        strTempMenuDescription = currentCartItem.Menu_Description;
                        strTempSpecialtyCode = itemSpecialityPizza.Specialty_Pizza_Code;

                        //'Remove the specialty from the 1st Half
                        SpecialtyItemChosen(currentCartItem.Menu_Description, strTempMenuCode, false, "1", SelectedLineNumber);

                        Session.SpecialtyItems.SpecialtyCode1stHalf = "";

                        currentCartItem = Session.cart.cartItems.Find(x => x.Line_Number == (SelectedLineNumber > 0 ? SelectedLineNumber : x.Line_Number));
                    }


                    itemSpecialityPizza = currentCartItem.itemSpecialtyPizzas.Find(x => x.Pizza_Part == "2");

                    if (itemSpecialityPizza != null)
                    {
                        strTempMenuCode = itemSpecialityPizza.Menu_Code;
                        strTempMenuDescription = currentCartItem.Menu_Description;
                        strTempSpecialtyCode = itemSpecialityPizza.Specialty_Pizza_Code;

                        //'Remove the specialty from the 2nd Half
                        SpecialtyItemChosen(currentCartItem.Menu_Description, strTempMenuCode, false, "2", SelectedLineNumber);

                        Session.SpecialtyItems.SpecialtyCode2ndHalf = "";

                        currentCartItem = Session.cart.cartItems.Find(x => x.Line_Number == (SelectedLineNumber > 0 ? SelectedLineNumber : x.Line_Number));
                    }
                }
            }

            pstrPizzaPart = strItemPart;

            if (pstrPizzaPart == "W")
            {
                Session.selectedMenuItem = Session.menuItems.Find(z => z.Menu_Code == strButtonTag);
                if (Session.selectedMenuItem == null)
                {
                    List<CatalogMenuItems> currentMenuItems = APILayer.GetMenuItems(strButtonTag, Session.selectedOrderType);
                    CartFunctions.RemoveItemsOrderTypeFromMenuItems(Session.selectedOrderType);
                    foreach (CatalogMenuItems catalogMenuItems in currentMenuItems)
                    {
                        if (!Session.menuItems.Any(z => z.Menu_Code == catalogMenuItems.Menu_Code)) Session.menuItems.Add(catalogMenuItems);
                    }
                    Session.selectedMenuItem = Session.menuItems.Find(z => z.Menu_Code == strButtonTag);
                }

                if (frmObj != null) ((frmOrder)frmObj).PopulateToppings(strButtonTag, currentSpecialtyPizza.Specialty_Pizza_Code);

                strPizzaHalves[0] = strButtonTag;
                strPizzaHalves[1] = strButtonTag;
            }
            else
            {
                if (blnSelected)
                {
                    blnSpecialtyPizzaOnHalf = true;
                    strPizzaHalves[Convert.ToInt32(pstrPizzaPart) - 1] = strButtonTag;
                }
                else
                {
                    blnSpecialtyPizzaOnHalf = false;
                    strPizzaHalves[Convert.ToInt32(pstrPizzaPart) - 1] = currentCartItem.Menu_Code;
                }


                if (Session.currentToppingCollection.pizzaToppings.Count > 0)
                {
                    for (index = Session.currentToppingCollection.pizzaToppings.Count - 1; index >= 0; index--)
                    {
                        if (Session.currentToppingCollection.pizzaToppings[index].ItemPart == pstrPizzaPart)
                            Session.currentToppingCollection.pizzaToppings.RemoveAt(index);
                    }
                }
            }



            if (blnSelected)
            {
                if (Session.ProcessingCombo)
                    currentCartItem.Menu_Item_Choosen = true;

                if (currentCartItem.itemOptionGroups != null && currentCartItem.itemOptionGroups.Count > 0)
                {
                    ItemOptionGroup itemOptionGroup = currentCartItem.itemOptionGroups.Find(x => x.Option_Group_Code == Session.currentToppingCollection.currentCatalogOptionGroups.Menu_Option_Group_Code);

                    if (itemOptionGroup != null)
                        itemOptionGroup.Option_Group_Complete = true;
                }

                if (pstrPizzaPart == "W")
                {
                    if (pstrOriginalPizzaMenuCode.Length == 0)
                    {
                        pstrOriginalPizzaMenuCode = currentCartItem.Menu_Code;
                        pstrOriginalPizzaMenuDescription = currentCartItem.Menu_Description;
                        pcurOriginalPizzaPrice = currentCartItem.Menu_Price;
                    }

                    if (currentSpecialtyPizza != null)
                    {
                        currentCartItem.Location_Code = Session.cart.cartHeader.LocationCode;
                        currentCartItem.Order_Date = Session.cart.cartHeader.Order_Date;
                        currentCartItem.Order_Number = Session.cart.cartHeader.Order_Number;

                        currentCartItem.Menu_Code = strButtonTag;

                        if (strPizzaHalves[0] == "")
                            strPizzaHalves[0] = strButtonTag;

                        if (strPizzaHalves[1] == "")
                            strPizzaHalves[1] = strButtonTag;

                        currentCartItem.Menu_Description = strButtonCaption;
                        currentCartItem.Menu_Price = currentSpecialtyPizza.Price;
                        currentCartItem.Menu_Price2 = currentSpecialtyPizza.Price2;
                        currentCartItem.Added_By = Session.CurrentEmployee.LoginDetail.EmployeeCode;
                        currentCartItem.Kitchen_Device_Count = currentSpecialtyPizza.Kitchen_Device_Count;
                        currentCartItem.Print_Nutritional_Label = Convert.ToBoolean(currentSpecialtyPizza.Print_Nutritional_Label);

                        if (currentCartItem.Kitchen_Device_Count > 0)
                            currentCartItem.Order_Line_Status_Code = 1;
                        else
                            currentCartItem.Order_Line_Status_Code = 2;

                        currentCartItem.Base_Price = currentCartItem.Menu_Price;
                        currentCartItem.Base_Price2 = currentCartItem.Menu_Price2;

                        currentCartItem.MenuItemType = currentSpecialtyPizza.MenuItemType;
                    }
                }
                else
                {
                    bool NonVegToppingExist = Session.cartToppings.Exists(x => x.MenuItemType == true);

                    CatalogSpecialtyPizzas OtherHalfSpecialtyPizza = null;
                    if (currentCartItem.itemSpecialtyPizzas != null && currentCartItem.itemSpecialtyPizzas.Count > 0)
                    {
                        string OtherHalfPizzaCode = String.Empty;
                        ItemSpecialtyPizza itemSpeciality = currentCartItem.itemSpecialtyPizzas.Find(x => x.Pizza_Part == (pstrPizzaPart == "1" ? "2" : "1"));
                        OtherHalfPizzaCode = itemSpeciality != null ? itemSpeciality.Menu_Code : String.Empty;
                        if (!String.IsNullOrEmpty(OtherHalfPizzaCode))
                            OtherHalfSpecialtyPizza = Session.SpecialtyPizzasList.Find(x => x.Menu_Code == OtherHalfPizzaCode);
                    }

                    if (currentSpecialtyPizza != null && OtherHalfSpecialtyPizza != null)
                        currentCartItem.MenuItemType = Convert.ToBoolean(currentSpecialtyPizza.MenuItemType) || Convert.ToBoolean(OtherHalfSpecialtyPizza.MenuItemType) || NonVegToppingExist;
                    else if (currentSpecialtyPizza != null)
                        currentCartItem.MenuItemType = Convert.ToBoolean(currentSpecialtyPizza.MenuItemType) || NonVegToppingExist;
                }


                if (currentCartItem.itemSpecialtyPizzas != null)
                {
                    for (index = currentCartItem.itemSpecialtyPizzas.Count - 1; index >= 0; index--)
                    {
                        if (currentCartItem.itemSpecialtyPizzas[index].Sequence == currentCartItem.Sequence && currentCartItem.itemSpecialtyPizzas[index].Pizza_Part == pstrPizzaPart)
                            currentCartItem.itemSpecialtyPizzas.RemoveAt(index);
                    }
                }

                ResetDefaults();

                if (currentCartItem.itemSpecialtyPizzas == null)
                    currentCartItem.itemSpecialtyPizzas = new List<ItemSpecialtyPizza>();

                string ItemOptionGroupCode = APILayer.GetMenuItemOptionGroups(strButtonTag);

                if (ItemOptionGroupCode != "")
                {
                    ItemSpecialtyPizza SpecialityPizza = new ItemSpecialtyPizza();

                    SpecialityPizza.CartId = currentCartItem.CartId;
                    SpecialityPizza.Location_Code = currentCartItem.Location_Code;
                    SpecialityPizza.Order_Date = currentCartItem.Order_Date;
                    SpecialityPizza.Order_Number = currentCartItem.Order_Number;
                    SpecialityPizza.Line_Number = currentCartItem.Line_Number;
                    SpecialityPizza.Sequence = currentCartItem.Sequence;
                    SpecialityPizza.Menu_Option_Group_Code = ItemOptionGroupCode;
                    SpecialityPizza.Pizza_Part = pstrPizzaPart;
                    SpecialityPizza.Specialty_Pizza_Code = currentSpecialtyPizza.Specialty_Pizza_Code;
                    SpecialityPizza.Added_By = currentCartItem.Added_By;
                    SpecialityPizza.Menu_Code = strButtonTag;

                    currentCartItem.itemSpecialtyPizzas.Add(SpecialityPizza);

                    if (pstrPizzaPart == "1")
                        Session.currentToppingCollection.MenuItemType1stHalf = Convert.ToBoolean(currentSpecialtyPizza.MenuItemType);
                    else if (pstrPizzaPart == "2")
                        Session.currentToppingCollection.MenuItemType2ndHalf = Convert.ToBoolean(currentSpecialtyPizza.MenuItemType);
                }


                List<CatalogOptionGroups> catalogOptionGroups = APILayer.GetOptionGroups(strButtonTag);

                if (catalogOptionGroups != null && catalogOptionGroups.Count > 0)
                {
                    CatalogOptionGroups currentCatalogOptionGroups = catalogOptionGroups[0]; //TO DO only one Option Group considered
                    if (currentCatalogOptionGroups.Topping_Group)
                    {
                        List<CatalogDefaultToppings> SpecialtyPizzaDefaultToppings = APILayer.GetDefaultToppings(strButtonTag, currentCatalogOptionGroups.Menu_Option_Group_Code);

                        if (SpecialtyPizzaDefaultToppings != null && SpecialtyPizzaDefaultToppings.Count > 0)
                        {
                            foreach (CatalogDefaultToppings specPizzaDefaultTopping in SpecialtyPizzaDefaultToppings)
                            {
                                int ButtonIndex = 0;
                                PizzaTopping currentPizzaTopping = Session.currentToppingCollection.pizzaToppings.Find(x => x.ButtonCaption == specPizzaDefaultTopping.Order_Description);
                                if (currentPizzaTopping != null)
                                    ButtonIndex = currentPizzaTopping.ButtonIndex;

                                if (ButtonIndex > 0)
                                {
                                    PizzaTopping pizzaTopping = new PizzaTopping();
                                    pizzaTopping.ButtonIndex = ButtonIndex;
                                    pizzaTopping.ButtonColor = UserFunctions.GetColorbyAmountCode(specPizzaDefaultTopping.Amount_Code);
                                    pizzaTopping.ItemPart = strItemPart;
                                    pizzaTopping.DefaultTopping = currentSpecialtyPizza.Specialty_Pizza_Code;
                                    pizzaTopping.ButtonCaption = specPizzaDefaultTopping.Order_Description;
                                    pizzaTopping.KitchenDisplayOrder = Convert.ToInt32(specPizzaDefaultTopping.Kitchen_Display_Order);
                                    pizzaTopping.ItemPartInt = strItemPart == "W" ? 0 : Convert.ToInt32(strItemPart);
                                    pizzaTopping.ToppingCode = currentPizzaTopping.ToppingCode;
                                    Session.currentToppingCollection.pizzaToppings.Add(pizzaTopping);

                                    if (frmObj != null) ((frmOrder)frmObj).SetToppingDefault(specPizzaDefaultTopping.Sub_Menu_Code, pizzaTopping.ButtonColor);
                                }
                            }
                        }
                    }
                }


                if (currentCartItem.itemOptions != null && currentCartItem.itemOptions.Count > 0)
                {
                    for (index = currentCartItem.itemOptions.Count - 1; index >= 0; index--)
                    {
                        if (currentCartItem.itemOptions[index].Sequence == currentCartItem.Sequence && currentCartItem.itemOptions[index].Menu_Option_Group_Code == Session.currentToppingCollection.currentCatalogOptionGroups.Menu_Option_Group_Code)
                            currentCartItem.itemOptions.RemoveAt(index);
                    }
                }

                if (Session.currentToppingCollection.pizzaToppings != null && Session.currentToppingCollection.pizzaToppings.Count > 0)
                {
                    Session.currentToppingCollection.pizzaToppings = Session.currentToppingCollection.pizzaToppings.OrderByDescending(x => x.ItemPartInt).ToList().OrderByDescending(x => x.KitchenDisplayOrder).ToList();

                    if (currentCartItem.itemOptions == null)
                        currentCartItem.itemOptions = new List<ItemOption>();

                    for (int i = Session.currentToppingCollection.pizzaToppings.Count - 1; i >= 0; i--)
                    {

                        ItemOption itemOption = new ItemOption();
                        itemOption.CartId = currentCartItem.CartId;
                        Topping ClickedTopping = Session.currentToppingCollection.currentToppings.Find(x => x.Order_Description == Session.currentToppingCollection.pizzaToppings[i].ButtonCaption);

                        if (Session.currentToppingCollection.pizzaToppings[i].ButtonColor == Session.ToppingSizeLightColor)
                            itemOption.Amount_Code = UserTypes.cstrLight_Code;
                        else if (Session.currentToppingCollection.pizzaToppings[i].ButtonColor == Session.ToppingSizeExtraColor)
                            itemOption.Amount_Code = UserTypes.cstrExtra_Code;
                        else if (Session.currentToppingCollection.pizzaToppings[i].ButtonColor == Session.ToppingSizeDoubleColor)
                            itemOption.Amount_Code = UserTypes.cstrDouble_Code;
                        else if (Session.currentToppingCollection.pizzaToppings[i].ButtonColor == Session.ToppingSizeTripleColor)
                            itemOption.Amount_Code = UserTypes.cstrTriple_Code;
                        else if (Session.currentToppingCollection.pizzaToppings[i].ButtonColor == Session.ToppingColor || Session.currentToppingCollection.pizzaToppings[i].ButtonColor == Color.Black || Session.currentToppingCollection.pizzaToppings[i].ButtonColor == Color.Gray)
                            itemOption.Amount_Code = "-";
                        else
                            itemOption.Amount_Code = "";

                        itemOption.Kitchen_Display_Order = Convert.ToInt32(ClickedTopping.Kitchen_Display_Order);
                        itemOption.Topping_group = Session.currentToppingCollection.currentCatalogOptionGroups.Topping_Group;
                        itemOption.Default_Topping = Session.currentToppingCollection.pizzaToppings[i].DefaultTopping;
                        itemOption.Pizza_Part = Session.currentToppingCollection.pizzaToppings[i].ItemPart;
                        itemOption.Description = ClickedTopping.Topping_Code;
                        itemOption.Size_Code = ClickedTopping.Size_Code;
                        itemOption.Menu_Description = ClickedTopping.Order_Description;
                        itemOption.Menu_Code = ClickedTopping.Menu_Code;
                        itemOption.Menu_Option_Group_Code = Session.currentToppingCollection.currentCatalogOptionGroups.Menu_Option_Group_Code;
                        itemOption.Index = Session.currentToppingCollection.pizzaToppings[i].ButtonIndex;
                        itemOption.Line_Number = currentCartItem.Line_Number;
                        itemOption.Location_Code = currentCartItem.Location_Code;
                        itemOption.Order_Date = currentCartItem.Order_Date;
                        itemOption.Order_Number = currentCartItem.Order_Number;
                        itemOption.Sequence = currentCartItem.Sequence;
                        itemOption.Added_By = Session.CurrentEmployee.LoginDetail.EmployeeCode;

                        currentCartItem.itemOptions.Add(itemOption);
                    }
                }
            }
            else if (pstrPizzaPart == "W")
            {
                currentCartItem.Location_Code = Session.cart.cartHeader.LocationCode;
                currentCartItem.Order_Date = Session.cart.cartHeader.Order_Date;
                currentCartItem.Order_Date = Session.cart.cartHeader.Order_Date;
                currentCartItem.Menu_Code = currentCartItem.Orig_Menu_Code;

                CatalogMenuItems catalogMenuItems = Session.menuItems.Find(x => x.Menu_Code == currentCartItem.Menu_Code);
                CatalogMenuItemSizes catalogMenuItemSizes = Session.menuItemSizes.Find(x => x.Size_Code == currentCartItem.Size_Code && x.Menu_Code == currentCartItem.Menu_Code);

                if (catalogMenuItemSizes != null && catalogMenuItems != null)
                {
                    currentCartItem.Menu_Description = catalogMenuItems.Order_Description;
                    currentCartItem.Menu_Price = catalogMenuItemSizes.Price;
                    currentCartItem.Menu_Price2 = catalogMenuItemSizes.Price2;
                    currentCartItem.MenuItemType = catalogMenuItems.MenuItemType;
                }

                currentCartItem.Added_By = Session.CurrentEmployee.LoginDetail.EmployeeCode;

                if (currentCartItem.itemSpecialtyPizzas != null && currentCartItem.itemSpecialtyPizzas.Count > 0)
                {
                    for (index = currentCartItem.itemSpecialtyPizzas.Count - 1; index >= 0; index--)
                    {
                        if (currentCartItem.itemSpecialtyPizzas[index].Sequence == currentCartItem.Sequence)
                            currentCartItem.itemSpecialtyPizzas.RemoveAt(index);
                    }
                }

                currentCartItem.Base_Price = currentCartItem.Menu_Price;
                currentCartItem.Base_Price2 = currentCartItem.Menu_Price2;

                if (currentCartItem.itemOptions != null && currentCartItem.itemOptions.Count > 0)
                {
                    for (index = currentCartItem.itemOptions.Count - 1; index >= 0; index--)
                    {
                        if (currentCartItem.itemOptions[index].Sequence == currentCartItem.Sequence && currentCartItem.itemOptions[index].Menu_Option_Group_Code == Session.currentToppingCollection.currentCatalogOptionGroups.Menu_Option_Group_Code && currentCartItem.itemOptions[index].Pizza_Part == pstrPizzaPart)
                            currentCartItem.itemOptions.RemoveAt(index);
                    }
                }

                Session.selectedMenuItem = Session.menuItems.Find(z => z.Menu_Code == currentCartItem.Menu_Code);
                if (Session.selectedMenuItem == null)
                {
                    List<CatalogMenuItems> currentMenuItems = APILayer.GetMenuItems(currentCartItem.Menu_Code, Session.selectedOrderType);
                    CartFunctions.RemoveItemsOrderTypeFromMenuItems(Session.selectedOrderType);
                    foreach (CatalogMenuItems catalogMenuItems1 in currentMenuItems)
                    {
                        if (!Session.menuItems.Any(z => z.Menu_Code == catalogMenuItems1.Menu_Code)) Session.menuItems.Add(catalogMenuItems1);
                    }
                    Session.selectedMenuItem = Session.menuItems.Find(z => z.Menu_Code == currentCartItem.Menu_Code);
                }
                if (frmObj != null) ((frmOrder)frmObj).PopulateToppings(currentCartItem.Menu_Code, "");

                if (frmObj != null) ((frmOrder)frmObj).ResetToppings();
                ResetDefaults();
            }
            else
            {
                if (currentCartItem.itemSpecialtyPizzas != null && currentCartItem.itemSpecialtyPizzas.Count > 0)
                {
                    for (index = currentCartItem.itemSpecialtyPizzas.Count - 1; index >= 0; index--)
                    {
                        if (currentCartItem.itemSpecialtyPizzas[index].Sequence == currentCartItem.Sequence && currentCartItem.itemSpecialtyPizzas[index].Pizza_Part == pstrPizzaPart)
                            currentCartItem.itemSpecialtyPizzas.RemoveAt(index);
                    }
                }

                if (currentCartItem.itemOptions != null && currentCartItem.itemOptions.Count > 0)
                {
                    for (index = currentCartItem.itemOptions.Count - 1; index >= 0; index--)
                    {
                        if (currentCartItem.itemOptions[index].Sequence == currentCartItem.Sequence && currentCartItem.itemOptions[index].Menu_Option_Group_Code == Session.currentToppingCollection.currentCatalogOptionGroups.Menu_Option_Group_Code && currentCartItem.itemOptions[index].Pizza_Part == pstrPizzaPart)
                            currentCartItem.itemOptions.RemoveAt(index);
                    }
                }

                ResetItemPartDefaults(pstrPizzaPart);
            }

            currentCartItem.Topping_String = Create_Topping_Strings(Session.currentToppingCollection.pizzaToppings);

            HandleToppings(ref currentCartItem);

            currentCartItem.Action = "M";
            cartLocal.cartItems.Add(currentCartItem);
            cartLocal.cartHeader = Session.cart.cartHeader;
            CartFunctions.UpdateCustomer(cartLocal);
            Session.cart = APILayer.Add2Cart(cartLocal);

        }

        public static void ResetDefaults()
        {
            if (Session.currentToppingCollection != null && Session.currentToppingCollection.currentToppings != null && Session.currentToppingCollection.currentToppings.Count > 0)
            {
                for (int i = 0; i <= Session.currentToppingCollection.currentToppings.Count - 1; i++)
                {
                    Session.currentToppingCollection.currentToppings[i].FirstHalfDefault = false;
                    Session.currentToppingCollection.currentToppings[i].FirstHalfDefaultAmount = "";
                    Session.currentToppingCollection.currentToppings[i].SecondHalfDefault = false;
                    Session.currentToppingCollection.currentToppings[i].SecondHalfDefaultAmount = "";
                    Session.currentToppingCollection.currentToppings[i].WholeDefault = false;
                    Session.currentToppingCollection.currentToppings[i].WholeDefaultAmount = "";
                }
            }
        }

        public static void ResetItemPartDefaults(string strItemPart)
        {
            switch (strItemPart)
            {
                case "1":
                    for (int i = 0; i <= Session.currentToppingCollection.currentToppings.Count - 1; i++)
                    {
                        Session.currentToppingCollection.currentToppings[i].FirstHalfDefault = false;
                        Session.currentToppingCollection.currentToppings[i].FirstHalfDefaultAmount = "";
                    }
                    break;
                case "2":
                    for (int i = 0; i <= Session.currentToppingCollection.currentToppings.Count - 1; i++)
                    {
                        Session.currentToppingCollection.currentToppings[i].SecondHalfDefault = false;
                        Session.currentToppingCollection.currentToppings[i].SecondHalfDefaultAmount = "";
                    }
                    break;
                case "W":
                    for (int i = 0; i <= Session.currentToppingCollection.currentToppings.Count - 1; i++)
                    {
                        Session.currentToppingCollection.currentToppings[i].WholeDefault = false;
                        Session.currentToppingCollection.currentToppings[i].WholeDefaultAmount = "";
                    }
                    break;
            }
        }

        private static int GetFirstAvailableItemIndex(Cart originalCart, List<int> Indices)
        {

            for (int i = 0; i <= originalCart.cartItems.Count - 1; i++)
            {
                if (originalCart.cartItems[i].Menu_Description.Trim() != APILayer.GetCatalogText(LanguageConstant.cintNA).Trim())
                {
                    if ((Indices != null && Indices.Count > 0 && !Indices.Contains(i)) || (Indices == null || Indices.Count <= 0))
                    {
                        return i;
                    }
                }
            }
            return -1;
        }

        public static bool IsMoreThenOneSizes(string Menu_Code)
        {
            List<CatalogMenuItemSizes> menuItemSizes = Session.menuItemSizes.FindAll(x => x.Menu_Code == Menu_Code);
            if (menuItemSizes != null && menuItemSizes.Count > 0)
            {
                if (menuItemSizes.Count > 1)
                    return true;
                else
                    return false;
            }
            else
                return false;

        }

        public static void SourceChange()
        {
            if (Session.cart != null && Session.cart.cartHeader.CartId != null && Session.cart.cartHeader.CartId != "")
            {
                Session.cart.cartHeader.ROI_Customer = Session.selectedSourceName;
                Cart cartLocal = (new Cart().GetCart());
                cartLocal.cartHeader = Session.cart.cartHeader;
                cartLocal.cartHeader.CartId = Session.cart.cartHeader.CartId;
                cartLocal.cartHeader.Action = "M";
                CartFunctions.UpdateCustomer(cartLocal);
                Session.cart = APILayer.Add2Cart(cartLocal);
            }
        }    

        public static void UpdateMenuStatus()
        {
            List<CatalogMenuItemStatus> catalogMenuItemStatus = APILayer.GetMenuItemStatus();
            List<CatalogMenuItemSizesStatus> catalogMenuItemSizesStatus = APILayer.GetMenuItemSizesStatus();

            if(Session.AllCatalogMenuItems != null && Session.AllCatalogMenuItems.Count>0 && catalogMenuItemStatus != null && catalogMenuItemStatus.Count>0)
            {
                foreach(CatalogMenuItemStatus menuStatus in catalogMenuItemStatus)
                {
                    foreach (var mc in Session.AllCatalogMenuItems.Where(x => x.Menu_Code == menuStatus.Menu_Code))
                        mc.EnabledInv = menuStatus.IsEnabled;
                }
            }

            if (Session.AllCatalogMenuItemSizes != null && Session.AllCatalogMenuItemSizes.Count > 0 && catalogMenuItemSizesStatus != null && catalogMenuItemSizesStatus.Count > 0)
            {
                foreach (CatalogMenuItemSizesStatus menuSizeStatus in catalogMenuItemSizesStatus)
                {
                    foreach (var mc in Session.AllCatalogMenuItemSizes.Where(x => x.Menu_Code == menuSizeStatus.Menu_Code && x.Size_Code == menuSizeStatus.Size_Code))
                        mc.EnabledInv = menuSizeStatus.IsEnabled;
                }
            }
        }

        public static List<int> OutofStockItemsFromCart()
        {
            List<CatalogMenuItemStatus> catalogMenuItemStatus = APILayer.GetMenuItemStatus();
            List<CatalogMenuItemSizesStatus> catalogMenuItemSizesStatus = APILayer.GetMenuItemSizesStatus();

            if (Session.cart == null || Session.cart.cartItems.Count <= 0) return (new List<int>());

            List<int> Indexes = new List<int>();

            
            if (catalogMenuItemStatus == null || catalogMenuItemStatus.Count <= 0) return (new List<int>());
                        
            for (int i = 0; i <= Session.cart.cartItems.Count - 1; i++)
            {
                if (Session.cart.cartItems[i].Combo_Group <= 0)
                {
                    int index = -1;
                    index = catalogMenuItemStatus.FindIndex(
                        delegate (CatalogMenuItemStatus mi)
                        {
                            return mi.Menu_Code == Session.cart.cartItems[i].Menu_Code &&  mi.IsEnabled == false;
                        }
                        );

                    if (index == -1)
                    {
                        List<CatalogMenuItemSizesStatus> menu_Item_Sizes_Status = catalogMenuItemSizesStatus.FindAll(x => x.Menu_Code == Session.cart.cartItems[i].Menu_Code);
                        index = menu_Item_Sizes_Status.FindIndex(
                        delegate (CatalogMenuItemSizesStatus mis)
                        {
                            return mis.Menu_Code == Session.cart.cartItems[i].Menu_Code && mis.Size_Code == Session.cart.cartItems[i].Size_Code && mis.IsEnabled == false;
                        }
                        );
                    }

                    if (index > -1) Indexes.Add(i);
                }

            }

            //TO Do Combo
            //if (Session.originalcart.itemCombos != null && Session.originalcart.itemCombos.Count > 0)
            //{
            //    for (int j = 0; j <= Session.originalcart.itemCombos.Count - 1; j++)
            //    {
            //        int indexCombo = -1;
            //        indexCombo = menu_Items.FindIndex(
            //            delegate (CatalogMenuItems mi)
            //            {
            //                return mi.Menu_Code == Session.originalcart.itemCombos[j].Combo_Menu_Code && (mi.Enabled == false || mi.EnabledInv == false);
            //            }
            //            );

            //        if (indexCombo == -1)
            //        {
            //            List<CatalogMenuItemSizes> menu_Item_Sizes = APILayer.GetMenuItemSizes(Session.originalcart.itemCombos[j].Combo_Menu_Code, Session.cart.cartHeader.Order_Type_Code);
            //            indexCombo = menu_Item_Sizes.FindIndex(
            //            delegate (CatalogMenuItemSizes mis)
            //            {
            //                return mis.Menu_Code == Session.originalcart.itemCombos[j].Combo_Menu_Code && mis.Size_Code == Session.originalcart.itemCombos[j].Combo_Size_Code && (mis.Enabled == false || mis.EnabledInv == false);
            //            }
            //            );
            //        }

            //        if (indexCombo > -1)
            //        {
            //            //ComboIndices.Add(j);

            //            for (int k = 0; k <= Session.originalcart.cartItems.Count - 1; k++)
            //            {
            //                if (Session.originalcart.itemCombos[j].Combo_Menu_Code == Session.originalcart.cartItems[k].Combo_Menu_Code && Session.originalcart.itemCombos[j].Combo_Size_Code == Session.originalcart.cartItems[k].Combo_Size_Code)
            //                    Indexes.Add(k);
            //            }

            //        }
            //    }
            //}

            return Indexes;
		}

        public static bool IsItemwiseUpsellAvailable(int selectedLineNumber, ref ItemwiseUpsellDatawithType itemwiseUpsellDatawithType)
        {
            if (Session.UpsellPopupCount >= Convert.ToInt32(Convert.ToString(SystemSettings.GetSettingValue("ItemwiseUpsellMaxPromptLimit", Session._LocationCode))))
                return false;

            if (Session.cart != null && Session.cart.cartItems != null && Session.cart.cartItems.Count > 0)
            {
                string Menu_Code = string.Empty;
                string Size_Code = string.Empty;
                int LocalLineNumber = UserFunctions.ActualLineNumber(Session.cart, selectedLineNumber);

                if (Session.itemwiseUpsellPromptMatrix.FindIndex(x => x.LineNumber == LocalLineNumber) >= 0) return false;

                if(Session.LineNumbersFromHistory != null && Session.LineNumbersFromHistory.Count>0)
                {
                    if (Session.LineNumbersFromHistory.IndexOf(LocalLineNumber) >= 0) return false;
                }

                CartItem cartItem = Session.cart.cartItems.Find(x => x.Line_Number == LocalLineNumber);

                if (cartItem.Combo_Group > 0)
                {
                    Menu_Code = cartItem.Combo_Menu_Code;
                    Size_Code = cartItem.Combo_Size_Code;
                }
                else
                {
                    Menu_Code = cartItem.Menu_Code;
                    Size_Code = cartItem.Size_Code;
                }

                if (!cartItem.Description.Contains("-->Size "))
                {
                    string LastAction = "BOTH";
                    int Priority = 1;
                    ItemwiseUpsellPromptRow itemwiseUpsellPromptRow = null;
                    List<ItemwiseUpsellPromptRow> itemwiseUpsellPromptRows = Session.itemwiseUpsellPromptMatrix.FindAll(z => z.Menu_Code == Menu_Code);
                    if (itemwiseUpsellPromptRows != null && itemwiseUpsellPromptRows.Count > 0)
                        itemwiseUpsellPromptRow = itemwiseUpsellPromptRows.OrderByDescending(z => z.Priority).First();

                    if (itemwiseUpsellPromptRow != null)
                    {
                        LastAction = itemwiseUpsellPromptRow.Action;
                        Priority = itemwiseUpsellPromptRow.Priority + 1;
                    }

                TryAgain: itemwiseUpsellDatawithType = GetUpsellDatawithType(Menu_Code, Priority, LastAction, Size_Code);
                    itemwiseUpsellDatawithType.LineNumber = LocalLineNumber;

                    if (itemwiseUpsellDatawithType != null && !String.IsNullOrEmpty(itemwiseUpsellDatawithType.UpsellType))
                    {
                        string UpsellType = itemwiseUpsellDatawithType.UpsellType;
                        if (Session.catalogUpsellData.catalogUpsellExcludeItem != null && Session.catalogUpsellData.catalogUpsellExcludeItem.Count > 0)
                        {
                            List<CatalogUpsellExcludeItem> catalogUpsellExcludeItems = Session.catalogUpsellData.catalogUpsellExcludeItem.FindAll(x => x.Menu_Code == Menu_Code && x.Upsell_Type == UpsellType);
                            if (catalogUpsellExcludeItems != null && catalogUpsellExcludeItems.Count > 0)
                            {
                                foreach (CatalogUpsellExcludeItem catalogUpsellExcludeItem in catalogUpsellExcludeItems)
                                {
                                    if (!String.IsNullOrEmpty(catalogUpsellExcludeItem.Exclude_Category_Code))
                                    {
                                        if (Session.cart.cartItems.FindIndex(z => z.Menu_Category_Code == catalogUpsellExcludeItem.Exclude_Category_Code && z.Line_Number != LocalLineNumber) > -1)
                                        {
                                            itemwiseUpsellDatawithType = null;
                                            Priority = Priority + 1;

                                            if (IsHigherPriorityExists(Menu_Code, Priority))
                                                goto TryAgain;
                                            else
                                                return false;
                                        }
                                    }
                                    else if (!String.IsNullOrEmpty(catalogUpsellExcludeItem.Exclude_Menu_Code))
                                    {
                                        if (Session.cart.cartItems.FindIndex(z => z.Menu_Code == catalogUpsellExcludeItem.Exclude_Menu_Code && z.Line_Number != LocalLineNumber) > -1)
                                        {
                                            itemwiseUpsellDatawithType = null;
                                            Priority = Priority + 1;

                                            if (IsHigherPriorityExists(Menu_Code, Priority))
                                                goto TryAgain;
                                            else
                                                return false;
                                        }
                                    }

                                }
                            }
                        }

                        UpdateDecriptions(ref itemwiseUpsellDatawithType);
                        return true;
                    }
                    else
                    {
                        itemwiseUpsellDatawithType = null;
                        Priority = Priority + 1;

                        if (IsHigherPriorityExists(Menu_Code, Priority))
                            goto TryAgain;
                        else
                            return false;
                    }
                }

            }

            return false;

        }


        private static ItemwiseUpsellDatawithType GetUpsellDatawithType(string Menu_Code, int Priority, string LastAction, string Size_Code)
        {
            ItemwiseUpsellDatawithType itemwiseUpsellDatawithType = new ItemwiseUpsellDatawithType();
            CatalogItemwiseUpsell catalogItemwiseUpsell = Session.catalogUpsellData.catalogItemwiseUpsell.Find(x => x.Menu_Code == Menu_Code && x.Priority == Priority);

            if (catalogItemwiseUpsell != null)
            {
                if (LastAction != "BOTH" && catalogItemwiseUpsell.Action != "BOTH")
                {
                    if (catalogItemwiseUpsell.Action != LastAction)
                        catalogItemwiseUpsell = null;
                }

                if (catalogItemwiseUpsell != null)
                {
                    switch (catalogItemwiseUpsell.Upsell_Type)
                    {
                        case "NEWITEM":
                            List<CatalogUpsellNewItem> catalogUpsellNewItemList = Session.catalogUpsellData.catalogUpsellNewItem.FindAll(z => z.Upsell_RuleId == catalogItemwiseUpsell.Upsell_RuleId);
                            if (catalogUpsellNewItemList != null && catalogUpsellNewItemList.Count > 0)
                            {
                                itemwiseUpsellDatawithType.UpsellType = catalogItemwiseUpsell.Upsell_Type;
                                itemwiseUpsellDatawithType.AttributeList = new List<Models.Cart.attribute>();
                                foreach (CatalogUpsellNewItem catalogUpsellNewItem in catalogUpsellNewItemList)
                                    itemwiseUpsellDatawithType.AttributeList.Add(new Models.Cart.attribute() { Code = catalogUpsellNewItem.New_Menu_Code });
                            }
                            break;
                        case "SIZECHANGE":
                            List<CatalogUpsellSizeChange> catalogUpsellSizeChangeList = Session.catalogUpsellData.catalogUpsellSizeChange.FindAll(z => z.Upsell_RuleId == catalogItemwiseUpsell.Upsell_RuleId && z.Current_Size_Code == Size_Code);
                            if (catalogUpsellSizeChangeList != null && catalogUpsellSizeChangeList.Count > 0)
                            {
                                itemwiseUpsellDatawithType.UpsellType = catalogItemwiseUpsell.Upsell_Type;
                                itemwiseUpsellDatawithType.AttributeList = new List<Models.Cart.attribute>();
                                foreach (CatalogUpsellSizeChange catalogUpsellSizeChange in catalogUpsellSizeChangeList)
                                    itemwiseUpsellDatawithType.AttributeList.Add(new Models.Cart.attribute() { Code = catalogUpsellSizeChange.New_Size_Code });
                            }
                            break;
                        case "ADDTOPPING":
                            List<CatalogUpsellAddTopping> catalogUpsellAddToppingList = Session.catalogUpsellData.catalogUpsellAddTopping.FindAll(z => z.Upsell_RuleId == catalogItemwiseUpsell.Upsell_RuleId);
                            if (catalogUpsellAddToppingList != null && catalogUpsellAddToppingList.Count > 0)
                            {
                                itemwiseUpsellDatawithType.UpsellType = catalogItemwiseUpsell.Upsell_Type;
                                itemwiseUpsellDatawithType.AttributeList = new List<Models.Cart.attribute>();
                                foreach (CatalogUpsellAddTopping catalogUpsellAddTopping in catalogUpsellAddToppingList)
                                    itemwiseUpsellDatawithType.AttributeList.Add(new Models.Cart.attribute() { Code = catalogUpsellAddTopping.Topping_Code });
                            }
                            break;
                        case "CONVERTTOCOMBO":
                            List<CatalogUpsellCombo> catalogUpsellComboList = Session.catalogUpsellData.catalogUpsellCombo.FindAll(z => z.Upsell_RuleId == catalogItemwiseUpsell.Upsell_RuleId);
                            if (catalogUpsellComboList != null && catalogUpsellComboList.Count > 0)
                            {
                                itemwiseUpsellDatawithType.AttributeList = new List<Models.Cart.attribute>();
                                foreach (CatalogUpsellCombo catalogUpsellCombo in catalogUpsellComboList)
                                {
                                    if (IsCurrentMenuItemExistInCombo(Menu_Code, catalogUpsellCombo.Combo_Menu_Code))
                                        itemwiseUpsellDatawithType.AttributeList.Add(new Models.Cart.attribute() { Code = catalogUpsellCombo.Combo_Menu_Code });
                                }

                                if (itemwiseUpsellDatawithType.AttributeList.Count > 0)
                                    itemwiseUpsellDatawithType.UpsellType = catalogItemwiseUpsell.Upsell_Type;
                            }
                            break;
                    }
                }

                itemwiseUpsellDatawithType.MenuCode = Menu_Code;
                itemwiseUpsellDatawithType.Priority = Priority;
            }

            return itemwiseUpsellDatawithType;
        }

        public static string GetMessageTextForUpsell(string UpsellType)
        {
            string Text = string.Empty;
            switch (UpsellType)
            {
                case "NEWITEM":
                    Text = MessageConstant.UpsellPromptNewItem;
                    break;
                case "SIZECHANGE":
                    Text = MessageConstant.UpsellPromptSizeChange;
                    break;
                case "ADDTOPPING":
                    Text = MessageConstant.UpsellPromptAddTopping;
                    break;
                case "CONVERTTOCOMBO":
                    Text = MessageConstant.UpsellPromptConvertToCombo;
                    break;
            }
            return Text;
        }

        public static void RecordUpsellPrompts(string Menu_Code, int Priority, string Action, int LineNumber)
        {
            Session.UpsellPopupCount++;

            if (Session.itemwiseUpsellPromptMatrix == null)
                Session.itemwiseUpsellPromptMatrix = new List<ItemwiseUpsellPromptRow>();

            Session.itemwiseUpsellPromptMatrix.Add(new ItemwiseUpsellPromptRow() { Menu_Code = Menu_Code, Priority = Priority, Action = Action, LineNumber = LineNumber });
        }

        private static bool IsHigherPriorityExists(string Menu_Code, int Priority)
        {
            CatalogItemwiseUpsell catalogItemwiseUpsell = Session.catalogUpsellData.catalogItemwiseUpsell.Find(x => x.Menu_Code == Menu_Code && x.Priority == Priority);
            if (catalogItemwiseUpsell != null)
                return true;
            else
                return false;
        }

        private static void UpdateDecriptions(ref ItemwiseUpsellDatawithType itemwiseUpsellDatawithType)
        {
            if (itemwiseUpsellDatawithType.AttributeList != null && itemwiseUpsellDatawithType.AttributeList.Count > 0)
            {
                string MenuCode = itemwiseUpsellDatawithType.MenuCode;
                for (int i = 0; i <= itemwiseUpsellDatawithType.AttributeList.Count - 1; i++)
                {
                    attribute attribute = itemwiseUpsellDatawithType.AttributeList[i];
                    attribute.IsEnabled = true;

                    switch (itemwiseUpsellDatawithType.UpsellType)
                    {
                        case "NEWITEM":
                        case "CONVERTTOCOMBO":
                            CatalogMenuItems catalogMenuItems = Session.AllCatalogMenuItems.Find(x => x.Menu_Code == attribute.Code && x.Order_Type_Code == Session.selectedOrderType);
                            if (catalogMenuItems != null)
                            {
                                attribute.Description = catalogMenuItems.Order_Description;
                                attribute.IsEnabled = catalogMenuItems.Enabled && catalogMenuItems.EnabledInv;
                            }
                            break;
                        case "SIZECHANGE":
                            CatalogMenuItemSizes catalogMenuItemSizes = Session.AllCatalogMenuItemSizes.Find(x => x.Menu_Code == MenuCode && x.Size_Code == attribute.Code && x.Order_Type_Code == Session.selectedOrderType);
                            if (catalogMenuItemSizes != null)
                            {
                                attribute.Description = catalogMenuItemSizes.Description;
                                attribute.IsEnabled = catalogMenuItemSizes.Enabled && catalogMenuItemSizes.EnabledInv;
                            }
                            break;
                        case "ADDTOPPING":
                            List<CatalogOptionGroups> catalogOptionGroups = APILayer.GetOptionGroups(MenuCode);
                            if (catalogOptionGroups == null || catalogOptionGroups.Count <= 0) return;
                            CatalogOptionGroups currentCatalogOptionGroups = catalogOptionGroups[0];
                            List<CatalogToppings> catalogToppings = APILayer.GetToppings(MenuCode, currentCatalogOptionGroups.Menu_Option_Group_Code);
                            if(catalogToppings!=null && catalogToppings.Count>0)
                            {
                                CatalogToppings topping = catalogToppings.Find(x => x.Menu_Code == attribute.Code);
                                if (topping != null)
                                    attribute.Description = topping.Order_Description;
                            }
                            break;
                    }
                }
            }
        }

        private static bool IsCurrentMenuItemExistInCombo(string Menu_Code, string Combo_Menu_Code )
        {
            CatalogMenuItemSizes catalogMenuItemSizes = Session.AllCatalogMenuItemSizes.FirstOrDefault(x => x.Menu_Code == Combo_Menu_Code);

            if(catalogMenuItemSizes!= null)
            {
                List<CatalogPOSComboMealItems> catalogPOSComboMealItems = APILayer.GetComboMealItems(Combo_Menu_Code, catalogMenuItemSizes.Size_Code);

                if(catalogPOSComboMealItems != null && catalogPOSComboMealItems.Count> 0)
                {
                    if (catalogPOSComboMealItems.FindIndex(z => z.Menu_Code == Menu_Code) > -1)
                        return true;
                }
            }

            return false;

        }

        public  static void LogItemwiseUpsellHistory(ItemwiseUpsellDatawithType itemwiseUpsellDatawithType, string Action, string SelectedAttributeCode)
        {
            if (Session.itemwiseUpsellHistory == null) Session.itemwiseUpsellHistory = new List<ItemwiseUpsellHistory>();

            CartItem cartItem = Session.cart.cartItems.Find(x => x.Line_Number == itemwiseUpsellDatawithType.LineNumber);

            foreach (attribute attribute in itemwiseUpsellDatawithType.AttributeList)
            {
                if (Action == "NO" || ( Action == "YES" && SelectedAttributeCode == attribute.Code))
                {
                    ItemwiseUpsellHistory history = new ItemwiseUpsellHistory();
                    history.Location_Code = Session._LocationCode;
                    history.Upsell_Type = itemwiseUpsellDatawithType.UpsellType;
                    history.Line_Number = itemwiseUpsellDatawithType.LineNumber;
                    history.Menu_Code = itemwiseUpsellDatawithType.MenuCode;
                    history.Size_Code = cartItem.Size_Code;
                    history.Price = cartItem.Order_Line_Total;
                    history.New_Line_Number = itemwiseUpsellDatawithType.UpsellType == "NEWITEM" || itemwiseUpsellDatawithType.UpsellType == "CONVERTTOCOMBO" ? itemwiseUpsellDatawithType.LineNumber + 1 : itemwiseUpsellDatawithType.LineNumber;
                    history.New_Menu_Code = itemwiseUpsellDatawithType.UpsellType == "NEWITEM" || itemwiseUpsellDatawithType.UpsellType == "CONVERTTOCOMBO" ? attribute.Code : itemwiseUpsellDatawithType.MenuCode;
                    history.New_Size_Code = itemwiseUpsellDatawithType.UpsellType == "SIZECHANGE" ? attribute.Code : "";
                    history.Topping_Code = itemwiseUpsellDatawithType.UpsellType == "ADDTOPPING" ? attribute.Code : "";
                    history.Action = Action;
                    history.Added_By = Session.CurrentEmployee.LoginDetail.EmployeeCode;

                    Session.itemwiseUpsellHistory.Add(history);
                }
            }
        }


    }
}
