using Jublfood.AppLogger;
using JublFood.POS.App.API;
using JublFood.POS.App.BusinessLayer;
using JublFood.POS.App.Cache;
using JublFood.POS.App.Class;
using JublFood.POS.App.Models.Cart;
using JublFood.POS.App.Models.Catalog;
using JublFood.POS.App.Models.Order;
using JublFood.POS.App.Models.Payment;
using JublFood.POS.App.Models.Cart;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
//using System.Threading;
using System.Windows.Forms;

namespace JublFood.POS.App
{
    public partial class frmPayment : Form
    {
        private bool ALT_F4 = false;
        List<CatalogCurrencyDenomination> CurrencyDenomination;

        string numberRole = "MONEY"; //DIGITS

        int IntegrationExeTimeOut, i = 0, x = 0, timerts = 60;
        int CashDrawerTimeOut = Session.CashDrawerTimeOut;
        System.Windows.Forms.Timer Timer = new System.Windows.Forms.Timer();
        System.Windows.Forms.Timer CDTimer = new System.Windows.Forms.Timer();
        int OrderPaytypeCode, TransactionID;
        bool isCashDrawerClose = false;

        string buttonName = string.Empty;
        int credit_card_Response = 0;
        string removecardResponse = string.Empty;

        int paymentLineNumber = 1;
        int paymentSequence = 1;
        int paidPayment = 0;

        decimal curAmountDue = 0;
        decimal curAmountTendered = 0;
        decimal curAppliedPayments = 0;
        decimal curChangeDue = 0;

        bool panelNumricPadEnabled = false;
        bool pnl_amountEnabled = false;
        bool btnDeleteEnabled = false;
        bool tdbnAmountTenderedEnabled = false;
        bool btnOKEnabled = false;
        bool flPanel_CardsWalletEnabled = false;
        bool flPanel_CardsWalletVisible = false;


        CatalogOrderPayTypeCodes orderPayType;

        public frmPayment()
        {
            InitializeComponent();
            uc_KeyBoardNumeric.txtUserID = tdbnAmountTendered;
            uc_KeyBoardNumeric.ChangeButtonColor(DefaultBackColor);

            //if (Session.responsePayment == null)
            //{
            //    Session.responsePayment = new ResponsePayment();
            //}
            if (Session.cart.orderPayments == null)
            {
                Session.cart.orderPayments = new List<OrderPayment>();
            }

        }

        private void LoadCurrencyDenominations()
        {
            //CurrencyDenomination = APILayer.GetCurrencyDenomination(Session._LocationCode, SystemSettings.appControl.DefaultCurrency);
            CurrencyDenomination = new List<CatalogCurrencyDenomination>();
            CatalogCurrencyDenomination catalogCurrencyDenomination = new CatalogCurrencyDenomination();
            catalogCurrencyDenomination.Bill_Code = Constants.ExactButtonText;
            catalogCurrencyDenomination.Order_Description = Constants.ExactButtonText;

            CurrencyDenomination.Add(catalogCurrencyDenomination);
            CurrencyDenomination.AddRange(APILayer.GetCurrencyDenomination(Session._LocationCode, SystemSettings.appControl.DefaultCurrency));

            foreach (CatalogCurrencyDenomination denomination in CurrencyDenomination)
            {

                Button button = new Button();
                button.Click += new EventHandler(btn_Exact_Click);
                //button.Location = new System.Drawing.Point(Constants.HorizontalSpace, Constants.VerticalSpace);
                button.Font = new System.Drawing.Font("Microsoft Sans Serif", 15, FontStyle.Bold);
                button.Height = 62;
                button.Width = 76;
                button.Margin = new Padding(0);
                button.BackColor = System.Drawing.Color.FromArgb(240, 240, 240);
                button.Tag = denomination.Bill_Code;
                button.Name = "btn" + denomination.Order_Description;
                button.Text = denomination.Order_Description;
                pnl_amount.Controls.Add(button);

            }

        }

        private void cmdKeyboard_Click(object sender, EventArgs e)
        {
            //UserFunctions.GoToStartup(this);
            using (frmKeyBoard objfrmKeyBoard = new frmKeyBoard(txtCheckInfo, "Cheque Information"))
            {
                objfrmKeyBoard.ShowDialog();
            }
        }

        private void OnButtonClick(object sender, EventArgs e)
        {

            try
            {
                if (!CheckValidations())
                    return;

                Button btn = (Button)sender;
                orderPayType = new CatalogOrderPayTypeCodes();
                orderPayType = Session.orderPayTypeCodes.Find(x => x.Order_Pay_Type_Code == Convert.ToInt32(btn.Tag));
                SetButtonColorDigital(btn);
                if (!string.IsNullOrEmpty(orderPayType.Vendor_Integration_Status) && orderPayType.Vendor_Integration_Status == "1")
                {
                    CreditCardTrackingRequest creditCardTrackingRequest = new CreditCardTrackingRequest();
                    creditCardTrackingRequest.cartHeader = Session.cart.cartHeader;
                    creditCardTrackingRequest.Customer = Session.cart.Customer;
                    creditCardTrackingRequest.Order_Pay_Type_Code = orderPayType.Order_Pay_Type_Code;
                    creditCardTrackingRequest.Amount_Tendered = Convert.ToDecimal(tdbnAmountTendered.Text);
                    creditCardTrackingRequest.Credit_Card_Code = orderPayType.Credit_Card_Code;
                    creditCardTrackingRequest.Credit_Card_ID = orderPayType.Credit_Card_ID;
                    creditCardTrackingRequest.Added_by = Session.CurrentEmployee.LoginDetail.EmployeeCode;

                    TransactionID = APILayer.InsertCreditCardTransaction(creditCardTrackingRequest);
                    if (TransactionID == 0)
                    {
                        CustomMessageBox.Show(MessageConstant.PayModeNotworking, CustomMessageBox.Buttons.OK, CustomMessageBox.Icon.Warning);
                        return;
                    }
                    IntegrationExeTimeOut = orderPayType.Vendor_Time_Out;
                    CallIntegration(TransactionID, orderPayType.Order_Pay_Type_Code, orderPayType.Vendor_Exe_Name);

                }
                else
                {
                    btnOK_Click(new object(), new EventArgs());

                }

            }
            catch (Exception ex)
            {
                Logger.Trace("ERROR", "frmPayment-OnButtonClick(): " + ex.Message, ex, true);
            }

        }


        private void btn_Exact_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(tdbnAmountTendered.Text))
                    tdbnAmountTendered.Text = "0";

                Button btn = (Button)sender;
                if (btn.Text == "Exact")
                    tdbnAmountTendered.Text = String.Format(Session.DisplayFormat, Convert.ToDecimal(tlblAmountDue.Text));
                else
                    tdbnAmountTendered.Text = String.Format(Session.DisplayFormat, Convert.ToDecimal(tdbnAmountTendered.Text) + Convert.ToInt32(btn.Text));

