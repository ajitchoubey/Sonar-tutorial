using Jublfood.AppLogger;
using JublFood.POS.App.API;
using JublFood.POS.App.Cache;
using JublFood.POS.App.Class;
using JublFood.POS.App.Models;
using JublFood.POS.App.Models.Cart;
using JublFood.POS.App.Models.Employee;
using JublFood.POS.App.Models.Order;
using JublFood.POS.App.Models.Catalog;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Threading;

namespace JublFood.POS.App.BusinessLayer
{
    public static class OrderCompleteFunctions
    {

        public static bool OrderComplete(bool blnCancel = false)
        {
            bool blnPrintOnDemandReceipt = false;
            bool blnPrintOnDemandLabel = false;
            bool blnSaveSuccessful = false;
            bool blnLoginSuccessful = false;
            bool blnModifyingPaymentChange = false;
            DateTime dtmSaved_Time = DateTime.MinValue;
            bool blnRecalcLeadTime = false;
            decimal curAmountTendered = 0;
            decimal curChangeDue = 0;
            EmployeeResult oldLoginEmployee;

            OrderRequest orderRequest = new OrderRequest();
            //SystemSettings.LoadSettings(Session._LocationCode);
            DateTime serverDateTime = Settings.Settings.GetServerDateTime();

            try
            {
                //blnPrintOnDemandReceipt = VerifyPrintOnDemandReceipt(True) //TO DO Printing Part
                ///blnPrintOnDemandLabel = VerifyPrintOnDemandLabel(True)

                if (string.IsNullOrEmpty(Session.CurrentEmployee.LoginDetail.EmployeeCode))
                {
                    Logger.Trace("INFO", "S-12.7 PaymentComplete : ProcessPayments is success", null, false);
                    while (string.IsNullOrEmpty(Session.CurrentEmployee.LoginDetail.EmployeeCode))
                    {
                        frmLogin frm = new frmLogin();
                        frm.SpecialAccess = false;
                        frm.RequirePassword = false;
                        frm.ShowDialog();
                    }
                }

                #region training Mode
                if (!blnCancel)
                {
                    Logger.Trace("INFO", "S-12.8 PaymentComplete : ProcessPayments is success", null, false);
                    if (SystemSettings.settings.pblnTrainingMode)
                    {
                        Logger.Trace("INFO", "S-12.9 PaymentComplete : ProcessPayments is success", null, false);
                        DialogResult result = CustomMessageBox.Show(APILayer.GetCatalogText(LanguageConstant.cintMSGTrainingOrder), CustomMessageBox.Buttons.YesNo);//TO DO FOR TEXT MESSAGE
                        if (result == DialogResult.Yes)
                            blnCancel = true;
                        else
                        {
                            if (!string.IsNullOrEmpty(Session.CurrentEmployee.LoginDetail.EmployeeCode) && Session.CurrentEmployee.LoginDetail.blnRealOrder)
                            {
                                if (SystemSettings.settings.pblnRequirePasswordForSpecialAccess)
                                {
                                    if (EmployeeFunctions.MatchEmployeePassword())
                                        blnLoginSuccessful = true;
                                    else
                                        blnLoginSuccessful = false;
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
                                    if (string.IsNullOrEmpty(Session.CurrentEmployee.LoginDetail.EmployeeCode) && Session.CurrentEmployee.LoginDetail.blnRealOrder)
                                        blnLoginSuccessful = true;
                                    else
                                    {
                                        blnLoginSuccessful = false;
                                        CustomMessageBox.Show(APILayer.GetCatalogText(LanguageConstant.cintMSGInsufficientPrivileges), CustomMessageBox.Buttons.OK);
                                    }
                                }
                                else
                                    blnLoginSuccessful = false;

                                Session.CurrentEmployee = oldLoginEmployee;
                            }
                            if (blnLoginSuccessful)
                                blnCancel = false;
                            else
                                blnCancel = true;
                        }
                    }

                }
                #endregion

                if (!blnCancel)
                {
                    Logger.Trace("INFO", "S-13 OrderComplete ", null, false);
                    PaymentFunctions.SetPayNowFlag(ref curAmountTendered, ref curChangeDue);

                    if (!Session.pblnModifyingOrder)
                    {
                        Session.cart.cartHeader.Order_Status_Code = 1;
                        blnModifyingPaymentChange = false;
                    }
                    else
                    {
                        if (Session.originalcart.cartHeader.Order_Number > 0)
                        {
                            if (Session.originalcart.cartHeader.Pay_Now != Session.cart.cartHeader.Pay_Now
                                || Session.originalcart.cartHeader.Final_Total != Session.cart.cartHeader.Final_Total
                                && Session.cart.cartHeader.Pay_Now)
                            {
                                blnModifyingPaymentChange = true;
                            }
                            else
                            {
                                if (Session.originalresponsePayment.payment.Count > 0)
                                {
                                    if (Session.cart.orderPayments.Count != Session.originalresponsePayment.payment.Count && Session.cart.cartHeader.Pay_Now)
                                        blnModifyingPaymentChange = true;
                                }
                                else
                                {
                                    if (Session.cart.cartHeader.Pay_Now)
                                        blnModifyingPaymentChange = true;
                                    else
                                        blnModifyingPaymentChange = false;
                                }
                            }
                        }
                        else
                            blnModifyingPaymentChange = false;

                    }

                    #region handling delayed date
                    //Need to check to make sure we don't end up w/ negative time in a delayed order situation

                    if (!Session.pblnModifyingOrder)
                    {
                        if (Session.cart.cartHeader.Delayed_Date != DateTime.MinValue)
                        {
                            if (!Session.pblnModifyingOrder)
                                blnRecalcLeadTime = true;
                            else if (Session.pblnModifyingOrder)
                            {
                                if (Session.originalcart.cartHeader.Delayed_Date != Session.cart.cartHeader.Delayed_Date)
                                    blnRecalcLeadTime = true;
                                else
                                    blnRecalcLeadTime = false;
                            }
                            else
                                blnRecalcLeadTime = false;

                            if (blnRecalcLeadTime == true)
                            {
                                if (Session.cart.cartHeader.Order_Type_Code == "D")
                                {
                                    if (Session.cart.cartHeader.Delayed_Date.AddMinutes(-SystemSettings.settings.pbytDeliveryLeadTime) < serverDateTime)
                                    {
                                        Session.cart.cartHeader.Delayed_Date = serverDateTime.AddMinutes(SystemSettings.settings.pbytDeliveryLeadTime);
                                        Session.cart.cartHeader.Kitchen_Display_Time = serverDateTime;
                                    }

                                }
                                else
                                {
                                    if (Session.cart.cartHeader.Delayed_Date.AddMinutes(-SystemSettings.settings.pbytCarryOutLeadTime) < serverDateTime)
                                    {
                                        Session.cart.cartHeader.Delayed_Date = serverDateTime.AddMinutes(SystemSettings.settings.pbytCarryOutLeadTime);
                                        Session.cart.cartHeader.Kitchen_Display_Time = serverDateTime;
                                    }
                                }

                            }
                            dtmSaved_Time = Session.cart.cartHeader.Delayed_Date;
                            Session.cart.cartHeader.Delayed_Order = 1;

                        }
                        else
                        {
                            dtmSaved_Time = Settings.Settings.GetServerDateTime();
                            Session.cart.cartHeader.Delayed_Order = 0;
                        }
                    }

                    #endregion

                    //'If you are not paying now then you want to update the order_saved time
                    if (Session.cart.cartHeader.Pay_Now == false)
                        Session.cart.cartHeader.Order_Saved = dtmSaved_Time;

                    //'If you are paying now and you have never saved the order then you need to update the order_saved time
                    else if (Session.cart.cartHeader.Pay_Now == true && Session.cart.cartHeader.Order_Saved == DateTime.MinValue)
                        Session.cart.cartHeader.Order_Saved = dtmSaved_Time;

                    if (!blnCancel && Session.pblnModifyingOrder)
                    {
                        if (Session.cart.cartHeader.Order_Lines_Modified && Session.pblnShowModifyScreen ||
                           Session.cart.cartHeader.Order_Type_Code != Session.originalcart.cartHeader.Order_Type_Code ||
                           Session.cart.cartHeader.Final_Total != Session.originalcart.cartHeader.Final_Total)
                        {
                            //frmModify.show(); //TO DO
                        }

                        #region handling route time and delivery time

                        if (Session.cart.cartHeader.Order_Type_Code == "D" && Session.originalcart.cartHeader.Order_Type_Code != "D")
                        {
                            if (Session.cart.cartHeader.Order_Lines_Modified && Session.pblnShowModifyScreen)
                            {
                                if (Session.cart.cartHeader.Order_Status_Code == 3)
                                {
                                    Session.cart.cartHeader.Driver_ID = string.Empty;
                                    Session.cart.cartHeader.Driver_Shift = "0";
                                    Session.cart.cartHeader.Route_Time = DateTime.MinValue;
                                    Session.cart.cartHeader.Delivery_Time = DateTime.MinValue;
                                }
                                else
                                {
                                    Session.cart.cartHeader.Driver_ID = string.Empty;
                                    Session.cart.cartHeader.Driver_Shift = "0";
                                    Session.cart.cartHeader.Route_Time = DateTime.MinValue;
                                    Session.cart.cartHeader.Return_Time = DateTime.MinValue;
                                    Session.cart.cartHeader.Delivery_Time = DateTime.MinValue;
                                }
                                Session.cart.cartHeader.Order_Status_Code = 1;
                            }
                            else
                            {
                                if (Session.cart.cartHeader.Order_Status_Code == 3)
                                {
                                    Session.cart.cartHeader.Driver_ID = string.Empty;
                                    Session.cart.cartHeader.Driver_Shift = "0";
                                    Session.cart.cartHeader.Route_Time = DateTime.MinValue;
                                    Session.cart.cartHeader.Return_Time = DateTime.MinValue;
                                    Session.cart.cartHeader.Delivery_Time = DateTime.MinValue;
                                    Session.cart.cartHeader.Order_Status_Code = 2;
                                }
                            }
                        }
                        else
                        {
                            if (Session.cart.cartHeader.Order_Lines_Modified && Session.pblnShowModifyScreen)
                            {
                                if (Session.cart.cartHeader.Order_Status_Code == 3)
                                {
                                    Session.cart.cartHeader.Driver_ID = string.Empty;
                                    Session.cart.cartHeader.Driver_Shift = "0";
                                    Session.cart.cartHeader.Route_Time = DateTime.MinValue;
                                    Session.cart.cartHeader.Delivery_Time = DateTime.MinValue;
                                }
                                else
                                {
                                    Session.cart.cartHeader.Driver_ID = string.Empty;
                                    Session.cart.cartHeader.Driver_Shift = "0";
                                    Session.cart.cartHeader.Route_Time = DateTime.MinValue;
                                    Session.cart.cartHeader.Return_Time = DateTime.MinValue;
                                    Session.cart.cartHeader.Delivery_Time = DateTime.MinValue;
                                }
                                Session.cart.cartHeader.Order_Status_Code = 1;
                            }
                            else
                            {
                                if (Session.cart.cartHeader.Order_Type_Code == "D" && Session.originalcart.cartHeader.Order_Type_Code != "D")
                                {
                                    if (Session.cart.cartHeader.Order_Status_Code == 3)
                                    {
                                        Session.cart.cartHeader.Driver_ID = string.Empty;
                                        Session.cart.cartHeader.Driver_Shift = "0";
                                        Session.cart.cartHeader.Route_Time = DateTime.MinValue;
                                        Session.cart.cartHeader.Delivery_Time = DateTime.MinValue;
                                    }
                                }

                                if (Session.cart.cartHeader.Order_Type_Code == "C" && SystemSettings.appControl.AutoRoutePickUp == true && Session.cart.cartHeader.Order_Status_Code > 1)
                                {
                                    Session.cart.cartHeader.Order_Status_Code = 4;
                                }
                                else if (Session.cart.cartHeader.Order_Type_Code == "P" && SystemSettings.appControl.AutoRouteCarryOut == true && Session.cart.cartHeader.Order_Status_Code > 1)
                                {
                                    Session.cart.cartHeader.Order_Status_Code = 4;
                                }
                                else if (Session.cart.cartHeader.Order_Type_Code == "I" && SystemSettings.appControl.AutoRouteDineIn == true && Session.cart.cartHeader.Order_Status_Code > 1)
                                {
                                    Session.cart.cartHeader.Order_Status_Code = 4;
                                }
                            }
                        }
                        #endregion

                    }

                    if (!blnCancel && Session.pblnModifyingOrder)
                    {
                        //handle payment condition
                        if (Session.pblnNewOrderTime)
                        {
                            if (Session.cart.cartHeader.Delayed_Date == DateTime.MinValue)
                            {
                                Session.cart.cartHeader.Order_Date = dtmSaved_Time;
                                Session.cart.cartHeader.Actual_Order_Date = dtmSaved_Time;
                                Session.cart.cartHeader.Delayed_Order = 0;
                            }
                            else
                            {
                                Session.cart.cartHeader.Actual_Order_Date = Session.cart.cartHeader.Delayed_Date;
                                Session.cart.cartHeader.Delayed_Order = 1;
                            }
                        }
                        else
                        {
                            if (Session.originalcart != null)
                            {
                                if (Session.originalcart.cartHeader != null)
                                {
                                    if (Session.cart.cartHeader.Delayed_Date != Session.originalcart.cartHeader.Delayed_Date)
                                    {
                                        Session.cart.cartHeader.Actual_Order_Date = Session.cart.cartHeader.Delayed_Date;
                                        Session.cart.cartHeader.Delayed_Order = 1;
                                    }
                                }
                            }
                            Session.pblnNewOrderTime = true;

                        }
                    }
                    else
                    {
                        if (Session.cart.cartHeader.Delayed_Date == DateTime.MinValue)
                        {
                            //Session.cart.cartHeader.Delayed_Date = Session.SystemDate;
                            Session.cart.cartHeader.Actual_Order_Date = dtmSaved_Time;
                            Session.cart.cartHeader.Delayed_Order = 0;
                        }
                        else
                        {
                            Session.cart.cartHeader.Actual_Order_Date = Session.cart.cartHeader.Delayed_Date;
                            Session.cart.cartHeader.Delayed_Order = 1;
                        }
                    }

                    Session.cart.cartHeader.Workstation_ID = Session._WorkStationID;

                    if (string.IsNullOrEmpty(Session.cart.Customer.Location_Code))
                        Session.cart.Customer.Location_Code = Session._LocationCode;

                    Logger.Trace("INFO", "S-14 OrderComplete : before call order api ", null, false);

                    Cart cartLocal = (new Cart()).GetCart();
                    cartLocal.cartHeader = Session.cart.cartHeader;
                    cartLocal.cartHeader.Action = "M";
                    cartLocal.Customer = Session.cart.Customer;
                    cartLocal.Customer.Action = "M";
                    foreach (OrderPayment payment in Session.cart.orderPayments)
                    {
                        payment.Action = "M";
                    }
                    foreach (OrderCreditCard creditpayment in Session.cart.orderCreditCards)
                    {
                        creditpayment.Action = "M";
                    }
                    cartLocal.orderPayments = Session.cart.orderPayments;
                    cartLocal.orderCreditCards = Session.cart.orderCreditCards;
                    CartFunctions.UpdateCustomer(cartLocal);

                    Logger.Trace("INFO", "S-15 OrderComplete : call final Add2Cart api ", null, false);

                    cartLocal.cartHeader.OriginalLocationCode = cartLocal.cartHeader.LocationCode;
                    Session.cart = APILayer.Add2Cart(cartLocal);

                    orderRequest.locationCode = Session.cart.cartHeader.LocationCode;
                    orderRequest.Order_Date = Session.cart.cartHeader.Order_Date;
                    orderRequest.cartId = Session.cart.cartHeader.CartId;
                    orderRequest.modifyOrder = Session.pblnModifyingOrder;
                    orderRequest.newOrderTime = UserFunctions.OrderTimeinSeconds();

                    Logger.Trace("INFO", "S-16 OrderComplete : call PusOrder api ", null, false);
                    if (APILayer.PushOrder(orderRequest))
                    {
                        Logger.Trace("INFO", "S-17 OrderComplete : call PusOrder api -Success", null, false);
                        blnSaveSuccessful = true;

                        /////////////////////////////////////////////
                        ItemwiseUpsellHistory();
                        ////////////////////////////////////////////

                        if (!Session.pblnModifyingOrder)
                        {
                            //Call Print Function
                            PrintFunctions.PrintReceipt(Session.currentOrderResponse.Order_Number, Session.currentOrderResponse.Order_Date, false, false);
                        }
                    }
                }
                else
                {
                    blnSaveSuccessful = true;
                }

                Logger.Trace("INFO", "S-18 OrderComplete : order pushed ", null, false);

                if (blnSaveSuccessful)
                {
                    ////TO DO SaveCustomerProfileData
                    //{ 
                    //  xml
                    //}

                    if (Session.pblnModifyingOrder)
                    {
                        OrderFunctions.POSUpdateOrderModifyingStatus(Session._LocationCode, Convert.ToInt64(Session.cart.cartHeader.Order_Number), Session.cart.cartHeader.Order_Date, Session.CurrentEmployee.LoginDetail.EmployeeCode, false);
                        PrintFunctions.PrintReceipt(Convert.ToInt64(Session.cart.cartHeader.Order_Number), Session.cart.cartHeader.Order_Date, false, true);

                        //Update_Order_Modifying_Status //TO DO
                    }

                    if (curChangeDue > 0 && !blnCancel)
                    {
                        CustomMessageBox.Show(APILayer.GetCatalogText(LanguageConstant.cintChangesDue) + " " + string.Format(Session.DisplayFormat, curChangeDue), CustomMessageBox.Buttons.OK, CustomMessageBox.Icon.Info);
                    }
                    Logger.Trace("INFO", "S-19 OrderComplete : Call cartOnHold API ", null, false);

                    //cash drawer open/close for an order
                    if ((SystemSettings.GetSettingValue("ManageCashDrawer", Session._LocationCode) == "1"))
                    {

                        if (Session.cashDrawerReasonsForOrder != null && Session.cashDrawerReasonsForOrder.Count > 0)
                        {
                            foreach (CashDrawerReason cashDrawerReason in Session.cashDrawerReasonsForOrder)
                            {
                                cashDrawerReason.Order_Number = Session.currentOrderResponse.Order_Number;
                            }
                            APILayer.InsertCashRegisterReasonForOrder(Session.cashDrawerReasonsForOrder);

                        }
                        
                    }

                    if ((SystemSettings.GetSettingValue("CartOnHold", Session._LocationCode) == "1"))
                    {
                        //APILayer.DeleteCartOnHold(Session.cart.cartHeader.CartId);
                        CartOnHoldRequest cartOnHoldRequest = new CartOnHoldRequest();
                        cartOnHoldRequest.CartId = Session.cart.cartHeader.CartId;
                        cartOnHoldRequest.Time = DateTime.Now;
                        cartOnHoldRequest.CustomerName = Session.cart.Customer.Name;
                        cartOnHoldRequest.CustomerNumber = Session.cart.Customer.Phone_Number;
                        cartOnHoldRequest.OrderAmount = Convert.ToDecimal(Session.cart.cartHeader.Total);
                        cartOnHoldRequest.OrderTaker = Session.CurrentEmployee.LoginDetail.FirstName + " " + Session.CurrentEmployee.LoginDetail.LastName;
                        cartOnHoldRequest.Terminal = Session.ComputerName;
                        cartOnHoldRequest.IsActive = 0;

                        Session.cart = APILayer.UpdateCartOnHold(cartOnHoldRequest);
                    }

                    UserFunctions.ClearSession();
                }
                Logger.Trace("INFO", "S-20 OrderComplete : order steps completed ", null, false);
                return blnSaveSuccessful;
            }
            catch (Exception ex)
            {
                Logger.Trace("ERROR", "OrderCompleteFunctions-OrderComplete(): " + ex.Message, ex, true);
                throw;
                //return blnSaveSuccessful;
            }
        }

