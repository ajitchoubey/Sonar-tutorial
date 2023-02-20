using Jublfood.AppLogger;
using JublFood.POS.App.API;
using JublFood.POS.App.BusinessLayer;
using JublFood.POS.App.Cache;
using JublFood.POS.App.Class;
using JublFood.POS.App.Models.Customer;
using JublFood.POS.App.Models.Catalog;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using JublFood.POS.App.Models.Employee;
using System.Diagnostics;
using System.IO;
using System.Drawing;

namespace JublFood.POS.App
{
    public partial class UC_Customer_OrderMenu : UserControl
    {

        public UC_Customer_OrderMenu()
        {
            try
            {
                InitializeComponent();
                SetAllbuttonText();
                cmdRemake.Enabled = false; /*Abhishek*/

                if (this.Parent != null)
                {
                    Form frm = this.Parent.FindForm();
                    if (frm.Name != "frmOrder")
                    {
                        cmdSearch.Enabled = false;
                    }
                }

            }
            catch (Exception ex)
            {
                Logger.Trace("ERROR", "uc_customer_ordermenu-uc_customer_ordermenu(): " + ex.Message, ex, true);
            }
        }
        public UC_FunctionList ucFunctionList;
        public UC_InformationList ucInformationList;
        public string CurrentForm;


        private void UC_Customer_OrderMenu_Click(object sender, EventArgs e)
        {
            this.Height = 55;
        }

        public void SetAllbuttonText()
        {
            string labelText = null;
            if (Common.DictAllButtonText.TryGetValue(LanguageConstant.cintExit, out labelText))
            {
                cmdExit.Text = labelText;
            }
            if (Session.CurrentEmployee != null && Session.CurrentEmployee.LoginDetail != null)
            {
                cmdLogin.Text = "Log Out";
            }
            else if (Common.DictAllButtonText.TryGetValue(LanguageConstant.cintLogin, out labelText))
            {
                cmdLogin.Text = labelText;
            }
            else
            {
                cmdLogin.Text = "Log In";
            }
            if (Common.DictAllButtonText.TryGetValue(LanguageConstant.cintModify, out labelText))
            {
                cmdChangeOrders.Text = labelText;
            }
            if (Common.DictAllButtonText.TryGetValue(LanguageConstant.cintCoupons, out labelText))
            {
                cmdOrderCoupons.Text = labelText;
            }
            if (Common.DictAllButtonText.TryGetValue(LanguageConstant.cintTimedOrders, out labelText))
            {
                cmdTimedOrders.Text = labelText;
            }
            if (Common.DictAllButtonText.TryGetValue(LanguageConstant.cintFunctions, out labelText))
            {
                cmdFunctions.Text = labelText;
            }
            if (Common.DictAllButtonText.TryGetValue(LanguageConstant.cintInformation, out labelText))
            {
                cmdInformation.Text = labelText;
            }
            if (Common.DictAllButtonText.TryGetValue(LanguageConstant.cintHistory, out labelText))
            {
                cmdHistory.Text = labelText;
            }
            //if (Common.DictAllButtonText.TryGetValue(LanguageConstant.cintCheckGiftCardBalance, out labelText))
            //{
            //    cmdSearch.Text = labelText;
            //}

        }
        public void SetButtonText(string name)
        {
            cmdDelivery_Info.Text = name;
            if (name == "Order")
            {
                cmdDelivery_Info.Image = Properties.Resources._193;
            }
            else if (name == "Customer")
            {
                cmdDelivery_Info.Image = Properties.Resources._95;
            }
        }