                if (Convert.ToDecimal(tlblAmountDue.Text) > 0)
                    btnOK.Enabled = true;
            }
            catch (Exception ex)
            {
                Logger.Trace("ERROR", "frmPayment-btn_exact_click(): " + ex.Message, ex, true);
            }
        }
        private void btn_Denomination_Click(object sender, EventArgs e)
        {

        }

        private void frmPayment_Load(object sender, EventArgs e)
        {
            try
            {
                this.Location = new Point(((Screen.PrimaryScreen.Bounds.Width - this.Size.Width) / 2) + 5, ((Screen.PrimaryScreen.Bounds.Height - this.Size.Height) / 2));
                CheckTrainningMode();
                PaymentFunctions.LoadOrderPayTypes();

                cmdCash.Text = APILayer.GetCatalogText(LanguageConstant.cintCash);
                cmdCheque.Text = APILayer.GetCatalogText(LanguageConstant.cintCheck);
                cmdDigital.Text = APILayer.GetCatalogText(LanguageConstant.cintCreditCard);

                if (SystemSettings.GetSettingValue("RequireSudexoVoucherPay", Session._LocationCode) == "1")
                {
                    cmdSodexo.Visible = true;
                    cmdSodexo.Text = APILayer.GetCatalogText(LanguageConstant.cintSudexo);
                }
                if (SystemSettings.GetSettingValue("RequireAccorVoucherPay", Session._LocationCode) == "1")
                {
                    cmdAccor.Visible = true;
                    cmdAccor.Text = APILayer.GetCatalogText(LanguageConstant.cintAccor);
                }

                LoadCurrencyDenominations();
                RefreshPaymentGrid();

                if (curAmountDue <= 0)
                {
                    btnOK.Enabled = false;
                }


                // one customer
                UserFunctions.OpenOneCustomer(Session.cart.Customer.Phone_Number + " " + 3);
            }
            catch (Exception ex)
            {
                Logger.Trace("ERROR", "frmPayment-frmPayment_load(): " + ex.Message, ex, true);
            }

        }

        private void GeneratePayment(bool defaultPay = false)
        {
            OrderPayment orderPayment = new OrderPayment();
            int orderPaymentPaid = 0;
            try
            {
                orderPayment = PaymentFunctions.GetPayment();

                if (/*Session.responsePayment != null &&*/ Session.cart.orderPayments != null && Session.cart.orderPayments.Count > 0)
                {
                    paymentLineNumber = Session.cart.orderPayments.Count + 1;
                    paymentSequence = Session.cart.orderPayments[0].Sequence;
                }
                else
                {
                    paymentLineNumber = 1;
                    paymentSequence = 1;
                }
                if (Session.IsPayClick == true)
                    orderPaymentPaid = 1;
                else
                    orderPaymentPaid = 0;

                if (defaultPay)
                {
                    orderPayType = new CatalogOrderPayTypeCodes();
                    orderPayType = Session.orderPayTypeCodes.Find(x => x.Credit_Card_Code == Convert.ToInt32(cmdCash.Tag) && x.Order_Pay_Type_Code == 1);
                    orderPaymentPaid = 0;
                    orderPayment.Amount_Tendered = Convert.ToDecimal(tlblAmountDue.Text);
                }
                else
                    orderPayment.Amount_Tendered = Convert.ToDecimal(tdbnAmountTendered.Text);

                orderPayment.Payment_Line_Number = paymentLineNumber;
                orderPayment.Sequence = paymentSequence;


                if (orderPayment.Amount_Tendered < Convert.ToDecimal(tlblAmountDue.Text))
                    orderPayment.Payment_Amount = orderPayment.Amount_Tendered;
                else
                    orderPayment.Payment_Amount = Convert.ToDecimal(tlblAmountDue.Text);

                if (Convert.ToDecimal(tdbnAmountTendered.Text) > Convert.ToDecimal(tlblAmountDue.Text))
                {
                    orderPayment.Change_Due = Convert.ToDecimal((Convert.ToDecimal(tdbnAmountTendered.Text) - Convert.ToDecimal(tlblAmountDue.Text)));
                }

                orderPayment.Currency_Amount = orderPayment.Payment_Amount;
                orderPayment.Currency_Code = SystemSettings.appControl.DefaultCurrency;
                orderPayment.CashOut_Time = DateTime.Now;

                orderPayment.Order_Pay_Type_Code = orderPayType.Order_Pay_Type_Code;
                orderPayment.Credit_Card_Code = orderPayType.Credit_Card_Code;

                orderPayment.Paid = orderPaymentPaid;

                orderPayment.Data_Changed = true;
                orderPayment.NewPayment = true;
                orderPayment.Data_Processed = false;

                if (orderPayType.Credit_Card_Code == 2)
                {
                    orderPayment.Check_Info = Convert.ToString(txtCheckInfo.Text);
                }

                if (orderPayType.Credit_Card_Code == 4)
                {
                    orderPayment.CreditCardID = orderPayType.Credit_Card_ID;
                    orderPayment.CreditCardAmount = orderPayment.Amount_Tendered;
                    orderPayment.CreditCardDescription = orderPayType.Credit_Card_Default_Description;
                    orderPayment.CreditCardID = orderPayType.Credit_Card_ID;
                    orderPayment.Tip = 0;
                    orderPayment.Transaction_ID = TransactionID;
                    orderPayment.Check_Info = Convert.ToString(TransactionID);

                }

                if (Session.cart.orderPayments == null)
                {
                    Session.cart.orderPayments = new List<OrderPayment>();
                }


                //Session.cart.orderPayments.Add(orderPayment);
                Logger.Trace("INFO", "S-2 GeneratePayment:  payment collection created "
                    + orderPayType.Order_Pay_Type_Code + " Check_Info " + orderPayment.Check_Info + " amt :" + orderPayment.Amount_Tendered, null, false);
                if (orderPayType.Credit_Card_Code == 4)
                {
                    Logger.Trace("INFO", "S-3 GeneratePayment:  before credit card creation "
                    + orderPayType.Order_Pay_Type_Code + " Check_Info " + orderPayment.Check_Info + " amt :" + orderPayment.Amount_Tendered, null, false);
                    GenerateCreditCardPayment(orderPayment);
                }
                else
                {
                    Logger.Trace("INFO", "S-4 GeneratePayment: before send payment API "
                    + orderPayType.Order_Pay_Type_Code + " Check_Info " + orderPayment.Check_Info + " amt :" + orderPayment.Amount_Tendered, null, false);
                    PaymentFunctions.SendPaymentAPI(orderPayment, null);
                }


            }
            catch (Exception ex)
            {
                Logger.Trace("ERROR", "frmPayment-generatePayment(): " + ex.Message, ex, true);

                throw;
            }
        }


        public void GenerateCreditCardPayment(OrderPayment orderPayment)
        {
            try
            {
                OrderCreditCard orderCreditCard = new OrderCreditCard();
                orderCreditCard = PaymentFunctions.GetCreditCardPayment();

                //OrderPayment orderPayment = new OrderPayment();
                //orderPayment = Session.cart.orderPayments.Find(x => x.Payment_Line_Number == paymentLineNumber);
                orderCreditCard.CartId = Session.cart.cartHeader.CartId;
                orderCreditCard.Location_Code = orderPayment.Location_Code;
                orderCreditCard.Order_Date = orderPayment.Order_Date;
                orderCreditCard.Order_Number = orderPayment.Order_Number;
                orderCreditCard.Payment_Line_Number = orderPayment.Payment_Line_Number;
                Random r = new Random();
                orderCreditCard.Transaction_Number = Convert.ToString((-1) * (9999 * r.Next()));
                //orderCreditCard.Transaction_Number = Convert.ToString(-1 * (Convert.ToInt32(((9999 * r.Next())) + 1) & Math.Abs(orderPayment.Order_Number)));
                orderCreditCard.Credit_Card_Transaction_Type = 15;
                orderCreditCard.Credit_Card_ID = Convert.ToString(orderPayment.CreditCardID);//
                orderCreditCard.Credit_Card_Amount = orderPayment.Amount_Tendered;
                orderCreditCard.Credit_Card_Account = orderPayment.CardNumber;
                //orderCreditCard.Credit_Card_Expiration = orderPayment.CardExpiration.Substring(orderPayment.CardExpiration.Length - 1, 2) + orderPayment.CardExpiration.Substring(0, 2);

                orderCreditCard.Name_On_Card = orderPayment.NameOnCard;
                orderCreditCard.AVS_Street = orderPayment.AVSStreet;
                orderCreditCard.Postal_Code = orderPayment.PostalCode;
                orderCreditCard.CVV2 = orderPayment.CVV2;
                orderCreditCard.Credit_Card_Track_1_Data = orderPayment.Track1Data;
                orderCreditCard.Credit_Card_Track_2_Data = orderPayment.Track2Data;
                orderCreditCard.Credit_Card_Tip = orderPayment.Tip;

                if (Session.cart.orderCreditCards == null)
                {
                    Session.cart.orderCreditCards = new List<OrderCreditCard>();
                }
                PaymentFunctions.SendPaymentAPI(orderPayment, orderCreditCard);
                //Session.cart.orderCreditCards.Add(orderCreditCard);
            }
            catch (Exception ex)
            {
                Logger.Trace("ERROR", "frmPayment-generateCreditcardPayment(): " + ex.Message, ex, true);
                throw;
            }
        }


        private void gridPayment_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                gridPayment.Rows[gridPayment.CurrentRow.Index].Selected = true;
                if (gridPayment.CurrentCell.ColumnIndex == 0 || gridPayment.CurrentCell.ColumnIndex == 1)
                {
                    string currIsPayment = Convert.ToString(gridPayment.Rows[gridPayment.CurrentRow.Index].Cells["IsPayment"].Value);
                    string currOrderPayTypeCode = Convert.ToString(gridPayment.Rows[gridPayment.CurrentRow.Index].Cells["OrderPayType"].Value);
                    if (currIsPayment == "1")
                    {

                        string currAmount = Convert.ToString(gridPayment.Rows[gridPayment.CurrentRow.Index].Cells["Value"].Value);
                        int currPaymentLineNumber = Convert.ToInt32(gridPayment.Rows[gridPayment.CurrentRow.Index].Cells["Payment_Line_Number"].Value);
                        OrderPayment orderPayment = new OrderPayment();
                        orderPayment = Session.cart.orderPayments.Find(x => x.Payment_Line_Number == currPaymentLineNumber);
                        NavigatePaymentControl(Convert.ToInt32(currOrderPayTypeCode), Convert.ToDecimal(currAmount), false, orderPayment.Check_Info);
                        if (currOrderPayTypeCode != "1")
                            tdbnAmountTendered.Enabled = false;
                    }
                    else
                    {
                        tdbnAmountTendered.Enabled = false;
                    }

                }
            }
            catch (Exception ex)
            {
                Logger.Trace("ERROR", "frmPayment-gridPayment_cellclick(): " + ex.Message, ex, true);
            }
        }

        private void NavigatePaymentControl(int OrderPayTypeCode, decimal TenderedAmount, bool NewPayment, string CheckInfo)
        {
            switch (OrderPayTypeCode)
            {
                case 1:
                    ManageCashPayment(NewPayment, TenderedAmount);
                    break;
                case 2:
                    ManageChequePayment(NewPayment, Convert.ToDecimal(TenderedAmount), CheckInfo);
                    break;
                case 3:
                    ManageDigitalPayment(NewPayment, Convert.ToDecimal(TenderedAmount));
                    break;
                case 11:
                    ManageSodexoPayment(NewPayment, Convert.ToDecimal(TenderedAmount));
                    break;
                case 28:
                    ManageAccorPayment(NewPayment, Convert.ToDecimal(TenderedAmount));
                    break;
                default:
                    ManageDigitalPayment(NewPayment, Convert.ToDecimal(TenderedAmount));
                    break;
            }
        }

        public bool CheckValidations()
        {
            try
            {
                if (tdbnAmountTendered.Text.Length == 0)
                {
                    CustomMessageBox.Show(MessageConstant.InvalidAmount, CustomMessageBox.Buttons.OK, CustomMessageBox.Icon.Warning);
                    return false;
                }
                if ((Convert.ToDecimal(tlblAmountDue.Text) > 0) && (Convert.ToDecimal(tdbnAmountTendered.Text) == 0))
                {
                    CustomMessageBox.Show(MessageConstant.InvalidAmount, CustomMessageBox.Buttons.OK, CustomMessageBox.Icon.Warning);
                    return false;
                }
                if (Convert.ToDecimal(tdbnAmountTendered.Text) <= 0)
                {
                    CustomMessageBox.Show(MessageConstant.InvalidAmount, CustomMessageBox.Buttons.OK, CustomMessageBox.Icon.Warning);
                    return false;
                }
                if (orderPayType.Order_Pay_Type_Code != 1)
                {
                    if (Convert.ToDecimal(tlblAmountDue.Text) < Convert.ToDecimal(tdbnAmountTendered.Text))
                    {
                        CustomMessageBox.Show(MessageConstant.InvalidAmount, CustomMessageBox.Buttons.OK, CustomMessageBox.Icon.Warning);
                        return false;
                    }
                }


            }
            catch (Exception ex)
            {
                Logger.Trace("ERROR", "frmPayment-checkvalidations(): " + ex.Message, ex, true);
                throw;
                //return false;
            }
            return true;

        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            decimal tmpAmountTendered = 0;
            tmpAmountTendered = Convert.ToDecimal(tdbnAmountTendered.Text);
            try
            {
                if (!CheckValidations())
                    return;

                GeneratePayment(false);
                RefreshPaymentGrid();
                //cash drawer
                
                if (Session.IsPayClick && orderPayType.CashRegAllow)
                {
                    if (PaymentFunctions.CashDrawerFlag(tmpAmountTendered))
                    {
                        //open cash drawer
                        isCashDrawerClose = false;
                        CashDrawerTimeOut = Session.CashDrawerTimeOut;
                        CashDrawerProcess();
                    }
                    else
                    {
                        if (curAmountDue <= 0)
                            PaymentFunctions.PaymentComplete();
                    }
                }
                else
                {

                    if (curAmountDue <= 0)
                        PaymentFunctions.PaymentComplete();

                }
                //

            }
            catch (Exception ex)
            {
                Logger.Trace("ERROR", "frmPayment-btnOK_Click(): " + ex.Message, ex, true);
                CustomMessageBox.Show("btnOK_Click " + ex.Message, CustomMessageBox.Buttons.OK, CustomMessageBox.Icon.Error);

            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                //kill running exe (PineLabs.exe/Paytm.exe/QwikcilverWPOS.exe)
                //Process[] processes = Process.GetProcessesByName("HHTCtrlp");
                //foreach (var process in processes)
                //{
                //    process.Kill();
                //}



                if (curAmountTendered < Session.cart.cartHeader.Final_Total)
                {
                    DialogResult messageResult = CustomMessageBox.Show(APILayer.GetCatalogText(LanguageConstant.cintMSGStillAmtDue) + string.Format(Session.DisplayFormat, curAmountDue)
                        + "\n" + APILayer.GetCatalogText(LanguageConstant.cintMSGDoYouWishToContinue), CustomMessageBox.Buttons.OKCancel, CustomMessageBox.Icon.Info);
                    if (messageResult == DialogResult.Cancel)
                        return;
                    else
                    {
                        if (Session.cart.cartHeader.Order_Type_Code == "I")
                            Session.IsPayClick = true;
                        else
                            Session.IsPayClick = false;

                        Logger.Trace("INFO", "S-1 btnSave_Click : before payment generate", null, false);
                        GeneratePayment(true);
                    }


                }
                Logger.Trace("INFO", "S-1.1 btnSave_Click : PaymentComplete", null, false);
                PaymentFunctions.PaymentComplete();
            }
            catch (Exception ex)
            {
                Logger.Trace("ERROR", "frmPayment-btnSave_click(): " + ex.Message, ex, true);
            }

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                OrderPayment orderPayment = new OrderPayment();
                int currLineNumber = 0;
                string currOrderPayTypeCode = Convert.ToString(gridPayment.Rows[gridPayment.CurrentRow.Index].Cells["OrderPayType"].Value);
                decimal currAmount = Convert.ToDecimal(gridPayment.Rows[gridPayment.CurrentRow.Index].Cells["Value"].Value);
                string currIsPayment = Convert.ToString(gridPayment.Rows[gridPayment.CurrentRow.Index].Cells["IsPayment"].Value);

                if (currIsPayment == "1")
                {
                    currLineNumber = Convert.ToInt32(gridPayment.Rows[gridPayment.CurrentRow.Index].Cells["Payment_Line_Number"].Value);
                    orderPayment = Session.cart.orderPayments.Find(x => x.Payment_Line_Number == currLineNumber);
                    if (orderPayment != null)
                    {
                        if (orderPayment.NewPayment)
                        {
                            orderPayment.Action = "D";
                            PaymentFunctions.SendPaymentAPI(orderPayment, null);
                            //Session.cart.orderPayments.Remove(orderPayment);
                        }
                        else
                        {
                            orderPayment.Action = "M";
                            orderPayment.Deleted = true;
                            orderPayment.Data_Changed = true;
                            PaymentFunctions.SendPaymentAPI(orderPayment, null);
                        }
                        int rowindex = gridPayment.CurrentRow.Index;
                        gridPayment.Rows.RemoveAt(rowindex);
                        RefreshPaymentGrid();

                        gridPayment.ClearSelection();
                        int nRowIndex = gridPayment.Rows.Count - 1;
                        gridPayment.Rows[nRowIndex].Selected = true;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Trace("ERROR", "frmPayment-btndelete_click(): " + ex.Message, ex, true);
            }

        }
        private void tdbnAmountTendered_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (tdbnAmountTendered.Text.Length >= 10)
                return;

            if (((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8 && e.KeyChar != 46))
            {
                e.Handled = true;
                return;
            }

            // checks to make sure only 1 decimal is allowed
            if (e.KeyChar == 46)
            {
                if ((sender as TextBox).Text.IndexOf(e.KeyChar) != -1)
                    e.Handled = true;
            }
            if (e.KeyChar != 8 && e.KeyChar != 48 && e.KeyChar != 46)
            {
                MakeAmount(e.KeyChar.ToString());
                e.Handled = true;
            }
            if (e.KeyChar == 48)
            {
                btnNum_0_Click(null, null);
                e.Handled = true;
            }
        }
        private void RefreshPaymentGrid()
        {
            try
            {
                int nRowIndex = 0;
                if (Session.cart.orderUDT != null)
                {
                    Session.CatalogCartCaptions = APILayer.GetCartCaptions();

                    DataTable dtCartTotals = new DataTable();

                    dtCartTotals.Columns.Add("Name");
                    dtCartTotals.Columns.Add("Value");
                    dtCartTotals.Columns.Add("OrderPayType");
                    dtCartTotals.Columns.Add("Transaction_number");
                    dtCartTotals.Columns.Add("Credit_Card_Code");
                    dtCartTotals.Columns.Add("IsPayment");
                    dtCartTotals.Columns.Add("Payment_Line_Number");
                    dtCartTotals.Columns.Add("Payment_Sequence");

                    DataRow dr = dtCartTotals.NewRow();
                    dr["Name"] = Session.CatalogCartCaptions.SubTotal;
                    dr["Value"] = Convert.ToDecimal(String.Format(Session.DisplayFormat, Session.cart.cartHeader.SubTotal, 2));
                    dr["OrderPayType"] = "";
                    dr["Transaction_number"] = "";
                    dr["Credit_Card_Code"] = "";
                    dr["IsPayment"] = "";
                    dr["Payment_Line_Number"] = "";
                    dr["Payment_Sequence"] = "";
                    dtCartTotals.Rows.Add(dr);

                    //Doubles Pricing
                    if (SystemSettings.settings.pbytTaxStructure < 4)
                    {
                        dr = dtCartTotals.NewRow();
                        dr["Name"] = Session.CatalogCartCaptions.TaxLessthen4;
                        dr["Value"] = Convert.ToDecimal(Math.Round(Session.cart.orderUDT.Sales_Tax1, 2));
                        dr["OrderPayType"] = "";
                        dr["Transaction_number"] = "";
                        dr["Credit_Card_Code"] = "";
                        dr["IsPayment"] = "";
                        dr["Payment_Line_Number"] = "";
                        dr["Payment_Sequence"] = "";
                        dtCartTotals.Rows.Add(dr);

                        dr = dtCartTotals.NewRow();
                        dr["Name"] = Session.CatalogCartCaptions.Tax2Lessthen4;
                        dr["Value"] = Convert.ToDecimal(Math.Round(Session.cart.orderUDT.Sales_Tax2, 2));
                        dr["OrderPayType"] = "";
                        dr["Transaction_number"] = "";
                        dr["Credit_Card_Code"] = "";
                        dr["IsPayment"] = "";
                        dr["Payment_Line_Number"] = "";
                        dr["Payment_Sequence"] = "";
                        dtCartTotals.Rows.Add(dr);
                    }
                    else
                    {
                        if (SystemSettings.settings.pblnUseUserDefinedTax1)
                        {
                            dr = dtCartTotals.NewRow();
                            dr["Name"] = Session.CatalogCartCaptions.Tax1;
                            dr["Value"] = Convert.ToDecimal(String.Format(Session.DisplayFormat, Session.cart.orderUDT.Sales_Tax1));
                            dr["OrderPayType"] = "";
                            dr["Transaction_number"] = "";
                            dr["Credit_Card_Code"] = "";
                            dr["IsPayment"] = "";
                            dr["Payment_Line_Number"] = "";
                            dr["Payment_Sequence"] = "";
                            dtCartTotals.Rows.Add(dr);
                        }

                        if (SystemSettings.settings.pblnUseUserDefinedTax2)
                        {
                            dr = dtCartTotals.NewRow();
                            dr["Name"] = Session.CatalogCartCaptions.Tax2;
                            dr["Value"] = Convert.ToDecimal(String.Format(Session.DisplayFormat, Session.cart.orderUDT.Sales_Tax2));
                            dr["OrderPayType"] = "";
                            dr["Transaction_number"] = "";
                            dr["Credit_Card_Code"] = "";
                            dr["IsPayment"] = "";
                            dr["Payment_Line_Number"] = "";
                            dr["Payment_Sequence"] = "";
                            dtCartTotals.Rows.Add(dr);
                        }

                        if (SystemSettings.settings.pblnUseUserDefinedTax3)
                        {
                            dr = dtCartTotals.NewRow();
                            dr["Name"] = Session.CatalogCartCaptions.Tax3;
                            dr["Value"] = Convert.ToDecimal(String.Format(Session.DisplayFormat, Session.cart.orderUDT.Sales_Tax3));
                            dr["OrderPayType"] = "";
                            dr["Transaction_number"] = "";
                            dr["Credit_Card_Code"] = "";
                            dr["IsPayment"] = "";
                            dr["Payment_Line_Number"] = "";
                            dr["Payment_Sequence"] = "";
                            dtCartTotals.Rows.Add(dr);
                        }

                        if (SystemSettings.settings.pblnUseUserDefinedTax4)
                        {
                            dr = dtCartTotals.NewRow();
                            if (!Session.ODC_Tax)
                            {
                                dr["Name"] = Session.CatalogCartCaptions.Tax4;
                            }
                            else
                            {
                                dr["Name"] = SystemSettings.GetSettingValue("ODC_Change_Tax_Description", Session._LocationCode);
                            }
                            dr["Value"] = Convert.ToDecimal(String.Format(Session.DisplayFormat, Session.cart.orderUDT.Sales_Tax4));
                            dr["OrderPayType"] = "";
                            dr["Transaction_number"] = "";
                            dr["Credit_Card_Code"] = "";
                            dr["IsPayment"] = "";
                            dr["Payment_Line_Number"] = "";
                            dr["Payment_Sequence"] = "";
                            dtCartTotals.Rows.Add(dr);
                        }
                    }

                    //AggregatorGST
                    bool AggegatorGSTFromCart = false; 
                    if (Session.cart.orderAdditionalCharges != null && Session.cart.orderAdditionalCharges.Count > 0)
                    {
                        int ExemptChargeId = Convert.ToInt32(Convert.ToString(SystemSettings.GetSettingValue("AggregatorGSTChargeID", Session._LocationCode)));

                        for (int i = 0; i <= Session.cart.orderAdditionalCharges.Count - 1; i++)
                        {
                            if (Session.cart.orderAdditionalCharges[i].Charge_Id == ExemptChargeId)
                            {
                                dr = dtCartTotals.NewRow();
                                dr["Name"] = Session.cart.orderAdditionalCharges[i].ChargeDesc;
                                dr["Value"] = Convert.ToDecimal(String.Format(Session.DisplayFormat, Session.cart.orderAdditionalCharges[i].Amount));
                                dtCartTotals.Rows.Add(dr);

                                AggegatorGSTFromCart = true;
                            }
                            
                        }

                    }

                    if (!AggegatorGSTFromCart && Session.AggregatorGSTValue > 0)
                    {
                        dr = dtCartTotals.NewRow();
                        dr["Name"] = Constants.AggregatorText;
                        dr["Value"] = Convert.ToDecimal(String.Format(Session.DisplayFormat, Session.AggregatorGSTValue));
                        dtCartTotals.Rows.Add(dr);
                    }



                    dr = dtCartTotals.NewRow();
                    dr["Name"] = Session.CatalogCartCaptions.Total;
                    dr["Value"] = Convert.ToDecimal(String.Format(Session.DisplayFormat, Session.cart.cartHeader.Final_Total));
                    dtCartTotals.Rows.Add(dr);

                    curAmountTendered = 0;
                    curAppliedPayments = 0;
                    curChangeDue = 0;
                    OrderPayment firstOrderPayment = new OrderPayment();
                    bool firstPaymentCapture = false;

                    foreach (OrderPayment orderPayment in Session.cart.orderPayments)
                    {
                        if (orderPayment.Deleted == false)
                        {
                            dr = dtCartTotals.NewRow();
                            CatalogOrderPayTypeCodes TempCatalogOrderPayType = new CatalogOrderPayTypeCodes();
                            TempCatalogOrderPayType = Session.orderPayTypeCodes.Find(x => x.Order_Pay_Type_Code == orderPayment.Order_Pay_Type_Code);
                            dr["Name"] = TempCatalogOrderPayType.Order_Pay_English_Description;
                            dr["Value"] = Convert.ToDecimal(String.Format(Session.DisplayFormat, orderPayment.Amount_Tendered));
                            dr["OrderPayType"] = orderPayment.Order_Pay_Type_Code;
                            dr["Transaction_number"] = orderPayment.Check_Info;
                            dr["Credit_Card_Code"] = orderPayment.Credit_Card_Code;
                            dr["IsPayment"] = "1";
                            dr["Payment_Line_Number"] = orderPayment.Payment_Line_Number;
                            dr["Payment_Sequence"] = orderPayment.Sequence;
                            dtCartTotals.Rows.Add(dr);
                            curAmountTendered = curAmountTendered + orderPayment.Amount_Tendered;
                            if (Session.pblnModifyingOrder == true)
                            {
                                if (!firstPaymentCapture)
                                {
                                    firstOrderPayment = new OrderPayment();
                                    firstOrderPayment = orderPayment;
                                    nRowIndex = dtCartTotals.Rows.Count - 1;
                                    firstPaymentCapture = true;

                                }
                            }
                        }

                    }

                    dr = dtCartTotals.NewRow();
                    dtCartTotals.Rows.Add(dr);
                    gridPayment.DataSource = dtCartTotals;

                    if (curAmountTendered > 0)
                        btn_Order.Enabled = false;
                    else
                        btn_Order.Enabled = true;

                    if (curAmountTendered > Session.cart.cartHeader.Final_Total)
                        curAppliedPayments = Session.cart.cartHeader.Final_Total;
                    else
                        curAppliedPayments = curAmountTendered;

                    curAmountDue = Session.cart.cartHeader.Final_Total - curAppliedPayments;

                    if (Session.cart.cartHeader.Final_Total - curAmountTendered < 0)
                    {
                        lblAmountDue.Text = APILayer.GetCatalogText(LanguageConstant.cintChangeDue);
                        curChangeDue = Math.Abs(Session.cart.cartHeader.Final_Total - curAmountTendered);
                        curAmountDue = 0;
                        lblAmount.Text = string.Format(Session.DisplayFormat, curChangeDue);

                        gridPayment.ClearSelection();
                        gridPayment.Rows[nRowIndex].Selected = true;

                    }

                    else
                    {
                        lblAmountDue.Text = APILayer.GetCatalogText(LanguageConstant.cintAmountDue);
                        lblAmount.Text = string.Format(Session.DisplayFormat, curAmountDue);
                        if (curAmountDue <= 0)
                        {
                            tdbnAmountTendered.Enabled = false;
                        }
                        if (Session.cart.cartHeader.Final_Total - curAmountTendered == 0)
                        {
                            if (!Session.pblnModifyingOrder && Session.cart.cartHeader.Final_Total == 0)
                            {
                                NavigatePaymentControl(1, firstOrderPayment.Amount_Tendered, false, firstOrderPayment.Check_Info);
                            }
                            else
                            {
                                if (firstOrderPayment != null && firstOrderPayment.Order_Pay_Type_Code > 0)
                                {
                                    NavigatePaymentControl(firstOrderPayment.Order_Pay_Type_Code, firstOrderPayment.Amount_Tendered, false, firstOrderPayment.Check_Info);
                                }
                            }

                            gridPayment.ClearSelection();
                            gridPayment.Rows[nRowIndex].Selected = true;
                        }
                        else
                        {
                            if (orderPayType == null)
                            {
                                orderPayType = new CatalogOrderPayTypeCodes();
                                orderPayType = Session.orderPayTypeCodes.Find(x => x.Credit_Card_Code == Convert.ToInt32(cmdCash.Tag) && x.Order_Pay_Type_Code == 1);
                            }
                            NavigatePaymentControl(orderPayType.Order_Pay_Type_Code, curAmountDue, true, "");

                            gridPayment.ClearSelection();
                            nRowIndex = gridPayment.Rows.Count - 1;
                            gridPayment.Rows[nRowIndex].Selected = true;
                        }


                    }
                    tlblAmountDue.Text = string.Format(Session.DisplayFormat, curAmountDue);



                    #region GridFormat
                    gridPayment.RowHeadersVisible = false;
                    gridPayment.Columns[0].Width = 200;
                    gridPayment.Columns[1].Width = 80;

                    gridPayment.Columns["OrderPayType"].Visible = false;
                    gridPayment.Columns["Transaction_number"].Visible = false;
                    gridPayment.Columns["Credit_Card_Code"].Visible = false;
                    gridPayment.Columns["IsPayment"].Visible = false;
                    gridPayment.Columns["Payment_Line_Number"].Visible = false;
                    gridPayment.Columns["Payment_Sequence"].Visible = false;
                    gridPayment.Columns["Name"].HeaderText = "";
                    gridPayment.Columns["Value"].HeaderText = "";

                    gridPayment.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                    gridPayment.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

                    gridPayment.Columns["Name"].SortMode = DataGridViewColumnSortMode.NotSortable;
                    gridPayment.Columns["Value"].SortMode = DataGridViewColumnSortMode.NotSortable;
                    gridPayment.Rows[gridPayment.Rows.Count - 1].DefaultCellStyle.Font = new Font("Microsoft Sans Serif", 10, FontStyle.Regular);

                    #endregion


                    tdbnAmountTendered.ContainsFocus.ToString();


                }
            }
            catch (Exception ex)
            {
                Logger.Trace("ERROR", "frmPayment-refreshpaymentgrid(): " + ex.Message, ex, true);
            }

        }

        public void GetPaymentStatus(int TransactionID)
        {
            try
            {
                lblmsg.Text = "";
                int PaymentStatus = APILayer.GetPaymentStatus(TransactionID);

                Logger.Trace("INFO", PaymentStatus.ToString(), null, true);
                if (PaymentStatus == 0)
                {
                    lblmsg.Text = "Transaction Successful";
                    Timer.Stop();
                    btnOK_Click(new object(), new EventArgs());
                    IntegrationExeTimeOut = 0;
                    return;

                }
                if (PaymentStatus == 100)
                {
                    lblmsg.Text = "Transaction is processing..";
                    return;
                }
                if ((PaymentStatus == 99) && !Session.IsPayClick)
                {
                    lblmsg.Text = "Response Recieved";
                    Timer.Stop();
                    btnOK_Click(new object(), new EventArgs());
                    return;
                }
                if (PaymentStatus == 1 || PaymentStatus == -1)
                {

                    lblmsg.Text = "Transaction Failed !! Retry";
                    IntegrationExeTimeOut = 0;
                    Timer.Stop();
                    return;
                }
                if (PaymentStatus == -1)
                {
                    Timer.Stop();
                    return;
                }

            }
            catch (Exception ex)
            {
                Logger.Trace("ERROR", "frmPayment-getpaymentstatus(): " + ex.Message, ex, true);
                Timer.Stop();
            }
        }

        private void btndot_Click(object sender, EventArgs e)
        {
            tdbnAmountTendered.Text = tdbnAmountTendered.Text + ".";
        }

        void timer_Tick(object sender, EventArgs e)
        {
            try
            {
                if (IntegrationExeTimeOut == 0)
                {
                    Timer.Stop();
                    return;
                }
                if (i >= IntegrationExeTimeOut)
                {
                    IntegrationExeTimeOut = 0;
                    if (x >= timerts)
                    {
                        Timer.Stop();
                    }
                    else
                    {
                        var timespan = TimeSpan.FromSeconds(timerts);
                        timerts--;
                    }
                }
                else
                {
                    if (IntegrationExeTimeOut == 0)
                    {
                        Timer.Stop();
                        return;
                    }
                    GetPaymentStatus(TransactionID);
                    var timespan = TimeSpan.FromSeconds(IntegrationExeTimeOut);
                    IntegrationExeTimeOut--;

                }
            }
            catch (Exception ex)
            {
                Logger.Trace("ERROR", "frmPayment-timer_tick(): " + ex.Message, ex, true);
                Timer.Stop();
            }
        }

        public void CallIntegration(int TransactionID, int Order_Pay_type_code, string ExeName)
        {
            try
            {
                string ParameterDesc = string.Empty;
                ParameterDesc = PaymentFunctions.GetIntegrationModeParameterDesc(Order_Pay_type_code);

                if (TransactionID <= 0)
                    return;

                if (!CheckValidations())
                    return;

                ProcessStartInfo start = new ProcessStartInfo();
                var folderPath = Application.StartupPath + "\\DigitalExe\\";

                string newPath = Path.GetFullPath(folderPath + ExeName);
                bool fileExists = File.Exists(newPath);
                if (!fileExists)
                {
                    CustomMessageBox.Show(MessageConstant.ExeNotExist, CustomMessageBox.Buttons.OK, CustomMessageBox.Icon.Error);
                    return;
                }

                start.FileName = newPath;

                if (Session.IsPayClick)
                {
                    if (Order_Pay_type_code == 43 || Order_Pay_type_code == 37 || Order_Pay_type_code == 47)
                        start.Arguments = Convert.ToString(TransactionID) + " " + ParameterDesc;
                    else
                        start.Arguments = Convert.ToString(TransactionID);



                    Timer.Enabled = true;
                    Timer.Interval = 1000;
                    Timer.Tick += new EventHandler(timer_Tick);
                    start.UseShellExecute = false;
                    //Process.Start(start);

                    using (Process proc = Process.Start(start))
                    {

                        proc.WaitForExit();
                        if (proc.HasExited == true)
                            Timer.Start();


                    }

                }
                else
                {
                    if (Order_Pay_type_code == 43 || Order_Pay_type_code == 37 || Order_Pay_type_code == 47)
                        start.Arguments = Convert.ToString(TransactionID) + " " + ParameterDesc + " " + Convert.ToDecimal(tlblAmountDue.Text) + " DELIVERY";
                    else if (Order_Pay_type_code == 16 || Order_Pay_type_code == 27)
                        start.Arguments = Convert.ToString(TransactionID) + "  DELIVERY" + " " + Convert.ToDecimal(tlblAmountDue.Text);
                    else
                        start.Arguments = Convert.ToString(TransactionID);
                    start.WindowStyle = ProcessWindowStyle.Hidden;
                    start.UseShellExecute = false;
                    Timer.Enabled = true;
                    Timer.Interval = 1000;
                    Timer.Tick += new EventHandler(timer_Tick);

                    if (Order_Pay_type_code == 48 || Order_Pay_type_code == 17 || Order_Pay_type_code == 18)
                    {
                        using (Process proc = Process.Start(start))
                        {

                            if (Timer.Enabled == true)
                                Timer.Stop();
                            lblmsg.Text = "";
                            proc.Kill();
                            proc.Dispose();
                            proc.Close();
                            btnOK_Click(new object(), new EventArgs());

                        }
                    }
                    else
                    {
                        using (Process proc = Process.Start(start))
                        {

                            proc.WaitForExit();
                            if (proc.HasExited == true)
                                Timer.Start();
                            GetPaymentStatus(TransactionID);
                        }

                    }


                }



            }
            catch (Exception ex)
            {
                Logger.Trace("ERROR", "frmPayment-callIntegration(): " + ex.Message, ex, true);
            }
        }

        private void frmPayment_KeyDown(object sender, KeyEventArgs e)
        {
            ALT_F4 = (e.KeyCode.Equals(Keys.F4) && e.Alt == true);
        }

        private void frmPayment_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (ALT_F4)
            {
                ALT_F4 = false;
                e.Cancel = true;
                return;
            }
        }
        private void cmdCash_Click(object sender, EventArgs e)
        {

            ManageCashPayment(true, curAmountDue);
            //if(Session.CashDrawerOpen)
            //    CashDrawerProcess();

        }

        public void ManageCashPayment(bool IsNewPayment, decimal AmountTendered)
        {
            try
            {
                orderPayType = new CatalogOrderPayTypeCodes();
                orderPayType = Session.orderPayTypeCodes.Find(x => x.Credit_Card_Code == Convert.ToInt32(cmdCash.Tag) && x.Order_Pay_Type_Code == 1);

                if (IsNewPayment == true)
                {
                    if (AmountTendered <= 0)
                        btnOK.Enabled = false;
                    else
                        btnOK.Enabled = true;

                    panelNumricPad.Enabled = true;
                    pnl_amount.Enabled = true;
                    btnDelete.Enabled = false;
                    tdbnAmountTendered.Enabled = true;
                    tdbnAmountTendered.Text = String.Format(Session.DisplayFormat, 0);
                }
                else
                {
                    btnOK.Enabled = false;
                    panelNumricPad.Enabled = false;
                    pnl_amount.Enabled = false;
                    btnDelete.Enabled = true;
                    tdbnAmountTendered.Enabled = false;
                    tdbnAmountTendered.Text = String.Format(Session.DisplayFormat, AmountTendered);
                }
                txtCheckInfo.Text = string.Empty;
                lblPlaceHolder.Text = cmdCash.Text;
                pnl_Cheque.Visible = false;
                flPanel_CardsWallet.Visible = false;
                cmdKeyboard.Visible = false;
                //lblmsg.Visible = false;
                SetButtonColor(cmdCash);


            }
            catch (Exception ex)
            {
                Logger.Trace("ERROR", "frmPayment-manageCashPayment(): " + ex.Message, ex, true);
            }
        }

        public void ManageSodexoPayment(bool IsNewPayment, decimal AmountTendered)
        {
            try
            {
                orderPayType = new CatalogOrderPayTypeCodes();
                orderPayType = Session.orderPayTypeCodes.Find(x => x.Credit_Card_Code == Convert.ToInt32(cmdCash.Tag) && x.Order_Pay_Type_Code == 11);
                //LoadPosCommonBills();
                //LoadCurrencyDenominations();
                if (IsNewPayment == true)
                {
                    if (AmountTendered <= 0)
                        btnOK.Enabled = false;
                    else
                        btnOK.Enabled = true;

                    panelNumricPad.Enabled = true;
                    pnl_amount.Enabled = true;
                    btnDelete.Enabled = false;
                    tdbnAmountTendered.Enabled = true;
                    tdbnAmountTendered.Text = String.Format(Session.DisplayFormat, 0);
                }
                else
                {
                    btnOK.Enabled = false;
                    panelNumricPad.Enabled = false;
                    pnl_amount.Enabled = false;
                    btnDelete.Enabled = true;
                    tdbnAmountTendered.Enabled = false;
                    tdbnAmountTendered.Text = String.Format(Session.DisplayFormat, AmountTendered);
                }
                txtCheckInfo.Text = string.Empty;
                lblPlaceHolder.Text = cmdSodexo.Text;
                pnl_Cheque.Visible = false;
                flPanel_CardsWallet.Visible = false;
                cmdKeyboard.Visible = false;
                lblmsg.Visible = false;
                SetButtonColor(cmdSodexo);


            }
            catch (Exception ex)
            {
                Logger.Trace("ERROR", "frmPayment-managesodexopayment(): " + ex.Message, ex, true);
            }
        }

        public void ManageAccorPayment(bool IsNewPayment, decimal AmountTendered)
        {
            try
            {
                orderPayType = new CatalogOrderPayTypeCodes();
                orderPayType = Session.orderPayTypeCodes.Find(x => x.Credit_Card_Code == Convert.ToInt32(cmdCash.Tag) && x.Order_Pay_Type_Code == 28);
                //LoadPosCommonBills();
                //LoadCurrencyDenominations();
                if (IsNewPayment == true)
                {
                    if (AmountTendered <= 0)
                        btnOK.Enabled = false;
                    else
                        btnOK.Enabled = true;

                    panelNumricPad.Enabled = true;
                    pnl_amount.Enabled = true;
                    btnDelete.Enabled = false;
                    tdbnAmountTendered.Enabled = true;
                    tdbnAmountTendered.Text = String.Format(Session.DisplayFormat, 0);
                }
                else
                {
                    btnOK.Enabled = false;
                    panelNumricPad.Enabled = false;
                    pnl_amount.Enabled = false;
                    btnDelete.Enabled = true;
                    tdbnAmountTendered.Enabled = false;
                    tdbnAmountTendered.Text = String.Format(Session.DisplayFormat, AmountTendered);
                }
                txtCheckInfo.Text = string.Empty;
                lblPlaceHolder.Text = cmdAccor.Text;
                pnl_Cheque.Visible = false;
                flPanel_CardsWallet.Visible = false;
                cmdKeyboard.Visible = false;
                lblmsg.Visible = false;
                SetButtonColor(cmdAccor);


            }
            catch (Exception ex)
            {
                Logger.Trace("ERROR", "frmPayment-manageaccorpayment(): " + ex.Message, ex, true);
            }
        }

        public void ManageChequePayment(bool IsNewPayment, decimal AmountTendered, string Check_Info = "")
        {
            try
            {
                orderPayType = new CatalogOrderPayTypeCodes();
                orderPayType = Session.orderPayTypeCodes.Find(x => x.Credit_Card_Code == Convert.ToInt32(cmdCheque.Tag) && x.Order_Pay_Type_Code == 2);
                //LoadPosCommonBills();
                tdbnAmountTendered.Text = String.Format(Session.DisplayFormat, AmountTendered);
                if (IsNewPayment == true)
                {
                    if (AmountTendered <= 0)
                        btnOK.Enabled = false;
                    else
                        btnOK.Enabled = true;

                    panelNumricPad.Enabled = true;
                    btnDelete.Enabled = false;
                    tdbnAmountTendered.Enabled = true;
                    txtCheckInfo.Text = "";
                    txtCheckInfo.Enabled = true;
                }
                else
                {
                    btnOK.Enabled = false;
                    panelNumricPad.Enabled = false;
                    btnDelete.Enabled = true;
                    tdbnAmountTendered.Enabled = false;
                    txtCheckInfo.Text = Check_Info;
                    txtCheckInfo.Enabled = false;
                }
                pnl_amount.Enabled = false;
                lblPlaceHolder.Text = cmdCheque.Text;
                pnl_Cheque.Visible = true;
                flPanel_CardsWallet.Visible = false;
                cmdKeyboard.Visible = true;
                panelNumricPad.Enabled = true;
                lblmsg.Visible = false;
                SetButtonColor(cmdCheque);
            }
            catch (Exception ex)
            {
                Logger.Trace("ERROR", "frmPayment-maangechequepayment(): " + ex.Message, ex, true);
            }
        }


        public void ManageDigitalPayment(bool IsNewPayment, decimal AmountTendered)
        {
            try
            {
                if (IsNewPayment == true)
                {
                    flPanel_CardsWallet.Controls.Clear();
                    List<CatalogOrderPayTypeCodes> orderPaytypes = Session.orderPayTypeCodes.FindAll(x => x.Credit_Card_Code == Convert.ToInt32(cmdDigital.Tag));
                    foreach (CatalogOrderPayTypeCodes orderPayTypeCode in orderPaytypes)
                    {
                        Button button = new Button();
                        //if (orderPayTypeCode.Vendor_Integration_Status == "1")
                        if (orderPayTypeCode.Active == true && orderPayTypeCode.Credit_Card_Code == 4 && !string.IsNullOrEmpty(orderPayTypeCode.Vendor_Integration_Status))
                        {
                            button.Click += new EventHandler(OnButtonClick);
                            //button.Location = new System.Drawing.Point(Constants.HorizontalSpace, Constants.VerticalSpace);
                            button.Font = new System.Drawing.Font("Microsoft Sans Serif", 10, FontStyle.Regular);
                            button.Height = 62;
                            button.Width = 76;
                            button.Margin = new Padding(0);
                            button.BackColor = System.Drawing.Color.FromArgb(240, 240, 240);
                            button.Tag = orderPayTypeCode.Order_Pay_Type_Code;
                            button.Name = "btn_" + orderPayTypeCode.Credit_Card_Default_Description;
                            button.Text = orderPayTypeCode.Credit_Card_Default_Description;
                            flPanel_CardsWallet.Controls.Add(button);
                        }
                    }
                }


                tdbnAmountTendered.Text = String.Format(Session.DisplayFormat, AmountTendered);
                btnOK.Enabled = false;
                txtCheckInfo.Text = string.Empty;
                lblPlaceHolder.Text = cmdDigital.Text;
                pnl_Cheque.Visible = false;
                flPanel_CardsWallet.Visible = true;
                cmdKeyboard.Visible = false;
                btnDelete.Enabled = false;
                lblmsg.Visible = true;
                lblmsg.Text = "";
                pnl_amount.Enabled = false;
                if (!IsNewPayment)
                {
                    flPanel_CardsWallet.Enabled = false;
                    panelNumricPad.Enabled = false;
                    lblmsg.Visible = true;
                }
                else
                {
                    flPanel_CardsWallet.Enabled = true;
                    panelNumricPad.Enabled = true;
                }
                SetButtonColor(cmdDigital);
            }
            catch (Exception ex)
            {
                Logger.Trace("ERROR", "frmPayment-managedigitalpayment(): " + ex.Message, ex, true);
            }
        }

        private void panelPayType_Paint(object sender, PaintEventArgs e)
        {

        }

        private void cmdSodexo_Click(object sender, EventArgs e)
        {
            ManageSodexoPayment(true, curAmountDue);
        }

        private void pnl_payment_Paint(object sender, PaintEventArgs e)
        {

        }

        private void cmdDigital_Click(object sender, EventArgs e)
        {
            ManageDigitalPayment(true, curAmountDue);
            SetButtonColor(cmdDigital);
        }

        private void btnNum_1_Click(object sender, EventArgs e)
        {
            if (numberRole == "DIGITS")
            {
                tdbnAmountTendered.Text = tdbnAmountTendered.Text + "1";
            }
            else
                MakeAmount(btnNum_1.Text);

        }

        private void btnNum_2_Click(object sender, EventArgs e)
        {
            if (numberRole == "DIGITS")
            {
                tdbnAmountTendered.Text = tdbnAmountTendered.Text + "2";
            }
            else
                MakeAmount(btnNum_2.Text);
        }

        private void btnNum_3_Click(object sender, EventArgs e)
        {
            if (numberRole == "DIGITS")
            {
                tdbnAmountTendered.Text = tdbnAmountTendered.Text + "3";
            }
            else
                MakeAmount(btnNum_3.Text);
        }

        private void btnNum_4_Click(object sender, EventArgs e)
        {
            if (numberRole == "DIGITS")
            {
                tdbnAmountTendered.Text = tdbnAmountTendered.Text + "4";
            }
            else
                MakeAmount(btnNum_4.Text);
        }

        private void btnNum_5_Click(object sender, EventArgs e)
        {
            if (numberRole == "DIGITS")
            {
                tdbnAmountTendered.Text = tdbnAmountTendered.Text + "5";
            }
            else
                MakeAmount(btnNum_5.Text);
        }

        private void btnNum_6_Click(object sender, EventArgs e)
        {
            if (numberRole == "DIGITS")
            {
                tdbnAmountTendered.Text = tdbnAmountTendered.Text + "6";
            }
            else
                MakeAmount(btnNum_6.Text);
        }

        private void btnNum_7_Click(object sender, EventArgs e)
        {
            if (numberRole == "DIGITS")
            {
                tdbnAmountTendered.Text = tdbnAmountTendered.Text + "7";
            }
            else
                MakeAmount(btnNum_7.Text);
        }

        private void btnNum_8_Click(object sender, EventArgs e)
        {
            if (numberRole == "DIGITS")
            {
                tdbnAmountTendered.Text = tdbnAmountTendered.Text + "8";
            }
            else
                MakeAmount(btnNum_8.Text);
        }

        private void btnNum_9_Click(object sender, EventArgs e)
        {
            if (numberRole == "DIGITS")
            {
                tdbnAmountTendered.Text = tdbnAmountTendered.Text + "9";
            }
            else
                MakeAmount(btnNum_9.Text);
        }

        private void btnNum_0_Click(object sender, EventArgs e)
        {
            decimal meanWhile;
            if (numberRole == "DIGITS")
            {
                tdbnAmountTendered.Text = tdbnAmountTendered.Text + "0";
            }
            else
            {
                if (tdbnAmountTendered.Text.Length >= 10)
                    return;
                if (tdbnAmountTendered.Text.IndexOf(".") < 0)
                {
                    tdbnAmountTendered.Text = tdbnAmountTendered.Text + ".00";
                }

                tdbnAmountTendered.Text = tdbnAmountTendered.Text + "0";
                if (tdbnAmountTendered.Text.Length == 1)
                {
                    tdbnAmountTendered.Text = "0.0" + tdbnAmountTendered.Text;
                }
                if (tdbnAmountTendered.Text.Length >= 4)
                {
                    meanWhile = Convert.ToDecimal(tdbnAmountTendered.Text) * 10;
                    tdbnAmountTendered.Text = meanWhile.ToString("G29");
                }
                if (Convert.ToDecimal(tdbnAmountTendered.Text) < 1)
                {
                    tdbnAmountTendered.Text = tdbnAmountTendered.Text + "0";
                }
                if (tdbnAmountTendered.Text.IndexOf(".") < 0)
                {
                    tdbnAmountTendered.Text = tdbnAmountTendered.Text + ".00";
                }
                if (Convert.ToDecimal(tdbnAmountTendered.Text) > 1 && (tdbnAmountTendered.Text.Length - tdbnAmountTendered.Text.IndexOf(".") - 1 == 1))
                {
                    tdbnAmountTendered.Text = tdbnAmountTendered.Text + "0";
                }
            }
        }

        private void btnNum_Dot_Click(object sender, EventArgs e)
        {
            if (numberRole == "DIGITS")
            {
                tdbnAmountTendered.Text = tdbnAmountTendered.Text + ".";
            }
            else
            {
                //MakeAmount(btnNum_Dot.Text);
            }
        }

        private void btnBackSpace_Click(object sender, EventArgs e)
        {
            //NumberClick(btnBackSpace.Text);
            if (tdbnAmountTendered.Text.Length > 0)
            {
                tdbnAmountTendered.Text = tdbnAmountTendered.Text.Remove(tdbnAmountTendered.Text.Length - 1);
            }
        }



        private void cmdCheque_Click(object sender, EventArgs e)
        {
            ManageChequePayment(true, curAmountDue);
            SetButtonColor(cmdCheque);
        }

        private void cmdAccor_Click(object sender, EventArgs e)
        {
            ManageAccorPayment(true, curAmountDue);
        }

        private void tdbnAmountTendered_TextChanged(object sender, EventArgs e)
        {
            //decimal meanWhile;
            //if (tdbnAmountTendered.Text.Length > 0)
            //{
            //    meanWhile = Convert.ToDecimal(tdbnAmountTendered.Text) * 10;
            //    tdbnAmountTendered.Text = meanWhile.ToString("G29");
            //}
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
                foreach (Control pnl in pnl_payment.Controls)
                {
                    if (pnl is Panel)
                    {
                        pnl.BackColor = color;
                    }
                }
                pnl_payment.BackColor = color;
                // pnl_amount.BackColor = color;
            }
            catch (Exception ex)
            {
                Logger.Trace("ERROR", "frmPayment-checktrainningmode(): " + ex.Message, ex, true);
            }
        }

        private void btn_Order_Click(object sender, EventArgs e)
        {
            Form frmObj = Application.OpenForms["frmOrder"];
            if (frmObj != null)
            {
                ((frmOrder)frmObj).Show();
            }
            //else
            //{
            //    frmOrder frmOrder = new frmOrder();
            //    frmOrder.Show();
            //}
            this.Hide();
        }

        private void SetButtonColor(Button btn)
        {
            foreach (Button orderBtn in panelPayType.Controls)
            {
                orderBtn.BackColor = DefaultBackColor;
            }
            btn.BackColor = (Session.DefaultEntityColor);
        }
        private void SetButtonColorDigital(Button btn)
        {
            foreach (Button orderBtn in flPanel_CardsWallet.Controls)
            {
                orderBtn.BackColor = DefaultBackColor;
            }
            btn.BackColor = (Session.DefaultEntityColor);
        }


        private void MakeAmount(string amountTxt)
        {
            decimal meanWhile;
            if (tdbnAmountTendered.Text.Length >= 10)
                return;
            if (amountTxt == "\b")
                return;
            tdbnAmountTendered.Text = tdbnAmountTendered.Text + amountTxt;
            if (tdbnAmountTendered.Text.Length == 1)
            {
                tdbnAmountTendered.Text = "0.0" + tdbnAmountTendered.Text;
            }
            if (tdbnAmountTendered.Text.Length > 4)
            {
                meanWhile = Convert.ToDecimal(tdbnAmountTendered.Text) * 10;
                tdbnAmountTendered.Text = meanWhile.ToString("G29");
            }
        }

        private void num0_Click(object sender, EventArgs e)
        {
            decimal meanWhile;
            if (numberRole == "DIGITS")
            {
                tdbnAmountTendered.Text = tdbnAmountTendered.Text + "0";
            }
            else
            {
                tdbnAmountTendered.Text = tdbnAmountTendered.Text + "0";
                if (tdbnAmountTendered.Text.Length == 1)
                {
                    tdbnAmountTendered.Text = "0.0" + tdbnAmountTendered.Text;
                }
                if (tdbnAmountTendered.Text.Length >= 4)
                {
                    meanWhile = Convert.ToDecimal(tdbnAmountTendered.Text) * 10;
                    tdbnAmountTendered.Text = meanWhile.ToString("G29");
                }
                if (Convert.ToDecimal(tdbnAmountTendered.Text) < 1)
                {
                    tdbnAmountTendered.Text = tdbnAmountTendered.Text + "0";
                }
                if (tdbnAmountTendered.Text.IndexOf(".") < 0)
                {
                    tdbnAmountTendered.Text = tdbnAmountTendered.Text + ".00";
                }
                if (Convert.ToDecimal(tdbnAmountTendered.Text) > 1 && (tdbnAmountTendered.Text.Length - tdbnAmountTendered.Text.IndexOf(".") - 1 == 1))
                {
                    tdbnAmountTendered.Text = tdbnAmountTendered.Text + "0";
                }
            }
        }

        private void CashDrawerProcess()
        {
            CashDrawer cashDrawer = new CashDrawer();
            if (SystemSettings.WorkStationSettings.pstrPOSOrderTypePreference == "1")
                cashDrawer.OpenDrawer();
            else
                cashDrawer.OpenDrawerCommon();

            //CashDrawer cashDrawer = new CashDrawer();
            //if(CashDrawer.SendDataToWorkStation(Session.WorkstationIP,Session.ComputerName))
            //{

            CashDrawerReason drawerReason = new CashDrawerReason();
            drawerReason.Reason_Group_Code = 14;
            drawerReason.Reason_ID = Session.CashDrawerOpenCloseReasonID;
            drawerReason.iStatus = 1;
            drawerReason.Added_By = Session.CurrentEmployee.LoginDetail.EmployeeCode;
            drawerReason.Workstation_id = Session._WorkStationID;
            drawerReason.Order_Number = Session.cart.cartHeader.Order_Number;


            if (Session.cashDrawerReasonsForOrder == null)
                Session.cashDrawerReasonsForOrder = new List<CashDrawerReason>();

            Session.cashDrawerReasonsForOrder.Add(drawerReason);

            btnOK.Enabled = false;
            CDTimer.Enabled = true;

            CapturePayTypePreviousState();
            DisablePayTypeState();

            CDTimer.Interval = 1000;
            CDTimer.Tick += CDTimer_Tick;
            CDTimer.Start();
            //}

        }

        private void CDTimer_Tick(object sender, EventArgs e)
        {

            if (CashDrawerTimeOut == 0)
            {
                lblmsg.Text = " ";
                CDTimer.Stop();
                CDTimer.Enabled = false;
                isCashDrawerClose = true;
                EnablePayTypePreviousState();
                finalizePayment();

                return;
            }
            lblmsg.Text = "wait to close cash drawer " + CashDrawerTimeOut;

            //CashDrawer cashDrawer = new CashDrawer();
            if (CashDrawer.ReceiveDataFromWrokStation())
            {
                isCashDrawerClose = true;
                CashDrawerTimeOut = 0;
                lblmsg.Text = " ";
                CDTimer.Stop();
                EnablePayTypePreviousState();
                finalizePayment();

                return;
            }
            CashDrawerTimeOut--;

        }

        private void finalizePayment()
        {

            if (curAmountDue <= 0)
                PaymentFunctions.PaymentComplete();
        }

        private void CapturePayTypePreviousState()
        {
            btnOKEnabled = btnOK.Enabled;
            panelNumricPadEnabled = panelNumricPad.Enabled;
            pnl_amountEnabled = pnl_amount.Enabled;
            btnDeleteEnabled = btnDelete.Enabled;
            tdbnAmountTendered.Enabled = tdbnAmountTendered.Enabled;
            flPanel_CardsWalletEnabled = flPanel_CardsWallet.Enabled;
            flPanel_CardsWalletVisible = flPanel_CardsWallet.Visible;
        }
        private void EnablePayTypePreviousState()
        {
            btnOK.Enabled = btnOKEnabled;
            panelNumricPad.Enabled = panelNumricPadEnabled;
            pnl_amount.Enabled = pnl_amountEnabled;
            btnDelete.Enabled = btnDeleteEnabled;
            tdbnAmountTendered.Enabled = tdbnAmountTendered.Enabled;
            flPanel_CardsWallet.Enabled = flPanel_CardsWalletEnabled;
            flPanel_CardsWallet.Visible = flPanel_CardsWalletVisible;
        }

        private void DisablePayTypeState()
        {
            btnOK.Enabled = false;
            panelNumricPad.Enabled = false;
            pnl_amount.Enabled = false;
            btnDelete.Enabled = false;
            tdbnAmountTendered.Enabled = false;
            flPanel_CardsWallet.Enabled = false;

        }


    }


}
