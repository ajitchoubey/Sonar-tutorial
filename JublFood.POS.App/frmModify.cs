using Jublfood.AppLogger;
using JublFood.POS.App.API;
using JublFood.POS.App.BusinessLayer;
using JublFood.POS.App.Cache;
using JublFood.POS.App.Class;
using JublFood.POS.App.Models.Employee;
using JublFood.POS.App.Models.Order;
using JublFood.POS.App.Models.Payment;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http;
using System.Windows.Forms;

namespace JublFood.POS.App
{
    public partial class frmModify : Form
    {
        private bool ALT_F4 = false;
        long Printing_Order_Number;
        DateTime Printing_Order_date;
        public static string phonenumber;
        bool OrderModify = false;
        DataTable table;
        string order_type = "'C', 'D', 'I', 'P'";
        public static bool PayandModifyOrder = false;
        string labelMessgae = string.Empty;
        public  bool goPayment = false;
        public static bool CloseApp = false;
        public  bool cmdOrderModify = false;
        public static int FutureTodayOrder = 0;
        public static DateTime OrderTime;
        public  bool logout;
        public frmModify()
        {
            InitializeComponent();
            //Common.colorListViewHeader(ref gridmodify, Color.Teal, Color.White);
            uC_CustomerOrderBottomMenu.Formname = "frmModify";
            uC_CustomerOrderBottomMenu.ListViewFormModify = gridmodify;
            uC_CustomerOrderBottomMenu.SetButtonColorForFormModify();
            uC_CustomerOrderBottomMenu.LoadOrderType();
            uC_CustomerOrderBottomMenu.UserControlButtonClicked += new
            EventHandler(OrderType_Click);

            SetButtonText();
        }
        
        private void OrderType_Click(object sender, EventArgs e)
        {
            try
            {
                
                string SelectedPaytype = "";
                foreach (Control control in uC_CustomerOrderBottomMenu.Controls)
                {

                    foreach (Control _control in control.Controls)
                    {
                        if (((Button)_control).BackColor == Color.FromArgb(255, 128, 128))
                        {
                            string button = Convert.ToString(((Button)_control).Tag);
                            SelectedPaytype += "\"" + button + "\"" + ",";
                        }


                    }
                    SelectedPaytype = SelectedPaytype.TrimEnd(',');
                    order_type = SelectedPaytype;
                }
                if (labelMessgae == "All")
                {
                    CallGrid(false, SelectedPaytype, FutureTodayOrder);
                }
                else if (labelMessgae == "Closed")
                {
                    closeorder(SelectedPaytype, FutureTodayOrder);
                }
                else if (labelMessgae == "Abandoned")
                {
                    Abandonedorder(SelectedPaytype, FutureTodayOrder);
                }
                else
                {
                    CallGrid(true, SelectedPaytype, FutureTodayOrder);
                }
                ;
            }
            catch(Exception ex)
            {
                Logger.Trace("ERROR", "frmModify-OrderType_Click(): " + ex.Message, ex, true);
                CustomMessageBox.Show(ex.Message);
            }
        }
        private void SetButtonText()
        {
            string labelText = null;
            if (Common.DictAllButtonText.TryGetValue(LanguageConstant.cintKeyBoard, out labelText))
            {
                cmdKeyboard.Text = labelText;
            }
            if (Common.DictAllButtonText.TryGetValue(LanguageConstant.cintClear, out labelText))
            {
                cmdClear.Text = labelText;
            }
            if (Common.DictAllButtonText.TryGetValue(LanguageConstant.cintNoSale, out labelText))
            {
                cmdNoSale.Text = labelText;
            }
            if (Common.DictAllButtonText.TryGetValue(LanguageConstant.cintPrintExtra, out labelText))
            {
                cmdPrintOnDemand.Text = labelText;
            }
            if (Common.DictAllButtonText.TryGetValue(LanguageConstant.cintLike, out labelText))
            {
                cmdLike.Text = labelText;
            }
            if (Common.DictAllButtonText.TryGetValue(LanguageConstant.cintExact, out labelText))
            {
                cmdExact.Text = labelText;
            }
            if (Common.DictAllButtonText.TryGetValue(LanguageConstant.cintPay, out labelText))
            {
                cmdPay.Text = labelText;
            }
            if (Common.DictAllButtonText.TryGetValue(LanguageConstant.cintClose, out labelText))
            {
                cmdCloseOrder.Text = labelText;
            }
            if (Common.DictAllButtonText.TryGetValue(LanguageConstant.cintTillChange, out labelText))
            {
                cmdTillChange.Text = labelText;
            }
            if (Common.DictAllButtonText.TryGetValue(LanguageConstant.cintTillStatus, out labelText))
            {
                cmdTillStatus.Text = labelText;
            }
            if (Common.DictAllButtonText.TryGetValue(LanguageConstant.cintFuture, out labelText))
            {
                cmdDayFilter.Text = labelText;
            }
            if (Common.DictAllButtonText.TryGetValue(LanguageConstant.cintAll, out labelText))
            {
                cmdStatusFilter.Text = labelText;
            }
            
        }
        private void cmdKeyboard_Click(object sender, EventArgs e)
        {

            frmKeyBoard objfrmKeyBoard = new frmKeyBoard(txtFilter, "Search");
            objfrmKeyBoard.ShowDialog();
        }

        private void cmdRight_Click(object sender, EventArgs e)
        {
            try
            {
                if (!(gridmodify.FirstDisplayedScrollingColumnIndex + 4 >= gridmodify.Columns.Count - 1))
                    gridmodify.FirstDisplayedScrollingColumnIndex = gridmodify.FirstDisplayedScrollingColumnIndex + 1;
            }
            catch(Exception ex)
            {
                CustomMessageBox.Show(ex.Message);
            }
        }

        private void cmdStatusFilter_Click(object sender, EventArgs e)
        {
            SetbuttonImageandText(cmdStatusFilter);
           
            cmdPrintOnDemand.BackColor = DefaultBackColor;
            cmdCloseOrder.BackColor = DefaultBackColor;
            cmdPay.BackColor = DefaultBackColor;
        }
        private void SetbuttonImageandText(Button btn)
        {
            try
            {
                labelMessgae = string.Empty;
                if (btn.Text == "All")
                {
                    labelMessgae = "All";
                    btn.Text = "Closed";
                    btn.Image = Properties.Resources._35;
                    CallGrid(false, order_type, FutureTodayOrder);
                }
                else if (btn.Text == "Closed")
                {
                    closeorder(order_type, FutureTodayOrder);
                    labelMessgae = "Closed";
                    btn.Text = "Abandoned";
                    btn.Image = Properties.Resources._92;
                }
                else if (btn.Text == "Abandoned")
                {
                    Abandonedorder(order_type, FutureTodayOrder);
                    labelMessgae = "Abandoned";
                    btn.Text = "Open";
                    btn.Image = Properties.Resources._25;
                }
                else if (btn.Text == "Open")
                {
                    labelMessgae = "Open";
                    btn.Text = "All";
                    btn.Image = Properties.Resources._43;
                    CallGrid(true, order_type, FutureTodayOrder);
                }
                if (FutureTodayOrder == 0)
                    cmdDayFilter.Text = "Today";
                else
                    cmdDayFilter.Text = "Future";
                lblTitle.Text = cmdDayFilter.Text + " - " + labelMessgae + " Orders";
            }
            catch(Exception ex)
            {
                CustomMessageBox.Show(ex.Message);
            }
        }