        public void ConvertExittoCancel()
        {
            string labelText = null;
            if (Common.DictAllButtonText.TryGetValue(LanguageConstant.cintCancel, out labelText))
            {
                if (cmdExit.Text != labelText)
                {
                    cmdExit.Text = labelText;
                    cmdExit.Image = Properties.Resources._92;
                }
            }
        }
        public void HandleRemakeButton(bool blnEnable)
        {
            cmdRemake.Enabled = blnEnable;
        }
        public void HandleModifyButton(bool blnEnable)
        {
            cmdChangeOrders.Enabled = blnEnable;
        }
        private void cmdExit_Click(object sender, EventArgs e)
        {
            try
            {
                HideFunctionandInformationMenu();
                Form CurrentForm = this.Parent.FindForm();
                Session.IsTimerStarted = false;
                bool ReasonConfirmed = false;
                Session.Upsellcnt = 0;
                if (Session.CurrentEmployee != null && Session.cart != null && Session.CartInitiated)
                {
                    frmReason objfrmReason = new frmReason(Convert.ToInt32(enumReasonGroupID.AbandonOrder));
                    objfrmReason.isExit = false;
                    objfrmReason.ShowDialog();
                    ReasonConfirmed = objfrmReason.isExit;
                    if (!ReasonConfirmed) return;
                }

                if (cmdExit.Text.Trim() == APILayer.GetCatalogText(LanguageConstant.cintCancel))
                {
                    if (!ReasonConfirmed)
                    {
                        if (CustomMessageBox.Show(APILayer.GetCatalogText(LanguageConstant.cintMSGCancelCurrentOrder), CustomMessageBox.Buttons.YesNo, CustomMessageBox.Icon.Question) == DialogResult.Yes)
                        {
                            if (OrderFunctions.AbandonOrder())
                            {
                                Session.FormClockOpened = false;

                            }
                            UserFunctions.GoToStartup(CurrentForm);
                        }
                    }
                    else
                    {
                        if (OrderFunctions.AbandonOrder())
                        {
                            Session.FormClockOpened = false;

                        }
                        UserFunctions.GoToStartup(CurrentForm);
                    }
                }
                else
                {
                    if (CustomMessageBox.Show(APILayer.GetCatalogText(LanguageConstant.cintMSGExitOrderRoutine), CustomMessageBox.Buttons.YesNo, CustomMessageBox.Icon.Question) == DialogResult.Yes)
                    {
                        Session.ExitApplication = true;
                        Application.Exit();
                    }



                }
            }
            catch (Exception ex)
            {
                Logger.Trace("ERROR", "uc_customer_ordermenu-cmdexit_click(): " + ex.Message, ex, true);
            }

        }

        private void cmdFunctions_Click(object sender, EventArgs e)
        {
            try
            {
                ucInformationList.Visible = false;
                ucFunctionList.Visible = !ucFunctionList.Visible;
            }
            catch (Exception ex)
            {
                Logger.Trace("ERROR", "uc_customer_ordermenu-cmdfunctions_click(): " + ex.Message, ex, true);
            }
        }

        private void cmdChangeOrders_Click(object sender, EventArgs e)
        {
            Session.PreventItemwiseUpsell = true;
            try
            {
                HideFunctionandInformationMenu();
                Button btn;
                btn = sender as Button;
                if (btn.Text != "Order")
                {
                    //Changes by Vikas Saraswat
                    using (frmModify objfrmModify = new frmModify())
                    {
                        objfrmModify.ShowDialog();
                        Session.RefreshFromModifyForOrder = true;
                        Session.RefreshFromModifyForCustomer = true;

                        //if (Session.pblnModifyingOrder)
                        //{
                        Form frm = this.Parent.FindForm();
                        if (Session.pblnModifyingOrder && objfrmModify.goPayment)
                        {
                            if (frm != null)
                                frm.Hide();
                            frmPayment frmPayment = new frmPayment();
                            frmPayment.Show();
                        }
                        else if (objfrmModify.logout)
                        {
                            if (frm != null)
                                frm.Hide();
                            UserFunctions.GoToStartup(frm);
                        }
                        else
                        {
                            if (Session.pblnModifyingOrder && objfrmModify.cmdOrderModify == true)
                            {
                                if (frm.Text == "Customer")
                                {
                                    cmdDelivery_Info_Click(cmdDelivery_Info, new EventArgs());
                                }
                                else
                                {

                                    Form frmObj = Application.OpenForms["frmOrder"];
                                    if (frmObj != null)
                                    {
                                        ((frmOrder)frmObj).RefreshCartUI();
                                    }
                                }
                            }
                        }

                        //}
                    }

                }
                else
                {

                    ((Form)this.Parent).Close();

                }
            }
            catch (Exception ex)
            {
                Logger.Trace("ERROR", "uc_customer_ordermenu-cmdchangeorders_click(): " + ex.Message, ex, true);
            }
            Session.PreventItemwiseUpsell = false;
        }