        public static UserTypes.OrderCompletionState CheckOrderCompletion()
        {
            UserTypes.OrderCompletionState OrderCompletionValue = UserTypes.OrderCompletionState.OrderComplete;

            CheckForEmployeeCode();

            if (!CheckOrderLinesComplete())
            {
                OrderCompletionValue = UserTypes.OrderCompletionState.OrderNotComplete;
                return OrderCompletionValue;
            }

            if (!CheckUserClockedIn())
            {
                OrderCompletionValue = UserTypes.OrderCompletionState.OrderNotComplete;
                return OrderCompletionValue;
            }

            OrderCompletionValue = CheckDeliveryInformation();
            if (OrderCompletionValue != UserTypes.OrderCompletionState.OrderComplete)
            {
                return OrderCompletionValue;
            }

            if (!CheckTaxExemptPhone())
            {
                OrderCompletionValue = UserTypes.OrderCompletionState.OrderNotComplete;
                return OrderCompletionValue;
            }

            if (CheckReduceOrderRights() == false)
            {
                OrderCompletionValue = UserTypes.OrderCompletionState.OrderNotComplete;
                return OrderCompletionValue;
            }

            if (Session.pblnModifyingOrder)
            {
                Session.pblnAddModPrepared = CheckModifiedPreparedItem();

                if (Session.pblnAddModPrepared)
                {
                    Session.cart.cartHeader.Order_Lines_Modified = true;
                    Session.pblnOrderModifications = true;
                }
                else
                {
                    if (OrderLineAddedOrDeleted())
                    {
                        Session.cart.cartHeader.Order_Lines_Modified = true;
                        Session.pblnOrderModifications = true;
                    }
                    else
                    {
                        if (Session.cart.cartHeader.Final_Total != Session.originalcart.cartHeader.Final_Total)
                        {
                            if (Session.cart.cartHeader.Delivery_Fee != Session.originalcart.cartHeader.Delivery_Fee)
                            {
                                if (Session.cart.cartHeader.Final_Total - Session.originalcart.cartHeader.Final_Total == ((Session.cart.cartHeader.Delivery_Fee + Session.cart.cartHeader.Delivery_Fee_Tax1 + Session.cart.cartHeader.Delivery_Fee_Tax2) - (Session.originalcart.cartHeader.Delivery_Fee + Session.originalcart.cartHeader.Delivery_Fee_Tax1 + Session.originalcart.cartHeader.Delivery_Fee_Tax2)))
                                {
                                    Session.cart.cartHeader.Order_Lines_Modified = false;
                                    Session.pblnShowModifyScreen = false;
                                }
                                else
                                {
                                    Session.cart.cartHeader.Order_Lines_Modified = true;
                                    Session.pblnShowModifyScreen = false;
                                }
                                Session.pblnOrderModifications = true;
                            }
                            else
                            {
                                Session.cart.cartHeader.Order_Lines_Modified = true;
                                Session.pblnShowModifyScreen = false;
                                Session.pblnOrderModifications = true;
                            }
                            Session.pblnModifyPrint = true;
                        }
                        else
                        {
                            if (Session.originalcart != null)
                            {
                                if (Session.originalcart.cartHeader.Order_Date != Session.cart.cartHeader.Order_Date)
                                {
                                    Session.cart.cartHeader.Order_Lines_Modified = true;

                                    Session.pintSendOption = 1;
                                    Session.pblnNewOrderTime = true;
                                    Session.pblnShowModifyScreen = false;
                                    Session.pblnModifyPrint = true;

                                    Session.pblnOrderModifications = true;
                                }
                                else
                                {
                                    Session.cart.cartHeader.Order_Lines_Modified = true;
                                    Session.pblnShowModifyScreen = false;
                                }

                            }
                            else
                            {
                                Session.cart.cartHeader.Order_Lines_Modified = false;
                                Session.pblnShowModifyScreen = false;
                            }
                        }
                    }
                }
            }
            else
            {
                Session.cart.cartHeader.Order_Lines_Modified = true;
            }

            if (!CheckOrderTypePreference())
            {
                OrderCompletionValue = UserTypes.OrderCompletionState.OrderNotComplete;
                return OrderCompletionValue;
            }

            DeliveryPOSCaptureInfo();

            ShowGSTIN();

            Cart cartLocal = (new Cart()).GetCart();
            cartLocal.cartHeader = Session.cart.cartHeader;
            cartLocal.cartHeader.Action = "M";
            CartFunctions.UpdateCustomer(cartLocal);

            if (Session.cart.cartHeader.Order_Adjustments <= 0 && Session.cart.cartHeader.Order_Coupon_Total <= 0 && !String.IsNullOrEmpty(Session.cart.cartHeader.Coupon_Code))
                cartLocal.cartHeader.Coupon_Code = "";




            Session.cart = APILayer.Add2Cart(cartLocal);

            return OrderCompletionValue;
        }

