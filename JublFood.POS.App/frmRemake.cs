using Jublfood.AppLogger;
using JublFood.POS.App.API;
using JublFood.POS.App.BusinessLayer;
using JublFood.POS.App.Cache;
using JublFood.POS.App.Class;
using JublFood.POS.App.Models.Cart;
using JublFood.POS.App.Models.Catalog;
using JublFood.POS.App.Models.Order;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;


namespace JublFood.POS.App
{
    public partial class frmRemake : Form
    {
        private bool ALT_F4 = false;
        static int[] Item_Qty = new int[200];
        static int[] Process_Qty = new int[200];
        int OrderCounter = 0;
        int MaxOrder = 0;
        List<CustomerOrderRemake> customerOrderRemakes;
        Color paytm_lightcolor = Color.FromArgb(0, 185, 241);
        Color vegColor = Color.Green;
        Color nonVegColor = Color.Red;
        int selectedLineNumber = -1;
        string CurrentLineType = "";
        public static string Remake_Order_Type;
        public int Old_Order_Number;
        DataTable dtCart;

        public int MaxOrder1 { get => MaxOrder; set => MaxOrder = value; }
        public enum enumReasonGroupID
        {
            BadOrder = 1,
            VoidOrder = 2,
            AbandonOrder = 3,
            Coupon = 4,
            CookingInstruction = 5,
            Remake = 11

        }
        List<CatalogReasons> lstCatalogReasons = new List<CatalogReasons>();
        public frmRemake()
        {
            InitializeComponent();
            SetButtonText();
            CheckTrainningMode();

            OrderCounter = 0;
            MaxOrder1 = 0;
            Combox_Item_Display();





        }
        void Combox_Item_Display()
        {
            try
            {

                lstCatalogReasons = APILayer.GetCatalogReasons(SystemSettings.settings.pstrDefault_Location_Code, Convert.ToInt32(Session.CurrentEmployee.LoginDetail.LanguageCode), Convert.ToInt16(enumReasonGroupID.Remake));
                comReason.Items.Clear();
                comReason.Items.Add("Please select reason");
                foreach (CatalogReasons item in lstCatalogReasons)
                {

                    comReason.Items.Add(item.System_Text);
                }
                comReason.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                Logger.Trace("ERROR", "frmRemake-combox_item_display(): " + ex.Message, ex, true);
                CustomMessageBox.Show(PrintFunctions.System_Error_Message, CustomMessageBox.Buttons.OK, CustomMessageBox.Icon.Warning);

            }

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
            Session.RemakeOrder = false;
            //Session.handleRemakebutton = false;
            this.Close();
        }

        private void cmdNext_Click(object sender, EventArgs e)
        {
            if (OrderCounter < MaxOrder1)
            {
                OrderCounter = OrderCounter + 1;
                FillGrid(OrderCounter);
            }

        }