        public void cmdDelivery_Info_Click(object sender, EventArgs e)
        {
            try
            {
                HideFunctionandInformationMenu();
                Form frm = this.Parent.FindForm();
                if (frm.Text.ToUpper() == "CUSTOMER")
                {
                    //Session.IsTimerStarted = true;

                   
                    if ((SystemSettings.GetSettingValue("CustomerValidationBeforeOrder", Session._LocationCode) == "1"))
                    {
                        if (string.IsNullOrWhiteSpace(Session.selectedOrderType))
                        {
                            CustomMessageBox.Show(APILayer.GetCatalogText(LanguageConstant.cintMSGChooseOrderType), CustomMessageBox.Buttons.OK, CustomMessageBox.Icon.Exclamation);
                            return;
                        }

                        if (!((frmCustomer)frm).ValidateCustomerInfo1())
                            return;


                        //if (Session.cart.Customer != null)
                        //{
                        //    if(Session.selectedOrderType=="D" || Session.selectedOrderType == "P")
                        //    {
                        //        if(Session.cart.Customer.Phone_Number ==null || Session.cart.Customer.Phone_Number.Length <= Session.MinPhoneDigits)
                        //        {
                        //            CustomMessageBox.Show(MessageConstant.AllYellowFieldNotSelected, CustomMessageBox.Buttons.OK, CustomMessageBox.Icon.Exclamation);
                        //            return;
                        //        }
                        //        if (Session.cart.Customer.Name == null || Session.cart.Customer.Name=="")
                        //        {
                        //            CustomMessageBox.Show(MessageConstant.AllYellowFieldNotSelected, CustomMessageBox.Buttons.OK, CustomMessageBox.Icon.Exclamation);
                        //            return;
                        //        }

                        //    }
                        //    else if (Session.selectedOrderType == "I" || Session.selectedOrderType == "C")
                        //    {
                        //        if (Session.cart.Customer.Name == null || Session.cart.Customer.Name == "")
                        //        {
                        //            CustomMessageBox.Show(MessageConstant.AllYellowFieldNotSelected, CustomMessageBox.Buttons.OK, CustomMessageBox.Icon.Exclamation);
                        //            return;
                        //        }
                        //    }

                        //}
                        //else
                        //{
                        //    CustomMessageBox.Show(MessageConstant.AllYellowFieldNotSelected, CustomMessageBox.Buttons.OK, CustomMessageBox.Icon.Exclamation);
                        //    return;
                        //}
                    }

                    #region Customer Collection

                    if (Session.cart.Customer != null && (!string.IsNullOrEmpty(Session.cart.Customer.Phone_Number) && Session.cart.Customer.Phone_Number.Length == 10))
                    {
                        if (((frmCustomer)frm).CustomerResult != null && ((frmCustomer)frm).CustomerResult.CustomerDetail != null)
                        {
                            if (((frmCustomer)frm).CustomerResult.CustomerDetail.CustomerCode > 0)
                            {
                                Session.cart.Customer.Customer_Code = ((frmCustomer)frm).CustomerResult.CustomerDetail.CustomerCode;
                            }
                            else
                            {
                                Session.cart.Customer.Customer_Code = 0;
                            }
                        }
                        else
                        {
                            Session.cart.Customer.Customer_Code = 0;
                        }

                        if (Session.StreetsAPIResponse != null && Session.StreetsAPIResponse.Count > 0 && !string.IsNullOrEmpty(Session.cart.Customer.Customer_Street_Name))
                        {
                            StreetLookUp street = Session.StreetsAPIResponse.Where(x => x.StreetName.Trim() == Session.cart.Customer.Customer_Street_Name.Trim()).FirstOrDefault();

                            if(street == null)
                            {
                                StreetLookUpRequest streetLookUpRequest = new StreetLookUpRequest();
                                streetLookUpRequest.LocationCode = Session._LocationCode;
                                streetLookUpRequest.StreetName = "%%";
                                StreetLookUpResponse streetLookUpResponse = APILayer.StreetLookUp(APILayer.CallType.POST, streetLookUpRequest);

                                if (streetLookUpResponse != null && streetLookUpResponse.Result != null && streetLookUpResponse.Result.Streets != null)
                                {
                                    street = streetLookUpResponse.Result.Streets.Where(x => x.StreetName.Trim() == Session.cart.Customer.Customer_Street_Name.Trim()).FirstOrDefault();
                                }
                            }

                            if (street != null)
                            {
                                Session.cart.Customer.Street_Code = street.StreetCode;
                                //Session.cart.Customer.Cross_Street_Code = street.StreetCode;
                            }
                            else if (!string.IsNullOrEmpty(Session.cart.Customer.Customer_Street_Name.Trim()))
                            {
                                CustomMessageBox.Show(MessageConstant.StreetNotExists, CustomMessageBox.Buttons.OK, CustomMessageBox.Icon.Warning);
                                return;
                            }
                        }

                        Session.cart.Customer.Added_By = Session.CurrentEmployee.LoginDetail.UserID;
                        Session.cart.Customer.Tax_Exempt1 = false;
                        Session.cart.Customer.Tax_Exempt1 = false;
                        Session.cart.Customer.Tax_Exempt1 = false;
                        Session.cart.Customer.Tax_Exempt1 = false;

                        Session.cart.Customer.Tax_ID1 = "";
                        Session.cart.Customer.Tax_ID2 = "";
                        Session.cart.Customer.Tax_ID3 = "";
                        Session.cart.Customer.Tax_ID4 = "";

                        Session.cart.Customer.Finance_Charge_Rate = 0.0d;
                        if (string.IsNullOrEmpty(Session.cart.Customer.Phone_Ext))
                        {
                            Session.cart.Customer.Phone_Ext = string.Empty;
                        }

                        if (string.IsNullOrEmpty(Session.cart.Customer.Address_Type))
                        {
                            Session.cart.Customer.Address_Type = string.Empty;
                        }
                        if (string.IsNullOrEmpty(Session.cart.Customer.Address_Line_2))
                        {
                            Session.cart.Customer.Address_Line_2 = string.Empty;
                        }
                        if (string.IsNullOrEmpty(Session.cart.Customer.Address_Line_3))
                        {
                            Session.cart.Customer.Address_Line_3 = string.Empty;
                        }
                        if (string.IsNullOrEmpty(Session.cart.Customer.Address_Line_4))
                        {
                            Session.cart.Customer.Address_Line_4 = string.Empty;
                        }
                        if (string.IsNullOrEmpty(Session.cart.Customer.gstin_number))
                        {
                            Session.cart.Customer.gstin_number = string.Empty;
                        }
                        if (string.IsNullOrEmpty(Session.cart.Customer.DriverComments))
                        {
                            Session.cart.Customer.DriverComments = string.Empty;
                        }
                        if (string.IsNullOrEmpty(Session.cart.Customer.Suite))
                        {
                            Session.cart.Customer.Suite = string.Empty;
                        }
                        if (string.IsNullOrEmpty(Session.cart.Customer.Manager_Notes))
                        {
                            Session.cart.Customer.Manager_Notes = string.Empty;
                        }
                        if (string.IsNullOrEmpty(Session.cart.Customer.Comments))
                        {
                            Session.cart.Customer.Comments = string.Empty;
                        }

                        #endregion

                    }

                    //var objfrmOrder = new frmOrder();
                    //objfrmOrder.Closed += (s, args) => frm.Close();
                    //objfrmOrder.Show();

                    Form frmObj = Application.OpenForms["frmOrder"];
                    if (frmObj != null)
                    {
                        ((frmOrder)frmObj).Show();
                    }
                    else
                    {
                        frmOrder frmOrder = new frmOrder();
                        frmOrder.Show();
                    }
                    frm.Hide();


                    // one customer
                    UserFunctions.OpenOneCustomer(Session.cart.Customer.Phone_Number + " " + 2);
                }
                else
                {


                    //var objfrmCustomer = new frmCustomer();
                    //objfrmCustomer.Closed += (s, args) => frm.Close();
                    //objfrmCustomer.Show();

                    Form frmObj = Application.OpenForms["frmCustomer"];
                    if (frmObj != null)
                    {
                        ((frmCustomer)frmObj).Show();
                    }
                    else
                    {
                        frmCustomer frmCustomer = new frmCustomer();
                        frmCustomer.Show();
                    }
                    frm.Hide();


                    // one customer
                    UserFunctions.OpenOneCustomer(Session.cart.Customer.Phone_Number + " " + 1);                  
                }
            }
            catch (Exception ex)
            {
                Logger.Trace("ERROR", "uc_customer_ordermenu-cmddelivery_info_click(): " + ex.Message, ex, true);
            }
        }

