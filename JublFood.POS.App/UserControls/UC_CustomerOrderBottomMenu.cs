using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using JublFood.POS.App.Class;
using JublFood.Settings;
using JublFood.POS.App.Models;
using System.Configuration;
using JublFood.POS.App.BusinessLayer;
using JublFood.POS.App.Cache;
using JublFood.POS.App.Models.Cart;
using JublFood.POS.App.API;
using Jublfood.AppLogger;
using JublFood.POS.App.Models.Employee;

namespace JublFood.POS.App
{
    public partial class UC_CustomerOrderBottomMenu : UserControl
    {
        public string Formname;
        public DataGridView ListViewFormModify;
        public List<UC_CatalogOrderTypes> listCatalogOrderTypes;
        UC_CatalogOrderTypes catalogOrder;
        //BAL balObj;
        public event EventHandler UserControlButtonClicked;
        private DateTime _pdtmServerDateTime = Settings.Settings.GetServerDateTime();
        decimal ODCMinOrderAmount = 0;
        
        private void OnUserControlButtonClick(object sender, EventArgs e)
        {
            if (UserControlButtonClicked != null)
            {
                UserControlButtonClicked(sender, e);
            }
        }
        public UC_CustomerOrderBottomMenu()
        {
            try
            {
                InitializeComponent();
                SetButtonText();

                if (Session.cart != null)
                {
                    //cmdPrintOnDemand.Enabled = true;
                    cmdComplete.Enabled = true;
                    cmdPay.Enabled = true;
                }
                else
                {
                    cmdPrintOnDemand.Enabled = false;
                    cmdComplete.Enabled = false;
                    cmdPay.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                Logger.Trace("ERROR", "uc_customerorderbottommenu-uc_customerorderbottommenu(): " + ex.Message, ex, true);
            }
        }

        public void LoadOrderType()
        {
            flPanel_orderType.Controls.Clear();
            //balObj = new BAL();
            //DataTable DtOrderType;
            //DtOrderType = balObj.GetAllOrderType();
            List<EmployeeOrderTypes> GetEmployeeOrderTypes=null;
            listCatalogOrderTypes = new List<UC_CatalogOrderTypes>();

            for (int i = 0; i < Session.OrderType.Count; i++)
            {
                if (Session.OrderType[i].Order_Type_Code == "C")
                {
                    catalogOrder = new UC_CatalogOrderTypes();
                    catalogOrder.Workstation_ID = 1;
                    catalogOrder.Location_Code = Session._LocationCode;
                    catalogOrder.Order_Type_Code = Session.OrderType[i].Order_Type_Code;
                    catalogOrder.Description = Session.OrderType[i].Description;
                    listCatalogOrderTypes.Add(catalogOrder);
                }

                if (Session.OrderType[i].Order_Type_Code == "D")
                {
                    catalogOrder = new UC_CatalogOrderTypes();
                    catalogOrder.Workstation_ID = 1;
                    catalogOrder.Location_Code = Session._LocationCode;
                    catalogOrder.Order_Type_Code = Session.OrderType[i].Order_Type_Code;
                    catalogOrder.Description = Session.OrderType[i].Description;
                    listCatalogOrderTypes.Add(catalogOrder);
                }

                if (Session.OrderType[i].Order_Type_Code == "I")
                {
                    catalogOrder = new UC_CatalogOrderTypes();
                    catalogOrder.Workstation_ID = 1;
                    catalogOrder.Location_Code = Session._LocationCode;
                    catalogOrder.Order_Type_Code = Session.OrderType[i].Order_Type_Code;
                    catalogOrder.Description = Session.OrderType[i].Description;
                    listCatalogOrderTypes.Add(catalogOrder);
                }

                if (Session.OrderType[i].Order_Type_Code == "P")
                {
                    catalogOrder = new UC_CatalogOrderTypes();
                    catalogOrder.Workstation_ID = 1;
                    catalogOrder.Location_Code = Session._LocationCode;
                    catalogOrder.Order_Type_Code = Session.OrderType[i].Order_Type_Code;
                    catalogOrder.Description = Session.OrderType[i].Description;
                    listCatalogOrderTypes.Add(catalogOrder);
                }
				

            }
            Button btn;
            if (Session.CurrentEmployee != null && Session.CurrentEmployee.LoginDetail !=null)
            { 
               GetEmployeeOrderTypes = APILayer.GetEmployeeOrderTypes(Session.CurrentEmployee.LoginDetail.LocationCode, Session.CurrentEmployee.LoginDetail.EmployeeCode);
            }
            foreach (UC_CatalogOrderTypes catalogOrdertype in listCatalogOrderTypes)
            {
                btn = new Button();

                btn.Text = catalogOrdertype.Description;
                btn.Tag = catalogOrdertype.Order_Type_Code;
                btn.Margin = new Padding(0);
                btn.Size = new Size(68, 55);
                btn.BackColor = SystemColors.Control;
                btn.TextImageRelation = TextImageRelation.ImageAboveText;
                btn.TextAlign = ContentAlignment.BottomCenter;
                btn.Click += new EventHandler(btn_CarryOut_Click);
                flPanel_orderType.Controls.Add(btn);
                if (catalogOrdertype.Order_Type_Code == "C")
                {
                    btn.Image = Properties.Resources._96;
                }
                else if (catalogOrdertype.Order_Type_Code == "D")
                {
                    btn.Image = Properties.Resources._261;
                }
                else if (catalogOrdertype.Order_Type_Code == "I")
                {
                    btn.Image = Properties.Resources._36;
                }
                else if (catalogOrdertype.Order_Type_Code == "P")
                {
                    btn.Image = Properties.Resources._104;
                    
                }

                if (GetEmployeeOrderTypes != null)
                {
                    if (GetEmployeeOrderTypes.FindIndex(x => x.Order_Type_Code == catalogOrdertype.Order_Type_Code) > -1)
                    {
                        btn.Enabled = false;
                    }
                }
                //if (Session.selectedOrderType == "C")
                //{
                //    btn.Select();
                //}
                //else if (Session.selectedOrderType == "D")
                //{
                //    btn.Select();
                //}
                //else if (Session.selectedOrderType == "I")
                //{
                //    btn.Select();
                //}
                //else if (Session.selectedOrderType == "P")
                //{
                //    btn.Select();
                //}
            }
        }

        //public void LoadOrderTypeRemake(string Order_Type)
        //{
        //    flPanel_orderType.Controls.Clear();
        //    balObj = new BAL();
        //    DataTable DtOrderType;
        //    DtOrderType = balObj.GetAllOrderType();


        //    listCatalogOrderTypes = new List<UC_CatalogOrderTypes>();

        //    catalogOrder = new UC_CatalogOrderTypes();
        //    catalogOrder.Workstation_ID = 1;
        //    catalogOrder.Location_Code = Session._LocationCode;
        //    catalogOrder.Order_Type_Code = DtOrderType.Select("Order_Type_Code=" + "'" + "C" + "'")[0]["Order_Type_Code"].ToString();
        //    catalogOrder.Description = DtOrderType.Select("Order_Type_Code=" + "'" + "C" + "'")[0]["Description"].ToString();
        //    listCatalogOrderTypes.Add(catalogOrder);

        //    catalogOrder = new UC_CatalogOrderTypes();
        //    catalogOrder.Workstation_ID = 1;
        //    catalogOrder.Location_Code = Session._LocationCode;
        //    catalogOrder.Order_Type_Code = DtOrderType.Select("Order_Type_Code=" + "'" + "D" + "'")[0]["Order_Type_Code"].ToString();
        //    catalogOrder.Description = DtOrderType.Select("Order_Type_Code=" + "'" + "D" + "'")[0]["Description"].ToString();
        //    listCatalogOrderTypes.Add(catalogOrder);

        //    catalogOrder = new UC_CatalogOrderTypes();
        //    catalogOrder.Workstation_ID = 1;
        //    catalogOrder.Location_Code = Session._LocationCode;
        //    catalogOrder.Order_Type_Code = DtOrderType.Select("Order_Type_Code=" + "'" + "I" + "'")[0]["Order_Type_Code"].ToString();
        //    catalogOrder.Description = DtOrderType.Select("Order_Type_Code=" + "'" + "I" + "'")[0]["Description"].ToString();
        //    listCatalogOrderTypes.Add(catalogOrder);

        //    catalogOrder = new UC_CatalogOrderTypes();
        //    catalogOrder.Workstation_ID = 1;
        //    catalogOrder.Location_Code = Session._LocationCode;
        //    catalogOrder.Order_Type_Code = DtOrderType.Select("Order_Type_Code=" + "'" + "P" + "'")[0]["Order_Type_Code"].ToString();
        //    catalogOrder.Description = DtOrderType.Select("Order_Type_Code=" + "'" + "P" + "'")[0]["Description"].ToString();
        //    listCatalogOrderTypes.Add(catalogOrder);
        //    Button btn;
        //    foreach (UC_CatalogOrderTypes catalogOrdertype in listCatalogOrderTypes)
        //    {
        //        btn = new Button();

        //        btn.Text = catalogOrdertype.Description;
        //        btn.Tag = catalogOrdertype.Order_Type_Code;
        //        btn.Margin = new Padding(0);
        //        btn.Size = new Size(68, 55);
        //        btn.BackColor = SystemColors.Control;
        //        btn.TextImageRelation = TextImageRelation.ImageAboveText;
        //        btn.TextAlign = ContentAlignment.BottomCenter;
        //        btn.Click += new EventHandler(btn_CarryOut_Click);
        //        flPanel_orderType.Controls.Add(btn);
        //        if (catalogOrdertype.Order_Type_Code == "C")
        //        {
        //            btn.Image = Properties.Resources._96;
        //        }
        //        else if (catalogOrdertype.Order_Type_Code == "D")
        //        {
        //            btn.Image = Properties.Resources._261;
        //        }
        //        else if (catalogOrdertype.Order_Type_Code == "I")
        //        {
        //            btn.Image = Properties.Resources._36;
        //        }
        //        else if (catalogOrdertype.Order_Type_Code == "P")
        //        {
        //            btn.Image = Properties.Resources._104;
        //        }

        //        if (catalogOrdertype.Order_Type_Code == Order_Type)
        //        {
        //            btn.Image = Properties.Resources._96;
        //            btn.BackColor = SystemColors.ControlText;
        //        }
        //        else if (catalogOrdertype.Order_Type_Code == Order_Type)
        //        {
        //            btn.Image = Properties.Resources._261;
        //            btn.BackColor = SystemColors.ControlText;
        //        }
        //        else if (catalogOrdertype.Order_Type_Code == Order_Type)
        //        {
        //            btn.Image = Properties.Resources._36;
        //            btn.BackColor = SystemColors.ControlText;
        //        }
        //        else if (catalogOrdertype.Order_Type_Code == Order_Type)
        //        {
        //            btn.Image = Properties.Resources._104;
        //            btn.BackColor = SystemColors.ControlText;
        //        }
        //        if (catalogOrdertype.Order_Type_Code == Order_Type)
        //        {
        //            btn.BackColor = Color.FromArgb(255, 128, 128);
        //            //btn.BackColor = SystemColors.ControlText;
        //        }
        //        else if (catalogOrdertype.Order_Type_Code == Order_Type)
        //        {
        //            btn.BackColor = Color.FromArgb(255, 128, 128);
        //            //btn.BackColor = SystemColors.ControlText;
        //        }
        //        else if (catalogOrdertype.Order_Type_Code == Order_Type)
        //        {
        //            btn.BackColor = Color.FromArgb(255, 128, 128);
        //            //btn.BackColor = SystemColors.ControlText;
        //        }
        //        else if (catalogOrdertype.Order_Type_Code == Order_Type)
        //        {
        //            btn.BackColor = Color.FromArgb(255, 128, 128);
        //            //btn.BackColor = SystemColors.ControlText;
        //        }

        //        if (btn.Text != "NO")
        //        {
        //            btn.Enabled = false;

        //        }
        //    }
        //}



        private void SetButtonText()
        {
            string labelText = string.Empty;
            if (Common.DictAllButtonText.TryGetValue(LanguageConstant.cintPrintExtra, out labelText))
            {
                cmdPrintOnDemand.Text = labelText;
            }
            if (Common.DictAllButtonText.TryGetValue(LanguageConstant.cintPay, out labelText))
            {
                cmdPay.Text = labelText;
            }
            if (Common.DictAllButtonText.TryGetValue(LanguageConstant.cintSave, out labelText))
            {
                cmdComplete.Text = labelText;
            }
        }
        private void btn_CarryOut_Click(object sender, EventArgs e)
        {
            try
            {
                Button btn = (Button)sender;

                //Form currentForm = this.Parent.FindForm();
                //foreach (Control control in currentForm.Controls)
                //{
                //    if (control.GetType() == typeof(TableLayoutPanel))
                //    {
                //        if (control.Name == "tlpCustomerPanel")
                //        {
                //            foreach (Control _control in control.Controls)
                //            {
                //                if (_control.GetType() == typeof(Label))
                //                {
                //                    if (btn.Text == "Carry-Out")
                //                    {
                //                        Session.selectedOrderType = "C";

                //                        if (Convert.ToString(_control.Name) == "ltxtPhone_Number")
                //                        {
                //                            _control.ForeColor = Color.Yellow;
                //                        }

                //                        if (Convert.ToString(_control.Name) == "ltxtName")
                //                        {
                //                            _control.ForeColor = Color.Yellow;
                //                        }

                //                        if (Convert.ToString(_control.Name) == "ltxtStreet_Number")
                //                        {
                //                            _control.ForeColor = Color.White;
                //                        }

                //                        if (Convert.ToString(_control.Name) == "ltxtCity")
                //                        {
                //                            _control.ForeColor = Color.White;
                //                        }

                //                        if (Convert.ToString(_control.Name) == "ltxtRegion")
                //                        {
                //                            _control.ForeColor = Color.White;
                //                        }

                //                        if (Convert.ToString(_control.Name) == "ltxtPostal_Code")
                //                        {
                //                            _control.ForeColor = Color.White;
                //                        }
                //                    }
                //                    else if (btn.Text == "Delivery")
                //                    {
                //                        Session.selectedOrderType = "D";

                //                        if (Convert.ToString(_control.Name) == "ltxtPhone_Number")
                //                        {
                //                            _control.ForeColor = Color.Yellow;
                //                        }

                //                        if (Convert.ToString(_control.Name) == "ltxtName")
                //                        {
                //                            _control.ForeColor = Color.Yellow;
                //                        }

                //                        if (Convert.ToString(_control.Name) == "ltxtStreet_Number")
                //                        {
                //                            _control.ForeColor = Color.Yellow;
                //                        }

                //                        if (Convert.ToString(_control.Name) == "ltxtCity")
                //                        {
                //                            _control.ForeColor = Color.Yellow;
                //                        }

                //                        if (Convert.ToString(_control.Name) == "ltxtRegion")
                //                        {
                //                            _control.ForeColor = Color.Yellow;
                //                        }

                //                        if (Convert.ToString(_control.Name) == "ltxtPostal_Code")
                //                        {
                //                            _control.ForeColor = Color.Yellow;
                //                        }
                //                    }
                //                    else if (btn.Text == "Dine-In")
                //                    {
                //                        Session.selectedOrderType = "I";

                //                        if (Convert.ToString(_control.Name) == "ltxtPhone_Number")
                //                        {
                //                            _control.ForeColor = Color.White;
                //                        }

                //                        if (Convert.ToString(_control.Name) == "ltxtName")
                //                        {
                //                            _control.ForeColor = Color.Yellow;
                //                        }

                //                        if (Convert.ToString(_control.Name) == "ltxtStreet_Number")
                //                        {
                //                            _control.ForeColor = Color.White;
                //                        }

                //                        if (Convert.ToString(_control.Name) == "ltxtCity")
                //                        {
                //                            _control.ForeColor = Color.White;
                //                        }

                //                        if (Convert.ToString(_control.Name) == "ltxtRegion")
                //                        {
                //                            _control.ForeColor = Color.White;
                //                        }

                //                        if (Convert.ToString(_control.Name) == "ltxtPostal_Code")
                //                        {
                //                            _control.ForeColor = Color.White;
                //                        }
                //                    }
                //                    else if (btn.Text == "ODC")
                //                    {
                //                        Session.selectedOrderType = "P";

                //                        if (Convert.ToString(_control.Name) == "ltxtPhone_Number")
                //                        {
                //                            _control.ForeColor = Color.Yellow;
                //                        }

                //                        if (Convert.ToString(_control.Name) == "ltxtName")
                //                        {
                //                            _control.ForeColor = Color.Yellow;
                //                        }

                //                        if (Convert.ToString(_control.Name) == "ltxtStreet_Number")
                //                        {
                //                            _control.ForeColor = Color.White;
                //                        }

                //                        if (Convert.ToString(_control.Name) == "ltxtCity")
                //                        {
                //                            _control.ForeColor = Color.White;
                //                        }

                //                        if (Convert.ToString(_control.Name) == "ltxtRegion")
                //                        {
                //                            _control.ForeColor = Color.White;
                //                        }

                //                        if (Convert.ToString(_control.Name) == "ltxtPostal_Code")
                //                        {
                //                            _control.ForeColor = Color.White;
                //                        }

                                        
                //                    }
                //                }
                //            }
                //        }
                //    }

                //}

                
                if (Formname == "frmModify")
                {
                    if (btn.BackColor != Color.FromArgb(255, 128, 128))
                    {
                        btn.BackColor = Color.FromArgb(255, 128, 128);
                        SetColumnVisibilityListViewModify(btn, true);
                    }
                    else
                    {
                        btn.BackColor = DefaultBackColor;
                        SetColumnVisibilityListViewModify(btn, false);
                    }
                }
                else
                {
                    SetButtonColor(btn);
                }

                
                if (Formname == "frmOrder" || Formname == "frmCustomer" || Formname == "frmModify")
                {
                    OnUserControlButtonClick(sender, e);

                }

                Form currentForm = this.Parent.FindForm();
                if(currentForm.Name == "frmCustomer") ((frmCustomer)currentForm).Set_Required_Fields(Session.selectedOrderType, Session.cart.Customer.Address_Type);

                //ODC Tax
                if (!Session.OrderTypeAutoSelect && (Formname == "frmOrder" || Formname == "frmCustomer") && (Session.selectedOrderType=="P"))
                {
                    if (SystemSettings.GetSettingValue("ODC_Tax_Change", Session._LocationCode) == "1")
                    {
                        frmTaxPrompt objtaxprompt = new frmTaxPrompt();
                        objtaxprompt.ShowDialog();
                    }                   
                }

            }
            catch (Exception ex)
            {
                Logger.Trace("ERROR", "uc_customerorderbottommenu-btn_carryout_click(): " + ex.Message, ex, true);
            }

        }

        private void SetButtonColor(Button btn)
        {

            foreach (Button orderBtn in flPanel_orderType.Controls)
            {
                orderBtn.BackColor = DefaultBackColor;
            }
            btn.BackColor = Session.DefaultEntityColor; //Color.FromArgb(255, 128, 128);

        }
        public void SetButtonColorForFormModify()
        {
            foreach (Button orderBtn in flPanel_orderType.Controls)
            {
                orderBtn.BackColor = Color.FromArgb(255, 128, 128);
            }

        }

        private void SetColumnVisibilityListViewModify(Button btn, bool IsChecked)
        {
            if (!IsChecked)
            {
                if (btn.Tag.ToString() == "C")
                {
                    ListViewFormModify.Columns[7].Width = 0;

                }
                else if (btn.Tag.ToString() == "D")
                {
                    ListViewFormModify.Columns[3].Width = 0;
                    ListViewFormModify.Columns[4].Width = 0;
                    ListViewFormModify.Columns[5].Width = 0;
                    ListViewFormModify.Columns[6].Width = 0;
                    ListViewFormModify.Columns[7].Width = 0;
                    ListViewFormModify.Columns[8].Width = 0;
                    ListViewFormModify.Columns[9].Width = 0;
                    ListViewFormModify.Columns[10].Width = 0;
                }
                else if (btn.Tag.ToString() == "I")
                {
                    ListViewFormModify.Columns[7].Width = 0;
                }
                else if (btn.Tag.ToString() == "P")
                {
                    ListViewFormModify.Columns[3].Width = 0;
                    ListViewFormModify.Columns[7].Width = 0;
                }
            }

            foreach (Button btnOrder in flPanel_orderType.Controls)
            {

                if (btnOrder.Tag.ToString() == "C" && btnOrder.BackColor == Color.FromArgb(255, 128, 128))
                {
                    ListViewFormModify.Columns[7].Width = 100;
                }
                else if (btnOrder.Tag.ToString() == "D" && btnOrder.BackColor == Color.FromArgb(255, 128, 128))
                {
                    ListViewFormModify.Columns[3].Width = 100;
                    ListViewFormModify.Columns[4].Width = 120;
                    ListViewFormModify.Columns[5].Width = 150;
                    ListViewFormModify.Columns[6].Width = 100;
                    ListViewFormModify.Columns[7].Width = 100;
                    ListViewFormModify.Columns[8].Width = 100;
                    ListViewFormModify.Columns[9].Width = 100;
                    ListViewFormModify.Columns[10].Width = 100;
                }
                else if (btnOrder.Tag.ToString() == "I" && btnOrder.BackColor == Color.FromArgb(255, 128, 128))
                {
                    ListViewFormModify.Columns[7].Width = 100;
                }
                else if (btnOrder.Tag.ToString() == "O" && btnOrder.BackColor == Color.FromArgb(255, 128, 128))
                {
                    ListViewFormModify.Columns[7].Width = 100;
                    ListViewFormModify.Columns[3].Width = 100;
                }
            }
        }

        private void cmdComplete_Click(object sender, EventArgs e)
        {
            string Cartitems = string.Empty;
            string UpsellPopUp = string.Empty;
            frmPayment frmPayment = null;
            bool TempPayStatus = Session.IsPayClick;
            DateTime serverDateTime = Settings.Settings.GetServerDateTime();
            decimal Final_Total_bf_del;
            bool exemitem = false;
            ODCMinOrderAmount = Convert.ToDecimal(SystemSettings.GetSettingValue("ODCMinimumOrderAmount", Session._LocationCode));

            try
            {

                if (Session.cart.cartHeader.Delayed_Date != DateTime.MinValue)
                {
                    if (Session.cart.cartHeader.Delayed_Date < _pdtmServerDateTime)
                    {
                        if (CustomMessageBox.Show(MessageConstant.InvalidTimedOrderDate, CustomMessageBox.Buttons.YesNo, CustomMessageBox.Icon.Question) == DialogResult.No)
                        {
                            return;
                        }
                        else
                        {
                            Session.cart.cartHeader.Delayed_Date = DateTime.MinValue;
                            Session.cart.cartHeader.Delayed_Order = 0;
                            Session.cart.cartHeader.Order_Date = serverDateTime;
                            Session.cart.cartHeader.Actual_Order_Date = serverDateTime;
                            Session.cart.cartHeader.Kitchen_Display_Time = serverDateTime;
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
                        }

                    }

                }

                CartFunctions.FillCustomerToCart(ref Session.cart);

                if(Session.cart !=null && Session.cart.cartHeader.Order_Type_Code !=null && (Session.cart.cartHeader.Order_Type_Code=="I"))
                    Session.IsPayClick = true;
                else
                    Session.IsPayClick = false;

                List<CartItem> lstCartItem = Session.cart.cartItems;

                //----------------------Minimum Order Value Check----------------
                foreach (CartItem cartitem in lstCartItem)
                {
                    exemitem = APILayer.CheckExeItemMov(cartitem.Location_Code, cartitem.Menu_Code, cartitem.Size_Code);

                    if (exemitem)
                    {
                        exemitem = true;
                        break;
                    }
                    else
                    {
                        exemitem = false;
                    }
                }

                Final_Total_bf_del = Session.cart.cartHeader.Final_Total - (Session.cart.cartHeader.Delivery_Fee + Session.cart.orderUDT.Delivery_Fee_Tax1 + Session.cart.orderUDT.Delivery_Fee_Tax2 + Session.cart.orderUDT.Delivery_Fee_Tax3 + Session.cart.orderUDT.Delivery_Fee_Tax4);
                if (Final_Total_bf_del <= ODCMinOrderAmount && Session.cart.cartHeader.Order_Type_Code == "P")
                {
                    CustomMessageBox.Show(MessageConstant.ODCMinOrderAmount + Convert.ToString(ODCMinOrderAmount), CustomMessageBox.Buttons.OK);
                    return;
                }
                if (!exemitem)
                {
                    var Min_Order_Mov = APILayer.GetDeliveryFeeMOV(Session.cart.cartHeader.LocationCode, Session.cart.cartHeader.Order_Type_Code, Session.cart.cartHeader.Coupon_Code, decimal.ToDouble(Final_Total_bf_del));

                    if (Min_Order_Mov > 0)
                    {
                        CustomMessageBox.Show(APILayer.GetCatalogText(LanguageConstant.cintMovPrompt) + Min_Order_Mov, CustomMessageBox.Buttons.OK);
                        //----------show upsell
                        foreach (CartItem cartitem in lstCartItem)
                        {
                            if (cartitem.Combo_Group == 0)
                            {
                                Cartitems = Cartitems + cartitem.Menu_Code + "|" + cartitem.Size_Code + ";";
                            }
                        }

                        UpsellPopUp = SystemSettings.GetSettingValue("UpsellPopUp", Session._LocationCode);
                        if (Session.Upsellcnt == 0 && UpsellPopUp == "1" && Session.selectedOrderType != "P")
                        {
                            var rowcount = APILayer.GetUpsellMenu(Cartitems);
                            if (rowcount != null && rowcount.Count > 0 && Session.RemakeOrder == false)
                            {
                                Session.Upsellcnt = Session.Upsellcnt + 1;
                                using (frmUpsell objfrmUpsell = new frmUpsell())
                                {
                                    if (!Session._LocationCode.Contains("PLK")) objfrmUpsell.ShowDialog();

                                }
                            }
                        }

                        //-------------------
                        return;
                    }
                }
                //---------------------------------------------------------------


                foreach (CartItem cartitem in lstCartItem)
                {
                    if (cartitem.Combo_Group == 0)
                    {
                        Cartitems = Cartitems + cartitem.Menu_Code + "|" + cartitem.Size_Code + ";";
                    }
                }

                UpsellPopUp = SystemSettings.GetSettingValue("UpsellPopUp", Session._LocationCode);
                if (Session.Upsellcnt == 0 && UpsellPopUp == "1" && Session.selectedOrderType != "P")
                {
                    var rowcount = APILayer.GetUpsellMenu(Cartitems);
                    if (rowcount != null && rowcount.Count > 0 && Session.RemakeOrder == false)
                    {
                        Session.Upsellcnt = Session.Upsellcnt + 1;
                        using (frmUpsell objfrmUpsell = new frmUpsell())
                        {
                            if (!Session._LocationCode.Contains("PLK")) objfrmUpsell.ShowDialog();

                        }
                    }
                }

                //----------------------ODC Tax Change---------------------------
                if (SystemSettings.GetSettingValue("ODC_Tax_Change", Session._LocationCode) == "1" && Session.selectedOrderType == "P")
                {
                    if (CustomMessageBox.Show(MessageConstant.AllowODCChangeMSG, CustomMessageBox.Buttons.YesNo, CustomMessageBox.Icon.Question) == DialogResult.No)
                    {
                        frmTaxPrompt frmtax = new frmTaxPrompt();
                        frmtax.ShowDialog();
                    }
                }
                //---------------------------------------------------------------

                Form frmchk = this.Parent.FindForm();
                if (frmchk.Text == "Customer" || frmchk.Text == "Order")
                {
                    OrderFunctions.HandleAddressControlData();
                }


                UserTypes.OrderCompletionState orderCompletionState = new UserTypes.OrderCompletionState();
                orderCompletionState = OrderCompleteFunctions.CheckOrderCompletion();

                if (orderCompletionState == UserTypes.OrderCompletionState.OrderNotComplete)
                {
                    return;
                }
                else if (orderCompletionState == UserTypes.OrderCompletionState.OpenCustomer)
                {
                    Form frm = this.Parent.FindForm();
                    if (frm.Text == "Order")
                    {
                        ((frmOrder)frm).uC_Customer_OrderMenu.cmdDelivery_Info_Click(null, new EventArgs());

                    }
                    return;
                }
                else if (orderCompletionState == UserTypes.OrderCompletionState.OpenOrder)
                {
                    Form frm = this.Parent.FindForm();
                    if (frm.Text == "Customer")
                    {
                        ((frmOrder)frm).uC_Customer_OrderMenu.cmdDelivery_Info_Click(null, new EventArgs());

                    }
                    return;
                }

                if (!PaymentFunctions.PayNow())
                {
                    Form frm = this.Parent.FindForm();
                    if (frm.Text == "Customer")
                    {
                        ((frmOrder)frm).uC_Customer_OrderMenu.cmdDelivery_Info_Click(null, new EventArgs());

                    }
                    return;
                }

                PaymentFunctions.OpenPaymentScreen();

                
            }
            catch (Exception ex)
            {
                Logger.Trace("ERROR", "uc_customerorderbottommenu-cmdcomplete_click(): " + ex.Message, ex, true);
            }
        }
        public bool checkItemsize()
        {
            List<CartItem> lstCartItem = Session.cart.cartItems;
            if (Session.cart != null && lstCartItem.Count > 0)
            {

                foreach (CartItem cartitem in lstCartItem)
                {

                    string cartdesc = cartitem.Description;
                    if (cartdesc.Contains("-->Size") || cartdesc == "")
                    {
                        return false;
                    }
                }
            }
            else
            {
                return false;
            }

            return true;
        }

        private void cmdPay_Click(object sender, EventArgs e)
        {

            string Cartitems = string.Empty;
            string UpsellPopUp = string.Empty;
            frmPayment frmPayment = null;
            bool TempPayStatus = Session.IsPayClick;
            DateTime serverDateTime = Settings.Settings.GetServerDateTime();
            decimal Final_Total_bf_del;
            bool exemitem=false;
            ODCMinOrderAmount = Convert.ToDecimal(SystemSettings.GetSettingValue("ODCMinimumOrderAmount", Session._LocationCode));

            try
            {

                //Out of Stock Popup
                List<int> ItemIndex = CartFunctions.OutofStockItemsFromCart();
                if(ItemIndex != null && ItemIndex.Count > 0)
                {
                    string strItems = string.Empty;
                    foreach (int index in ItemIndex)
                        strItems += "● " + Session.cart.cartItems[index].Description + " \n\r";

                    CustomMessageBox.Show(MessageConstant.OutOfStock + "\n\r\n\r" + strItems, CustomMessageBox.Buttons.OK, CustomMessageBox.Icon.Warning);
                    return;
                }


                CartFunctions.FillCustomerToCart(ref Session.cart);

                Session.IsPayClick = true;

                List<CartItem> lstCartItem = Session.cart.cartItems;

                //----------------------Minimum Order Value Check----------------
                foreach (CartItem cartitem in lstCartItem)
                {
                    exemitem = APILayer.CheckExeItemMov(cartitem.Location_Code,cartitem.Menu_Code,cartitem.Size_Code);

                    if (exemitem)
                    {
                        exemitem = true;
                        break;
                    }
                    else
                    {
                        exemitem = false;
                    }
                } 

                  Final_Total_bf_del = Session.cart.cartHeader.Final_Total - (Session.cart.cartHeader.Delivery_Fee + Session.cart.orderUDT.Delivery_Fee_Tax1 + Session.cart.orderUDT.Delivery_Fee_Tax2 + Session.cart.orderUDT.Delivery_Fee_Tax3 + Session.cart.orderUDT.Delivery_Fee_Tax4);
                if (Final_Total_bf_del <= ODCMinOrderAmount && Session.cart.cartHeader.Order_Type_Code == "P")
                {
                    CustomMessageBox.Show(MessageConstant.ODCMinOrderAmount + Convert.ToString(ODCMinOrderAmount), CustomMessageBox.Buttons.OK);
                    return;
                }
                if (!exemitem)
                    {
                        var Min_Order_Mov = APILayer.GetDeliveryFeeMOV(Session.cart.cartHeader.LocationCode,Session.cart.cartHeader.Order_Type_Code,Session.cart.cartHeader.Coupon_Code, decimal.ToDouble(Final_Total_bf_del));

                        if (Min_Order_Mov > 0)
                        {
                        CustomMessageBox.Show(APILayer.GetCatalogText(LanguageConstant.cintMovPrompt)  + Min_Order_Mov, CustomMessageBox.Buttons.OK);
                        //----------show upsell
                        foreach (CartItem cartitem in lstCartItem)
                        {
                            if (cartitem.Combo_Group == 0)
                            {
                                Cartitems = Cartitems + cartitem.Menu_Code + "|" + cartitem.Size_Code + ";";
                            }
                        }

                        UpsellPopUp = SystemSettings.GetSettingValue("UpsellPopUp", Session._LocationCode);
                        if (Session.Upsellcnt == 0 && UpsellPopUp == "1" && Session.selectedOrderType != "P")
                        {
                            var rowcount = APILayer.GetUpsellMenu(Cartitems);
                            if (rowcount != null && rowcount.Count > 0 && Session.RemakeOrder == false)
                            {
                                Session.Upsellcnt = Session.Upsellcnt + 1;
                                using (frmUpsell objfrmUpsell = new frmUpsell())
                                {
                                    if (!Session._LocationCode.Contains("PLK")) objfrmUpsell.ShowDialog();

                                }
                            }
                        }

                        //-------------------
                        return;
                        }
                    }
                //---------------------------------------------------------------

                if (Session.cart.cartHeader.Delayed_Date != DateTime.MinValue)
                {
                    if (Session.cart.cartHeader.Delayed_Date < _pdtmServerDateTime)
                    {
                        if (CustomMessageBox.Show(MessageConstant.InvalidTimedOrderDate, CustomMessageBox.Buttons.YesNo, CustomMessageBox.Icon.Question) == DialogResult.No)
                        {
                            return;
                        }
                        else
                        {
                            Session.cart.cartHeader.Delayed_Date = DateTime.MinValue;
                            Session.cart.cartHeader.Delayed_Order = 0;
                            Session.cart.cartHeader.Order_Date = serverDateTime;
                            Session.cart.cartHeader.Actual_Order_Date = serverDateTime;
                            Session.cart.cartHeader.Kitchen_Display_Time = serverDateTime;
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
                        }

                    }

                }


                foreach (CartItem cartitem in lstCartItem)
                {
                    if (cartitem.Combo_Group == 0)
                    {
                        Cartitems = Cartitems + cartitem.Menu_Code + "|" + cartitem.Size_Code + ";";
                    }
                }

                UpsellPopUp = SystemSettings.GetSettingValue("UpsellPopUp", Session._LocationCode);
                if (Session.Upsellcnt == 0 && UpsellPopUp == "1" && Session.selectedOrderType !="P")
                {
                    var rowcount = APILayer.GetUpsellMenu(Cartitems);
                    if (rowcount != null && rowcount.Count > 0 && Session.RemakeOrder == false)
                    {
                        Session.Upsellcnt = Session.Upsellcnt + 1;
                        using (frmUpsell objfrmUpsell = new frmUpsell())
                        {
                            if(!Session._LocationCode.Contains("PLK")) objfrmUpsell.ShowDialog();

                        }
                    }
                }

                //----------------------ODC Tax Change---------------------------
                if (SystemSettings.GetSettingValue("ODC_Tax_Change", Session._LocationCode) == "1" && Session.selectedOrderType=="P")
                {
                    if (CustomMessageBox.Show(MessageConstant.AllowODCChangeMSG, CustomMessageBox.Buttons.YesNo, CustomMessageBox.Icon.Question) == DialogResult.No)
                    {
                        frmTaxPrompt frmtax = new frmTaxPrompt();
                        frmtax.ShowDialog();
                    }
                }
                //---------------------------------------------------------------

                if (Session.cart.Customer.Customer_City_Code != 1 && (Session.selectedOrderType == "I" || Session.selectedOrderType == "C"))
                {
                    Form frmObj = Application.OpenForms["frmCustomer"];
                    if (frmObj != null) ((frmCustomer)frmObj).ChangeCityToDefault();
                }

                Form frmchk = this.Parent.FindForm();
                if (frmchk.Text == "Customer" || frmchk.Text == "Order")
                {
                    OrderFunctions.HandleAddressControlData();
                }

                

                UserTypes.OrderCompletionState orderCompletionState = new UserTypes.OrderCompletionState();
                orderCompletionState = OrderCompleteFunctions.CheckOrderCompletion();

                if (orderCompletionState == UserTypes.OrderCompletionState.OrderNotComplete)
                {
                    return;
                }
                else if (orderCompletionState == UserTypes.OrderCompletionState.OpenCustomer)
                {
                    Form frm = this.Parent.FindForm();
                    if (frm.Text == "Order")
                    {
                        ((frmOrder)frm).uC_Customer_OrderMenu.cmdDelivery_Info_Click(null, new EventArgs());

                    }
                    return;
                }
                else if (orderCompletionState == UserTypes.OrderCompletionState.OpenOrder)
                {
                    Form frm = this.Parent.FindForm();
                    if (frm.Text == "Customer")
                    {
                        ((frmOrder)frm).uC_Customer_OrderMenu.cmdDelivery_Info_Click(null, new EventArgs());

                    }
                    return;
                }

                if (!PaymentFunctions.PayNow())
                {
                    Form frm = this.Parent.FindForm();
                    if (frm.Text == "Customer")
                    {
                        ((frmOrder)frm).uC_Customer_OrderMenu.cmdDelivery_Info_Click(null, new EventArgs());

                    }
                    return;
                }

                PaymentFunctions.OpenPaymentScreen();

            }
            catch (Exception ex)
            {
                Logger.Trace("ERROR", "uc_customerorderbottommenu-cmdpay_click(): " + ex.Message, ex, true);
            }
        }

        public class UC_CatalogOrderTypes
        {
            public string Location_Code { get; set; }
            public int Workstation_ID { get; set; }
            public string Order_Type_Code { get; set; }
            public string Description { get; set; }
        }

       
    }
}