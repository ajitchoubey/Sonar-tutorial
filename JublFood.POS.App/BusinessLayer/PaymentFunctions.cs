using JublFood.POS.App.API;
using JublFood.POS.App.Cache;
using JublFood.POS.App.Models.Catalog;
using JublFood.POS.App.Models.Order;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Jublfood.AppLogger;
using JublFood.POS.App.Class;
using JublFood.POS.App.Models.Cart;
using System.Linq;
using JublFood.POS.App.Models.Employee;
using System.Diagnostics;

namespace JublFood.POS.App.BusinessLayer
{
    public class PaymentFunctions
    {
        public enum enumPaymentTypes
        {
            ecash1 = 0,
            eCash2 = 1,
            eCash3 = 2,
            eCash4 = 3,
            eCreditCard = 4,
            eCheck = 5,
            eGiftCard = 6,
            eCharge = 7,
            eSudexo = 8,
            eAccor = 9,
            eGiftCard1 = 10
        }

        public static UC_CustomerOrderBottomMenu UC_CustomerOrderBottomMenu = new UC_CustomerOrderBottomMenu();
        public static void ButtonsEnabled(bool blnEnabled)
        {
            UC_CustomerOrderBottomMenu.cmdPay.Enabled = blnEnabled;
            UC_CustomerOrderBottomMenu.cmdComplete.Enabled = blnEnabled;
            //UC_CustomerOrderBottomMenu.cmdPrintOnDemand.Enabled = blnEnabled;
        }
        public static void PayButtonEnabled(bool blnEnabled)
        {
            UC_CustomerOrderBottomMenu.cmdPay.Enabled = blnEnabled;
        }

        public static void EnableOrderTypes(bool blnEnabled)
        {
            foreach (Control control in UC_CustomerOrderBottomMenu.Controls)
            {

                foreach (Control _control in control.Controls)
                {
                    ((Button)_control).Enabled = blnEnabled;

                }
            }
        }
       
