using Jublfood.AppLogger;
using JublFood.POS.App.API;
using JublFood.POS.App.BusinessLayer;
using JublFood.POS.App.Cache;
using JublFood.POS.App.Class;
using JublFood.POS.App.Models.Cart;
using JublFood.POS.App.Models.Customer;
using JublFood.POS.App.Models.Employee;
using JublFood.POS.App.Models.Order;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using JublFood.POS.App.Models.Catalog;

namespace JublFood.POS.App
{
    public partial class frmCustomer : Form
    {
        private const int CP_NOCLOSE_BUTTON = 0x200;
        private bool ALT_F4 = false;
        public List<GetCallerIDLine> CallerIDLines { get; set; }
        public List<GetCallerIDLog> CallerIDLogToday { get; set; }
        public List<GetCallerIDLog> CallerIDLogPrevious { get; set; }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams myCp = base.CreateParams;
                myCp.ClassStyle = myCp.ClassStyle | CP_NOCLOSE_BUTTON;
                return myCp;
            }
        }
        public frmCustomer()
        {
            try
            {
                Session.ExitApplication = false;

                CartFunctions.CheckCart();

                if (Session.cart.Customer == null)
                {
                    Session.cart.Customer = new Customer();
                }


                if (Session.CustomerProfileCollection == null)
                {
                    Session.CustomerProfileCollection = new GetCustomerProfile();
                }
                InitializeComponent();
                Session.handleRemakebutton = false;
                
                if (Session.RemakeOrder == true)
                {

                    btn_College.Enabled = false;
                    btn_Business.Enabled = false;
                    btn_Kiosk.Enabled = false;
                    btn_Residence.Enabled = false;
                    cmdDeliveryInfoKeyBoard.Enabled = false;
                    cmdNewExt.Enabled = false;
                    pnl_button.Enabled = false;
                    tlpCustomerPanel.Enabled = false;
                    pnl_CallerDetail.Enabled = false;
                }


                UserFunctions.CheckSetup();
                RemakeButtonStatus();


                ucCustomer_OrderMenu.SetButtonText("Order");
                
                if (Session.CartInitiated)
                {
                    ucCustomer_OrderMenu.cmdTimeClock.Enabled = false;
                    ucCustomer_OrderMenu.cmdLogin.Enabled = false;
                    ucCustomer_OrderMenu.cmdChangeOrders.Enabled = false;
                }

                ucCustomerOrderBottomMenu.Formname = this.Name;
                ucCustomerOrderBottomMenu.LoadOrderType();
                ucCustomerOrderBottomMenu.UserControlButtonClicked += new
                       EventHandler(OrderType_Click);

            }
            catch (Exception ex)
            {
                Logger.Trace("ERROR", "frmCustomer-frmCustomer(): " + ex.Message, ex, true);
            }

        }
        private void RemakeButtonStatus()
        {
            List<CustomerOrderRemake> customerOrderRemakes = new List<CustomerOrderRemake>();
            if (Convert.ToInt64(Session.cart.Customer.Customer_Code) == 0)
                return;
            customerOrderRemakes = APILayer.GetCustomerOrderRemake(Session._LocationCode, Convert.ToInt64(Session.cart.Customer.Customer_Code));

            if (customerOrderRemakes != null && customerOrderRemakes.Count > 0)
            {
                Session.handleRemakebutton = true;
                ucCustomer_OrderMenu.HandleRemakeButton(true);
            }
            else
            {
                Session.handleRemakebutton = false;
                ucCustomer_OrderMenu.HandleRemakeButton(false);

            }
            if (customerOrderRemakes != null && customerOrderRemakes.Count > 0)
            {
                Session.handleRemakebutton = true;
                ucCustomer_OrderMenu.HandleRemakeButton(true);
            }
            else
            {
                Session.handleRemakebutton = false;
                ucCustomer_OrderMenu.HandleRemakeButton(false);

            }

        }
        private void OrderType_Click(object sender, EventArgs e)
        {
            try
            {
                Session.selectedOrderType = Convert.ToString(((Button)sender).Tag);

                if(Session.cart != null && Session.cart.Customer != null )
                {
                    if (Session.cart.Customer.Customer_City_Code != 1 && (Session.selectedOrderType == "I" || Session.selectedOrderType == "C"))
                        ChangeCityToDefault();
                }

                CartFunctions.OrderTypeChange();
            }
            catch (Exception ex)
            {
                Logger.Trace("ERROR", "frmCustomer-OrderType_Click(): " + ex.Message, ex, true);
            }
        }
       

        public string AddressType { get; set; }

        public Result CustomerResult { get; set; }

        Dictionary<string, CustomerKeyBoardInfo> DictKeyBoardInfo;
        CustomerKeyBoardInfo objCustomerKeyBoardInfo;
        TextBox txtFocusedtextBox;

        private void frmCustomer_Load(object sender, EventArgs e)
        {
            try
            {
                Session.IsCallerIDClicked = false;
                this.ActiveControl = tdbmPhone_Number;
                Session.FormClockOpened = true;

                Session.handleModify = true;
                

                this.tdbmPhone_Number.Text = SystemSettings.appControl.DefaultPhonePrefix;
                lblPriorityCustomer.Visible = false;
                lblPriorityCustomer.Text = string.Empty;
                tmrVIP.Stop();
                tmrVIP.Enabled = false;

                ucCustomer_OrderMenu.ucFunctionList = ucFunctionList;
                ucCustomer_OrderMenu.ucInformationList = ucInformationList;
                
            //ucFunctionList.Location = new Point(408, 79);
            //ucInformationList.Location = new Point(491, 78);
                uC_Customer_order_Header1.pnl_MinMax.Visible = false;
                if (Session.pblnModifyingOrder)
                {
                    FillAddressScreen();
                
                }

                this.txtCity.Tag = SystemSettings.settings.plngDefaultCityCode;
                this.txtCity.Text = SystemSettings.settings.pstrDefaultCityName;
                this.txtRegion.Text = SystemSettings.settings.pstrDefaultRegionName;
                this.txtPostal_Code.Text = SystemSettings.settings.pstrDefaultPostalCode;

                ucInformationList.Size = new Size(70, 275);
                ucFunctionList.Size = new Size(70, 165);
                if ((SystemSettings.GetSettingValue("CartOnHold", Session._LocationCode) == "1"))
                {
                    ucFunctionList.btnPutOnHold.Visible = true;
                }
                else
                {
                    ucFunctionList.btnPutOnHold.Visible = false;
                }

                SetControlPosition();
                SetControlText();
                panelCallerIDButton.Visible = false;

                GetAllCustomersRequest requestModel = new GetAllCustomersRequest();
                requestModel.LocationCode = Session._LocationCode;
                GetCallerIDLinesResponse getCallerIDLinesResponse = APILayer.GetCallerIDLines(APILayer.CallType.POST, requestModel);

                for (int i = 0; i < 8; i++)
                {
                    //var label = new Label();
                    //label.Height = 90;
                    //label.Width = 105;
                    //label.BorderStyle = BorderStyle.FixedSingle;
                    //label.BackColor = Color.Salmon;
                    //uC_CallerIDButton1 = new UserControls.UC_CallerIDButton();
                    //uC_CallerIDButton1.Height = 90;
                    //uC_CallerIDButton1.Width = 105;
                    //uC_CallerIDButton1.BorderStyle = BorderStyle.FixedSingle;
                    //uC_CallerIDButton1.BackColor = Color.Salmon;
                    //uC_CallerIDButton1.lblLineNumber.Text = Convert.ToString(i + 1);
                    //flLayout_ActiveCalls.Controls.Add(uC_CallerIDButton1);                

                    panelCallerIDButton = new Panel();
                    panelCallerIDButton.Name = "RuntimePanel" + Convert.ToString(i + 1);
                    panelCallerIDButton.Visible = true;
                    panelCallerIDButton.Height = 90;
                    panelCallerIDButton.Width = 105;
                    panelCallerIDButton.BorderStyle = BorderStyle.FixedSingle;
                    panelCallerIDButton.BackColor = Color.Salmon;
                    panelCallerIDButton.Controls.Remove(this.lblLineNumber);
                    panelCallerIDButton.Controls.Remove(this.lblNew);
                    panelCallerIDButton.Controls.Remove(this.lblTime);
                    panelCallerIDButton.Controls.Remove(this.lblCustomerName);
                    panelCallerIDButton.Controls.Remove(this.lblPhoneNumber);
                    panelCallerIDButton.Click += new System.EventHandler(this.panelCallerIDButton_Click);

                    lblLineNumber = new Label();
                    lblLineNumber.Visible = true;
                    lblLineNumber.Text = Convert.ToString(i + 1);
                    lblLineNumber.AutoSize = true;
                    lblLineNumber.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
                    lblLineNumber.Location = new Point(5, 4);
                    lblLineNumber.Size = new Size(47, 15);
                    lblLineNumber.TabIndex = i + 1;

                    lblNew = new Label();
                    lblNew.AutoSize = true;
                    lblNew.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
                    lblNew.Location = new Point(55, 70);
                    lblNew.Size = new Size(47, 15);
                    lblNew.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
                    lblNew.TabIndex = i + 5;

                    lblTime = new Label();
                    lblTime.AutoSize = true;
                    lblTime.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
                    lblTime.Location = new Point(3, 70);
                    lblTime.Size = new Size(47, 15);
                    lblTime.TabIndex = i + 4;

                    lblCustomerName = new Label();
                    lblCustomerName.AutoSize = true;
                    lblCustomerName.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
                    lblCustomerName.Location = new Point(3, 36);
                    lblCustomerName.Size = new Size(47, 15);
                    lblCustomerName.TabIndex = i + 3;

                    lblPhoneNumber = new Label();
                    lblPhoneNumber.AutoSize = true;
                    lblPhoneNumber.Font = new Font("Microsoft Sans Serif", 6.7F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
                    lblPhoneNumber.Location = new Point(27, 4);
                    lblPhoneNumber.MaximumSize = new Size(75, 0);
                    lblPhoneNumber.MinimumSize = new Size(75, 0);
                    this.lblPhoneNumber.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
                    lblPhoneNumber.Size = new Size(75, 15);
                    lblPhoneNumber.TabIndex = i + 2;






                    if (getCallerIDLinesResponse != null && getCallerIDLinesResponse.Result != null && (getCallerIDLinesResponse.Result.CallerIDLines != null && getCallerIDLinesResponse.Result.CallerIDLines.Count > 0))
                    {
                        CallerIDLines = getCallerIDLinesResponse.Result.CallerIDLines;
                        foreach (GetCallerIDLine line in getCallerIDLinesResponse.Result.CallerIDLines)
                        {
                            if (lblLineNumber.Text == line.Line)
                            {
                                panelCallerIDButton.BackColor = line.Answered ? Color.LightYellow : Color.LightGreen;
                                lblPhoneNumber.Text = line.PhoneNumber;
                                lblCustomerName.Text = line.CustomerName;
                                lblTime.Text = line.CallTime;
                                lblNew.Text = line.Existing == 1 ? "Existing" : "New";
                            }
                        }
                    }

                    panelCallerIDButton.Controls.Add(lblLineNumber);
                    panelCallerIDButton.Controls.Add(lblPhoneNumber);
                    panelCallerIDButton.Controls.Add(lblCustomerName);
                    panelCallerIDButton.Controls.Add(lblTime);
                    panelCallerIDButton.Controls.Add(lblNew);

                    flLayout_ActiveCalls.Controls.Add(panelCallerIDButton);
                }

                //CallerIDLogs
                GetCallerIDLogRequest getCallerIDLogrequestModel = new GetCallerIDLogRequest();
                getCallerIDLogrequestModel.LocationCode = Session._LocationCode;
                getCallerIDLogrequestModel.blnToday = false;
                GetCallerIDLogResponse getCallerIDLogResponse = APILayer.GetCallerIDLog(APILayer.CallType.POST, getCallerIDLogrequestModel);

                GetCallerIDLogRequest getCallerIDLogTodayrequestModel = new GetCallerIDLogRequest();
                getCallerIDLogTodayrequestModel.LocationCode = Session._LocationCode;
                getCallerIDLogTodayrequestModel.blnToday = true;
                GetCallerIDLogResponse getCallerIDLogTodayResponse = APILayer.GetCallerIDLog(APILayer.CallType.POST, getCallerIDLogTodayrequestModel);

                if (getCallerIDLogResponse != null && getCallerIDLogResponse.Result != null && (getCallerIDLogResponse.Result.CallerIDLogs != null && getCallerIDLogResponse.Result.CallerIDLogs.Count > 0))
                {
                    CallerIDLogPrevious = getCallerIDLogResponse.Result.CallerIDLogs;
                    foreach (GetCallerIDLog log in getCallerIDLogResponse.Result.CallerIDLogs)
                    {
                        gvPastCalls.Rows.Add(log.CallDate, log.PhoneNumber, log.Name, log.OrderNumber);
                    }
                    gvPastCalls.AllowUserToAddRows = false;
                    if (gvPastCalls.Rows.Count > 1)
                    {
                        if (gvPastCalls.Rows.Count > 2)
                        {
                            for (int i = 0; i < gvPastCalls.Rows.Count; i++)
                            {
                                if (IsOdd(i))
                                {
                                    gvPastCalls.Rows[i].DefaultCellStyle.BackColor = Color.Gray;
                                }
                            }
                        }
                        else
                        {
                            gvPastCalls.Rows[gvPastCalls.Rows.Count - 1].DefaultCellStyle.BackColor = Color.Gray;
                        }
                    }
                }

                //GetCallerIDLogRequest getCallerIDLogTodayrequestModel = new GetCallerIDLogRequest();
                //getCallerIDLogTodayrequestModel.LocationCode = Session._LocationCode;
                //getCallerIDLogTodayrequestModel.blnToday = true;
                //GetCallerIDLogResponse getCallerIDLogTodayResponse = APILayer.GetCallerIDLog(APILayer.CallType.POST, getCallerIDLogTodayrequestModel);
                if (getCallerIDLogTodayResponse != null && getCallerIDLogTodayResponse.Result != null && (getCallerIDLogTodayResponse.Result.CallerIDLogs != null && getCallerIDLogTodayResponse.Result.CallerIDLogs.Count > 0))
                {
                    CallerIDLogToday = getCallerIDLogTodayResponse.Result.CallerIDLogs;
                    foreach (GetCallerIDLog log in getCallerIDLogTodayResponse.Result.CallerIDLogs)
                    {
                        gvTodaysCalls.Rows.Add(log.CallDate, log.PhoneNumber, log.Name, log.OrderNumber);
                    }
                    gvTodaysCalls.AllowUserToAddRows = false;
                    if (gvTodaysCalls.Rows.Count > 1)
                    {
                        if (gvTodaysCalls.Rows.Count > 2)
                        {
                            for (int i = 0; i < gvTodaysCalls.Rows.Count; i++)
                            {
                                if (IsOdd(i))
                                {
                                    gvTodaysCalls.Rows[i].DefaultCellStyle.BackColor = Color.Gray;
                                }
                            }
                        }
                        else
                        {
                            gvTodaysCalls.Rows[gvTodaysCalls.Rows.Count - 1].DefaultCellStyle.BackColor = Color.Gray;
                        }
                    }
                }

                LoadKeyBoardInfo();
                SetButtonText();

                this.lblCreditLimitData.Text = string.Empty;
                this.lblARBalanceData.Text = string.Empty;
                this.lblInStoreCreditData.Text = string.Empty;
                this.lblLastVoidAmount.Text = string.Empty;
                this.lblLastVoidCount.Text = string.Empty;
                this.lblLastBadAmount.Text = string.Empty;
                this.lblLastBadCount.Text = string.Empty;
                this.lblLastLateAmount.Text = string.Empty;
                this.lblLastLateCount.Text = string.Empty;
                this.lblLastOrdersAmount.Text = string.Empty;
                this.lblLastOrdersCount.Text = string.Empty;
                this.lblLastAverageData.Text = string.Empty;
                this.lblYTDVoidAmount.Text = string.Empty;
                this.lblYTDVoidCount.Text = string.Empty;
                this.lblYTDBadAmount.Text = string.Empty;
                this.lblYTDBadCount.Text = string.Empty;
                this.lblYTDLateAmount.Text = string.Empty;
                this.lblYTDLateCount.Text = string.Empty;
                this.lblYTDOrdersAmount.Text = string.Empty;
                this.lblYTDAverageData.Text = string.Empty;
                this.lblYTDOrdersCount.Text = string.Empty;
                this.lblPerMonth.Text = "per Month";
                this.lblAverageOrdersData.Text = string.Empty;
                this.lblElapsedDaysData.Text = string.Empty;
                this.lblLastOrderData.Text = string.Empty;
                this.lblFirstOrderData.Text = string.Empty;
                this.lblGatherInformation.Text = string.Empty;

                if (Session.cart.Customer != null)
                {
                    if (!string.IsNullOrEmpty(Session.cart.Customer.Phone_Number))
                    {
                        this.tdbmPhone_Number.Text =Session.cart.Customer.Phone_Number;
                    }

                    if (!string.IsNullOrEmpty(Session.PriorityCustomer))
                    {
                        lblPriorityCustomer.Visible = true;
                        lblPriorityCustomer.BackColor = Color.Yellow;
                        lblPriorityCustomer.Text = Session.PriorityCustomer;
                        
                        tmrVIP.Start();
                        tmrVIP.Enabled = true;
                    }
                    else
                    {
                        lblPriorityCustomer.Visible = false;
                        lblPriorityCustomer.Text = string.Empty;
                        tmrVIP.Stop();
                        tmrVIP.Enabled = false;
                    }

                    if (!string.IsNullOrEmpty(Session.cart.Customer.Phone_Ext))
                    {
                        this.tdbtxtPhone_Ext.Text = Session.cart.Customer.Phone_Ext;
                    }
                    if (!string.IsNullOrEmpty(Session.cart.Customer.Name))
                    {
                        this.txtName.Text = Session.cart.Customer.Name;
                    }
                    if (Session.cart.Customer.date_of_birth != null && Session.cart.Customer.date_of_birth.Date != Convert.ToDateTime("01-01-0001"))
                    {
                        this.TDBdob.Value = Session.cart.Customer.date_of_birth;
                    }
                    else
                    {
                        this.TDBdob.Value = TDBdob.MinDate;
                    }
                    if (Session.cart.Customer.anniversary_date != null && Session.cart.Customer.anniversary_date.Date != Convert.ToDateTime("01-01-0001"))
                    {
                        this.TDBanniversary.Value = Session.cart.Customer.anniversary_date;
                    }
                    else
                    {
                        this.TDBanniversary.Value = TDBanniversary.MinDate;
                    }
                    if (!string.IsNullOrEmpty(Session.cart.Customer.Company_Name))
                    {
                        this.txtCompanyName.Text = Session.cart.Customer.Company_Name;
                    }
                    if (!string.IsNullOrEmpty(Session.cart.Customer.Street_Number))
                    {
                        this.txtStreet_Number.Text = Session.cart.Customer.Street_Number;
                    }
                    if (!string.IsNullOrEmpty(Session.cart.Customer.Customer_Street_Name))
                    {
                        this.txtStreet.Text = Session.cart.Customer.Customer_Street_Name;
                    }


                    if (!string.IsNullOrEmpty(Session.cart.Customer.Address_Line_2))
                    {
                        this.txtAddress_Line_2.Text = Session.cart.Customer.Address_Line_2;
                    }
                    if (!string.IsNullOrEmpty(Session.cart.Customer.City))
                    {
                        this.txtCity.Text = Session.cart.Customer.City;
                    }
                    if (!string.IsNullOrEmpty(Session.cart.Customer.Region))
                    {
                        this.txtRegion.Text = Session.cart.Customer.Region;
                    }
                    if (!string.IsNullOrEmpty(Session.cart.Customer.Postal_Code))
                    {
                        this.txtPostal_Code.Text = Session.cart.Customer.Postal_Code;
                    }
                    if (!string.IsNullOrEmpty(Session.cart.Customer.Address_Line_3))
                    {
                        this.txtAddress_Line_3.Text = Session.cart.Customer.Address_Line_3;
                    }

                    if (!string.IsNullOrEmpty(Session.cart.Customer.Address_Line_4))
                    {
                        this.txtAddress_Line_4.Text = Session.cart.Customer.Address_Line_4;
                    }
                    if (!string.IsNullOrEmpty(Session.cart.Customer.Cross_Street))
                    {
                        this.txtCross_Street.Text = Session.cart.Customer.Cross_Street;
                    }
                    if (!string.IsNullOrEmpty(Session.cart.Customer.gstin_number))
                    {
                        this.txtgstin_number.Text = Session.cart.Customer.gstin_number;
                    }

                    if (!string.IsNullOrEmpty(Session.cart.Customer.Manager_Notes))
                    {
                        TabControl tabControl = (TabControl)(this.tabControl_CallerNotesProfile);
                        tabControl.SelectedTab = this.tabPage_Notes;

                        this.txtNotes_manager.Text = Session.cart.Customer.Manager_Notes;
                    }
                    if (!string.IsNullOrEmpty(Session.cart.Customer.DriverComments))
                    {
                        TabControl tabControl = (TabControl)(this.tabControl_CallerNotesProfile);
                        tabControl.SelectedTab = this.tabPage_Notes;

                        this.txtNotes_Delivery.Text = Session.cart.Customer.DriverComments;
                    }
                    if (!string.IsNullOrEmpty(Session.cart.Customer.Comments))
                    {
                        TabControl tabControl = (TabControl)(this.tabControl_CallerNotesProfile);
                        tabControl.SelectedTab = this.tabPage_Notes;
                        this.txtNotes_Instore.Text = Session.cart.Customer.Comments;
                    }

                    //ProfileData
                    if (!string.IsNullOrEmpty(Session.CustomerProfileCollection.CreditLimit.ToString()))
                    {
                        this.lblCreditLimitData.Text = Session.CustomerProfileCollection.CreditLimit.ToString();
                    }
                    if (!string.IsNullOrEmpty(Session.CustomerProfileCollection.ARBalance.ToString()))
                    {
                        this.lblARBalanceData.Text = Session.CustomerProfileCollection.ARBalance.ToString();
                    }
                    if (!string.IsNullOrEmpty(Session.CustomerProfileCollection.InStoreCredit.ToString()))
                    {
                        this.lblInStoreCreditData.Text = Session.CustomerProfileCollection.InStoreCredit.ToString();
                    }
                    if (!string.IsNullOrEmpty(Session.CustomerProfileCollection.LastVoidAmount.ToString()))
                    {
                        this.lblLastVoidAmount.Text = Session.CustomerProfileCollection.LastVoidAmount.ToString("#,##0.00");
                    }
                    if (!string.IsNullOrEmpty(Session.CustomerProfileCollection.LastVoidCount.ToString()))
                    {
                        this.lblLastVoidCount.Text = Session.CustomerProfileCollection.LastVoidCount.ToString();
                    }
                    if (!string.IsNullOrEmpty(Session.CustomerProfileCollection.LastBadAmount.ToString()))
                    {
                        this.lblLastBadAmount.Text = Session.CustomerProfileCollection.LastBadAmount.ToString("#,##0.00");
                    }
                    if (!string.IsNullOrEmpty(Session.CustomerProfileCollection.LastBadCount.ToString()))
                    {
                        this.lblLastBadCount.Text = Session.CustomerProfileCollection.LastBadCount.ToString();
                    }
                    if (!string.IsNullOrEmpty(Session.CustomerProfileCollection.LastLateAmount.ToString()))
                    {
                        this.lblLastLateAmount.Text = Session.CustomerProfileCollection.LastLateAmount.ToString("#,##0.00");
                    }
                    if (!string.IsNullOrEmpty(Session.CustomerProfileCollection.LastLateCount.ToString()))
                    {
                        this.lblLastLateCount.Text = Session.CustomerProfileCollection.LastLateCount.ToString();
                    }
                    if (!string.IsNullOrEmpty(Session.CustomerProfileCollection.LastOrdersAmount.ToString()))
                    {
                        this.lblLastOrdersAmount.Text = Session.CustomerProfileCollection.LastOrdersAmount.ToString("#,##0.00");
                    }
                    if (!string.IsNullOrEmpty(Session.CustomerProfileCollection.LastOrdersCount.ToString()))
                    {
                        this.lblLastOrdersCount.Text = Session.CustomerProfileCollection.LastOrdersCount.ToString();
                    }
                    if (!string.IsNullOrEmpty(Session.CustomerProfileCollection.LastAverage.ToString()))
                    {
                        this.lblLastAverageData.Text = Session.CustomerProfileCollection.LastAverage.ToString();
                    }
                    if (!string.IsNullOrEmpty(Session.CustomerProfileCollection.YTDVoidAmount.ToString()))
                    {
                        this.lblYTDVoidAmount.Text = Session.CustomerProfileCollection.YTDVoidAmount.ToString("#,##0.00");
                    }
                    if (!string.IsNullOrEmpty(Session.CustomerProfileCollection.YTDVoidCount.ToString()))
                    {
                        this.lblYTDVoidCount.Text = Session.CustomerProfileCollection.YTDVoidCount.ToString();
                    }
                    if (!string.IsNullOrEmpty(Session.CustomerProfileCollection.YTDBadAmount.ToString()))
                    {
                        this.lblYTDBadAmount.Text = Session.CustomerProfileCollection.YTDBadAmount.ToString("#,##0.00");
                    }
                    if (!string.IsNullOrEmpty(Session.CustomerProfileCollection.YTDBadCount.ToString()))
                    {
                        this.lblYTDBadCount.Text = Session.CustomerProfileCollection.YTDBadCount.ToString();
                    }
                    if (!string.IsNullOrEmpty(Session.CustomerProfileCollection.YTDLateAmount.ToString()))
                    {
                        this.lblYTDLateAmount.Text = Session.CustomerProfileCollection.YTDLateAmount.ToString("#,##0.00");
                    }
                    if (!string.IsNullOrEmpty(Session.CustomerProfileCollection.YTDLateCount.ToString()))
                    {
                        this.lblYTDLateCount.Text = Session.CustomerProfileCollection.YTDLateCount.ToString();
                    }
                    if (!string.IsNullOrEmpty(Session.CustomerProfileCollection.YTDOrdersAmount.ToString()))
                    {
                        this.lblYTDOrdersAmount.Text = Session.CustomerProfileCollection.YTDOrdersAmount.ToString("#,##0.00");
                    }
                    if (!string.IsNullOrEmpty(Session.CustomerProfileCollection.YTDAverage.ToString()))
                    {
                        this.lblYTDAverageData.Text = Session.CustomerProfileCollection.YTDAverage.ToString();
                    }
                    if (!string.IsNullOrEmpty(Session.CustomerProfileCollection.YTDOrdersCount.ToString()))
                    {
                        this.lblYTDOrdersCount.Text = Session.CustomerProfileCollection.YTDOrdersCount.ToString();
                    }
                    //if (!string.IsNullOrEmpty(Session.CustomerProfileCollection.CreditLimit.ToString()))
                    //{
                    this.lblGatherInformation.Text = "";
                    //}
                    //if (!string.IsNullOrEmpty(Session.CustomerProfileCollection.CreditLimit.ToString()))
                    //{
                    this.lblPerMonth.Text = "per Month";
                    //}
                    if (!string.IsNullOrEmpty(Session.CustomerProfileCollection.OrdersPerMonth.ToString()))
                    {
                        this.lblAverageOrdersData.Text = Session.CustomerProfileCollection.OrdersPerMonth.ToString();
                    }
                    if (!string.IsNullOrEmpty(Session.CustomerProfileCollection.Elapsed.ToString()))
                    {
                        this.lblElapsedDaysData.Text = Session.CustomerProfileCollection.Elapsed.ToString();
                    }
                    if (!string.IsNullOrEmpty(Session.CustomerProfileCollection.LastOrderDate.ToString()))
                    {
                        this.lblLastOrderData.Text = Session.CustomerProfileCollection.LastOrderDate.ToString("MM-dd-yyyy");
                    }
                    if (!string.IsNullOrEmpty(Session.CustomerProfileCollection.FirstOrderDate.ToString()))
                    {
                        this.lblFirstOrderData.Text = Session.CustomerProfileCollection.FirstOrderDate.ToString("MM-dd-yyyy");
                    }

                    if (Session.CustomerProfileCollection.InStoreCredit == 0)
                    {
                        this.lblARBalance.Visible = false;
                        this.lblCreditLimit.Visible = false;
                        this.lblARBalanceData.Visible = false;
                        this.lblCreditLimitData.Visible = false;
                    }

                    //AddressType
                    if (!string.IsNullOrEmpty(Session.cart.Customer.Address_Type))
                    {
                        if (Session.cart.Customer.Address_Type == "R")
                        {
                            this.btn_Residence.PerformClick();
                        }
                        else if (Session.cart.Customer.Address_Type == "K")
                        {
                            this.btn_Kiosk.PerformClick();
                        }
                        else if (Session.cart.Customer.Address_Type == "H")
                        {
                            this.btn_Kiosk.PerformClick();
                        }
                        else if (Session.cart.Customer.Address_Type == "B")
                        {
                            this.btn_Business.PerformClick();
                        }
                        else if (Session.cart.Customer.Address_Type == "C")
                        {
                            this.btn_College.PerformClick();
                        }
                    }
                    else
                    {
                        //by default select Residence Address Type
                        this.btn_Residence.PerformClick();
                    }
                }
                
                UserFunctions.AutoSelectOrderType(ucCustomerOrderBottomMenu);
                ucCustomer_OrderMenu.HandleHistoryButton(false);
                CheckTrainningMode();

                TDBdob.MaxDate = DateTime.Now.Date;
                //TDBanniversary.MaxDate = DateTime.Now.Date;

                if(Session.CurrentEmployee == null || Session.CurrentEmployee.LoginDetail == null)
                {
                    ucCustomer_OrderMenu.HandleModifyButton(false);
                    Session.handleModify = false;
                    btn_College.Enabled = false;
                    btn_Business.Enabled = false;
                    btn_Kiosk.Enabled = false;
                    btn_Residence.Enabled = false;
                    cmdDeliveryInfoKeyBoard.Enabled = false;
                    cmdNewExt.Enabled = false;
                    pnl_button.Enabled = false;
                    tlpCustomerPanel.Enabled = false;
                    pnl_CallerDetail.Enabled = false;
                }


                PopulateStreets();

                this.Location = new Point(((Screen.PrimaryScreen.Bounds.Width - this.Size.Width) / 2 ) +5,((Screen.PrimaryScreen.Bounds.Height - this.Size.Height) / 2));

                Console.WriteLine("TimeStamp Customer :" + DateTime.Now);
                PopulateSourceName();

                CartFunctions.UpdateMenuStatus();
            }
            catch (Exception ex)
            {
                Logger.Trace("ERROR", "frmCustomer-frmCustomer_Load(): " + ex.Message, ex, true);
            }
        }

        private void SetButtonText()
        {
            string str = null;
            //balObj = new BAL();
            //DataTable DtAddressType;
            //DtAddressType = balObj.GetAllAddressType();

            for (int i = 0; i < Session.CatalogAddressTypes.Count; i++)
            {
                if (Session.CatalogAddressTypes[i].Address_Type == "R")
                {

                    btn_Residence.Text = Session.CatalogAddressTypes[i].Description;
                    
                }
                if (Session.CatalogAddressTypes[i].Address_Type == "H")
                {

                    btn_Kiosk.Text = Session.CatalogAddressTypes[i].Description;

                }

                if (Session.CatalogAddressTypes[i].Address_Type == "B")
                {

                    btn_Business.Text = Session.CatalogAddressTypes[i].Description;

                }

                if (Session.CatalogAddressTypes[i].Address_Type == "C")
                {

                    btn_College.Text = Session.CatalogAddressTypes[i].Description;

                }

                
            }
            string labelText = null;
            if (Common.DictAllButtonText.TryGetValue(LanguageConstant.cintNewExt, out labelText))
            {
                cmdNewExt.Text = labelText;
            }
            if (Common.DictAllButtonText.TryGetValue(LanguageConstant.cintKeyBoard, out labelText))
            {
                cmdDeliveryInfoKeyBoard.Text = labelText;
            }
            if (Common.DictAllButtonText.TryGetValue(LanguageConstant.cintProfile, out labelText))
            {
                cmdCustomerProfile.Text = labelText;
            }
            //Caller Notes
            if (Common.DictAllButtonText.TryGetValue(LanguageConstant.cintKeyBoard, out labelText))
            {
                btn_keyBoardInstore.Text = labelText;
                btn_Keyboarddelivery.Text = labelText;
                btn_Keyboardmanager.Text = labelText;
            }
            if (Common.DictAllButtonText.TryGetValue(LanguageConstant.cintClear, out labelText))
            {
                btn_ClearInstore.Text = labelText;
                btn_ClearDelivery.Text = labelText;
                btn_KeyboardClear.Text = labelText;
            }
            // Caller Ids tabs
            if (Common.DictAllButtonText.TryGetValue(LanguageConstant.cintCallerID, out labelText))
            {
                tabPage_CallerId.Text = labelText;
            }

            if (Common.DictAllButtonText.TryGetValue(LanguageConstant.cintActiveCalls, out labelText))
            {
                tabPage_ActiveCalls.Text = labelText;

            }
            if (Common.DictAllButtonText.TryGetValue(LanguageConstant.cintTodaysCalls, out labelText))
            {
                tabPage_Todayscalls.Text = labelText;
            }
            if (Common.DictAllButtonText.TryGetValue(LanguageConstant.cintPastCalls, out labelText))
            {
                tabPage_PostCalls.Text = labelText;
            }
            if (Common.DictAllButtonText.TryGetValue(LanguageConstant.cintNotes, out labelText))
            {
                tabPage_Notes.Text = labelText;
            }
            if (Common.DictAllButtonText.TryGetValue(LanguageConstant.cintInstoreComments, out labelText))
            {
                tabPage_InStore.Text = labelText;
            }
            if (Common.DictAllButtonText.TryGetValue(LanguageConstant.cintDeliveryComments, out labelText))
            {
                tabPage_Delivery.Text = labelText;
            }
            if (Common.DictAllButtonText.TryGetValue(LanguageConstant.cintManagerNotes, out labelText))
            {
                tabPage_Manager.Text = labelText;
            }

            //Profile

            if (Common.DictAllButtonText.TryGetValue(LanguageConstant.cintMSGGatheringCustomerInfo, out labelText))
            {
                lblGatherInformation.Text = labelText;
            }
            if (Common.DictAllButtonText.TryGetValue(LanguageConstant.cintFirstOrder, out labelText))
            {
                lblFirstOrder.Text = labelText;
            }
            if (Common.DictAllButtonText.TryGetValue(LanguageConstant.cintLastOrder, out labelText))
            {
                lblLastOrder.Text = labelText;
            }
            if (Common.DictAllButtonText.TryGetValue(LanguageConstant.cintAverageOrders, out labelText))
            {
                lblAverageOrders.Text = labelText;
            }
            if (Common.DictAllButtonText.TryGetValue(LanguageConstant.cintElapsedDays, out labelText))
            {
                lblElapsedDays.Text = labelText;
            }
            if (Common.DictAllButtonText.TryGetValue(LanguageConstant.cintThisYear, out labelText))
            {
                lblThisYear.Text = labelText;
            }



            if (Common.DictAllButtonText.TryGetValue(LanguageConstant.cintAverage, out labelText))
            {
                lblYTDAverage.Text = labelText;
            }
            if (Common.DictAllButtonText.TryGetValue(LanguageConstant.cintOrders, out labelText))
            {
                lblYTDOrders.Text = labelText;
            }
            if (Common.DictAllButtonText.TryGetValue(LanguageConstant.cintLate, out labelText))
            {
                lblYTDLate.Text = labelText;
            }
            if (Common.DictAllButtonText.TryGetValue(LanguageConstant.cintBad, out labelText))
            {
                lblYTDBad.Text = labelText;
            }
            if (Common.DictAllButtonText.TryGetValue(LanguageConstant.cintVoid, out labelText))
            {
                lblYTDVoid.Text = labelText;
            }
            if (Common.DictAllButtonText.TryGetValue(LanguageConstant.cintLastYear, out labelText))
            {
                lblLastYear.Text = labelText;
            }
            if (Common.DictAllButtonText.TryGetValue(LanguageConstant.cintAverage, out labelText))
            {
                lblLastAverage.Text = labelText;
            }
            if (Common.DictAllButtonText.TryGetValue(LanguageConstant.cintOrders, out labelText))
            {
                lblLastOrders.Text = labelText;
            }
            if (Common.DictAllButtonText.TryGetValue(LanguageConstant.cintLate, out labelText))
            {
                lblLastLate.Text = labelText;
            }
            if (Common.DictAllButtonText.TryGetValue(LanguageConstant.cintBad, out labelText))
            {
                lblLastBad.Text = labelText;
            }
            if (Common.DictAllButtonText.TryGetValue(LanguageConstant.cintVoid, out labelText))
            {
                lblLastVoid.Text = labelText;
            }

            if (Common.DictAllButtonText.TryGetValue(LanguageConstant.cintInStoreCredit, out labelText))
            {
                lblInStoreCredit.Text = labelText;
            }
            if (Common.DictAllButtonText.TryGetValue(LanguageConstant.cintARBalance, out labelText))
            {
                lblARBalance.Text = labelText;
            }
            if (Common.DictAllButtonText.TryGetValue(LanguageConstant.cintCreditLimit, out labelText))
            {
                lblCreditLimit.Text = labelText;
            }
            if (Common.DictAllButtonText.TryGetValue(LanguageConstant.cintPerMonth, out labelText))
            {
                lblPerMonth.Text = labelText;
            }
        }
        private void SetControlPosition()
        {
            //balObj = new BAL();
            //listControlPropeties = balObj.GetControlSetting("frmAddress");

            foreach (Control ctl in this.tlpCustomerPanel.Controls)
            {
                foreach (CatalogControlPropeties controlPropeties in Session.catalogControlPropeties)
                {
                    if (ctl.Name == controlPropeties.Control)
                    {
                        //ctl.Left = controlPropeties.X_Left;
                        //ctl.Top = controlPropeties.Y_Top;
                        ctl.TabIndex = controlPropeties.Tab_Order;
                        ctl.Visible = Convert.ToBoolean(controlPropeties.Visibility);
                        //ctl.Visible = true;
                        //if (controlPropeties.Width > 0)
                        //{
                        //    ctl.Width = (controlPropeties.Width) * 6 / 100;
                        //}
                        //ctl.Height = controlPropeties.Height;
                        if (controlPropeties.MaxLength > 0)
                        {
                            TextBox textBox = (TextBox)ctl;
                            textBox.MaxLength = controlPropeties.MaxLength;

                        }
                    }
                }
            }
        }

        private void SetControlText()
        {
            //BAL obj = new BAL();
            //List<FormField> listFormField = obj.GetControlText("frmAddress");
            foreach (Control ctl in this.tlpCustomerPanel.Controls)
            {
                if (ctl is Label)
                {
                    foreach (CatalogControlText formField in Session.catalogControlText)
                    {
                        if (ctl.Name.Substring(4, ctl.Name.Length - 4) == formField.Field_Name)
                        {
                            ctl.Text = formField.text.Trim();
                        }
                    }
                }
            }


        }
        private void lblLastAverageData_Click(object sender, EventArgs e)
        {

        }

        private void tabPage_Profile_Click(object sender, EventArgs e)
        {

        }

        private void txtStreet_Click(object sender, EventArgs e)
        {
            try
            {
                lvw_Streets.Visible = true;
                //lvw_Streets.Location = new Point(221, 135);
                lvw_Streets.Size = new Size(290, 150);

                if (Session.CustomerStreets == null || (Session.CustomerStreets.Count == 0 && string.IsNullOrEmpty(this.txtStreet.Text)))
                {
                    PopulateStreets();
                    lvw_Streets.Items.Clear();
                    lvw_Streets.Items.AddRange(Session.CustomerStreets.ToArray());
                }
                else if (lvw_Streets.Items.Count == 0)
                {
                    lvw_Streets.Items.AddRange(Session.CustomerStreets.ToArray());
                }
            }
            catch (Exception ex)
            {
                Logger.Trace("ERROR", "frmCustomer-txtStreet_Click(): " + ex.Message, ex, true);
            }
        }

        private void txtStreet_Leave(object sender, EventArgs e)
        {
            try
            {
                lvw_Streets.Visible = false;
            }
            catch (Exception ex)
            {
                Logger.Trace("ERROR", ex.Message, ex, true);
            }
        }

        private void txtAddress_Line_2_Click(object sender, EventArgs e)
        {
            try
            {
                lvw_Address.Visible = true;
                //lvw_Address.Location = new Point(117, 168);
                lvw_Address.Size = new Size(351, 150);

                
                if (CustomerResult != null && CustomerResult.CustomerDetail != null)
                {
                    lvw_Address.Items.Clear();
                    if (CustomerResult.CustomerDetail.PhoneNumber.Trim() == tdbmPhone_Number.Text.Trim())
                    {
                        if (CustomerResult.customerAddresses != null)
                        {
                            foreach (CustomerAddressResult address in CustomerResult.customerAddresses)
                            {
                                ListViewItem item = new ListViewItem(new[] { address.AddressLine2.Trim(), address.StreetNumber.Trim(), address.Street_Name.Trim(),  address.CompanyName.Trim() });
                                //item.SubItems.Add();
                                lvw_Address.Items.Add(item);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Trace("ERROR", "frmCustomer-txtAddress_Line_2_Click(): " + ex.Message, ex, true);
            }

        }

        private void txtAddress_Line_2_Leave(object sender, EventArgs e)
        {
            try
            {
                lvw_Address.Visible = false;
            }
            catch (Exception ex)
            {
                Logger.Trace("ERROR", ex.Message, ex, true);
            }
        }

        private void txtCity_Click(object sender, EventArgs e)
        {
            try
            {
                lvw_Cities.Visible = true;
                //lvw_Cities.Location = new Point(117, 196);
                lvw_Cities.Size = new Size(121, 150);

                //GetAllCities(CallType callType)

                if (lvw_Cities.Items.Count == 0)
                {
                    GetAllCitiesResponse citiesResponse = APILayer.GetAllCities(APILayer.CallType.GET);

                    if (citiesResponse != null && citiesResponse.Result != null && citiesResponse.Result.Cities != null)
                    {
                        Session.CitiesAPIResponse = citiesResponse.Result.Cities;

                        foreach (GetAllCities city in citiesResponse.Result.Cities)
                        {
                            lvw_Cities.Items.Add(city.CityName);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Trace("ERROR", "frmCustomer-txtCity_Click(): " + ex.Message, ex, true);
            }
        }

        private void txtCity_Leave(object sender, EventArgs e)
        {
            try
            {
                lvw_Cities.Visible = false;
            }
            catch (Exception ex)
            {
                Logger.Trace("ERROR", ex.Message, ex, true);
            }
        }

        private void LoadKeyBoardInfo()
        {
            DictKeyBoardInfo = new Dictionary<string, CustomerKeyBoardInfo>();

            objCustomerKeyBoardInfo = new CustomerKeyBoardInfo();
            objCustomerKeyBoardInfo.CaptionName = "Phone Number";
            objCustomerKeyBoardInfo.KeyBoardType = 2;
            DictKeyBoardInfo.Add("tdbmPhone_Number", objCustomerKeyBoardInfo);

            objCustomerKeyBoardInfo = new CustomerKeyBoardInfo();
            objCustomerKeyBoardInfo.CaptionName = "Ext";
            objCustomerKeyBoardInfo.KeyBoardType = 2;
            DictKeyBoardInfo.Add("tdbtxtPhone_Ext", objCustomerKeyBoardInfo);

            objCustomerKeyBoardInfo = new CustomerKeyBoardInfo();
            objCustomerKeyBoardInfo.CaptionName = "Name";
            objCustomerKeyBoardInfo.KeyBoardType = 1;
            DictKeyBoardInfo.Add("txtName", objCustomerKeyBoardInfo);

            objCustomerKeyBoardInfo = new CustomerKeyBoardInfo();
            objCustomerKeyBoardInfo.CaptionName = "DOB";
            objCustomerKeyBoardInfo.KeyBoardType = 1;
            DictKeyBoardInfo.Add("TDBdob", objCustomerKeyBoardInfo);

            objCustomerKeyBoardInfo = new CustomerKeyBoardInfo();
            objCustomerKeyBoardInfo.CaptionName = "Anniversary";
            objCustomerKeyBoardInfo.KeyBoardType = 1;
            DictKeyBoardInfo.Add("TDBanniversary", objCustomerKeyBoardInfo);

            objCustomerKeyBoardInfo = new CustomerKeyBoardInfo();
            objCustomerKeyBoardInfo.CaptionName = "Company Name";
            objCustomerKeyBoardInfo.KeyBoardType = 1;
            DictKeyBoardInfo.Add("txtCompanyName", objCustomerKeyBoardInfo);

            objCustomerKeyBoardInfo = new CustomerKeyBoardInfo();
            objCustomerKeyBoardInfo.CaptionName = "Street Number";
            objCustomerKeyBoardInfo.KeyBoardType = 1;
            DictKeyBoardInfo.Add("txtStreet_Number", objCustomerKeyBoardInfo);

            objCustomerKeyBoardInfo = new CustomerKeyBoardInfo();
            objCustomerKeyBoardInfo.CaptionName = "Street Number";
            objCustomerKeyBoardInfo.KeyBoardType = 1;
            DictKeyBoardInfo.Add("txtStreet", objCustomerKeyBoardInfo);

            objCustomerKeyBoardInfo = new CustomerKeyBoardInfo();
            objCustomerKeyBoardInfo.CaptionName = "Address";
            objCustomerKeyBoardInfo.KeyBoardType = 1;
            DictKeyBoardInfo.Add("txtAddress_Line_2", objCustomerKeyBoardInfo);

            objCustomerKeyBoardInfo = new CustomerKeyBoardInfo();
            objCustomerKeyBoardInfo.CaptionName = "Suite";
            objCustomerKeyBoardInfo.KeyBoardType = 1;
            DictKeyBoardInfo.Add("txtSuite", objCustomerKeyBoardInfo);

            objCustomerKeyBoardInfo = new CustomerKeyBoardInfo();
            objCustomerKeyBoardInfo.CaptionName = "City";
            objCustomerKeyBoardInfo.KeyBoardType = 1;
            DictKeyBoardInfo.Add("txtCity", objCustomerKeyBoardInfo);

            objCustomerKeyBoardInfo = new CustomerKeyBoardInfo();
            objCustomerKeyBoardInfo.CaptionName = "Region";
            objCustomerKeyBoardInfo.KeyBoardType = 1;
            DictKeyBoardInfo.Add("txtRegion", objCustomerKeyBoardInfo);

            objCustomerKeyBoardInfo = new CustomerKeyBoardInfo();
            objCustomerKeyBoardInfo.CaptionName = "Postal Code";
            objCustomerKeyBoardInfo.KeyBoardType = 2;
            DictKeyBoardInfo.Add("txtPostal_Code", objCustomerKeyBoardInfo);

            objCustomerKeyBoardInfo = new CustomerKeyBoardInfo();
            objCustomerKeyBoardInfo.CaptionName = "Address";
            objCustomerKeyBoardInfo.KeyBoardType = 1;
            DictKeyBoardInfo.Add("txtAddress_Line_3", objCustomerKeyBoardInfo);

            objCustomerKeyBoardInfo = new CustomerKeyBoardInfo();
            objCustomerKeyBoardInfo.CaptionName = "Address";
            objCustomerKeyBoardInfo.KeyBoardType = 1;
            DictKeyBoardInfo.Add("txtAddress_Line_4", objCustomerKeyBoardInfo);

            objCustomerKeyBoardInfo = new CustomerKeyBoardInfo();
            objCustomerKeyBoardInfo.CaptionName = "Cross Street";
            objCustomerKeyBoardInfo.KeyBoardType = 1;
            DictKeyBoardInfo.Add("txtCross_Street", objCustomerKeyBoardInfo);

            objCustomerKeyBoardInfo = new CustomerKeyBoardInfo();
            objCustomerKeyBoardInfo.CaptionName = "GSTIN Number";
            objCustomerKeyBoardInfo.KeyBoardType = 1;
            DictKeyBoardInfo.Add("txtgstin_number", objCustomerKeyBoardInfo);

            objCustomerKeyBoardInfo = new CustomerKeyBoardInfo();
            objCustomerKeyBoardInfo.CaptionName = "Sector";
            objCustomerKeyBoardInfo.KeyBoardType = 1;
            DictKeyBoardInfo.Add("lblSector_Code", objCustomerKeyBoardInfo);

            objCustomerKeyBoardInfo = new CustomerKeyBoardInfo();
            objCustomerKeyBoardInfo.CaptionName = "Tent Number";
            objCustomerKeyBoardInfo.KeyBoardType = 1;
            DictKeyBoardInfo.Add("txtTentNumber", objCustomerKeyBoardInfo);


        }

        private void TDBdob_Enter(object sender, EventArgs e)
        {
            try
            {
                txtFocusedtextBox = (TextBox)sender;
            }
            catch (Exception ex)
            {
                Logger.Trace("ERROR", ex.Message, ex, true);
            }
        }

        private void frmCustomer_Click(object sender, EventArgs e)
        {
            try
            {
                ucInformationList.Visible = false;
                ucFunctionList.Visible = false;
            }
            catch (Exception ex)
            {
                Logger.Trace("ERROR", ex.Message, ex, true);
            }
        }

        private void btn_College_Click(object sender, EventArgs e)
        {
            try
            {
                SetButtonColor((Button)sender);

                Button btn = (Button)sender;
                if (btn.Text.Trim().ToUpper() == "RESIDENCE")
                {
                    Session.cart.Customer.Address_Type = "R";
                    SetControlText();
                }
                else if (btn.Text.Trim().ToUpper() == "KIOSK")
                {
                    Session.cart.Customer.Address_Type = "K";
                    this.ltxtSuite.Text = "Room";
                }
                else if (btn.Text.Trim().ToUpper() == "HOTEL")
                {
                    Session.cart.Customer.Address_Type = "H";
                    this.ltxtSuite.Text = "Room";
                }
                else if (btn.Text.Trim().ToUpper() == "BUSINESS")
                {
                    Session.cart.Customer.Address_Type = "B";
                    SetControlText();
                }
                else if (btn.Text.Trim().ToUpper() == "COLLEGE")
                {
                    Session.cart.Customer.Address_Type = "C";
                    this.ltxtSuite.Text = "Room";
                }

                Set_Required_Fields(Session.selectedOrderType, Session.cart.Customer.Address_Type);
            }
            catch (Exception ex)
            {
                Logger.Trace("ERROR", "frmCustomer-btn_College_Click(): " + ex.Message, ex, true);
            }
        }
        private void SetButtonColor(Button btn)
        {
            cmdCustomerProfile.BackColor = DefaultBackColor;
            btn_Residence.BackColor = DefaultBackColor;
            btn_Kiosk.BackColor = DefaultBackColor;
            btn_Business.BackColor = DefaultBackColor;
            btn_College.BackColor = DefaultBackColor;
            btn.BackColor = Color.FromArgb(255, 128, 128);
        }

        private void btn_keyBoardInstore_Click(object sender, EventArgs e)
        {
            try
            {
                txtFocusedtextBox = txtNotes_Instore;
                frmKeyBoard objfrmKeyBoard = new frmKeyBoard(txtFocusedtextBox, objCustomerKeyBoardInfo.CaptionName);
                objfrmKeyBoard.ShowDialog();
            }
            catch (Exception ex)
            {
                Logger.Trace("ERROR", ex.Message, ex, true);
            }
        }

        private void btn_Keyboarddelivery_Click(object sender, EventArgs e)
        {
            try
            {
                txtFocusedtextBox = txtNotes_Delivery;
                frmKeyBoard objfrmKeyBoard = new frmKeyBoard(txtFocusedtextBox, objCustomerKeyBoardInfo.CaptionName);
                objfrmKeyBoard.ShowDialog();
            }
            catch (Exception ex)
            {
                Logger.Trace("ERROR", ex.Message, ex, true);
            }
        }

        private void btn_Keyboardmanager_Click(object sender, EventArgs e)
        {
            try
            {
                txtFocusedtextBox = txtNotes_manager;
                frmKeyBoard objfrmKeyBoard = new frmKeyBoard(txtFocusedtextBox, objCustomerKeyBoardInfo.CaptionName);
                objfrmKeyBoard.ShowDialog();
            }
            catch (Exception ex)
            {
                Logger.Trace("ERROR", ex.Message, ex, true);
            }
        }

        private void txtCompanyName_TextChanged(object sender, EventArgs e)
        {
            try
            {
                //if (!Session.FromOrder)
                Session.cart.Customer.Company_Name = this.txtCompanyName.Text;
            }
            catch (Exception ex)
            {
                Logger.Trace("ERROR", ex.Message, ex, true);
            }
        }
        
        private void cmdDeliveryInfoKeyBoard_Click(object sender, EventArgs e)
        {
           
            try
            {
                
                objCustomerKeyBoardInfo = null;
                int frmtxtboxmaxlength = 0;
                if (txtFocusedtextBox != null)
                {
                    objCustomerKeyBoardInfo = DictKeyBoardInfo.FirstOrDefault(x => x.Key == txtFocusedtextBox.Name).Value;
                    TextBox textBox = (TextBox)(txtFocusedtextBox);
                    frmtxtboxmaxlength = textBox.MaxLength;


                }
                if (objCustomerKeyBoardInfo != null)
                {
                    if (objCustomerKeyBoardInfo.KeyBoardType == 1)
                    {
                        frmKeyBoard objfrmKeyBoard = new frmKeyBoard(txtFocusedtextBox, objCustomerKeyBoardInfo.CaptionName);
                        objfrmKeyBoard.txt_Input.MaxLength = frmtxtboxmaxlength;
                        objfrmKeyBoard.ShowDialog();
                    }
                    else if (objCustomerKeyBoardInfo.KeyBoardType == 2)
                    {
                        frmKeyBoardNumeric objfrmKeyBoardNumeric = new frmKeyBoardNumeric(txtFocusedtextBox, objCustomerKeyBoardInfo.CaptionName);
                        objfrmKeyBoardNumeric.txt_Input.MaxLength = frmtxtboxmaxlength;
                        objfrmKeyBoardNumeric.ShowDialog();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Trace("ERROR", ex.Message, ex, true);
            }
        }

        private void tabControl_CallerNotesProfile_Click(object sender, EventArgs e)
        {
            //if(this.tabControl_CallerNotesProfile.Controls.Owner 

            //TabControl tabControl = (TabControl)sender;
            //if (tabControl.SelectedIndex == 2)
            //{
            //    this.lblCreditLimitData.Text = string.Empty;
            //    this.lblARBalanceData.Text = string.Empty;
            //    this.lblInStoreCreditData.Text = string.Empty;
            //    this.lblLastVoidAmount.Text = string.Empty;
            //    this.lblLastVoidCount.Text = string.Empty;
            //    this.lblLastBadAmount.Text = string.Empty;
            //    this.lblLastBadCount.Text = string.Empty;
            //    this.lblLastLateAmount.Text = string.Empty;
            //    this.lblLastLateCount.Text = string.Empty;
            //    this.lblLastOrdersAmount.Text = string.Empty;
            //    this.lblLastOrdersCount.Text = string.Empty;
            //    this.lblLastAverageData.Text = string.Empty;
            //    this.lblYTDVoidAmount.Text = string.Empty;
            //    this.lblYTDVoidCount.Text = string.Empty;
            //    this.lblYTDBadAmount.Text = string.Empty;
            //    this.lblYTDBadCount.Text = string.Empty;
            //    this.lblYTDLateAmount.Text = string.Empty;
            //    this.lblYTDLateCount.Text = string.Empty;
            //    this.lblYTDOrdersAmount.Text = string.Empty;
            //    this.lblYTDAverageData.Text = string.Empty;
            //    this.lblYTDOrdersCount.Text = string.Empty;
            //    this.lblPerMonth.Text = "per Month";
            //    this.lblAverageOrdersData.Text = string.Empty;
            //    this.lblElapsedDaysData.Text = string.Empty;
            //    this.lblLastOrderData.Text = string.Empty;
            //    this.lblFirstOrderData.Text = string.Empty;
            //    this.lblGatherInformation.Text = string.Empty;
            //}



            //TabControl tabControl = (TabControl)sender;
            //if (tabControl.SelectedIndex == 2)
            //{
            //    

            //    GetCustomerProfileRequest getCustomerProfileRequest = new GetCustomerProfileRequest();
            //    getCustomerProfileRequest.CustomerCode = "153539";//Session.CurrentEmployee.LoginDetail.
            //    getCustomerProfileRequest.LocationCode = Session._LocationCode;// Session.CurrentEmployee.LoginDetail.

            //    GetCustomerProfileResponse getCustomerProfileResponse = APILayer.GetCustomerProfile(APILayer.CallType.POST, getCustomerProfileRequest);

            //    if (getCustomerProfileResponse != null && getCustomerProfileResponse.Result != null && getCustomerProfileResponse.Result.CustomerProfile != null)
            //    {
            //        this.lblCreditLimitData.Text = getCustomerProfileResponse.Result.CustomerProfile.CreditLimit.ToString();
            //        this.lblARBalanceData.Text = getCustomerProfileResponse.Result.CustomerProfile.ARBalance.ToString();
            //        this.lblInStoreCreditData.Text = getCustomerProfileResponse.Result.CustomerProfile.InStoreCredit.ToString();
            //        this.lblLastVoidAmount.Text = getCustomerProfileResponse.Result.CustomerProfile.LastVoidAmount.ToString();
            //        this.lblLastVoidCount.Text = getCustomerProfileResponse.Result.CustomerProfile.LastVoidCount.ToString();
            //        this.lblLastBadAmount.Text = getCustomerProfileResponse.Result.CustomerProfile.LastBadAmount.ToString();
            //        this.lblLastBadCount.Text = getCustomerProfileResponse.Result.CustomerProfile.LastBadCount.ToString();
            //        this.lblLastLateAmount.Text = getCustomerProfileResponse.Result.CustomerProfile.LastLateAmount.ToString();
            //        this.lblLastLateCount.Text = getCustomerProfileResponse.Result.CustomerProfile.LastLateCount.ToString();
            //        this.lblLastOrdersAmount.Text = getCustomerProfileResponse.Result.CustomerProfile.LastOrdersAmount.ToString();
            //        this.lblLastOrdersCount.Text = getCustomerProfileResponse.Result.CustomerProfile.LastOrdersCount.ToString();
            //        this.lblLastAverageData.Text = getCustomerProfileResponse.Result.CustomerProfile.LastAverage.ToString();
            //        this.lblYTDVoidAmount.Text = getCustomerProfileResponse.Result.CustomerProfile.YTDVoidAmount.ToString();
            //        this.lblYTDVoidCount.Text = getCustomerProfileResponse.Result.CustomerProfile.YTDVoidCount.ToString();
            //        this.lblYTDBadAmount.Text = getCustomerProfileResponse.Result.CustomerProfile.YTDBadAmount.ToString();
            //        this.lblYTDBadCount.Text = getCustomerProfileResponse.Result.CustomerProfile.YTDBadCount.ToString();
            //        this.lblYTDLateAmount.Text = getCustomerProfileResponse.Result.CustomerProfile.YTDLateAmount.ToString();
            //        this.lblYTDLateCount.Text = getCustomerProfileResponse.Result.CustomerProfile.YTDLateCount.ToString();
            //        this.lblYTDOrdersAmount.Text = getCustomerProfileResponse.Result.CustomerProfile.YTDOrdersAmount.ToString();
            //        this.lblYTDAverageData.Text = getCustomerProfileResponse.Result.CustomerProfile.YTDAverage.ToString();
            //        this.lblYTDOrdersCount.Text = getCustomerProfileResponse.Result.CustomerProfile.YTDOrdersCount.ToString();
            //        //this.lblCreditLimit.Text = getCustomerProfileResponse.Result.CustomerProfile.CreditLimit.ToString();
            //        //this.lblARBalance.Text = getCustomerProfileResponse.Result.CustomerProfile.ARBalance.ToString();
            //        //this.lblInStoreCredit.Text = getCustomerProfileResponse.Result.CustomerProfile.InStoreCredit.ToString();
            //        //this.label22.Text = getCustomerProfileResponse.Result.CustomerProfile.labe.ToString();
            //        //this.lblLastVoid.Text = getCustomerProfileResponse.Result.CustomerProfile.last.ToString();
            //        //this.lblLastBad.Text = getCustomerProfileResponse.Result.CustomerProfile.LastVoidAmount.ToString();
            //        //this.lblLastLate.Text = getCustomerProfileResponse.Result.CustomerProfile.LastVoidAmount.ToString();
            //        //this.lblLastOrders.Text = getCustomerProfileResponse.Result.CustomerProfile.LastVoidAmount.ToString();
            //        // this.lblLastAverage.Text = getCustomerProfileResponse.Result.CustomerProfile.LastVoidAmount.ToString();
            //        //this.lblLastYear.Text = getCustomerProfileResponse.Result.CustomerProfile.LastVoidAmount.ToString();
            //        // this.label15.Text = getCustomerProfileResponse.Result.CustomerProfile.LastVoidAmount.ToString();
            //        //this.lblYTDVoid.Text = getCustomerProfileResponse.Result.CustomerProfile.LastVoidAmount.ToString();
            //        // this.lblYTDBad.Text = getCustomerProfileResponse.Result.CustomerProfile.LastVoidAmount.ToString();
            //        //this.lblYTDLate.Text = getCustomerProfileResponse.Result.CustomerProfile.LastVoidAmount.ToString();
            //        //this.lblYTDOrders.Text = getCustomerProfileResponse.Result.CustomerProfile.LastVoidAmount.ToString();
            //        //this.lblYTDAverage.Text = getCustomerProfileResponse.Result.CustomerProfile.LastVoidAmount.ToString();
            //        //this.lblElapsedDays.Text = getCustomerProfileResponse.Result.CustomerProfile.Elapsed.ToString();
            //        //this.lblLastOrder.Text = getCustomerProfileResponse.Result.CustomerProfile.LastOrderDate.ToString();
            //        //this.lblFirstOrder.Text = getCustomerProfileResponse.Result.CustomerProfile.FirstOrderDate.ToString();
            //    }

            //}

            //        //this.lblThisYear.Text = getCustomerProfileResponse.Result.CustomerProfile.LastVoidAmount.ToString();
            //        //this.label6.Text = getCustomerProfileResponse.Result.CustomerProfile.LastVoidAmount.ToString();
            //        this.lblGatherInformation.Text = "";
            //        this.lblPerMonth.Text = "per Month";
            //        this.lblAverageOrdersData.Text = getCustomerProfileResponse.Result.CustomerProfile.OrdersPerMonth.ToString();
            //        this.lblElapsedDaysData.Text = getCustomerProfileResponse.Result.CustomerProfile.Elapsed.ToString();
            //        this.lblLastOrderData.Text = getCustomerProfileResponse.Result.CustomerProfile.LastOrderDate.ToString("MM-dd-yyyy");
            //        this.lblFirstOrderData.Text = getCustomerProfileResponse.Result.CustomerProfile.FirstOrderDate.ToString("MM-dd-yyyy");
            //        //this.lblAverageOrders.Text = getCustomerProfileResponse.Result.CustomerProfile.avera.ToString();
        }

        private void tdbmPhone_Number_TextChanged(object sender, EventArgs e)
        {
            try
            {
                
                lblPriorityCustomer.Visible = false;
                
                if (tdbmPhone_Number.TextLength < Session.MaxPhoneDigits)
                {
                    Session.handleHistorybutton = false;
                   
                    ucCustomer_OrderMenu.HandleHistoryButton(Session.handleHistorybutton);
                    Session.handleRemakebutton = false;
                    ucCustomer_OrderMenu.HandleRemakeButton(false);
                }

              
                if (tdbmPhone_Number.TextLength == Session.MaxPhoneDigits)
                {
                    Session.handleRemakebutton = false;
                    Session.handleModify = false;
                    ucCustomer_OrderMenu.HandleModifyButton(false);
                    ucCustomer_OrderMenu.HandleRemakeButton(false);

                    //LaunchCommandLineApp(tdbmPhone_Number.Text + " " + "1"); // One Customer
                    if (Session.cart.Customer == null || (tdbmPhone_Number.Text != Session.cart.Customer.Phone_Number && !Session.IsCallerIDClicked))
                    {
                        uC_Customer_order_Header1.btnStart_Click();
                        if (!Session.IsCallerIDClicked)
                        {
                            FindCustomer(Session._LocationCode, tdbmPhone_Number.Text, tdbtxtPhone_Ext.Text);

                        }
                    }
                    if (!Session.IsCallerIDClicked)
                    {
                        Session.cart.Customer.Phone_Number = this.tdbmPhone_Number.Text;
                    }
                    Session.cart.Customer.Location_Code = Session._LocationCode;//Session.
                    ucCustomer_OrderMenu.ConvertExittoCancel();

                    // one customer
                    UserFunctions.OpenOneCustomer(tdbmPhone_Number.Text + " " + 1);
                }
                else if (Session.cart.Customer !=null && Session.cart.Customer.Phone_Number != null && Session.cart.Customer.Phone_Number.Length == Session.MaxPhoneDigits && tdbmPhone_Number.TextLength < Session.MaxPhoneDigits)
                {
                    Session.cart.Customer = new Customer();
                    
                    tdbtxtPhone_Ext.Text = string.Empty;
                    txtName.Text = string.Empty;
                    TDBdob.Text = string.Empty;
                    TDBanniversary.Text = string.Empty;
                    txtCompanyName.Text = string.Empty;
                    txtStreet_Number.Text = string.Empty;
                    txtAddress_Line_2.Text = string.Empty;
                    this.txtCity.Text = SystemSettings.settings.pstrDefaultCityName;
                    this.txtRegion.Text = SystemSettings.settings.pstrDefaultRegionName;
                    this.txtPostal_Code.Text = SystemSettings.settings.pstrDefaultPostalCode;
                    txtSuite.Text = string.Empty;
                    txtCross_Street.Text = string.Empty;
                    txtStreet.Text = string.Empty;
                    txtTentNumber.Text = string.Empty;
                    txtgstin_number.Text = string.Empty;

                    //Notes
                    txtNotes_Instore.Text = string.Empty;
                    txtNotes_Delivery.Text = string.Empty;
                    txtNotes_manager.Text = string.Empty;

                    //Profile
                    this.lblCreditLimitData.Text = string.Empty;
                    this.lblARBalanceData.Text = string.Empty;
                    this.lblInStoreCreditData.Text = string.Empty;
                    this.lblLastVoidAmount.Text = string.Empty;
                    this.lblLastVoidCount.Text = string.Empty;
                    this.lblLastBadAmount.Text = string.Empty;
                    this.lblLastBadCount.Text = string.Empty;
                    this.lblLastLateAmount.Text = string.Empty;
                    this.lblLastLateCount.Text = string.Empty;
                    this.lblLastOrdersAmount.Text = string.Empty;
                    this.lblLastOrdersCount.Text = string.Empty;
                    this.lblLastAverageData.Text = string.Empty;
                    this.lblYTDVoidAmount.Text = string.Empty;
                    this.lblYTDVoidCount.Text = string.Empty;
                    this.lblYTDBadAmount.Text = string.Empty;
                    this.lblYTDBadCount.Text = string.Empty;
                    this.lblYTDLateAmount.Text = string.Empty;
                    this.lblYTDLateCount.Text = string.Empty;
                    this.lblYTDOrdersAmount.Text = string.Empty;
                    this.lblYTDAverageData.Text = string.Empty;
                    this.lblYTDOrdersCount.Text = string.Empty;
                    this.lblPerMonth.Text = "per Month";
                    this.lblAverageOrdersData.Text = string.Empty;
                    this.lblElapsedDaysData.Text = string.Empty;
                    this.lblLastOrderData.Text = string.Empty;
                    this.lblFirstOrderData.Text = string.Empty;
                    this.lblGatherInformation.Text = string.Empty;

                    this.TDBdob.Value = TDBdob.MinDate;
                    this.TDBanniversary.Value = TDBanniversary.MinDate;

                    this.lvw_Cities.Items.Clear();
                    this.lvw_Streets.Items.Clear();
                    this.lvw_CrossStreets.Items.Clear();
                    this.lvw_Address.Items.Clear();

                }
            }
            catch (Exception ex)
            {
                Logger.Trace("ERROR", "frmCustomer-tdbmPhone_Number_TextChanged(): " + ex.Message, ex, true);
            }
        }


        private void FindCustomer(string locationCode, string phoneNumber, string phoneExtension)
        {
            try
            {   
                
                CustomerLookUpRequest requestModel = new CustomerLookUpRequest();

                requestModel.LocationCode = locationCode;
                requestModel.PhoneNumber = phoneNumber;
                requestModel.PhoneNumberExt = phoneExtension;

                CustomerLookUpResponse apiResponse = APILayer.CustomerLookUp(APILayer.CallType.POST, requestModel);
                //CustomerLookUpResponse apiResponseGlobalList = new CustomerLookUpResponse();
                if (apiResponse != null && apiResponse.Result != null)
                {
                    CustomerResult = apiResponse.Result;
                    //if (apiResponse.Result.CustomerDetail != null && !string.IsNullOrEmpty(apiResponse.Result.CustomerDetail.PhoneNumber))
                    if (apiResponse.Result.CustomerDetail != null)
                    {
                        //this.tabControl_CallerNotesProfile.Controls[0].Visible = false;
                        //this.tabControl_CallerNotesProfile.Controls[1].Visible = true;
                        //this.tabPage_Notes.Show();
                        //this.tabControl_CallerNotesProfile.Controls[2].Visible = false;
                        if (Session.cart.Customer == null)
                        {
                            Session.cart.Customer = new Customer();
                        }

                        Session.cart.Customer.Customer_Code = apiResponse.Result.CustomerDetail.CustomerCode;
                        if (Session.cart.Customer.Customer_Code != 0)
                        {
                            List<CustomerOrderHistory> customerOrderHistories = new List<CustomerOrderHistory>();
                            customerOrderHistories = APILayer.GetCustomerOrderHistory(Session._LocationCode, Convert.ToInt64(Session.cart.Customer.Customer_Code));
                            if (customerOrderHistories != null && customerOrderHistories.Count > 0)
                            {

                                //Session.handleModify = false;
                                Session.handleHistorybutton = true;
                                Session.handleRemakebutton = false;
                                ucCustomer_OrderMenu.HandleHistoryButton(Session.handleHistorybutton);
                            }
                            List<CustomerOrderRemake> customerOrderRemakes = new List<CustomerOrderRemake>();
                            customerOrderRemakes = APILayer.GetCustomerOrderRemake(Session._LocationCode, Convert.ToInt64(Session.cart.Customer.Customer_Code));

                            if (customerOrderRemakes != null && customerOrderRemakes.Count > 0)
                            {
                                Session.handleRemakebutton = true;
                                ucCustomer_OrderMenu.HandleRemakeButton(true);
                            }
                            else
                            {
                                Session.handleRemakebutton = false;
                                ucCustomer_OrderMenu.HandleRemakeButton(false);

                            }

                        }
                        else
                        {
                            Session.handleHistorybutton = false;
                            Session.handleRemakebutton = false;
                            ucCustomer_OrderMenu.HandleHistoryButton(Session.handleHistorybutton);
                            ucCustomer_OrderMenu.HandleRemakeButton(false);
                            
                        }


                        Session.cart.Customer.Postal_Code = txtPostal_Code.Text;
                        Session.cart.Customer.Street_Number = txtStreet_Number.Text;
                        Session.cart.Customer.City = txtCity.Text;
                        Session.cart.Customer.Region = txtRegion.Text;
                        TabControl tabControl = (TabControl)(this.tabControl_CallerNotesProfile);
                        tabControl.SelectedTab = this.tabPage_Notes;

                        tdbtxtPhone_Ext.Text = apiResponse.Result.CustomerDetail.PhoneExt;
                        txtName.Text = apiResponse.Result.CustomerDetail.Name;
                        if (apiResponse.Result.CustomerDetail.dateofbirth.ToString() == "1/1/0001 12:00:00 AM")

                            TDBdob.Text = TDBdob.MinDate.ToShortDateString();
                        else
                            TDBdob.Text = (apiResponse.Result.CustomerDetail.dateofbirth >= TDBdob.MinDate && apiResponse.Result.CustomerDetail.dateofbirth <= TDBdob.MaxDate) ? apiResponse.Result.CustomerDetail.dateofbirth.ToShortDateString() : TDBdob.MinDate.ToShortDateString();

                        /* TDBdob.Text = (apiResponse.Result.CustomerDetail.dateofbirth.ToShortDateString() == "01-01-1900" || apiResponse.Result.CustomerDetail.dateofbirth.ToShortDateString() == "01-01-0001")  ? TDBdob.MinDate.ToShortDateString() : apiResponse.Result.CustomerDetail.dateofbirth.ToShortDateString();*/

                        if (apiResponse.Result.CustomerDetail.anniversarydate.ToString() == "1/1/0001 12:00:00 AM")
                            TDBanniversary.Text= DateTime.Now.ToString("dd/MM/yyyy");
                        else
                        TDBanniversary.Text = (apiResponse.Result.CustomerDetail.anniversarydate >= TDBanniversary.MinDate && apiResponse.Result.CustomerDetail.anniversarydate <= TDBanniversary.MaxDate) ? apiResponse.Result.CustomerDetail.anniversarydate.ToShortDateString() : TDBanniversary.MinDate.ToShortDateString();

                        //txtCompanyName.Text = apiResponse.Result.CustomerDetail.comp;
                        //txtStreet_Number.Text = apiResponse.Result.CustomerDetail.Street;
                        //txtAddress_Line_2.Text = apiResponse.Result.CustomerDetail.str;
                        txtCity.Tag =  apiResponse.Result.CustomerDetail.Customer_City_Code;
                        txtCity.Text = apiResponse.Result.CustomerDetail.CityName;
                        Session.cart.Customer.City = apiResponse.Result.CustomerDetail.CityName;
                        //txtPostal_Code.Text = apiResponse.Result.CustomerDetail.pin;
                        //txtGSTIN.Text = string.Empty;
                        //txtSuite.Text = apiResponse.Result.CustomerDetail.sui;
                        txtgstin_number.Text = apiResponse.Result.CustomerDetail.GSTIN;
                        txtCross_Street.Text = apiResponse.Result.CustomerDetail.CrossStreet;
                        txtRegion.Text = apiResponse.Result.CustomerDetail.State;
                        Session.cart.Customer.Region = apiResponse.Result.CustomerDetail.State;
                        txtStreet.Text = apiResponse.Result.CustomerDetail.Street;
                        txtTentNumber.Text = string.Empty;

                        if (!string.IsNullOrEmpty(apiResponse.Result.CustomerDetail.AddressType))
                        {
                            Session.cart.Customer.Address_Type = apiResponse.Result.CustomerDetail.AddressType;
                            if (!string.IsNullOrEmpty(Session.cart.Customer.Address_Type))
                            {
                                if (Session.cart.Customer.Address_Type == "R")
                                {
                                    this.btn_Residence.PerformClick();
                                }
                                else if (Session.cart.Customer.Address_Type == "K")
                                {
                                    this.btn_Kiosk.PerformClick();
                                }
                                else if (Session.cart.Customer.Address_Type == "H")
                                {
                                    this.btn_Kiosk.PerformClick();
                                }
                                else if (Session.cart.Customer.Address_Type == "B")
                                {
                                    this.btn_Business.PerformClick();
                                }
                                else if (Session.cart.Customer.Address_Type == "C")
                                {
                                    this.btn_College.PerformClick();
                                }
                            }
                        }

                        if (apiResponse.Result.CustomerDetail.Comments != null)
                        {
                            int indexCommentsStart = apiResponse.Result.CustomerDetail.Comments.IndexOf("fs17");

                            if (indexCommentsStart > 0)
                            {
                                string comment = apiResponse.Result.CustomerDetail.Comments.Substring(indexCommentsStart + 4, apiResponse.Result.CustomerDetail.Comments.Length - (indexCommentsStart + 4) - 8);
                                txtNotes_Instore.Text = comment;
                            }
                            else
                            {
                                //txtNotes_Instore.Text = apiResponse.Result.CustomerDetail.Comments;
                            }
                        }
                        else
                        {
                            txtNotes_Instore.Text = apiResponse.Result.CustomerDetail.Comments;
                        }


                        if (apiResponse.Result.CustomerDetail.DriverComments != null)
                        {
                            int indexDriverCommentsStart = apiResponse.Result.CustomerDetail.DriverComments.IndexOf("fs17");

                            if (indexDriverCommentsStart > 0)
                            {
                                string comment = apiResponse.Result.CustomerDetail.DriverComments.Substring(indexDriverCommentsStart + 4, apiResponse.Result.CustomerDetail.DriverComments.Length - (indexDriverCommentsStart + 4) - 8);
                                txtNotes_Delivery.Text = comment;
                            }
                            else
                            {
                                //txtNotes_Delivery.Text = apiResponse.Result.CustomerDetail.DriverComments;
                                txtNotes_Delivery.Text = "";
                            }
                        }
                        else
                        {
                            txtNotes_Delivery.Text = apiResponse.Result.CustomerDetail.DriverComments;
                        }

                        if (apiResponse.Result.CustomerDetail.ManagerNotes != null)
                        {
                            int indexManagerNotesStart = apiResponse.Result.CustomerDetail.ManagerNotes.IndexOf("fs17");

                            if (indexManagerNotesStart > 0)
                            {
                                string comment = apiResponse.Result.CustomerDetail.ManagerNotes.Substring(indexManagerNotesStart + 4, apiResponse.Result.CustomerDetail.ManagerNotes.Length - (indexManagerNotesStart + 4) - 8);
                                txtNotes_manager.Text = comment;
                            }
                            else
                            {
                                txtNotes_manager.Text = apiResponse.Result.CustomerDetail.ManagerNotes;
                            }

                        }
                        else
                        {
                            txtNotes_manager.Text = apiResponse.Result.CustomerDetail.ManagerNotes;
                        }

                        //Session.cart.Customer.credit
                        //txtNotes_Instore.Text = apiResponse.Result.CustomerDetail.Comments;
                        //txtNotes_Delivery.Text = apiResponse.Result.CustomerDetail.DriverComments;
                        //txtNotes_manager.Text = apiResponse.Result.CustomerDetail.ManagerNotes;

                        //Profile
                        GetCustomerProfileRequest getCustomerProfileRequest = new GetCustomerProfileRequest();
                        getCustomerProfileRequest.CustomerCode = apiResponse.Result.CustomerDetail.CustomerCode.ToString();//Session.CurrentEmployee.LoginDetail.
                        getCustomerProfileRequest.LocationCode = Session._LocationCode;// Session.CurrentEmployee.LoginDetail.

                        GetCustomerProfileResponse getCustomerProfileResponse = APILayer.GetCustomerProfile(APILayer.CallType.POST, getCustomerProfileRequest);

                        if (getCustomerProfileResponse != null && getCustomerProfileResponse.Result != null && getCustomerProfileResponse.Result.CustomerProfile != null)
                        {
                            //if (!Session.FromOrder)
                            //{
                            Session.cart.Customer.First_Order_Date = getCustomerProfileResponse.Result.CustomerProfile.FirstOrderDate;
                            Session.cart.Customer.Last_Order_Date = getCustomerProfileResponse.Result.CustomerProfile.LastOrderDate;
                            //}

                            this.lblCreditLimitData.Text = getCustomerProfileResponse.Result.CustomerProfile.CreditLimit.ToString();
                            Session.CustomerProfileCollection.CreditLimit = getCustomerProfileResponse.Result.CustomerProfile.CreditLimit;
                            this.lblARBalanceData.Text = getCustomerProfileResponse.Result.CustomerProfile.ARBalance.ToString();
                            Session.CustomerProfileCollection.ARBalance = getCustomerProfileResponse.Result.CustomerProfile.ARBalance;
                            this.lblInStoreCreditData.Text = getCustomerProfileResponse.Result.CustomerProfile.InStoreCredit.ToString();
                            Session.CustomerProfileCollection.InStoreCredit = getCustomerProfileResponse.Result.CustomerProfile.InStoreCredit;
                            this.lblLastVoidAmount.Text = getCustomerProfileResponse.Result.CustomerProfile.LastVoidAmount.ToString("#,##0.00");
                            Session.CustomerProfileCollection.LastVoidAmount = getCustomerProfileResponse.Result.CustomerProfile.LastVoidAmount;
                            this.lblLastVoidCount.Text = getCustomerProfileResponse.Result.CustomerProfile.LastVoidCount.ToString();
                            Session.CustomerProfileCollection.LastVoidCount = getCustomerProfileResponse.Result.CustomerProfile.LastVoidCount;

                            this.lblLastBadAmount.Text = getCustomerProfileResponse.Result.CustomerProfile.LastBadAmount.ToString("#,##0.00");
                            Session.CustomerProfileCollection.LastBadAmount = getCustomerProfileResponse.Result.CustomerProfile.LastBadAmount;
                            this.lblLastBadCount.Text = getCustomerProfileResponse.Result.CustomerProfile.LastBadCount.ToString();
                            Session.CustomerProfileCollection.LastBadCount = getCustomerProfileResponse.Result.CustomerProfile.LastBadCount;

                            this.lblLastLateAmount.Text = getCustomerProfileResponse.Result.CustomerProfile.LastLateAmount.ToString("#,##0.00");
                            Session.CustomerProfileCollection.LastLateAmount = getCustomerProfileResponse.Result.CustomerProfile.LastLateAmount;

                            this.lblLastLateCount.Text = getCustomerProfileResponse.Result.CustomerProfile.LastLateCount.ToString();
                            Session.CustomerProfileCollection.LastLateCount = getCustomerProfileResponse.Result.CustomerProfile.LastLateCount;
                            this.lblLastOrdersAmount.Text = getCustomerProfileResponse.Result.CustomerProfile.LastOrdersAmount.ToString("#,##0.00");
                            Session.CustomerProfileCollection.LastOrdersAmount = getCustomerProfileResponse.Result.CustomerProfile.LastOrdersAmount;
                            this.lblLastOrdersCount.Text = getCustomerProfileResponse.Result.CustomerProfile.LastOrdersCount.ToString();
                            Session.CustomerProfileCollection.LastOrdersCount = getCustomerProfileResponse.Result.CustomerProfile.LastOrdersCount;
                            this.lblLastAverageData.Text = getCustomerProfileResponse.Result.CustomerProfile.LastAverage.ToString();
                            Session.CustomerProfileCollection.LastAverage = getCustomerProfileResponse.Result.CustomerProfile.LastAverage;
                            this.lblYTDVoidAmount.Text = getCustomerProfileResponse.Result.CustomerProfile.YTDVoidAmount.ToString("#,##0.00");
                            Session.CustomerProfileCollection.YTDVoidAmount = getCustomerProfileResponse.Result.CustomerProfile.YTDVoidAmount;

                            this.lblYTDVoidCount.Text = getCustomerProfileResponse.Result.CustomerProfile.YTDVoidCount.ToString();
                            Session.CustomerProfileCollection.YTDVoidCount = getCustomerProfileResponse.Result.CustomerProfile.YTDVoidCount;
                            this.lblYTDBadAmount.Text = getCustomerProfileResponse.Result.CustomerProfile.YTDBadAmount.ToString("#,##0.00");
                            Session.CustomerProfileCollection.YTDBadAmount = getCustomerProfileResponse.Result.CustomerProfile.YTDBadAmount;

                            this.lblYTDBadCount.Text = getCustomerProfileResponse.Result.CustomerProfile.YTDBadCount.ToString();
                            Session.CustomerProfileCollection.YTDBadCount = getCustomerProfileResponse.Result.CustomerProfile.YTDBadCount;
                            this.lblYTDLateAmount.Text = getCustomerProfileResponse.Result.CustomerProfile.YTDLateAmount.ToString("#,##0.00");
                            Session.CustomerProfileCollection.YTDLateAmount = getCustomerProfileResponse.Result.CustomerProfile.YTDLateAmount;
                            this.lblYTDLateCount.Text = getCustomerProfileResponse.Result.CustomerProfile.YTDLateCount.ToString();
                            Session.CustomerProfileCollection.YTDLateCount = getCustomerProfileResponse.Result.CustomerProfile.YTDLateCount;
                            this.lblYTDOrdersAmount.Text = getCustomerProfileResponse.Result.CustomerProfile.YTDOrdersAmount.ToString("#,##0.00");
                            Session.CustomerProfileCollection.YTDOrdersAmount = getCustomerProfileResponse.Result.CustomerProfile.YTDOrdersAmount;
                            this.lblYTDAverageData.Text = getCustomerProfileResponse.Result.CustomerProfile.YTDAverage.ToString();
                            Session.CustomerProfileCollection.YTDAverage = getCustomerProfileResponse.Result.CustomerProfile.YTDAverage;
                            this.lblYTDOrdersCount.Text = getCustomerProfileResponse.Result.CustomerProfile.YTDOrdersCount.ToString();
                            Session.CustomerProfileCollection.YTDOrdersCount = getCustomerProfileResponse.Result.CustomerProfile.YTDOrdersCount;
                            //this.lblCreditLimit.Text = getCustomerProfileResponse.Result.CustomerProfile.CreditLimit.ToString();
                            //this.lblARBalance.Text = getCustomerProfileResponse.Result.CustomerProfile.ARBalance.ToString();
                            //this.lblInStoreCredit.Text = getCustomerProfileResponse.Result.CustomerProfile.InStoreCredit.ToString();
                            //this.label22.Text = getCustomerProfileResponse.Result.CustomerProfile.labe.ToString();
                            //this.lblLastVoid.Text = getCustomerProfileResponse.Result.CustomerProfile.last.ToString();
                            //this.lblLastBad.Text = getCustomerProfileResponse.Result.CustomerProfile.LastVoidAmount.ToString();
                            //this.lblLastLate.Text = getCustomerProfileResponse.Result.CustomerProfile.LastVoidAmount.ToString();
                            //this.lblLastOrders.Text = getCustomerProfileResponse.Result.CustomerProfile.LastVoidAmount.ToString();
                            // this.lblLastAverage.Text = getCustomerProfileResponse.Result.CustomerProfile.LastVoidAmount.ToString();
                            //this.lblLastYear.Text = getCustomerProfileResponse.Result.CustomerProfile.LastVoidAmount.ToString();
                            // this.label15.Text = getCustomerProfileResponse.Result.CustomerProfile.LastVoidAmount.ToString();
                            //this.lblYTDVoid.Text = getCustomerProfileResponse.Result.CustomerProfile.LastVoidAmount.ToString();
                            // this.lblYTDBad.Text = getCustomerProfileResponse.Result.CustomerProfile.LastVoidAmount.ToString();
                            //this.lblYTDLate.Text = getCustomerProfileResponse.Result.CustomerProfile.LastVoidAmount.ToString();
                            //this.lblYTDOrders.Text = getCustomerProfileResponse.Result.CustomerProfile.LastVoidAmount.ToString();
                            //this.lblYTDAverage.Text = getCustomerProfileResponse.Result.CustomerProfile.LastVoidAmount.ToString();
                            //this.lblThisYear.Text = getCustomerProfileResponse.Result.CustomerProfile.LastVoidAmount.ToString();
                            //this.label6.Text = getCustomerProfileResponse.Result.CustomerProfile.LastVoidAmount.ToString();
                            this.lblGatherInformation.Text = "";
                            this.lblPerMonth.Text = "per Month";
                            this.lblAverageOrdersData.Text = getCustomerProfileResponse.Result.CustomerProfile.OrdersPerMonth.ToString();
                            Session.CustomerProfileCollection.OrdersPerMonth = getCustomerProfileResponse.Result.CustomerProfile.OrdersPerMonth;
                            this.lblElapsedDaysData.Text = getCustomerProfileResponse.Result.CustomerProfile.Elapsed.ToString();
                            Session.CustomerProfileCollection.Elapsed = getCustomerProfileResponse.Result.CustomerProfile.Elapsed;
                            this.lblLastOrderData.Text = getCustomerProfileResponse.Result.CustomerProfile.LastOrderDate.ToString("MM-dd-yyyy");
                            Session.CustomerProfileCollection.LastOrderDate = getCustomerProfileResponse.Result.CustomerProfile.LastOrderDate;
                            this.lblFirstOrderData.Text = getCustomerProfileResponse.Result.CustomerProfile.FirstOrderDate.ToString("MM-dd-yyyy");
                            Session.CustomerProfileCollection.FirstOrderDate = getCustomerProfileResponse.Result.CustomerProfile.FirstOrderDate;
                            //this.lblAverageOrders.Text = getCustomerProfileResponse.Result.CustomerProfile.avera.ToString();
                            //this.lblElapsedDays.Text = getCustomerProfileResponse.Result.CustomerProfile.Elapsed.ToString();
                            //this.lblLastOrder.Text = getCustomerProfileResponse.Result.CustomerProfile.LastOrderDate.ToString();
                            //this.lblFirstOrder.Text = getCustomerProfileResponse.Result.CustomerProfile.FirstOrderDate.ToString();
                            if (getCustomerProfileResponse.Result.CustomerProfile.InStoreCredit == 0)
                            {
                                this.lblARBalance.Visible = false;
                                this.lblCreditLimit.Visible = false;
                                this.lblARBalanceData.Visible = false;
                                this.lblCreditLimitData.Visible = false;
                            }
                        }

                    }
                    //else
                    //{
                    //    //Fetch Customer details from GlobalList
                    //    apiResponseGlobalList = APILayer.CustomerLookUpGlobalList(APILayer.CallType.POST, requestModel);
                    //    if (apiResponseGlobalList != null && apiResponseGlobalList.Result != null)
                    //    {
                    //        CustomerResult = apiResponseGlobalList.Result;
                    //        if (apiResponseGlobalList.Result.CustomerDetail != null && !string.IsNullOrEmpty(apiResponseGlobalList.Result.CustomerDetail.PhoneNumber))
                    //        {
                    //            TabControl tabControl = (TabControl)(this.tabControl_CallerNotesProfile);
                    //            tabControl.SelectedTab = this.tabPage_Notes;

                    //            tdbtxtPhone_Ext.Text = apiResponseGlobalList.Result.CustomerDetail.PhoneExt;
                    //            txtName.Text = apiResponseGlobalList.Result.CustomerDetail.Name;
                    //            TDBdob.Text = (apiResponseGlobalList.Result.CustomerDetail.dateofbirth.ToShortDateString() == "01-01-1900" || apiResponseGlobalList.Result.CustomerDetail.dateofbirth.ToShortDateString() == "01-01-0001") ? string.Empty : apiResponseGlobalList.Result.CustomerDetail.dateofbirth.ToShortDateString();
                    //            TDBanniversary.Text = (apiResponseGlobalList.Result.CustomerDetail.anniversarydate.ToShortDateString() == "01-01-1900" || apiResponseGlobalList.Result.CustomerDetail.anniversarydate.ToShortDateString() == "01-01-0001") ? string.Empty : apiResponseGlobalList.Result.CustomerDetail.anniversarydate.ToShortDateString();
                    //            //txtCompanyName.Text = apiResponse.Result.CustomerDetail.comp;
                    //            //txtStreet_Number.Text = apiResponse.Result.CustomerDetail.Street;
                    //            //txtAddress_Line_2.Text = apiResponse.Result.CustomerDetail.str;
                    //            txtCity.Text = apiResponseGlobalList.Result.CustomerDetail.CityName;
                    //            //txtPostal_Code.Text = apiResponse.Result.CustomerDetail.pin;
                    //            //txtGSTIN.Text = string.Empty;
                    //            //txtSuite.Text = apiResponse.Result.CustomerDetail.sui;
                    //            txtgstin_number.Text = apiResponseGlobalList.Result.CustomerDetail.GSTIN;
                    //            txtCross_Street.Text = apiResponseGlobalList.Result.CustomerDetail.CrossStreet;
                    //            txtRegion.Text = apiResponseGlobalList.Result.CustomerDetail.State;
                    //            txtStreet.Text = apiResponseGlobalList.Result.CustomerDetail.Street;
                    //            txtTentNumber.Text = string.Empty;

                    //            if (!string.IsNullOrEmpty(apiResponseGlobalList.Result.CustomerDetail.AddressType))
                    //            {
                    //                Session.cart.Customer.Address_Type = apiResponseGlobalList.Result.CustomerDetail.AddressType;
                    //                if (!string.IsNullOrEmpty(Session.cart.Customer.Address_Type))
                    //                {
                    //                    if (Session.cart.Customer.Address_Type == "R")
                    //                    {
                    //                        this.btn_Residence.PerformClick();
                    //                    }
                    //                    else if (Session.cart.Customer.Address_Type == "K")
                    //                    {
                    //                        this.btn_Kiosk.PerformClick();
                    //                    }
                    //                    else if (Session.cart.Customer.Address_Type == "H")
                    //                    {
                    //                        this.btn_Kiosk.PerformClick();
                    //                    }
                    //                    else if (Session.cart.Customer.Address_Type == "B")
                    //                    {
                    //                        this.btn_Business.PerformClick();
                    //                    }
                    //                    else if (Session.cart.Customer.Address_Type == "C")
                    //                    {
                    //                        this.btn_College.PerformClick();
                    //                    }
                    //                }
                    //            }

                    //            if (apiResponseGlobalList.Result.CustomerDetail.Comments != null)
                    //            {
                    //                int indexCommentsStart = apiResponseGlobalList.Result.CustomerDetail.Comments.IndexOf("fs17");

                    //                if (indexCommentsStart > 0)
                    //                {
                    //                    string comment = apiResponseGlobalList.Result.CustomerDetail.Comments.Substring(indexCommentsStart + 4, apiResponseGlobalList.Result.CustomerDetail.Comments.Length - (indexCommentsStart + 4) - 8);
                    //                    txtNotes_Instore.Text = comment;
                    //                }
                    //                else
                    //                {
                    //                    txtNotes_Instore.Text = apiResponseGlobalList.Result.CustomerDetail.Comments;
                    //                }
                    //            }
                    //            else
                    //            {
                    //                txtNotes_Instore.Text = apiResponseGlobalList.Result.CustomerDetail.Comments;
                    //            }


                    //            if (apiResponseGlobalList.Result.CustomerDetail.DriverComments != null)
                    //            {
                    //                int indexDriverCommentsStart = apiResponseGlobalList.Result.CustomerDetail.DriverComments.IndexOf("fs17");

                    //                if (indexDriverCommentsStart > 0)
                    //                {
                    //                    string comment = apiResponseGlobalList.Result.CustomerDetail.DriverComments.Substring(indexDriverCommentsStart + 4, apiResponseGlobalList.Result.CustomerDetail.DriverComments.Length - (indexDriverCommentsStart + 4) - 8);
                    //                    txtNotes_Delivery.Text = comment;
                    //                }
                    //                else
                    //                {
                    //                    txtNotes_Delivery.Text = apiResponseGlobalList.Result.CustomerDetail.DriverComments;
                    //                }
                    //            }
                    //            else
                    //            {
                    //                txtNotes_Delivery.Text = apiResponseGlobalList.Result.CustomerDetail.DriverComments;
                    //            }

                    //            if (apiResponseGlobalList.Result.CustomerDetail.ManagerNotes != null)
                    //            {
                    //                int indexManagerNotesStart = apiResponseGlobalList.Result.CustomerDetail.ManagerNotes.IndexOf("fs17");

                    //                if (indexManagerNotesStart > 0)
                    //                {
                    //                    string comment = apiResponseGlobalList.Result.CustomerDetail.ManagerNotes.Substring(indexManagerNotesStart + 4, apiResponseGlobalList.Result.CustomerDetail.ManagerNotes.Length - (indexManagerNotesStart + 4) - 8);
                    //                    txtNotes_manager.Text = comment;
                    //                }
                    //                else
                    //                {
                    //                    txtNotes_manager.Text = apiResponseGlobalList.Result.CustomerDetail.ManagerNotes;
                    //                }

                    //            }
                    //            else
                    //            {
                    //                txtNotes_manager.Text = apiResponseGlobalList.Result.CustomerDetail.ManagerNotes;
                    //            }
                    //        }
                    //    }

                    //}

                    if (apiResponse.Result.customerAddresses != null)
                    {
                        //CustomerAPIAddress = apiResponse.Result.customerAddresses;
                        if (apiResponse.Result.customerAddresses != null && apiResponse.Result.customerAddresses.Count > 0)
                        {
                            foreach (CustomerAddressResult address in apiResponse.Result.customerAddresses)
                            {
                                if (address.IsLastAddress)
                                {
                                    txtCompanyName.Text = address.CompanyName;
                                    txtStreet_Number.Text = address.StreetNumber;
                                    txtAddress_Line_2.Text = address.AddressLine2;
                                    txtAddress_Line_3.Text = address.AddressLine3;
                                    txtAddress_Line_4.Text = address.AddressLine4;
                                    txtPostal_Code.Text = address.PostalCode;
                                    txtSuite.Text = address.Suite;
                                 
                                }
                                else
                                {
                                    txtStreet_Number.Text = address.StreetNumber;
                                    txtPostal_Code.Text = address.PostalCode;

                                }
                                Session.cart.Customer.Postal_Code = txtPostal_Code.Text;
                                Session.cart.Customer.Street_Number =txtStreet_Number.Text;
                            }
                            
                        }
                        else
                        {
                            //if (apiResponseGlobalList.Result.customerAddresses != null && apiResponseGlobalList.Result.customerAddresses.Count > 0)
                            //{
                            //    foreach (CustomerAddressResult address in apiResponseGlobalList.Result.customerAddresses)
                            //    {
                            //        if (address.IsLastAddress)
                            //        {
                            //            txtCompanyName.Text = address.CompanyName;
                            //            txtStreet_Number.Text = address.StreetNumber;
                            //            txtAddress_Line_2.Text = address.AddressLine2;
                            //            txtAddress_Line_3.Text = address.AddressLine3;
                            //            txtAddress_Line_4.Text = address.AddressLine4;
                            //            txtPostal_Code.Text = address.PostalCode;
                            //            txtSuite.Text = address.Suite;
                            //        }
                            //    }
                            //}
                            //else
                            //{
                            //    txtCompanyName.Text = string.Empty;
                            //    txtStreet_Number.Text = string.Empty;
                            //    txtAddress_Line_2.Text = string.Empty;
                            //    txtAddress_Line_3.Text = string.Empty;
                            //    txtAddress_Line_4.Text = string.Empty;
                            //    //txtPostal_Code.Text = string.Empty;
                            //    txtSuite.Text = string.Empty;
                            //}

                            txtCompanyName.Text = string.Empty;
                            txtStreet_Number.Text = string.Empty;
                            txtAddress_Line_2.Text = string.Empty;
                            txtAddress_Line_3.Text = string.Empty;
                            txtAddress_Line_4.Text = string.Empty;
                            txtPostal_Code.Text = string.Empty;
                            txtSuite.Text = string.Empty;

                        }
                    }

                    if (apiResponse.Result.customerPriorities != null)
                    {
                        //CustomerAPIAddress = apiResponse.Result.customerAddresses;
                        if (apiResponse.Result.customerPriorities != null && apiResponse.Result.customerPriorities.Count > 0)
                        {
                            if (!string.IsNullOrEmpty(apiResponse.Result.customerPriorities[0].PriorityCustomerSymbol))
                            {
                                lblPriorityCustomer.Visible = true;
                                lblPriorityCustomer.BackColor = Color.Yellow;
                                lblPriorityCustomer.Text = apiResponse.Result.customerPriorities[0].PriorityCustomerSymbol;
                                Session.PriorityCustomer = lblPriorityCustomer.Text;

                                tmrVIP.Start();
                                tmrVIP.Enabled = true;
                            }
                            else
                            {
                                lblPriorityCustomer.Visible = false;
                                lblPriorityCustomer.Text = string.Empty;
                                tmrVIP.Stop();
                                tmrVIP.Enabled = false;
                            }
                        }
                        else
                        {
                            lblPriorityCustomer.Visible = false;
                            lblPriorityCustomer.Text = string.Empty;

                        }
                    }
                }

                //txtPhoneNumber.Text = apiResponse.result.CustomerDetail.Phone_Number;
                //txtExt.Text = apiResponse.result.CustomerDetail.Phone_Ext;
                //txtName.Text = apiResponse.result.CustomerDetail.Name;
                //txtDOB.Text = apiResponse.result.CustomerDetail.date_of_birth.ToShortDateString() == "01-01-1900" ? string.Empty : apiResponse.result.CustomerDetail.date_of_birth.ToShortDateString();
                //txtAnniversary.Text = apiResponse.result.CustomerDetail.anniversary_date.ToShortDateString() == "01-01-1900" ? string.Empty : apiResponse.result.CustomerDetail.anniversary_date.ToShortDateString();
                //txtCompanyName.Text = apiResponse.result.CustomerDetail.Company_Name;
                //txtStreetNumber1.Text = apiResponse.result.CustomerDetail.Street_Number;
                //txtAddress.Text = apiResponse.result.CustomerDetail.Address_Line_2 + apiResponse.result.CustomerDetail.Address_Line_3 + apiResponse.result.CustomerDetail.Address_Line_4;
                //txtCity.Text = apiResponse.result.CustomerDetail.Address_Line_2;
                //txtPostalCode.Text = apiResponse.result.CustomerDetail.Postal_Code;
                //txtGSTIN.Text = apiResponse.result.CustomerDetail.GSTIN;
                ////tbNotes.Text = apiResponse.result.CustomerDetail.Comments;

                //Setting.CustomerCode = apiResponse.result.CustomerDetail.Customer_Code;




                //tbNotes.Text = apiResponse.result.CustomerDetail;


            }
            catch (Exception ex)
            {
                // Session.cart.Customer.Customer_Street_Name;
                Logger.Trace("ERROR", "frmCustomer-FindCustomer(): " + ex.Message, ex, true);
            }
        }

        private void lvw_Streets_Click(object sender, EventArgs e)
        {
            try
            {
                ListView listView = (ListView)sender;
                StreetLookUp street = Session.StreetsAPIResponse.Where(x => x.StreetName.Trim() == listView.FocusedItem.Text.Trim()).FirstOrDefault();
                this.txtStreet.Text = street.StreetName;

                Session.cart.Customer.Customer_Street_Name = this.txtStreet.Text;

                lvw_Streets.Items.Clear();
                ListViewItem item = new ListViewItem(new[] { street.StreetName, street.CityName });
                lvw_Streets.Items.Add(item);
                
                this.txtCity.Tag = street.CityCode;
                this.txtCity.Text = street.CityName;
                this.txtRegion.Text = street.RegionName;

                lvw_Streets.Items[0].Selected = true;

                txtCity_TextChanged(null, null);
                txtRegion_TextChanged(null, null);
                txtPostal_Code_TextChanged(null, null);

                //lvw_Streets.Items.Add(listView.FocusedItem.Text);
            }
            catch (Exception ex)
            {
                Logger.Trace("ERROR", "frmCustomer-lvw_Streets_Click(): " + ex.Message, ex, true);
            }
        }

        private void txtStreet_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Session.cart.Customer.Customer_Street_Name = this.txtStreet.Text;
                Session.cart.Customer.Street_Code = 0;

                if (Session.StreetsAPIResponse == null)
                    PopulateStreets();

                if (Session.StreetsAPIResponse != null)
                {
                    if (!string.IsNullOrEmpty(this.txtStreet.Text.Trim()))
                    {
                        StreetLookUp street = Session.StreetsAPIResponse.Where(x => x.StreetName.ToLower().Trim() == this.txtStreet.Text.ToLower().Trim()).FirstOrDefault();

                        if (street != null && !string.IsNullOrEmpty(street.StreetName.Trim()) && street.StreetName.Trim() != "#")
                        {
                            lvw_Streets.Items.Clear();
                            ListViewItem item = new ListViewItem(new[] { street.StreetName.Trim(), street.CityName.Trim() });
                            lvw_Streets.Items.Add(item);

                            Session.cart.Customer.Street_Code = street.StreetCode;
                        }
                        else
                        {
                            List<StreetLookUp> streets = Session.StreetsAPIResponse.Where(x => x.StreetName.ToLower().Contains(this.txtStreet.Text.ToLower().Trim())).ToList();

                            lvw_Streets.Items.Clear();

                            if (streets != null)
                            {
                                foreach (StreetLookUp streetLookUp in streets)
                                {
                                    ListViewItem item = new ListViewItem(new[] { streetLookUp.StreetName, streetLookUp.CityName });
                                    lvw_Streets.Items.Add(item);
                                }
                            }
                        }
                    }
                    else if (lvw_Streets.Items.Count == 0)
                    {
                        lvw_Streets.Items.AddRange(Session.CustomerStreets.ToArray());
                    }

                    if (lvw_Streets.Items != null && lvw_Streets.Items.Count > 0)
                        lvw_Streets.Items[0].Selected = true;
                }
            }
            catch (Exception ex)
            {
                Logger.Trace("ERROR", "frmCustomer-txtStreet_Text(): " + ex.Message, ex, true);
            }
        }

        private void lvw_Cities_Click(object sender, EventArgs e)
        {
            try
            {
                ListView listView = (ListView)sender;
                this.txtCity.Tag = Session.CitiesAPIResponse.Where(x => x.CityName == listView.FocusedItem.Text).FirstOrDefault().CityCode;
                this.txtCity.Text = listView.FocusedItem.Text;
                this.txtRegion.Text = Session.CitiesAPIResponse.Where(x => x.CityName == listView.FocusedItem.Text).FirstOrDefault().RegionName;
                txtRegion_TextChanged(null, null);
                
                //this.txtPostal_Code.Text = Session.CitiesAPIResponse.Where(x => x.CityName == listView.FocusedItem.Text).FirstOrDefault();
            }
            catch (Exception ex)
            {
                Logger.Trace("ERROR", "frmCustomer-lvw_Cities_Click(): " + ex.Message, ex, true);
            }
        }

        private void lvw_Address_Click(object sender, EventArgs e)
        {
            try
            {
                ListView listView = (ListView)sender;
                if (listView != null && listView.Items.Count > 0 && listView.FocusedItem.SubItems != null && listView.FocusedItem.SubItems.Count > 0)
                {
                    //string addressLine = listView.FocusedItem.Text;
                    //string[] addressData = addressLine.Split(new char[] { '|' });
                    //if (addressData != null)
                    //{
                    this.txtStreet_Number.Text = listView.FocusedItem.SubItems[1].Text;
                    this.txtStreet.Text = listView.FocusedItem.SubItems[2].Text;
                    this.txtAddress_Line_2.Text = listView.FocusedItem.SubItems[0].Text;
                    this.txtCompanyName.Text = listView.FocusedItem.SubItems[3].Text;

                    //}
                }
                //this.txtAddress_Line_2.Text = listView.FocusedItem.Text;
                lvw_Address.Items[listView.FocusedItem.Index].Selected = true;
            }
            catch (Exception ex)
            {
                Logger.Trace("ERROR", "frmCustomer-lvw_Address_Click(): " + ex.Message, ex, true);
            }
        }

        private void tdbmPhone_Number_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                Session.IsCallerIDClicked = false;
                e.Handled = !Char.IsNumber(e.KeyChar) && e.KeyChar != 8;
            }
            catch (Exception ex)
            {
                Logger.Trace("ERROR", ex.Message, ex, true);
            }
        }

        private void tdbtxtPhone_Ext_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                e.Handled = !Char.IsNumber(e.KeyChar) && e.KeyChar != 8;
            }
            catch (Exception ex)
            {
                Logger.Trace("ERROR", ex.Message, ex, true);
            }
        }

        private void txtpostal_code_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                e.Handled = !Char.IsNumber(e.KeyChar) && e.KeyChar != 8;
            }
            catch (Exception ex)
            {
                Logger.Trace("ERROR", ex.Message, ex, true);
            }
        }


        private void txtCity_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void txtRegion_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void lvw_CrossStreets_Click(object sender, EventArgs e)
        {
            try
            {
                ListView listView = (ListView)sender;
                StreetLookUp street = Session.StreetsAPIResponse.Where(x => x.StreetName.Trim() == listView.FocusedItem.Text.Trim()).FirstOrDefault();
                this.txtCross_Street.Text = street.StreetName;
                Session.cart.Customer.Cross_Street = street.StreetName;
                //Session.cart.Customer.Cross_Street_Code = street.StreetCode;

                lvw_CrossStreets.Items.Clear();
                ListViewItem item = new ListViewItem(new[] { street.StreetName, street.CityName });
                lvw_CrossStreets.Items.Add(item);

                lvw_CrossStreets.Items[0].Selected = true;
            }
            catch (Exception ex)
            {
                Logger.Trace("ERROR", "frmCustomer-lvw_crossstreets_click(): " + ex.Message, ex, true);
            }
        }

        private void txtCross_Street_Click(object sender, EventArgs e)
        {
            try
            {
                lvw_CrossStreets.Visible = true;
                //lvw_CrossStreets.Location = new Point(221, 135);
                lvw_CrossStreets.Size = new Size(350, 150);

                if(Session.CustomerStreets == null && Session.CustomerStreets.Count == 0 && string.IsNullOrEmpty(this.txtCross_Street.Text))
                {
                    PopulateStreets();
                    lvw_CrossStreets.Items.Clear();
                    lvw_CrossStreets.Items.AddRange(Session.CustomerStreets.ToArray());
                }
                else if (lvw_CrossStreets.Items.Count == 0)
                {
                    ListViewItem[] items = new ListViewItem[Session.CustomerStreets.Count];
                    items = Session.CustomerStreets.ToArray();
                    lvw_CrossStreets.Items.AddRange(items);
                }
            }
            catch (Exception ex)
            {
                Logger.Trace("ERROR", "frmCustomer-txtcross_street_click(): " + ex.Message, ex, true);
            }
        }

        private void txtCross_Street_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Session.cart.Customer.Cross_Street = this.txtCross_Street.Text;
                Session.cart.Customer.Cross_Street_Code = 0;

                if (Session.StreetsAPIResponse == null)
                    PopulateStreets();
 
                if (Session.StreetsAPIResponse != null)
                {
                    if (!string.IsNullOrEmpty(this.txtCross_Street.Text.Trim()))
                    {
                        StreetLookUp street = Session.StreetsAPIResponse.Where(x => x.StreetName.ToLower().Trim() == this.txtCross_Street.Text.ToLower().Trim()).FirstOrDefault();

                        if (street != null && !string.IsNullOrEmpty(street.StreetName.ToLower().Trim()) && street.StreetName.ToLower().Trim() != "#")
                        {
                            lvw_CrossStreets.Items.Clear();
                            ListViewItem item = new ListViewItem(new[] { street.StreetName.Trim(), street.CityName.Trim() });
                            lvw_CrossStreets.Items.Add(item);

                            Session.cart.Customer.Cross_Street_Code = street.StreetCode;
                        }
                        else
                        {
                            List<StreetLookUp> streets = Session.StreetsAPIResponse.Where(x => x.StreetName.ToLower().Contains(this.txtCross_Street.Text.ToLower().Trim())).ToList();
                            lvw_CrossStreets.Items.Clear();

                            if (streets != null)
                            {
                                foreach (StreetLookUp streetLookUp in streets)
                                {
                                    ListViewItem item = new ListViewItem(new[] { streetLookUp.StreetName, streetLookUp.CityName });
                                    lvw_CrossStreets.Items.Add(item);
                                }
                            }
                        }
                    }
                    else if (lvw_CrossStreets.Items.Count == 0)
                    {
                        lvw_CrossStreets.Items.AddRange(Session.CustomerStreets.ToArray());
                    }

                    lvw_CrossStreets.Items[0].Selected = true;
                }
            }
            catch (Exception ex)
            {
                Logger.Trace("ERROR", ex.Message, ex, true);
            }
        }

        private void txtCross_Street_Leave(object sender, EventArgs e)
        {
            lvw_CrossStreets.Visible = false;
        }

        private void btn_ClearInstore_Click(object sender, EventArgs e)
        {
            try
            {
                TabControl tabControl = (TabControl)(this.tabControl_Notes);
                if (tabControl.SelectedTab == this.tabPage_InStore)
                {
                    txtNotes_Instore.Text = string.Empty;
                }
                else if (tabControl.SelectedTab == this.tabPage_Delivery)
                {
                    txtNotes_Delivery.Text = string.Empty;
                }
                else if (tabControl.SelectedTab == this.tabPage_Manager)
                {
                    txtNotes_manager.Text = string.Empty;
                }
            }
            catch (Exception ex)
            {
                Logger.Trace("ERROR", ex.Message, ex, true);
            }


        }

        private void tdbtxtPhone_Ext_TextChanged(object sender, EventArgs e)
        {
            try
            {
                //if (!Session.FromOrder)
                //{
                Session.cart.Customer.Phone_Ext = this.tdbtxtPhone_Ext.Text;
                //}
            }
            catch (Exception ex)
            {
                Logger.Trace("ERROR", ex.Message, ex, true);
            }
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {
            try
            {
                //if (!Session.FromOrder)
                //{
                Session.cart.Customer.Name = this.txtName.Text;

                //}
            }
            catch (Exception ex)
            {
                Logger.Trace("ERROR", ex.Message, ex, true);
            }
        }

        private void TDBdob_ValueChanged(object sender, EventArgs e)
        {
            try
            {
              if(Session.cart.Customer != null)
                Session.cart.Customer.date_of_birth = this.TDBdob.Value.Date;

              
            }
            catch (Exception ex)
            {
                Logger.Trace("ERROR", ex.Message, ex, true);
            }
        }

        private void TDBanniversary_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                //if (!Session.FromOrder)
                Session.cart.Customer.anniversary_date = this.TDBanniversary.Value.Date;
            }
            catch (Exception ex)
            {
                Logger.Trace("ERROR", ex.Message, ex, true);
            }
        }

        private void txtCompanyName_TextAlignChanged(object sender, EventArgs e)
        {

        }

        private void txtStreet_Number_TextChanged(object sender, EventArgs e)
        {
            try
            {
                //if (!Session.FromOrder)
                Session.cart.Customer.Street_Number = this.txtStreet_Number.Text;
            }
            catch (Exception ex)
            {
                Logger.Trace("ERROR", ex.Message, ex, true);
            }
        }

        private void txtAddress_Line_2_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Session.cart.Customer.Address_Line_2 = this.txtAddress_Line_2.Text;

                if (CustomerResult != null && CustomerResult.CustomerDetail != null)
                {
                    if (CustomerResult.CustomerDetail.PhoneNumber.Trim() == tdbmPhone_Number.Text.Trim())
                    {
                        if (CustomerResult.customerAddresses != null)
                        {
                            List<CustomerAddressResult> addressList = CustomerResult.customerAddresses.Where(x => x.AddressLine2.ToLower().Contains(this.txtAddress_Line_2.Text.ToLower().Trim())).ToList();
                            lvw_Address.Items.Clear();

                            if (addressList != null)
                            {
                                foreach (CustomerAddressResult address in addressList)
                                {
                                    ListViewItem item = new ListViewItem(new[] { address.AddressLine2.Trim(), address.StreetNumber.Trim(), address.Street_Name.Trim(),  address.CompanyName.Trim() });
                                    lvw_Address.Items.Add(item);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Trace("ERROR", "frmCustomer-txtAddress_Line_2_TextChanged(): " + ex.Message, ex, true);
            }
        }

        private void txtCity_TextChanged(object sender, EventArgs e)
        {
            try
            {
                //if (!Session.FromOrder)
                Session.cart.Customer.City = this.txtCity.Text;
                //Session.cart.Customer.Customer_City_Code = Convert.ToInt32(Convert.ToString(this.txtCity.Tag));

                if (Session.CitiesAPIResponse != null && !string.IsNullOrEmpty(txtCity.Text))
                {
                    Session.cart.Customer.Customer_City_Code = Session.CitiesAPIResponse.Where(x => x.CityName == this.txtCity.Text).FirstOrDefault().CityCode; //Convert.ToInt32(Convert.ToString(this.txtCity.Tag));
                }
                if (Session.CitiesAPIResponse == null)
                {
                    GetAllCitiesResponse citiesResponse = APILayer.GetAllCities(APILayer.CallType.GET);

                    if (citiesResponse != null && citiesResponse.Result != null && citiesResponse.Result.Cities != null)
                    {
                        Session.CitiesAPIResponse = citiesResponse.Result.Cities;
                    }
                }


                if (Session.StreetsAPIResponse == null)
                    PopulateStreets();

                if (Session.StreetsAPIResponse != null && Session.cart != null && Session.cart.Customer != null && Session.cart.Customer.Street_Code > 0)
                {
                    StreetLookUp street = Session.StreetsAPIResponse.Where(x => x.StreetCode == Session.cart.Customer.Street_Code ).FirstOrDefault();
                    if(street.CityCode != Session.cart.Customer.Customer_City_Code)
                        txtStreet.Text = "";
                }
                    
            }
            catch (Exception ex)
            {
                Logger.Trace("ERROR", "frmCustomer-txtCity_TextChanged(): " + ex.Message, ex, true);
            }
        }

        private void txtSuite_TextChanged(object sender, EventArgs e)
        {
            try
            {
                //if (!Session.FromOrder)
                Session.cart.Customer.Suite = this.txtSuite.Text;
            }
            catch (Exception ex)
            {
                Logger.Trace("ERROR", ex.Message, ex, true);
            }
        }

        private void txtRegion_TextChanged(object sender, EventArgs e)
        {
            try
            {
                // if (!Session.FromOrder)
                Session.cart.Customer.Region = this.txtRegion.Text;
            }
            catch (Exception ex)
            {
                Logger.Trace("ERROR", ex.Message, ex, true);
            }
        }

        private void txtAddress_Line_3_TextChanged(object sender, EventArgs e)
        {
            try
            {
                //if (!Session.FromOrder)
                Session.cart.Customer.Address_Line_3 = this.txtAddress_Line_3.Text;
            }
            catch (Exception ex)
            {
                Logger.Trace("ERROR", ex.Message, ex, true);
            }
        }

        private void txtAddress_Line_4_TextChanged(object sender, EventArgs e)
        {
            try
            {
                //if (!Session.FromOrder)
                Session.cart.Customer.Address_Line_4 = this.txtAddress_Line_4.Text;
            }
            catch (Exception ex)
            {
                Logger.Trace("ERROR", ex.Message, ex, true);
            }
        }

        private void txtNotes_Instore_TextChanged(object sender, EventArgs e)
        {
            try
            {
                //if (!Session.FromOrder)
                Session.cart.Customer.Comments = this.txtNotes_Instore.Text;
            }
            catch (Exception ex)
            {
                Logger.Trace("ERROR", ex.Message, ex, true);
            }
        }

        private void txtNotes_Delivery_TextChanged(object sender, EventArgs e)
        {
            try
            {
                // if (!Session.FromOrder)
                Session.cart.Customer.DriverComments = this.txtNotes_Delivery.Text;
            }
            catch (Exception ex)
            {
                Logger.Trace("ERROR", ex.Message, ex, true);
            }
        }

        private void txtNotes_manager_TextChanged(object sender, EventArgs e)
        {
            try
            {
                //if (!Session.FromOrder)
                Session.cart.Customer.Manager_Notes = this.txtNotes_manager.Text;
            }
            catch (Exception ex)
            {
                Logger.Trace("ERROR", ex.Message, ex, true);
            }
        }

        private void txtPostal_Code_TextChanged(object sender, EventArgs e)
        {
            try
            {
                //if (!Session.FromOrder)
                Session.cart.Customer.Postal_Code = this.txtPostal_Code.Text;
            }
            catch (Exception ex)
            {
                Logger.Trace("ERROR", ex.Message, ex, true);
            }
        }

        private void txtgstin_number_TextChanged(object sender, EventArgs e)
        {
            try
            {
                //if (!Session.FromOrder)
                Session.cart.Customer.gstin_number = this.txtgstin_number.Text;
            }
            catch (Exception ex)
            {
                Logger.Trace("ERROR", ex.Message, ex, true);
            }
        }

        private void LaunchCommandLineApp(string param)
        {
            // For the example
            const string ex1 = "C:\\";
            const string ex2 = "C:\\Dir";

            // Use ProcessStartInfo class
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.CreateNoWindow = false;
            startInfo.UseShellExecute = true;
            startInfo.FileName = @"E:\GitHub\JublFood.POS.App\JublFood.POS.App\bin\Debug\SingleCustomer.exe";
            startInfo.WindowStyle = ProcessWindowStyle.Normal;
            //startInfo.Arguments = "-f j -o \"" + ex1 + "\" -z 1.0 -s y " + ex2;
            startInfo.Arguments = param;

            try
            {
                // Start the process with the info we specified.
                // Call WaitForExit and then the using statement will close.
                using (Process exeProcess = Process.Start(startInfo))
                {
                    exeProcess.WaitForExit();
                }
            }
            catch (Exception ex)
            {
                Logger.Trace("ERROR", "frmCustomer-LaunchCommandLineApp(): " + ex.Message, ex, true);
            }
        }

        private void ucCustomerOrderBottomMenu_Click(object sender, EventArgs e)
        {
            try
            {
                Button btn = (Button)sender;
                if (btn.Text.Trim().ToUpper() == "RESIDENCE")
                {
                    //if (!Session.FromOrder)
                    //{
                    Session.cart.Customer.Address_Type = "R";
                    //}

                    this.ltxtSuite.Text = "Suite";
                    this.ltxtName.ForeColor = Color.Yellow;
                    //this.ltxtName.ForeColor = Color.White;
                    this.ltxtSuite.ForeColor = Color.White;
                    this.ltxtCompanyName.ForeColor = Color.White;
                }
                else if (btn.Text.Trim().ToUpper() == "KIOSK")
                {
                    //if (!Session.FromOrder)
                    //{
                    Session.cart.Customer.Address_Type = "K";
                    //}
                    this.ltxtSuite.Text = "Room";
                    this.ltxtName.ForeColor = Color.Yellow;
                    this.ltxtSuite.ForeColor = Color.Yellow;
                    this.ltxtCompanyName.ForeColor = Color.White;
                }
                else if (btn.Text.Trim().ToUpper() == "HOTEL")
                {
                    //if (!Session.FromOrder)
                    //{
                    Session.cart.Customer.Address_Type = "H";
                    //}
                    this.ltxtSuite.Text = "Room";
                    this.ltxtName.ForeColor = Color.Yellow;
                    this.ltxtSuite.ForeColor = Color.Yellow;
                    this.ltxtCompanyName.ForeColor = Color.White;
                }
                else if (btn.Text.Trim().ToUpper() == "BUSINESS")
                {
                    //if (!Session.FromOrder)
                    //{
                    Session.cart.Customer.Address_Type = "B";
                    //}
                    this.ltxtSuite.Text = "Suite";
                    this.ltxtName.ForeColor = Color.Yellow;
                    this.ltxtCompanyName.ForeColor = Color.Yellow;
                    this.ltxtSuite.ForeColor = Color.White;
                }
                else if (btn.Text.Trim().ToUpper() == "COLLEGE")
                {
                    //if (!Session.FromOrder)
                    //{
                    Session.cart.Customer.Address_Type = "C";
                    //}
                    this.ltxtSuite.Text = "Room";
                    this.ltxtCompanyName.ForeColor = Color.White;
                    this.ltxtName.ForeColor = Color.Yellow;
                    this.ltxtSuite.ForeColor = Color.Yellow;
                }
            }
            catch (Exception ex)
            {
                Logger.Trace("ERROR", ex.Message, ex, true);
            }
        }

        private void ValidateAddressType(object sender, EventArgs e)
        {
            if (Session.cart.Customer.Address_Type == "R")
            {
                if (string.IsNullOrEmpty(this.ltxtName.Text))
                {
                    CustomMessageBox.Show(MessageConstant.EnterCustomerName, CustomMessageBox.Buttons.OK, CustomMessageBox.Icon.Warning);
                }
            }
            else if (Session.cart.Customer.Address_Type == "K")
            {
                if (string.IsNullOrEmpty(this.ltxtName.Text))
                {
                    CustomMessageBox.Show(MessageConstant.EnterCustomerName, CustomMessageBox.Buttons.OK, CustomMessageBox.Icon.Warning);
                }
                if (string.IsNullOrEmpty(this.ltxtSuite.Text))
                {
                    CustomMessageBox.Show(MessageConstant.EnterCustomerSuite, CustomMessageBox.Buttons.OK, CustomMessageBox.Icon.Warning);
                }
            }
            else if (Session.cart.Customer.Address_Type == "H")
            {
                if (string.IsNullOrEmpty(this.ltxtName.Text))
                {
                    CustomMessageBox.Show(MessageConstant.EnterCustomerName, CustomMessageBox.Buttons.OK, CustomMessageBox.Icon.Warning);
                }
                if (string.IsNullOrEmpty(this.ltxtSuite.Text))
                {
                    CustomMessageBox.Show(MessageConstant.EnterCustomerSuite, CustomMessageBox.Buttons.OK, CustomMessageBox.Icon.Warning);
                }
            }
            else if (Session.cart.Customer.Address_Type == "B")
            {
                if (string.IsNullOrEmpty(this.ltxtName.Text))
                {
                    CustomMessageBox.Show(MessageConstant.EnterCustomerName, CustomMessageBox.Buttons.OK, CustomMessageBox.Icon.Warning);
                }
                if (string.IsNullOrEmpty(this.ltxtCompanyName.Text))
                {
                    CustomMessageBox.Show(MessageConstant.EnterCustomerCompany, CustomMessageBox.Buttons.OK, CustomMessageBox.Icon.Warning);
                }
            }
            else if (Session.cart.Customer.Address_Type == "C")
            {
                if (string.IsNullOrEmpty(this.ltxtName.Text))
                {
                    CustomMessageBox.Show(MessageConstant.EnterCustomerName, CustomMessageBox.Buttons.OK, CustomMessageBox.Icon.Warning);
                }
                if (string.IsNullOrEmpty(this.ltxtSuite.Text))
                {
                    CustomMessageBox.Show(MessageConstant.EnterCustomerSuite, CustomMessageBox.Buttons.OK, CustomMessageBox.Icon.Warning);
                }
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

                foreach (Control ctl in tlpCustomerPanel.Controls)
                {
                    if (ctl is Label)
                    {
                        ctl.BackColor = color;
                    }
                }
                pnl_button.BackColor = color;
                ucCustomer_OrderMenu.BackColor = color;
                ucCustomerOrderBottomMenu.BackColor = color;

                ucFunctionList.Visible = false;
                ucCustomer_OrderMenu.DisableModifyInTrainingMode();
            }
            catch (Exception ex)
            {
                Logger.Trace("ERROR", ex.Message, ex, true);
            }
        }

        private void frmCustomer_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                //e.Cancel = true;
                if (Session.ExitApplication)
                {
                    Session.ExitApplication = false;
                    e.Cancel = false;
                    return;
                }
                else if (ALT_F4)
                {
                    e.Cancel = true;
                    return;
                }
            }
            catch (Exception ex)
            {
                Logger.Trace("ERROR", ex.Message, ex, true);
            }
        }

        private void frmCustomer_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                ALT_F4 = (e.KeyCode.Equals(Keys.F4) && e.Alt == true);
                if (e.Modifiers == Keys.Control && e.KeyCode == Keys.H)
                {
                    // Open frmCartOnHold
                    if ((SystemSettings.GetSettingValue("CartOnHold", Session._LocationCode) == "1"))
                    {
                        frmCartOnHold frmCartOnHold = new frmCartOnHold();
                        frmCartOnHold.ShowDialog();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Trace("ERROR", ex.Message, ex, true);
            }
        }

        private void btn_KeyboardClear_Click(object sender, EventArgs e)
        {
            try
            {
                TabControl tabControl = (TabControl)(this.tabControl_Notes);
                if (tabControl.SelectedTab == this.tabPage_InStore)
                {
                    txtNotes_Instore.Text = string.Empty;
                }
                else if (tabControl.SelectedTab == this.tabPage_Delivery)
                {
                    txtNotes_Delivery.Text = string.Empty;
                }
                else if (tabControl.SelectedTab == this.tabPage_Manager)
                {
                    txtNotes_manager.Text = string.Empty;
                }
            }
            catch (Exception ex)
            {
                Logger.Trace("ERROR", ex.Message, ex, true);
            }
        }

        private void btn_ClearDelivery_Click(object sender, EventArgs e)
        {
            try
            {
                TabControl tabControl = (TabControl)(this.tabControl_Notes);
                if (tabControl.SelectedTab == this.tabPage_InStore)
                {
                    txtNotes_Instore.Text = string.Empty;
                }
                else if (tabControl.SelectedTab == this.tabPage_Delivery)
                {
                    txtNotes_Delivery.Text = string.Empty;
                }
                else if (tabControl.SelectedTab == this.tabPage_Manager)
                {
                    txtNotes_manager.Text = string.Empty;
                }
            }
            catch (Exception ex)
            {
                Logger.Trace("ERROR", ex.Message, ex, true);
            }
        }

        private void btn_DeleteLogTodaysCall_Click(object sender, EventArgs e)
        {
            try
            {
                string password = string.Empty;

                frmPassword objfrmPassword = new frmPassword();
                objfrmPassword.ShowDialog();
                password = objfrmPassword.TypedPassword;
                objfrmPassword.Dispose();

                if (password == string.Empty)
                {
                    //CustomMessageBox.Show(MessageConstant.EnterPassword, CustomMessageBox.Buttons.OK, CustomMessageBox.Icon.Warning);
                    //txtUserID.Text = string.Empty;
                    //return false;
                    //this.Hide();
                }
                else
                {
                    bool status = EmployeeFunctions.MatchEmployeePassword();
                    var openFormCustomer = Application.OpenForms.Cast<Form>().Where(x => x.Text.ToUpper() == "CUSTOMER").FirstOrDefault();
                    if (openFormCustomer != null)
                    {
                        openFormCustomer.Show();
                    }
                    if (status)
                    {
                        GetAllCustomersRequest requestModel = new GetAllCustomersRequest();
                        requestModel.LocationCode = Session._LocationCode;
                        SaveCustomerResponse response = APILayer.DeleteCallerIDLog(APILayer.CallType.POST, requestModel);
                        if (response != null && response.Result != null && response.Result.ResponseStatus.ToUpper() == "SUCCESS")
                        {
                            var openFormCustomer1 = Application.OpenForms.Cast<Form>().Where(x => x.Text.ToUpper() == "CUSTOMER").FirstOrDefault();
                            if (openFormCustomer1 != null)
                            {
                                openFormCustomer1.Show();
                            }

                            if (gvTodaysCalls.Rows.Count > 0 && gvTodaysCalls.Rows[0].Cells[0].Value != null)
                            {
                                gvTodaysCalls.Rows.Clear();
                                gvTodaysCalls.Rows.Add("", "", "", "");
                            }

                            if (gvPastCalls.Rows.Count > 0 && gvPastCalls.Rows[0].Cells[0].Value != null)
                            {
                                gvPastCalls.Rows.Clear();
                                gvPastCalls.Rows.Add("", "", "", "");
                            }
                        }
                    }
                    else
                    {
                        btn_DeleteLogTodaysCall_Click(sender, e);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Trace("ERROR", "frmCustomer-btnDeleteLogTodaysCall_Click(): " + ex.Message, ex, true);
            }

        }

        private void btn_deletLogPastCall_Click(object sender, EventArgs e)
        {
            try
            {
                bool status = EmployeeFunctions.MatchEmployeePassword();
                var openFormCustomer = Application.OpenForms.Cast<Form>().Where(x => x.Text.ToUpper() == "CUSTOMER").FirstOrDefault();
                if (openFormCustomer != null)
                {
                    openFormCustomer.Show();
                }
                if (status)
                {
                    GetAllCustomersRequest requestModel = new GetAllCustomersRequest();
                    requestModel.LocationCode = Session._LocationCode;
                    SaveCustomerResponse response = APILayer.DeleteCallerIDLog(APILayer.CallType.POST, requestModel);
                    if (response != null && response.Result != null && response.Result.ResponseStatus.ToUpper() == "SUCCESS")
                    {
                        var openFormCustomer1 = Application.OpenForms.Cast<Form>().Where(x => x.Text.ToUpper() == "CUSTOMER").FirstOrDefault();
                        if (openFormCustomer1 != null)
                        {
                            openFormCustomer1.Show();
                        }

                        if (gvTodaysCalls.Rows.Count > 0 && gvTodaysCalls.Rows[0].Cells[0].Value != null)
                        {
                            gvTodaysCalls.Rows.Clear();
                            gvTodaysCalls.Rows.Add("", "", "", "");
                        }

                        if (gvPastCalls.Rows.Count > 0 && gvPastCalls.Rows[0].Cells[0].Value != null)
                        {
                            gvPastCalls.Rows.Clear();
                            gvPastCalls.Rows.Add("", "", "", "");
                        }
                    }
                }
                else
                {
                    btn_deletLogPastCall_Click(sender, e);
                }

            }
            catch (Exception ex)
            {
                Logger.Trace("ERROR", "frmCustomer-btn_deleteLogPastCall_click(): " + ex.Message, ex, true);
            }
        }

        private void panelCallerIDButton_Click(object sender, EventArgs e)
        {
            try
            {
                Panel currentPanel = (Panel)sender;
                if (currentPanel.BackColor == Color.LightGreen)
                {
                    currentPanel.BackColor = Color.Salmon;
                    Session.IsCallerIDClicked = true;

                    TabControl tabControl = (TabControl)(this.tabControl_CallerNotesProfile);
                    tabControl.SelectedTab = this.tabPage_Notes;

                    tdbmPhone_Number.Text = currentPanel.Controls[1].Text;
                    Session.cart.Customer.Phone_Number = tdbmPhone_Number.Text;
                    uC_Customer_order_Header1.btnStart_Click();
                    txtNotes_Instore.Text = string.Empty;
                    txtNotes_Delivery.Text = string.Empty;
                    txtNotes_manager.Text = string.Empty;

                    //foreach (Control control in this.Controls)
                    //{
                    //    if (control.Name.StartsWith("RuntimePanel"))
                    //    {

                    //    }
                    //    //else if (control is Checkbox)
                    //    //{
                    //    //}
                    //    //else if (control.Controls.Count > 0)
                    //    //{

                    //    //}
                    //}
                }
                else if (currentPanel.BackColor == Color.Salmon && currentPanel.Controls[4].Text.ToUpper() == "NEW")
                {
                    //Session.IsTimerStarted = false;
                    //uC_Customer_order_Header1.btnStop_Click();
                    tdbtxtPhone_Ext.Text = string.Empty;
                    txtName.Text = string.Empty;
                    TDBdob.Text = string.Empty;
                    TDBanniversary.Text = string.Empty;
                    txtCompanyName.Text = string.Empty;
                    txtStreet_Number.Text = string.Empty;
                    txtAddress_Line_2.Text = string.Empty;
                    txtCity.Text = string.Empty;
                    txtPostal_Code.Text = string.Empty;
                    //txtGSTIN.Text = string.Empty;
                    txtSuite.Text = string.Empty;
                    txtCross_Street.Text = string.Empty;
                    txtStreet.Text = string.Empty;
                    txtTentNumber.Text = string.Empty;
                    txtRegion.Text = string.Empty;
                    txtgstin_number.Text = string.Empty;

                    //Notes
                    txtNotes_Instore.Text = string.Empty;
                    txtNotes_Delivery.Text = string.Empty;
                    txtNotes_manager.Text = string.Empty;

                    currentPanel.BackColor = Color.LightGreen;
                    tdbmPhone_Number.Text = SystemSettings.appControl.DefaultPhonePrefix+ string.Empty;
                    Session.cart.Customer.Phone_Number = string.Empty;
                    lblPriorityCustomer.Visible = false;
                    lblPriorityCustomer.Text = string.Empty;
                    tmrVIP.Stop();
                    tmrVIP.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                Logger.Trace("ERROR", "frmCustomer-panelCallerIDButton_Click(): " + ex.Message, ex, true);
            }
        }

        private void tabControl_CallerId_Selected(object sender, TabControlEventArgs e)
        {
            try
            {
                TabControl tabControl = (TabControl)sender;

                GetAllCustomersRequest requestModel = new GetAllCustomersRequest();
                requestModel.LocationCode = Session._LocationCode;
                GetCallerIDLinesResponse getCallerIDLinesResponse = APILayer.GetCallerIDLines(APILayer.CallType.POST, requestModel);

                if (getCallerIDLinesResponse != null && getCallerIDLinesResponse.Result != null && (getCallerIDLinesResponse.Result.CallerIDLines != null && getCallerIDLinesResponse.Result.CallerIDLines.Count > 0))
                {
                    CallerIDLines = getCallerIDLinesResponse.Result.CallerIDLines;
                }

                GetCallerIDLogRequest getCallerIDLogTodayrequestModel = new GetCallerIDLogRequest();
                getCallerIDLogTodayrequestModel.LocationCode = Session._LocationCode;
                getCallerIDLogTodayrequestModel.blnToday = true;
                GetCallerIDLogResponse getCallerIDLogTodayResponse = APILayer.GetCallerIDLog(APILayer.CallType.POST, getCallerIDLogTodayrequestModel);
                if (getCallerIDLogTodayResponse != null && getCallerIDLogTodayResponse.Result != null && getCallerIDLogTodayResponse.Result.CallerIDLogs == null)
                {
                    CallerIDLogToday = new List<GetCallerIDLog>();
                }
                if (getCallerIDLogTodayResponse != null && getCallerIDLogTodayResponse.Result != null && (getCallerIDLogTodayResponse.Result.CallerIDLogs != null && getCallerIDLogTodayResponse.Result.CallerIDLogs.Count > 0))
                {
                    CallerIDLogToday = getCallerIDLogTodayResponse.Result.CallerIDLogs;
                }

                GetCallerIDLogRequest getCallerIDLogrequestModel = new GetCallerIDLogRequest();
                getCallerIDLogrequestModel.LocationCode = Session._LocationCode;
                getCallerIDLogrequestModel.blnToday = false;
                GetCallerIDLogResponse getCallerIDLogResponse = APILayer.GetCallerIDLog(APILayer.CallType.POST, getCallerIDLogrequestModel);

                if (getCallerIDLogResponse != null && getCallerIDLogResponse.Result != null && getCallerIDLogResponse.Result.CallerIDLogs == null)
                {
                    CallerIDLogPrevious = new List<GetCallerIDLog>();
                }

                if (getCallerIDLogResponse != null && getCallerIDLogResponse.Result != null && (getCallerIDLogResponse.Result.CallerIDLogs != null && getCallerIDLogResponse.Result.CallerIDLogs.Count > 0))
                {
                    CallerIDLogPrevious = getCallerIDLogResponse.Result.CallerIDLogs;
                }

                if (tabControl.SelectedTab == tabPage_ActiveCalls)
                {
                }
                else if (tabControl.SelectedTab == tabPage_Todayscalls)
                {
                    if (CallerIDLogToday.Count > 0)
                    {
                        gvTodaysCalls.Rows.Clear();
                    }
                    //GetCallerIDLogRequest getCallerIDLogTodayrequestModel = new GetCallerIDLogRequest();
                    //getCallerIDLogTodayrequestModel.LocationCode = Session._LocationCode;
                    //getCallerIDLogTodayrequestModel.blnToday = true;
                    //GetCallerIDLogResponse getCallerIDLogTodayResponse = APILayer.GetCallerIDLog(APILayer.CallType.POST, getCallerIDLogTodayrequestModel);
                    //if (getCallerIDLogTodayResponse != null && getCallerIDLogTodayResponse.Result != null && (getCallerIDLogTodayResponse.Result.CallerIDLogs != null && getCallerIDLogTodayResponse.Result.CallerIDLogs.Count > 0))
                    //{
                    //    CallerIDLogToday = new List<GetCallerIDLog>();
                    //}
                    if (getCallerIDLogTodayResponse != null && getCallerIDLogTodayResponse.Result != null && (getCallerIDLogTodayResponse.Result.CallerIDLogs != null && getCallerIDLogTodayResponse.Result.CallerIDLogs.Count > 0))
                    {
                        CallerIDLogToday = getCallerIDLogTodayResponse.Result.CallerIDLogs;
                        foreach (GetCallerIDLog log in getCallerIDLogTodayResponse.Result.CallerIDLogs)
                        {
                            gvTodaysCalls.Rows.Add(log.CallDate, log.PhoneNumber, log.Name, log.OrderNumber);
                        }
                        gvTodaysCalls.AllowUserToAddRows = false;
                        if (gvTodaysCalls.Rows.Count > 1)
                        {
                            if (gvTodaysCalls.Rows.Count > 2)
                            {
                                for (int i = 0; i < gvTodaysCalls.Rows.Count; i++)
                                {
                                    if (IsOdd(i))
                                    {
                                        gvTodaysCalls.Rows[i].DefaultCellStyle.BackColor = Color.Gray;
                                    }
                                }
                            }
                            else
                            {
                                gvTodaysCalls.Rows[gvTodaysCalls.Rows.Count - 1].DefaultCellStyle.BackColor = Color.Gray;
                            }
                        }
                    }
                    else if (gvTodaysCalls.Rows[0].Cells[0].Value != null)
                    {
                        gvTodaysCalls.Rows.Clear();
                        gvTodaysCalls.Rows.Add("", "", "", "");
                    }
                }
                else if (tabControl.SelectedTab == tabPage_PostCalls)
                {
                    if (CallerIDLogPrevious.Count > 0)
                    {
                        gvPastCalls.Rows.Clear();
                    }
                    //GetCallerIDLogRequest getCallerIDLogrequestModel = new GetCallerIDLogRequest();
                    //getCallerIDLogrequestModel.LocationCode = Session._LocationCode;
                    //getCallerIDLogrequestModel.blnToday = false;
                    //GetCallerIDLogResponse getCallerIDLogResponse = APILayer.GetCallerIDLog(APILayer.CallType.POST, getCallerIDLogrequestModel);

                    //if (getCallerIDLogResponse != null && getCallerIDLogResponse.Result != null && (getCallerIDLogResponse.Result.CallerIDLogs != null && getCallerIDLogResponse.Result.CallerIDLogs.Count == 0))
                    //{
                    //    CallerIDLogPrevious = new List<GetCallerIDLog>();
                    //}

                    if (getCallerIDLogResponse != null && getCallerIDLogResponse.Result != null && (getCallerIDLogResponse.Result.CallerIDLogs != null && getCallerIDLogResponse.Result.CallerIDLogs.Count > 0))
                    {
                        CallerIDLogPrevious = getCallerIDLogResponse.Result.CallerIDLogs;
                        foreach (GetCallerIDLog log in getCallerIDLogResponse.Result.CallerIDLogs)
                        {
                            gvPastCalls.Rows.Add(log.CallDate, log.PhoneNumber, log.Name, log.OrderNumber);
                        }
                        gvPastCalls.AllowUserToAddRows = false;
                        if (gvPastCalls.Rows.Count > 1)
                        {
                            if (gvPastCalls.Rows.Count > 2)
                            {
                                for (int i = 0; i < gvPastCalls.Rows.Count; i++)
                                {
                                    if (IsOdd(i))
                                    {
                                        gvPastCalls.Rows[i].DefaultCellStyle.BackColor = Color.Gray;
                                    }
                                }
                            }
                            else
                            {
                                gvPastCalls.Rows[gvPastCalls.Rows.Count - 1].DefaultCellStyle.BackColor = Color.Gray;
                            }
                        }
                    }
                    else if (gvPastCalls.Rows[0].Cells[0].Value != null)
                    {
                        gvPastCalls.Rows.Clear();
                        gvPastCalls.Rows.Add("", "", "", "");
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Trace("ERROR", "frmCustomer-tabControl_CallerId_selected(): " + ex.Message, ex, true);
            }
        }

        private static bool IsOdd(int value)
        {
            return value % 2 != 0;
        }

        private void frmCustomer_Activated(object sender, EventArgs e)
        {
            
            try
            {
                txtPostal_Code.MaxLength = 6;
                EnableDisableCustomerFormControls(Session.CurrentEmployee == null || Session.CurrentEmployee.LoginDetail == null ? false : true);
                if(Session.IsTimerStarted) uC_Customer_order_Header1.btnStart_Click();

                if(Session.cart !=null)
                {
                    if (Session.cart.cartHeader.Total > 0)
                    {
                        ucCustomerOrderBottomMenu.cmdComplete.Enabled = true;
                        ucCustomerOrderBottomMenu.cmdPay.Enabled = true;
                    }
                    else
                    {
                        if (Session.cart.cartHeader.Total == 0 && Session.cart.cartItems != null && Session.cart.cartItems.Count > 0)
                        {
                            ucCustomerOrderBottomMenu.cmdComplete.Enabled = true;
                        }
                        else
                        {
                            ucCustomerOrderBottomMenu.cmdComplete.Enabled = false;
                        }
                        ucCustomerOrderBottomMenu.cmdPay.Enabled = false;

                    }
                }
                else
                {
                    ucCustomerOrderBottomMenu.cmdComplete.Enabled = false;
                    ucCustomerOrderBottomMenu.cmdPay.Enabled = false;
                }
                if (Session.RefreshFromModifyForCustomer || Session.RefreshFromHistoryForCustomer || Session.RefreshFromRemakeForCustomer)
                {

                   
                    Session.IsCallerIDClicked = false;
                    this.ActiveControl = tdbmPhone_Number;
                    Session.FormClockOpened = true;
                    if (Session.pblnModifyingOrder)
                    {
                        FillAddressScreen();
                    }

                    UserFunctions.AutoSelectOrderType(ucCustomerOrderBottomMenu);
                    //ucCustomer_OrderMenu.HandleHistoryButton(false);

                    if (Session.RemakeOrder == true)
                    {

                        btn_College.Enabled = false;
                        btn_Business.Enabled = false;
                        btn_Kiosk.Enabled = false;
                        btn_Residence.Enabled = false;
                        cmdDeliveryInfoKeyBoard.Enabled = false;
                        cmdNewExt.Enabled = false;
                        pnl_button.Enabled = false;
                        tlpCustomerPanel.Enabled = false;
                        pnl_CallerDetail.Enabled = false;
                    }

                    CheckTrainningMode();

                    Session.RefreshFromModifyForCustomer = false;
                    Session.RefreshFromHistoryForCustomer = false;
                    Session.RefreshFromRemakeForCustomer = false;
                }
                else
                {
                    if (UserFunctions.GetCurrentSelectedOrderTypeOnForm(ucCustomerOrderBottomMenu) != Session.selectedOrderType)
                        UserFunctions.AutoSelectOrderType(ucCustomerOrderBottomMenu);
                }

                if (Session.CartInitiated)
                {
                    ucCustomer_OrderMenu.cmdTimeClock.Enabled = false;
                    ucCustomer_OrderMenu.cmdLogin.Enabled = false;
                    ucCustomer_OrderMenu.cmdChangeOrders.Enabled = false;
                    ucCustomer_OrderMenu.ConvertExittoCancel();
                }

                ucCustomer_OrderMenu.cmdSearch.Enabled = false;
            }
            catch (Exception ex)
            {
                Logger.Trace("ERROR", "frmCustomer-frmCustomer_Activated(): " + ex.Message, ex, true);
            }
        }

        private void EnableDisableCustomerFormControls(bool Enabled)
        {
            cmdCustomerProfile.Enabled = false;
            tabControl_CallerNotesProfile.Enabled = Enabled;
            tlpCustomerPanel.Enabled = Enabled;
            pnl_button.Enabled = Enabled;

            foreach (Control control in ucCustomer_OrderMenu.Controls)
            {
                control.Enabled = Enabled;
                if (Session.pblnModifyingOrder)
                {
                    if (control.Name == "cmdTimeClock")
                        control.Enabled = false;
                    if (control.Name == "cmdLogin")
                        control.Enabled = false;
                    if (control.Name == "cmdChangeOrders")
                        control.Enabled = false;
                    if (control.Name == "cmdExit")
                        control.Enabled = true;
                    if (control.Name == "cmdOrderCoupons")
                        control.Enabled = false;
                    if (control.Name == "cmdRemake")
                        control.Enabled = Session.handleRemakebutton;
                    if (control.Name == "cmdHistory")
                    {
                        control.Enabled = Session.handleHistorybutton;
                    }
                    if (control.Name == "cmdInformation")
                        control.Enabled = true;
                }
                else
                {
                    if (control.Name == "cmdExit")
                        control.Enabled = true;
                    if (control.Name == "cmdOrderCoupons")
                        control.Enabled = false;
                    if (control.Name == "cmdRemake")
                        control.Enabled = Session.handleRemakebutton;
                    if (control.Name == "cmdHistory")
                    {
                        control.Enabled = Session.handleHistorybutton;
                    }
                    if (control.Name == "cmdTimeClock")
                        control.Enabled = true;
                    if (control.Name == "cmdLogin")
                        control.Enabled = true;
                    if (control.Name == "cmdInformation")
                        control.Enabled = true;
                    if (control.Name == "cmdChangeOrders")
                        control.Enabled = Session.handleModify;
                }
            }
        }
        public void  FillAddressScreen()
        {
           
             
            if (Session.cart.Customer.Address_Type == "H" || Session.cart.Customer.Address_Type == "C")
            {
                Session.cart.Customer.HotelorCollege = true;
            }
            else
            {
                Session.cart.Customer.HotelorCollege = false;
            }
            
            tdbtxtPhone_Ext.Text = Session.cart.Customer.Phone_Ext;

            Session.cart.Customer.Phone_Ext= Session.cart.Customer.Phone_Ext;
            txtName.Text = Session.cart.Customer.Name;
            Session.cart.Customer.Name = Session.cart.Customer.Name;
            txtCompanyName.Text = Session.cart.Customer.Company_Name;
            Session.cart.Customer.Company_Name = Session.cart.Customer.Company_Name;
            txtStreet_Number.Text = Session.cart.Customer.Street_Number;
            Session.cart.Customer.Street_Number = Session.cart.Customer.Street_Number;
            txtCross_Street.Text = Session.cart.Customer.Cross_Street;
            Session.cart.Customer.Cross_Street = Session.cart.Customer.Cross_Street;
            txtAddress_Line_2.Text = Session.cart.Customer.Address_Line_2;
            Session.cart.Customer.Address_Line_2 = Session.cart.Customer.Address_Line_2;
            txtAddress_Line_4.Text = Session.cart.Customer.Address_Line_4;
            Session.cart.Customer.Address_Line_4 = Session.cart.Customer.Address_Line_4;
            txtSuite.Text = Session.cart.Customer.Suite;
            Session.cart.Customer.Suite = Session.cart.Customer.Suite;
            txtCity.Text = Session.cart.Customer.City;
            Session.cart.Customer.City = Session.cart.Customer.City;
            txtPostal_Code.Text = Session.cart.Customer.Postal_Code;
            Session.cart.Customer.Postal_Code = Session.cart.Customer.Postal_Code;
            tdbmPhone_Number.Text = Session.cart.Customer.Phone_Number;
            Session.cart.Customer.Phone_Number = Session.cart.Customer.Phone_Number;
            lvw_Cities.Text = Session.cart.Customer.City;
            Session.cart.Customer.City = Session.cart.Customer.City;
            txtStreet.Text = Session.blnmodify;
            Session.cart.Customer.Customer_Street_Name = Session.blnmodify;
            UpdateAddressType(true, null, Session.cart.Customer.Address_Type);


        }
        public void UpdateAddressType(bool blnAllowAccess, string strOrigAddressType, string strNewAddressType)
        {
            string strAddressType;

            if (blnAllowAccess)
            {
                strAddressType = strNewAddressType;
            }
            else
            {

                strAddressType = strOrigAddressType;
            }
            Session.cart.Customer.Address_Type = strAddressType;

            switch (Session.cart.Customer.Address_Type)
            {
                case "R":
                    btn_Residence.BackColor = Color.FromArgb(255, 128, 128);
                    Session.cart.Customer.HotelorCollege = false;
                    break;
                case "H":
                    btn_Kiosk.BackColor = Color.FromArgb(255, 128, 128);
                    Session.cart.Customer.HotelorCollege = true;
                    break;
                case "B":
                    btn_Business.BackColor = Color.FromArgb(255, 128, 128);
                    Session.cart.Customer.HotelorCollege = false;
                    break;
                case "C":
                    btn_College.BackColor = Color.FromArgb(255, 128, 128);
                    Session.cart.Customer.HotelorCollege = true;
                    break;

            }
            if (Session.cart.Customer.Customer_Code != 0)
            {
                // Call pobjPOSGeneral.Change_Address_Type(m_colOrder.Customer_Code, m_colCustomer.Address_Type)//TO DO

            }
            Set_Required_Fields(Session.cart.cartHeader.Order_Type_Code, Session.cart.Customer.Address_Type);
        }
        public void Set_Required_Fields(string strOrder_Type, string strAddress_Type)
        {
            if (strAddress_Type == "H" || strAddress_Type == "C")
            {
                ltxtName.ForeColor = Color.Yellow;
                ltxtSuite.ForeColor = Color.Yellow;
                ltxtCompanyName.ForeColor = Color.White;
                ltxtPhone_Ext.Enabled = true;
                cmdNewExt.Enabled = false;

            }
            else
            {
                if (strAddress_Type == "B")
                    ltxtCompanyName.ForeColor = Color.Yellow;
                else

                    ltxtCompanyName.ForeColor = Color.White;

                ltxtName.ForeColor = Color.White;
                ltxtSuite.ForeColor = Color.White;
                ltxtSuite.Visible = true;
                ltxtPhone_Ext.Enabled = true;
                cmdNewExt.Enabled = true;
                txtName.Tag = "";
                txtSuite.Tag = "";
            }
            switch (strOrder_Type)
            {
                case "D":
                    if (SystemSettings.settings.pblnCustNameReqDelivery == true)
                    {
                        ltxtName.ForeColor = Color.Yellow;
                    }
                    else
                    {
                        ltxtName.ForeColor = Color.White;
                    }
                    ltxtPhone_Number.ForeColor = Color.Yellow;
                    ltxtStreet_Number.ForeColor = Color.Yellow;
                    ltxtCity.ForeColor = Color.Yellow;
                    ltxtRegion.ForeColor = Color.Yellow;
                    ltxtPostal_Code.ForeColor = Color.Yellow;
                    break;
                case "I":
                    if (SystemSettings.settings.pblnCustNameReqDineIn == true)
                    {

                        ltxtName.ForeColor = Color.Yellow;
                    }
                    else
                    {
                        ltxtName.ForeColor = Color.White;
                    }
                    ltxtPhone_Number.ForeColor = Color.White;
                    ltxtStreet_Number.ForeColor = Color.White;
                    ltxtCity.ForeColor = Color.White;
                    ltxtRegion.ForeColor = Color.White;
                    ltxtPostal_Code.ForeColor = Color.White;
                    break;
                case "C":
                    if (SystemSettings.settings.pblnCustNameReqCarryOut == true)
                    {

                        ltxtName.ForeColor = Color.Yellow;
                    }
                    else
                    {
                        ltxtName.ForeColor = Color.White;
                    }
                    ltxtPhone_Number.ForeColor = Color.White;
                    ltxtStreet_Number.ForeColor = Color.White;
                    ltxtCity.ForeColor = Color.White;
                    ltxtRegion.ForeColor = Color.White;
                    ltxtPostal_Code.ForeColor = Color.White;
                    break;
                case "P":
                    if (SystemSettings.settings.pblnCustNameReqPickUp == true)
                    {

                        ltxtName.ForeColor = Color.Yellow;
                    }
                    else
                    {
                        ltxtName.ForeColor = Color.White;
                    }
                    ltxtPhone_Number.ForeColor = Color.Yellow;
                    ltxtStreet_Number.ForeColor = Color.White;
                    ltxtCity.ForeColor = Color.White;
                    ltxtRegion.ForeColor = Color.White;
                    ltxtPostal_Code.ForeColor = Color.White;
                    break;
            }
        }
        public void UpdatePhoneNumber(string strPhoneNumber, Customer customer, CartHeader cartHeader)
        {
            //if (Information.IsNumeric(strPhoneNumber))
            {
                tdbmPhone_Number.Text = customer.Phone_Number;


                if (SystemSettings.settings.pblnSaveDriverComment == true)
                {
                    if (customer.DriverComments == null)
                    {
                        customer.DriverComments = "";
                    }
                    if (customer.Manager_Notes == null)
                    {
                        customer.Manager_Notes = "";
                    }
                    if (customer.DriverComments.Length == 0 && customer.Manager_Notes.Length == 0 && customer.Accept_Cash == true && customer.Accept_Checks == true && customer.Accept_Credit_Cards == true)
                    {
                        //Call ToggleCommentsIcon(False)
                    }
                    else
                    {
                        //Call ToggleCommentsIcon(true)
                    }
                    UpdateComments(customer.Comments, customer.DriverComments, customer.Manager_Notes, customer.Accept_Cash, customer.Accept_Checks, customer.Accept_Credit_Cards);
                }
                else
                {

                    if ((cartHeader.Comments.Length) == 0 && (customer.Comments.Length) == 0 && (customer.Manager_Notes.Length) == 0 && customer.Accept_Cash == true && customer.Accept_Checks == true && customer.Accept_Credit_Cards == true)
                    {
                        //Call ToggleCommentsIcon(False) // Icon Set DO TO
                    }
                    else
                    {
                        //Call ToggleCommentsIcon(true)
                    }

                    UpdateComments(customer.Comments, customer.DriverComments, customer.Manager_Notes, customer.Accept_Cash, customer.Accept_Checks, customer.Accept_Credit_Cards);

                }
            }
        }
        public void UpdateComments(string strInstore, string strComments, string Manager_Notes, bool Accept_Cash, bool Accept_Checks, bool Accept_Credit_Cards)
        {

            tabControl_Notes.SelectedIndex = 0;
            txtNotes_Instore.Text = strInstore;
            if (string.IsNullOrEmpty(strInstore))
            {
                Session.m_blnNotesExist = false;
            }
            else
            {
                Session.m_blnNotesExist = true;
            }

            txtNotes_Delivery.Text = strComments;
            if (string.IsNullOrEmpty(txtNotes_Delivery.Text))
            {
                Session.m_blnDriverCommentsExist = false;
            }
            else
            {
                Session.m_blnDriverCommentsExist = true;
            }
            if (Accept_Cash == false && Accept_Checks == false && Accept_Credit_Cards == false)
            {
                tabControl_Notes.SelectedIndex = 3;
                tabPage_Manager.Text = null;
            }
            else
            {
                tabPage_Manager.Text = null;
            }
            if (!(Accept_Cash))
            {
               tabPage_Manager.Text = APILayer.GetCatalogText(LanguageConstant.cintMSGDoNotAcceptCash);
            }
            if (!(Accept_Checks))
            {
                if (tabPage_Manager.Text.Length == 0)
                {
                    tabPage_Manager.Text = APILayer.GetCatalogText(LanguageConstant.cintMSGDoNotAcceptChecks);

                }
                else
                {
                    tabPage_Manager.Text = APILayer.GetCatalogText(LanguageConstant.cintMSGDoNotAcceptChecks);

                }

            }
            if (!(Accept_Credit_Cards))
            {
                if (tabPage_Manager.Text.Length == 0)
                {
                    tabPage_Manager.Text = APILayer.GetCatalogText(LanguageConstant.cintMSGDoNotAcceptCreditCards);

                }
                else
                {
                    tabPage_Manager.Text = APILayer.GetCatalogText(LanguageConstant.cintMSGDoNotAcceptCreditCards);

                }

            }

            if (tabPage_Manager.Text.Length == 0)
            {

                tabPage_Manager.Text = Manager_Notes;
            }
            else
            {
                tabPage_Manager.Text = Manager_Notes;
            }
        }

        public void UpdateUseronHeader()
        {
            uC_Customer_order_Header1.LoadlabelText();
            ucCustomer_OrderMenu.SetAllbuttonText();
        }

        private void ucCustomer_OrderMenu_Load(object sender, EventArgs e)
        {

        }

        public void loadCustomer(string phone_number, string phone_extn)
        {   
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(Session.cart.Customer);
            Customer customerHold = Newtonsoft.Json.JsonConvert.DeserializeObject<Customer>(json);

            Session.cart.Customer.Phone_Number = "";
            tdbtxtPhone_Ext.Text = phone_extn;
            tdbmPhone_Number.Text = phone_number;
            Session.cart.Customer.Phone_Number = phone_number;

            FillCustomerFromSessionCustomer(customerHold);

            //FindCustomer(Location_code, phone_number, phone_extn);

        }

        private void PopulateStreets()
        {
            if (Session.CustomerStreets == null || Session.CustomerStreets.Count == 0)
            {
                Session.CustomerStreets = new List<ListViewItem>();

                StreetLookUpRequest streetLookUpRequest = new StreetLookUpRequest();
                streetLookUpRequest.LocationCode = Session._LocationCode;
                streetLookUpRequest.StreetName = "%%";
                StreetLookUpResponse streetLookUpResponse = APILayer.StreetLookUp(APILayer.CallType.POST, streetLookUpRequest);

                if (streetLookUpResponse != null && streetLookUpResponse.Result != null && streetLookUpResponse.Result.Streets != null)
                {
                    Session.StreetsAPIResponse = streetLookUpResponse.Result.Streets;
                    foreach (StreetLookUp street in streetLookUpResponse.Result.Streets)
                    {
                        ListViewItem item = new ListViewItem(new[] { street.StreetName.Trim(), street.CityName.Trim() });
                        Session.CustomerStreets.Add(item);
                    }
                }
            }
        }

        private void tmrVIP_Tick(object sender, EventArgs e)
        {
            if (lblPriorityCustomer.BackColor == Color.Yellow)
                lblPriorityCustomer.BackColor = Color.Teal;
            else
                lblPriorityCustomer.BackColor = Color.Yellow;
        }


        public void ChangeCityToDefault()
        {
            this.txtCity.Tag = SystemSettings.settings.plngDefaultCityCode;
            this.txtCity.Text = SystemSettings.settings.pstrDefaultCityName;
            this.txtRegion.Text = SystemSettings.settings.pstrDefaultRegionName;
            this.txtPostal_Code.Text = SystemSettings.settings.pstrDefaultPostalCode;
		}

        public void RefreshCustomerHandling()
        {
            frmCustomer_Activated(null, null);
        }

        private void FillCustomerFromSessionCustomer(Customer customerHold)
        {
            try
            {
                if (customerHold != null)
                {
                    tdbtxtPhone_Ext.Text = String.IsNullOrEmpty(customerHold.Phone_Ext) ? "" : customerHold.Phone_Ext;
                    txtName.Text = String.IsNullOrEmpty(customerHold.Name) ? "" : customerHold.Name;
                    if (customerHold.date_of_birth.ToString() == "1/1/0001 12:00:00 AM")
                        TDBdob.Text = TDBdob.MinDate.ToShortDateString();
                    else
                        TDBdob.Text = (customerHold.date_of_birth >= TDBdob.MinDate && customerHold.date_of_birth <= TDBdob.MaxDate) ? customerHold.date_of_birth.ToShortDateString() : TDBdob.MinDate.ToShortDateString();

                    if (customerHold.anniversary_date.ToString() == "1/1/0001 12:00:00 AM")
                        TDBanniversary.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    else
                        TDBanniversary.Text = (customerHold.anniversary_date >= TDBanniversary.MinDate && customerHold.anniversary_date <= TDBanniversary.MaxDate) ? customerHold.anniversary_date.ToShortDateString() : TDBanniversary.MinDate.ToShortDateString();

                    txtCity.Tag = customerHold.Customer_City_Code;
                    txtCity.Text = customerHold.City;
                    txtgstin_number.Text = customerHold.gstin_number;
                    txtCross_Street.Text = customerHold.Cross_Street;
                    txtRegion.Text = customerHold.Region;
                    txtStreet.Text = customerHold.Customer_Street_Name;
                    txtTentNumber.Text = string.Empty;

                    if (!string.IsNullOrEmpty(customerHold.Address_Type))
                    {
                        if (!string.IsNullOrEmpty(customerHold.Address_Type))
                        {
                            if (customerHold.Address_Type == "R")
                            {
                                this.btn_Residence.PerformClick();
                            }
                            else if (customerHold.Address_Type == "K")
                            {
                                this.btn_Kiosk.PerformClick();
                            }
                            else if (customerHold.Address_Type == "H")
                            {
                                this.btn_Kiosk.PerformClick();
                            }
                            else if (customerHold.Address_Type == "B")
                            {
                                this.btn_Business.PerformClick();
                            }
                            else if (customerHold.Address_Type == "C")
                            {
                                this.btn_College.PerformClick();
                            }
                        }
                    }

                    if (customerHold.Comments != null)
                    {
                        int indexCommentsStart = customerHold.Comments.IndexOf("fs17");

                        if (indexCommentsStart > 0)
                        {
                            string comment = customerHold.Comments.Substring(indexCommentsStart + 4, customerHold.Comments.Length - (indexCommentsStart + 4) - 8);
                            txtNotes_Instore.Text = comment;
                        }
                        else
                        {
                            txtNotes_Instore.Text = customerHold.Comments;
                        }
                    }
                    else
                    {
                        txtNotes_Instore.Text = customerHold.Comments;
                    }


                    if (customerHold.DriverComments != null)
                    {
                        int indexDriverCommentsStart = customerHold.DriverComments.IndexOf("fs17");

                        if (indexDriverCommentsStart > 0)
                        {
                            string comment = customerHold.DriverComments.Substring(indexDriverCommentsStart + 4, customerHold.DriverComments.Length - (indexDriverCommentsStart + 4) - 8);
                            txtNotes_Delivery.Text = comment;
                        }
                        else
                        {
                            txtNotes_Delivery.Text = customerHold.DriverComments;
                        }
                    }
                    else
                    {
                        txtNotes_Delivery.Text = customerHold.DriverComments;
                    }

                    if (customerHold.Manager_Notes != null)
                    {
                        int indexManagerNotesStart = customerHold.Manager_Notes.IndexOf("fs17");

                        if (indexManagerNotesStart > 0)
                        {
                            string comment = customerHold.Manager_Notes.Substring(indexManagerNotesStart + 4, customerHold.Manager_Notes.Length - (indexManagerNotesStart + 4) - 8);
                            txtNotes_manager.Text = comment;
                        }
                        else
                        {
                            txtNotes_manager.Text = customerHold.Manager_Notes;
                        }

                    }
                    else
                    {
                        txtNotes_manager.Text = customerHold.Manager_Notes;
                    }

                }

                txtCompanyName.Text = customerHold.Company_Name;
                txtStreet_Number.Text = customerHold.Street_Number;
                txtAddress_Line_2.Text = customerHold.Address_Line_2;
                txtAddress_Line_3.Text = customerHold.Address_Line_3;
                txtAddress_Line_4.Text = customerHold.Address_Line_4;
                txtPostal_Code.Text = customerHold.Postal_Code;
                txtSuite.Text = customerHold.Suite;

            }
            catch (Exception ex)
            {
                // customerHold.Customer_Street_Name;
                Logger.Trace("ERROR", "frmCustomer-FillCustomerFromSessionCustomer(): " + ex.Message, ex, true);
            }
        }

        private bool IsControlMandatory(string ctlName)
        {
            bool controlStatus = false;
            foreach (Control c in tlpCustomerPanel.Controls)
            {
                if (c.Name == ctlName)
                {
                    if (c.ForeColor == Color.Yellow)
                    {
                        controlStatus = true;
                        break;
                    }
                }
            }
            return controlStatus; 
        }
        public bool ValidateCustomerInfo1()
        {
            bool validationStatus = true;
            if (IsControlMandatory("ltxtPhone_Number"))
            {
                if (tdbmPhone_Number.Text.Length < Session.MinPhoneDigits)
                {
                    CustomMessageBox.Show("Phone number " + MessageConstant.NotValidMessage, CustomMessageBox.Buttons.OK, CustomMessageBox.Icon.Exclamation);
                    validationStatus = false;
                    return validationStatus;
                }
            }
            if (IsControlMandatory("ltxtName"))
            {
                if (txtName.Text.Length == 0)
                {
                    CustomMessageBox.Show("Customer name " + MessageConstant.NotValidMessage, CustomMessageBox.Buttons.OK, CustomMessageBox.Icon.Exclamation);
                    validationStatus = false;
                    return validationStatus;
                }
            }
            if (IsControlMandatory("ltxtCompanyName"))
            {
                if (txtCompanyName.Text.Length == 0)
                {
                    CustomMessageBox.Show("Company name " + MessageConstant.NotValidMessage, CustomMessageBox.Buttons.OK, CustomMessageBox.Icon.Exclamation);
                    validationStatus = false;
                    return validationStatus;
                }
            }
            if (IsControlMandatory("ltxtStreet_Number"))
            {
                if (txtStreet.Text.Length == 0)
                {
                    CustomMessageBox.Show(OrderCompleteFunctions.GetText(LanguageConstant.cintMSGValidStreetRequired), CustomMessageBox.Buttons.OK, CustomMessageBox.Icon.Exclamation);
                    validationStatus = false;
                    return validationStatus;
                }
            }
            if (IsControlMandatory("ltxtSuite"))
            {
                if (txtSuite.Text.Length == 0)
                {
                    CustomMessageBox.Show("Customer Suite/Room " + MessageConstant.NotValidMessage, CustomMessageBox.Buttons.OK, CustomMessageBox.Icon.Exclamation);
                    validationStatus = false;
                    return validationStatus;
                }
            }
            if (IsControlMandatory("ltxtCity"))
            {
                if (txtCity.Text.Length == 0)
                {
                    CustomMessageBox.Show("Customer city " + MessageConstant.NotValidMessage, CustomMessageBox.Buttons.OK, CustomMessageBox.Icon.Exclamation);
                    validationStatus = false;
                    return validationStatus;
                }
            }
            if (IsControlMandatory("ltxtRegion"))
            {
                if (txtRegion.Text.Length == 0)
                {
                    CustomMessageBox.Show("Customer region " + MessageConstant.NotValidMessage, CustomMessageBox.Buttons.OK, CustomMessageBox.Icon.Exclamation);
                    validationStatus = false;
                    return validationStatus;
                }
            }
            if (IsControlMandatory("ltxtPostal_Code"))
            {
                if (txtPostal_Code.Text.Length == 0)
                {
                    CustomMessageBox.Show("Customer postal code " + MessageConstant.NotValidMessage, CustomMessageBox.Buttons.OK, CustomMessageBox.Icon.Exclamation);
                    validationStatus = false;
                    return validationStatus;
                }
            }

            return validationStatus;
        }
        public bool ValidateCustomerInfo()
        {
            bool validationStatus =true;
            foreach (Control c in tlpCustomerPanel.Controls)
            {
                if (c.GetType() == typeof(Label))
                {
                    string ctlTypeName = c.Name.Substring(0, 4);
                    string ctlName = c.Name;
                    if (ctlTypeName.ToLower() == "ltxt")
                    {
                        if (c.ForeColor == Color.Yellow)
                        {
                            switch (ctlName)
                            {
                                case "ltxtPhone_Number":
                                    if (tdbmPhone_Number.Text.Length < Session.MinPhoneDigits)
                                    {
                                        CustomMessageBox.Show("Phone number " + MessageConstant.NotValidMessage, CustomMessageBox.Buttons.OK, CustomMessageBox.Icon.Exclamation);
                                        validationStatus = false;
                                    }
                                    break;
                                case "ltxtCompanyName":
                                    if (txtCompanyName.Text.Length ==0)
                                    {
                                        CustomMessageBox.Show("Company name " + MessageConstant.NotValidMessage, CustomMessageBox.Buttons.OK, CustomMessageBox.Icon.Exclamation);
                                        validationStatus = false;
                                    }
                                    break;
                                case "ltxtCity":
                                    if (txtCity.Text.Length == 0)
                                    {
                                        CustomMessageBox.Show("Customer city " + MessageConstant.NotValidMessage, CustomMessageBox.Buttons.OK, CustomMessageBox.Icon.Exclamation);
                                        validationStatus = false;
                                    }
                                    break;
                                case "ltxtRegion":
                                    if (txtRegion.Text.Length == 0)
                                    {
                                        CustomMessageBox.Show("Customer region " + MessageConstant.NotValidMessage, CustomMessageBox.Buttons.OK, CustomMessageBox.Icon.Exclamation);
                                        validationStatus = false;
                                    }
                                    break;
                                case "ltxtPostal_Code":
                                    if (txtPostal_Code.Text.Length == 0)
                                    {
                                        CustomMessageBox.Show("Customer postal code " + MessageConstant.NotValidMessage, CustomMessageBox.Buttons.OK, CustomMessageBox.Icon.Exclamation);
                                        validationStatus = false;
                                    }
                                    break;
                                case "ltxtName":
                                    if (txtName.Text.Length == 0)
                                    {
                                        CustomMessageBox.Show("Customer name " + MessageConstant.NotValidMessage, CustomMessageBox.Buttons.OK, CustomMessageBox.Icon.Exclamation);
                                        validationStatus = false;
                                    }
                                    break;
                                case "ltxtStreet_Number":
                                    if (txtStreet.Text.Length == 0)
                                    {
                                        CustomMessageBox.Show(OrderCompleteFunctions.GetText(LanguageConstant.cintMSGValidStreetRequired), CustomMessageBox.Buttons.OK, CustomMessageBox.Icon.Exclamation);
                                        validationStatus = false;
                                    }
                                    break;
                                case "ltxtSuite":
                                    if (txtSuite.Text.Length == 0)
                                    {
                                        CustomMessageBox.Show("Customer suite " + MessageConstant.NotValidMessage, CustomMessageBox.Buttons.OK, CustomMessageBox.Icon.Exclamation);
                                        validationStatus = false;
                                    }
                                    break;
                               
                            }
                        }
                    }
                }
                if (!validationStatus)
                    break;
            }

            return validationStatus;
        }

        
        
        private void PopulateSourceName()
        
        {
            try
            {
                cmbSourceName.DataSource = Session.SourceName;
                cmbSourceName.ValueMember = "Source_Code";
                cmbSourceName.DisplayMember = "Source_Desc";
                
            }
            catch (Exception ex)
            {
                Logger.Trace("ERROR", "PopulateSourceName: " + ex.Message, ex, true);
            }
        }

        private void cmbSourceName_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session.selectedSourceName = cmbSourceName.Text;
            CartFunctions.SourceChange();
            Form frmObj = Application.OpenForms["frmOrder"];
            if (frmObj != null)
            {
                ((frmOrder)frmObj).RefreshCartUI();
            }
        }
    }
}