        private void cmdDayFilter_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmdDayFilter.Text == "Future")
                {
                    FutureTodayOrder = 1;
                    cmdDayFilter.Text = "Today";
                    cmdDayFilter.Image = Properties.Resources._202;
                    lblTitle.Text = lblTitle.Text.Replace("Today", "Future");
                    if (labelMessgae == "All")
                    {
                        CallGrid(true, order_type, 1);
                    }
                    else if (labelMessgae == "Closed")
                    {
                        closeorder(order_type, FutureTodayOrder);
                    }
                    else if (labelMessgae == "Abandoned")
                    {
                        Abandonedorder(order_type, FutureTodayOrder);
                    }
                    else
                    {
                        CallGrid(true, order_type, 1);
                    }

                    
                }
                else if (cmdDayFilter.Text == "Today")
                {
                    FutureTodayOrder = 0;
                    cmdDayFilter.Text = "Future";
                    lblTitle.Text = lblTitle.Text.Replace("Future", "Today");
                    cmdDayFilter.Image = Properties.Resources._201;

                    if (labelMessgae == "All")
                    {
                        CallGrid(false, order_type, 0);
                    }
                   else if (labelMessgae == "Closed")
                    {
                        closeorder(order_type,FutureTodayOrder);
                    }
                   else if (labelMessgae == "Abandoned")
                    {
                        Abandonedorder(order_type,FutureTodayOrder);
                    }
                   else
                    {
                        CallGrid(true, order_type, 0);
                    }
                        
                   
                   
                   
                }