        public static void CheckPaymentsDelayedDate()
        {
            try
            {
                OrderPayment orderPayment = new OrderPayment();

                if (Session.originalresponsePayment.payment.Count > 0)
                {
                    foreach (OrderPayment OrigPayment in Session.originalresponsePayment.payment)
                    {
                        foreach (OrderPayment payment in Session.cart.orderPayments)
                        {
                            if (payment.Payment_Line_Number == OrigPayment.Payment_Line_Number)
                            {
                                if (payment.Deleted == false)
                                {
                                    OrderPayment newOrderPayment = new OrderPayment();
                                    newOrderPayment.Added_By = payment.Added_By;
                                    newOrderPayment.Amount_Tendered = payment.Amount_Tendered;
                                    newOrderPayment.AVSStreet = payment.AVSStreet;
                                    newOrderPayment.CardExpiration = payment.CardExpiration;
                                    newOrderPayment.CardNumber = payment.CardNumber;
                                    newOrderPayment.Cash_Out_Shift = Session.CurrentEmployee.LoginDetail.DateShiftNumber;
                                    newOrderPayment.CashOut_ID = Session.CurrentEmployee.LoginDetail.EmployeeCode;
                                    newOrderPayment.Change_Due = payment.Change_Due;
                                    newOrderPayment.Check_Info = payment.Check_Info;
                                    newOrderPayment.CreditCardAmount = payment.CreditCardAmount;
                                    newOrderPayment.CreditCardDescription = payment.CreditCardDescription;
                                    newOrderPayment.CreditCardID = payment.CreditCardID;
                                    newOrderPayment.Currency_Amount = payment.Currency_Amount;
                                    newOrderPayment.Currency_Code = payment.Currency_Code;
                                    newOrderPayment.CVV2 = payment.CVV2;
                                    newOrderPayment.Data_Changed = true;
                                    newOrderPayment.Data_Processed = false;
                                    newOrderPayment.Deleted = false;
                                    newOrderPayment.Location_Code = payment.Location_Code;
                                    newOrderPayment.NameOnCard = payment.NameOnCard;
                                    newOrderPayment.NewPayment = true;
                                    newOrderPayment.Order_Date = payment.Order_Date;
                                    newOrderPayment.Order_Number = payment.Order_Number;
                                    newOrderPayment.Order_Pay_Type_Code = payment.Order_Pay_Type_Code;
                                    newOrderPayment.Payment_Amount = payment.Payment_Amount;
                                    newOrderPayment.Payment_Line_Number = Session.cart.orderPayments.Count + 1;
                                    newOrderPayment.PostalCode = payment.PostalCode;
                                    newOrderPayment.Process_Failed = false;
                                    newOrderPayment.Sequence = 1;
                                    newOrderPayment.Tip = payment.Tip;
                                    newOrderPayment.Track1Data = payment.Track1Data;
                                    newOrderPayment.Track2Data = payment.Track2Data;
                                    newOrderPayment.CashOut_Time = DateTime.Now;

                                    //Session.cart.orderPayments.Add(newOrderPayment);
                                    SendPaymentAPI(newOrderPayment, null); 
                                    payment.Data_Changed = true;
                                    payment.Deleted = true;
                                }
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                Logger.Trace("ERROR", "PaymentFunctions-CheckPaymentsDelayedDate(): " + ex.Message, ex, true);
                throw;
            }                
            
        }

        public static void OpenPaymentScreen()
        {
            bool blnAllowChargeAccount = false;
            bool blnAllowCreditCards = false;
            //CurrencyCode //TO DO
            if (Session.cart.Customer.Accept_Charge_Account ==true && Convert.ToInt32( Session.cart.Customer.Customer_Code) > 0)  //m_blnAcceptChargeAccounts = rsARSettings!Allow_Customer_Charge_Accounts
            {
                blnAllowChargeAccount = true;
            }
            else 
            {
                blnAllowChargeAccount = false;
            }
            if (Session.cart.Customer.Accept_Credit_Cards == true)
            {
                switch (SystemSettings.settings.pbytCreditCardProcessing)
                {
                    case 0:
                        blnAllowCreditCards = false;
                        break;
                    case 1:
                        blnAllowCreditCards = true;
                        break;
                    default:
                        blnAllowCreditCards = SystemSettings.WorkStationSettings.pblnAccept_Credit_Cards;
                        break;
                }

            }
            else
            {
                blnAllowCreditCards = false;
            }
            
            frmPayment frmPayment = new frmPayment();
            frmPayment.Show();
            foreach (Form form in Application.OpenForms)
            {
                if (form.Text != "PAYMENT")
                    form.Hide();
            }



        }

        public static void LoadOrderPayTypes()
        {
            try
            {
                CatalogOrderPayTypeCodeResponse catalogOrderPayTypeCodeData = new CatalogOrderPayTypeCodeResponse();
                Session.orderPayTypeCodes = new List<CatalogOrderPayTypeCodes>();
                catalogOrderPayTypeCodeData = APILayer.GetOrderPayTypeCodes(Session._LocationCode, 1, 0);
                foreach (CatalogOrderPayTypeCodes catalogOrderPayTypeCode in catalogOrderPayTypeCodeData.catalogOrderPayTypeCodesForCash)
                {
                    CatalogOrderPayTypeCodes payTypeCode = new CatalogOrderPayTypeCodes();
                    payTypeCode = catalogOrderPayTypeCode;
                    Session.orderPayTypeCodes.Add(payTypeCode);
                }

                foreach (CatalogOrderPayTypeCodes catalogOrderPayTypeCode in catalogOrderPayTypeCodeData.catalogOrderPayTypeCodesForCheque)
                {
                    CatalogOrderPayTypeCodes payTypeCode = new CatalogOrderPayTypeCodes();
                    payTypeCode = catalogOrderPayTypeCode;
                    Session.orderPayTypeCodes.Add(payTypeCode);
                }

                foreach (CatalogOrderPayTypeCodes catalogOrderPayTypeCode in catalogOrderPayTypeCodeData.catalogOrderPayTypeCodesForDigital)
                {
                    CatalogOrderPayTypeCodes payTypeCode = new CatalogOrderPayTypeCodes();
                    payTypeCode = catalogOrderPayTypeCode;
                    Session.orderPayTypeCodes.Add(payTypeCode);
                }
            }
            catch(Exception ex)
            {
                Logger.Trace("ERROR", "PaymentFunctions-LoadOrderPayTypes(): " + ex.Message, ex, true);
            }
        }

        public static OrderPayment GetPayment()
        {
            OrderPayment orderPayment = new OrderPayment();
            try 
            {
                orderPayment.CartId = Session.cart.cartHeader.CartId;
                orderPayment.Added_By = Session.CurrentEmployee.LoginDetail.EmployeeCode; // ordercollection.employeeCode
                orderPayment.Location_Code = Session.cart.cartHeader.LocationCode;
                orderPayment.Order_Date = Session.cart.cartHeader.Order_Date;
                orderPayment.Order_Number = Session.cart.cartHeader.Order_Number;
                orderPayment.Payment_Line_Number = 1;
                orderPayment.Sequence = 1;
                orderPayment.Order_Pay_Type_Code = 0;
                orderPayment.Amount_Tendered = 0;
                orderPayment.Payment_Amount = 0;
                orderPayment.Change_Due = 0;
                orderPayment.Currency_Amount = 0;
                orderPayment.Currency_Code = SystemSettings.appControl.DefaultCurrency;
                orderPayment.Check_Info = "";
                orderPayment.Deleted = false;
                orderPayment.CashOut_ID = Session.CurrentEmployee.LoginDetail.EmployeeCode;
                orderPayment.Cash_Out_Shift = Session.CurrentEmployee.LoginDetail.DateShiftNumber;
                orderPayment.CashOut_Time = DateTime.Now;
                orderPayment.Tip = 0;
                orderPayment.Added_By = Session.CurrentEmployee.LoginDetail.EmployeeCode;
                orderPayment.Paid = 1;
                orderPayment.Data_Changed = true;
                orderPayment.NewPayment = true;
                orderPayment.Data_Processed = false;
                orderPayment.Deleted = false;
                orderPayment.Credit_Card_Code = 0;
                orderPayment.AVSStreet = "";
                orderPayment.ApprovalCode = "";
                orderPayment.CVV2 = "";
                orderPayment.CardExpiration = "";
                orderPayment.CardNumber = "";
                orderPayment.CreditCardDescription = "";
                orderPayment.Double_Code = "";
                orderPayment.NameOnCard = "";
                orderPayment.PostalCode = "";
                orderPayment.Track1Data = "";
                orderPayment.Track2Data = "";
                orderPayment.Action = "A";
                orderPayment.RRNumber = "";
                orderPayment.TransactionTime = Convert.ToDateTime(new DateTime(1899, 12, 30));
            }
            catch(Exception ex)
            {
                Logger.Trace("ERROR", "PaymentFunctions-GetPayment(): " + ex.Message, ex, true);
            }
            return orderPayment;

        }

        public static OrderCreditCard GetCreditCardPayment()
        {
            OrderCreditCard orderCreditCard = new OrderCreditCard();
            orderCreditCard.CartId = Session.cart.cartHeader.CartId;
            orderCreditCard.Location_Code = Session.cart.cartHeader.LocationCode;
            orderCreditCard.Order_Number = Session.cart.cartHeader.Order_Number;
            orderCreditCard.Order_Date = Session.cart.cartHeader.Order_Date;
            orderCreditCard.Payment_Line_Number = 1;
            orderCreditCard.Credit_Card_ID ="";
            orderCreditCard.Credit_Card_Transaction_Type =15;
            orderCreditCard.Credit_Card_Account ="";
            orderCreditCard.Credit_Card_Expiration ="";
            orderCreditCard.CVV2 ="";
            orderCreditCard.Credit_Card_Track_1_Data ="";
            orderCreditCard.Credit_Card_Track_2_Data = "";
            orderCreditCard.Credit_Card_Track_3_Data = "";
            orderCreditCard.Credit_Card_Amount =0;
            orderCreditCard.Credit_Card_Tip =0;
            orderCreditCard.Credit_Card_Approval ="";
            orderCreditCard.Transaction_Number ="";
            orderCreditCard.Name_On_Card ="";
            orderCreditCard.AVS_Street ="";
            orderCreditCard.Postal_Code ="";
            orderCreditCard.Security_Code ="";
            // public int Credit_Loss =;
            orderCreditCard.Entry_Method =0;
            orderCreditCard.Settlement_Date = Session.cart.cartHeader.Order_Date;
            orderCreditCard.Action_Code =1;
            orderCreditCard.Return_Code ="";
            orderCreditCard.Response_Code ="";
            orderCreditCard.Reference_Number ="";
            orderCreditCard.Batch_Number ="";
            orderCreditCard.Retrieval_Reference_Code ="";
            orderCreditCard.AVS_Result_Code ="";
            orderCreditCard.Card_Present_Value ="";
            orderCreditCard.Response_Text ="";
            orderCreditCard.Comment ="";
            orderCreditCard.Internal_Seq_Number ="";
            orderCreditCard.Trans_Item_Number =0;
            orderCreditCard.ACI ="";
            orderCreditCard.Est_Tip_Amount =0;
            orderCreditCard.Result_Code ="";
            orderCreditCard.Net_ID ="";
            orderCreditCard.Card_ID_Code = "";
            orderCreditCard.Acct_Data_Source ="";
            orderCreditCard.CVV2_Result_Code ="";
            orderCreditCard.Added_By = Session.CurrentEmployee.LoginDetail.EmployeeCode; 
            orderCreditCard.Added =DateTime.Now;
            orderCreditCard.Action = "A";
            return orderCreditCard;
        }

        public static bool ProcessPayments()
        {
            bool blnAllPaymentsProcessed = true;
            bool blnReqCheckInfo=false;

            Logger.Trace("INFO", "S-9 ProcessPayments :", null, false);
            if (SystemSettings.GetSettingValue("RequireCheckInformation", Session._LocationCode) == "1")
                blnReqCheckInfo = true;

            foreach(OrderPayment payment in Session.cart.orderPayments)
            {
                if(payment.Credit_Card_Code==2)
                {
                    if (payment.Check_Info.Length == 0 && blnReqCheckInfo == true)
                    {
                        CustomMessageBox.Show(APILayer.GetCatalogText(LanguageConstant.cintMSGStillAmtDue), CustomMessageBox.Buttons.OK, CustomMessageBox.Icon.Warning);
                        blnAllPaymentsProcessed = false;
                        break;
                    }
                }
                payment.Added_By = Session.CurrentEmployee.LoginDetail.EmployeeCode;
                payment.CashOut_ID = payment.CashOut_ID;//Session.CurrentEmployee.LoginDetail.EmployeeCode;
                payment.Cash_Out_Shift = Session.CurrentEmployee.LoginDetail.DateShiftNumber;
                payment.CashOut_Time = DateTime.Now;
                payment.Data_Processed = true;
                payment.Process_Failed = false;
                //if(payment.Credit_Card_Code == 4)
                //{

                //}
                Logger.Trace("INFO", "S-9.1 ProcessPayments : "
                + payment.Order_Pay_Type_Code + " Check_Info " + payment.Check_Info + " amt :" + payment.Amount_Tendered, null, false);

            }

            //if (blnAllPaymentsProcessed == true)
            //{
            //    foreach (OrderPayment payment in Session.cart.orderPayments)
            //    {
            //        if (payment.Deleted && payment.NewPayment)
            //        {
            //            Session.cart.orderPayments.Remove(payment);
            //            SendPaymentAPI(payment, null);
            //        }
            //    }
            //}
            Logger.Trace("INFO", "S-10 ProcessPayments : PaymentsProcessed =" + blnAllPaymentsProcessed, null, false);
            return blnAllPaymentsProcessed; 
        }

        public static void PaymentComplete()
        {
            try
            {
                if (Session.originalcart == null)
                {
                    Cart cartLocal = (new Cart().GetCartWithDefaultValues());
                    Session.originalcart = cartLocal;
                }
                if (Session.cart.orderPayments.Count > 0)
                {
                    //load frmProgressBar
                }

                Logger.Trace("INFO", "S-7 PaymentComplete", null, false);
                if (Session.originalcart != null && Session.originalresponsePayment != null)
                {
                    if (Session.originalresponsePayment.payment != null)
                    {
                        if (Session.originalcart.cartHeader != null || (Session.originalresponsePayment != null && Session.originalresponsePayment.payment.Count > 0))
                        {
                            if (Session.originalcart.cartHeader.Order_Date != Session.cart.cartHeader.Order_Date)
                            {
                                //PaymentFunctions.CheckPaymentsDelayedDate();
                            }
                        }
                    }
                }

                Logger.Trace("INFO", "S-8 PaymentComplete : before ProcessPayments", null, false);
                bool blnPaymentProcessed = PaymentFunctions.ProcessPayments();

                Logger.Trace("INFO", "S-11 PaymentComplete : after ProcessPayments : blnPaymentProcessed =" + blnPaymentProcessed, null, false);

                if (Session.cart.cartHeader.Delayed_Date != DateTime.MinValue)
                {
                    if (!Session.cart.cartHeader.Delayed_Same_Day)
                    {
                        foreach (OrderPayment payment in Session.cart.orderPayments)
                        {
                            if (payment.Credit_Card_Code == 4 || payment.Credit_Card_Code == 5)
                                CustomMessageBox.Show(APILayer.GetCatalogText(LanguageConstant.cintMSGCCFuturePmt), CustomMessageBox.Buttons.OK, CustomMessageBox.Icon.Warning);
                            break;
                        }
                    }
                }

                if (blnPaymentProcessed)
                {
                    Logger.Trace("INFO", "S-12 PaymentComplete : ProcessPayments is success", null, false);
                    if (Session.cart.orderPayments.Count > 0)
                    {
                        // unload frmProgressBar
                    }

                    foreach (Process process in Process.GetProcessesByName("SingleCustomer"))
                    {
                        string UserName = UserFunctions.GetProcessOwner(process.Id);
                        if (Environment.UserName.ToLower() == UserName.ToLower())
                        {
                            process.Kill();
                        }
                    }

                    if (Session.cart.cartHeader.Order_Lines_Modified)
                        OrderCompleteFunctions.ShowRecap();
                    

                    if (Session.pblnModifyingOrder)
                    {
                       
                        //if( )  //pudtWorkstation_Device_Options.pblnPrintCashOutReceipt = True  //Used for printing settings //TO DO
                        //{
                        foreach (OrderPayment payment in Session.cart.orderPayments)
                        {
                            if (payment.NewPayment)
                            {
                                Session.pblnModifyPrint = true;
                                break;
                            }
                        }
                        //}
                    
                        //cash drawer
                            
                        //
                    }

                    

                    Form currentForm =  Application.OpenForms.Cast<Form>().ToList().Find(x => x.Text.ToUpper() == "PAYMENT");

                    Logger.Trace("INFO", "S-12.6 PaymentComplete : ProcessPayments is success", null, false);

                    if (OrderCompleteFunctions.OrderComplete())
                    {
                        UserFunctions.GoToStartup(currentForm);
                    }
                    else
                    {
                        CustomMessageBox.Show(MessageConstant.OrderNotCompleted, CustomMessageBox.Buttons.OK, CustomMessageBox.Icon.Error);
                        UserFunctions.GoToStartup(currentForm);
                    }
                }

            }
            catch (Exception ex)
            {
                Logger.Trace("ERROR", "PaymentFunctions-PaymentComplete(): " + ex.Message, ex, true);
                CustomMessageBox.Show( "Error in Payment complete : " + ex.Message, CustomMessageBox.Buttons.OK, CustomMessageBox.Icon.Error);
            }

        }
        public static void SetPayNowFlag(ref decimal curAmountTendered, ref decimal curChangeDue)
        {
            curChangeDue = 0;
            curAmountTendered = 0;
            foreach (OrderPayment payment in Session.cart.orderPayments)
            {
                if (!payment.Deleted && payment.Paid == 1)
                    curAmountTendered = curAmountTendered + payment.Amount_Tendered;
            }
                //if(Session.IsPayClick)
                    if(curAmountTendered>= Session.cart.cartHeader.Final_Total  && Session.cart.cartHeader.Final_Total>0)
                    {
                        curChangeDue = curAmountTendered - Session.cart.cartHeader.Final_Total;
                        Session.cart.cartHeader.Pay_Now = true;
                    }
                    else if(Session.cart.cartHeader.Final_Total==0)
                        Session.cart.cartHeader.Pay_Now = true;
                    else
                        Session.cart.cartHeader.Pay_Now = false;
                //else
                    //Session.cart.cartHeader.Pay_Now = false;
            //}
        }

        public static string GetIntegrationModeParameterDesc(int Order_Pay_type_code)
        {
            
            string ParameterDesc = string.Empty;
            switch (Order_Pay_type_code)
            {
                case 16:
                    ParameterDesc = "DELIVERY";
                    break;
                case 27:
                    ParameterDesc = "DELIVERY";
                    break;
                case 37:
                    ParameterDesc = "REWARDS";
                    break;
                case 43:
                    ParameterDesc = "SODEXO";
                    break;
                case 47:
                    ParameterDesc = "PineLabsUPI";
                    break;
                default:
                    ParameterDesc = "";
                    break;
            }
            return ParameterDesc;
        }

        public static void SendPaymentAPI(OrderPayment orderPayment, OrderCreditCard orderCreditCard)
        {
            if (orderPayment != null)
            {
                Cart cartLocal = new Cart().GetCart();
                cartLocal.orderPayments.Add(orderPayment);

                if (orderPayment.Credit_Card_Code == 4 && orderCreditCard != null)
                    cartLocal.orderCreditCards.Add(orderCreditCard);

                cartLocal.cartHeader = Session.cart.cartHeader;
                cartLocal.Customer = Session.cart.Customer;
                Logger.Trace("INFO", "S-5 SendPaymentAPI: before update customer", null, false);
                CartFunctions.UpdateCustomer(cartLocal);
                Logger.Trace("INFO", "S-6 SendPaymentAPI: before add2cart", null, false);
                Session.cart = APILayer.Add2Cart(cartLocal);
            }
        }

        public static bool PayNow()
        {
            bool pblnPayNow = true;
            bool blnLoginSuccessful = false;

            EmployeeResult oldLoginEmployee;



            if (Session.cart.cartHeader.Final_Total >0)
            {
                if (!string.IsNullOrEmpty(Session.CurrentEmployee.LoginDetail.EmployeeCode) && Session.CurrentEmployee.LoginDetail.blnPayNow)
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
                        if (!string.IsNullOrEmpty(Session.CurrentEmployee.LoginDetail.EmployeeCode) && Session.CurrentEmployee.LoginDetail.blnPayNow)
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
            }   
            else
            {
                blnLoginSuccessful = true;
            }

            if (blnLoginSuccessful)
            {
                pblnPayNow = true;
                
                //kill singlecustiomer exe //TO DO
            }
            else
                pblnPayNow = false;

            return pblnPayNow;
        }


        #region CashDrawer
        public static bool CashDrawerFlag(decimal curAmountTendered)
        {
            bool CashDrawerOpen = false;
            if ((SystemSettings.GetSettingValue("ManageCashDrawer", Session._LocationCode) == "1"))
            {
                CashDrawerInfoDto cashDrawer = APILayer.GetCashDrawerInfo(Session.ComputerName, Session.CurrentEmployee.LoginDetail.EmployeeCode, 1);
                if (cashDrawer != null)
                {
                    if (cashDrawer.cash_register_status)
                    {
                        if (cashDrawer.cash_register_lock)
                        {
                            if (curAmountTendered > 0)
                            {
                                //if (cashDrawer.Cash_Register_Locked_By.Trim() == Session.CurrentEmployee.LoginDetail.EmployeeCode.Trim())
                                //{
                                    CustomMessageBox.Show(MessageConstant.CashDrowerLocked, CustomMessageBox.Buttons.OK, CustomMessageBox.Icon.Info);
                                    CashDrawerOpen = false;
                                    return CashDrawerOpen;
                                //}
                                //else
                                //{
                                //    CashDrawerOpen = false;
                                //}
                            }
                            else
                            {
                                CashDrawerOpen = false;
                            }
                        }
                        else
                        {

                            CashDrawerOpen = true;
                        }
                    }
                    else
                    {
                        CashDrawerOpen = false;
                    }

                }
                else
                {
                    CashDrawerOpen = false;
                }

            }
            else
            {
                CashDrawerOpen = false;
            }
            return CashDrawerOpen;


        }
        #endregion

    }
}
