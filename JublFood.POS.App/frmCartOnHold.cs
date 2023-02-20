using Jublfood.AppLogger;
using JublFood.POS.App.API;
using JublFood.POS.App.BusinessLayer;
using JublFood.POS.App.Cache;
using JublFood.POS.App.Class;
using JublFood.POS.App.Models.Cart;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace JublFood.POS.App
{
    public partial class frmCartOnHold : Form
    {        
        public frmCartOnHold()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void frmCartOnHold_Load(object sender, EventArgs e)
        {
            dgvOnHold.AllowUserToAddRows = false;
            dgvOnHold.Refresh();
            dgvOnHold.ClearSelection();
            //dgvOnHold.CurrentCell = null;
            dgvOnHold.EnableHeadersVisualStyles = false;
            dgvOnHold.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            CartOnHoldResponse cartOnHoldResponse = APILayer.GetCartOnHold();
            if (cartOnHoldResponse != null)
            {
                if (cartOnHoldResponse.cartOnHolds != null)
                {
                    if (cartOnHoldResponse.cartOnHolds.Count > 0)
                    {
                        foreach (CartOnHoldModel model in cartOnHoldResponse.cartOnHolds)
                        {
                            dgvOnHold.Rows.Add(model.Id, Convert.ToDateTime(model.Time).ToString("hh:mm:ss tt"), model.CustomerName, model.CustomerNumber, Decimal.Round(model.OrderAmount, 2).ToString("0.00"), model.OrderTaker, model.Terminal, model.CartId, model.IsActive);
                        }
                        lblList.Visible = true;
                        dgvOnHold.Visible = true;
                        dgvOnHold.Rows[0].Cells[0].Selected = false;
                        lblNoRecord.Visible = false;
                    }
                    else
                    {
                        lblList.Visible = false;
                        dgvOnHold.Visible = false;
                        lblNoRecord.Visible = true;
                    }
                }

                //Remove from CartOnHold Panel once item populated to cart
                if (Session.cart != null)
                {
                    if (Session.cart.cartHeader != null)
                    {
                        if (!string.IsNullOrEmpty(Session.cart.cartHeader.CartId))
                        {
                            for (int i = 0; i < dgvOnHold.Rows.Count; i++)
                            {
                                if (dgvOnHold["CartId", i].Value != null && (dgvOnHold["CartId", i].Value.ToString().Trim() == Session.cart.cartHeader.CartId))
                                {
                                    DataGridViewRow dgvDelRow = dgvOnHold.Rows[i];
                                    dgvOnHold.Rows.Remove(dgvDelRow);
                                }
                            }
                            if (dgvOnHold.Rows.Count == 0)
                            {
                                lblList.Visible = false;
                                dgvOnHold.Visible = false;
                                lblNoRecord.Visible = true;
                            }
                        }
                    }
                }
            }
        }        

        private void dgvOnHold_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            string cartId = string.Empty;
            DataGridViewRow row = dgvOnHold.CurrentRow;
            if (row != null)
            {
                if (!row.IsNewRow)
                {
                    cartId = row.Cells["CartId"].FormattedValue.ToString();
                }
            }
            if (Session.cart != null && Session.cart.cartItems.Count > 0)
            {
                if (CustomMessageBox.Show(MessageConstant.DiscardCart, CustomMessageBox.Buttons.YesNo, CustomMessageBox.Icon.Question) == DialogResult.Yes)
                {
                    //populate cart
                    CartResponse cartreponse = APILayer.GetCart(cartId);
                    if (cartreponse != null)
                    {
                        if (cartreponse.cartItems != null)
                        {
                            Cart cartOnHold = new Cart();
                            cartOnHold.Customer = cartreponse.Customer;
                            cartOnHold.cartHeader = cartreponse.cartHeader;
                            cartOnHold.cartItems = cartreponse.cartItems;
                            cartOnHold.orderUDT = cartreponse.orderUDT;
                            Session.selectedOrderType = cartreponse.cartHeader.Order_Type_Code;

                            if (Session.cart == null)
                            {
                                Session.cart = cartOnHold;
                            }
                            else
                            {
                                Session.cart = cartOnHold;
                            }
                            CartFunctions.FillCartToCustomer();
                            
                            var openFormOrder = Application.OpenForms.Cast<Form>().Where(x => x.Text.ToUpper() == "ORDER").FirstOrDefault();
                            if (openFormOrder != null)
                            {
                                //openFormOrder.Close();
                                //openFormCustomer = null;
                            }
                            var openFormCustomer = Application.OpenForms.Cast<Form>().Where(x => x.Text.ToUpper() == "CUSTOMER").FirstOrDefault();
                            if (openFormCustomer != null)
                            {
                                //openFormCustomer.Close();
                                //openFormCustomer = null;
                            }
                            var objfrmOrder = new frmOrder();
                            objfrmOrder.btnOnHold.Enabled = true;
                            objfrmOrder.uC_Customer_OrderMenu.ConvertExittoCancel();
                            objfrmOrder.StartTimer();
                            objfrmOrder.Show();
                            if (openFormOrder != null) openFormOrder.Hide();
                            if (openFormCustomer != null)
                            {
                                ((frmCustomer)openFormCustomer).loadCustomer(Session.cart.Customer.Phone_Number, Session.cart.Customer.Phone_Ext);
                                openFormCustomer.Hide();
                            }
                            MarkOnHoldOrderInProgress();
                            this.Close();
                            //objfrmOrder.RefreshCartUI();
                        }
                    }
                }
            }
            else
            {
                //populate cart
                CartResponse cartreponse = APILayer.GetCart(cartId);
                if (cartreponse != null)
                {
                    if (cartreponse.cartItems != null)
                    {
                        Cart cartOnHold = new Cart();
                        cartOnHold.Customer = cartreponse.Customer;
                        cartOnHold.cartHeader = cartreponse.cartHeader;
                        cartOnHold.cartItems = cartreponse.cartItems;
                        cartOnHold.orderUDT = cartreponse.orderUDT;
                        Session.selectedOrderType = cartreponse.cartHeader.Order_Type_Code;
                        Session.ODC_Tax = cartreponse.cartHeader.ODC_Tax;

                        if (Session.cart == null)
                        {
                            Session.cart = cartOnHold;
                        }
                        else
                        {
                            Session.cart = cartOnHold;
                        }
                        CartFunctions.FillCartToCustomer();

                        var openFormOrder = Application.OpenForms.Cast<Form>().Where(x => x.Text.ToUpper() == "ORDER").FirstOrDefault();
                        if (openFormOrder != null)
                        {
                            //openFormOrder.Close();
                            //openFormCustomer = null;
                        }
                        var openFormCustomer = Application.OpenForms.Cast<Form>().Where(x => x.Text.ToUpper() == "CUSTOMER").FirstOrDefault();
                        if (openFormCustomer != null)
                        {
                            //openFormCustomer.Close();
                            //openFormCustomer = null;
                        }
                        var objfrmOrder = new frmOrder();
                        objfrmOrder.btnOnHold.Enabled = true;
                        objfrmOrder.uC_Customer_OrderMenu.ConvertExittoCancel();
                        objfrmOrder.StartTimer();
                        objfrmOrder.Show();
                        if (openFormOrder != null) openFormOrder.Hide();
                        if (openFormCustomer != null)
                        {
                            ((frmCustomer)openFormCustomer).loadCustomer(Session.cart.Customer.Phone_Number, Session.cart.Customer.Phone_Ext);
                            openFormCustomer.Hide();
                        }
                        MarkOnHoldOrderInProgress();
                        this.Close();
                        //objfrmOrder.RefreshCartUI();
                    }
                }
            }

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void MarkOnHoldOrderInProgress()
        {
            CartOnHoldRequest cartOnHoldRequest = new CartOnHoldRequest();
            cartOnHoldRequest.CartId = Session.cart.cartHeader.CartId;
            cartOnHoldRequest.Time = DateTime.Now;
            cartOnHoldRequest.CustomerName = Session.cart.Customer.Name;
            cartOnHoldRequest.CustomerNumber = Session.cart.Customer.Phone_Number;
            cartOnHoldRequest.OrderAmount = Convert.ToDecimal(Session.cart.cartHeader.Total);
            cartOnHoldRequest.OrderTaker = Session.CurrentEmployee.LoginDetail.FirstName + " " + Session.CurrentEmployee.LoginDetail.LastName;
            cartOnHoldRequest.Terminal = Session.ComputerName;
            cartOnHoldRequest.IsActive = 2;

            APILayer.UpdateCartOnHold(cartOnHoldRequest);
        }
                
    }
}