        private void cmdTimeClock_Click(object sender, EventArgs e)
        {
            try
            {
                HideFunctionandInformationMenu();
                using (frmTimeClock objfrmTimeClock = new frmTimeClock())
                {
                    objfrmTimeClock.ShowDialog();
                    Form frm = this.Parent.FindForm();
                    if (frm.Text == "Customer")
                        ((frmCustomer)frm).UpdateUseronHeader();
                    else if (frm.Text == "Order")
                        ((frmOrder)frm).UpdateUseronHeader();

                }
            }
            catch (Exception ex)
            {
                Logger.Trace("ERROR", "uc_customer_ordermenu-cmdtimeclock_click(): " + ex.Message, ex, true);
            }
        }

        private void cmdInformation_Click(object sender, EventArgs e)
        {
            Int16 Result = 0;
            CashDrorStatusRequest CashDrorStatus = new CashDrorStatusRequest();
            try
            {

                ucFunctionList.Visible = false;

                ucInformationList.Visible = !ucInformationList.Visible;
                //CashRegister
                CashDrorStatus.Workstation_Name = Session.ComputerName;
                CashDrorStatus.Employee_Code = Session.CurrentEmployee.LoginDetail.EmployeeCode;
                CashDrorStatus.Flag = 0;
                if ((SystemSettings.GetSettingValue("ManageCashDrawer", Session._LocationCode) == "1"))
                {
                    Result = APILayer.CashDrawerStatus(CashDrorStatus.Workstation_Name, CashDrorStatus.Employee_Code, CashDrorStatus.Flag);
                    if (Result == 0)
                        ucInformationList.HandleCashDrawerClick(false);
                    else
                        ucInformationList.HandleCashDrawerClick(true);
                }
                else
                {
                    ucInformationList.HandleCashDrawerClick(false);
                }
            }
            catch (Exception ex)
            {
                Logger.Trace("ERROR", "uc_customer_ordermenu-cmdinformation_click(): " + ex.Message, ex, true);
            }
        }

