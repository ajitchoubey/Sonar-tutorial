using Jublfood.AppLogger;
using JublFood.POS.App.Cache;
using JublFood.POS.App.Class;
using System;
using System.Drawing;
using System.Windows.Forms;
using JublFood.POS.App.API;
using System.Collections.Generic;
using JublFood.POS.App.BusinessLayer;
using JublFood.POS.App.Models.Order;
using JublFood.POS.App.Models.Catalog;
using System.Data;
using JublFood.POS.App.Models.Cart;
using JublFood.POS.App.Models.Customer;
using Microsoft.VisualBasic.CompilerServices;

namespace JublFood.POS.App
{
    public partial class frmHistory : Form
    {
        
        int OrderCounter = 0;
        int MaxOrder = 0;
        List<CustomerOrderHistory> customerOrderHistories;
        Color paytm_lightcolor = Color.FromArgb(0, 185, 241);
        Color vegColor = Color.Green;
        Color nonVegColor = Color.Red;
        string DesableItemList = Environment.NewLine;
        DataTable dtCart;

        public frmHistory()
        {
            InitializeComponent();
            SetButtonText();
            CheckTrainningMode();
            OrderCounter = 0;
            MaxOrder = 0;
                
        }

      void SetButtonText()
        {
            string labelText = null;

            if (Common.DictAllButtonText.TryGetValue(LanguageConstant.cintNewest, out labelText))
            {
                cmdFirst.Text = labelText;
            }
            if (Common.DictAllButtonText.TryGetValue(LanguageConstant.cintOldest, out labelText))
            {
                cmdLast.Text = labelText;
            }
           
            if (Common.DictAllButtonText.TryGetValue(LanguageConstant.cintCancel, out labelText))
            {
                cmdCancel.Text = labelText;
            }
            if (Common.DictAllButtonText.TryGetValue(LanguageConstant.cintReplace, out labelText))
            {
                cmdReplace.Text = labelText;
            }
            if (Common.DictAllButtonText.TryGetValue(LanguageConstant.cintAdd, out labelText))
            {
                cmdAdd.Text = labelText;
            }
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            this.Close();
           
        }

        private void cmdNext_Click(object sender, EventArgs e)
        {
            if (OrderCounter < MaxOrder)
            {
                OrderCounter = OrderCounter + 1;
                FillGrid(OrderCounter);
                cmdPrevious.Enabled = true;
                cmdFirst.Enabled = true;
                cmdNext.Enabled = true;
                cmdLast.Enabled = true;
            }
            if (OrderCounter == MaxOrder)
            {
                cmdNext.Enabled = false;
                cmdLast.Enabled = false;

            }

        }

        private void cmdPrevious_Click(object sender, EventArgs e)
        {
            if (OrderCounter > 0)
            {
                OrderCounter = OrderCounter - 1;
                FillGrid(OrderCounter);
                cmdPrevious.Enabled = true;
                cmdFirst.Enabled = true;
                cmdNext.Enabled = true;
                cmdLast.Enabled = true;

            }
            if(OrderCounter== 0)
            {
                cmdPrevious.Enabled = false;
                cmdFirst.Enabled = false;

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

                pnl_History.BackColor = color;
            }
            catch (Exception ex)
            {
                Logger.Trace("ERROR", ex.Message, ex, true);
            }
        }

        private void pnl_History_Paint(object sender, PaintEventArgs e)
        {

        }

