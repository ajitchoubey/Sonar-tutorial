using Jublfood.AppLogger;
using JublFood.POS.App.API;
using JublFood.POS.App.BusinessLayer;
using JublFood.POS.App.Cache;
using JublFood.POS.App.Class;
using JublFood.POS.App.Models.Cart;
using JublFood.POS.App.Models.Catalog;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace JublFood.POS.App
{
    public partial class frmTimedOrders : Form
    {
        public string date1;
        public string date2;
        public string choosenDate;
        private DateTime _pdtmServerDateTime= Settings.Settings.GetServerDateTime();

        
        public DateTime pdtmServerDateTime
        {

            get { return _pdtmServerDateTime; }

        }
        public frmTimedOrders()
        {
            InitializeComponent();
            SetControlText();
            SetButtonText();
            CheckTrainningMode();
            // public short DeliveryLeadTime { get; set; }
            //public short CarryOutLeadTime { get; set; }
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            Cart cartLocal = null;
            if (Session.cart == null)
            {
                cartLocal = (new Cart().GetCart());
            }
            else
            {
                cartLocal = Session.cart;
            }
            cartLocal.cartHeader.Delayed_Date = Convert.ToDateTime("01-01-0001");


            //   if (Session.pblnModifyingOrder)
            //   {
            //       cartLocal.cartHeader.Order_Date = Session.SystemDate.Date;
            //       cartLocal.cartHeader.Actual_Order_Date = _pdtmServerDateTime;
            //       cartLocal.cartHeader.Kitchen_Display_Time = _pdtmServerDateTime;
            //       cartLocal.cartHeader.Delayed_Order = 0;
            //       cartLocal.cartHeader.Delayed_Same_Day = false;
            //       Session.pblnModifyDelayedTime = true;
            //}
            frmOrder frmord = null;
            foreach (Form form in Application.OpenForms)
            {
                if (form.Name == "frmOrder")
                    frmord = (frmOrder)form;
            }
            if (frmord != null)
            {

                frmord.RefreshCartUI();
            }
            this.Close();
        }
        private void LoadDefault()
        {
            tdbtOrderTime.CustomFormat = "HH:mm";
            tdbtLeadTime.CustomFormat = "HH:mm";
            tdbtOrderTime.Format = DateTimePickerFormat.Custom;
            tdbtLeadTime.Format = DateTimePickerFormat.Custom;
        }

        private void LoadOrderAndKitchenDatetime()
        {
            try
            {
                int bytMinutes = 0;
                DateTime strCurrentDateTime = _pdtmServerDateTime;
                Cart cartLocal = null;
                if (Session.cart == null)
                {
                    cartLocal = (new Cart().GetCart());
                }
                else
                {
                    cartLocal = Session.cart;
                }

                if (cartLocal.cartHeader.Delayed_Date.GetHashCode() == 0)
                {
                    strCurrentDateTime = Convert.ToDateTime(pdtmServerDateTime);
                }
                else
                {
                    strCurrentDateTime = cartLocal.cartHeader.Delayed_Date;
                }
                tdbdOrderDate.Value = strCurrentDateTime;

                if (cartLocal.cartHeader.Delayed_Date.GetHashCode() == 0)
                {

                    if (Session.selectedOrderType == "D")
                    {
                        tdbtOrderTime.Value = strCurrentDateTime.AddMinutes(SystemSettings.appControl.DeliveryLeadTime);
                        tdbtLeadTime.Value = tdbtOrderTime.Value.AddMinutes(-SystemSettings.appControl.DeliveryLeadTime);
                    }
                    else
                    {
                        tdbtOrderTime.Value = strCurrentDateTime.AddMinutes(SystemSettings.appControl.CarryOutLeadTime);
                        tdbtLeadTime.Value = tdbtOrderTime.Value.AddMinutes(-SystemSettings.appControl.CarryOutLeadTime);
                    }
                    bytMinutes = tdbtOrderTime.Value.Minute;

                    tdbtOrderTime.Value = tdbtOrderTime.Value.AddMinutes(((bytMinutes / 15) + 1) * 15 - bytMinutes);
                    tdbtLeadTime.Value = tdbtLeadTime.Value.AddMinutes(((bytMinutes / 15) + 1) * 15 - bytMinutes);
                    Check15_HourLeadTimes();
                    Check_Date(ref tdbtOrderTime);
                }
                else
                {
                    tdbtLeadTime.Value = cartLocal.cartHeader.Kitchen_Display_Time;
                    tdbtOrderTime.Value = strCurrentDateTime;
                }
                if (cartLocal.cartHeader.Delayed_Date.GetHashCode() != 0)
                {
                    cmdDown.Enabled = true;
                    cmdDownHour.Enabled = true;
                    cmdDown15.Enabled = true;
                    if (cartLocal.cartHeader.Delayed_Date > strCurrentDateTime)
                    {
                        if (cartLocal.cartHeader.Delayed_Date.AddDays(1) > strCurrentDateTime)
                        {
                            cmdDownDay.Enabled = true;
                        }
                        else
                        {
                            cmdDownDay.Enabled = false;
                        }
                        if (cartLocal.cartHeader.Delayed_Date.AddDays(7) > strCurrentDateTime)
                        {
                            cmdDownWeek.Enabled = true;
                        }
                        else
                        {
                            cmdDownWeek.Enabled = false;
                        }
                        if (cartLocal.cartHeader.Delayed_Date.AddMonths(1) > strCurrentDateTime)
                        {
                            cmdDownMonth.Enabled = true;
                        }
                        else
                        {
                            cmdDownMonth.Enabled = false;
                        }
                    }
                    else
                    {
                        cmdDownDay.Enabled = false;
                        cmdDownWeek.Enabled = false;
                        cmdDownMonth.Enabled = false;
                    }
                }

            }
            catch (Exception ex)
            {
                Logger.Trace("ERROR", "frmTimedOrders-loadorderandkitchdatetime(): " + ex.Message, ex, true);
            }
        }
        private void SetControlText()
        {
            //BAL obj = new BAL();
            //List<FormField> listFormField = obj.GetControlText("frmtimedorders");
            Session.catalogControlText = APILayer.GetControlText("frmtimedorders");
            foreach (Control ctl in this.pnl_TimedOrders.Controls)
            {
                if (ctl is Label)
                {
                    foreach (CatalogControlText formField in Session.catalogControlText)
                    {
                        if (ctl.Name.Substring(4, ctl.Name.Length - 4) == formField.Field_Name)
                        {
                            ctl.Text = formField.text;
                        }
                    }
                }
            }


        }
        private void SetButtonText()
        {
            string labelText = null;
            if (Common.DictAllButtonText.TryGetValue(LanguageConstant.cintClose, out labelText))
            {
                cmdClose.Text = labelText;
            }
            if (Common.DictAllButtonText.TryGetValue(LanguageConstant.cintCancel, out labelText))
            {
                cmdCancel.Text = labelText;
            }
            if (Common.DictAllButtonText.TryGetValue(LanguageConstant.cintReset, out labelText))
            {
                cmdResetDate.Text = labelText;
            }
            if (Common.DictAllButtonText.TryGetValue(LanguageConstant.cintReset, out labelText))
            {
                cmdResetLeadTime.Text = labelText;
            }
            if (Common.DictAllButtonText.TryGetValue(LanguageConstant.cintReset, out labelText))
            {
                cmdResetTime.Text = labelText;
            }

        }
        private void frmTimedOrders_Load(object sender, EventArgs e)
        {
            LoadDefault();
            LoadOrderAndKitchenDatetime();
            CheckLeadTime(tdbdOrderDate.Value);
            tdbdOrderDate.MinDate = Session.SystemDate;
            tdbdOrderDate.Format = DateTimePickerFormat.Custom;
            tdbdOrderDate.CustomFormat = "MM/dd/yyyy";
        }
        DateTime RoundUp(DateTime dt, TimeSpan d)
        {
            return new DateTime((dt.Ticks + d.Ticks - 1) / d.Ticks * d.Ticks, dt.Kind);
        }

        private void Check_Date(ref DateTimePicker tdbtTimeControl)
        {
            //    If CDate(Format$(tdbdOrderDate.Text, strShortDateFormat) &" " & Format$(tdbtTimeControl.Value, TimeFormat)) <= _
            //CDate(Format$(pobjBackoGeneral.GetServerDateTime, strShortDateFormat) & " " & _
            //Format$(DateAdd("n", -pudtSystem_Settings.bytCarryOutLeadTime, pobjBackoGeneral.GetServerDateTime), TimeFormat)) _
            try
            {
                if (Convert.ToDateTime(tdbdOrderDate.Text + " " + tdbtTimeControl.Text) <= pdtmServerDateTime.AddMinutes(-SystemSettings.appControl.CarryOutLeadTime))
                {
                    cmdDown15.Enabled = false;
                    cmdDown.Enabled = false;
                    cmdDownHour.Enabled = false;
                    cmdDownDay.Enabled = false;
                    cmdDownWeek.Enabled = false;
                    cmdDownMonth.Enabled = false;
                    if (Session.selectedOrderType == "D")
                    {
                        //        tdbtTimeControl.Text = Format$(DateAdd("n", pudtSystem_Settings.bytDeliveryLeadTime, pobjBackoGeneral.GetServerDateTime), TimeFormat)
                        //tdbdOrderDate.Value = DateAdd("n", pudtSystem_Settings.bytDeliveryLeadTime, pobjBackoGeneral.GetServerDateTime)
                        tdbtTimeControl.Text = pdtmServerDateTime.AddMinutes(SystemSettings.appControl.DeliveryLeadTime).ToShortTimeString();
                        tdbdOrderDate.Value = pdtmServerDateTime.AddMinutes(SystemSettings.appControl.DeliveryLeadTime);
                    }
                    else
                    {
                        tdbtTimeControl.Text = pdtmServerDateTime.AddMinutes(SystemSettings.appControl.CarryOutLeadTime).ToShortTimeString();
                        tdbdOrderDate.Value = pdtmServerDateTime.AddMinutes(SystemSettings.appControl.CarryOutLeadTime);
                    }
                    return;
                }
                cmdDown15.Enabled = true;
                cmdDown.Enabled = true;
                cmdDownHour.Enabled = true;
                DateTime temptdbdOrderDate = Convert.ToDateTime(tdbdOrderDate.Text);
                DateTime temppdtmServerDateTime = Convert.ToDateTime(pdtmServerDateTime.ToShortDateString());
                if (temptdbdOrderDate.AddDays(-1) >= temppdtmServerDateTime)
                {
                    cmdDownDay.Enabled = true;
                }
                else
                {
                    cmdDownDay.Enabled = false;
                }
                if (temptdbdOrderDate.AddDays(-7) >= temppdtmServerDateTime)
                {
                    cmdDownWeek.Enabled = true;
                }
                else
                {
                    cmdDownWeek.Enabled = false;
                }

                if (temptdbdOrderDate.AddMonths(-1) >= temppdtmServerDateTime)
                {
                    cmdDownMonth.Enabled = true;
                }
                else
                {
                    cmdDownMonth.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                Logger.Trace("ERROR", "frmTimedOrders-check_date(): " + ex.Message, ex, true);
            }
        }
        private bool CheckLeadTime(DateTime dteLeadTimeToCheck)
        {
            bool leadTime = false;
            DateTime dtePrevDayClosingTime = _pdtmServerDateTime;
            DateTime dteLeadTime = _pdtmServerDateTime;
            try
            {
                dtePrevDayClosingTime = APILayer.GetStoreClosingDateTime(SystemSettings.LocationCodes.LocationCode, tdbdOrderDate.Value.AddDays(-1).ToString());
                DateTime temptdbdOrderDate = Convert.ToDateTime(tdbdOrderDate.Text);
                DateTime temppdtmServerDateTime = Convert.ToDateTime(pdtmServerDateTime.ToShortDateString());
                if (temppdtmServerDateTime != temptdbdOrderDate)
                {
                    if (dteLeadTimeToCheck > dtePrevDayClosingTime && dteLeadTimeToCheck < Convert.ToDateTime(tdbdOrderDate.Text + " " + tdbtOrderTime.Text))
                    {
                        leadTime = true;
                    }
                    else
                    {
                        leadTime = false;
                    }
                }
                else
                {
                    if (dteLeadTimeToCheck > pdtmServerDateTime && dteLeadTimeToCheck <= Convert.ToDateTime(tdbdOrderDate.Text + " " + tdbtOrderTime.Text))
                    {
                        leadTime = true;
                    }
                    else
                    {
                        leadTime = false;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Trace("ERROR", "frmTimedOrders-checkleadtime(): " + ex.Message, ex, true);
            }
            return leadTime;
        }
        private void Check15_HourLeadTimes()
        {

            bool blnTimeOK = false;
            // blnTimeOK = CheckLeadTime(CDate(tdbdOrderDate.Text & " " & Format$(DateAdd("n", -1, tdbtLeadTime.Value), "hh:mm")))
            try
            {
                //Check to see if the leadtime -1 minutes is still good
                String hourMinute = tdbtLeadTime.Value.AddMinutes(-1).ToString("HH:mm");
                blnTimeOK = CheckLeadTime(Convert.ToDateTime(tdbdOrderDate.Text + " " + hourMinute));
                if (!blnTimeOK)
                {
                    cmdLeadTimeDown.Enabled = false;
                }
                else
                {
                    cmdLeadTimeDown.Enabled = true;
                }

                //Check to see if the leadtime -15 minutes is still good
                hourMinute = tdbtLeadTime.Value.AddMinutes(-15).ToString("HH:mm");
                blnTimeOK = CheckLeadTime(Convert.ToDateTime(tdbdOrderDate.Text + " " + hourMinute));
                if (!blnTimeOK)
                {
                    cmdLeadTimeDown15.Enabled = false;
                }
                else
                {
                    cmdLeadTimeDown15.Enabled = true;
                }

                //Check to see if the leadtime -60 minutes is still good
                hourMinute = tdbtLeadTime.Value.AddMinutes(-60).ToString("HH:mm");
                blnTimeOK = CheckLeadTime(Convert.ToDateTime(tdbdOrderDate.Text + " " + hourMinute));
                if (!blnTimeOK)
                {
                    cmdLeadTimeDownHour.Enabled = false;
                }
                else
                {
                    cmdLeadTimeDownHour.Enabled = true;
                }
                //Check to see if the leadtime +1 minutes is still good
                hourMinute = tdbtLeadTime.Value.AddMinutes(1).ToString("HH:mm");
                blnTimeOK = CheckLeadTime(Convert.ToDateTime(tdbdOrderDate.Text + " " + hourMinute));
                if (!blnTimeOK)
                {
                    cmdLeadTimeUP.Enabled = false;
                }
                else
                {
                    cmdLeadTimeUP.Enabled = true;
                }

                //Check to see if the leadtime +15 minutes is still good
                hourMinute = tdbtLeadTime.Value.AddMinutes(15).ToString("HH:mm");
                blnTimeOK = CheckLeadTime(Convert.ToDateTime(tdbdOrderDate.Text + " " + hourMinute));
                if (!blnTimeOK)
                {
                    cmdLeadTimeUp15.Enabled = false;
                }
                else
                {
                    cmdLeadTimeUp15.Enabled = true;
                }

                //Check to see if the leadtime +60 minutes is still good
                hourMinute = tdbtLeadTime.Value.AddMinutes(60).ToString("HH:mm");
                blnTimeOK = CheckLeadTime(Convert.ToDateTime(tdbdOrderDate.Text + " " + hourMinute));
                if (!blnTimeOK)
                {
                    cmdLeadTimeUpHour.Enabled = false;
                }
                else
                {
                    cmdLeadTimeUpHour.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                Logger.Trace("ERROR", "frmTimedOrders-check15_hourleadtime(): " + ex.Message, ex, true);
            }
        }

        private void tdbtLeadTime_ValueChanged(object sender, EventArgs e)
        {
            //if (tdbtLeadTime.Value.AddMinutes(-15) > DateTime.Now)
            //{
            //    cmdLeadTimeDown15.Enabled = true;
            //}
            //else
            //{
            //    cmdLeadTimeDown15.Enabled = false;
            //}
            //if (tdbtLeadTime.Value.AddHours(-1) > DateTime.Now)
            //{
            //    cmdLeadTimeDownHour.Enabled = true;
            //}
            //else
            //{
            //    cmdLeadTimeDownHour.Enabled = false;
            //}
        }

        private void cmdResetDate_Click(object sender, EventArgs e)
        {
            SystemSettings.LoadSettings(Session._LocationCode);
            cmdDownDay.Enabled = false;
            cmdDownWeek.Enabled = false;
            cmdDownMonth.Enabled = false;
            lblOrderDayOfWeek.Text = DateTime.Now.DayOfWeek.ToString();
            tdbdOrderDate.Value = pdtmServerDateTime;

            //  tdbdOrderDate.Value = pobjBackoGeneral.GetServerDateTime
        }

        private void cmdResetTime_Click(object sender, EventArgs e)
        {
            cmdDown.Enabled = false;
            cmdDown15.Enabled = false;
            cmdDownHour.Enabled = false;
            tdbtOrderTime.Value = Convert.ToDateTime(pdtmServerDateTime);
            Cart cartLocal = null;
            if (Session.cart == null)
            {
                cartLocal = (new Cart().GetCart());
            }
            else
            {
                cartLocal = Session.cart;
            }
            if (cartLocal.cartHeader.Order_Type_Code == "D")
            {
                tdbtLeadTime.Value = tdbtOrderTime.Value.AddMinutes(-SystemSettings.appControl.DeliveryLeadTime);
            }
            else
            {
                tdbtLeadTime.Value = tdbtOrderTime.Value.AddMinutes(-SystemSettings.appControl.CarryOutLeadTime);
            }
            //   tdbtOrderTime.Value = DateTime.Now;

            //cmdUp.Enabled = true;
            //cmdUp15.Enabled = true;
            //cmdUpHour.Enabled = true;
        }

        private void cmdResetLeadTime_Click(object sender, EventArgs e)
        {
            //  tdbtLeadTime.Value = tdbtOrderTime.Value.AddMinutes(-10);
            Cart cartLocal = null;
            if (Session.cart == null)
            {
                cartLocal = (new Cart().GetCart());
            }
            else
            {
                cartLocal = Session.cart;
            }
            if (Session.selectedOrderType == "D")
            {
                tdbtLeadTime.Value = tdbtOrderTime.Value.AddMinutes(-SystemSettings.appControl.DeliveryLeadTime);
            }
            else
            {
                tdbtLeadTime.Value = tdbtOrderTime.Value.AddMinutes(-SystemSettings.appControl.CarryOutLeadTime);
            }

            if (cmdDown.Enabled == false && cmdDown15.Enabled == false && cmdDownHour.Enabled == false)
            {
                cmdLeadTimeDown.Enabled = false;
                cmdLeadTimeUP.Enabled = false;
                cmdLeadTimeDown15.Enabled = false;
                cmdLeadTimeUp15.Enabled = false;
                cmdLeadTimeDownHour.Enabled = false;
                cmdLeadTimeUpHour.Enabled = false;
            }
            Check15_HourLeadTimes();
        }

        private void tdbdOrderDate_ValueChanged(object sender, EventArgs e)
        {
            lblOrderDayOfWeek.Text = tdbdOrderDate.Value.DayOfWeek.ToString();
            //if (tdbdOrderDate.Value.Date > DateTime.Now.Date.Date)
            //{
            //    cmdDownDay.Enabled = true;
            //    cmdDownWeek.Enabled = true;
            //    cmdDownMonth.Enabled = true;

            //    cmdDown.Enabled = true;
            //    cmdDown15.Enabled = true;
            //    cmdDownHour.Enabled = true;

            //    cmdLeadTimeDown.Enabled = true;
            //    cmdLeadTimeDown15.Enabled = true;
            //    cmdLeadTimeDownHour.Enabled = true;
            //}
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime dteDate1;
                DateTime dteDate2;
                string labelText = null;
                if (Common.DictAllButtonText.TryGetValue(LanguageConstant.cintMSGVerifyTimedDate, out labelText)) { }

                tdbtOrderTime.CustomFormat = "HH:mm";
                string datetime = tdbdOrderDate.Text + " " + tdbtOrderTime.Text;

                if (!string.IsNullOrWhiteSpace(datetime) && Convert.ToDateTime(datetime) > DateTime.Now)
                {
                    DialogResult dialogresult = CustomMessageBox.Show(tdbdOrderDate.Text + " " + tdbtOrderTime.Text + Environment.NewLine + labelText, CustomMessageBox.Buttons.YesNo, CustomMessageBox.Icon.Question);
                    if (dialogresult == DialogResult.Yes)
                    {
                        //  tdbtOrderTime.CustomFormat = "hh:mm";
                        CatalogDelayedOrder catalogDelayedOrder = APILayer.CheckCatalogDelayOrders(SystemSettings.LocationCodes.LocationCode, datetime);

                        Cart cartLocal = null;
                        if (Session.cart == null)
                        {
                            Session.cart = (new Cart()).GetCart();
                            cartLocal = (new Cart().GetCart());

                            if (catalogDelayedOrder.IsStoreOpened)
                            {
                                if (catalogDelayedOrder.IsAfterMidnight)
                                {
                                    cartLocal.cartHeader.Order_Date = tdbdOrderDate.Value.AddDays(-1).Date;
                                }
                                else
                                {
                                    cartLocal.cartHeader.Order_Date = tdbdOrderDate.Value.Date;
                                }
                                cartLocal.cartHeader.Delayed_Date = Convert.ToDateTime(tdbdOrderDate.Text + " " + tdbtOrderTime.Text);
                            }
                            else
                            {
                                if (catalogDelayedOrder.IsAfterMidnight)
                                {
                                    dteDate1 = tdbdOrderDate.Value.AddDays(-1);
                                    dteDate2 = tdbdOrderDate.Value;
                                }
                                else
                                {
                                    dteDate1 = tdbdOrderDate.Value;
                                    dteDate2 = tdbdOrderDate.Value.AddDays(1);
                                }
                                frmDetermineOrderDate frmDetermineOrderDate = new frmDetermineOrderDate();
                                frmDetermineOrderDate.date1 = dteDate1.ToShortDateString();
                                frmDetermineOrderDate.date2 = dteDate2.ToShortDateString();
                                frmDetermineOrderDate.ShowDialog();
                                cartLocal.cartHeader.Order_Date = Convert.ToDateTime(choosenDate).Date;
                                cartLocal.cartHeader.Delayed_Date = Convert.ToDateTime(tdbdOrderDate.Text + " " + tdbtOrderTime.Text);
                            }

                            if (cartLocal.cartHeader.Order_Date > SystemSettings.settings.pdtmSystem_Date)
                            {
                                cartLocal.cartHeader.Delayed_Same_Day = false;
                            }
                            else
                            {
                                cartLocal.cartHeader.Delayed_Same_Day = true;
                            }

                            //            If Format(tdbtLeadTime.Text, "AMPM") = Format(tdbtOrderTime.Text, "AMPM") Then
                            //    OrderCollection.Kitchen_Display_Time = Format(tdbdOrderDate.Text, strShortDateFormat) & " " & tdbtLeadTime.Text
                            //ElseIf Format(tdbtLeadTime.Text, "AMPM") = "AM" Then
                            //    OrderCollection.Kitchen_Display_Time = Format(tdbdOrderDate.Text, strShortDateFormat) & " " & tdbtLeadTime.Text
                            //ElseIf Format(tdbtLeadTime.Text, "AMPM") = "PM" Then
                            //    OrderCollection.Kitchen_Display_Time = Format(DateAdd("d", -1, tdbdOrderDate.Text), strShortDateFormat) & " " & tdbtLeadTime.Text
                            //End If
                            cartLocal.cartHeader.Kitchen_Display_Time = Convert.ToDateTime(tdbdOrderDate.Text + " " + tdbtLeadTime.Text);

                            if (cartLocal.cartHeader.Delayed_Date != DateTime.MinValue)
                                cartLocal.cartHeader.Delayed_Order = 1;

                                Session.cart = cartLocal;
                        }
                        else
                        {
                            cartLocal = (new Cart().GetCart());

                            cartLocal.Customer = Session.cart.Customer;

                            if (String.IsNullOrEmpty(Session.cart.cartHeader.CartId))
                            {
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
                                cartLocal.cartHeader.ODC_Tax = Session.ODC_Tax;
                            }
                            else
                            {
                                cartLocal.cartHeader = Session.cart.cartHeader;
                            }

                            
                            if (catalogDelayedOrder.IsStoreOpened)
                            {
                                if (catalogDelayedOrder.IsAfterMidnight)
                                {
                                    cartLocal.cartHeader.Order_Date = tdbdOrderDate.Value.AddDays(-1).Date;
                                }
                                else
                                {
                                    cartLocal.cartHeader.Order_Date = tdbdOrderDate.Value.Date;
                                }
                                cartLocal.cartHeader.Delayed_Date = Convert.ToDateTime(tdbdOrderDate.Text + " " + tdbtOrderTime.Text);
                            }
                            else
                            {
                                if (catalogDelayedOrder.IsAfterMidnight)
                                {
                                    dteDate1 = tdbdOrderDate.Value.AddDays(-1);
                                    dteDate2 = tdbdOrderDate.Value;
                                }
                                else
                                {
                                    dteDate1 = tdbdOrderDate.Value;
                                    dteDate2 = tdbdOrderDate.Value.AddDays(1);
                                }
                                frmDetermineOrderDate frmDetermineOrderDate = new frmDetermineOrderDate();
                                frmDetermineOrderDate.date1 = dteDate1.ToShortDateString();
                                frmDetermineOrderDate.date2 = dteDate2.ToShortDateString();
                                frmDetermineOrderDate.ShowDialog();
                                cartLocal.cartHeader.Order_Date = Convert.ToDateTime(choosenDate).Date;
                                cartLocal.cartHeader.Delayed_Date = Convert.ToDateTime(tdbdOrderDate.Text + " " + tdbtOrderTime.Text);
                            }

                            if (cartLocal.cartHeader.Order_Date > SystemSettings.settings.pdtmSystem_Date)
                            {
                                cartLocal.cartHeader.Delayed_Same_Day = false;
                            }
                            else
                            {
                                cartLocal.cartHeader.Delayed_Same_Day = true;
                            }

                            //            If Format(tdbtLeadTime.Text, "AMPM") = Format(tdbtOrderTime.Text, "AMPM") Then
                            //    OrderCollection.Kitchen_Display_Time = Format(tdbdOrderDate.Text, strShortDateFormat) & " " & tdbtLeadTime.Text
                            //ElseIf Format(tdbtLeadTime.Text, "AMPM") = "AM" Then
                            //    OrderCollection.Kitchen_Display_Time = Format(tdbdOrderDate.Text, strShortDateFormat) & " " & tdbtLeadTime.Text
                            //ElseIf Format(tdbtLeadTime.Text, "AMPM") = "PM" Then
                            //    OrderCollection.Kitchen_Display_Time = Format(DateAdd("d", -1, tdbdOrderDate.Text), strShortDateFormat) & " " & tdbtLeadTime.Text
                            //End If
                            cartLocal.cartHeader.Kitchen_Display_Time = Convert.ToDateTime(tdbdOrderDate.Text + " " + tdbtLeadTime.Text);
                            cartLocal.cartHeader.Action = "M";

                            if (cartLocal.cartHeader.Delayed_Date != DateTime.MinValue)
                                cartLocal.cartHeader.Delayed_Order = 1;

                            CartFunctions.UpdateCustomer(cartLocal);
                            Session.cart = APILayer.Add2Cart(cartLocal);
                        }

                    frmOrder frmord = null;
                    foreach (Form form in Application.OpenForms)
                    {
                        if (form.Name == "frmOrder")
                            frmord = (frmOrder)form;
                    }
                    if (frmord != null)
                    {
                        
                        frmord.RefreshCartUI();
                    }

                        this.Close();
                    }
                }
                else
                {
                    CustomMessageBox.Show(MessageConstant.TimedOrderValidation, CustomMessageBox.Buttons.OK, CustomMessageBox.Icon.Warning);
                    return;
                }
            }
            catch (Exception ex)
            {
                Logger.Trace("ERROR", "frmTimedOrders-cmdclose_click(): " + ex.Message, ex, true);
            }           
        }
        public static void SetOrderDateIfStoreClosed(Cart cartLocal, string choosendate)
        {
            cartLocal.cartHeader.Order_Date = Convert.ToDateTime(choosendate).Date;
        }

        private void cmdDownDay_Click(object sender, EventArgs e)
        {
            try
            {
                tdbdOrderDate.Value = tdbdOrderDate.Value.AddDays(-1);
                Check_Date(ref tdbtOrderTime);
                Check15_HourLeadTimes();
                
            }
            catch (Exception ex)
            {
                Logger.Trace("ERROR", "frmTimedOrders-cmddownday_click(): " + ex.Message, ex, true);
            }
        }

        private void cmdUpDay_Click(object sender, EventArgs e)
        {
            try
            {
                tdbdOrderDate.Value = tdbdOrderDate.Value.AddDays(1);
                DateTime temptdbdOrderDate = Convert.ToDateTime(tdbdOrderDate.Text);
                DateTime temppdtmServerDateTime = Convert.ToDateTime(pdtmServerDateTime.ToShortDateString());

                if (tdbdOrderDate.Value.AddDays(1) > temppdtmServerDateTime)
                {
                    cmdDownDay.Enabled = true;
                }
                else
                {
                    cmdDownDay.Enabled = false;
                }
                if (tdbdOrderDate.Value.AddDays(7) > temppdtmServerDateTime)
                {
                    cmdDownWeek.Enabled = true;
                }
                else
                {
                    cmdDownWeek.Enabled = false;
                }
                if (tdbdOrderDate.Value.AddMonths(1) > temppdtmServerDateTime)
                {
                    cmdDownMonth.Enabled = true;
                }
                else
                {
                    cmdDownMonth.Enabled = false;
                }
                cmdDownHour.Enabled = true;
                cmdDown15.Enabled = true;
                cmdDown.Enabled = true;
                Check15_HourLeadTimes();
            }
            catch (Exception ex)
            {
                Logger.Trace("ERROR", "frmTimedOrders-cmdupday_click(): " + ex.Message, ex, true);
            }
        }

        private void cmdDownWeek_Click(object sender, EventArgs e)
        {
            try
            {
                //if (tdbdOrderDate.Value.AddDays(-7) > DateTime.Now)
                //{
                tdbdOrderDate.Value = tdbdOrderDate.Value.AddDays(-7);
                //}

                Check_Date(ref tdbtOrderTime);
                Check15_HourLeadTimes();
            }
            catch (Exception ex)
            {
                Logger.Trace("ERROR", "frmTimedOrders-cmddownweek_click(): " + ex.Message, ex, true);
            }
        }

        private void cmdUpWeek_Click(object sender, EventArgs e)
        {
            try
            {
                tdbdOrderDate.Value = tdbdOrderDate.Value.AddDays(7);// DateAdd("d", 7, tdbdOrderDate.Value)
                DateTime temptdbdOrderDate = Convert.ToDateTime(tdbdOrderDate.Text);
                DateTime temppdtmServerDateTime = Convert.ToDateTime(pdtmServerDateTime.ToShortDateString());
                if (tdbdOrderDate.Value.AddDays(1) > temppdtmServerDateTime)
                {
                    cmdDownDay.Enabled = true;
                }
                else
                {
                    cmdDownDay.Enabled = false;
                }
                if (tdbdOrderDate.Value.AddDays(7) > temppdtmServerDateTime)
                {
                    cmdDownWeek.Enabled = true;
                }
                else
                {
                    cmdDownWeek.Enabled = false;
                }

                if (tdbdOrderDate.Value.AddMonths(1) > temppdtmServerDateTime)
                {
                    cmdDownMonth.Enabled = true;
                }
                else
                {
                    cmdDownMonth.Enabled = false;
                }
                cmdDownHour.Enabled = true;
                cmdDown15.Enabled = true;
                cmdDown.Enabled = true;
                Check15_HourLeadTimes();


            }
            catch (Exception ex)
            {
                Logger.Trace("ERROR", "frmTimedOrders-cmdupweek_click(): " + ex.Message, ex, true);
            }

        }

        private void cmdDownMonth_Click(object sender, EventArgs e)
        {
            try
            {
                tdbdOrderDate.Value = tdbdOrderDate.Value.AddMonths(-1);
                Check_Date(ref tdbtOrderTime);
                Check15_HourLeadTimes();
            }
            catch (Exception ex)
            {
                Logger.Trace("ERROR", "frmTimedOrders-cmddownmonth_click(): " + ex.Message, ex, true);
            }

        }

        private void cmdUpMonth_Click(object sender, EventArgs e)
        {
            try
            {
                tdbdOrderDate.Value = tdbdOrderDate.Value.AddMonths(1);
                DateTime temptdbdOrderDate = Convert.ToDateTime(tdbdOrderDate.Text);
                DateTime temppdtmServerDateTime = Convert.ToDateTime(pdtmServerDateTime.ToShortDateString());
                if (tdbdOrderDate.Value.AddDays(1) > temppdtmServerDateTime)
                {
                    cmdDownDay.Enabled = true;
                }
                else
                {
                    cmdDownDay.Enabled = false;
                }
                if (tdbdOrderDate.Value.AddDays(7) > temppdtmServerDateTime)
                {
                    cmdDownWeek.Enabled = true;
                }
                else
                {
                    cmdDownWeek.Enabled = false;
                }
                if (tdbdOrderDate.Value.AddMonths(1) > temppdtmServerDateTime)
                {
                    cmdDownMonth.Enabled = true;
                }
                else
                {
                    cmdDownMonth.Enabled = false;
                }
                cmdDownHour.Enabled = true;
                cmdDown15.Enabled = true;
                cmdDown.Enabled = true;
                Check15_HourLeadTimes();
            }
            catch (Exception ex)
            {
                Logger.Trace("ERROR", "frmTimedOrders-cmdupmonth_click(): " + ex.Message, ex, true);
            }

        }

        private void cmdDown_Click(object sender, EventArgs e)
        {
            try
            {
                tdbtOrderTime.Value = tdbtOrderTime.Value.AddMinutes(-1);
                //
                tdbtLeadTime.CustomFormat = "HH:mm";
                DateTime temppdtmServerDateTime = pdtmServerDateTime;
                temppdtmServerDateTime = temppdtmServerDateTime.AddSeconds(-temppdtmServerDateTime.Second);
                string orderdatetime = tdbdOrderDate.Text + " " + tdbtLeadTime.Text;
                DateTime dtTempOrderDate = Convert.ToDateTime(orderdatetime);
                //   tdbtLeadTime.CustomFormat = "hh:mm";
                //
                if (dtTempOrderDate <= temppdtmServerDateTime)
                {
                    tdbtLeadTime.Value = pdtmServerDateTime;
                }
                else
                {
                    tdbtLeadTime.Value = tdbtLeadTime.Value.AddMinutes(-1);
                }
                Check_Date(ref tdbtOrderTime);
                Check15_HourLeadTimes();
            }
            catch (Exception ex)
            {
                Logger.Trace("ERROR", "frmTimedOrders-cmddown_click(): " + ex.Message, ex, true);
            }
        }

        private void cmdUp_Click(object sender, EventArgs e)
        {
            try
            {
                string strOrigTime = string.Empty;
                string strOrigLeadTime = string.Empty;
                strOrigTime = tdbtOrderTime.Text;
                strOrigLeadTime = tdbtLeadTime.Text;

                tdbtOrderTime.Value = tdbtOrderTime.Value.AddMinutes(1);
                tdbtLeadTime.Value = tdbtLeadTime.Value.AddMinutes(1);

                //Check to see if the user has scrolled over midnight, if so increment the day also
                if (Convert.ToDateTime(tdbdOrderDate.Text + " " + tdbtOrderTime.Text) < Convert.ToDateTime(tdbdOrderDate.Text + " " + strOrigTime))
                {
                    tdbdOrderDate.Value = tdbdOrderDate.Value.AddDays(1);
                }
                cmdDown15.Enabled = true;
                cmdDownHour.Enabled = true;
                cmdDown.Enabled = true;


                Check15_HourLeadTimes();
            }
            catch (Exception ex)
            {
                Logger.Trace("ERROR", "frmTimedOrders-cmdup_click(): " + ex.Message, ex, true);
            }
        }

        private void cmdDown15_Click(object sender, EventArgs e)
        {
            try
            {
                tdbtOrderTime.Value = tdbtOrderTime.Value.AddMinutes(-15);

                //
                tdbtLeadTime.CustomFormat = "HH:mm";
                DateTime temppdtmServerDateTime = pdtmServerDateTime;
                temppdtmServerDateTime = temppdtmServerDateTime.AddSeconds(-temppdtmServerDateTime.Second);
                string orderdatetime = tdbdOrderDate.Text + " " + tdbtLeadTime.Text;
                DateTime dtTempOrderDate = Convert.ToDateTime(orderdatetime);
                // tdbtLeadTime.CustomFormat = "hh:mm";
                //
                if (dtTempOrderDate.AddMinutes(-15) <= temppdtmServerDateTime)
                {
                    tdbtLeadTime.Value = temppdtmServerDateTime;
                }
                else
                {
                    tdbtLeadTime.Value = tdbtLeadTime.Value.AddMinutes(-15);// DateAdd("n", -15, tdbtLeadTime.Value)
                }
                Check_Date(ref tdbtOrderTime);
                Check15_HourLeadTimes();
            }
            catch (Exception ex)
            {
                Logger.Trace("ERROR", "frmTimedOrders-cmddown15_click(): " + ex.Message, ex, true);
            }
        }

        private void cmdUp15_Click(object sender, EventArgs e)
        {
            string strOrigTime = string.Empty;
            string strOrigLeadTime = string.Empty;
            strOrigTime = tdbtOrderTime.Text;
            strOrigLeadTime = tdbtLeadTime.Text;
            try
            {
                tdbtOrderTime.Value = tdbtOrderTime.Value.AddMinutes(15);
                tdbtLeadTime.Value = tdbtLeadTime.Value.AddMinutes(15);
                //Check to see if the user has scrolled over midnight, if so increment the day also
                if (Convert.ToDateTime(tdbdOrderDate.Text + " " + tdbtOrderTime.Text) < Convert.ToDateTime(tdbdOrderDate.Text + " " + strOrigTime))
                {
                    tdbdOrderDate.Value = tdbdOrderDate.Value.AddDays(1);
                }
                cmdDown15.Enabled = true;
                cmdDownHour.Enabled = true;
                cmdDown.Enabled = true;
                Check15_HourLeadTimes();
            }
            catch (Exception ex)
            {
                Logger.Trace("ERROR", "frmTimedOrders-cmdup15_click(): " + ex.Message, ex, true);
            }
        }

        private void cmdDownHour_Click(object sender, EventArgs e)
        {
            try
            {
                tdbtOrderTime.Value = tdbtOrderTime.Value.AddHours(-1);

                //
                tdbtLeadTime.CustomFormat = "HH:mm";
                DateTime temppdtmServerDateTime = pdtmServerDateTime;
                temppdtmServerDateTime = temppdtmServerDateTime.AddSeconds(-temppdtmServerDateTime.Second);
                string orderdatetime = tdbdOrderDate.Text + " " + tdbtLeadTime.Text;
                DateTime dtTempOrderDate = Convert.ToDateTime(orderdatetime);
                //  tdbtLeadTime.CustomFormat = "hh:mm";
                //
                if (dtTempOrderDate.AddHours(-1) <= temppdtmServerDateTime)
                {
                    tdbtLeadTime.Value = pdtmServerDateTime;
                }
                else
                {
                    tdbtLeadTime.Value = tdbtLeadTime.Value.AddHours(-1);
                }
                Check_Date(ref tdbtOrderTime);
            }
            catch (Exception ex)
            {
                Logger.Trace("ERROR", "frmTimedOrders-cmddownhour_click(): " + ex.Message, ex, true);
            }
        }

        private void cmdUpHour_Click(object sender, EventArgs e)
        {
            try
            {
                string strOrigTime = string.Empty;
                string strOrigLeadTime = string.Empty;
                strOrigTime = tdbtOrderTime.Text;
                strOrigLeadTime = tdbtLeadTime.Text;

                tdbtOrderTime.Value = tdbtOrderTime.Value.AddHours(1);
                tdbtLeadTime.Value = tdbtLeadTime.Value.AddHours(1);

                //Check to see if the user has scrolled over midnight, if so increment the day also
                if (Convert.ToDateTime(tdbdOrderDate.Text + " " + tdbtOrderTime.Text) < Convert.ToDateTime(tdbdOrderDate.Text + " " + strOrigTime))
                {
                    tdbdOrderDate.Value = tdbdOrderDate.Value.AddDays(1);
                }

                Check15_HourLeadTimes();
                cmdDown15.Enabled = true;
                cmdDownHour.Enabled = true;
                cmdDown.Enabled = true;
            }
            catch (Exception ex)
            {
                Logger.Trace("ERROR", "frmTimedOrders-cmduphour_click(): " + ex.Message, ex, true);
            }
        }

        private void cmdLeadTimeDown_Click(object sender, EventArgs e)
        {
            bool blnTimeOK = false;
            DateTime dteTempLeadTimeDate;
            dteTempLeadTimeDate = tdbtLeadTime.Value;
            tdbtLeadTime.Value = tdbtLeadTime.Value.AddMinutes(-1);
            try
            {
                blnTimeOK = CheckLeadTime(Convert.ToDateTime(tdbdOrderDate.Text + " " + tdbtLeadTime.Text));
                if (!blnTimeOK || Convert.ToDateTime(tdbdOrderDate.Text + " " + tdbtLeadTime.Text) <= Convert.ToDateTime(tdbdOrderDate.Text + " " + tdbtOrderTime.Text))
                {
                    cmdLeadTimeDown.Enabled = false;
                }
                else
                {
                    cmdLeadTimeDown.Enabled = true;
                }
                Check15_HourLeadTimes();
            }
            catch (Exception ex)
            {
                Logger.Trace("ERROR", "frmTimedOrders-cmdleadtimedown_click(): " + ex.Message, ex, true);
            }
        }

        private void cmdLeadTimeUP_Click(object sender, EventArgs e)
        {
            try
            {
                bool blnTimeOK = false;
                DateTime dteTempLeadTimeDate = tdbtLeadTime.Value;

                tdbtLeadTime.Value = tdbtLeadTime.Value.AddMinutes(1);

                blnTimeOK = CheckLeadTime(Convert.ToDateTime(tdbdOrderDate.Text + " " + tdbtLeadTime.Text));

                if (!blnTimeOK || Convert.ToDateTime(tdbdOrderDate.Text + " " + tdbtLeadTime.Text) < Convert.ToDateTime(tdbdOrderDate.Text + " " + tdbtOrderTime.Text))
                {
                    cmdLeadTimeUP.Enabled = true;
                    Check15_HourLeadTimes();
                }
                else
                {
                    cmdLeadTimeUP.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                Logger.Trace("ERROR", "frmTimedOrders-cmdleadtimeup_click(): " + ex.Message, ex, true);
            }
        }

        private void cmdLeadTimeDown15_Click(object sender, EventArgs e)
        {
            bool blnTimeOK;
            DateTime dteTempLeadTimeDate = tdbtLeadTime.Value;
            tdbtLeadTime.Value = tdbtLeadTime.Value.AddMinutes(-15);
            try
            {
                blnTimeOK = CheckLeadTime(Convert.ToDateTime(tdbdOrderDate.Text + " " + tdbtLeadTime.Text));

                if (!blnTimeOK || Convert.ToDateTime(tdbdOrderDate.Text + " " + tdbtLeadTime.Text) <= Convert.ToDateTime(tdbdOrderDate.Text + " " + tdbtOrderTime.Text))
                {
                    cmdLeadTimeDown15.Enabled = false;
                }
                else
                {
                    cmdLeadTimeDown15.Enabled = true;
                }
                Check15_HourLeadTimes();
            }
            catch (Exception ex)
            {
                Logger.Trace("ERROR", "frmTimedOrders-cmdleadtimedown15_click(): " + ex.Message, ex, true);
            }
        }

        private void cmdLeadTimeUp15_Click(object sender, EventArgs e)
        {
            try
            {
                bool blnTimeOK;
                DateTime dteTempLeadTimeDate = tdbtLeadTime.Value;

                tdbtLeadTime.Value = tdbtLeadTime.Value.AddMinutes(15);
                blnTimeOK = CheckLeadTime(Convert.ToDateTime(tdbdOrderDate.Text + " " + tdbtLeadTime.Text));
                if (!blnTimeOK || Convert.ToDateTime(tdbdOrderDate.Text + " " + tdbtLeadTime.Text) < Convert.ToDateTime(tdbdOrderDate.Text + " " + tdbtOrderTime.Text))
                {
                    cmdLeadTimeUp15.Enabled = true;
                    Check15_HourLeadTimes();
                }
                else
                {
                    cmdLeadTimeUp15.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                Logger.Trace("ERROR", "frmTimedOrders-cmdleadtimeup15_click(): " + ex.Message, ex, true);
            }
        }

        private void cmdLeadTimeDownHour_Click(object sender, EventArgs e)
        {
            try
            {
                bool blnTimeOK;
                DateTime dteTempLeadTimeDate = tdbtLeadTime.Value;
                tdbtLeadTime.Value = tdbtLeadTime.Value.AddHours(-1);
                blnTimeOK = CheckLeadTime(Convert.ToDateTime(tdbdOrderDate.Text + " " + tdbtLeadTime.Text));
                if (!blnTimeOK || Convert.ToDateTime(tdbdOrderDate.Text + " " + tdbtLeadTime.Text) <= Convert.ToDateTime(tdbdOrderDate.Text + " " + tdbtOrderTime.Text))
                {
                    cmdLeadTimeDownHour.Enabled = false;
                }
                else
                {
                    cmdLeadTimeDownHour.Enabled = true;
                }
                Check15_HourLeadTimes();
            }
            catch (Exception ex)
            {
                Logger.Trace("ERROR", "frmTimedOrders-cmdleadtimedownhour_click(): " + ex.Message, ex, true);
            }
        }

        private void cmdLeadTimeUpHour_Click(object sender, EventArgs e)
        {
            try
            {
                bool blnTimeOK;
                DateTime dteTempLeadTimeDate = tdbtLeadTime.Value;
                tdbtLeadTime.Value = tdbtLeadTime.Value.AddHours(1);
                blnTimeOK = CheckLeadTime(Convert.ToDateTime(tdbdOrderDate.Text + " " + tdbtLeadTime.Text));

                if (!blnTimeOK || Convert.ToDateTime(tdbdOrderDate.Text + " " + tdbtLeadTime.Value) <= Convert.ToDateTime(tdbdOrderDate.Text + " " + tdbtOrderTime.Text))
                {
                    cmdLeadTimeUpHour.Enabled = true;
                    Check15_HourLeadTimes();
                }
                else
                {
                    cmdLeadTimeUpHour.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                Logger.Trace("ERROR", "frmTimedOrders-cmdleadtimeuphour_click(): " + ex.Message, ex, true);
            }
        }

        public void CheckTrainningMode()
        {
            try
            {
                Color color = DefaultBackColor;
                if (SystemSettings.settings.pblnTrainingMode)
                {
                    color = Session.TrainningModeColor;
                }
                else
                {
                    color = Session.NormalModeColor;
                }

                pnl_TimedOrders.BackColor = color;
            }
            catch (Exception ex)
            {
                Logger.Trace("ERROR", "frmTimedOrders-checktrainning(): " + ex.Message, ex, true);
            }
        }

       
    }
}