        private void cmdPrevious_Click(object sender, EventArgs e)
        {
            if (OrderCounter > 0)
            {
                OrderCounter = OrderCounter - 1;
                FillGrid(OrderCounter);

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
                Logger.Trace("ERROR", "frmRemake-trainning(): " + ex.Message, ex, true);
                CustomMessageBox.Show(PrintFunctions.System_Error_Message, CustomMessageBox.Buttons.OK, CustomMessageBox.Icon.Warning);

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
                MaxOrder1 = 0;
                customerOrderRemakes = new List<CustomerOrderRemake>();
                customerOrderRemakes = APILayer.GetCustomerOrderRemake(Session._LocationCode, Convert.ToInt64(Session.cart.Customer.Customer_Code));
                Session.RemakeOrder = true;
                if (Convert.ToInt64(Session.cart.Customer.Customer_Code) == 0)
                {
                    CustomMessageBox.Show("There are no  remake order availables for this customer today. ", CustomMessageBox.Buttons.OK, CustomMessageBox.Icon.Warning);
                    Session.RemakeOrder = false;
                    this.Close();
                    return;
                }
                //if (customerOrderRemakes != null || customerOrderRemakes.Count > 0)
                if (customerOrderRemakes.Count > 0)
                {
                    //cmdFirst.Enabled = false;
                    //cmdPrevious.Enabled = false;

                    if (customerOrderRemakes.Count == 1)
                    {
                        cmdLast.Enabled = false;
                        cmdNext.Enabled = false;
                    }

                    MaxOrder1 = customerOrderRemakes.Count - 1;

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
                Logger.Trace("ERROR", "frmRemake-frmHistory_load(): " + ex.Message, ex, true);
                CustomMessageBox.Show(PrintFunctions.System_Error_Message, CustomMessageBox.Buttons.OK, CustomMessageBox.Icon.Warning);

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
                        dr["Item"] = cartItem.Combo_Group > 0 ? (UserTypes.TabSpace + cartItem.Size_Description + " " + cartItem.Menu_Description) : (cartItem.Size_Description + " " + cartItem.Menu_Description);
                        dr["VegNVegColor"] = cartItem.MenuItemType == true ? Properties.Resources.NonVeg : Properties.Resources.Veg;
                        dr["Price"] = cartItem.Combo_Group > 0 ? "" : String.Format(Session.DisplayFormat, (Convert.ToDecimal(cartItem.Quantity) * cartItem.Menu_Price));
                        dr["LineType"] = cartItem.Menu_Item_Type_Code == 2 ? "G" : "M";
                        dr["Combo_Group"] = cartItem.Combo_Group;
                        dr["Combo_Item_Number"] = cartItem.Combo_Item_Number;
                        dtCart.Rows.Add(dr);

                        if (cartItem.itemReasons != null && cartItem.itemReasons.Count > 0)
                        {
                            foreach (ItemReason itemReason in cartItem.itemReasons)
                            {
                                dr = dtCart.NewRow();
                                dr["CartId"] = cartItem.CartId;
                                dr["Line_Number"] = cartItem.Line_Number;
                                dr["Menu_Category_Code"] = cartItem.Menu_Category_Code;
                                dr["Menu_Code"] = cartItem.Menu_Code;
                                dr["Size_Code"] = cartItem.Size_Code;
                                dr["Qty"] = "";
                                dr["Item"] = cartItem.Combo_Group > 0 ? (UserTypes.TabSpace + UserTypes.TabSpace + UserTypes.ItemReasonPrefix + itemReason.Reason_Description) : (UserTypes.TabSpace + UserTypes.ItemReasonPrefix + itemReason.Reason_Description);
                                dr["VegNVegColor"] = null;
                                dr["Price"] = "";
                                dr["LineType"] = "I";
                                dr["Combo_Group"] = cartItem.Combo_Group;
                                dr["Combo_Item_Number"] = cartItem.Combo_Item_Number;
                                dtCart.Rows.Add(dr);
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

                //UserFunctions.AddCombotoCartUI(ref dtCart,Session.originalcart);
                //ComboFunctions.AddCombotoCartUI(ref dtCart, Session.originalcart);




                if (Session.originalcart.cartHeader.Delivery_Fee > 0)
                {
                    DataRow dr = dtCart.NewRow();
                    dr["CartId"] = Session.originalcart.cartHeader.CartId;
                    dr["Line_Number"] = "";
                    dr["Menu_Category_Code"] = "";
                    dr["Menu_Code"] = "";
                    dr["Size_Code"] = "";
                    dr["Qty"] = "";
                    dr["Item"] = APILayer.GetCatalogText(304);
                    dr["VegNVegColor"] = null;
                    dr["Price"] = String.Format(Session.DisplayFormat, Session.originalcart.cartHeader.Delivery_Fee);
                    dr["LineType"] = "D";
                    dtCart.Rows.Add(dr);
                }

                //if (Session.originalcart.cartHeader.Order_Adjustments > 0)
                //{
                //    DataRow dr = dtCart.NewRow();
                //    dr["CartId"] = Session.cart.cartHeader.CartId;
                //    dr["Line_Number"] = "";
                //    dr["Menu_Category_Code"] = "";
                //    dr["Menu_Code"] = "";
                //    dr["Size_Code"] = "";
                //    dr["Qty"] = "";
                //    dr["Item"] = Session.selectedOrderCoupon.Description;
                //    dr["VegNVegColor"] = null;
                //    dr["Price"] = String.Format(Session.DisplayFormat, (-1) * Session.cart.cartHeader.Order_Adjustments);
                //    dr["LineType"] = "E";
                //    dtCart.Rows.Add(dr);
                //}
                //if (Session.originalcart.cartHeader.Order_Coupon_Total > 0)
                //{
                //    DataRow dr = dtCart.NewRow();
                //    dr["CartId"] = Session.cart.cartHeader.CartId;
                //    dr["Line_Number"] = "";
                //    dr["Menu_Category_Code"] = "";
                //    dr["Menu_Code"] = "";
                //    dr["Size_Code"] = "";
                //    dr["Qty"] = "";
                //    dr["Item"] = Session.selectedOrderCoupon.Description;
                //    dr["VegNVegColor"] = null;
                //    dr["Price"] = String.Format(Session.DisplayFormat, (-1) * Session.cart.cartHeader.Order_Coupon_Total);
                //    dr["LineType"] = "E";
                //    dtCart.Rows.Add(dr);
                //}


                dgvCart.DataSource = dtCart;
                ((DataGridViewImageColumn)dgvCart.Columns["VegNVegColor"]).DefaultCellStyle.NullValue = null;

                //lbltotalAmount.Text = Convert.ToString(Session.originalcart.cartHeader.Final_Total);
                //lbltotalAmount.Text = String.Format(Session.DisplayFormat, Session.originalcart.cartHeader.SubTotal-Session.originalcart.cartHeader.Delivery_Fee + Session.originalcart.cartHeader.Order_Adjustments  );
                lbltotalAmount.Text = String.Format(Session.DisplayFormat, Session.originalcart.cartHeader.SubTotal - Session.originalcart.cartHeader.Delivery_Fee);


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
                if (customerOrderRemakes.Count == 0)
                {
                    CustomMessageBox.Show("There are no  remake order availables for this customer today. ", CustomMessageBox.Buttons.OK, CustomMessageBox.Icon.Warning);
                    Session.RemakeOrder = false;
                    this.Close();
                    return;

                }
                string strOrder = string.Empty;
                Common.DictAllButtonText.TryGetValue(LanguageConstant.cintOrder, out strOrder);
                OrderFunctions.LoadOrderDetails(customerOrderRemakes[orderCounter].Order_Number, customerOrderRemakes[orderCounter].Order_Date, false, false, false, false, Session.cart.Customer.Phone_Number);
                lblOrderInfo.Text = strOrder + " # " + customerOrderRemakes[orderCounter].Order_Number + " - " + customerOrderRemakes[orderCounter].Order_Date.ToString("dd-MMM-yyyy") + " - " + "Good Order";
                /*customerOrderRemakes[orderCounter].Order_Type;*/
                Old_Order_Number = Convert.ToInt32(customerOrderRemakes[orderCounter].Order_Number);

                if (Session.originalcart != null && Session.originalcart.cartItems != null && Session.originalcart.cartItems.Count > 0)
                {
                    foreach (CartItem cartItem in Session.originalcart.cartItems)
                    {
                        cartItem.Combo_Group = 0;
                        cartItem.Price = Convert.ToDecimal(cartItem.Quantity) * cartItem.Menu_Price;
                        cartItem.SubTotal = cartItem.Price;
                    }
                    Session.originalcart.cartHeader.SubTotal = 0;
                    foreach (CartItem cartItem in Session.originalcart.cartItems)
                    {
                        Session.originalcart.cartHeader.SubTotal = Session.originalcart.cartHeader.SubTotal + cartItem.SubTotal;

                    }
                    Session.originalcart.cartHeader.Total = Session.originalcart.cartHeader.SubTotal;

                }


                RefreshCartUI();
                int i = 0;
                i = 1;
                Remake_Order_Type = Session.originalcart.cartHeader.Order_Type_Code;

                //Remake_Order_Type
                foreach (CartItem cartItem in Session.originalcart.cartItems)
                {
                    Item_Qty[i] = Convert.ToInt32(cartItem.Quantity);
                    i = i + 1;

                }

                Int64 Total;
                Total = 0;
                foreach (CartItem cartItem in Session.originalcart.cartItems)
                {
                    Total = Convert.ToInt64(Session.originalcart.cartHeader.SubTotal) + Total;

                }
                btn_Quantity.PerformClick();

            }
            catch (Exception ex)
            {
                Logger.Trace("ERROR", "frmRemake-fillgrid(): " + ex.Message, ex, true);
                CustomMessageBox.Show(PrintFunctions.System_Error_Message, CustomMessageBox.Buttons.OK, CustomMessageBox.Icon.Warning);

            }
        }



        private void cmdLast_Click(object sender, EventArgs e)
        {
            OrderCounter = MaxOrder1;
            FillGrid(OrderCounter);

        }

        private void cmdFirst_Click(object sender, EventArgs e)
        {
            if (OrderCounter > 0)
            {
                OrderCounter = 0;
                FillGrid(OrderCounter);
            }

        }

        private void lbltotalAmount_TextAlignChanged(object sender, EventArgs e)
        {

        }

        private void cmdAdd_Click(object sender, EventArgs e)
        {

            GenerateOrderCart(false);
            this.Close();

        }

        private void cmdReplace_Click(object sender, EventArgs e)
        {
            try
            {
                bool flag;
                Session.RemakeOrder = true;
                if (dgvCart.Rows.Count == 0)
                {
                    CustomMessageBox.Show("There are no any item in cart. ", CustomMessageBox.Buttons.OK, CustomMessageBox.Icon.Warning);
                    return;
                }
                if (lbltotalAmount.Text == "0.00" || lbltotalAmount.Text.Length == 0)
                {
                    CustomMessageBox.Show("There are no any item in cart. ", CustomMessageBox.Buttons.OK, CustomMessageBox.Icon.Warning);
                    return;
                }
                if (comReason.SelectedIndex == 0)
                {
                    CustomMessageBox.Show("Please select reason. ", CustomMessageBox.Buttons.OK, CustomMessageBox.Icon.Warning);
                    return;
                }

                flag = GenerateOrderCart(true);
                if (flag == false)
                {

                    return;
                }
                Session.RemakeOrder = true;
                List<CatalogCoupons> CurrentCatalogCoupons = new List<CatalogCoupons>();
                if (Session.OrderCoupons == null) Session.OrderCoupons = new List<CatalogCoupons>();

                CurrentCatalogCoupons = APILayer.GetOrderCoupons(Session._LocationCode, true, Session.selectedOrderType, "");


                foreach (CatalogCoupons catalogCoupons in CurrentCatalogCoupons)
                {
                    if (!Session.OrderCoupons.Contains(catalogCoupons)) Session.OrderCoupons.Add(catalogCoupons);
                }

                CartFunctions.GetCartRemake("OPS004");

                this.Close();
            }
            catch (Exception ex)
            {
                Logger.Trace("ERROR", "frmRemake-cmdreplace_click(): " + ex.Message, ex, true);
                CustomMessageBox.Show(PrintFunctions.System_Error_Message, CustomMessageBox.Buttons.OK, CustomMessageBox.Icon.Warning);

            }

        }


        public bool GenerateOrderCart(bool blnReplace)
        {
            try
            {
                bool newCart = false;
                int cartItemNumber = 0;
                CartItem cartItemOrignal1 = new CartItem();

                UserFunctions.CheckCart();
                Cart cartLocal = (new Cart().GetCart());
                if (blnReplace == true)
                {
                    foreach (CartItem cartItem in Session.cart.cartItems)
                    {
                        CartItem cartItemlocal = new CartItem();
                        cartItemlocal = cartItem;
                        cartItemlocal.Action = "D";

                        cartLocal.cartItems.Add(cartItemlocal);
                    }
                }

                if (!(Session.cart != null && Session.cart.cartHeader != null && !String.IsNullOrEmpty(Session.cart.cartHeader.CartId)))
                {
                    newCart = true;
                    CreateCart(Session.originalcart);
                }
                foreach (CartItem cartItemOrignal in Session.originalcart.cartItems)
                {
                    CartItem cartItem = new CartItem();
                    cartItem = cartItemOrignal;
                    cartItem.Coupon_Adjustment = false;
                    cartItem.Coupon_Amount = 0;
                    cartItem.Coupon_Code = "";
                    cartItem.Coupon_Description = "";
                    cartItem.Coupon_Min_Price = 0;
                    cartItem.Coupon_Taxable = false;
                    cartItem.Coupon_Type_Code = 0;
                    cartItem.Order_Line_Coupon_Amount = 0;
                    cartItem.Order_Line_No_Tax_Discount = 0;


                    if (newCart == true && cartItemNumber == 0)
                    {
                        cartItem.Action = "M";
                        cartItem.CartId = Session.cart.cartHeader.CartId;
                    }
                    else
                    {
                        cartItem.Action = "A";
                    }

                    cartLocal.cartItems.Add(cartItem);

                    if (cartItem.Combo_Group > 0)
                    {
                        Session.ProcessingCombo = true;
                        Session.CurrentComboGroup = cartItem.Combo_Group;
                        Session.CurrentComboItem = cartItem.Combo_Item_Number;
                    }
                    else
                    {
                        Session.ProcessingCombo = false;
                        Session.CurrentComboGroup = 0;
                        Session.CurrentComboItem = 0;
                    }
                    //Default Toppings
                    List<CatalogOptionGroups> catalogOptionGroups = APILayer.GetOptionGroups(cartItem.Menu_Code);
                    if (catalogOptionGroups != null && catalogOptionGroups.Count > 0)
                    {
                        CatalogOptionGroups currentCatalogOptionGroups = catalogOptionGroups[0]; //TO DO only one Option Group considered

                        if (currentCatalogOptionGroups.Topping_Group)
                        {
                            Session.currentToppingCollection = new UserTypes.ToppingCollection();
                            Session.currentToppingCollection.currentCatalogOptionGroups = currentCatalogOptionGroups;
                            Session.currentToppingCollection.currentToppings = CartFunctions.GetToppingsFromCatalogToppings(APILayer.GetToppings(cartItem.Menu_Code, currentCatalogOptionGroups.Menu_Option_Group_Code), cartItem.Line_Number, cartLocal, cartItem.Menu_Code, cartItem.Size_Code, cartItem.Menu_Code, cartItem.Size_Code);
                            string itemSpecialtyPizzasCode = string.Empty;
                            if (cartItem.itemSpecialtyPizzas != null && cartItem.itemSpecialtyPizzas.Count > 0)
                            {
                                itemSpecialtyPizzasCode = cartItem.itemSpecialtyPizzas[0].Specialty_Pizza_Code;
                            }
                            else
                            {
                                itemSpecialtyPizzasCode = "";
                            }

                            CartFunctions.PopulateDefaultToppings(ref cartLocal, cartItem.Line_Number, cartItem.Menu_Code, cartItem.Menu_Code, itemSpecialtyPizzasCode, itemSpecialtyPizzasCode);
                        }
                    }
                    Session.currentToppingSizeCode = "";

                    List<ItemOption> localItemOptions = new List<ItemOption>();

                    if (cartItem.itemOptions != null && cartItem.itemOptions.Count > 0)
                    {
                        foreach (ItemOption itemOption in cartItem.itemOptions)
                        {
                            ItemOption TempItemOption = new ItemOption();
                            TempItemOption = itemOption;
                            TempItemOption.CartId = Session.cart.cartHeader.CartId;
                            localItemOptions.Add(TempItemOption);
                        }
                        cartItem.itemOptions = localItemOptions;
                    }
                    else
                    {
                        cartItem.itemOptions = new List<ItemOption>();
                    }

                    Session.ProcessingCombo = false;
                    Session.CurrentComboGroup = 0;
                    Session.CurrentComboItem = 0;
                    cartItemNumber++;
                }
                cartLocal.itemCombos = new List<ItemCombo>(); //Session.originalcart.itemCombos;

                foreach (ItemCombo itemCombo in cartLocal.itemCombos)
                {
                    itemCombo.CartId = Session.cart.cartHeader.CartId;
                    itemCombo.Action = "A";
                }
                cartLocal.Customer = Session.cart.Customer;
                cartLocal.cartHeader = Session.cart.cartHeader;
                cartLocal.cartHeader.Old_Order_Number = Old_Order_Number;


                Session.cart.cartHeader.Coupon_Amount = 0;


                //\\Abhishek
                cartLocal.cartHeader.Order_Type_Code = Session.originalcart.cartHeader.Order_Type_Code;

                cartLocal.cartHeader.Action = "M";
                OrderReason Reason = new OrderReason();
                string ReasonText = Convert.ToString(comReason.SelectedItem);
                var value = lstCatalogReasons.Find(item => item.System_Text == ReasonText).Reason_ID;
                //if(Session.cart.cartHeader.Order_Type_Code =="D" && Session.cart.cartHeader.Delivery_Fee > 0)

                //{
                //    Session.cart.cartHeader.SubTotal = Session.cart.cartHeader.SubTotal - Session.cart.cartHeader.Delivery_Fee;
                //}
                Reason.CartId = Session.cart.cartHeader.CartId;
                Reason.Location_Code = Session.cart.cartHeader.LocationCode;
                Reason.Order_Number = Session.cart.cartHeader.Order_Number;
                Reason.Order_Date = Session.cart.cartHeader.Order_Date;
                Reason.Reason_Sequence = lstCatalogReasons.Find(item => item.System_Text == ReasonText).Reason_Group_Code;
                Reason.Reason_Group_Code = lstCatalogReasons.Find(item => item.System_Text == ReasonText).Reason_Group_Code;
                Reason.Reason_Group_Code = lstCatalogReasons.Find(item => item.System_Text == ReasonText).Reason_ID;
                Reason.Added_By = Session.cart.cartHeader.Added_By;
                cartLocal.orderReasons.Add(Reason);

                cartLocal.cartHeader.Delivery_Fee = 0;
                //cartLocal.cartHeader.Modifying = "1";
                cartLocal.cartHeader.IsRemake = true;
                Session.cart = APILayer.Add2Cart(cartLocal);
                Session.selectedOrderType = Session.cart.cartHeader.Order_Type_Code;
                Session.originalcart = (new Cart().GetCartWithDefaultValues());
                if (Session.cart.cartItems.Count == 0)
                {
                    CustomMessageBox.Show("There are no any item to remake", CustomMessageBox.Buttons.OK, CustomMessageBox.Icon.Question);
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                Logger.Trace("ERROR", "frmRemake-generateordercart(): " + ex.Message, ex, true);
                CustomMessageBox.Show(PrintFunctions.System_Error_Message, CustomMessageBox.Buttons.OK, CustomMessageBox.Icon.Warning);
                return false;

            }
        }


        public void CreateCart(Cart originalCart)
        {
            try
            {
                UserFunctions.CheckCart();
                Cart cartLocal = (new Cart().GetCart());

                FillCustomerToCartLocal(ref cartLocal);
                FillCartHeaderDefault(ref cartLocal);
                cartLocal.cartHeader.Order_Type_Code = originalCart.cartHeader.Order_Type_Code;

                CartItem cartItemLocal = new CartItem();
                cartItemLocal = originalCart.cartItems[0];

                cartItemLocal.Location_Code = Session._LocationCode;
                cartItemLocal.Order_Date = Session.SystemDate;
                cartItemLocal.Line_Number = 1;
                cartItemLocal.Sequence = 1;
                cartItemLocal.Action = "A";
                originalCart.cartHeader.Old_Order_Number = Old_Order_Number;

                cartLocal.cartItems.Add(cartItemLocal);

                Session.cart = APILayer.Add2Cart(cartLocal);

                Session.selectedOrderType = cartLocal.cartHeader.Order_Type_Code;
            }
            catch (Exception ex)
            {
                Logger.Trace("ERROR", "frmRemake-createcart(): " + ex.Message, ex, true);
                CustomMessageBox.Show(PrintFunctions.System_Error_Message, CustomMessageBox.Buttons.OK, CustomMessageBox.Icon.Warning);

            }
        }


        public void FillCustomerToCartLocal(ref Cart cart)
        {


            if (Session.cart.Customer != null)
            {

                cart.Customer.Location_Code = String.IsNullOrEmpty(Session.cart.Customer.Location_Code) ? "" : Session.cart.Customer.Location_Code;
                cart.Customer.Phone_Number = String.IsNullOrEmpty(Session.cart.Customer.Phone_Number) ? "" : Session.cart.Customer.Phone_Number;
                cart.Customer.Phone_Ext = String.IsNullOrEmpty(Session.cart.Customer.Phone_Ext) ? "" : Session.cart.Customer.Phone_Ext;
                cart.Customer.Customer_Code = Convert.ToInt32(Session.cart.Customer.Customer_Code);
                cart.Customer.Name = String.IsNullOrEmpty(Session.cart.Customer.Name) ? "" : Session.cart.Customer.Name;
                cart.Customer.Company_Name = String.IsNullOrEmpty(Session.cart.Customer.Company_Name) ? "" : Session.cart.Customer.Company_Name;
                cart.Customer.Street_Number = String.IsNullOrEmpty(Session.cart.Customer.Street_Number) ? "" : Session.cart.Customer.Street_Number;
                cart.Customer.Street_Code = Session.cart.Customer.Street_Code == 0 ? 1 : Session.cart.Customer.Street_Code;
                cart.Customer.Cross_Street_Code = Session.cart.Customer.Cross_Street_Code;
                cart.Customer.Suite = String.IsNullOrEmpty(Session.cart.Customer.Suite) ? "" : Session.cart.Customer.Suite;
                cart.Customer.Address_Line_2 = String.IsNullOrEmpty(Session.cart.Customer.Address_Line_2) ? "" : Session.cart.Customer.Address_Line_2;
                cart.Customer.Address_Line_3 = String.IsNullOrEmpty(Session.cart.Customer.Address_Line_3) ? "" : Session.cart.Customer.Address_Line_3;
                cart.Customer.Address_Line_4 = String.IsNullOrEmpty(Session.cart.Customer.Address_Line_4) ? "" : Session.cart.Customer.Address_Line_4;
                cart.Customer.Mailing_Address = "";
                cart.Customer.Postal_Code = String.IsNullOrEmpty(Session.cart.Customer.Postal_Code) ? "" : Session.cart.Customer.Postal_Code;
                cart.Customer.Plus4 = "";
                cart.Customer.Cart = "";
                cart.Customer.Delivery_Point_Code = "";
                cart.Customer.Walk_Sequence = "";
                cart.Customer.Address_Type = String.IsNullOrEmpty(Session.cart.Customer.Address_Type) ? "" : Session.cart.Customer.Address_Type;
                cart.Customer.Set_Discount = 0;
                cart.Customer.Tax_Exempt1 = Session.cart.Customer.Tax_Exempt1;
                cart.Customer.Tax_ID1 = String.IsNullOrEmpty(Session.cart.Customer.Tax_ID1) ? "" : Session.cart.Customer.Tax_ID1;
                cart.Customer.Tax_Exempt2 = Session.cart.Customer.Tax_Exempt2;
                cart.Customer.Tax_ID2 = String.IsNullOrEmpty(Session.cart.Customer.Tax_ID2) ? "" : Session.cart.Customer.Tax_ID2;
                cart.Customer.Tax_Exempt3 = Session.cart.Customer.Tax_Exempt3;
                cart.Customer.Tax_ID3 = String.IsNullOrEmpty(Session.cart.Customer.Tax_ID3) ? "" : Session.cart.Customer.Tax_ID3;
                cart.Customer.Tax_Exempt4 = Session.cart.Customer.Tax_Exempt4;
                cart.Customer.Tax_ID4 = String.IsNullOrEmpty(Session.cart.Customer.Tax_ID4) ? "" : Session.cart.Customer.Tax_ID4;
                cart.Customer.Accept_Checks = false;
                cart.Customer.Accept_Credit_Cards = false;
                cart.Customer.Accept_Gift_Cards = false;
                cart.Customer.Accept_Charge_Account = false;
                cart.Customer.Accept_Cash = false;
                cart.Customer.Finance_Charge_Rate = Session.cart.Customer.Finance_Charge_Rate;
                cart.Customer.Credit_Limit = 0;
                cart.Customer.Credit = 0;
                cart.Customer.Payment_Terms = 0;
                cart.Customer.First_Order_Date = Session.cart.Customer.First_Order_Date == Convert.ToDateTime("01-01-0001") ? new DateTime(1899, 12, 30) : Session.cart.Customer.First_Order_Date;
                cart.Customer.Last_Order_Date = Session.cart.Customer.Last_Order_Date == Convert.ToDateTime("01-01-0001") ? new DateTime(1899, 12, 30) : Session.cart.Customer.Last_Order_Date;
                cart.Customer.Added_By = String.IsNullOrEmpty(Session.cart.Customer.Added_By) ? "" : Session.cart.Customer.Added_By;
                cart.Customer.Comments = String.IsNullOrEmpty(Session.cart.Customer.Comments) ? "" : Session.cart.Customer.Comments;
                cart.Customer.DriverComments = String.IsNullOrEmpty(Session.cart.Customer.DriverComments) ? "" : Session.cart.Customer.DriverComments;
                cart.Customer.DriverCommentsAddUpdateDelete = false;
                cart.Customer.Manager_Notes = String.IsNullOrEmpty(Session.cart.Customer.Manager_Notes) ? "" : Session.cart.Customer.Manager_Notes;
                cart.Customer.Customer_City_Code = Session.cart.Customer.Customer_City_Code;
                cart.Customer.Customer_Street_Name = "";
                cart.Customer.HotelorCollege = false;
                cart.Customer.City = String.IsNullOrEmpty(Session.cart.Customer.City) ? "" : Session.cart.Customer.City;
                cart.Customer.Region = String.IsNullOrEmpty(Session.cart.Customer.Region) ? "" : Session.cart.Customer.Region;
                cart.Customer.TaxRate1 = 0;
                cart.Customer.TaxRate2 = 0;
                cart.Customer.Cross_Street = "";
                cart.Customer.NoteAddUpdateDelete = false;
                cart.Customer.gstin_number = String.IsNullOrEmpty(Session.cart.Customer.gstin_number) ? "" : Session.cart.Customer.gstin_number;
                cart.Customer.date_of_birth = Session.cart.Customer.date_of_birth == Convert.ToDateTime("01-01-0001") ? new DateTime(1899, 12, 30) : Session.cart.Customer.date_of_birth;
                cart.Customer.anniversary_date = Session.cart.Customer.anniversary_date == Convert.ToDateTime("01-01-0001") ? new DateTime(1899, 12, 30) : Session.cart.Customer.anniversary_date;
                cart.Customer.Action = "M";

            }
        }

        private void frmHistory_Activated(object sender, EventArgs e)
        {
            //OrderCounter = 0;
            //MaxOrder = 0;
        }

        public void FillCartHeaderDefault(ref Cart cartLocal)
        {
            try
            {
                cartLocal.cartHeader.CartId = Session.cart.cartHeader.CartId;
                cartLocal.cartHeader.LocationCode = Session._LocationCode;
                cartLocal.cartHeader.Order_Date = Session.cart.cartHeader.Order_Date == DateTime.MinValue ? Session.SystemDate : Session.cart.cartHeader.Order_Date;
                cartLocal.cartHeader.ctlAddressCity = cartLocal.Customer.City.Trim();
                cartLocal.cartHeader.Actual_Order_Date = Convert.ToDateTime(SystemSettings.settings.pdtmServerDateTime);
                cartLocal.cartHeader.Customer_Code = cartLocal.Customer.Customer_Code;
                cartLocal.cartHeader.Customer_Name = cartLocal.Customer.Name;
                cartLocal.cartHeader.Computer_Name = Session.ComputerName;
                cartLocal.cartHeader.Order_Status_Code = 1;
                cartLocal.cartHeader.Order_Taker_ID = Session.CurrentEmployee.LoginDetail.EmployeeCode;
                cartLocal.cartHeader.Order_Taker_Shift = Convert.ToString(Session.CurrentEmployee.LoginDetail.DateShiftNumber); //TO DO
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
                cartLocal.cartHeader.Delivery_Fee = 0;
            }
            catch (Exception ex)
            {
                Logger.Trace("ERROR", "frmRemake-fillcartheaderdefault(): " + ex.Message, ex, true);
                CustomMessageBox.Show(PrintFunctions.System_Error_Message, CustomMessageBox.Buttons.OK, CustomMessageBox.Icon.Warning);

            }
        }
        private void QuantityChange(bool Replace, int Qty)
        {
            try
            {
                if (selectedLineNumber > -1)
                {
                    bool QtyChanged = false;
                    int FocusedRow = dgvCart.SelectedCells.Count > 0 ? dgvCart.SelectedCells[0].RowIndex : 0;
                    string ItemName = Convert.ToString(dgvCart.Rows[FocusedRow].Cells[2].Value);

                    //dgvCart.SelectedCells[1].Value;
                    //if (FocusedRow ==0)
                    // {
                    //     selectedLineNumber = 1;

                    // }
                    QtyChanged = QuantityChangeRemake(Replace, Qty, selectedLineNumber);
                    

                    if (QtyChanged)
                    {
                        RefreshCartUI();
                        CartControl();
                    }



                    Session.originalcart.cartHeader.SubTotal = 0;
                    foreach (CartItem cartItem in Session.originalcart.cartItems)
                    {
                        Session.originalcart.cartHeader.SubTotal = Session.originalcart.cartHeader.SubTotal + cartItem.SubTotal;

                    }
                    Session.originalcart.cartHeader.Total = Session.originalcart.cartHeader.SubTotal;
                    lbltotalAmount.Text = String.Format(Session.DisplayFormat, Session.originalcart.cartHeader.Total);



                    //if (FocusedRow > -1)
                    //    dgvCart.CurrentCell = dgvCart.Rows[FocusedRow].Cells["Qty"];
                }
            }
            catch (Exception ex)
            {
                Logger.Trace("ERROR", "frmRemake-quantitychange(): " + ex.Message, ex, true);
                CustomMessageBox.Show(PrintFunctions.System_Error_Message, CustomMessageBox.Buttons.OK, CustomMessageBox.Icon.Warning);

            }
        }

        private void QuantityIncrease(bool Replace, int Qty)
        {
            try
            {
                if (selectedLineNumber > -1)
                {
                    bool QtyChanged = false;
                    int FocusedRow = dgvCart.SelectedCells.Count > 0 ? dgvCart.SelectedCells[0].RowIndex : 0;
                    string ItemName = Convert.ToString(dgvCart.Rows[FocusedRow].Cells[2].Value);

                    //dgvCart.SelectedCells[1].Value;
                    //if (FocusedRow ==0)
                    // {
                    //     selectedLineNumber = 1;

                    // }
                    QtyChanged = QuantityIncreaseRemake(Replace, Qty, selectedLineNumber);


                    if (QtyChanged)
                    {
                        RefreshCartUI();
                        CartControl();
                    }



                    Session.originalcart.cartHeader.SubTotal = 0;
                    foreach (CartItem cartItem in Session.originalcart.cartItems)
                    {
                        Session.originalcart.cartHeader.SubTotal = Session.originalcart.cartHeader.SubTotal + cartItem.SubTotal;

                    }
                    Session.originalcart.cartHeader.Total = Session.originalcart.cartHeader.SubTotal;
                    lbltotalAmount.Text = String.Format(Session.DisplayFormat, Session.originalcart.cartHeader.Total);



                    //if (FocusedRow > -1)
                    //    dgvCart.CurrentCell = dgvCart.Rows[FocusedRow].Cells["Qty"];
                }
            }
            catch (Exception ex)
            {
                Logger.Trace("ERROR", "frmRemake-quantitychange(): " + ex.Message, ex, true);
                CustomMessageBox.Show(PrintFunctions.System_Error_Message, CustomMessageBox.Buttons.OK, CustomMessageBox.Icon.Warning);

            }
        }
        public static bool QuantityChangeRemake(bool Replace, int Qty, int lselectedLineNumber)
        {
            if (Session.originalcart != null && Session.originalcart.cartItems.Count > 0)
            {
                CartItem cartItemLocal = new CartItem();
                if (lselectedLineNumber > -1)
                    cartItemLocal = Session.originalcart.cartItems.Find(x => x.Line_Number == lselectedLineNumber);
                else
                    cartItemLocal = Session.originalcart.cartItems[Session.originalcart.cartItems.Count - 1];
                if (cartItemLocal is null)
                {
                    MessageBox.Show("Please select one item");
                    return true;
                }
                if (Replace)
                    cartItemLocal.Quantity = Qty < 0 ? 0 : Qty;
                else
                    cartItemLocal.Quantity = cartItemLocal.Quantity + (Qty);
                int asd = Item_Qty[lselectedLineNumber];

                if (cartItemLocal.Quantity <= 0)
                {
                    if (CustomMessageBox.Show(cartItemLocal.Description + " " + APILayer.GetCatalogText(LanguageConstant.cintMSGRemoveItemFromOrder), CustomMessageBox.Buttons.YesNo, CustomMessageBox.Icon.Question) == DialogResult.No)

                    {
                        return false;
                    }
                    else
                    {

                        Session.originalcart.cartItems.Remove(cartItemLocal);
                    }
                }
                cartItemLocal.Price = Convert.ToDecimal(cartItemLocal.Quantity) * cartItemLocal.Menu_Price;
                cartItemLocal.SubTotal = cartItemLocal.Price;
                Session.originalcart.cartHeader.SubTotal = 0;
                foreach (CartItem cartItem in Session.originalcart.cartItems)
                {
                    Session.originalcart.cartHeader.SubTotal = Session.originalcart.cartHeader.SubTotal + cartItem.SubTotal;

                }
                Session.originalcart.cartHeader.Total = Session.originalcart.cartHeader.SubTotal;

                return true;
            }
            return false;
        }

        public static bool QuantityIncreaseRemake(bool Replace, int Qty, int lselectedLineNumber)
        {
            if (Session.originalcart != null && Session.originalcart.cartItems.Count > 0)
            {
                CartItem cartItemLocal = new CartItem();
                if (lselectedLineNumber > -1)
                    cartItemLocal = Session.originalcart.cartItems.Find(x => x.Line_Number == lselectedLineNumber);
                else
                    cartItemLocal = Session.originalcart.cartItems[Session.originalcart.cartItems.Count - 1];
                if (cartItemLocal is null)
                {
                    MessageBox.Show("Please select one item");
                    return true;
                }
                if (Replace)
                    cartItemLocal.Quantity = Qty < 0 ? 0 : Qty;
                else
                    cartItemLocal.Quantity = (Qty);
                int asd = Item_Qty[lselectedLineNumber];

                if (cartItemLocal.Quantity <= 0)
                {
                    if (CustomMessageBox.Show(cartItemLocal.Description + " " + APILayer.GetCatalogText(LanguageConstant.cintMSGRemoveItemFromOrder), CustomMessageBox.Buttons.YesNo, CustomMessageBox.Icon.Question) == DialogResult.No)

                    {
                        return false;
                    }
                    else
                    {

                        Session.originalcart.cartItems.Remove(cartItemLocal);
                    }
                }
                cartItemLocal.Price = Convert.ToDecimal(cartItemLocal.Quantity) * cartItemLocal.Menu_Price;
                cartItemLocal.SubTotal = cartItemLocal.Price;
                Session.originalcart.cartHeader.SubTotal = 0;
                foreach (CartItem cartItem in Session.originalcart.cartItems)
                {
                    Session.originalcart.cartHeader.SubTotal = Session.originalcart.cartHeader.SubTotal + cartItem.SubTotal;

                }
                Session.originalcart.cartHeader.Total = Session.originalcart.cartHeader.SubTotal;

                return true;
            }
            return false;
        }













        private void CartControl()
        {
            bool selected = false;
            if (dgvCart != null && CurrentLineType != "")
            {
                if (Session.ProcessingCombo)
                {
                    if (Session.CurrentComboItem == 0)
                    {
                        for (int i = dgvCart.RowCount - 1; i >= 0; i--)
                        {
                            if (Convert.ToString(dgvCart.Rows[i].Cells["LineType"].Value) == CurrentLineType && Convert.ToInt32(dgvCart.Rows[i].Cells["Combo_Group"].Value) == Session.CurrentComboGroup)
                            {
                                dgvCart.Rows[i].Selected = true;
                                selected = true;
                                break;
                            }
                        }
                    }
                    else
                    {
                        for (int i = dgvCart.RowCount - 1; i >= 0; i--)
                        {
                            if (Convert.ToString(dgvCart.Rows[i].Cells["LineType"].Value) == CurrentLineType && Convert.ToInt32(dgvCart.Rows[i].Cells["Combo_Group"].Value) == Session.CurrentComboGroup && Convert.ToInt32(dgvCart.Rows[i].Cells["Combo_Item_Number"].Value) == Session.CurrentComboItem)
                            {
                                dgvCart.Rows[i].Selected = true;
                                selected = true;
                                break;
                            }
                        }
                    }
                }
                else
                {
                    if (selectedLineNumber < 0)
                    {
                        for (int i = dgvCart.RowCount - 1; i >= 0; i--)
                        {
                            if (Convert.ToString(dgvCart.Rows[i].Cells["LineType"].Value) == CurrentLineType && Convert.ToString(dgvCart.Rows[i].Cells["Menu_Code"].Value) == Session.selectedMenuItem.Menu_Code)
                            {
                                dgvCart.Rows[i].Selected = true;
                                selected = true;
                                break;
                            }
                        }
                    }
                    else
                    {
                        for (int i = dgvCart.RowCount - 1; i >= 0; i--)
                        {
                            if (Convert.ToString(dgvCart.Rows[i].Cells["LineType"].Value) == CurrentLineType && Convert.ToInt32(dgvCart.Rows[i].Cells["Line_Number"].Value) == selectedLineNumber)
                            {
                                dgvCart.Rows[i].Selected = true;
                                selected = true;
                                break;
                            }
                        }
                    }
                }
            }
        }
        private void btn_Plus_Click(object sender, EventArgs e)
        {
            QuantityChange(false, 1);
        }

        private void btn_Minus_Click(object sender, EventArgs e)
        {
            QuantityChange(false, -1);
        }
        public void DeleteItem()
        {
            try
            {
                int RowIndex = 0;

                if (RowIndex >= 0)
                {
                    if (dgvCart != null && dgvCart.Columns.Contains("LineType"))
                    {

                        if (Convert.ToInt32(Convert.ToString(dgvCart.Rows[RowIndex].Cells["Combo_Group"].Value)) > 0)
                        {
                            Session.CurrentComboGroup = Convert.ToInt32(Convert.ToString(dgvCart.Rows[RowIndex].Cells["Combo_Group"].Value));
                            Session.CurrentComboItem = Convert.ToInt32(Convert.ToString(dgvCart.Rows[RowIndex].Cells["Combo_Item_Number"].Value));
                            Session.ProcessingCombo = true;
                        }
                        else
                        {
                            Session.CurrentComboGroup = 0;
                            Session.CurrentComboItem = 0;
                            Session.ProcessingCombo = false;
                        }

                        if (Convert.ToString(dgvCart.Rows[RowIndex].Cells["LineType"].Value) == "M" || Convert.ToString(dgvCart.Rows[RowIndex].Cells["LineType"].Value) == "G" || Convert.ToString(dgvCart.Rows[RowIndex].Cells["LineType"].Value) == "B")
                        {
                            selectedLineNumber = Convert.ToInt32(Convert.ToString(dgvCart.Rows[RowIndex].Cells["Line_Number"].Value));

                            txt_Quantity.Text = Convert.ToString(Item_Qty[selectedLineNumber]);

                            if (Session.ProcessingCombo && Session.CurrentComboItem > 0)
                            {
                                //HandleComboMenuItems();
                            }
                            else
                            {
                                //Session.selectedMenuItem = Session.menuItems.First(x => x.Menu_Code == Convert.ToString(dgvCart.Rows[e.RowIndex].Cells["Menu_Code"].Value));
                                //DyanmicButtonValue("MenuItems", Convert.ToString(dgvCart.Rows[e.RowIndex].Cells["Menu_Code"].Value), true, Convert.ToString(dgvCart.Rows[e.RowIndex].Cells["Size_Code"].Value));
                            }
                        }
                        else if (Convert.ToString(dgvCart.Rows[RowIndex].Cells["LineType"].Value) == "O")
                        {
                            selectedLineNumber = Convert.ToInt32(Convert.ToString(dgvCart.Rows[RowIndex].Cells["Line_Number"].Value));

                            txt_Quantity.Text = Convert.ToString(Item_Qty[selectedLineNumber]);


                        }
                        else if (Convert.ToString(dgvCart.Rows[RowIndex].Cells["LineType"].Value) == "C")
                        {
                            selectedLineNumber = Convert.ToInt32(Convert.ToString(dgvCart.Rows[RowIndex].Cells["Line_Number"].Value));

                            txt_Quantity.Text = Convert.ToString(Item_Qty[selectedLineNumber]);

                        }
                        else
                        {
                            selectedLineNumber = -1;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Trace("ERROR", "frmRemake-dgvcart_cellclick(): " + ex.Message, ex, true);
                CustomMessageBox.Show(PrintFunctions.System_Error_Message, CustomMessageBox.Buttons.OK, CustomMessageBox.Icon.Warning);

            }
        }
        private void dgvCart_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {


                if (e.RowIndex >= 0)
                {
                    if (dgvCart != null && dgvCart.Columns.Contains("LineType"))
                    {
                        //HiglightMenuCategory(Convert.ToString(dgvCart.Rows[e.RowIndex].Cells["Menu_Category_Code"].Value));

                        if (Convert.ToInt32(Convert.ToString(dgvCart.Rows[e.RowIndex].Cells["Combo_Group"].Value)) > 0)
                        {
                            Session.CurrentComboGroup = Convert.ToInt32(Convert.ToString(dgvCart.Rows[e.RowIndex].Cells["Combo_Group"].Value));
                            Session.CurrentComboItem = Convert.ToInt32(Convert.ToString(dgvCart.Rows[e.RowIndex].Cells["Combo_Item_Number"].Value));
                            Session.ProcessingCombo = true;
                        }
                        else
                        {
                            Session.CurrentComboGroup = 0;
                            Session.CurrentComboItem = 0;
                            Session.ProcessingCombo = false;
                        }
                        //btn_Quantity_Click(e, e);
                        //txt_Quantity.Text = Convert.ToString(Item_Qty[selectedLineNumber]);
                        if (Convert.ToString(dgvCart.Rows[e.RowIndex].Cells["LineType"].Value) == "M" || Convert.ToString(dgvCart.Rows[e.RowIndex].Cells["LineType"].Value) == "G" || Convert.ToString(dgvCart.Rows[e.RowIndex].Cells["LineType"].Value) == "B")
                        {
                            selectedLineNumber = Convert.ToInt32(Convert.ToString(dgvCart.Rows[e.RowIndex].Cells["Line_Number"].Value));

                            //btn_Quantity_Click(e, e);
                            txt_Quantity.Text = Convert.ToString(Item_Qty[selectedLineNumber]);

                            if (Session.ProcessingCombo && Session.CurrentComboItem > 0)
                            {
                                //HandleComboMenuItems();
                            }
                            else
                            {
                                //Session.selectedMenuItem = Session.menuItems.First(x => x.Menu_Code == Convert.ToString(dgvCart.Rows[e.RowIndex].Cells["Menu_Code"].Value));
                                //DyanmicButtonValue("MenuItems", Convert.ToString(dgvCart.Rows[e.RowIndex].Cells["Menu_Code"].Value), true, Convert.ToString(dgvCart.Rows[e.RowIndex].Cells["Size_Code"].Value));
                            }
                        }
                        //else if (Convert.ToString(dgvCart.Rows[e.RowIndex].Cells["LineType"].Value) == "I")
                        //{
                        //    selectedLineNumber = Convert.ToInt32(Convert.ToString(dgvCart.Rows[e.RowIndex].Cells["Line_Number"].Value));
                        //    if (selectedLineNumber > 0)
                        //    {
                        //        return;
                        //        //using (frmReason objfrmReason = new frmReason(Convert.ToInt32(enumReasonGroupID.CookingInstruction)))
                        //        //{
                        //        //    objfrmReason.SelectedLineNumber = selectedLineNumber;
                        //        //    objfrmReason.HighlightItemReason(selectedLineNumber);
                        //        //    objfrmReason.ShowDialog();
                        //        //}
                        //    }
                        //}
                        else if (Convert.ToString(dgvCart.Rows[e.RowIndex].Cells["LineType"].Value) == "O")
                        {
                            selectedLineNumber = Convert.ToInt32(Convert.ToString(dgvCart.Rows[e.RowIndex].Cells["Line_Number"].Value));
                            txt_Quantity.Text = Convert.ToString(Item_Qty[selectedLineNumber]);


                        }
                        else if (Convert.ToString(dgvCart.Rows[e.RowIndex].Cells["LineType"].Value) == "C")
                        {
                            selectedLineNumber = Convert.ToInt32(Convert.ToString(dgvCart.Rows[e.RowIndex].Cells["Line_Number"].Value));
                            txt_Quantity.Text = Convert.ToString(Item_Qty[selectedLineNumber]);
                            //btn_Coupons_Click(btn_Coupons, new EventArgs());
                        }
                        else
                        {
                            selectedLineNumber = -1;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Trace("ERROR", "frmRemake-dgvcart_cellclick(): " + ex.Message, ex, true);
                CustomMessageBox.Show(PrintFunctions.System_Error_Message, CustomMessageBox.Buttons.OK, CustomMessageBox.Icon.Warning);

            }
        }

        private void lbltotalAmount_Click(object sender, EventArgs e)
        {

        }
        public void OrderCoupons()
        {

            int x = Constants.HorizontalSpace, y = Constants.VerticalSpace;
            string PreviousSelectedCoupons = "";



            List<CatalogCoupons> CurrentCatalogCoupons = APILayer.GetOrderCoupons(Session._LocationCode, true, Session.selectedOrderType, "");

            foreach (CatalogCoupons catalogCoupons in CurrentCatalogCoupons)
            {
                if (!Session.OrderCoupons.Contains(catalogCoupons)) Session.OrderCoupons.Add(catalogCoupons);
            }

            //Fix Button
            CatalogCoupons catalogCouponsCross = new CatalogCoupons();
            catalogCouponsCross.Coupon_Code = "D";
            catalogCouponsCross.Description = "";
            CurrentCatalogCoupons.Insert(0, catalogCouponsCross);

            if (Session.cart != null && Session.cart.cartHeader != null)
            {
                PreviousSelectedCoupons = Session.cart.cartHeader.Coupon_Code;
            }

            foreach (var item in CurrentCatalogCoupons)
            {
                Button btnDynamic = new Button();

                btnDynamic.Location = new System.Drawing.Point(x, y);
                btnDynamic.Size = new System.Drawing.Size(Constants.ButtonWidthG, Constants.ButtonHeightG);
                btnDynamic.Name = item.Coupon_Code;
                btnDynamic.Tag = "OrderCoupons";
                btnDynamic.Text = item.Description;
                btnDynamic.Font = new Font(new FontFamily("Microsoft Sans Serif"), 8);

                btnDynamic.BackColor = SystemColors.Control;

                btnDynamic.BackColor = SystemColors.Control;

                btnDynamic.Margin = new Padding(0);

                if (item.Coupon_Code == "D")
                {
                    btnDynamic.BackgroundImage = Properties.Resources._97;
                    btnDynamic.TextAlign = ContentAlignment.BottomCenter;
                    btnDynamic.BackgroundImageLayout = ImageLayout.Center;
                }
                else
                {
                    btnDynamic.BackgroundImage = null;
                    btnDynamic.TextAlign = ContentAlignment.MiddleCenter;
                }

                if (item.Coupon_Code == PreviousSelectedCoupons)
                    btnDynamic.BackColor = Session.DefaultEntityColor;


                //flowLayoutPanelMenuItems.Controls.Add(btnDynamic);

                x += Constants.ButtonWidthG;
                ////btnDynamic.Click += new EventHandler(this.DynamicButtonClick);

            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            OrderCoupons();


        }

        private void btn_Quantity_Click(object sender, EventArgs e)

        {
            btnQty_OK.Enabled = true;
            txt_Quantity.Visible = true;
            if (btn_Dot.Visible == false && selectedLineNumber == -1)
            {

                selectedLineNumber = 1;
            }
            pnl_Quantity.Visible = true;
            btn_Dot.Visible = true;
            btn_Dot.BackColor = DefaultBackColor;
            uC_KeyBoardNumeric.Visible = true;

            if (selectedLineNumber > -1)
            {
                if (Session.ProcessingCombo && Session.CurrentComboGroup > 0)
                {
                    ItemCombo itemCombo = Session.cart.itemCombos.Find(x => x.Combo_Group == Session.CurrentComboGroup);
                    if (itemCombo == null)
                        return;
                    txt_Quantity.Text = Convert.ToString(itemCombo.Combo_Quantity);
                }
                else
                {
                    //txt_Quantity.Text = Convert.ToString(Session.cart.cartItems.Find(x => x.Line_Number == selectedLineNumber).Quantity);
                }
                uC_KeyBoardNumeric.txtUserID = txt_Quantity;
                uC_KeyBoardNumeric.ChangeButtonColor(DefaultBackColor);
                //pnl_Quantity.Location = new Point(0, 0);
                //pnl_Quantity.Visible = true;
                //pnl_Quantity.BringToFront();
                //txt_Quantity.SelectAll();
            }
        }

        public void btn_Quantity_Click1(object sender, EventArgs e)

        {
            btnQty_OK.Enabled = true;
            txt_Quantity.Visible = true;
            if (btn_Dot.Visible == false && selectedLineNumber == -1)
            {

                selectedLineNumber = 1;
            }
            pnl_Quantity.Visible = true;
            btn_Dot.Visible = true;
            btn_Dot.BackColor = DefaultBackColor;
            uC_KeyBoardNumeric.Visible = true;

            if (selectedLineNumber > -1)
            {
                if (Session.ProcessingCombo && Session.CurrentComboGroup > 0)
                {
                    ItemCombo itemCombo = Session.cart.itemCombos.Find(x => x.Combo_Group == Session.CurrentComboGroup);
                    if (itemCombo == null)
                        return;
                    txt_Quantity.Text = Convert.ToString(itemCombo.Combo_Quantity);
                }
                else
                {
                    //txt_Quantity.Text = Convert.ToString(Session.cart.cartItems.Find(x => x.Line_Number == selectedLineNumber).Quantity);
                }
                uC_KeyBoardNumeric.txtUserID = txt_Quantity;
                uC_KeyBoardNumeric.ChangeButtonColor(DefaultBackColor);
                //pnl_Quantity.Location = new Point(0, 0);
                //pnl_Quantity.Visible = true;
                //pnl_Quantity.BringToFront();
                //txt_Quantity.SelectAll();
            }
        }




        private void btn_Dot_Click(object sender, EventArgs e)
        {
            if (txt_Quantity.Text.Length > 0)
            {
                txt_Quantity.Text = "0";

            }
        }

        private void btnQty_OK_Click(object sender, EventArgs e)
        {
            Int32 i = 1;
            Int32 Cur_Qty = 0;
            Int32 Desire_Qty = 0;
            Int32 Act_Qty = 0;
            if (selectedLineNumber < 0)
            {
                CustomMessageBox.Show("Please select at least one item", CustomMessageBox.Buttons.OK, CustomMessageBox.Icon.Warning);
                return;

            }
            if (dgvCart.Rows.Count == 0)
            {
                CustomMessageBox.Show("There are no any item in cart. ", CustomMessageBox.Buttons.OK, CustomMessageBox.Icon.Warning);
                return;
            }
            Act_Qty = Item_Qty[selectedLineNumber];
            foreach (CartItem cartItem in Session.originalcart.cartItems)
            {
                Process_Qty[i] = Convert.ToInt32(cartItem.Quantity);
                i = i + 1;

            }
            Cur_Qty = Process_Qty[selectedLineNumber];
            if (txt_Quantity.Text.Length == 0)
            {
                CustomMessageBox.Show("Please enter quantity", CustomMessageBox.Buttons.OK, CustomMessageBox.Icon.Warning);
                return;

            }
            Desire_Qty = Convert.ToInt32(txt_Quantity.Text);

            if (Desire_Qty < Cur_Qty)
            {
                QuantityChange(false, -(Cur_Qty - Desire_Qty));
                if (Session.originalcart.cartItems.Count > 0)
                {
                    DeleteItem();
                }

                return;
            }
            if (Desire_Qty > Act_Qty)
            {
                CustomMessageBox.Show("Number of Item In Remake Cannot be Greater then Master Order Item Count", CustomMessageBox.Buttons.OK, CustomMessageBox.Icon.Warning);
                return;
            }
            if (Act_Qty > Cur_Qty)
            {
                
                QuantityIncrease(false, Desire_Qty);
                if (Session.originalcart.cartItems.Count > 0)
                {
                    DeleteItem();
                }


                return;
            }
            //QuantityChange(false, Convert.ToInt32(txt_Quantity.Text));

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void comReason_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void txt_Quantity_TextChanged(object sender, EventArgs e)
        {
        }

        private void frmRemake_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (ALT_F4)
            {
                ALT_F4 = false;
                e.Cancel = true;
                return;
            }
        }

        private void frmRemake_KeyDown(object sender, KeyEventArgs e)
        {
            ALT_F4 = (e.KeyCode.Equals(Keys.F4) && e.Alt == true);
        }

        private void txt_Quantity_KeyPress(object sender, KeyPressEventArgs e)
        {

            e.Handled = !Char.IsNumber(e.KeyChar) && e.KeyChar != 8;

        }

        private void comReason_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }
    }
}