        public static void CheckForEmployeeCode()
        {
            if (string.IsNullOrEmpty(Session.CurrentEmployee.LoginDetail.EmployeeCode))
            {
                frmLogin frmlogin = new frmLogin();
                frmlogin = new frmLogin();
                frmlogin.SpecialAccess = false;
                frmlogin.RequirePassword = false;
                frmlogin.ShowDialog();
            }
        }

        public static bool CheckOrderLinesComplete()
        {
            bool CheckOrderLinesComplete = true;
            try
            {
                foreach (CartItem cartItem in Session.cart.cartItems)
                {
                    //if (cartItem.Order_Line_Complete == false)
                    //{
                    //CustomMessageBox.Show(GetText(LanguageConstant.cintMSGOrderNotComplete), CustomMessageBox.Buttons.OK, CustomMessageBox.Icon.Exclamation);
                    if (cartItem.Description.Contains("-->"))
                    {
                        CustomMessageBox.Show(GetText(LanguageConstant.cintMSGOrderNotComplete), CustomMessageBox.Buttons.OK, CustomMessageBox.Icon.Exclamation);
                        CheckOrderLinesComplete = false;
                        break;
                    }
                    //}
                }
            }
            catch (Exception ex)
            {
                Logger.Trace("ERROR", "OrderCompleteFunctions-CheckOrderLinesComplete(): " + ex.Message, ex, true);
                CheckOrderLinesComplete = false;
            }

            return CheckOrderLinesComplete;
        }