        private void cmdTimedOrders_Click(object sender, EventArgs e)
        {
            try
            {
                HideFunctionandInformationMenu();
                using (frmTimedOrders objfrmTimedOrders = new frmTimedOrders())
                {
                    objfrmTimedOrders.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                Logger.Trace("ERROR", "uc_customer_ordermenu-cmdtimedorders_click(): " + ex.Message, ex, true);
            }
        }
        private void btn_exit_Click(object sender, EventArgs e)
        {
            try
            {
                //exit
                //((Form)this.TopLevelControl).Close();                
                //frmLogin objfrmLogin = new frmLogin();
                //objfrmLogin.Show();
                //this.Parent.FindForm().Close();
                UserFunctions.GoToStartup(this.Parent.FindForm());
            }
            catch (Exception ex)
            {
                Logger.Trace("ERROR", "uc_customer_ordermenu-btn_exit_click(): " + ex.Message, ex, true);
            }
        }

        private void cmdHistory_Click(object sender, EventArgs e)
        {
            Session.PreventItemwiseUpsell = true;
            try
            {
                HideFunctionandInformationMenu();

                frmHistory frmHistory = new frmHistory();

                frmHistory.ShowDialog();
                Session.RefreshFromHistoryForOrder = true;
                Session.RefreshFromHistoryForCustomer = true;

                Form frm = this.Parent.FindForm();
                if (frm.Text == "Customer")
                {
                    cmdDelivery_Info_Click(cmdDelivery_Info, new EventArgs());
                }
                else
                {

                    Form frmObj = Application.OpenForms["frmOrder"];
                    if (frmObj != null)
                    {
                        ((frmOrder)frmObj).RefreshCartUI();
                        ((frmOrder)frmObj).CloseToppings();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Trace("ERROR", "uc_customer_ordermenu-cmdhistory_click(): " + ex.Message, ex, true);
            }
            Session.PreventItemwiseUpsell = false;
        }

        private void cmdLogin_Click(object sender, EventArgs e)
        {
            try
            {
                HideFunctionandInformationMenu();
                Form CurrentForm = this.Parent.FindForm();

                Session.IsTimerStarted = false;
                cmdLogin.Text = "Log In";
                Session.CurrentEmployee = null;
                Session.FormClockOpened = false;
                if (CurrentForm.Name == "frmModify")
                {
                    Form frmObj = Application.OpenForms["frmModify"];
                    if (frmObj != null)
                    {
                        ((frmModify)frmObj).logout = true;
                        ((frmModify)frmObj).Close();
                    }
                }

                else
                {
                    UserFunctions.GoToStartup(CurrentForm);
                }

                //for (int index = Application.OpenForms.Count - 1; index >= 0; index--)
                //{
                //    Application.OpenForms[index].Close();
                //}
                //frmCustomer frmCust = new frmCustomer();
                //frmCust.Show();
                //frmLogin frm = new frmLogin();
                //frm.ShowDialog();
            }
            catch (Exception ex)
            {
                Logger.Trace("ERROR", "uc_customer_ordermenu-cmdlogin_click(): " + ex.Message, ex, true);
            }
        }
        public void DisableModifyInTrainingMode()
        {
            if (SystemSettings.settings.pblnTrainingMode)
            {
                cmdChangeOrders.Enabled = false;
            }
        }

        private void cmdOrderCoupons_Click(object sender, EventArgs e)
        {
            try
            {
                HideFunctionandInformationMenu();
                ((frmOrder)this.Parent.FindForm()).OrderCoupons();
            }
            catch (Exception ex)
            {
                Logger.Trace("ERROR", "uc_customer_ordermenu-cmdordercoupons_click(): " + ex.Message, ex, true);
            }
        }


        public void HandleHistoryButton(bool blnEnable)
        {
            cmdHistory.Enabled = blnEnable;
        }


        private void HideFunctionandInformationMenu()
        {
            ucInformationList.Visible = false;
            ucFunctionList.Visible = false;
        }

        private void cmdRemake_Click(object sender, EventArgs e)
        {
            Session.PreventItemwiseUpsell = true;
            bool blnLoginSuccessful = false;
            EmployeeResult oldLoginEmployee;
            
            //oldLoginEmployee = Session.CurrentEmployee.LoginDetail;

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
                    if (Session.CurrentEmployee != null && !string.IsNullOrEmpty(Session.CurrentEmployee.LoginDetail.EmployeeCode) && Session.CurrentEmployee.LoginDetail.blnVariableCoupons)
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
            {
                frmRemake frmRemake = new frmRemake();
                frmRemake.ShowDialog();
                if(Session.RemakeOrder == true )
                {
                    Session.RefreshFromRemakeForOrder = true;
                    
                }
                else
                {
                    Session.RefreshFromRemakeForOrder = false;
                    
          
                }
               

                Form frm = this.Parent.FindForm();
                if (frm.Text == "Customer")
                {
                    cmdDelivery_Info_Click(cmdDelivery_Info, new EventArgs());
                }
                else
                {
                    Form frmObj = Application.OpenForms["frmOrder"];
                    if (frmObj != null)
                    {
                        ((frmOrder)frmObj).RefreshCartUI();
                        ((frmOrder)frmObj).SelectOrderType();
                    }
                }

            }
            else
            {
                Form frm = this.Parent.FindForm();
                if (frm.Text == "Customer")
                {
                    Form frmObj = Application.OpenForms["frmCustomer"];
                    if (frmObj != null)
                    {
                        ((frmCustomer)frmObj).RefreshCustomerHandling();
                    }
                }
            }

            Session.PreventItemwiseUpsell = false;
        }

        private void cmdSearch_Click(object sender, EventArgs e)
        {
            Form frm = this.Parent.FindForm();
            if (frm.Name  == "frmOrder")
            {
                Form frmObj = Application.OpenForms["frmOrder"];
                if (frmObj != null)
                {
                    frmSearch frmSearch = new frmSearch(((frmOrder)frmObj).VegChecked);
                    frmSearch.ShowDialog();
                }
            }
        }
    }

}