                cmdPrintOnDemand.BackColor = DefaultBackColor;
                cmdCloseOrder.BackColor = DefaultBackColor;
                cmdPay.BackColor = DefaultBackColor;
            }
            catch(Exception ex)
            {
                CustomMessageBox.Show(ex.Message);
            }
        }

        private void cmdCloseOrder_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmdCloseOrder.BackColor == Color.FromArgb(255, 128, 128))
                {
                    cmdCloseOrder.BackColor = DefaultBackColor;
                    cmdPay.BackColor = DefaultBackColor;
                    cmdPrintOnDemand.BackColor = DefaultBackColor;
                }
                else
                {
                    if (cmdPay.BackColor == Color.FromArgb(255, 128, 128)|| cmdPrintOnDemand.BackColor == Color.FromArgb(255, 128, 128))
                    {
                        cmdPay.BackColor = DefaultBackColor;
                        cmdPrintOnDemand.BackColor = DefaultBackColor;
                    }
                    cmdCloseOrder.BackColor = Color.FromArgb(255, 128, 128);
                }
            }
            catch(Exception ex)
            {
                CustomMessageBox.Show(ex.Message);
            }
            
        }
        
        private void cmdPay_Click(object sender, EventArgs e)
        {
            try
            {
                //Session.pblnModifyingOrder = true;
                if (cmdPay.BackColor == Color.FromArgb(255, 128, 128))
                {
                    cmdPay.BackColor = DefaultBackColor;
                    cmdCloseOrder.BackColor = DefaultBackColor;
                    cmdPrintOnDemand.BackColor = DefaultBackColor;
                }
                else
                {
                    if (cmdCloseOrder.BackColor == Color.FromArgb(255, 128, 128) || cmdPrintOnDemand.BackColor == Color.FromArgb(255, 128, 128))
                    {
                        cmdCloseOrder.BackColor = DefaultBackColor;
                        cmdPrintOnDemand.BackColor = DefaultBackColor;
                    }
                    cmdPay.BackColor = Color.FromArgb(255, 128, 128);
                    
                }
            }
            catch(Exception ex)
            {
                Logger.Trace("ERROR", "frmModify-cmdPay_Click(): " + ex.Message, ex, true);
            }
           
        }
        public void SetConntol()
        {
            try
            {
                foreach (Control control in uC_CustomerOrderBottomMenu.Controls)
                {

                    foreach (Control _control in control.Controls)
                    {
                        ((Button)_control).BackColor = Color.FromArgb(255, 128, 128);

                    }
                }
            }
            catch(Exception ex)
            {
                CustomMessageBox.Show(ex.Message);
            }

        }
        
        private void frmModify_Load(object sender, EventArgs e)
        {
            try
            {
                cmdOrderModify = false;
                cmdNoSale.Enabled = false;
                uC_Customer_OrderMenu1.ucFunctionList = uC_FunctionList1;
                uC_Customer_OrderMenu1.ucInformationList = uC_InformationList1;
                SetConntol();
                getDeviceSettings();
                CallGrid(true, order_type, 0);
                cmdChangeOrders_Click();
                uC_InformationList1.Size = new Size(70, 165);
                uC_FunctionList1.Size = new Size(70, 165);
                this.Location = new Point(((Screen.PrimaryScreen.Bounds.Width - this.Size.Width) / 2) + 5, ((Screen.PrimaryScreen.Bounds.Height - this.Size.Height) / 2));

                uC_Customer_OrderMenu1.cmdSearch.Enabled = false;

                //string AggregatorGST = Convert.ToString(SystemSettings.GetSettingValue("AggregatorGSTCalculation", Session._LocationCode));
                //cmdPrintOnDemand.Enabled = AggregatorGST == "1" ? false : true;

            }
            catch (Exception ex)
            {
                Logger.Trace("ERROR", "frmModify-frmModify_Load(): " + ex.Message, ex, true);
            }
            
        }
        public void CallGrid(bool ViewOrder, string Order_Type, int FutureOrder)
        {
            try
            {
                if (Session.CurrentEmployee != null)
                {
                    HttpClient client1 = new HttpClient();
                    GetOrderRequest getOrderRequest = new GetOrderRequest();
                    getOrderRequest.LocationCode = Session._LocationCode;
                    getOrderRequest.Language_Code = Session.CurrentEmployee.LoginDetail.LanguageCode;
                    getOrderRequest.Order_Date = Session.SystemDate;
                    getOrderRequest.Address_Format = "CASE ISNULL(Street_Number, '') WHEN '' THEN '' ELSE SUBSTRING(Street_Number, 1, 10) + ' ' END + CASE ISNULL(Pre_Direction, '') WHEN '' THEN '' ELSE SUBSTRING(Pre_Direction, 1, 2) + ' ' END + CASE ISNULL(Street_Name, '') WHEN '' THEN '' ELSE SUBSTRING(Street_Name, 1, 50) + ' ' END + CASE ISNULL(Post_Direction, '') WHEN '' THEN '' ELSE SUBSTRING(Post_Direction, 1, 2) END + CASE ISNULL((CASE address_type WHEN 'H' THEN Customer_Name ELSE Suite END), '') WHEN '' THEN '' ELSE '#' + SUBSTRING(CASE address_type WHEN 'H' THEN Customer_Name ELSE Suite END, 1, 50) END"; ;
                    getOrderRequest.Order_Type_String = Order_Type;
                    getOrderRequest.Show_Order_Takers_Orders_Only = false;
                    getOrderRequest.Employee_Code = Session.CurrentEmployee.LoginDetail.EmployeeCode;
                    if (ViewOrder != true)
                        getOrderRequest.View_All_Orders = true;
                    DataTable dt = bindgrid(getOrderRequest, true, FutureOrder);
                    table = dt;
                    gridmodify.DataSource = dt;
                    gridmodify.Columns[0].HeaderText = "#";
                    gridmodify.Columns[0].Width = 50;
                    gridmodify.Columns[1].Width = 100;
                    gridmodify.Columns[2].Width = 100;
                    gridmodify.Columns[3].Width = 100;
                    gridmodify.Columns[4].Width = 100;
                    gridmodify.Columns[5].Width = 250;
                    gridmodify.Columns[6].Width = 100;
                    gridmodify.Columns[7].Width = 100;
                    gridmodify.Columns[8].Width = 100;
                    gridmodify.Columns[9].Width = 100;
                    gridmodify.Columns[10].Width = 100;
                    gridmodify.Columns[11].Width = 100;
                    gridmodify.Columns["ROI_Customer"].Width = 0;
                    gridmodify.Columns["AggregatorGSTCalculation"].Width = 0;
                    this.gridmodify.Columns[2].Frozen = true;

                    gridmodify.DefaultCellStyle.BackColor = Color.LightYellow;
                    gridmodify.ClearSelection();
                    if (gridmodify.Rows.Count > 11)
                    {
                        cmdUp.Enabled = true;
                        cmdDown.Enabled = true;
                    }
                    else
                    {
                        cmdUp.Enabled = false;
                        cmdDown.Enabled = false;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Trace("ERROR", "frmModify-CallGrid(): " + ex.Message, ex, true);
            }
        }

        private void cmdLeft_Click(object sender, EventArgs e)
        {
            try
            {
                if (!(gridmodify.FirstDisplayedScrollingColumnIndex - 1>= gridmodify.Columns.Count - 1))
                    gridmodify.FirstDisplayedScrollingColumnIndex = gridmodify.FirstDisplayedScrollingColumnIndex -1;
                //this.gridmodify.HorizontalScrollingOffset = this.gridmodify.HorizontalScrollingOffset - 60;
            }
            catch
            {

            }
        }

        private void cmdUp_Click(object sender, EventArgs e)
        {
            try
            {
                //if (gridmodify.Rows.Count > gridmodify.FirstDisplayedScrollingRowIndex +1)
                    gridmodify.FirstDisplayedScrollingRowIndex = this.gridmodify.FirstDisplayedScrollingRowIndex + 1;
            }
            catch(Exception ex)
            {

            }
        }

        private void cmdDown_Click(object sender, EventArgs e)
        {
            try
            {

                gridmodify.FirstDisplayedScrollingRowIndex = this.gridmodify.FirstDisplayedScrollingRowIndex - 1;
            }
            catch (Exception ex)
            {
                //CustomMessageBox.Show(ex.Message);
            }
        }
        
        private void gridmodify_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                goPayment = false;
                Session.pblnModifyingOrder = true;
                OriginalOrderInfo originalOrderInfos = new OriginalOrderInfo();
                gridmodify.Rows[gridmodify.CurrentRow.Index].Selected = true;

                long order_number;
                Session.originalresponsePayment = new ResponsePayment();
                string Gridorder = Convert.ToString(gridmodify.Rows[gridmodify.CurrentRow.Index].Cells[0].Value);
                string roiCustomer = Convert.ToString(gridmodify.Rows[gridmodify.CurrentRow.Index].Cells["ROI_Customer"].Value);
                bool AggregatorGSTCalculation= Convert.ToBoolean(gridmodify.Rows[gridmodify.CurrentRow.Index].Cells["AggregatorGSTCalculation"].Value);
                if (Gridorder.Contains("*"))
                {
                    string[] arr = Gridorder.Split('*');
                    order_number = Convert.ToInt32(arr[1]);
                    PayandModifyOrder = true;
                }
                else if (Gridorder.ToUpper().Contains("A"))
                {
                    string[] arr = Gridorder.Split('A');
                    order_number = Convert.ToInt32(arr[1]);
                    CustomMessageBox.Show(MessageConstant.AbandedOrderModify, CustomMessageBox.Buttons.OK, CustomMessageBox.Icon.Exclamation);
                    return;
                }
                else
                {
                    order_number = Convert.ToInt32(Gridorder);
                    PayandModifyOrder = false;
                }
                string Location_code = Session._LocationCode;
                DateTime Order_Date = Convert.ToDateTime(gridmodify.Rows[gridmodify.CurrentRow.Index].Cells[1].Value);
               
               
                string Phone_Number = Convert.ToString(gridmodify.Rows[gridmodify.CurrentRow.Index].Cells[3].Value);
                phonenumber = Phone_Number;
                Printing_Order_Number = order_number;
                Printing_Order_date = Order_Date;
                if (cmdPay.BackColor == Color.FromArgb(255, 128, 128))
                {
                    
                    PayForOrder(order_number, Order_Date, PayandModifyOrder, Phone_Number);
                    
                }
                else if (cmdPrintOnDemand.BackColor == Color.FromArgb(255, 128, 128))
                {
                    string RePrintAggregatorGST = Convert.ToString(SystemSettings.GetSettingValue("RePrintForAggregatorGST", Session._LocationCode));
                    string AggregatorsListForGST = Convert.ToString(SystemSettings.GetSettingValue("AggregatorsListForGST", Session._LocationCode));

                    //if (string.IsNullOrEmpty(roiCustomer))
                    //    roiCustomer = "0";

                    //bool ApplyAggregatorGST = false;
                    //List<string> AggregatorList = AggregatorsListForGST.Split(',').ToList();
                    //if (AggregatorList != null && AggregatorList.FindIndex(x => x.ToUpper() ==roiCustomer.ToUpper()) > -1)
                    //    ApplyAggregatorGST = true;

                    if (AggregatorGSTCalculation && RePrintAggregatorGST == "0")
                    {
                        CustomMessageBox.Show("Re-Print not allowed for aggregator orders.", CustomMessageBox.Buttons.OK, CustomMessageBox.Icon.Error);
                        return;
                    }
                    else
                    {
                        PrintOrder(order_number, Order_Date, PayandModifyOrder, Phone_Number); 
                    }
                    
                        
                }
                else if (cmdCloseOrder.BackColor == Color.FromArgb(255, 128, 128))
                {
                    
                   CloseOrder(order_number, Order_Date, PayandModifyOrder, Phone_Number);
                }
                else
                {
                    CustomMessageBox.Show("Order modification not allowed.", CustomMessageBox.Buttons.OK, CustomMessageBox.Icon.Error);
                    //cmdOrderModify = true;
                    //ModifyOrder(order_number, Order_Date, PayandModifyOrder, Phone_Number);
                    
                }
            }
            catch(Exception ex)
            {
                Logger.Trace("ERROR", "frmModify-gridmodify_CellClick(): " + ex.Message, ex, true);
                CustomMessageBox.Show(ex.Message);
                Session.pblnModifyingOrder = false;
            }
            
        }

        private void cmdExact_Click(object sender, EventArgs e)
        {           
            try
            {
                cmdPrintOnDemand.BackColor = DefaultBackColor;
                cmdCloseOrder.BackColor = DefaultBackColor;
                cmdPay.BackColor = DefaultBackColor;
                DataView dv = table.DefaultView;
                dv.RowFilter = string.Format("SNo='{0}' OR Name='{1}' OR Phone='{2}'  OR Employee='{3}' OR Driver='{4}' OR Status='{5}'", txtFilter.Text, txtFilter.Text, txtFilter.Text, txtFilter.Text, txtFilter.Text, txtFilter.Text);
                gridmodify.DataSource = dv;
                txtFilter.Text = "";
            }
            catch
            {

            }
        }

        private void cmdLike_Click(object sender, EventArgs e)
        {
            try
            {
                cmdPrintOnDemand.BackColor = DefaultBackColor;
                cmdCloseOrder.BackColor = DefaultBackColor;
                cmdPay.BackColor = DefaultBackColor;
                DataView dv = table.DefaultView;
                dv.RowFilter = string.Format("SNo like '%{0}%' OR Name like'%{1}%' OR Phone like '%{2}%'  OR Employee like'%{3}%' OR Driver like'%{4}%' OR Status like'%{5}%'", txtFilter.Text, txtFilter.Text, txtFilter.Text, txtFilter.Text, txtFilter.Text, txtFilter.Text);
                gridmodify.DataSource = dv;
                txtFilter.Text = "";
            }
            catch
            {

            }
        }

        private void cmdClear_Click(object sender, EventArgs e)
        {
            try
            {
                //CallGrid(true, order_type, 0);
                txtFilter.Text = "";
                cmdPrintOnDemand.BackColor = DefaultBackColor;
                cmdCloseOrder.BackColor = DefaultBackColor;
                cmdPay.BackColor = DefaultBackColor;                
            }
            catch
            {

            }
        }

        private void cmdPrintOnDemand_Click(object sender, EventArgs e)
        {
            if (cmdPrintOnDemand.BackColor == Color.FromArgb(255, 128, 128))
            {
                cmdPrintOnDemand.BackColor = DefaultBackColor;
                cmdCloseOrder.BackColor = DefaultBackColor;
                cmdPay.BackColor = DefaultBackColor;
            }
            else
            {
                if (cmdCloseOrder.BackColor == Color.FromArgb(255, 128, 128)|| cmdPay.BackColor == Color.FromArgb(255, 128, 128))
                {
                    cmdCloseOrder.BackColor = DefaultBackColor;
                    cmdPay.BackColor = DefaultBackColor;
                }
                    cmdPrintOnDemand.BackColor = Color.FromArgb(255, 128, 128);
            }
            
        }

        private DataTable bindgrid(GetOrderRequest getOrderRequest, bool order_modify,int Future)
        {
           
            DataTable dtCartTotals = new DataTable();
            try
            {
                dtCartTotals.Rows.Clear();
                dtCartTotals.Columns.Clear();
                dtCartTotals.Columns.Add("SNo");
                dtCartTotals.Columns.Add("Date");
                dtCartTotals.Columns.Add("Time");
                dtCartTotals.Columns.Add("Phone");
                dtCartTotals.Columns.Add("Name");
                dtCartTotals.Columns.Add("Address");
                dtCartTotals.Columns.Add("Employee");
                dtCartTotals.Columns.Add("Price");
                dtCartTotals.Columns.Add("Driver");
                dtCartTotals.Columns.Add("Route Time");
                dtCartTotals.Columns.Add("Return Time");
                dtCartTotals.Columns.Add("Status");
                dtCartTotals.Columns.Add("ROI_Customer");
                dtCartTotals.Columns.Add("AggregatorGSTCalculation");
                //dtCartTotals.Columns.Add("Added_By");
                GetOrderResponse getorderResponse = APILayer.getorder(getOrderRequest);
               

                if (getorderResponse.ResponseStatusCode == "1")
                {
                    if (getorderResponse.getOrders.Count > 0)
                    {
                        foreach (var orders in getorderResponse.getOrders)
                        {
                            DataRow dr = dtCartTotals.NewRow();
                            if (Future == 0)
                            {
                                if (SystemSettings.settings.pdtmSystem_Date == orders.Order_Date)
                                {
                                    dr["SNo"] = orders.BeingModified + Convert.ToString(orders.Order_Number);
                                    dr["Date"] = Convert.ToString(Convert.ToDateTime(orders.Order_Date).ToString("yyyy-MM-dd"));
                                    dr["Time"] = orders.Actual_Order_Time;
                                    dr["Phone"] = orders.Phone_Number;
                                    dr["Name"] = orders.Name;
                                    dr["Address"] = orders.Address;
                                    dr["Employee"] = orders.Order_Taker;
                                    dr["Price"] = orders.Price;
                                    dr["Driver"] = orders.Driver;
                                    if (Convert.ToString(orders.Route_Time) == "1/1/0001 12:00:00 AM" || Convert.ToString(orders.Route_Time)== "1/1/1900 12:00:00 AM" || orders.Route_Time==null)

                                        dr["Route Time"] = null;
                                    else
                                        dr["Route Time"] = Convert.ToString(Convert.ToDateTime(orders.Route_Time).ToShortTimeString());
                                    if (Convert.ToString(orders.Return_Time) == "1/1/0001 12:00:00 AM" || Convert.ToString(orders.Return_Time)== "1/1/1900 12:00:00 AM" || orders.Return_Time == null)
                                        dr["Return Time"] = null;
                                    else
                                    dr["Return Time"] = Convert.ToString(Convert.ToDateTime(orders.Return_Time).ToShortTimeString());

                                    dr["Status"] = orders.OpenStatus;
                                    dr["ROI_Customer"] = orders.ROI_Customer;
                                    dr["AggregatorGSTCalculation"] = orders.AggregatorGSTCalculation;
                                    dtCartTotals.Rows.Add(dr);
                                }
                            }
                            else
                            {
                                if (orders.Order_Date>SystemSettings.settings.pdtmSystem_Date)
                                {
                                    dr["SNo"] = orders.BeingModified + Convert.ToString(orders.Order_Number);
                                    dr["Date"] = Convert.ToString(Convert.ToDateTime(orders.Order_Date).ToString("yyyy-MM-dd"));
                                    dr["Time"] = orders.Actual_Order_Time;
                                    dr["Phone"] = orders.Phone_Number;
                                    dr["Name"] = orders.Name;
                                    dr["Address"] = orders.Address;
                                    dr["Employee"] = orders.Order_Taker;
                                    dr["Price"] = orders.Price;
                                    dr["Driver"] = orders.Driver;
                                    if (Convert.ToString(orders.Route_Time) == "1/1/0001 12:00:00 AM"|| Convert.ToString(orders.Route_Time) == "1/1/1900 12:00:00 AM" || orders.Route_Time == null)

                                        dr["Route Time"] = null;
                                    else
                                        dr["Route Time"] = Convert.ToString(Convert.ToDateTime(orders.Route_Time).ToShortTimeString());
                                    if (Convert.ToString(orders.Return_Time) == "1/1/0001 12:00:00 AM" || Convert.ToString(orders.Return_Time) == "1/1/1900 12:00:00 AM" || orders.Return_Time == null)
                                        dr["Return Time"] = null;

                                       else
                                        dr["Return Time"] = Convert.ToString(Convert.ToDateTime(orders.Return_Time).ToShortTimeString());
                                    dr["Status"] = orders.OpenStatus;
                                    dr["ROI_Customer"] = orders.ROI_Customer;
                                    dr["AggregatorGSTCalculation"] = orders.AggregatorGSTCalculation;
                                    dtCartTotals.Rows.Add(dr);
                                }
                            }

                        }

                    }

                }

            }
            catch (Exception ex)
            {
                Logger.Trace("ERROR", "frmModify-bindgrid(): " + ex.Message, ex, true);
            }
            return dtCartTotals;
        }
       
        public void cmdChangeOrders_Click()
        {
            try
            {
                Session.OrderReqField = new OrderReqField();
                if (SystemSettings.settings.pblnCashDrawerManagement == true)
                {
                    
                }
                uC_Customer_OrderMenu1.cmdTimedOrders.Enabled = false;
                uC_FunctionList1.cmdTaxExempt.Enabled = false;
                uC_Customer_OrderMenu1.cmdHistory.Enabled = false;
                uC_FunctionList1.cmdGiveCredit.Enabled = false;
                uC_FunctionList1.cmdUseCredit.Enabled = false;
                uC_Customer_OrderMenu1.cmdOrderCoupons.Enabled = false;
                uC_FunctionList1.cmdVoid.Enabled = false;
                uC_Customer_OrderMenu1.cmdDelivery_Info.Enabled = false;
                uC_Customer_OrderMenu1.cmdChangeOrders.Text = "Order";
                uC_Customer_OrderMenu1.cmdChangeOrders.Enabled = true;
                if (this.Text == APILayer.GetCatalogText(LanguageConstant.cintModify))
                {
                    if (!string.IsNullOrEmpty(Session.CurrentEmployee.LoginDetail.EmployeeCode))
                    {
                        gridmodify.Enabled = true;
                    }
                    else
                    {
                        gridmodify.Enabled = false;
                    }
                    if (!string.IsNullOrEmpty(Session.CurrentEmployee.LoginDetail.EmployeeCode))
                    {
                      
                    }
                    else
                    {
                        uC_Customer_OrderMenu1.cmdLogin.Enabled = true;
                        uC_Customer_OrderMenu1.cmdTimeClock.Enabled = true;
                        uC_Customer_OrderMenu1.cmdTimedOrders.Enabled = false;
                        uC_FunctionList1.cmdTaxExempt.Enabled = false;
                        uC_FunctionList1.cmdVoid.Enabled = false;
                        uC_Customer_OrderMenu1.cmdFunctions.Enabled = true;
                        uC_Customer_OrderMenu1.cmdChangeOrders.Enabled = true;
                        uC_Customer_OrderMenu1.cmdHistory.Enabled = false;
                        uC_Customer_OrderMenu1.cmdSearch.Enabled = false;
                        //ResetQSScreen False 'Disable Category screen
                        uC_Customer_OrderMenu1.cmdOrderCoupons.Enabled = false;
                        Session.OrderReqField.btn_Plus = false;
                        Session.OrderReqField.btn_Minus = false;
                        Session.OrderReqField.btn_Coupons = false;
                        Session.OrderReqField.btn_Up= false;
                        Session.OrderReqField.btn_Down = false;
                        Session.OrderReqField.btn_Instructions = false;
                        Session.OrderReqField.btn_Quantity = false;
                        uC_Customer_OrderMenu1.cmdDelivery_Info.Enabled = false;
                        PaymentFunctions.EnableOrderTypes(false);
                    }
                }
                else
                {
                    uC_Customer_OrderMenu1.cmdLogin.Enabled = true;
                    uC_Customer_OrderMenu1.cmdTimeClock.Enabled = true;
                    uC_Customer_OrderMenu1.cmdTimedOrders.Enabled = false;
                    uC_FunctionList1.cmdTaxExempt.Enabled = false;
                    uC_Customer_OrderMenu1.cmdFunctions.Enabled = true;
                    uC_Customer_OrderMenu1.cmdChangeOrders.Enabled = true;
                    uC_Customer_OrderMenu1.cmdHistory.Enabled = false;
                    //ResetQSScreen False 'Disable Category screen
                    uC_Customer_OrderMenu1.cmdOrderCoupons.Enabled = false;
                    Session.OrderReqField.btn_Plus = false;
                    Session.OrderReqField.btn_Minus = false;
                    Session.OrderReqField.btn_Coupons = false;
                    Session.OrderReqField.btn_Up = false;
                    Session.OrderReqField.btn_Down = false;
                    Session.OrderReqField.btn_Instructions = false;
                    Session.OrderReqField.btn_Quantity = false;
                    uC_Customer_OrderMenu1.cmdDelivery_Info.Enabled = false;
                    PaymentFunctions.EnableOrderTypes(false);

                }
            }
            catch(Exception ex)
            {
                CustomMessageBox.Show(ex.Message);
            }

        }
       private void closeorder(string Order_Type_Code, int value)
        {
            try
            {
                if (Session.CurrentEmployee != null)
                {
                    CallGrid(false, Order_Type_Code, value);
                    DataView dv = table.DefaultView;
                    dv.RowFilter = string.Format("Status='{0}'", "Closed");
                    gridmodify.DataSource = dv;
                    if (gridmodify.Rows.Count > 11)
                    {
                        cmdUp.Enabled = true;
                        cmdDown.Enabled = true;
                    }
                    else
                    {
                        cmdUp.Enabled = false;
                        cmdDown.Enabled = false;
                    }
                }
            }
            catch(Exception ex)
            {
                Logger.Trace("ERROR", "frmModify-closeorder(): " + ex.Message, ex, true);
            }

        }

        private void Abandonedorder(string type_code,int value)
        {
            try
            {
                if (Session.CurrentEmployee != null)
                {
                    CallGrid(false, type_code, value);
                    DataView dv = table.DefaultView;
                    dv.RowFilter = string.Format("Status='{0}'", "Abandoned");
                    gridmodify.DataSource = dv;
                    if (gridmodify.Rows.Count > 11)
                    {
                        cmdUp.Enabled = true;
                        cmdDown.Enabled = true;
                    }
                    else
                    {
                        cmdUp.Enabled = false;
                        cmdDown.Enabled = false;
                    }
                }
            }
            catch(Exception ex)
            {

            }
        }
        public void PayForOrder(long Order_number, DateTime Order_date, Boolean blnBeingModified,string phonenumber)
        {
            EmployeeResult oldLoginEmployee;
            frmLogin frmLogin = new frmLogin();
            int intReturn;
           
            string strCurrencyFormat;
            bool blnLoginSuccessful;
            bool blnClockedIn = false;
            bool TechnicalSupport = false;

            if (!string.IsNullOrEmpty(Session.CurrentEmployee.LoginDetail.EmployeeCode) && Session.CurrentEmployee.LoginDetail.blnPayNow)
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
                       /// return;
                    }
                    
                }
                else
                    blnLoginSuccessful = true;
            }
            else
            {
                oldLoginEmployee = Session.CurrentEmployee;
                frmLogin.SpecialAccess = true;
                frmLogin.Text = APILayer.GetCatalogText(LanguageConstant.cintSpecialAccess);
                frmLogin.RequirePassword = true;
                frmLogin.ShowDialog();

                if (Session.CurrentEmployee != null && Session.CurrentEmployee.LoginDetail != null)
                {
                    if (!string.IsNullOrEmpty(Session.CurrentEmployee.LoginDetail.EmployeeCode) && Session.CurrentEmployee.LoginDetail.blnPayNow)
                    {
                        blnLoginSuccessful = true;
                    }
                    else
                    {
                        blnLoginSuccessful = false;
                    }
                }
                else
                    blnLoginSuccessful = false;
                Session.CurrentEmployee = oldLoginEmployee;
            }

             TechnicalSupport = EmployeeFunctions.TechnicalSupport(Session._LocationCode, Session.CurrentEmployee.LoginDetail.EmployeeCode);

            if (!blnLoginSuccessful)
            {
                if (EmployeeFunctions.TechnicalSupport(Session._LocationCode, Session.CurrentEmployee.LoginDetail.EmployeeCode) == false)
                {
                    CustomMessageBox.Show(APILayer.GetCatalogText(LanguageConstant.cintMSGInsufficientPrivileges), CustomMessageBox.Buttons.OK, CustomMessageBox.Icon.Warning);
                    Session.pblnModifyingOrder = false;
                    return;
                }
            }


            if (TechnicalSupport)
            {
                blnClockedIn = true;
            }
            else
            {
                blnClockedIn = Convert.ToBoolean(EmployeeClockIn(Session._LocationCode, Session.SystemDate.ToString("yyyy-MM-dd"), Session.CurrentEmployee.LoginDetail.EmployeeCode));

            }
            if (blnClockedIn == false)
            {
                CustomMessageBox.Show(APILayer.GetCatalogText(LanguageConstant.cintMSGIDNoLongerClockedIn), CustomMessageBox.Buttons.OK, CustomMessageBox.Icon.Warning);
                frmLogin.ShowDialog();
                if (EmployeeFunctions.TechnicalSupport(Session._LocationCode, Session.CurrentEmployee.LoginDetail.EmployeeCode))
                {
                    blnClockedIn = true;

                }
                else
                {
                    blnClockedIn = Convert.ToBoolean(EmployeeClockIn(Session._LocationCode,Session.SystemDate.ToString("yyyy-MM-dd"), Session.CurrentEmployee.LoginDetail.EmployeeCode));
                }
            }

            if (blnBeingModified)
            {
                blnLoginSuccessful = false;

                if (!string.IsNullOrEmpty(Session.CurrentEmployee.LoginDetail.EmployeeCode) && (Session.CurrentEmployee.LoginDetail.blnAllowOrdersBeingModified))
                {
                    blnLoginSuccessful = true;
                }
                else
                {
                    oldLoginEmployee = Session.CurrentEmployee;
                    frmLogin.SpecialAccess = true;
                    frmLogin.Text = APILayer.GetCatalogText(LanguageConstant.cintSpecialAccess);
                    frmLogin.RequirePassword = true;
                    frmLogin.ShowDialog();

                    if (Session.CurrentEmployee != null && Session.CurrentEmployee.LoginDetail != null)
                    {
                        if (!string.IsNullOrEmpty(Session.CurrentEmployee.LoginDetail.EmployeeCode) && (Session.CurrentEmployee.LoginDetail.blnAllowOrdersBeingModified))
                        {
                            blnLoginSuccessful = true;
                        }
                        else
                        {
                            blnLoginSuccessful = false;
                        }
                    }
                    else
                        blnLoginSuccessful = false;

                    Session.CurrentEmployee=oldLoginEmployee;
                }

                if (blnLoginSuccessful == false)
                {
                    if (EmployeeFunctions.TechnicalSupport(Session._LocationCode, Session.CurrentEmployee.LoginDetail.EmployeeCode) == false)
                    {

                        CustomMessageBox.Show(APILayer.GetCatalogText(LanguageConstant.cintMSGInsufficientPrivileges), CustomMessageBox.Buttons.OK, CustomMessageBox.Icon.Warning);
                        Session.pblnModifyingOrder = false;
                        return;
                    }

                }
            }

           
            OrderFunctions.LoadOrderDetails(Order_number, Order_date, blnBeingModified,false, true,true, phonenumber);
            //Session.cart.cartHeader.Final_Total = GetFinalTotalforPayment(Session._LocationCode, Session.SystemDate, Order_number);
            goPayment = true;
            Session.IsPayClick = true;
            
            this.Close();
            
            

        }
        public void PrintOrder(long Order_number, DateTime Order_date, Boolean blnBeingModified,string phonenumber)
        {
            bool blnClockedIn = false;
            frmLogin frmLogin = new frmLogin();
            LoginResult oldLoginEmployee;
           
            bool blnLoginSuccessful;
            if (EmployeeFunctions.TechnicalSupport(Session._LocationCode, Session.CurrentEmployee.LoginDetail.EmployeeCode))
            {
                blnClockedIn = true;
            }
            else
            {

                blnClockedIn =Convert.ToBoolean(EmployeeClockIn(Session._LocationCode, Session.SystemDate.ToString("yyyy-MM-dd"), Session.CurrentEmployee.LoginDetail.EmployeeCode));
            }
            if (!blnClockedIn)
            {
                CustomMessageBox.Show(APILayer.GetCatalogText(LanguageConstant.cintMSGIDNoLongerClockedIn), CustomMessageBox.Buttons.OK, CustomMessageBox.Icon.Warning);
                Session.CurrentEmployee = null;

                frmLogin.ShowDialog();

                if (EmployeeFunctions.TechnicalSupport(Session._LocationCode, Session.CurrentEmployee.LoginDetail.EmployeeCode))
                {
                    blnClockedIn = true;
                }
                else
                {

                    blnClockedIn = Convert.ToBoolean(EmployeeClockIn(Session._LocationCode, Session.SystemDate.ToString("yyyy-MM-dd"), Session.CurrentEmployee.LoginDetail.EmployeeCode));
                }

            }
            if (blnBeingModified)
            {
                if (!string.IsNullOrEmpty(Session.CurrentEmployee.LoginDetail.EmployeeCode) && Session.CurrentEmployee.LoginDetail.blnAllowOrdersBeingModified)
                {
                    blnLoginSuccessful = true;
                }
                else
                {
                    oldLoginEmployee = Session.CurrentEmployee.LoginDetail;
                    frmLogin.RequireSpecialAccess = true;
                    frmLogin.SpecialAccess = true;
                    frmLogin.Text = APILayer.GetCatalogText(LanguageConstant.cintSpecialAccess);
                    frmLogin.RequirePassword = true;
                    frmLogin.ShowDialog();


                    
                    if (!string.IsNullOrEmpty(Session.CurrentEmployee.LoginDetail.EmployeeCode)  && Session.CurrentEmployee.LoginDetail.blnAllowOrdersBeingModified)
                    {
                        blnLoginSuccessful = true;
                    }
                    else
                    {
                        blnLoginSuccessful = false; 
                    }
                    Session.CurrentEmployee.LoginDetail = oldLoginEmployee;

                    if (blnLoginSuccessful == false)
                    {
                        if (EmployeeFunctions.TechnicalSupport(Session._LocationCode, Session.CurrentEmployee.LoginDetail.EmployeeCode) == false)
                        {
                            CustomMessageBox.Show(APILayer.GetCatalogText(LanguageConstant.cintMSGInsufficientPrivileges), CustomMessageBox.Buttons.OK, CustomMessageBox.Icon.Warning);
                            Session.pblnModifyingOrder = false;
                            return;
                        }

                    }

                }
            }
            if (Printing_Order_Number != 0)
            {
                   PrintFunctions.PrintReceipt(Printing_Order_Number, Printing_Order_date,true,false);
                //Commented by Vikas Saraswat
                //    oldLoginEmployee = Session.CurrentEmployee.LoginDetail;
                //    frmLogin.Text = APILayer.GetCatalogText(LanguageConstant.cintUserID);
                //    frmLogin.ShowDialog();
                //    Session.pblnModifyingOrder = false;
                //    gridmodify.ClearSelection();
                //   cmdPrintOnDemand.BackColor = DefaultBackColor;
                ////Session.CurrentEmployee.LoginDetail = oldLoginEmployee;


                ////UserFunctions.GoToStartup(this);

                //Added by Vikas Saraswat
                logout = true;
                this.Close();
            }
            // ResetOrder TO DO

        }
        public void ModifyOrder(long Order_number, DateTime Order_date, Boolean blnBeingModified, string phonenumber)
        {

            bool blnClockedIn = false;
            frmLogin frmLogin1 = new frmLogin();
            EmployeeResult oldLoginEmployee;
            bool blnLoginSuccessful;

            if (EmployeeFunctions.TechnicalSupport(Session._LocationCode, Session.CurrentEmployee.LoginDetail.EmployeeCode))
                blnClockedIn = true;
            else

                blnClockedIn = Convert.ToBoolean(EmployeeClockIn(Session._LocationCode, Session.SystemDate.ToString("yyyy-MM-dd"), Session.CurrentEmployee.LoginDetail.EmployeeCode));

            if (!blnClockedIn)
            {
                MessageBox.Show(APILayer.GetCatalogText(LanguageConstant.cintMSGIDNoLongerClockedIn), "OMS", MessageBoxButtons.OK);

                oldLoginEmployee = Session.CurrentEmployee;
                frmLogin1.RequireSpecialAccess = true;
                frmLogin1.SpecialAccess = true;
                frmLogin1.Text = APILayer.GetCatalogText(LanguageConstant.cintSpecialAccess);
                frmLogin1.RequirePassword = true;
                frmLogin1.ShowDialog();

                if (Session.CurrentEmployee != null && Session.CurrentEmployee.LoginDetail != null)
                {
                    if (EmployeeFunctions.TechnicalSupport(Session._LocationCode, Session.CurrentEmployee.LoginDetail.EmployeeCode))
                        blnClockedIn = true;
                    else
                        // frmLogin1.ClockInUser();
                        blnClockedIn = Convert.ToBoolean(EmployeeClockIn(Session._LocationCode, Session.SystemDate.ToString("yyyy-MM-dd"), Session.CurrentEmployee.LoginDetail.EmployeeCode));
                }

                Session.CurrentEmployee = oldLoginEmployee;
            }
            if (blnBeingModified)
            {
                blnLoginSuccessful = false;

                if (!string.IsNullOrEmpty(Session.CurrentEmployee.LoginDetail.EmployeeCode) && Session.CurrentEmployee.LoginDetail.blnAllowOrdersBeingModified)
                {
                    blnLoginSuccessful = true;
                }
                else
                {
                    oldLoginEmployee = Session.CurrentEmployee;
                    frmLogin1.RequireSpecialAccess = true;
                    frmLogin1.SpecialAccess = true;
                    frmLogin1.Text = APILayer.GetCatalogText(LanguageConstant.cintSpecialAccess);
                    frmLogin1.RequirePassword = true;
                    frmLogin1.ShowDialog();

                    if (Session.CurrentEmployee != null && Session.CurrentEmployee.LoginDetail != null)
                    {
                        if (!string.IsNullOrEmpty(Session.CurrentEmployee.LoginDetail.EmployeeCode) && Session.CurrentEmployee.LoginDetail.blnAllowOrdersBeingModified)
                            blnLoginSuccessful = true;
                        else
                            blnLoginSuccessful = false;
                    }
                    else
                        blnLoginSuccessful = false;

                    if (blnLoginSuccessful == false)
                    {
                        bool technicalSupport = EmployeeFunctions.TechnicalSupport(Session._LocationCode, Session.CurrentEmployee.LoginDetail.EmployeeCode);
                        if (technicalSupport == false)
                        {
                            CustomMessageBox.Show(APILayer.GetCatalogText(LanguageConstant.cintMSGInsufficientPrivileges), CustomMessageBox.Buttons.OK, CustomMessageBox.Icon.Warning);
                            Session.pblnModifyingOrder = false;
                            return;
                        }
                        else
                        {
                            CustomMessageBox.Show(APILayer.GetCatalogText(LanguageConstant.cintMSGIDNoLongerClockedIn), CustomMessageBox.Buttons.OK, CustomMessageBox.Icon.Warning);
                            Session.pblnModifyingOrder = false;
                            return;
                        }

                    }

                    Session.CurrentEmployee = oldLoginEmployee;

                }
            }
            OrderFunctions.LoadOrderDetails(Order_number, Order_date, true, false, false, false, phonenumber);
            if (Session.pblnModifyingOrder == true)
                this.Close();

        }
        public void CloseOrder(long Order_number, DateTime Order_date, Boolean blnBeingModified, string phonenumber)
        {
            try
            {
                EmployeeResult oldLoginEmployee;
                bool blnClockedIn = false;
                frmLogin frmLogin = new frmLogin();
                bool ctlModifyCashOut = false;
                bool blnLoginSuccessful;
                //BAL bAL = new BAL();
                decimal curAmountTendered = 0;
                OriginalOrderInfo originalOrderInfos = new OriginalOrderInfo();
                originalOrderInfos = APILayer.LoadOriginalOrderInfo(SystemSettings.LocationCodes.LocationCode, Order_date, Order_number, false);
                if (EmployeeFunctions.TechnicalSupport(Session._LocationCode, Session.CurrentEmployee.LoginDetail.EmployeeCode))
                    blnClockedIn = true;
                else
                    
                    blnClockedIn = Convert.ToBoolean(EmployeeClockIn(Session._LocationCode, Session.SystemDate.ToString("yyyy-MM-dd"), Session.CurrentEmployee.LoginDetail.EmployeeCode));
                if (!blnClockedIn)
                    ctlModifyCashOut = true;
                else
                
                if (blnBeingModified)
                {
                    if (!string.IsNullOrEmpty(Session.CurrentEmployee.LoginDetail.EmployeeCode) && Session.CurrentEmployee.LoginDetail.blnAllowOrdersBeingModified)
                    {
                        blnLoginSuccessful = true;
                    }
                    else
                    {
                        oldLoginEmployee = Session.CurrentEmployee;
                        frmLogin.RequireSpecialAccess = true;
                        frmLogin.SpecialAccess = true;
                        frmLogin.Text = APILayer.GetCatalogText(LanguageConstant.cintSpecialAccess);
                        frmLogin.RequirePassword = true;
                        frmLogin.ShowDialog();

                        Session.CurrentEmployee = oldLoginEmployee;
                    }
                    if (!string.IsNullOrEmpty(Session.CurrentEmployee.LoginDetail.EmployeeCode))
                    {
                        blnLoginSuccessful = true;
                    }
                    else
                    {
                        blnLoginSuccessful = false; 
                    }
                    if (blnLoginSuccessful == false)
                    {
                        if (EmployeeFunctions.TechnicalSupport(Session._LocationCode, Session.CurrentEmployee.LoginDetail.EmployeeCode) == false)
                        {
                            CustomMessageBox.Show(APILayer.GetCatalogText(LanguageConstant.cintMSGInsufficientPrivileges), CustomMessageBox.Buttons.OK, CustomMessageBox.Icon.Warning);
                            Session.pblnModifyingOrder = false;
                            return;
                        }

                    }
                }

                if (originalOrderInfos.cartHeader.Order_Type_Code == "D")
                {
                    CustomMessageBox.Show(APILayer.GetCatalogText(LanguageConstant.cintMSGCannotCloseDeliveryOrder), CustomMessageBox.Buttons.OK, CustomMessageBox.Icon.Warning);

                }

                foreach (OrderPayment orderPayment in originalOrderInfos.orderPayments)
                {
                    if (orderPayment.Deleted == false)
                    {
                        curAmountTendered += orderPayment.Amount_Tendered;
                    }
                }
                if (curAmountTendered < originalOrderInfos.cartHeader.Final_Total)
                {
                    CustomMessageBox.Show(APILayer.GetCatalogText(LanguageConstant.cintMSGCannotCloseUnpaidOrder), CustomMessageBox.Buttons.OK, CustomMessageBox.Icon.Warning);
                    Session.pblnModifyingOrder = false;
                    return;
                }
                else
                {
                    if (originalOrderInfos.cartHeader.Order_Status_Code == 1)
                    {
                      CustomMessageBox.Show(APILayer.GetCatalogText(LanguageConstant.cintMSGCannotCloseUnmadeOrder), CustomMessageBox.Buttons.OK, CustomMessageBox.Icon.Warning);
                        Session.pblnModifyingOrder = false;
                        return;

                    }
                    else
                    {
                       
                        //int result = bAL.UpdateClosedOrderTime(SystemSettings.LocationCodes.LocationCode, Order_date, Order_number, true, true);
                        int result = APILayer.UpdateClosedOrderTime(SystemSettings.LocationCodes.LocationCode, Order_date, Order_number, true, true);
                        if (result == 1)
                        {
                            CustomMessageBox.Show("Update Order Closed Time Successfully", CustomMessageBox.Buttons.OK, CustomMessageBox.Icon.Warning);
                        }
                    }
                }

                Session.pblnModifyingOrder = false;

                // ResetOrder TO DO

                //frmLogin frm = new frmLogin();
                //frm.SpecialAccess = true;
                //frm.Text = APILayer.GetCatalogText(LanguageConstant.cintSpecialAccess);
                //frm.RequirePassword = true;
                //frm.ShowDialog();

                ///frmLogin.ShowDialog();
                //var openFormCustomer = Application.OpenForms.Cast<Form>().Where(x => x.Text.ToUpper() == "CUSTOMER").FirstOrDefault();
                //if (openFormCustomer != null)
                //{
                //    openFormCustomer.Hide();

                //}


            }
            catch (Exception ex)
            {
                Logger.Trace("ERROR", "frmModify-closeorder(): " + ex.Message, ex, true);
                CustomMessageBox.Show(ex.StackTrace);
            }
        }

        //public static decimal GetFinalTotalforPayment(string Location_Code , DateTime Order_Date , long Order_Number )
        //{ BAL bAL = new BAL();
        //    decimal GetFinalTotalforPayment = 0;

        //    DataTable dt = bAL.GetOrderFinalValue(Location_Code, Order_Date, Order_Number);
        //    if(dt.Rows.Count>0)
        //    {
        //        GetFinalTotalforPayment = Convert.ToDecimal(dt.Rows[0][0]);
        //    }
        //    return GetFinalTotalforPayment;

        //}

        private void cmdNoSale_Click(object sender, EventArgs e)
        {
            CustomMessageBox.Show("There is not a cash Drawer attached", CustomMessageBox.Buttons.OK, CustomMessageBox.Icon.Warning);
        }
        private void getDeviceSettings()
        {
            var DeviceMappings = Settings.Settings.GetDeviceSetting(Session._LocationCode, Convert.ToInt32(SystemSettings.WorkStationSettings.plngWorkstation_ID));
            if (DeviceMappings == null)
            {
                cmdPrintOnDemand.Visible = false;
            }
            else
            {
                for (int i = 0; i < DeviceMappings.Count; i++)
                {
                    int device_code = Convert.ToInt32(DeviceMappings[i].Device_Type_Code);
                    if (device_code == 2)
                    {
                        cmdPrintOnDemand.Visible = true;
                        break;
                    }
                    else
                    {
                        cmdPrintOnDemand.Visible = false;
                    }
                }
            }
        }

        public int EmployeeClockIn(string Location_Code, string Systemdatetime, string Employee_Code)
        {
            int result = 0;
            try
            {
                CheckEmployeeRequest checkEmployeeRequest = new CheckEmployeeRequest();
                checkEmployeeRequest.LocationCode = Location_Code;
                checkEmployeeRequest.SystemDate = Convert.ToDateTime(Systemdatetime);
                checkEmployeeRequest.UserId = Employee_Code;
                checkEmployeeRequest.PositionCode = "";
                result = APILayer.EmployeeClockIn(checkEmployeeRequest);
            }
            catch (Exception ex)
            {
                Logger.Trace("ERROR", "frmModify-EmployeeClockIn(): " + ex.Message, ex, true);
                CustomMessageBox.Show(ex.StackTrace);
            }
            return result;
        }

        private void LoadSerial(DataGridView grid)
        {
            foreach (DataGridViewRow row in grid.Rows)
            {
                grid.Rows[row.Index].HeaderCell.Value = string.Format("{0}", row.Index + 1).ToString();
               
            }
        }

        private void gridmodify_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            try
            {
                LoadSerial(gridmodify);
            }
            catch
            {

            }
        }

        private void frmModify_FormClosing(object sender, FormClosingEventArgs e)
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
                    ALT_F4 = false;
                    e.Cancel = true;
                    return;
                }
            }
            catch (Exception ex)
            {
                Logger.Trace("ERROR", ex.Message, ex, true);
            }
        }

        private void frmModify_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                ALT_F4 = (e.KeyCode.Equals(Keys.F4) && e.Alt == true);                
            }
            catch (Exception ex)
            {
                Logger.Trace("ERROR", ex.Message, ex, true);
            }
        }
        public bool PrntingtimeValedation()
        {
            bool Restult;
            int SettingValue = Convert.ToInt32(SystemSettings.GetSettingValue("Printtimediff", Session._LocationCode));

             TimeSpan ts= (DateTime.Now-OrderTime);
           int Minutes = Convert.ToInt32(ts.TotalMinutes);
            if (SettingValue > Minutes)
                Restult = false;
            else
                Restult= true;

            return Restult;
        }
    }
}