        public static bool CheckUserClockedIn()
        {
            bool CheckUserClockedIn = true;

            bool blnClockedIn;
            EmployeeLoginRequest loginRequest = new EmployeeLoginRequest();
            loginRequest.UserId = Session.UserID;
            loginRequest.Password = (Session.CurrentEmployee.LoginDetail.EmployeeCode != null && Session.CurrentEmployee.LoginDetail != null && Session.CurrentEmployee.LoginDetail.UserID == Session.UserID) ? Session.LoginPassword : Constants.DefaultPassword;

            loginRequest.LocationCode = Session._LocationCode;
            loginRequest.SystemDate = Session.SystemDate;
            loginRequest.Source = Constants.Source;
            loginRequest.EmployeeCode = Session.CurrentEmployee.LoginDetail.EmployeeCode;
            //'-----------------------------------------------------------------------------
            //' Make sure user is clocked in
            //'-----------------------------------------------------------------------------
            if (APILayer.IsTechnicalSupport(APILayer.CallType.POST, loginRequest))
            {
                blnClockedIn = true;
            }
            else
            {
                CheckEmployeeRequest checkEmployeeRequest = new CheckEmployeeRequest();
                checkEmployeeRequest.LocationCode = Session._LocationCode;
                checkEmployeeRequest.UserId = Session.CurrentEmployee.LoginDetail.EmployeeCode;
                checkEmployeeRequest.SystemDate = Session.SystemDate;

                TimeClockGetEmpClockedInResponse timeClockGetEmpClockedIn = new TimeClockGetEmpClockedInResponse();
                timeClockGetEmpClockedIn = APILayer.TimeClockGetEmpClockedIn(APILayer.CallType.POST, checkEmployeeRequest);
                if (timeClockGetEmpClockedIn != null && timeClockGetEmpClockedIn.Result != null && timeClockGetEmpClockedIn.Result.TimeClockGetEmpClockedIn != null && string.IsNullOrEmpty(timeClockGetEmpClockedIn.Result.TimeClockGetEmpClockedIn.EmployeeCode))
                {
                    blnClockedIn = false;
                }
                else
                {
                    blnClockedIn = true;
                }
            }

            if (!blnClockedIn)
            {
                CustomMessageBox.Show(GetText(LanguageConstant.cintMSGIDNoLongerClockedIn), CustomMessageBox.Buttons.OK, CustomMessageBox.Icon.Exclamation);

                Session.CurrentEmployee = null;

                frmLogin frmLogin = new frmLogin();
                frmLogin.ShowDialog();

                loginRequest.UserId = Session.UserID;
                loginRequest.Password = (Session.CurrentEmployee.LoginDetail.EmployeeCode != null && Session.CurrentEmployee.LoginDetail != null && Session.CurrentEmployee.LoginDetail.UserID == Session.UserID) ? Session.LoginPassword : Constants.DefaultPassword;
                loginRequest.LocationCode = Session._LocationCode;
                loginRequest.SystemDate = Session.SystemDate;
                loginRequest.Source = Constants.Source;
                loginRequest.EmployeeCode = Session.CurrentEmployee.LoginDetail.EmployeeCode; ;

                if (APILayer.IsTechnicalSupport(APILayer.CallType.POST, loginRequest))
                {
                    blnClockedIn = true;
                }
                else
                {
                    CheckEmployeeRequest checkEmployeeRequest = new CheckEmployeeRequest();
                    checkEmployeeRequest.LocationCode = Session._LocationCode;
                    checkEmployeeRequest.UserId = Session.CurrentEmployee.LoginDetail.EmployeeCode;
                    checkEmployeeRequest.SystemDate = Session.SystemDate;
                    //timeClockClockInOutRequest.EmployeeCode = Session.CurrentEmployee.LoginDetail.EmployeeCode; ;
                    //timeClockClockInOutRequest.Source = Constants.Source;

                    TimeClockGetEmpClockedInResponse timeClockGetEmpClockedIn = new TimeClockGetEmpClockedInResponse();
                    timeClockGetEmpClockedIn = APILayer.TimeClockGetEmpClockedIn(APILayer.CallType.POST, checkEmployeeRequest);
                    if (timeClockGetEmpClockedIn != null && timeClockGetEmpClockedIn.Result != null && timeClockGetEmpClockedIn.Result.TimeClockGetEmpClockedIn != null && string.IsNullOrEmpty(timeClockGetEmpClockedIn.Result.TimeClockGetEmpClockedIn.EmployeeCode))
                    {
                        blnClockedIn = false;
                    }
                    else
                    {
                        blnClockedIn = true;
                    }

                    if (!blnClockedIn)
                    {
                        CheckUserClockedIn = false;

                        return CheckUserClockedIn;
                    }
                }
            }

            //frmOrder.lblOrderTaker.Caption = EmployeeCollection.Last_Name & ", " & EmployeeCollection.First_Name

            return CheckUserClockedIn;
        }