        private void frmHistory_Load(object sender, EventArgs e)
        {
            try
            {
                OrderCounter = 0;
                MaxOrder = 0;
                customerOrderHistories = new List<CustomerOrderHistory>();
                customerOrderHistories = APILayer.GetCustomerOrderHistory(Session._LocationCode, Convert.ToInt64(Session.cart.Customer.Customer_Code));
                if (customerOrderHistories != null || customerOrderHistories.Count > 0)
                {
                    cmdPrevious.Enabled = false;
                    cmdFirst.Enabled = false;
                    if (customerOrderHistories.Count == 1)
                    {
                        cmdLast.Enabled = false;
                        cmdNext.Enabled = false;
                    }

                    MaxOrder = customerOrderHistories.Count - 1;

                    FillGrid(OrderCounter);

                }
                else
                {
                    cmdFirst.Enabled = false;
                    cmdLast.Enabled = false;
                    cmdNext.Enabled = false;
                    cmdPrevious.Enabled = false;
                    cmdReplace.Enabled = false;
                    cmdAdd.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                Logger.Trace("ERROR", "fromHistory-fromHistory_Load(): " + ex.Message, ex, true);
                //CustomMessageBox.Show(PrintFunctions.System_Error_Message, CustomMessageBox.Buttons.OK, CustomMessageBox.Icon.Warning);
            }
        }

        public void RefreshCartUI()
        {
            if (dtCart != null)
            {
                dtCart.Rows.Clear();
            }
            else
            {
                dtCart = new DataTable();
                dtCart.Columns.Add("CartId");
                dtCart.Columns.Add("Line_Number");
                dtCart.Columns.Add("Menu_Category_Code");
                dtCart.Columns.Add("Menu_Code");
                dtCart.Columns.Add("Size_Code");
                dtCart.Columns.Add("Qty");
                dtCart.Columns.Add("Item");
                dtCart.Columns.Add("VegNVegColor", typeof(Image));
                dtCart.Columns.Add("Price");
                dtCart.Columns.Add("LineType");
                dtCart.Columns.Add("Combo_Group");
                dtCart.Columns.Add("Combo_Item_Number");

                if (dgvCart.Columns.Count > 0) dgvCart.Columns.Clear();
            }

            if (Session.originalcart != null) //&& cart.cartItems[0].Location_Code != null)
            {
                if (Session.originalcart.cartItems.Count > 0)
                {

                    foreach (CartItem cartItem in Session.originalcart.cartItems)
                    {
                        string OptionGroupDescription = "";
                        bool OptionGroupComplete = false;
                        if (cartItem.itemOptionGroups != null && cartItem.itemOptionGroups.Count > 0)
                        {
                            OptionGroupDescription = cartItem.itemOptionGroups[0].Option_Group_Description;
                            OptionGroupComplete = cartItem.itemOptionGroups[0].Option_Group_Complete;
                        }

                        DataRow dr = dtCart.NewRow();
                        dr["CartId"] = cartItem.CartId;
                        dr["Line_Number"] = cartItem.Line_Number;
                        dr["Menu_Category_Code"] = cartItem.Menu_Category_Code;
                        dr["Menu_Code"] = cartItem.Menu_Code;
                        dr["Size_Code"] = cartItem.Size_Code;
                        dr["Qty"] = cartItem.Combo_Group > 0 ? "" : Convert.ToString(cartItem.Quantity);
                        dr["Item"] = cartItem.Combo_Group > 0 ? (UserTypes.TabSpace + cartItem.Size_Description  + " " + cartItem.Menu_Description) : (cartItem.Size_Description + " " + cartItem.Menu_Description);
                        dr["VegNVegColor"] = cartItem.MenuItemType == true ? Properties.Resources.NonVeg : Properties.Resources.Veg;
                        dr["Price"] = cartItem.Combo_Group > 0 ? "" : String.Format(Session.DisplayFormat, cartItem.Price);
                        dr["LineType"] = cartItem.Menu_Item_Type_Code == 2 ? "G" : "M";
                        dr["Combo_Group"] = cartItem.Combo_Group;
                        dr["Combo_Item_Number"] = cartItem.Combo_Item_Number;
                        dtCart.Rows.Add(dr);


                        if (cartItem.itemReasons != null && cartItem.itemReasons.Count > 0)
                        {
                            foreach (ItemReason itemReason in cartItem.itemReasons)
                            {
                                if (itemReason.Reason_Group_Code == 5)
                                {
                                    dr = dtCart.NewRow();
                                    dr["CartId"] = cartItem.CartId;
                                    dr["Line_Number"] = cartItem.Line_Number;
                                    dr["Menu_Category_Code"] = cartItem.Menu_Category_Code;
                                    dr["Menu_Code"] = cartItem.Menu_Code;
                                    dr["Size_Code"] = cartItem.Size_Code;
                                    dr["Qty"] = "";
                                    if (itemReason.Reason_ID == 0)
                                        dr["Item"] = cartItem.Combo_Group > 0 ? (UserTypes.TabSpace + UserTypes.TabSpace + UserTypes.ItemReasonPrefix + itemReason.Other_Information) : (UserTypes.TabSpace + UserTypes.ItemReasonPrefix + itemReason.Other_Information);
                                    else
                                        dr["Item"] = cartItem.Combo_Group > 0 ? (UserTypes.TabSpace + UserTypes.TabSpace + UserTypes.ItemReasonPrefix + itemReason.Reason_Description) : (UserTypes.TabSpace + UserTypes.ItemReasonPrefix + itemReason.Reason_Description);
               
                                    dr["VegNVegColor"] = null;
                                    dr["Price"] = "";
                                    dr["LineType"] = "I";
                                    dr["Combo_Group"] = cartItem.Combo_Group;
                                    dr["Combo_Item_Number"] = cartItem.Combo_Item_Number;
                                    dtCart.Rows.Add(dr);
                                }
                            }
                        }


                        if (!string.IsNullOrEmpty(cartItem.Topping_String))
                        {
                            dr = dtCart.NewRow();
                            dr["CartId"] = cartItem.CartId;
                            dr["Line_Number"] = cartItem.Line_Number;
                            dr["Menu_Category_Code"] = cartItem.Menu_Category_Code;
                            dr["Menu_Code"] = cartItem.Menu_Code;
                            dr["Size_Code"] = cartItem.Size_Code;
                            dr["Qty"] = "";
                            dr["Item"] = cartItem.Combo_Group > 0 ? ((OptionGroupComplete == false && OptionGroupDescription != "") ? (UserTypes.TabSpace + UserTypes.TabSpace + "-->" + OptionGroupDescription) : (UserTypes.TabSpace + UserTypes.TabSpace + cartItem.Topping_String)) : UserTypes.TabSpace + cartItem.Topping_String; //(OptionGroupComplete == false && OptionGroupDescription != "") ? ("-->" + OptionGroupDescription) : (cartItem.Combo_Group > 0 ? UserTypes.TabSpace + UserTypes.TabSpace + cartItem.Topping_String : UserTypes.TabSpace + cartItem.Topping_String;                             
                            dr["VegNVegColor"] = null;
                            dr["Price"] = "";
                            dr["LineType"] = "O";
                            dr["Combo_Group"] = cartItem.Combo_Group;
                            dr["Combo_Item_Number"] = cartItem.Combo_Item_Number;
                            dtCart.Rows.Add(dr);
                        }
                    }
                }

                ComboFunctions.AddCombotoCartUI(ref dtCart,Session.originalcart);

                //if (Session.originalcart.cartHeader.Delivery_Fee > 0)
                //{
                //    DataRow dr = dtCart.NewRow();
                //    dr["CartId"] = Session.originalcart.cartHeader.CartId;
                //    dr["Line_Number"] = "";
                //    dr["Menu_Category_Code"] = "";
                //    dr["Menu_Code"] = "";
                //    dr["Size_Code"] = "";
                //    dr["Qty"] = "";
                //    dr["Item"] = APILayer.GetCatalogText(304);
                //    dr["VegNVegColor"] = null;
                //    dr["Price"] = String.Format(Session.DisplayFormat, Session.originalcart.cartHeader.Delivery_Fee);
                //    dr["LineType"] = "D";
                //    dtCart.Rows.Add(dr);
                //}

                dgvCart.DataSource = dtCart;
                ((DataGridViewImageColumn)dgvCart.Columns["VegNVegColor"]).DefaultCellStyle.NullValue = null;

                //lbltotalAmount.Text = Convert.ToString(Session.originalcart.cartHeader.Final_Total);
                lbltotalAmount.Text = String.Format(Session.DisplayFormat, Session.originalcart.cartHeader.Final_Total);
                

            }
            else
            {
               
                DataRow dr = dtCart.NewRow();
                dtCart.Rows.Add(dr);
                dgvCart.DataSource = dtCart;
                ((DataGridViewImageColumn)dgvCart.Columns["VegNVegColor"]).DefaultCellStyle.NullValue = null;
                
            }

            FormatCartGrid();
        }

        private void FormatCartGrid()
        {
            dgvCart.Columns["Qty"].Width = 30;
            dgvCart.Columns["Price"].Width = 60;
            dgvCart.Columns["VegNVegColor"].Width = 20;

            
            dgvCart.Columns["Item"].Width = 160;

            dgvCart.Columns["CartId"].Visible = false;
            dgvCart.Columns["Line_Number"].Visible = false;
            dgvCart.Columns["Menu_Code"].Visible = false;
            dgvCart.Columns["Size_Code"].Visible = false;
            dgvCart.Columns["Menu_Category_Code"].Visible = false;
            dgvCart.Columns["LineType"].Visible = false;
            dgvCart.Columns["Combo_Group"].Visible = false;
            dgvCart.Columns["Combo_Item_Number"].Visible = false;

            dgvCart.Columns["Qty"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvCart.Columns["Item"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvCart.Columns["Price"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvCart.Columns["VegNVegColor"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgvCart.Columns["Qty"].SortMode = DataGridViewColumnSortMode.NotSortable;
            dgvCart.Columns["Item"].SortMode = DataGridViewColumnSortMode.NotSortable;
            dgvCart.Columns["Price"].SortMode = DataGridViewColumnSortMode.NotSortable;
            dgvCart.Columns["VegNVegColor"].SortMode = DataGridViewColumnSortMode.NotSortable;

            Session.CatalogCartCaptions = APILayer.GetCartCaptions();
            if (dgvCart.Columns.Count > 0)
            {
                dgvCart.Columns["Qty"].HeaderText = Session.CatalogCartCaptions.Qty;
                dgvCart.Columns["Item"].HeaderText = Session.CatalogCartCaptions.Item;
                dgvCart.Columns["Price"].HeaderText = Session.CatalogCartCaptions.Price;
                dgvCart.Columns["VegNVegColor"].HeaderText = string.Empty;
            }


            if (dgvCart != null && dgvCart.Columns.Contains("LineType"))
            {
                for (int i = 0; i <= dgvCart.RowCount - 1; i++)
                {
                    if (Convert.ToString(dgvCart.Rows[i].Cells["Item"].Value).Contains("-->"))
                        dgvCart.Rows[i].DefaultCellStyle.ForeColor = Color.Red;
                    else
                        dgvCart.Rows[i].DefaultCellStyle.ForeColor = Color.Black;
                }
            }

            if (dgvCart != null && dgvCart.RowCount > 0)
            {
                dgvCart.FirstDisplayedScrollingRowIndex = dgvCart.Rows.Count - 1;

                if (dgvCart.Rows.Count != dgvCart.Rows.GetRowCount(DataGridViewElementStates.Displayed))
                {
                    //btn_Down.Enabled = true;
                    //btn_Up.Enabled = true;
                }
                else
                {
                    //btn_Down.Enabled = false;
                    //btn_Up.Enabled = false;
                }
            }
        }

        public void FillGrid(int orderCounter)
        {
            try
            {
                string strOrder = string.Empty;
                Common.DictAllButtonText.TryGetValue(LanguageConstant.cintOrder, out strOrder);
                OrderFunctions.LoadOrderDetails(customerOrderHistories[orderCounter].Order_Number, customerOrderHistories[orderCounter].Order_Date, false, true, false, false, Session.cart.Customer.Phone_Number);
                lblOrderInfo.Text = strOrder + " # " + customerOrderHistories[orderCounter].Order_Number + " - " + customerOrderHistories[orderCounter].Order_Date.ToString("dd-MMM-yyyy") + " - " + customerOrderHistories[orderCounter].Order_Type;
                RefreshCartUI();
            }
            catch(Exception ex)
            {
                Logger.Trace("ERROR", "fromHistory-FillGrid(): " + ex.Message, ex, true);
            }
        }

        private void cmdLast_Click(object sender, EventArgs e)
        {
            try
            {
                OrderCounter = MaxOrder;
                FillGrid(OrderCounter);
                cmdFirst.Enabled = true;
                cmdPrevious.Enabled = true;
                cmdNext.Enabled = false;
                cmdLast.Enabled = false;
            }
            catch (Exception ex)
            {
                Logger.Trace("ERROR", "fromHistory-cmdLast_Click(): " + ex.Message, ex, true);
            }

        }

        private void cmdFirst_Click(object sender, EventArgs e)
        {
            try
            {
                if (OrderCounter > 0)
                {
                    OrderCounter = 0;
                    FillGrid(OrderCounter);
                    cmdFirst.Enabled = false;
                    cmdPrevious.Enabled = false;
                    cmdNext.Enabled = true;
                    cmdLast.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                Logger.Trace("ERROR", "fromHistory-cmdFirst_Click(): " + ex.Message, ex, true);
            }

        }

        private void lbltotalAmount_TextAlignChanged(object sender, EventArgs e)
        {

        }

        private void cmdAdd_Click(object sender, EventArgs e)
        {
            try
            {
                List<int> Indices = CartFunctions.ItemsToExcludeFromCart();
                if (Indices != null && Indices.Count > 0)
                {
                    for (int i = 0; i <= Session.originalcart.cartItems.Count - 1; i++)
                    {
                        if (Indices.Contains(i))
                        {

                            DesableItemList = DesableItemList + Session.originalcart.cartItems[i].Description + Environment.NewLine;
                        }
                    }
                    CustomMessageBox.Show(MessageConstant.EnableToAddReplace + Session.selectedOrderType + DesableItemList, CustomMessageBox.Buttons.OK, CustomMessageBox.Icon.Exclamation);
                    //return;
                }
                //else
                //{
                CartFunctions.GenerateOrderCart(false,Indices);
                    Session.originalcart = (new Cart().GetCartWithDefaultValues());
                    this.Close();
                    //RefreshCartUI();
                //}
            }
            catch (Exception ex)
            {
                Logger.Trace("ERROR", "fromHistory-cmdAdd_Click(): " + ex.Message, ex, true);
            }

        }

        private void cmdReplace_Click(object sender, EventArgs e)
        {

            try
            {
                List<int> Indices = CartFunctions.ItemsToExcludeFromCart();
                if (Indices != null && Indices.Count > 0)
                {
                    for (int i = 0; i <= Session.originalcart.cartItems.Count - 1; i++)
                    {
                        if (Indices.Contains(i))
                        {

                            DesableItemList = DesableItemList + Session.originalcart.cartItems[i].Description + Environment.NewLine;
                        }
                    }
                    CustomMessageBox.Show(MessageConstant.EnableToAddReplace + Session.selectedOrderType + DesableItemList, CustomMessageBox.Buttons.OK, CustomMessageBox.Icon.Exclamation);
                    //return;
                }
                //else
                //{
                CartFunctions.GenerateOrderCart(true, Indices);
                Session.originalcart = (new Cart().GetCartWithDefaultValues());
                this.Close();
                //}
            }
            catch (Exception ex)
            {
                Logger.Trace("ERROR", "fromHistory-cmdReplace_Click(): " + ex.Message, ex, true);
            }
        }

        

        


        

        private void frmHistory_Activated(object sender, EventArgs e)
        {
            //OrderCounter = 0;
            //MaxOrder = 0;
        }

        

    }
}
