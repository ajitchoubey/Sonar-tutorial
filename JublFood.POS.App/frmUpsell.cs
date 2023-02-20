using Jublfood.AppLogger;
using JublFood.POS.App.API;
using JublFood.POS.App.BusinessLayer;
using JublFood.POS.App.Cache;
using JublFood.POS.App.Class;
using JublFood.POS.App.Models.Cart;
using JublFood.POS.App.Models.Catalog;
using JublFood.POS.App.Models.Employee;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace JublFood.POS.App
{
    public partial class frmUpsell : Form
    {
        Color vegColor = Color.Green;
        Color nonVegColor = Color.Red;
        public bool AllVegItem { get; set; }
        string Cart_items_Upsell = string.Empty, Cart_items = string.Empty;
        int totalSelectedItem;
        List<CatalogUpsellMenu> lstCatalogUpsellMenu;
        private bool ALT_F4 = false;

        public frmUpsell()
        {
            InitializeComponent();
            // spUpsellMenuList
            LoadForm();

        }

        private void LoadForm()
        {
            totalSelectedItem = 0;
            AllVegItem = true;

            List<CartItem> lstCartItem = Session.cart.cartItems;
            lbltxtsubtotal.Text = Session.cart.cartHeader.SubTotal.ToString();
            try
            {
                foreach (CartItem cartitem in lstCartItem)
                {
                    if (!string.IsNullOrEmpty(cartitem.Combo_Menu_Code))
                    {
                        cartitem.Menu_Category_Code = "MCT0006";
                        Cart_items_Upsell = Cart_items_Upsell + cartitem.Combo_Menu_Code + "|" + cartitem.Combo_Size_Code + "|" + cartitem.Menu_Category_Code + ";";
                    }
                    else
                    {
                        Cart_items = Cart_items + cartitem.Menu_Code + "|" + cartitem.Size_Code + ";";
                        Cart_items_Upsell = Cart_items_Upsell + cartitem.Menu_Code + "|" + cartitem.Size_Code + "|" + cartitem.Menu_Category_Code + ";";
                    }
                }

                //if (Session.selectedMenuItems != null)
                //{
                //    foreach (KeyValuePair<string, Nullable<Boolean>> item in Session.selectedMenuItems)
                //    {
                //        //check if cart contains any non-veg item
                //        if (item.Value == true)
                //        {
                //            AllVegItem = false;
                //        }
                //    }
                //}

                foreach (CartItem cartitem in lstCartItem)
                {
                    if(cartitem.MenuItemType == true)
                    {
                        AllVegItem = false;
                    }
                }

                lstCatalogUpsellMenu = new List<CatalogUpsellMenu>();
                lstCatalogUpsellMenu = APILayer.GetUpsellMenu(Cart_items_Upsell);
                LoadMenu(lstCatalogUpsellMenu);
            }
            catch (Exception ex)
            {
                Logger.Trace("ERROR", "frmUpsell-loadForm(): " + ex.Message, ex, true);
            }
        }
        private void LoadMenu(List<CatalogUpsellMenu> lstCatalogUpsellMenu)
        {
            try
            {
                foreach (CatalogUpsellMenu catalogUpsellMenu in lstCatalogUpsellMenu)
                {
                    string ItemUniqueCode = catalogUpsellMenu.Menu_Code + "~" + catalogUpsellMenu.Size_Code + "~" + catalogUpsellMenu.location_code;
                    Panel pnl = new Panel { Width = 125, Height = 120, Name = "pnl_" + ItemUniqueCode };
                    Button btnItem = new Button { Width = 115, Height = 80, BackColor = Color.PeachPuff, Location = new Point(3, 3), Text = (catalogUpsellMenu.description == "." ? catalogUpsellMenu.order_description : catalogUpsellMenu.description + "  " + catalogUpsellMenu.order_description), Name = "btn_" + ItemUniqueCode };
                    btnItem.Click += new EventHandler(btn_QuantityClick);

                    //color classification of Veg/Non-Veg items in Upsell pop up
                    if (catalogUpsellMenu.MenuItemType == false)
                    {
                        btnItem.FlatStyle = FlatStyle.Flat;
                        btnItem.FlatAppearance.BorderColor = vegColor;
                        btnItem.FlatAppearance.BorderSize = 4;
                    }
                    else if (catalogUpsellMenu.MenuItemType == true)
                    {
                        btnItem.FlatStyle = FlatStyle.Flat;
                        btnItem.FlatAppearance.BorderColor = nonVegColor;
                        btnItem.FlatAppearance.BorderSize = 4;
                    }
                    else if (catalogUpsellMenu.MenuItemType == null)
                    {
                        btnItem.BackColor = Color.PeachPuff;
                    }

                    Button btnPlus = new Button { Width = 42, Height = 40, BackColor = Color.PeachPuff, Location = new Point(76, 80), Name = "btn_plus" + ItemUniqueCode, Text = "+", Font = new Font("Arial", 16, FontStyle.Bold) };
                    btnPlus.Click += new EventHandler(btn_QuantityClick);
                    Button btnminus = new Button { Width = 42, Height = 40, BackColor = Color.PeachPuff, Location = new Point(3, 80), Name = "btn_minus" + ItemUniqueCode, Text = "-", Font = new Font("Arial", 16, FontStyle.Bold), Enabled = false };
                    btnminus.Click += new EventHandler(btn_QuantityClick);
                    Label lblQuanity = new Label { Width = 40, Height = 40, Text = "0", Location = new Point(55, 95), Name = "lbl_" + ItemUniqueCode, ForeColor = Color.White, Font = new Font(this.Font, FontStyle.Bold) };
                    Label lblCoupon = new Label { Width = 105, Height = 18, BackColor = Color.PeachPuff, Location = new Point(7, 60), Name = "lblcoupon_" + ItemUniqueCode, ForeColor = Color.Blue,TextAlign = ContentAlignment.MiddleCenter };
                    //List<CatalogUpsellMenuCoupon> catalogUpsellMenuCoupons = APILayer.GetUpsellMenuCoupon(catalogUpsellMenu.Menu_Code, catalogUpsellMenu.Size_Code, Cart_items);
                    if (catalogUpsellMenu.Coupon_Code != null)
                    {
                        lblCoupon.Text = catalogUpsellMenu.Coupon_Description;
                        lblCoupon.Tag = catalogUpsellMenu.Coupon_Code;
                    }

                    //If the cart contains all Veg items then, Non-Veg items will not be shown in the upsell pop up (not dependent on VegOnly option)
                    if (AllVegItem)
                    {
                        if (btnItem.FlatAppearance.BorderColor == nonVegColor)
                        {
                            //btnItem.Visible = false;
                            //btnPlus.Visible = false;
                            //btnminus.Visible = false;
                            //lblQuanity.Visible = false;
                            //lblCoupon.Visible = false;

                            pnl.Controls.Remove(btnItem);
                            pnl.Controls.Remove(btnPlus);
                            pnl.Controls.Remove(btnminus);
                            pnl.Controls.Remove(lblQuanity);
                            pnl.Controls.Remove(lblCoupon);
                            flPanel.Controls.Remove(pnl);
                        }
                        else
                        {
                            pnl.Controls.Add(btnItem);
                            pnl.Controls.Add(btnPlus);
                            pnl.Controls.Add(btnminus);
                            pnl.Controls.Add(lblQuanity);
                            pnl.Controls.Add(lblCoupon);
                            flPanel.Controls.Add(pnl);
                        }
                    }
                    else
                    {
                        //btnItem.Visible = true;
                        //btnPlus.Visible = true;
                        //btnminus.Visible = true;
                        //lblQuanity.Visible = true;
                        //lblCoupon.Visible = true;

                        pnl.Controls.Add(btnItem);
                        pnl.Controls.Add(btnPlus);
                        pnl.Controls.Add(btnminus);
                        pnl.Controls.Add(lblQuanity);
                        pnl.Controls.Add(lblCoupon);
                        flPanel.Controls.Add(pnl);
                    }
                    //pnl.Controls.Add(btnItem);
                    //pnl.Controls.Add(btnPlus);
                    //pnl.Controls.Add(btnminus);
                    //pnl.Controls.Add(lblQuanity);
                    //pnl.Controls.Add(lblCoupon);
                    //flPanel.Controls.Add(pnl);
                    lblCoupon.BringToFront();
                }
            }
            catch (Exception ex)
            {
                Logger.Trace("ERROR", "frmUpsell-loadmenu(): " + ex.Message, ex, true);
            }
        }


        protected void btn_QuantityClick(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            Label tempLabel; Button tempButton;
            Label lblcoupon;
            try
            {
                if (btn.Text == "+")
                {
                    tempLabel = ((Label)flPanel.Controls["pnl_" + btn.Name.Replace("btn_plus", "")].Controls["lbl_" + btn.Name.Replace("btn_plus", "")]);
                    tempButton = ((Button)flPanel.Controls["pnl_" + btn.Name.Replace("btn_plus", "")].Controls["btn_" + btn.Name.Replace("btn_plus", "")]);
                    lblcoupon = ((Label)flPanel.Controls["pnl_" + btn.Name.Replace("btn_plus", "")].Controls["lblcoupon_" + btn.Name.Replace("btn_plus", "")]);
                    tempButton.BackColor = Color.FromArgb(255, 128, 128);
                    lblcoupon.BackColor = Color.FromArgb(255, 128, 128);
                    tempLabel.Text = Convert.ToString(Convert.ToInt32(tempLabel.Text) + 1);
                    Button btnMinus = ((Button)flPanel.Controls["pnl_" + btn.Name.Replace("btn_plus", "")].Controls["btn_minus" + btn.Name.Replace("btn_plus", "")]);
                    btnMinus.Enabled = true;
                    TotalCount(0);
                }
                else if (btn.Text == "-")
                {
                    tempLabel = ((Label)flPanel.Controls["pnl_" + btn.Name.Replace("btn_minus", "")].Controls["lbl_" + btn.Name.Replace("btn_minus", "")]);
                    tempButton = ((Button)flPanel.Controls["pnl_" + btn.Name.Replace("btn_minus", "")].Controls["btn_" + btn.Name.Replace("btn_minus", "")]);
                    lblcoupon = ((Label)flPanel.Controls["pnl_" + btn.Name.Replace("btn_minus", "")].Controls["lblcoupon_" + btn.Name.Replace("btn_minus", "")]);

                    if (tempLabel.Text == "0")
                    {
                        tempButton.BackColor = Color.PeachPuff;
                        lblcoupon.BackColor = Color.PeachPuff;
                        btn.Enabled = false;
                    }
                    else
                    {
                        tempLabel.Text = Convert.ToString(Convert.ToInt32(tempLabel.Text) - 1);
                        if (tempLabel.Text == "0")
                        {

                            tempButton.BackColor = Color.PeachPuff;
                            lblcoupon.BackColor = Color.PeachPuff;
                            btn.Enabled = false;
                        }
                    }
                    TotalCount(1);
                }
                else
                {
                    tempLabel = ((Label)flPanel.Controls["pnl_" + btn.Name.Replace("btn_", "")].Controls["lbl_" + btn.Name.Replace("btn_", "")]);
                    lblcoupon = ((Label)flPanel.Controls["pnl_" + btn.Name.Replace("btn_", "")].Controls["lblcoupon_" + btn.Name.Replace("btn_", "")]);
                    lblcoupon.BackColor = Color.FromArgb(255, 128, 128);
                    btn.BackColor = Color.FromArgb(255, 128, 128);
                    tempLabel.Text = Convert.ToString(Convert.ToInt32(tempLabel.Text) + 1);
                    Button btnMinus = ((Button)flPanel.Controls["pnl_" + btn.Name.Replace("btn_", "")].Controls["btn_minus" + btn.Name.Replace("btn_", "")]);
                    btnMinus.Enabled = true;
                    TotalCount(0);
                }
            }
            catch (Exception ex)
            {
                Logger.Trace("ERROR", "frmUpsell-btn_quantityclick(): " + ex.Message, ex, true);
            }
        }

        private void cmdSkip_Click(object sender, EventArgs e)
        {
            //Commented by Vikas Saraswat
            //int checkpassword = ValiddatePassword();
            //if (checkpassword == 1)
            //{
            //    this.Close();

            //    foreach (Form form in Application.OpenForms)
            //    {

            //        form.Hide();

            //    }

            //    //frmPayment frmPayment = new frmPayment();
            //    //frmPayment.ShowDialog();
            //    //this.Close();
            //}
            this.Close();
        }

        private void Cmdaddcart_Click(object sender, EventArgs e)
        {
            CatalogUpsellMenu catalogUpsellMenu;
            try
            {
                foreach (Panel pnl in flPanel.Controls)
                {                    
                    string uniqueItem = pnl.Name.Replace("pnl_", "");
                    Label quantity = (Label)pnl.Controls["lbl_" + uniqueItem];
                    if (Convert.ToInt32(quantity.Text) > 0)
                    {
                        string[]  itemdetails = uniqueItem.Split('~');
                        catalogUpsellMenu = lstCatalogUpsellMenu.Where(o => o.Menu_Code == itemdetails[0] && o.Size_Code == itemdetails[1] && o.location_code == itemdetails[2]).FirstOrDefault();
                        //  catalogUpsellMenu.price = catalogUpsellMenu.price * Convert.ToInt32(quantity.Text);
                        CartFunctions.GetCartUpsell(catalogUpsellMenu, Convert.ToInt32(quantity.Text));

                        Label lblCoupon = (Label)pnl.Controls["lblcoupon_" + uniqueItem];
                        if (Convert.ToString(lblCoupon.Tag) != "")
                        {
                            CouponFunctions.AddUpsellCouponFromUpsellScreen(Convert.ToString(lblCoupon.Tag), Convert.ToString(itemdetails[0]), Convert.ToString(itemdetails[1]));
                        }
                    }

                    

                }
                
                
                //Commented by Vikas Saraswat
                //foreach (Form form in Application.OpenForms)
                //{

                //    form.Hide();

                //}

                //Chkpwd:
                //int checkpassword = ValiddatePassword();
                //if (checkpassword == 1)
                //{
                Form lastOpenedForm = Application.OpenForms["frmOrder"];
                if (lastOpenedForm != null)
                {
                    frmOrder frm = (frmOrder)lastOpenedForm;
                    frm.RefreshCartUI();
                    this.Close();

                }
                //}
                //else
                //{
                //    goto Chkpwd;
                //}


            }
            catch (Exception ex)
            {
                Logger.Trace("ERROR", "frmUpsell-cmdaddcart_click(): " + ex.Message, ex, true);
            }
        }

        private void frmUpsell_KeyDown(object sender, KeyEventArgs e)
        {
            ALT_F4 = (e.KeyCode.Equals(Keys.F4) && e.Alt == true);
        }

        private void frmUpsell_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (ALT_F4)
            {
                ALT_F4 = false;
                e.Cancel = true;
                return;
            }
        }

        private void TotalCount(int cart)
        {

            if (cart == 0)
            {
                totalSelectedItem = totalSelectedItem + 1;
                lbltxtquantity.Text = totalSelectedItem.ToString();
            }
            else
            {
                totalSelectedItem = totalSelectedItem - 1;
                lbltxtquantity.Text = totalSelectedItem.ToString();
            }
            try
            {
                lbltxtitemcost.Text = "0";
                lbltxtsubtotal1.Text = "0";
                foreach (Panel pnl in flPanel.Controls)
                {
                    string uniqueItem = pnl.Name.Replace("pnl_", "");
                    Label quantity = (Label)pnl.Controls["lbl_" + uniqueItem];
                    string[] itemdetails = uniqueItem.Split('~');
                    var results = lstCatalogUpsellMenu.Where(o => o.Menu_Code == itemdetails[0] && o.Size_Code == itemdetails[1] && o.location_code == itemdetails[2]).FirstOrDefault();
                    lbltxtitemcost.Text = Convert.ToString(Convert.ToDecimal(lbltxtitemcost.Text) + (Convert.ToDecimal(results.price) * Convert.ToDecimal(quantity.Text)));
                    lbltxtsubtotal1.Text = Convert.ToString(Convert.ToDecimal(lbltxtsubtotal.Text) + Convert.ToDecimal(lbltxtitemcost.Text));
                    lbltxtitemcost.Text = Convert.ToDecimal(lbltxtitemcost.Text).ToString("N0");
                    lbltxtsubtotal1.Text = Convert.ToDecimal(lbltxtsubtotal1.Text).ToString("N0");
                }
            }
            catch (Exception ex)
            {
                Logger.Trace("ERROR", "frmUpsell-totalcount(): " + ex.Message, ex, true);
            }
        }
    }
}