        public static UserTypes.OrderCompletionState CheckDeliveryInformation()
        {
            UserTypes.OrderCompletionState CheckDeliveryInformation = UserTypes.OrderCompletionState.OrderComplete;
            try
            {
                byte bytCount;
                int bytMinPhoneDigits = 0;
                bytMinPhoneDigits = Session.MaxPhoneDigits;
                if (Session.cart.Customer.Phone_Number == null)
                {
                    CheckDeliveryInformation = UserTypes.OrderCompletionState.OpenCustomer;
                    return CheckDeliveryInformation;
                }

                if (!Session.pblnCashingOutAfter)
                {
                    if (Session.cart.Customer.Address_Type == "B" && string.IsNullOrEmpty(Session.cart.Customer.Company_Name))
                    {
                        //CustomMessageBox.Show(GetText(LanguageConstant.cintMSGAllRequiredFieldsYellow), CustomMessageBox.Buttons.OK, CustomMessageBox.Icon.Exclamation);
                        CustomMessageBox.Show("Company name " + MessageConstant.NotValidMessage, CustomMessageBox.Buttons.OK, CustomMessageBox.Icon.Exclamation);

                        CheckDeliveryInformation = UserTypes.OrderCompletionState.OpenCustomer;
                        return CheckDeliveryInformation;
                    }

                    if (Session.cart.cartHeader.Order_Type_Code == "D")
                    {
                        if (Session.cart.Customer.Phone_Number.Length < bytMinPhoneDigits)
                        {
                            CustomMessageBox.Show("Phone number " + MessageConstant.NotValidMessage, CustomMessageBox.Buttons.OK, CustomMessageBox.Icon.Exclamation);
                            CheckDeliveryInformation = UserTypes.OrderCompletionState.OpenCustomer;
                            return CheckDeliveryInformation;
                        }
                        if (string.IsNullOrEmpty(Session.cart.Customer.City))
                        {
                            //CustomMessageBox.Show(GetText(LanguageConstant.cintMSGAllRequiredFieldsYellow), CustomMessageBox.Buttons.OK, CustomMessageBox.Icon.Exclamation);
                            CustomMessageBox.Show("Customer city " + MessageConstant.NotValidMessage, CustomMessageBox.Buttons.OK, CustomMessageBox.Icon.Exclamation);
                            CheckDeliveryInformation = UserTypes.OrderCompletionState.OpenCustomer;
                            return CheckDeliveryInformation;
                        }
                        if (string.IsNullOrEmpty(Session.cart.Customer.Region))
                        {
                            CustomMessageBox.Show("Customer region " + MessageConstant.NotValidMessage, CustomMessageBox.Buttons.OK, CustomMessageBox.Icon.Exclamation);
                            CheckDeliveryInformation = UserTypes.OrderCompletionState.OpenCustomer;
                            return CheckDeliveryInformation;
                        }
                        if (string.IsNullOrEmpty(Session.cart.Customer.Postal_Code))
                        {
                            CustomMessageBox.Show("Customer postal code " + MessageConstant.NotValidMessage, CustomMessageBox.Buttons.OK, CustomMessageBox.Icon.Exclamation);
                            CheckDeliveryInformation = UserTypes.OrderCompletionState.OpenCustomer;
                            return CheckDeliveryInformation;
                        }

                        if (SystemSettings.settings.pblnCustNameReqDelivery && string.IsNullOrEmpty(Session.cart.Customer.Name))
                        {
                            //CustomMessageBox.Show(GetText(LanguageConstant.cintMSGAllRequiredFieldsYellow), CustomMessageBox.Buttons.OK, CustomMessageBox.Icon.Exclamation);
                            CustomMessageBox.Show("Customer name " + MessageConstant.NotValidMessage, CustomMessageBox.Buttons.OK, CustomMessageBox.Icon.Exclamation);
                            CheckDeliveryInformation = UserTypes.OrderCompletionState.OpenCustomer;
                            return CheckDeliveryInformation;
                        }
                        if (Session.cart.Customer.Street_Code == Settings.Settings.GetNoneStreetCode())
                        {
                            CustomMessageBox.Show(GetText(LanguageConstant.cintMSGValidStreetRequired), CustomMessageBox.Buttons.OK, CustomMessageBox.Icon.Exclamation);

                            CheckDeliveryInformation = UserTypes.OrderCompletionState.OpenCustomer;
                            return CheckDeliveryInformation;
                        }
                    }

                    else if (Session.cart.cartHeader.Order_Type_Code == "C")
                    {
                        if (SystemSettings.settings.pblnCustNameReqCarryOut && string.IsNullOrEmpty(Session.cart.Customer.Name)
                            || SystemSettings.settings.pblnPromptTentCarryOut && string.IsNullOrEmpty(Session.cart.cartHeader.Tent_Number))
                        {
                            if (!QuickServiceCaptureInfo())
                            {
                                CheckDeliveryInformation = UserTypes.OrderCompletionState.OrderNotComplete;
                                return CheckDeliveryInformation;
                            }
                            return CheckDeliveryInformation;
                        }


                    }
                    else if (Session.cart.cartHeader.Order_Type_Code == "P")
                    {
                        if (SystemSettings.settings.pblnCustNameReqPickUp && string.IsNullOrEmpty(Session.cart.Customer.Name) ||
                            SystemSettings.settings.pblnPromptTentPickUp && string.IsNullOrEmpty(Session.cart.cartHeader.Tent_Number) ||
                            (Session.cart.Customer.Phone_Number.Length < bytMinPhoneDigits &&
                            (SystemSettings.WorkStationSettings.pblnRequire_Phone_Pick_Up || Session.cart.cartHeader.Delayed_Date == DateTime.MinValue || Session.cart.Customer.Tax_Exempt1)))
                        {
                            if (!QuickServiceCaptureInfo())
                            {
                                
                                CheckDeliveryInformation = UserTypes.OrderCompletionState.OrderNotComplete;
                                return CheckDeliveryInformation;
                            }
                        }

                    }
                    else if (Session.cart.cartHeader.Order_Type_Code == "I")
                    {
                        if (SystemSettings.settings.pblnCustNameReqDineIn && string.IsNullOrEmpty(Session.cart.Customer.Name)
                            || SystemSettings.settings.pblnPromptTentDineIn && string.IsNullOrEmpty(Session.cart.cartHeader.Tent_Number))
                        {
                            if (!QuickServiceCaptureInfo())
                            {
                                CheckDeliveryInformation = UserTypes.OrderCompletionState.OrderNotComplete;
                                return CheckDeliveryInformation;
                            }
                        }

                    }

                    //'-----------------------------------------------------------------------------
                    //' If this is a college or hotel then make sure we have the required fields
                    //'-----------------------------------------------------------------------------

                    if (Session.cart.Customer.Address_Type == "C" || Session.cart.Customer.Address_Type == "H")
                    {
                        if (string.IsNullOrEmpty(Session.cart.Customer.Name))
                        {
                            //CustomMessageBox.Show(GetText(LanguageConstant.cintMSGAllRequiredFieldsYellow), CustomMessageBox.Buttons.OK, CustomMessageBox.Icon.Exclamation);
                            CustomMessageBox.Show("Customer name " + MessageConstant.NotValidMessage, CustomMessageBox.Buttons.OK, CustomMessageBox.Icon.Exclamation);

                            CheckDeliveryInformation = UserTypes.OrderCompletionState.OpenOrder;
                            return CheckDeliveryInformation;

                        }
                        if (string.IsNullOrEmpty(Session.cart.Customer.Suite))
                        {

                            CustomMessageBox.Show("Customer suite " + MessageConstant.NotValidMessage, CustomMessageBox.Buttons.OK, CustomMessageBox.Icon.Exclamation);

                            CheckDeliveryInformation = UserTypes.OrderCompletionState.OpenOrder;
                            return CheckDeliveryInformation;

                        }
                    }

                    if (Session.cart.Customer.Phone_Number.Length >= bytMinPhoneDigits)
                    {
                        if (Session.CustomerProfileCollection == null)
                        {
                            //CustomMessageBox.Show(GetText(LanguageConstant.cintMSGAllRequiredFieldsYellow), CustomMessageBox.Buttons.OK, CustomMessageBox.Icon.Exclamation);
                            CustomMessageBox.Show("Phone number " + MessageConstant.NotValidMessage, CustomMessageBox.Buttons.OK, CustomMessageBox.Icon.Exclamation);
                            return CheckDeliveryInformation;
                        }
                    }


                }
            }
            catch (Exception ex)
            {
                Logger.Trace("ERROR", "OrderCompleteFunctions-CheckDeliveryInformation(): " + ex.Message, ex, true);
            }

            return CheckDeliveryInformation;
        }

        public static bool CheckTaxExemptPhone()
        {
            bool CheckTaxExemptPhone = true;
            byte bytCount;
            int bytMinPhoneDigits = 0;

            string pstrPhoneMask = SystemSettings.settings.pstrPhoneMask;
            bytMinPhoneDigits = Session.MaxPhoneDigits;

            if (Session.cart.Customer.Tax_Exempt1)
            {
                if (Session.cart.Customer.Phone_Number.Length < bytMinPhoneDigits)
                {
                    //frmCustomer.ltxtPhone_Number.ForeColor = System.Drawing.Color.Yellow
                    //CustomMessageBox.Show(GetText(LanguageConstant.cintMSGAllRequiredFieldsYellow), CustomMessageBox.Buttons.OK, CustomMessageBox.Icon.Exclamation);
                    CustomMessageBox.Show("Phone number " + MessageConstant.NotValidMessage, CustomMessageBox.Buttons.OK, CustomMessageBox.Icon.Exclamation);
                    CheckTaxExemptPhone = false;

                    return CheckTaxExemptPhone;
                }
            }

            return CheckTaxExemptPhone;
        }

        public static bool CheckReduceOrderRights()
        {
            bool CheckReduceOrderRights = true;
            EmployeeResult oldLoginEmployee;
            bool blnLoginSuccessful;
            EmployeeResult OldEmployeeCollection;// As bvOrderEntry.cEmployeeFields

            //'-----------------------------------------------------------------------------
            //' If they are modifying an order and the price is less then check if they have
            //' access to reduce an order.  If they do not then delete all the modifications
            //' they did and put them back to what they had.
            //'-----------------------------------------------------------------------------
            if (Session.pblnModifyingOrder)
            {
                if ((Session.originalcart.cartHeader.Final_Total - 0.01m) > Math.Round(Session.cart.cartHeader.Total))
                {
                    if (CustomMessageBox.Show(GetText(LanguageConstant.cintMSGReducingOrder), CustomMessageBox.Buttons.YesNo, CustomMessageBox.Icon.Question) == DialogResult.Yes)
                    {
                        OldEmployeeCollection = Session.CurrentEmployee;

                        if (!string.IsNullOrEmpty(Session.CurrentEmployee.LoginDetail.EmployeeCode) && Session.CurrentEmployee.LoginDetail.blnReduceOrder)
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
                                blnLoginSuccessful = true;
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
                                if (!string.IsNullOrEmpty(Session.CurrentEmployee.LoginDetail.EmployeeCode) && Session.CurrentEmployee.LoginDetail.blnRealOrder)
                                {
                                    blnLoginSuccessful = true;
                                }
                                else
                                {
                                    blnLoginSuccessful = false;
                                    CustomMessageBox.Show(APILayer.GetCatalogText(LanguageConstant.cintMSGInsufficientPrivileges), CustomMessageBox.Buttons.OK);
                                }
                            }
                            else
                                blnLoginSuccessful = false;
                            Session.CurrentEmployee = oldLoginEmployee;

                        }

                        if (!blnLoginSuccessful)
                        {
                            CheckReduceOrderRights = false;
                            return CheckReduceOrderRights;
                        }
                    }
                    else
                    {
                        CheckReduceOrderRights = false;
                        return CheckReduceOrderRights;
                    }
                }
            }

            OldEmployeeCollection = null;

            return CheckReduceOrderRights;
        }

        public static bool CheckModifiedPreparedItem()
        {
            bool CheckModifiedPreparedItem = true;


            foreach (CartItem cartItem in Session.cart.cartItems)
            {
                if (cartItem.New_Item && cartItem.Prepared)
                {
                    CheckModifiedPreparedItem = true;
                    Session.pblnShowModifyScreen = true;
                }
                else if (cartItem.Item_Modified && cartItem.Prepared)
                {
                    //'Check to make sure the actual item was changed
                    foreach (CartItem origCartItem in Session.cart.cartItems)
                    {
                        if (origCartItem.Line_Number == cartItem.Line_Number)
                        {
                            if (origCartItem.Size_Code != cartItem.Size_Code
                                || origCartItem.Combo_Menu_Code != cartItem.Combo_Menu_Code
                                || origCartItem.Menu_Code != cartItem.Menu_Code
                                || origCartItem.Quantity != cartItem.Quantity
                                || origCartItem.Topping_Codes != cartItem.Topping_Codes
                                || origCartItem.Topping_Codes != cartItem.Topping_Codes)
                            {
                                Session.pblnShowModifyScreen = true;

                                CheckModifiedPreparedItem = true;
                                break;
                            }
                            else
                            {
                                //'Check to see if the instructions have changed
                                if (BuildIndividualInstructionString(cartItem.Line_Number, cartItem.Sequence, cartItem.itemReasons) != origCartItem.Instruction_String)
                                {
                                    Session.pblnShowModifyScreen = true;
                                    CheckModifiedPreparedItem = true;
                                    break;
                                }
                                else
                                {
                                    break;
                                }
                            }
                        }
                    }
                }

                if (CheckModifiedPreparedItem)
                {
                    return CheckModifiedPreparedItem;
                }

            }

            return CheckModifiedPreparedItem;
        }

        public static bool OrderLineAddedOrDeleted()
        {
            bool OrderLineAddedOrDeleted = true;

            foreach (CartItem cartItem in Session.cart.cartItems)
            {
                if (cartItem.New_Item == true)
                {
                    OrderLineAddedOrDeleted = true;
                }
                else if (cartItem.Item_Modified == true)
                {
                    OrderLineAddedOrDeleted = true;
                }

                if (OrderLineAddedOrDeleted)
                {
                    return OrderLineAddedOrDeleted;
                }
            }
            return OrderLineAddedOrDeleted;
        }

        public static bool CheckOrderTypePreference()
        {
            bool CheckOrderTypePreference = true;

            if ((string.IsNullOrEmpty(Session.selectedOrderType)) || (Session.cart.cartHeader.Order_Type_Code.Trim().Length == 0))
            {
                CheckOrderTypePreference = false;
                CustomMessageBox.Show(GetText(LanguageConstant.cintMSGChooseOrderType), CustomMessageBox.Buttons.OK, CustomMessageBox.Icon.Exclamation);
            }


            return CheckOrderTypePreference;
        }


        public static void DeliveryPOSCaptureInfo()
        {
            frmCapture frmCaptureInfo = new frmCapture(true);

            frmCaptureInfo.blnDeliveryCaptureInfo = true;

            if (frmCaptureInfo.returnValue)
                frmCaptureInfo.ShowDialog();

        }

        public static string BuildIndividualInstructionString(int intLineNumber, int intSequence, List<ItemReason> OrderLineReasonsCollection)
        {
            //OrderReason OrderLineReason;
            int intReason;
            string strInstructions = string.Empty;

            if (OrderLineReasonsCollection.Count == 0)
            {
                return strInstructions;
            }

            for (intReason = 1; intReason < Session.cart.cartItems.Count; intReason++)
            {
                foreach (ItemReason reason in Session.cart.cartItems[intReason].itemReasons)
                {
                    if (reason.Line_Number == intLineNumber && reason.Sequence == intSequence && reason.Deleted == false && reason.Reason_Group_Code == 5)
                    {
                        if (string.IsNullOrEmpty(strInstructions))
                        {
                            if (reason.Reason_ID == 0)
                            {
                                strInstructions = reason.Other_Information;
                            }
                            else
                            {
                                strInstructions = reason.Reason_Description;
                            }
                        }
                        else
                        {
                            if (reason.Reason_ID == 0)
                            {
                                strInstructions = strInstructions + "; " + reason.Other_Information;
                            }
                            else
                            {
                                strInstructions = strInstructions + "; " + reason.Reason_Description;
                            }
                        }
                    }
                }
            }

            return strInstructions;
        }

        public static string GetText(int key)
        {
            string result = string.Empty;

            try
            {
                GetTextRequest getTextRequest = new GetTextRequest();
                getTextRequest.LocationCode = Session._LocationCode;
                getTextRequest.LanguageCode = Session.CurrentEmployee.LoginDetail.LanguageCode;
                getTextRequest.KeyField = key;

                result = APILayer.GetText(APILayer.CallType.POST, getTextRequest);
            }
            catch (Exception ex)
            {
                Logger.Trace("ERROR", "OrderCompleteFunctions-GetText(): " + ex.Message, ex, true);
            }
            return result;
        }

        public static bool QuickServiceCaptureInfo()
        {
            bool quickServiceCapture = false;
            frmCapture frmCaptureInfo = new frmCapture(false);

            frmCaptureInfo.blnDeliveryCaptureInfo = false;

            if (frmCaptureInfo.returnValue)
                frmCaptureInfo.ShowDialog();

            if (frmCaptureInfo.pblnCancel)
                quickServiceCapture = false;
            else
                quickServiceCapture = true;

            return quickServiceCapture;
        }

        public static void ShowGSTIN()
        {
            if (SystemSettings.settings.pbytGSTIN_Prompt_Mandatory == 1)
            {
                if (Session.cart.cartHeader.Order_Type_Code == "C" && SystemSettings.settings.pbytGSTIN_Prompt_CarryOut == 1)
                {
                    if (Session.cart.cartHeader.Final_Total >= Convert.ToDecimal(SystemSettings.settings.plngGSTIN_Amount))
                    {
                        frmGSTEntry frmGST = new frmGSTEntry();
                        frmGST.ShowDialog();
                    }
                }
                if (Session.cart.cartHeader.Order_Type_Code == "D" && SystemSettings.settings.pbytGSTIN_Prompt_Delivery == 1)
                {
                    if (Session.cart.cartHeader.Final_Total >= Convert.ToDecimal(SystemSettings.settings.plngGSTIN_Amount))
                    {
                        frmGSTEntry frmGST = new frmGSTEntry();
                        frmGST.ShowDialog();
                    }
                }
                if (Session.cart.cartHeader.Order_Type_Code == "I" && SystemSettings.settings.pbytGSTIN_Prompt_DineIn == 1)
                {
                    if (Session.cart.cartHeader.Final_Total >= Convert.ToDecimal(SystemSettings.settings.plngGSTIN_Amount))
                    {
                        frmGSTEntry frmGST = new frmGSTEntry();
                        frmGST.ShowDialog();
                    }
                }
                if (Session.cart.cartHeader.Order_Type_Code == "P" && SystemSettings.settings.pbytGSTIN_Prompt_ODC == 1)
                {
                    if (Session.cart.cartHeader.Final_Total >= Convert.ToDecimal(SystemSettings.settings.plngGSTIN_Amount))
                    {
                        frmGSTEntry frmGST = new frmGSTEntry();
                        frmGST.ShowDialog();
                    }
                }

            }
        }

        public static void ShowRecap()
        {
            if (SystemSettings.GetSettingValue("DisplayEstimatedTimeScreen", Session._LocationCode) == "1")
            {
                frmRecap frmRecap = new frmRecap();
                frmRecap.blnEstimatedTime = false;
                frmRecap.ShowDialog();
            }
        }

        private static void ItemwiseUpsellHistory()
        {
            try
            {

                if (Session.itemwiseUpsellHistory != null && Session.itemwiseUpsellHistory.Count > 0)
                {
                    foreach (ItemwiseUpsellHistory history in Session.itemwiseUpsellHistory)
                    {
                        history.Order_Date = Session.currentOrderResponse.Order_Date;
                        history.Order_Number = Session.currentOrderResponse.Order_Number;
                    }

                    APILayer.LogItemwiseUpsellHistory(Session.itemwiseUpsellHistory);
                }
            }
            catch
            {

            }
        }

    }
}
