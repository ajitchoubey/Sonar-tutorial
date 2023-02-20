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
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace JublFood.POS.App
{
    public partial class frmOrder : Form
    {
        DataTable dtCart;
        int selectedLineNumber = -1;
        //System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Catalog));
        Button btnPrev;
        Button btnNext;
        Button btnSpec;
        bool IsFromSpecialtyPizza;
        bool AutoClickSize = false;
        Button btnAutoClickSize = new Button();
        string TempOrderTypeCode = "I";
        private bool ALT_F4 = false;
        int pintCurrentOption_Group = 0;
        string CurrentLineType = "";
        public int pintCurrentBookmark;
        public string mstrUpsellReminder;
        private bool CartClicked = false;
        private int ComboParentComboGroup = 0;
        public bool VegChecked
        {
            get
            {
                return checkBoxVegOnly.Checked;
            }
        }        

        public frmOrder()
        {
            try
            {
                InitializeComponent();

                Session.menuItems = new List<CatalogMenuItems>();
                Session.menuItemSizes = new List<CatalogMenuItemSizes>();
                Session.comboMenuItems = new List<CatalogPOSComboMealItems>();
                Session.comboMenuItemSizes = new List<CatalogPOSComboMealItemSizes>();
                Session.catalogPOSComboMealItemsForButtons = new List<CatalogPOSComboMealItemsForButtons>();
                Session.OrderCoupons = new List<CatalogCoupons>();
                //Session.menuCategories = new List<CatalogMenuCategory>();
                Session.orderLineCoupons = new List<CatalogCoupons>();
                Session.itemwiseUpsellPromptMatrix = new List<ItemwiseUpsellPromptRow>();
                Session.itemwiseUpsellHistory = new List<ItemwiseUpsellHistory>();

                //UserFunctions.CheckSetup();
                uC_CustomerOrderBottomMenu1.LoadOrderType();
                uC_CustomerOrderBottomMenu1.UserControlButtonClicked += new
                EventHandler(OrderType_Click);
                RemakeButtonStatus();


                CheckTrainningMode();
            }
            catch (Exception ex)
            {
                Logger.Trace("ERROR", "frmOrder-frmOrder(): " + ex.Message, ex, true);
            }
        }


        private void OrderType_Click(object sender, EventArgs e)
        {
            try
            {
                Session.selectedOrderType = Convert.ToString(((Button)sender).Tag);

                if (Session.cart != null && Session.cart.Customer != null)
                {
                    if (Session.cart.Customer.Customer_City_Code != 1 && (Session.selectedOrderType == "I" || Session.selectedOrderType == "C"))
                    {
                        Form frmObj = Application.OpenForms["frmCustomer"];
                        if (frmObj != null) ((frmCustomer)frmObj).ChangeCityToDefault();
                    }
                }

                if (CartFunctions.OrderTypeChange())
                    RefreshCartUI();

                DyanmicCatalogCategories();
            }
            catch (Exception ex)
            {
                Logger.Trace("ERROR", "frmOrder-OrderType_Click(): " + ex.Message, ex, true);
            }

        }

        private void BtnDynamic_LostFocus(object sender, EventArgs e)
        {
            try
            {
                foreach (Button btnVal in flowLayoutPanelMenuCatagories.Controls.OfType<Button>())
                {
                    btnVal.BackColor = SystemColors.Control;
                }
            }
            catch (Exception ex)
            {
                Logger.Trace("ERROR", "frmOrder-btndynamic_lostfocus(): " + ex.Message, ex, true);
            }
        }

        private void MenuTypesLoad()
        {
            List<CatalogMenuTypes> catalogMenuTypesList = new List<CatalogMenuTypes>();
            catalogMenuTypesList = APILayer.GetMenuTypes();
            if (catalogMenuTypesList.Count <= 0) return;
            if (catalogMenuTypesList.Count == 1)
            {
                Session._MenuTypeID = catalogMenuTypesList[0].Menu_Type_ID;
            }
            else
            {
                foreach (CatalogMenuTypes _catalogMenuTypes in catalogMenuTypesList)
                {
                    if (_catalogMenuTypes.Default_Menu_Type == 1)
                        Session._MenuTypeID = _catalogMenuTypes.Menu_Type_ID;
                }
            }

            if (Session._MenuTypeID > 0)
                DyanmicCatalogCategories();

        }

        private void DyanmicCatalogCategories()
        {
            flowLayoutPanelMenuCatagories.Controls.Clear();
            flowLayoutPanelMenuItems.Controls.Clear();
            int x = Constants.HorizontalSpace, y = Constants.VerticalSpace;
            string DefaultMenuCategoryCode = "";

            List<CatalogMenuCategory> currentCatalogMenuCategories;
            if (Session.menuCategories != null && Session.menuCategories.Count > 0)
            {
                currentCatalogMenuCategories = Session.menuCategories;
            }
            else
            {
                currentCatalogMenuCategories = APILayer.GetMenuCategories(Session._MenuTypeID);
                foreach (CatalogMenuCategory catalogMenuCategory in currentCatalogMenuCategories)
                {
                    if (!Session.menuCategories.Any(z => z.Menu_Category_Code == catalogMenuCategory.Menu_Category_Code)) Session.menuCategories.Add(catalogMenuCategory);
                }
            }

            MenuCatagoryPanelSizeAdjustment(currentCatalogMenuCategories.Count);

            if (((flowLayoutPanelMenuCatagories.Width - Constants.HorizontalSpace) / Constants.MenuCardButtonWidthG) * 2 < Session.menuCategories.Count)
                Session.TotalPageMenuCategory = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(Session.menuCategories.Count) / Convert.ToDouble((((flowLayoutPanelMenuCatagories.Width - Constants.HorizontalSpace) / Constants.MenuCardButtonWidthG) * 2) - 1)));
            else
                Session.TotalPageMenuCategory = 1;

            PopulateMenuCategories(1, ref DefaultMenuCategoryCode);

            //foreach (var menuCategory in currentCatalogMenuCategories)
            //{
            //    Button btnDynamic = new Button();

            //    btnDynamic.Location = new System.Drawing.Point(x, y);
            //    btnDynamic.Size = new System.Drawing.Size(Constants.MenuCardButtonWidthG, Constants.ButtonHeightG);

            //    btnDynamic.Name = menuCategory.Menu_Category_Code.Trim();
            //    btnDynamic.Tag = "MenuCategories";
            //    btnDynamic.Text = menuCategory.Order_Description.ToUpper().Trim();
            //    btnDynamic.Font = new Font(new FontFamily("Microsoft Sans Serif"), 7);
            //    btnDynamic.BackColor = SystemColors.Control;
            //    btnDynamic.Margin = new Padding(0);
            //    btnDynamic.TextAlign = ContentAlignment.MiddleCenter;
            //    if (menuCategory.Default_Category == 1)
            //    {
            //        DefaultMenuCategoryCode = menuCategory.Menu_Category_Code;
            //        btnDynamic.BackColor = paytm_lightcolor;
            //    }

            //    flowLayoutPanelMenuCatagories.Controls.Add(btnDynamic);
            //    btnDynamic.Click += new EventHandler(this.DynamicButtonClick);

            //    x += Constants.MenuCardButtonWidthG;
            //}


            Button btnDynamic1 = new Button();
            string MenuCategoryTobeClicked = DefaultMenuCategoryCode != "" ? DefaultMenuCategoryCode : Session.currentMenuCategoryCode;
            if (!String.IsNullOrEmpty(MenuCategoryTobeClicked))
            {
                foreach (Control ctl in flowLayoutPanelMenuCatagories.Controls)
                {
                    if (ctl.Name == DefaultMenuCategoryCode)
                    {
                        btnDynamic1 = (Button)ctl;
                        break;
                    }
                }
            }
            else
            {
                btnDynamic1 = (Button)flowLayoutPanelMenuCatagories.Controls[1];
            }
            DynamicButtonClick(btnDynamic1, new EventArgs());

            dgvCart.ClearSelection();
        }

        private void DynamicButtonClick(object sender, EventArgs e)
        {
            try
            {
                AutoClickSize = false;
                Button btnDynamic = (Button)sender;

                BtnDynamic_LostFocus(sender, e);

                //if (Convert.ToString(btnDynamic.Tag) == "MenuItems")
                if (TempOrderTypeCode == "")
                {
                    if (string.IsNullOrWhiteSpace(Session.selectedOrderType))
                    {
                        CustomMessageBox.Show(APILayer.GetCatalogText(LanguageConstant.cintMSGChooseOrderType), CustomMessageBox.Buttons.OK, CustomMessageBox.Icon.Exclamation);
                        return;
                    }
                }

                btnDynamic.BackColor = Session.DefaultEntityColor;

                ClickEventProcess(Convert.ToString(btnDynamic.Tag), btnDynamic.Name);
            }
            catch (Exception ex)
            {
                Logger.Trace("ERROR", "frmOrder-dynamicbuttonclick(): " + ex.Message, ex, true);
            }

        }

        private void ClickEventProcess(string ParentMenu, string btnName)
        {
            if (ParentMenu == "MenuCategories")
            {
                //if (!ComboParentWithSingleSize)
                //{
                Session.ProcessingCombo = false; ;
                Session.CurrentComboItem = 0;
                Session.CurrentComboGroup = 0;
                //Session.MenuCategoryButtonClicked = false;
                //Session.MenuCategoryButtonClicked = true;
                pintCurrentOption_Group = 0;
                //}               
            }

            if (ComboFunctions.IsComboMealSelected(ParentMenu))
            {
                HandleCombos(btnName);
                return;
            }

            if (ComboFunctions.IsComboChildMenuSelected(ParentMenu))
            {
                ComboChildMenuItemSelected(btnName);
                return;
            }

            if (ParentMenu == "MenuItemSizes")
            {


                CurrentLineType = "O";
                if (Session.currentToppingCollection != null)
                {
                    lblWhere.Text = Session.selectedMenuItem.Order_Description + " - " + Session.currentToppingCollection.currentCatalogOptionGroups.Description;

                    tlpToppings.Visible = true;
                    tlpToppings.BringToFront();
                    //panelMinMax.Visible = true;
                    uC_Customer_order_Header1.pnl_MinMax.Visible = true;

                    if (Session.selectedMenuItem != null) // && selectedMenuItemSizes != null
                    {
                        //TO DO //Uncomment with Specialty Pizza functionality
                        if (!Convert.ToBoolean(Session.selectedMenuItem.Specialty_Pizza))
                            btnSpec.Visible = true;
                        else
                            btnSpec.Visible = false;
                    }
                    else
                        btnSpec.Visible = false;
                }
                else
                {
                    if (!(Session.ProcessingCombo && Session.CurrentComboItem > 0))
                    {
                        CurrentLineType = "";
                        int HoldSelectedLineNumber = selectedLineNumber;
                        Button btnDynamic1 = MenuCategoryToClick(true);
                        if(btnDynamic1 != null ) DynamicButtonClick(btnDynamic1, new EventArgs());
                        if (Session.currentToppingCollection == null && CartFunctions.IsMoreThenOneSizes(Session.selectedMenuItem.Menu_Code)) selectedLineNumber = HoldSelectedLineNumber;
                    }
                }

                if (!(Session.ProcessingCombo && Session.CurrentComboItem > 0))
                {
                    Session.selectedMenuItemSizes = Session.menuItemSizes.Find(x => x.Menu_Code == Session.selectedMenuItem.Menu_Code && x.Size_Code == btnName);
                    CartFunctions.PopulateDefaultToppings(ref Session.cart, selectedLineNumber, "",
                                                            Session.selectedMenuItem.Menu_Code, "",
                                                            Session.selectedMenuItem.Specialty_Pizza_Code);

                }

                //Session.cartToppings = new List<Topping>();

            }
            else
                DyanmicButtonValue(ParentMenu, btnName, false, string.Empty);
            CartFunctions.GetCart(ParentMenu, btnName, ref selectedLineNumber);

            if (Session.selectedMenuItem != null && Session.selectedMenuItems != null && Session.selectedMenuItems.Count > 0)
            {
                if (!Session.selectedMenuItems.ContainsKey(Session.selectedMenuItem.Menu_Code))
                {
                    Session.selectedMenuItems.Add(Session.selectedMenuItem.Menu_Code, Session.selectedMenuItem.MenuItemType);
                }
            }

            CheckcartItem();
            RefreshCartUI();
            CartControl();

            if (AutoClickSize)
            {
                AutoClickSize = false;
                DynamicButtonClick(btnAutoClickSize, new EventArgs());

            }

            if (ParentMenu == "MenuItemSizes")
            {
                if (Session.ProcessingCombo && Session.CurrentComboItem > 0)
                {
                    HandleComboOptions();
                }
            }

            if (ParentMenu == "OrderLineCoupons")
            {
                CurrentLineType = "";
                Button btnDynamic1 = MenuCategoryToClick(true);
                DynamicButtonClick(btnDynamic1, new EventArgs());
            }

            if (ParentMenu == "OrderCoupons")
            {
                CurrentLineType = "";
                uC_Customer_OrderMenu.cmdOrderCoupons.PerformClick();
            }
        }

        private async void DyanmicButtonValue(string ParentMenu, string btnName, bool cartLineSelected, string menuItemSize)
        {
            flowLayoutPanelMenuItems.Visible = true;
            flowLayoutPanelMenuItems.BringToFront();
            flowLayoutPanelMenuItems.Controls.Clear();
            flowLayoutPanelSpecialtyPizzas.Visible = false;
            int x = Constants.HorizontalSpace, y = Constants.VerticalSpace;

            if (ParentMenu == "MenuCategories")
            {
                if (!CartClicked) selectedLineNumber = -1;
                lblWhere.Text = APILayer.GetCatalogText(LanguageConstant.cintMenuItems);

                pnl_Quantity.Visible = false;
                pnl_Quantity.SendToBack();
                tlpToppings.Visible = false;
                tlpToppings.SendToBack();
                flowLayoutPanelSpecialtyPizzas.Visible = false;
                flowLayoutPanelSpecialtyPizzas.SendToBack();
                Session.currentToppingCollection = null;
                //panelMinMax.Visible = false;
                uC_Customer_order_Header1.pnl_MinMax.Visible = false;
                //lblSelectedValue.Text = "0";
                uC_Customer_order_Header1.lblSelectedValue.Text = "0";
                Session.currentMenuCategoryCode = btnName;

                List<CatalogMenuItems> currentMenuItems = APILayer.GetMenuItems(btnName, TempOrderTypeCode == "" ? Session.selectedOrderType : TempOrderTypeCode);
                CartFunctions.RemoveItemsOrderTypeFromMenuItems(TempOrderTypeCode == "" ? Session.selectedOrderType : TempOrderTypeCode);
                TempOrderTypeCode = "";
                foreach (CatalogMenuItems catalogMenuItems in currentMenuItems)
                {
                    if (!Session.menuItems.Any(z => z.Menu_Code == catalogMenuItems.Menu_Code)) Session.menuItems.Add(catalogMenuItems);
                }

                PopulateMenuItems(1);

                //Session.MenuCategoryButtonClicked = false;
            }
            else if (ParentMenu == "MenuItems")
            {
                StartTimer();
                uC_Customer_OrderMenu.ConvertExittoCancel();

                Session.selectedMenuItem = Session.menuItems.Find(z => z.Menu_Code == btnName);

                if (Session.selectedMenuItem != null)
                    lblWhere.Text = Session.selectedMenuItem.Order_Description + " - " + APILayer.GetCatalogText(LanguageConstant.cintSizes);

                List<CatalogMenuItemSizes> CurrentMenuItemSizes = APILayer.GetMenuItemSizes(btnName, Session.selectedOrderType);
                CartFunctions.RemoveItemSizesOrderTypeFromMenuItemSizes(Session.selectedOrderType);
                foreach (CatalogMenuItemSizes catalogMenuItemSizes in CurrentMenuItemSizes)
                {
                    if (!Session.menuItemSizes.Any(z => z.Menu_Code == catalogMenuItemSizes.Menu_Code && z.Size_Code == catalogMenuItemSizes.Size_Code)) Session.menuItemSizes.Add(catalogMenuItemSizes);
                }

                if (CurrentMenuItemSizes.Count == 1)
                    goto GetToppings;

                foreach (var itemSize in CurrentMenuItemSizes)
                {
                    Button btnDynamic = new Button();

                    btnDynamic.Location = new System.Drawing.Point(x, y);
                    btnDynamic.Size = new System.Drawing.Size(Constants.MenuCardButtonWidthG, Constants.ButtonHeightG);

                    btnDynamic.Name = itemSize.Size_Code.Trim(); ;
                    btnDynamic.Tag = "MenuItemSizes";
                    btnDynamic.Text = itemSize.Description.Trim();
                    btnDynamic.Font = new Font(new FontFamily("Microsoft Sans Serif"), 8);
                    if (cartLineSelected && menuItemSize.ToUpper().Trim() == itemSize.Size_Code.ToUpper().Trim())
                    {
                        btnDynamic.BackColor = Session.DefaultEntityColor;
                        //Session.currentMenuCategoryCode = itemSize.
                    }
                    else
                        btnDynamic.BackColor = SystemColors.Control;
                    btnDynamic.Margin = new Padding(0);
                    btnDynamic.Enabled = itemSize.Enabled ? itemSize.EnabledInv : itemSize.Enabled ;
                    btnDynamic.TextAlign = ContentAlignment.MiddleCenter;
                    if (itemSize.Default_Size)
                        btnDynamic.BackColor = Session.ToppingSizeSingleColor;

                    flowLayoutPanelMenuItems.Controls.Add(btnDynamic);
                    x += Constants.MenuCardButtonWidthG;
                    btnDynamic.Click += new EventHandler(this.DynamicButtonClick);
                }

            GetToppings: PopulateToppings(btnName);
                if (CurrentMenuItemSizes.Count == 1)
                {
                    AutoClickSize = true;
                    btnAutoClickSize = new Button();
                    btnAutoClickSize.Name = CurrentMenuItemSizes[0].Size_Code;
                    btnAutoClickSize.Tag = "MenuItemSizes";

                    //if(Session.ProcessingCombo && Session.ComboGroup >0 && Session.CurrentComboItem == 0)
                    //    ComboParentWithSingleSize = true;


                    //    ClickonCurrentSelectedItemCatagory();

                    //ComboParentWithSingleSize = false;
                }
                else
                {
                    CartItem curretCartItem = new CartItem();
                    curretCartItem = CartFunctions.GetCurrentCartItem(selectedLineNumber);

                    if (curretCartItem != null)
                    {
                        if (!curretCartItem.Prompt_For_Size)
                        {
                            AutoClickSize = true;
                            btnAutoClickSize = new Button();
                            btnAutoClickSize.Name = CurrentMenuItemSizes[CurrentMenuItemSizes.Count - 1].Size_Code;
                            btnAutoClickSize.Tag = "MenuItemSizes";
                        }
                    }
                }

                if (Session.selectedMenuItem != null)
                    if (Convert.ToBoolean(Session.selectedMenuItem.Combo_Meal)) CurrentLineType = "B"; else CurrentLineType = "M";
            }


        }

        public void RefreshCartUI()
        {
            if (dtCart != null)
            {
                if (Session.handleRemakebutton == true)
                {
                    uC_Customer_OrderMenu.cmdRemake.Enabled = true;
                    uC_Customer_OrderMenu.ConvertExittoCancel();

                }
                else
                {
                    uC_Customer_OrderMenu.cmdRemake.Enabled = false;
                    RemakeButtonStatus();

                }
                dtCart.Rows.Clear();
            }
            else
            {
                if (Session.handleRemakebutton == true)
                {
                    uC_Customer_OrderMenu.cmdRemake.Enabled = true;
                    uC_Customer_OrderMenu.ConvertExittoCancel();

                }
                else
                {
                    uC_Customer_OrderMenu.cmdRemake.Enabled = false;

                }
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

            if (Session.RemakeOrder == true)
            {
                Remake_Display();
            }


            if (Session.selectedOrderType == "I")
            {
                uC_Customer_OrderMenu.cmdTimedOrders.Enabled = false;
            }
            else
            {
                uC_Customer_OrderMenu.cmdTimedOrders.Enabled = true;
            }

            if (Session.cart != null && Session.cart.cartItems != null && Session.cart.cartItems.Count > 0)
                Session.CartInitiated = true;

            if (Session.CartInitiated)
            {
                uC_Customer_OrderMenu.cmdTimeClock.Enabled = false;
                uC_Customer_OrderMenu.cmdLogin.Enabled = false;
                uC_Customer_OrderMenu.cmdChangeOrders.Enabled = false;
            }


            if (Session.cart != null) //&& cart.cartItems[0].Location_Code != null)
            {
                if (Session.cart.cartItems.Count > 0)
                {
                    foreach (CartItem cartItem in Session.cart.cartItems)
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
                        dr["Item"] = cartItem.Combo_Group > 0 ? (UserTypes.TabSpace + cartItem.Description) : (cartItem.Description);
                        dr["VegNVegColor"] = cartItem.MenuItemType == true ? Properties.Resources.NonVeg : Properties.Resources.Veg;
                        dr["Price"] = cartItem.Combo_Group > 0 ? "" : String.Format(Session.DisplayFormat, Math.Floor(cartItem.Price * UserFunctions.GetDivider()) / UserFunctions.GetDivider());
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
                        else if (cartItem.Topping_String == "" && cartItem.Specialty_Pizza == false && cartItem.itemOptions != null && cartItem.itemOptions.Count > 0 && OptionGroupComplete == false && OptionGroupDescription != "")
                        {
                            dr = dtCart.NewRow();
                            dr["CartId"] = cartItem.CartId;
                            dr["Line_Number"] = cartItem.Line_Number;
                            dr["Menu_Category_Code"] = cartItem.Menu_Category_Code;
                            dr["Menu_Code"] = cartItem.Menu_Code;
                            dr["Size_Code"] = cartItem.Size_Code;
                            dr["Qty"] = "";
                            dr["Item"] = UserTypes.TabSpace + "-->" + OptionGroupDescription;
                            dr["VegNVegColor"] = null;
                            dr["Price"] = "";
                            dr["LineType"] = "O";
                            dr["Combo_Group"] = cartItem.Combo_Group;
                            dr["Combo_Item_Number"] = cartItem.Combo_Item_Number;
                            dtCart.Rows.Add(dr);
                        }

                        if (!string.IsNullOrEmpty(cartItem.Coupon_Code) && cartItem.Coupon_Code != "0")
                        {
                            dr = dtCart.NewRow();
                            dr["CartId"] = cartItem.CartId;
                            dr["Line_Number"] = cartItem.Line_Number;
                            dr["Menu_Category_Code"] = cartItem.Menu_Category_Code;
                            dr["Menu_Code"] = cartItem.Menu_Code;
                            dr["Size_Code"] = cartItem.Size_Code;
                            dr["Qty"] = "";
                            dr["Item"] = cartItem.Combo_Group > 0 ? UserTypes.TabSpace + UserTypes.TabSpace + cartItem.Coupon_Description : UserTypes.TabSpace + cartItem.Coupon_Description;
                            dr["VegNVegColor"] = null;
                            dr["Price"] = cartItem.Order_Line_Coupon_Amount > 0 ? String.Format(Session.DisplayFormat, (-1) * cartItem.Order_Line_Coupon_Amount) : "";
                            dr["LineType"] = "C";
                            dr["Combo_Group"] = cartItem.Combo_Group;
                            dr["Combo_Item_Number"] = cartItem.Combo_Item_Number;
                            dtCart.Rows.Add(dr);
                        }
                    }
                }


                ComboFunctions.AddCombotoCartUI(ref dtCart, Session.cart);



                if (Session.cart.cartHeader.Order_Adjustments > 0)
                {
                    DataRow dr = dtCart.NewRow();
                    dr["CartId"] = Session.cart.cartHeader.CartId;
                    dr["Line_Number"] = "";
                    dr["Menu_Category_Code"] = "";
                    dr["Menu_Code"] = "";
                    dr["Size_Code"] = "";
                    dr["Qty"] = "";
                    dr["Item"] = Session.selectedOrderCoupon.Description;
                    dr["VegNVegColor"] = null;
                    dr["Price"] = String.Format(Session.DisplayFormat, (-1) * Session.cart.cartHeader.Order_Adjustments);
                    dr["LineType"] = "E";
                    dtCart.Rows.Add(dr);
                }


                if (Session.cart.cartHeader.Order_Coupon_Total > 0)
                {
                    DataRow dr = dtCart.NewRow();
                    dr["CartId"] = Session.cart.cartHeader.CartId;
                    dr["Line_Number"] = "";
                    dr["Menu_Category_Code"] = "";
                    dr["Menu_Code"] = "";
                    dr["Size_Code"] = "";
                    dr["Qty"] = "";
                    dr["Item"] = Session.selectedOrderCoupon.Description;
                    dr["VegNVegColor"] = null;
                    dr["Price"] = String.Format(Session.DisplayFormat, (-1) * Session.cart.cartHeader.Order_Coupon_Total);
                    dr["LineType"] = "E";
                    dtCart.Rows.Add(dr);
                }

                //if (Session.cart.cartHeader.Delivery_Fee > 0)
                //{
                //    DataRow dr = dtCart.NewRow();
                //    dr["CartId"] = Session.cart.cartHeader.CartId;
                //    dr["Line_Number"] = "";
                //    dr["Menu_Category_Code"] = "";
                //    dr["Menu_Code"] = "";
                //    dr["Size_Code"] = "";
                //    dr["Qty"] = "";
                //    dr["Item"] = APILayer.GetCatalogText(304);
                //    dr["VegNVegColor"] = null;
                //    dr["Price"] = String.Format(Session.DisplayFormat, Session.cart.cartHeader.Delivery_Fee);
                //    dr["LineType"] = "D";
                //    dtCart.Rows.Add(dr);
                //}


                int ExemptChargeId = 0;
                string AggregatorGSTDesc = string.Empty;
                decimal AggregatorGSTValue = 0;

                if (Session.cart.orderAdditionalCharges != null && Session.cart.orderAdditionalCharges.Count > 0)
                {
                    ExemptChargeId = Convert.ToInt32( Convert.ToString(SystemSettings.GetSettingValue("AggregatorGSTChargeID", Session._LocationCode)));

                    for (int i = 0; i <= Session.cart.orderAdditionalCharges.Count - 1; i++)
                    {
                        if (Session.cart.orderAdditionalCharges[i].Charge_Id != ExemptChargeId)
                        {
                            DataRow dr = dtCart.NewRow();
                            dr["CartId"] = Session.cart.cartHeader.CartId;
                            dr["Line_Number"] = "";
                            dr["Menu_Category_Code"] = "";
                            dr["Menu_Code"] = "";
                            dr["Size_Code"] = "";
                            dr["Qty"] = "";
                            dr["Item"] = Session.cart.orderAdditionalCharges[i].ChargeDesc;
                            dr["VegNVegColor"] = null;
                            dr["Price"] = String.Format(Session.DisplayFormat, Session.cart.orderAdditionalCharges[i].Amount);
                            dr["LineType"] = "D";
                            dtCart.Rows.Add(dr);
                        }
                        else
                        {
                            AggregatorGSTDesc = Session.cart.orderAdditionalCharges[i].ChargeDesc;
                            AggregatorGSTValue = Session.cart.orderAdditionalCharges[i].Amount;
                        }
                    }

                }

                dgvCart.DataSource = dtCart;
                ((DataGridViewImageColumn)dgvCart.Columns["VegNVegColor"]).DefaultCellStyle.NullValue = null;


                if (dgvCartTotals.DataSource != null)
                {
                    

                    DataTable dtCartTotals = (DataTable)dgvCartTotals.DataSource;                                       
                    dtCartTotals.Rows[0]["Value"] = String.Format(Session.DisplayFormat, (Session.cart.cartHeader.SubTotal - Session.cart.cartHeader.Bottle_Deposit));
                    dtCartTotals.Rows[1]["Value"] = String.Format(Session.DisplayFormat, Session.cart.cartHeader.Bottle_Deposit);
                    dtCartTotals.Rows[2]["Value"] = String.Format(Session.DisplayFormat, Session.cart.cartHeader.Credit);
                    dtCartTotals.Rows[3]["Value"] = String.Format(Session.DisplayFormat, Session.cart.orderUDT.Sales_Tax1);
                    dtCartTotals.Rows[4]["Value"] = String.Format(Session.DisplayFormat, Session.cart.orderUDT.Sales_Tax2);
                    if (dtCartTotals.Rows.Count == 10)
                    {
                        dtCartTotals.Rows[5]["Value"] = String.Format(Session.DisplayFormat, Session.cart.orderUDT.Sales_Tax3);
                        dtCartTotals.Rows[6]["Value"] = String.Format(Session.DisplayFormat, Session.cart.orderUDT.Sales_Tax4);
                        dtCartTotals.Rows[7]["Value"] = String.Format(Session.DisplayFormat, 0);//Session.cart.cartHeader.Coupon_Amount);
                        dtCartTotals.Rows[8]["Name"] = String.Format(Session.DisplayFormat, AggregatorGSTDesc);
                        dtCartTotals.Rows[8]["Value"] = String.Format(Session.DisplayFormat, AggregatorGSTValue);
                        dtCartTotals.Rows[9]["Value"] = String.Format(Session.DisplayFormat, Session.cart.cartHeader.Total);
                        dtCartTotals.Rows[9]["Value"] = String.Format("{0:n}", Session.cart.cartHeader.Total);
                    }
                    else
                    {
                        dtCartTotals.Rows[5]["Value"] = String.Format(Session.DisplayFormat, 0);// Session.cart.cartHeader.Coupon_Amount);
                        dtCartTotals.Rows[6]["Name"] = String.Format(Session.DisplayFormat, AggregatorGSTDesc);
                        dtCartTotals.Rows[6]["Value"] = String.Format(Session.DisplayFormat, AggregatorGSTValue);
                        dtCartTotals.Rows[7]["Value"] = String.Format(Session.DisplayFormat, Session.cart.cartHeader.Total);
                    }

                    if (AggregatorGSTValue > 0)
                    {
                        if (dtCartTotals.Rows.Count == 10)
                            dgvCartTotals.Rows[8].Visible = true;
                        else
                            dgvCartTotals.Rows[6].Visible = true;

                        if (dgvCartTotals.Size.Height > 28)
                            dgvCartTotals.Size = new Size(274, 208);
                    }
                    else
                    {
                        if (dtCartTotals.Rows.Count == 10)
                            dgvCartTotals.Rows[8].Visible = false;
                        else
                            dgvCartTotals.Rows[6].Visible = false;

                        if (dgvCartTotals.Size.Height > 28)
                            dgvCartTotals.Size = new Size(274, 188);
                    }

                    
                }

                if (Session.cart.cartHeader.Total > 0)
                {
                    if (!string.IsNullOrEmpty(Session.cart.Customer.Phone_Number) || Convert.ToInt64(Session.cart.Customer.Customer_Code) != 0)
                    {
                        uC_Customer_OrderMenu.cmdRemake.Enabled = Session.handleRemakebutton;
                    }

                    btnOnHold.Enabled = true;
                    uC_Customer_OrderMenu.cmdOrderCoupons.Enabled = true;
                    //uC_Customer_OrderMenu.cmdRemake.Enabled = true;
                    uC_CustomerOrderBottomMenu1.cmdPay.Enabled = true;

                    btn_Coupons.Enabled = true;
                    btn_Quantity.Enabled = true;
                    btn_Instructions.Enabled = true;
                }
                else
                {
                    btnOnHold.Enabled = false;
                    uC_Customer_OrderMenu.cmdOrderCoupons.Enabled = false;
                    //uC_Customer_OrderMenu.cmdRemake.Enabled = false;
                    uC_CustomerOrderBottomMenu1.cmdPay.Enabled = false;

                    btn_Coupons.Enabled = false;
                    btn_Quantity.Enabled = false;
                    btn_Instructions.Enabled = false;
                }

            }
            else
            {
                uC_Customer_OrderMenu.cmdOrderCoupons.Enabled = false;
                btnOnHold.Enabled = false;
                //uC_Customer_OrderMenu.cmdRemake.Enabled = false;
                uC_CustomerOrderBottomMenu1.cmdPay.Enabled = false;

                btn_Coupons.Enabled = false;
                btn_Quantity.Enabled = false;
                btn_Instructions.Enabled = false;

                DataRow dr = dtCart.NewRow();
                dtCart.Rows.Add(dr);
                dgvCart.DataSource = dtCart;
                ((DataGridViewImageColumn)dgvCart.Columns["VegNVegColor"]).DefaultCellStyle.NullValue = null;

                ClearCartTotals();
            }

            FormatCartGrid();
            //--------------------Timed Order display on cart
            if (Session.cart != null)
            {
                if (Session.cart.cartHeader.Delayed_Date != DateTime.MinValue)
                {
                    dgvCart.Location = new Point(0, 22);
                    dgvCart.Size = new Size(273, 342);
                    lbltimed.Visible = true;
                    lbltimedorder.Visible = true;
                    lbltimedorder.Text = Session.cart.cartHeader.Delayed_Date.ToString("dd/MM/yyyy HH:mm tt");
                }
                else
                {
                    dgvCart.Location = new Point(0, 0);
                    dgvCart.Size = new Size(273, 364);
                    lbltimed.Visible = false;
                    lbltimedorder.Visible = false;
                }
            }
            //-------------------------------

            ////Combos For Upsell
            //if (Session.cart != null && Session.cart.cartItems.Count>1) 
            //{
            //    //TO DO //Checks for Combo Item, dont use combo items
            //    if (ComboFunctions.IsCombosAvailableForUpsell(Session.cart.cartItems[0].Menu_Code, Session.cart.cartItems[0].Size_Code, Session.cart.cartItems[1].Menu_Code, Session.cart.cartItems[1].Size_Code))
            //    {
            //        frmUpsellCombo frmUpsellComboObj = new frmUpsellCombo(Session.cart.cartItems[0].Menu_Code, Session.cart.cartItems[0].Size_Code, Session.cart.cartItems[1].Menu_Code, Session.cart.cartItems[1].Size_Code);
            //        frmUpsellComboObj.ShowDialog();
            //    }
            //}

            //if (!(Session.RefreshFromModifyForOrder || Session.RefreshFromHistoryForOrder || Session.RefreshFromRemakeForOrder))
                if (Session.CartInitiated && Session.currentToppingCollection == null && Session.PreventItemwiseUpsell == false) ItemwiseUpsell();

        }
        void Remake_Display()
        {

            string Order_type;
            panelAttributes.Enabled = false;

            uC_Customer_OrderMenu.cmdHistory.Enabled = false;
            uC_Customer_OrderMenu.cmdInformation.Enabled = false;
            uC_Customer_OrderMenu.cmdFunctions.Enabled = false;
            uC_Customer_OrderMenu.cmdLogin.Enabled = false;
            uC_Customer_OrderMenu.cmdChangeOrders.Enabled = false;
            uC_Customer_OrderMenu.cmdDelivery_Info.Enabled = false;
            uC_Customer_OrderMenu.cmdTimeClock.Enabled = false;
            uC_Customer_OrderMenu.cmdTimedOrders.Enabled = false;
            btn_Plus.Enabled = false;
            btn_Minus.Enabled = false;
            btn_Quantity.Enabled = false;
            btn_Instructions.Enabled = false;
            btn_Coupons.Enabled = false;



            uC_Customer_OrderMenu.ConvertExittoCancel();
            uC_Customer_OrderMenu.cmdRemake.Enabled = false;

            foreach (Control control in uC_CustomerOrderBottomMenu1.Controls)
            {
                if (control.GetType() == typeof(FlowLayoutPanel))
                {
                    foreach (Control _control in control.Controls)
                    {
                        if (Convert.ToString(_control.Tag) == "C" || Convert.ToString(_control.Tag) == "D" || Convert.ToString(_control.Tag) == "I" || Convert.ToString(_control.Tag) == "P")
                            ((Button)_control).Enabled = false;
                    }
                }
            }

        }
        private void RemakeButtonStatus()
        {
            if (Session.cart == null || Session.cart.Customer == null || Convert.ToInt64(Session.cart.Customer.Customer_Code) == 0)
                return;

            uC_Customer_OrderMenu.HandleRemakeButton(Session.handleRemakebutton);
        }

        private void dgvCart_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                pnl_Quantity.Visible = false;
                pnl_Quantity.SendToBack();
                tlpToppings.Visible = false;
                tlpToppings.SendToBack();

                btn_Coupons.Enabled = true;
                btn_Quantity.Enabled = true;
                btn_Instructions.Enabled = true;

                if (e.RowIndex >= 0)
                {
                    if (dgvCart != null && dgvCart.Columns.Contains("LineType"))
                    {
                        HiglightMenuCategory(Convert.ToString(dgvCart.Rows[e.RowIndex].Cells["Menu_Category_Code"].Value));

                        CartClicked = true;

                        string Combo_group = Convert.ToString(dgvCart.Rows[e.RowIndex].Cells["Combo_Group"].Value);


                        if (Combo_group != "" && Convert.ToInt32(Combo_group) > 0)
                        {
                            Session.CurrentComboGroup = Convert.ToInt32(Convert.ToString(dgvCart.Rows[e.RowIndex].Cells["Combo_Group"].Value));
                            Session.CurrentComboItem = Convert.ToInt32(Convert.ToString(dgvCart.Rows[e.RowIndex].Cells["Combo_Item_Number"].Value));
                            Session.ProcessingCombo = true;

                            if (Session.CurrentComboItem == 0)
                                ComboParentComboGroup = Session.CurrentComboGroup;
                            else
                                ComboParentComboGroup = 0;
                        }
                        else
                        {
                            Session.CurrentComboGroup = 0;
                            Session.CurrentComboItem = 0;
                            Session.ProcessingCombo = false;
                            Session.currentMenuCategoryCode = Convert.ToString(dgvCart.Rows[e.RowIndex].Cells["Menu_Category_Code"].Value);
                            ComboParentComboGroup = 0;
                        }


                        if (Convert.ToString(dgvCart.Rows[e.RowIndex].Cells["LineType"].Value) == "M" || Convert.ToString(dgvCart.Rows[e.RowIndex].Cells["LineType"].Value) == "G" || Convert.ToString(dgvCart.Rows[e.RowIndex].Cells["LineType"].Value) == "B")
                        {
                            selectedLineNumber = Convert.ToInt32(Convert.ToString(dgvCart.Rows[e.RowIndex].Cells["Line_Number"].Value));

                            if (Session.ProcessingCombo && Session.CurrentComboItem > 0)
                            {
                                //if (Session.pblnModifyingOrder) Session.MenuCategoryButtonClicked = true;
                                ComboFunctions.ComboMenuItemsCheck(Convert.ToString(dgvCart.Rows[e.RowIndex].Cells["Menu_Code"].Value));
                                ComboFunctions.ComboMenuItemSizesCheck(Convert.ToString(dgvCart.Rows[e.RowIndex].Cells["Menu_Code"].Value), Convert.ToString(dgvCart.Rows[e.RowIndex].Cells["Size_Code"].Value));
                                HandleComboMenuItems();
                            }
                            else
                            {
                                CartFunctions.MenuItemsCheck(Convert.ToString(dgvCart.Rows[e.RowIndex].Cells["Menu_Code"].Value), Convert.ToString(dgvCart.Rows[e.RowIndex].Cells["Menu_Category_Code"].Value));
                                CartFunctions.MenuItemSizesCheck(Convert.ToString(dgvCart.Rows[e.RowIndex].Cells["Menu_Code"].Value), Convert.ToString(dgvCart.Rows[e.RowIndex].Cells["Size_Code"].Value));
                                DyanmicButtonValue("MenuItems", Convert.ToString(dgvCart.Rows[e.RowIndex].Cells["Menu_Code"].Value), true, Convert.ToString(dgvCart.Rows[e.RowIndex].Cells["Size_Code"].Value));
                            }

                            if (AutoClickSize)
                            {
                                ClickonCurrentSelectedItemCatagory(Convert.ToString(dgvCart.Rows[e.RowIndex].Cells["Menu_Category_Code"].Value));
                                AutoClickSize = false;
                            }
                        }
                        else if (Convert.ToString(dgvCart.Rows[e.RowIndex].Cells["LineType"].Value) == "I")
                        {
                            selectedLineNumber = Convert.ToInt32(Convert.ToString(dgvCart.Rows[e.RowIndex].Cells["Line_Number"].Value));
                            if (selectedLineNumber > 0)
                            {
                                using (frmReason objfrmReason = new frmReason(Convert.ToInt32(enumReasonGroupID.CookingInstruction)))
                                {
                                    objfrmReason.SelectedLineNumber = selectedLineNumber;
                                    objfrmReason.HighlightItemReason(selectedLineNumber);
                                    objfrmReason.ShowDialog();
                                }
                            }
                        }
                        else if (Convert.ToString(dgvCart.Rows[e.RowIndex].Cells["LineType"].Value) == "O")
                        {
                            selectedLineNumber = Convert.ToInt32(Convert.ToString(dgvCart.Rows[e.RowIndex].Cells["Line_Number"].Value));

                            if (Session.ProcessingCombo && Session.CurrentComboItem > 0)
                            {
                                ComboFunctions.ComboMenuItemsCheck(Convert.ToString(dgvCart.Rows[e.RowIndex].Cells["Menu_Code"].Value));
                                ComboFunctions.ComboMenuItemSizesCheck(Convert.ToString(dgvCart.Rows[e.RowIndex].Cells["Menu_Code"].Value), Convert.ToString(dgvCart.Rows[e.RowIndex].Cells["Size_Code"].Value));
                            }
                            else
                            {
                                CartFunctions.MenuItemsCheck(Convert.ToString(dgvCart.Rows[e.RowIndex].Cells["Menu_Code"].Value), Convert.ToString(dgvCart.Rows[e.RowIndex].Cells["Menu_Category_Code"].Value));
                                CartFunctions.MenuItemSizesCheck(Convert.ToString(dgvCart.Rows[e.RowIndex].Cells["Menu_Code"].Value), Convert.ToString(dgvCart.Rows[e.RowIndex].Cells["Size_Code"].Value));
                                PopulateToppings(Convert.ToString(dgvCart.Rows[e.RowIndex].Cells["Menu_Code"].Value));
                            }

                            //Session.MenuCategoryButtonClicked = false;
                            ClickEventProcess("MenuItemSizes", Convert.ToString(dgvCart.Rows[e.RowIndex].Cells["Size_Code"].Value));
                        }
                        else if (Convert.ToString(dgvCart.Rows[e.RowIndex].Cells["LineType"].Value) == "C")
                        {
                            selectedLineNumber = Convert.ToInt32(Convert.ToString(dgvCart.Rows[e.RowIndex].Cells["Line_Number"].Value));
                            btn_Coupons_Click(btn_Coupons, new EventArgs());
                        }
                        else if (Convert.ToString(dgvCart.Rows[e.RowIndex].Cells["LineType"].Value) == "E")
                        {
                            OrderCoupons();
                        }
                        else
                        {
                            selectedLineNumber = -1;
                        }

                        CartClicked = false;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Trace("ERROR", "frmOrder-dgvcart_cellclick(): " + ex.Message, ex, true);
            }
        }

        private void QuantityChange(bool Replace, int Qty)
        {
            try
            {
                if (Session.cart != null && Session.cart.cartItems != null && Session.cart.cartItems.Count > 0)
                {
                    int PreviousItemCount = 0;
                    int PreviousComboCount = 0;
                    bool QtyChanged = false;
                    int FocusedRow = dgvCart.SelectedCells.Count > 0 ? dgvCart.SelectedCells[0].RowIndex : 0;

                    PreviousItemCount = Session.cart.cartItems.Count;

                    if (Session.cart.itemCombos != null)
                        PreviousComboCount = Session.cart.itemCombos.Count;

                    if (selectedLineNumber > 0)
                        ComboParentComboGroup = Session.cart.cartItems.Find(x => x.Line_Number == selectedLineNumber).Combo_Group;
                    else
                        ComboParentComboGroup = Session.cart.cartItems.Find(x => x.Line_Number == Session.cart.cartItems[Session.cart.cartItems.Count - 1].Line_Number).Combo_Group;

                    if ((Session.ProcessingCombo && Session.CurrentComboGroup > 0) || ComboParentComboGroup > 0)
                        QtyChanged = CartFunctions.QuantityChangeCombo(Replace, Qty, (ComboParentComboGroup > 0 ? ComboParentComboGroup : Session.CurrentComboGroup));
                    else
                        QtyChanged = CartFunctions.QuantityChange(Replace, Qty, selectedLineNumber);

                    ComboParentComboGroup = 0;

                    if (QtyChanged)
                    {
                        RefreshCartUI();
                        CartControl();
                    }

                    if (FocusedRow > -1 && dgvCart.Rows.Count > 0 && dgvCart.Rows.Count > FocusedRow)
                        dgvCart.CurrentCell = dgvCart.Rows[FocusedRow].Cells["Qty"];

                    if (PreviousItemCount != Session.cart.cartItems.Count || (Session.cart.itemCombos != null && PreviousComboCount != Session.cart.itemCombos.Count))
                    {
                        //tlpToppings.Visible = false;
                        //tlpToppings.SendToBack();
                        //flowLayoutPanelSpecialtyPizzas.Visible = false;
                        //flowLayoutPanelSpecialtyPizzas.SendToBack();
                        //pnl_Quantity.Visible = false;
                        //pnl_Quantity.SendToBack();

                        //Session.currentToppingCollection = null;
                        //uC_Customer_order_Header1.pnl_MinMax.Visible = false;
                        //uC_Customer_order_Header1.lblSelectedValue.Text = "0";

                        Button btnDynamic1 = MenuCategoryToClick(true);
                        DynamicButtonClick(btnDynamic1, new EventArgs());

                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Trace("ERROR", "frmOrder-quantitychange(): " + ex.Message, ex, true);
            }
        }

        private void btnDiscardCart_Click(object sender, EventArgs e)
        {
            try
            {

                //if (Session.cart != null && Session.cart.cartItems.Count > 0)
                //{
                //    if (CustomMessageBox.Show(MessageConstant.AreYouSure, "Confirmation", CustomMessageBox.Buttons.YesNo, CustomMessageBox.Icon.Question) == DialogResult.No)
                //    {
                //        return;
                //    }

                //    Session.cart = APILayer.DiscardCart(Session.cart.cartHeader.CartId, Session.cart);
                //    RefreshCartUI();

                //}

            }
            catch (Exception ex)
            {
                Logger.Trace("ERROR", "frmOrder-btndiscardcart_click(): " + ex.Message, ex, true);
            }
        }

        private void InitializeToppingControl()
        {

            flowLayoutPanelToppingSizes.Controls.Clear();
            int x = Constants.HorizontalSpace, y = Constants.VerticalSpace;

            Button btn = new Button();
            btn.Location = new System.Drawing.Point(x, y);
            btn.Size = new System.Drawing.Size(Constants.ButtonWidthG, Constants.ButtonHeightG);
            btn.Name = "btnOK";
            btn.Tag = "";
            btn.Text = "OK";
            btn.Font = new Font(new FontFamily("Microsoft Sans Serif"), 8);
            btn.BackColor = SystemColors.Control;
            btn.Margin = new Padding(0);
            btn.TextAlign = ContentAlignment.BottomCenter;
            btn.Image = Properties.Resources._171;
            btn.ImageAlign = ContentAlignment.TopCenter;
            btn.TextImageRelation = TextImageRelation.ImageAboveText;
            //btn.BackgroundImage = Properties.Resources._171;
            //btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            flowLayoutPanelToppingSizes.Controls.Add(btn);
            btn.Click += new EventHandler(this.ToppingsOkButtonClick);

            x += Constants.ButtonWidthG;

            foreach (CatalogText catalogText in APILayer.GetToppingSizes())
            {
                btn = new Button();
                btn.Location = new System.Drawing.Point(x, y);
                btn.Size = new System.Drawing.Size(Constants.ButtonWidthG, Constants.ButtonHeightG);
                btn.Name = "btn" + catalogText.Modified_Text;
                btn.Tag = catalogText.Alternate_Text;
                btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
                btn.Font = new Font(new FontFamily("Microsoft Sans Serif"), 8);
                btn.Margin = new Padding(0);

                Color LocalColor = UserFunctions.GetColorbyAmountCode(Convert.ToString(catalogText.Alternate_Text));
                btn.BackColor = LocalColor == Color.Gray ? Session.ToppingColor : LocalColor;

                if (catalogText.Alternate_Text == "")
                {
                    btn.Image = Properties.Resources.fingerprint_1;
                    btn.ImageAlign = ContentAlignment.TopCenter;
                    //btn.BackgroundImage = Properties.Resources.fingerprint_1;//((System.Drawing.Image)(resources.GetObject("fingerprint_1")));  
                    btn.TextAlign = ContentAlignment.BottomCenter;
                }

                btn.Text = catalogText.Modified_Text;
                flowLayoutPanelToppingSizes.Controls.Add(btn);
                btn.Click += new EventHandler(this.ToppingSizesButtonClick);

                x += Constants.ButtonWidthG;
            }

            foreach (CatalogText catalogText in APILayer.GetSpecialtyPizzaText())
            {
                btnSpec = new Button();
                btnSpec.Location = new System.Drawing.Point(x, y);
                btnSpec.Size = new System.Drawing.Size(Constants.ButtonWidthG, Constants.ButtonHeightG);
                btnSpec.Name = "btn" + catalogText.Modified_Text;
                btnSpec.Tag = "Spec";
                btnSpec.Text = catalogText.Modified_Text;
                btnSpec.Font = new Font(new FontFamily("Microsoft Sans Serif"), 8);
                btnSpec.BackColor = SystemColors.Control;
                btnSpec.Margin = new Padding(0);
                flowLayoutPanelToppingSizes.Controls.Add(btnSpec);
                btnSpec.Click += btnSpecPizza_Click;

                x += Constants.ButtonWidthG;
            }


            x = 0;
            y = Constants.VerticalSpace;
            foreach (CatalogText catalogText in APILayer.GetItemParts())
            {
                btn = new Button();
                btn.Location = new System.Drawing.Point(x, y);
                btn.Size = new System.Drawing.Size(Constants.MenuCardButtonWidthG, Constants.ButtonHeightG);
                btn.Name = "btn" + catalogText.Modified_Text;
                btn.Tag = catalogText.Alternate_Text;
                btn.Text = catalogText.Modified_Text;
                btn.Anchor = AnchorStyles.Left;
                btn.Font = new Font(new FontFamily("Microsoft Sans Serif"), 8);
                btn.Margin = new Padding(0);
                btn.Padding = new Padding(0, 0, 0, 10);
                btn.BackColor = SystemColors.Control;
                if (btn.Text == "Whole") btn.BackColor = Color.LightCoral;
                btn.TextAlign = ContentAlignment.BottomCenter;
                btn.ImageAlign = ContentAlignment.TopCenter;
                btn.TextImageRelation = TextImageRelation.ImageAboveText;

                if (btn.Text == "Whole")
                    btn.Image = Properties.Resources._198;
                else if (btn.Text == "1st Half")
                    btn.Image = Properties.Resources._199;
                else
                    btn.Image = Properties.Resources._200;

                panelItemParts.Controls.Add(btn);
                btn.Click += new EventHandler(this.ToppingsItemPartsButtonClick);

                x += Constants.MenuCardButtonWidthG + 1;
            }


            x = panelItemParts.Width - (2 * Constants.MenuCardButtonWidthG) - (2 * 7);
            y = Constants.VerticalSpace;
            btnPrev = new Button();
            btnPrev.Location = new System.Drawing.Point(x, y);
            btnPrev.Size = new System.Drawing.Size(Constants.MenuCardButtonWidthG, Constants.ButtonHeightG);
            btnPrev.Name = "btnPrev";
            btnPrev.Tag = "";
            btnPrev.Text = "Previous";
            btnPrev.Font = new Font(new FontFamily("Microsoft Sans Serif"), 8);
            btnPrev.BackColor = SystemColors.Control;
            btnPrev.Margin = new Padding(0);
            btnPrev.Anchor = AnchorStyles.Right;
            btnPrev.Visible = false;
            panelItemParts.Controls.Add(btnPrev);
            btnPrev.Click += btnpPrev_Click;

            x += Constants.MenuCardButtonWidthG + 1;


            btnNext = new Button();
            btnNext.Location = new System.Drawing.Point(x, y);
            btnNext.Size = new System.Drawing.Size(Constants.MenuCardButtonWidthG, Constants.ButtonHeightG);
            btnNext.Name = "btnNext";
            btnNext.Tag = "";
            btnNext.Text = "Next";
            btnNext.Font = new Font(new FontFamily("Microsoft Sans Serif"), 8);
            btnNext.BackColor = SystemColors.Control;
            btnNext.Margin = new Padding(0);
            btnNext.Anchor = AnchorStyles.Right;
            btnNext.Visible = false;
            panelItemParts.Controls.Add(btnNext);
            btnNext.Click += btnNext_Click;

        }

        private void InitializeSpecialtyPizzaControl()
        {

            flowLayoutPanelSpecialtyPizzas.Controls.Clear();
            int x = Constants.HorizontalSpace, y = Constants.VerticalSpace;

            Button btnSpecOk = new Button();
            btnSpecOk.Location = new System.Drawing.Point(x, y);
            btnSpecOk.Size = new System.Drawing.Size(Constants.MenuCardButtonWidthG, Constants.ButtonHeightG);
            btnSpecOk.Name = "btnSpecOk";
            btnSpecOk.Tag = "";
            btnSpecOk.Text = "OK";
            btnSpecOk.Font = new Font(new FontFamily("Microsoft Sans Serif"), 8);
            btnSpecOk.BackColor = SystemColors.Control;
            btnSpecOk.Margin = new Padding(0);
            btnSpecOk.TextAlign = ContentAlignment.BottomCenter;
            btnSpecOk.BackgroundImage = Properties.Resources._171;
            btnSpecOk.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            flowLayoutPanelSpecialtyPizzas.Controls.Add(btnSpecOk);
            btnSpecOk.Click += BtnSpecOk_Click;

            x += Constants.MenuCardButtonWidthG;
        }

        private void BtnSpecOk_Click(object sender, EventArgs e)
        {
            int SelectedLineNumber = UserFunctions.ActualLineNumber(Session.cart, selectedLineNumber);
            CartItem curretCartItem = Session.cart.cartItems.Find(x => x.Line_Number == (SelectedLineNumber > 0 ? SelectedLineNumber : x.Line_Number));

            if (Session.ProcessingCombo && !String.IsNullOrEmpty(curretCartItem.Menu_Code))
                curretCartItem.Menu_Item_Choosen = true;

            pnl_Quantity.Visible = false;
            pnl_Quantity.SendToBack();
            tlpToppings.Visible = true;
            tlpToppings.BringToFront();
            flowLayoutPanelSpecialtyPizzas.Visible = false;
            flowLayoutPanelSpecialtyPizzas.SendToBack();

            PopulateToppingPage();

            int ItemPartButtonText = 0;
            if (Session.currentItemPart == UserTypes.ItemParts.FirstHalf)
                ItemPartButtonText = LanguageConstant.cint1stHalf;
            else if (Session.currentItemPart == UserTypes.ItemParts.SecondHalf)
                ItemPartButtonText = LanguageConstant.cint2ndHalf;
            else
                ItemPartButtonText = LanguageConstant.cintWhole;

            string ButtonText = APILayer.GetCatalogText(ItemPartButtonText);
            ToppingsItemPartsButtonClick(panelItemParts.Controls.OfType<Button>().FirstOrDefault(z => z.Text == ButtonText), new EventArgs());

        }

        private void btnSpecPizza_Click(object sender, EventArgs e)
        {
            try
            {
                if (btnSpec.Visible)
                {
                    int x = Constants.HorizontalSpace + Constants.MenuCardButtonWidthG, y = Constants.VerticalSpace;
                    Session.SpecialtyPizzasList = APILayer.GetSpecialtyPizzas(Session.selectedMenuItemSizes.Size_Code, Session.selectedMenuItem.Menu_Category_Code);
                    //TO DO For Combo

                    InitializeSpecialtyPizzaControl();

                    foreach (CatalogSpecialtyPizzas _catalogSpecialtyPizzas in Session.SpecialtyPizzasList)
                    {
                        Button btnDynamic = new Button();
                        btnDynamic.Location = new System.Drawing.Point(x, y);
                        btnDynamic.Size = new System.Drawing.Size(Constants.MenuCardButtonWidthG, Constants.ButtonHeightG);

                        btnDynamic.Name = _catalogSpecialtyPizzas.Menu_Code;
                        btnDynamic.Tag = "SpecialtyPizzas";
                        btnDynamic.Text = _catalogSpecialtyPizzas.Order_Description;
                        btnDynamic.Font = new Font(new FontFamily("Microsoft Sans Serif"), 8);
                        btnDynamic.BackColor = SystemColors.Control;
                        if (_catalogSpecialtyPizzas.MenuItemType == false)
                        {
                            btnDynamic.FlatStyle = FlatStyle.Flat;
                            btnDynamic.FlatAppearance.BorderColor = Session.vegColor;
                            btnDynamic.FlatAppearance.BorderSize = 3;
                        }
                        else if (_catalogSpecialtyPizzas.MenuItemType == true)
                        {
                            btnDynamic.FlatStyle = FlatStyle.Flat;
                            btnDynamic.FlatAppearance.BorderColor = Session.nonVegColor;
                            btnDynamic.FlatAppearance.BorderSize = 3;
                        }

                        btnDynamic.Margin = new Padding(0);

                        if (_catalogSpecialtyPizzas.Menu_Item_Image != null)
                        {
                            byte[] binaryData = Convert.FromBase64String(_catalogSpecialtyPizzas.Menu_Item_Image);
                            btnDynamic.Image = Image.FromStream(new MemoryStream(binaryData));
                            btnDynamic.TextAlign = ContentAlignment.BottomCenter;
                            btnDynamic.ImageAlign = ContentAlignment.TopCenter;
                        }

                        flowLayoutPanelSpecialtyPizzas.Controls.Add(btnDynamic);
                        x += Constants.MenuCardButtonWidthG;
                        btnDynamic.Click += SpecialtyItemsClick;

                        if (Session.VegOnlySelected)
                        {
                            if (btnDynamic.FlatAppearance.BorderColor == Session.nonVegColor)
                            {
                                btnDynamic.Visible = false;
                            }
                        }
                        else
                        {
                            if (btnDynamic.FlatAppearance.BorderColor == Session.nonVegColor)
                            {
                                btnDynamic.Visible = true;
                            }
                        }
                    }

                    if (x > (Constants.HorizontalSpace + Constants.MenuCardButtonWidthG))
                    {
                        flowLayoutPanelSpecialtyPizzas.Visible = true;
                        flowLayoutPanelSpecialtyPizzas.BringToFront();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Trace("ERROR", "frmOrder-btnSpecPizza_Click(): " + ex.Message, ex, true);
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session.currentToppingCollection != null)
                {
                    if (Session.currentToppingCollection.currentPage < Session.currentToppingCollection.TotalPages)
                    {
                        Session.currentToppingCollection.currentPage++;
                        PopulateToppingPage();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Trace("ERROR", "frmOrder-btnNext_Click(): " + ex.Message, ex, true);
            }
        }

        private void btnpPrev_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session.currentToppingCollection != null)
                {
                    if (Session.currentToppingCollection.currentPage > 1)
                    {
                        Session.currentToppingCollection.currentPage--;
                        PopulateToppingPage();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Trace("ERROR", "frmOrder-btnpPrev_click(): " + ex.Message, ex, true);
            }
        }

        public void PopulateToppings(string btnName, string SpecialtyPizzaCode = "")
        {
            List<CatalogOptionGroups> catalogOptionGroups = APILayer.GetOptionGroups(btnName);

            if (catalogOptionGroups == null || catalogOptionGroups.Count <= 0) return;

            CatalogOptionGroups currentCatalogOptionGroups = catalogOptionGroups[0]; //TO DO only one Option Group considered

            if (currentCatalogOptionGroups.Topping_Group)
            {
                int x = Constants.HorizontalSpace, y = Constants.VerticalSpace;
                Session.currentToppingCollection = new UserTypes.ToppingCollection();
                Session.currentToppingCollection.currentCatalogOptionGroups = currentCatalogOptionGroups;
                Session.currentToppingCollection.MaxButtonsPerPage = UserFunctions.GetMaximumButtonBestFit(flowLayoutPanelToppings.Width, flowLayoutPanelToppings.Height, Constants.ButtonWidthG, Constants.ButtonHeightG, x, y);
                Session.currentToppingCollection.currentToppings = CartFunctions.GetToppingsFromCatalogToppings(APILayer.GetToppings(btnName, currentCatalogOptionGroups.Menu_Option_Group_Code), selectedLineNumber, Session.cart, (Session.selectedComboMenuItem != null ? Session.selectedComboMenuItem.Menu_Code : "")
                                                                                                                , (Session.selectedComboMenuItemSizes != null ? Session.selectedComboMenuItemSizes.Size_Code : ""), (Session.selectedMenuItem != null ? Session.selectedMenuItem.Menu_Code : ""), (Session.selectedMenuItemSizes != null ? Session.selectedMenuItemSizes.Size_Code : ""));
                Session.currentToppingCollection.pizzaToppings = CartFunctions.GetPizzaToppings(Session.currentToppingCollection.currentToppings, selectedLineNumber, SpecialtyPizzaCode);
                Session.currentToppingCollection.TotalPages = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(Session.currentToppingCollection.currentToppings.Count) / Convert.ToDouble(Session.currentToppingCollection.MaxButtonsPerPage)));
                Session.currentToppingCollection.currentPage = 1;

                if (CartClicked) FillMenuTypesFromHalf();

                uC_Customer_order_Header1.lblMinValue.Text = Convert.ToString(Session.currentToppingCollection.currentCatalogOptionGroups.Min_To_Choose);
                uC_Customer_order_Header1.lblMaxValue.Text = Convert.ToString(Session.currentToppingCollection.currentCatalogOptionGroups.Max_To_Choose);

                PopulateToppingPage();

                string ButtonText = APILayer.GetCatalogText(LanguageConstant.cintWhole);
                ToppingsItemPartsButtonClick(panelItemParts.Controls.OfType<Button>().FirstOrDefault(z => z.Text == ButtonText), new EventArgs());



                Session.cartToppings = new List<Topping>();
                if (flowLayoutPanelToppings.Controls.Count > 0)
                {
                    foreach (Control control in flowLayoutPanelToppings.Controls)
                    {
                        if (((Button)control).BackColor != Session.ToppingColor)
                        {
                            Topping topping = Session.currentToppingCollection.currentToppings.Find(z => z.Menu_Code == Convert.ToString(((Button)control).Tag));

                            if (!Session.cartToppings.Contains(topping))
                                Session.cartToppings.Add(topping);
                        }
                    }
                }


            }
        }



        private void ToppingsOkButtonClick(object sender, EventArgs e)
        {
            try
            {
                CloseToppingView();                
            }
            catch (Exception ex)
            {
                Logger.Trace("ERROR", "frmOrder-ToppingsOkbuttonclick(): " + ex.Message, ex, true);
            }
        }

        private void ToppingSizesButtonClick(object sender, EventArgs e)
        {
            try
            {
                Button btn = (Button)sender;

                if (Convert.ToString(btn.Tag) == "Spec") return;

                if (btn.Text == APILayer.GetCatalogText(LanguageConstant.cintLight))
                    Session.currentToppingSize = UserTypes.ToppingSizes.Light;
                else if (btn.Text == APILayer.GetCatalogText(LanguageConstant.cintSingle))
                    Session.currentToppingSize = UserTypes.ToppingSizes.Single;
                else if (btn.Text == APILayer.GetCatalogText(LanguageConstant.cintExtra))
                    Session.currentToppingSize = UserTypes.ToppingSizes.Extra;
                else if (btn.Text == APILayer.GetCatalogText(LanguageConstant.cintDouble))
                    Session.currentToppingSize = UserTypes.ToppingSizes.Double;
                else if (btn.Text == APILayer.GetCatalogText(LanguageConstant.cintTriple))
                    Session.currentToppingSize = UserTypes.ToppingSizes.Triple;

                Session.currentToppingSizeCode = Convert.ToString(btn.Tag);
                Color LocalColor = UserFunctions.GetColorbyAmountCode(Session.currentToppingSizeCode);
                Session.currentToppingColor = LocalColor == Color.Gray ? Session.ToppingColor : LocalColor;

                foreach (Control control in flowLayoutPanelToppingSizes.Controls)
                {
                    if (control.Name != "btnOK")
                    {
                        ((Button)control).Image = null;
                        //if (((Button)control).Image != null) ((Button)control).Image.Dispose();                        
                        //((Button)control).BackgroundImage = null;
                        ((Button)control).TextAlign = ContentAlignment.MiddleCenter;
                    }
                }

                if (((Button)sender).Name != "btnOK")
                {
                    ((Button)sender).ImageAlign = ContentAlignment.TopCenter;
                    ((Button)sender).Image = Properties.Resources.fingerprint_1;
                    //((Button)sender).BackgroundImage = Properties.Resources.fingerprint_1;//((System.Drawing.Image)(resources.GetObject("fingerprint_1")));
                    ((Button)sender).TextAlign = ContentAlignment.BottomCenter;
                }
            }
            catch (Exception ex)
            {
                Logger.Trace("ERROR", "frmOrder-toppingsizesbuttonclick(): " + ex.Message, ex, true);
            }
        }

        private void ToppingsButtonClick(object sender, EventArgs e)
        {
            try
            {
                ToppingsButtonClick((Button)sender);
            }
            catch (Exception ex)
            {
                Logger.Trace("ERROR", "frmOrder-toppingsbuttonclick(): " + ex.Message, ex, true);
            }
        }

        private void ToppingsItemPartsButtonClick(object sender, EventArgs e)
        {
            try
            {
                Button btn = (Button)sender;

                if (btn.Text == APILayer.GetCatalogText(LanguageConstant.cintWhole))
                    Session.currentItemPart = UserTypes.ItemParts.Whole;
                else if (btn.Text == APILayer.GetCatalogText(LanguageConstant.cint1stHalf))
                    Session.currentItemPart = UserTypes.ItemParts.FirstHalf;
                else if (btn.Text == APILayer.GetCatalogText(LanguageConstant.cint2ndHalf))
                    Session.currentItemPart = UserTypes.ItemParts.SecondHalf;

                foreach (Control control in panelItemParts.Controls)
                    ((Button)control).BackColor = SystemColors.Control;

                btn.BackColor = Color.LightCoral;

                ItemPartChanged();
            }
            catch (Exception ex)
            {
                Logger.Trace("ERROR", "frmOrder-toppingsItemPartsbuttonclick(): " + ex.Message, ex, true);
            }
        }

        private void PopulateToppingPage()
        {
            flowLayoutPanelToppings.Controls.Clear();

            if (Session.currentToppingCollection != null)
            {
                int x = Constants.HorizontalSpace;
                int startIndex = Session.currentToppingCollection.MaxButtonsPerPage * (Session.currentToppingCollection.currentPage - 1);
                int LastIndex = startIndex + (Session.currentToppingCollection.MaxButtonsPerPage - 1);

                if (LastIndex > Session.currentToppingCollection.currentToppings.Count - 1) LastIndex = Session.currentToppingCollection.currentToppings.Count - 1;

                for (int i = startIndex; i <= LastIndex; i++)
                {
                    Button btn = new Button();
                    btn.Location = new System.Drawing.Point(x, Constants.VerticalSpace);
                    btn.Size = new System.Drawing.Size(Constants.ButtonWidthG, Constants.ButtonHeightG);
                    btn.Name = "btn" + Session.currentToppingCollection.currentToppings[i].Topping_Code.Trim();
                    btn.Tag = Session.currentToppingCollection.currentToppings[i].Menu_Code.Trim();
                    btn.Text = Session.currentToppingCollection.currentToppings[i].Order_Description.Trim();
                    btn.Font = new Font(new FontFamily("Microsoft Sans Serif"), 8);
                    btn.TextAlign = ContentAlignment.MiddleCenter;
                    //btn.Image = _catalogToppings.Menu_Item_Image;
                    btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;

                    btn.BackColor = SystemColors.Control;
                    if (Session.currentToppingCollection.currentToppings[i].MenuItemType == false)
                    {
                        btn.FlatStyle = FlatStyle.Flat;
                        btn.FlatAppearance.BorderColor = Session.vegColor;
                        btn.FlatAppearance.BorderSize = 3;
                    }
                    else if (Session.currentToppingCollection.currentToppings[i].MenuItemType == true)
                    {
                        btn.FlatStyle = FlatStyle.Flat;
                        btn.FlatAppearance.BorderColor = Session.nonVegColor;
                        btn.FlatAppearance.BorderSize = 3;
                    }
                    else if (Session.currentToppingCollection.currentToppings[i].MenuItemType == null)
                    {
                        btn.BackColor = SystemColors.Control;
                    }

                    btn.Margin = new Padding(0);

                    if (Session.currentToppingCollection.currentToppings[i].Menu_Item_Image != null)
                    {
                        byte[] binaryData = Convert.FromBase64String(Session.currentToppingCollection.currentToppings[i].Menu_Item_Image);
                        btn.Image = Image.FromStream(new MemoryStream(binaryData));
                        btn.TextAlign = ContentAlignment.BottomCenter;
                        btn.ImageAlign = ContentAlignment.TopCenter;
                    }

                    if (Session.currentToppingCollection.currentToppings[i].Default)
                    {
                        Color LocalColor = UserFunctions.GetColorbyAmountCode(Convert.ToString(Session.currentToppingCollection.currentToppings[i].Amount_Code));
                        btn.BackColor = LocalColor == Color.Gray ? Session.ToppingColor : LocalColor;
                        uC_Customer_order_Header1.lblSelectedValue.Text = Convert.ToString(Convert.ToInt32(uC_Customer_order_Header1.lblSelectedValue.Text) + 1);
                    }

                    if (Session.currentToppingCollection.pizzaToppings != null && Session.currentToppingCollection.pizzaToppings.Count > 0)
                    {
                        PizzaTopping pizzaTopping = Session.currentToppingCollection.pizzaToppings.Find(z => z.ButtonIndex == i && z.DefaultTopping == "");
                        if (pizzaTopping != null)
                        {
                            btn.BackColor = pizzaTopping.ButtonColor == Color.Gray ? Session.ToppingColor : pizzaTopping.ButtonColor;
                            if (pizzaTopping.ButtonColor == Color.Gray)
                            {
                                btn.BackgroundImage = Properties.Resources._97;
                                btn.TextAlign = ContentAlignment.BottomCenter;
                            }

                        }
                    }

                    if (Session.VegOnlySelected)
                    {
                        if (btn.FlatAppearance.BorderColor == Session.nonVegColor)
                        {
                            btn.Visible = false;
                        }
                    }
                    else
                    {
                        if (btn.FlatAppearance.BorderColor == Session.nonVegColor)
                        {
                            btn.Visible = true;
                        }
                    }

                    flowLayoutPanelToppings.Controls.Add(btn);
                    btn.Click += new EventHandler(this.ToppingsButtonClick);

                    x += Constants.ButtonWidthG;
                }

                Session.currentToppingSize = UserTypes.ToppingSizes.Single;

                if (Session.currentToppingCollection.TotalPages > 1)
                {
                    if (btnPrev != null) btnPrev.Visible = true;
                    if (btnNext != null) btnNext.Visible = true;
                }
                else
                {
                    if (btnPrev != null) btnPrev.Visible = false;
                    if (btnNext != null) btnNext.Visible = false;
                }
            }
        }

        private void AdjustToppingButtonsCollection()
        {
            if (Session.currentToppingCollection != null)
            {
                Session.currentToppingCollection.MaxButtonsPerPage = UserFunctions.GetMaximumButtonBestFit(flowLayoutPanelToppings.Width, flowLayoutPanelToppings.Height, Constants.MenuCardButtonWidthG, Constants.ButtonHeightG, Constants.HorizontalSpace, Constants.VerticalSpace);
                Session.currentToppingCollection.TotalPages = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(Session.currentToppingCollection.currentToppings.Count) / Convert.ToDouble(Session.currentToppingCollection.MaxButtonsPerPage == 0 ? 1 : Session.currentToppingCollection.MaxButtonsPerPage)));
            }
        }

        private void Catalog_Resize(object sender, EventArgs e)
        {
            try
            {
                AdjustToppingButtonsCollection();
                if (flowLayoutPanelToppings.Visible) PopulateToppingPage();
            }
            catch (Exception ex)
            {
                Logger.Trace("ERROR", "frmOrder-catalog_resize(): " + ex.Message, ex, true);
            }
        }

        private void CloseToppingView()
        {
            if (Session.cartToppings != null && Session.cartToppings.Count > 0)
            {
                bool NonVegToppingExist = Session.cartToppings.Exists(x => x.MenuItemType == true);

                if (NonVegToppingExist)
                {
                    try
                    {
                        string description = Session.cart.cartItems.Find(x => x.Menu_Code == Session.selectedMenuItem.Menu_Code && x.Description.Contains(Session.selectedMenuItemSizes.Description)).Description;

                        UpdateCartItemBasedOnToppingRequest request = new UpdateCartItemBasedOnToppingRequest();
                        request.CartId = Session.cart.cartHeader.CartId;
                        request.Menu_Code = Session.selectedMenuItem.Menu_Code;
                        request.Description = description;
                        request.MenuItemType = NonVegToppingExist;
                        request.LineNumber = Session.SelectedLineNumber;
                        APILayer.UpdateCartItemBasedOnTopping(request);

                        CartFunctions.GetCart("MenuCategories", "", ref selectedLineNumber);
                    }
                    catch (Exception ex)
                    {
                        Logger.Trace("ERROR", "frmOrder-closetoppingview(): " + ex.Message, ex, true);
                    }

                }
            }

            CartItem cartItem = Session.cart.cartItems.Find(x => x.Line_Number == (selectedLineNumber < 0 ? Session.cart.cartItems[Session.cart.cartItems.Count - 1].Line_Number : selectedLineNumber));

            if (cartItem == null) return;

            pintCurrentOption_Group = 1;

            foreach (ItemOptionGroup itemOptionGroup in cartItem.itemOptionGroups)
            {
                if (Session.currentToppingCollection.currentCatalogOptionGroups.Menu_Option_Group_Code == itemOptionGroup.Option_Group_Code)
                {
                    if (Session.ProcessingCombo)
                    {
                        if (cartItem.Combo_Prompt_Options)
                        {
                            if (Convert.ToInt32(uC_Customer_order_Header1.lblSelectedValue.Text) >= Convert.ToInt32(uC_Customer_order_Header1.lblMinValue.Text))
                                itemOptionGroup.Option_Group_Complete = true;
                            else
                                itemOptionGroup.Option_Group_Complete = false;
                        }
                        else
                        {
                            itemOptionGroup.Option_Group_Complete = true;
                        }
                    }
                    else
                    {
                        if (Convert.ToInt32(uC_Customer_order_Header1.lblSelectedValue.Text) >= Convert.ToInt32(uC_Customer_order_Header1.lblMinValue.Text))
                            itemOptionGroup.Option_Group_Complete = true;
                        else
                            itemOptionGroup.Option_Group_Complete = false;
                    }
                }
                break;
            }

            CurrentLineType = "";
            CartFunctions.ToppingSelectionCompleted(cartItem);
            RefreshCartUI();
            CartControl();

            tlpToppings.Visible = false;
            tlpToppings.SendToBack();
            flowLayoutPanelSpecialtyPizzas.Visible = false;
            flowLayoutPanelSpecialtyPizzas.SendToBack();
            pnl_Quantity.Visible = false;
            pnl_Quantity.SendToBack();

            Session.currentToppingCollection = null;
            uC_Customer_order_Header1.pnl_MinMax.Visible = false;
            uC_Customer_order_Header1.lblSelectedValue.Text = "0";
            Session.cartToppings = new List<Topping>();

            if (Session.ProcessingCombo)
            {
                HandleComboOptions();
            }
            else
            {
                Button btnDynamic1 = MenuCategoryToClick(true);
                DynamicButtonClick(btnDynamic1, new EventArgs());
            }
        }

        private void frmOrder_Load(object sender, EventArgs e)
        {
            try
            {
                InitializeToppingControl();
                InitializeSpecialtyPizzaControl();
                InitializeCart();
                MenuTypesLoad();
                DyanmicBusinessUnit();
                uC_Customer_OrderMenu.SetButtonText("Customer");
                uC_Customer_OrderMenu.ucFunctionList = ucFunctionList;
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

                if ((SystemSettings.GetSettingValue("VegOnly", Session._LocationCode) == "1"))
                {
                    checkBoxVegOnly.Visible = true;

                }
                else
                {
                    checkBoxVegOnly.Visible = false;
                }
                uC_Customer_OrderMenu.ucInformationList = ucInformationList;
                uC_CustomerOrderBottomMenu1.Formname = this.Name;
                UserFunctions.AutoSelectOrderType(uC_CustomerOrderBottomMenu1);
                CartFunctions.FillCustomerToCart(ref Session.cart);
                //UserFunctions.GetEstimatedTimes();
                if ((SystemSettings.GetSettingValue("CartOnHold", Session._LocationCode) == "1"))
                {
                    btnOnHold.Visible = true;
                }

                if (Session.pblnModifyingOrder)
                {
                    OrderFunctions.LoadOrderDisplayOrderScreen(uC_Customer_OrderMenu, false, true, 0, 0);
                    btn_Minus.Enabled = Session.OrderReqField.btn_Minus;
                    btn_Plus.Enabled = Session.OrderReqField.btn_Plus;
                    btn_Instructions.Enabled = Session.OrderReqField.btn_Instructions;
                    btn_Up.Enabled = Session.OrderReqField.btn_Up;
                    btn_Down.Enabled = Session.OrderReqField.btn_Down;
                    btn_Coupons.Enabled = Session.OrderReqField.btn_Coupons;
                    btn_Quantity.Enabled = Session.OrderReqField.btn_Quantity;

                    if (Session.cart.cartHeader.Total > 0 && !string.IsNullOrEmpty(Session.cart.Customer.Phone_Number))
                    {
                        uC_Customer_OrderMenu.cmdRemake.Enabled = Session.handleRemakebutton;
                        uC_Customer_OrderMenu.cmdHistory.Enabled = Session.handleHistorybutton;
                    }
                }

                uC_Customer_OrderMenu.HandleHistoryButton(Session.handleHistorybutton);
                uC_Customer_OrderMenu.HandleModifyButton(Session.handleModify);
                if (Session.cart != null)
                {
                    RefreshCartUI();
                }


                //mstrUpsellReminder = pobjPOSGeneral.Upsell_Reminder(Format$(pudtSystem_Settings.pdtmSystem_Date, "mm/dd/yyyy"), pudtSystem_Settings.pstrDefault_Location_Code)
                //mstrUpsellReminder = OrderFunctions.UpsellReminder();
                rtfUpsellReminder.Text = Session.UpsellReminder;


                this.Location = new Point(((Screen.PrimaryScreen.Bounds.Width - this.Size.Width) / 2) + 5, ((Screen.PrimaryScreen.Bounds.Height - this.Size.Height) / 2));
            }
            catch (Exception ex)
            {
                Logger.Trace("ERROR", "frmOrder-frmOrder_Load(): " + ex.Message, ex, true);
            }
        }

        private void btn_Plus_Click(object sender, EventArgs e)
        {
            try
            {
                Session.PreventItemwiseUpsell = true;
                QuantityChange(false, 1);
                Session.PreventItemwiseUpsell = false;
            }
            catch (Exception ex)
            {
                Logger.Trace("ERROR", "frmOrder-btn_plus_click(): " + ex.Message, ex, true);
            }
        }

        private void btn_Minus_Click(object sender, EventArgs e)
        {
            try
            {
                Session.PreventItemwiseUpsell = true;
                QuantityChange(false, -1);
                Session.PreventItemwiseUpsell = false;
            }
            catch (Exception ex)
            {
                Logger.Trace("ERROR", "frmOrder-btn_minus_click(): " + ex.Message, ex, true);
            }
        }

        private void btn_Quantity_Click(object sender, EventArgs e)
        {
            try
            {
                if (selectedLineNumber > -1)
                {
                    CartItem cartItem = Session.cart.cartItems.Find(x => x.Line_Number == selectedLineNumber);

                    lblWhere.Text = cartItem.Menu_Description + " - " + APILayer.GetCatalogText(LanguageConstant.cintQuantity);

                    if (Session.ProcessingCombo && Session.CurrentComboGroup > 0)
                    {
                        ItemCombo itemCombo = Session.cart.itemCombos.Find(x => x.Combo_Group == Session.CurrentComboGroup);
                        if (itemCombo == null)
                            return;
                        txt_Quantity.Text = Convert.ToString(itemCombo.Combo_Quantity);
                    }
                    else
                    {
                        txt_Quantity.Text = Convert.ToString(cartItem.Quantity);
                    }
                    uC_KeyBoardNumeric.txtUserID = txt_Quantity;
                    uC_KeyBoardNumeric.ChangeButtonColor(DefaultBackColor);
                    pnl_Quantity.Location = new Point(0, 0);
                    pnl_Quantity.Visible = true;
                    pnl_Quantity.BringToFront();
                    txt_Quantity.SelectAll();
                }
            }
            catch (Exception ex)
            {
                Logger.Trace("ERROR", "frmOrder-btn_quantity_click(): " + ex.Message, ex, true);
            }
        }


        private void btn_Instructions_Click(object sender, EventArgs e)
        {
            try
            {
                if (selectedLineNumber > 0)
                {
                    using (frmReason objfrmReason = new frmReason(Convert.ToInt32(enumReasonGroupID.CookingInstruction)))
                    {
                        objfrmReason.SelectedLineNumber = selectedLineNumber;
                        objfrmReason.HighlightItemReason(selectedLineNumber);
                        objfrmReason.ShowDialog();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Trace("ERROR", "frmOrder-btn_instructions_click(): " + ex.Message, ex, true);
            }
        }

        private void btnQty_OK_Click(object sender, EventArgs e)
        {
            try
            {
                Session.PreventItemwiseUpsell = true;
                if (Convert.ToString(txt_Quantity.Text) != "") QuantityChange(true, Convert.ToInt32(Convert.ToString(txt_Quantity.Text)));
                pnl_Quantity.Visible = false;
                pnl_Quantity.SendToBack();
                Session.PreventItemwiseUpsell = false;
            }
            catch (Exception ex)
            {
                Logger.Trace("ERROR", "frmOrder-btnQty_OK_Click(): " + ex.Message, ex, true);
            }
        }

        public void InitializeCart()
        {
            Session.CatalogCartCaptions = APILayer.GetCartCaptions();

            if (dgvCart.Columns.Count > 0)
            {
                dgvCart.Columns["Qty"].HeaderText = Session.CatalogCartCaptions.Qty;
                dgvCart.Columns["Item"].HeaderText = Session.CatalogCartCaptions.Item;
                dgvCart.Columns["Price"].HeaderText = Session.CatalogCartCaptions.Price;
                dgvCart.Columns["VegNVegColor"].HeaderText = string.Empty;
            }

            DataTable dtCartTotals = new DataTable();
            dtCartTotals.Columns.Add("SNo");
            dtCartTotals.Columns.Add("Name");
            dtCartTotals.Columns.Add("Value");

            DataRow dr = dtCartTotals.NewRow();
            dr["Name"] = Session.CatalogCartCaptions.SubTotal;
            dr["Value"] = "0.00";
            dtCartTotals.Rows.Add(dr);

            dr = dtCartTotals.NewRow();
            dr["Name"] = Session.CatalogCartCaptions.BottleDeposit;
            dr["Value"] = "0.00";
            dtCartTotals.Rows.Add(dr);

            dr = dtCartTotals.NewRow();
            dr["Name"] = Session.CatalogCartCaptions.Credit;
            dr["Value"] = "0.00";
            dtCartTotals.Rows.Add(dr);

            if (SystemSettings.settings.pbytTaxStructure < 4)
            {
                dr = dtCartTotals.NewRow();
                dr["Name"] = Session.CatalogCartCaptions.TaxLessthen4;
                dr["Value"] = "0.00";
                dtCartTotals.Rows.Add(dr);

                dr = dtCartTotals.NewRow();
                dr["Name"] = Session.CatalogCartCaptions.Tax2Lessthen4;
                dr["Value"] = "0.00";
                dtCartTotals.Rows.Add(dr);
            }
            else
            {
                //if (SystemSettings.settings.pblnUseUserDefinedTax1)
                //{
                dr = dtCartTotals.NewRow();
                dr["Name"] = Session.CatalogCartCaptions.Tax1;
                dr["Value"] = "0.00";
                dtCartTotals.Rows.Add(dr);
                //}

                //if (SystemSettings.settings.pblnUseUserDefinedTax2)
                //{
                dr = dtCartTotals.NewRow();
                dr["Name"] = Session.CatalogCartCaptions.Tax2;
                dr["Value"] = "0.00";
                dtCartTotals.Rows.Add(dr);
                //}

                //if (SystemSettings.settings.pblnUseUserDefinedTax3)
                //{
                dr = dtCartTotals.NewRow();
                dr["Name"] = Session.CatalogCartCaptions.Tax3;
                dr["Value"] = "0.00";
                dtCartTotals.Rows.Add(dr);
                //}


                //if (SystemSettings.settings.pblnUseUserDefinedTax4)
                //{
                dr = dtCartTotals.NewRow();
                if (Session.ODC_Tax)
                {
                    dr["Name"] = SystemSettings.GetSettingValue("ODC_Change_Tax_Description", Session._LocationCode);
                }
                else
                {
                    dr["Name"] = Session.CatalogCartCaptions.Tax4;
                }
                dr["Value"] = "0.00";
                dtCartTotals.Rows.Add(dr);
                //}
            }



            dr = dtCartTotals.NewRow();
            dr["Name"] = Session.CatalogCartCaptions.Coupon;
            dr["Value"] = "0.00";
            dtCartTotals.Rows.Add(dr);

            dr = dtCartTotals.NewRow();
            dr["Name"] = "AggregatorGST";
            dr["Value"] = "0.00";
            dtCartTotals.Rows.Add(dr);

            dr = dtCartTotals.NewRow();
            dr["Name"] = Session.CatalogCartCaptions.Total;
            dr["Value"] = "0.00";
            dtCartTotals.Rows.Add(dr);



            dgvCartTotals.DataSource = dtCartTotals;

            dgvCartTotals.Columns["SNo"].Width = 10;
            dgvCartTotals.Columns["Value"].Width = 80;
            dgvCartTotals.Columns["Name"].Width = dgvCartTotals.Width - 95;

            dgvCartTotals.Columns["SNo"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvCartTotals.Columns["Name"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvCartTotals.Columns["Value"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            dgvCartTotals.Columns["SNo"].SortMode = DataGridViewColumnSortMode.NotSortable;
            dgvCartTotals.Columns["Name"].SortMode = DataGridViewColumnSortMode.NotSortable;
            dgvCartTotals.Columns["Value"].SortMode = DataGridViewColumnSortMode.NotSortable;

            dgvCartTotals.Rows[0].DefaultCellStyle.Font = new Font("Microsoft Sans Serif", 10, FontStyle.Bold);
            dgvCartTotals.Rows[dgvCartTotals.Rows.Count - 1].Height = 25;
            dgvCartTotals.Rows[dgvCartTotals.Rows.Count - 1].DefaultCellStyle.Font = new Font("Microsoft Sans Serif", 10, FontStyle.Bold);
            dgvCartTotals.Rows[dgvCartTotals.Rows.Count - 1].DefaultCellStyle.BackColor = Color.Teal;
            dgvCartTotals.Rows[dgvCartTotals.Rows.Count - 1].DefaultCellStyle.ForeColor = Color.White;

            dgvCartTotals.ClearSelection();
            dgvCartTotals.Size = new Size(274, 28);
            dgvCartTotals.FirstDisplayedScrollingRowIndex = dgvCartTotals.Rows.Count - 1;

        }

        private void btn_Details_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvCartTotals.Size.Height == 28)
                {
                    if(dgvCartTotals.RowCount == 10)
                    {
                        if(dgvCartTotals.Rows[8].Visible)
                            dgvCartTotals.Size = new Size(274, 208);
                        else
                            dgvCartTotals.Size = new Size(274, 188);
                    }
                    else
                    {
                        if (dgvCartTotals.Rows[6].Visible)
                            dgvCartTotals.Size = new Size(274, 208);
                        else
                            dgvCartTotals.Size = new Size(274, 188);
                    }
                }                   
                else
                    dgvCartTotals.Size = new Size(274, 28);
                dgvCartTotals.FirstDisplayedScrollingRowIndex = dgvCartTotals.Rows.Count - 1;
            }
            catch (Exception ex)
            {
                Logger.Trace("ERROR", ex.Message, ex, true);
            }

        }

        private void FormatCartGrid()
        {
            dgvCart.Columns["Qty"].Width = 30;
            dgvCart.Columns["Price"].Width = 60;
            dgvCart.Columns["VegNVegColor"].Width = 20;

            dgvCart.Columns["Item"].Width = dgvCart.Width - (dgvCart.Columns["Qty"].Width + dgvCart.Columns["Price"].Width + dgvCart.Columns["VegNVegColor"].Width + 5); //160;

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
                    btn_Down.Enabled = true;
                    btn_Up.Enabled = true;
                }
                else
                {
                    btn_Down.Enabled = false;
                    btn_Up.Enabled = false;
                }
            }

            //btn_Coupons.Enabled = false;
            //btn_Quantity.Enabled = false;
            //btn_Instructions.Enabled = false;
        }

        private void ClearCartTotals()
        {
            DataTable dtCartTotals = (DataTable)dgvCartTotals.DataSource;
            dtCartTotals.Rows[0]["Value"] = "0.00";
            dtCartTotals.Rows[1]["Value"] = "0.00";
            dtCartTotals.Rows[2]["Value"] = "0.00";
            dtCartTotals.Rows[3]["Value"] = "0.00";
            dtCartTotals.Rows[4]["Value"] = "0.00";
            if (dtCartTotals.Rows.Count == 9)
            {
                dtCartTotals.Rows[5]["Value"] = "0.00";
                dtCartTotals.Rows[6]["Value"] = "0.00";
                dtCartTotals.Rows[7]["Value"] = "0.00";
                dtCartTotals.Rows[8]["Value"] = "0.00";
            }
            else
            {
                dtCartTotals.Rows[5]["Value"] = "0.00";
                dtCartTotals.Rows[6]["Value"] = "0.00";
            }
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

            if (!selected)
            {
                dgvCart.ClearSelection();
            }
        }

        private void btn_Up_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvCart.FirstDisplayedScrollingRowIndex > 0) dgvCart.FirstDisplayedScrollingRowIndex = dgvCart.FirstDisplayedScrollingRowIndex - 1;
            }
            catch (Exception ex)
            {
                Logger.Trace("ERROR", ex.Message, ex, true);
            }
        }

        private void btn_Down_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvCart.FirstDisplayedScrollingRowIndex <= dgvCart.RowCount - 1) dgvCart.FirstDisplayedScrollingRowIndex = dgvCart.FirstDisplayedScrollingRowIndex + 1;
            }
            catch (Exception ex)
            {
                Logger.Trace("ERROR", ex.Message, ex, true);
            }
        }

        public void GenerateTicketCollection()
        {
            CartFunctions.GenerateTicketCollection((DataTable)dgvCart.DataSource);
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

                uC_Customer_OrderMenu.BackColor = color;
                flowLayoutPanelMenuItems.BackColor = color;
                flowLayoutPanelToppingSizes.BackColor = color;
                uC_CustomerOrderBottomMenu1.BackColor = color;
                uC_KeyBoardNumeric.BackColor = color;
                pnl_Quantity.BackColor = color;
                tlpMain.BackColor = color;
                uC_Customer_OrderMenu.DisableModifyInTrainingMode();
            }
            catch (Exception ex)
            {
                Logger.Trace("ERROR", ex.Message, ex, true);
            }
        }

        public void CheckcartItem()
        {
            List<CartItem> lstCartItem = Session.cart.cartItems;
            if (lstCartItem.Count > 0 && Session.cart != null)
            {
                //uC_CustomerOrderBottomMenu1.cmdPrintOnDemand.Enabled = true;
                uC_CustomerOrderBottomMenu1.cmdComplete.Enabled = true;
                uC_CustomerOrderBottomMenu1.cmdPay.Enabled = true;
            }
            else
            {
                uC_CustomerOrderBottomMenu1.cmdPrintOnDemand.Enabled = false;
                uC_CustomerOrderBottomMenu1.cmdComplete.Enabled = false;
                uC_CustomerOrderBottomMenu1.cmdPay.Enabled = false;
            }
        }

        private void frmOrder_Click(object sender, EventArgs e)
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

        private void frmOrder_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                ALT_F4 = (e.KeyCode.Equals(Keys.F4) && e.Alt == true);

                if (e.Modifiers == Keys.Control && e.KeyCode == Keys.H)
                {
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

        private void frmOrder_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
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

        private void ToppingsButtonClick(Button btnTopping)
        {
            try
            {

                int index = Session.currentToppingCollection.currentToppings.FindIndex(x => x.Menu_Code == Convert.ToString(btnTopping.Tag));

                bool blnSelected = false;
                bool blnDefaultTopping;
                string strAmountCode;
                string strDefaultAmount;
                Color lngDefaultColor = SystemColors.Control;
                int mintChosen = 0;

                //TO DO
                //'-->VIS0832 - CDT
                //If MaxItemsChosen(Index) Then
                //    Exit Sub
                //End If
                //'-->END VIS0832

                strAmountCode = Session.currentToppingSizeCode;

                switch (Session.currentItemPart)
                {
                    case UserTypes.ItemParts.FirstHalf:
                        blnDefaultTopping = Session.currentToppingCollection.currentToppings[index].FirstHalfDefault;
                        strDefaultAmount = Session.currentToppingCollection.currentToppings[index].FirstHalfDefaultAmount;
                        break;
                    case UserTypes.ItemParts.SecondHalf:
                        blnDefaultTopping = Session.currentToppingCollection.currentToppings[index].SecondHalfDefault;
                        strDefaultAmount = Session.currentToppingCollection.currentToppings[index].SecondHalfDefaultAmount;
                        break;
                    default:
                        blnDefaultTopping = Session.currentToppingCollection.currentToppings[index].WholeDefault;
                        strDefaultAmount = Session.currentToppingCollection.currentToppings[index].WholeDefaultAmount;
                        break;
                }


                if (blnDefaultTopping)
                {
                    Color LocalColor = UserFunctions.GetColorbyAmountCode(strDefaultAmount);
                    lngDefaultColor = LocalColor == Color.Gray ? Session.ToppingColor : LocalColor;

                    if (btnTopping.BackColor == lngDefaultColor && Session.currentToppingColor == Color.Black)
                    {
                        btnTopping.BackColor = Session.ToppingColor;
                        Session.currentToppingColor = Color.Gray; //Session.ToppingColor;

                        btnTopping.BackgroundImage = Properties.Resources._97;
                        btnTopping.TextAlign = ContentAlignment.BottomCenter;

                        blnSelected = true;

                        switch (strDefaultAmount)
                        {
                            case "~":
                                mintChosen = mintChosen - 1;
                                break;
                            case "":
                                mintChosen = mintChosen - 1;
                                break;
                            case "+":
                                mintChosen = mintChosen - 2;
                                break;
                            case "2":
                                mintChosen = mintChosen - 2;
                                break;
                            case "3":
                                mintChosen = mintChosen - 3;
                                break;
                        }

                        strAmountCode = "-";

                        if (Session.currentItemPart == UserTypes.ItemParts.Whole)
                            Session.currentToppingCollection.currentToppings[index].RemovedFromWhole = true;

                    }
                    else if (btnTopping.BackColor == lngDefaultColor && Session.currentToppingColor != Color.Black)
                    {
                        btnTopping.BackColor = Session.currentToppingColor == Color.Gray ? Session.ToppingColor : Session.currentToppingColor;

                        blnSelected = true;

                        switch (strDefaultAmount)
                        {
                            case "~":
                                mintChosen = mintChosen - 1;
                                break;
                            case "":
                                mintChosen = mintChosen - 1;
                                break;
                            case "+":
                                mintChosen = mintChosen - 2;
                                break;
                            case "2":
                                mintChosen = mintChosen - 2;
                                break;
                            case "3":
                                mintChosen = mintChosen - 3;
                                break;
                        }

                        if (Session.currentToppingColor == Session.ToppingSizeLightColor)
                            mintChosen = mintChosen + 1;
                        else if (Session.currentToppingColor == Session.ToppingSizeSingleColor)
                            mintChosen = mintChosen + 1;
                        else if (Session.currentToppingColor == Session.ToppingSizeExtraColor)
                            mintChosen = mintChosen + 2;
                        else if (Session.currentToppingColor == Session.ToppingSizeDoubleColor)
                            mintChosen = mintChosen + 2;
                        else if (Session.currentToppingColor == Session.ToppingSizeTripleColor)
                            mintChosen = mintChosen + 3;

                        if (Session.currentItemPart == UserTypes.ItemParts.Whole)
                            Session.currentToppingCollection.currentToppings[index].RemovedFromWhole = false;
                    }
                    else if (btnTopping.BackColor != lngDefaultColor && btnTopping.BackColor != Session.ToppingColor && Session.currentToppingColor == Color.Black)
                    {
                        blnSelected = false;

                        if (btnTopping.BackColor == Session.ToppingSizeLightColor)
                            mintChosen = mintChosen - 1;
                        else if (btnTopping.BackColor == Session.ToppingSizeSingleColor)
                            mintChosen = mintChosen - 1;
                        else if (btnTopping.BackColor == Session.ToppingSizeExtraColor)
                            mintChosen = mintChosen - 2;
                        else if (btnTopping.BackColor == Session.ToppingSizeDoubleColor)
                            mintChosen = mintChosen - 2;
                        else if (btnTopping.BackColor == Session.ToppingSizeTripleColor)
                            mintChosen = mintChosen - 3;


                        btnTopping.BackColor = lngDefaultColor;

                        Session.currentToppingColor = lngDefaultColor;

                        if (lngDefaultColor == Session.ToppingSizeLightColor)
                            mintChosen = mintChosen + 1;
                        else if (lngDefaultColor == Session.ToppingSizeSingleColor)
                            mintChosen = mintChosen + 1;
                        else if (lngDefaultColor == Session.ToppingSizeExtraColor)
                            mintChosen = mintChosen + 2;
                        else if (lngDefaultColor == Session.ToppingSizeDoubleColor)
                            mintChosen = mintChosen + 2;
                        else if (lngDefaultColor == Session.ToppingSizeTripleColor)
                            mintChosen = mintChosen + 3;
                    }
                    else if (btnTopping.BackColor != lngDefaultColor && btnTopping.BackColor != Session.ToppingColor && Session.currentToppingColor != Color.Black)
                    {
                        blnSelected = true;

                        if (btnTopping.BackColor == Session.ToppingSizeLightColor)
                            mintChosen = mintChosen - 1;
                        else if (btnTopping.BackColor == Session.ToppingSizeSingleColor)
                            mintChosen = mintChosen - 1;
                        else if (btnTopping.BackColor == Session.ToppingSizeExtraColor)
                            mintChosen = mintChosen - 2;
                        else if (btnTopping.BackColor == Session.ToppingSizeDoubleColor)
                            mintChosen = mintChosen - 2;
                        else if (btnTopping.BackColor == Session.ToppingSizeTripleColor)
                            mintChosen = mintChosen - 3;


                        btnTopping.BackColor = Session.currentToppingColor == Color.Gray ? Session.ToppingColor : Session.currentToppingColor;

                        if (Session.currentToppingColor == Session.ToppingSizeLightColor)
                            mintChosen = mintChosen + 1;
                        else if (Session.currentToppingColor == Session.ToppingSizeSingleColor)
                            mintChosen = mintChosen + 1;
                        else if (Session.currentToppingColor == Session.ToppingSizeExtraColor)
                            mintChosen = mintChosen + 2;
                        else if (Session.currentToppingColor == Session.ToppingSizeDoubleColor)
                            mintChosen = mintChosen + 2;
                        else if (Session.currentToppingColor == Session.ToppingSizeTripleColor)
                            mintChosen = mintChosen + 3;

                        if (Session.currentItemPart == UserTypes.ItemParts.Whole)
                            Session.currentToppingCollection.currentToppings[index].RemovedFromWhole = false;

                    }
                    else if (btnTopping.BackColor == Session.ToppingColor && Session.currentToppingColor == Color.Black)
                    {
                        btnTopping.BackColor = lngDefaultColor;
                        Session.currentToppingColor = lngDefaultColor;
                        btnTopping.BackgroundImage = null;
                        btnTopping.TextAlign = ContentAlignment.MiddleCenter;

                        blnSelected = false;

                        if (lngDefaultColor == Session.ToppingSizeLightColor)
                            mintChosen = mintChosen + 1;
                        else if (lngDefaultColor == Session.ToppingSizeSingleColor)
                            mintChosen = mintChosen + 1;
                        else if (lngDefaultColor == Session.ToppingSizeExtraColor)
                            mintChosen = mintChosen + 2;
                        else if (lngDefaultColor == Session.ToppingSizeDoubleColor)
                            mintChosen = mintChosen + 2;
                        else if (lngDefaultColor == Session.ToppingSizeTripleColor)
                            mintChosen = mintChosen + 3;

                        if (Session.currentItemPart == UserTypes.ItemParts.Whole)
                            Session.currentToppingCollection.currentToppings[index].RemovedFromWhole = false;
                    }
                    else if (btnTopping.BackColor == Session.ToppingColor && Session.currentToppingColor != Color.Black)
                    {
                        btnTopping.BackColor = Session.currentToppingColor;
                        btnTopping.BackgroundImage = null;
                        btnTopping.TextAlign = ContentAlignment.MiddleCenter;

                        blnSelected = true;

                        if (Session.currentToppingColor == Session.ToppingSizeLightColor)
                            mintChosen = mintChosen + 1;
                        else if (Session.currentToppingColor == Session.ToppingSizeSingleColor)
                            mintChosen = mintChosen + 1;
                        else if (Session.currentToppingColor == Session.ToppingSizeExtraColor)
                            mintChosen = mintChosen + 2;
                        else if (Session.currentToppingColor == Session.ToppingSizeDoubleColor)
                            mintChosen = mintChosen + 2;
                        else if (Session.currentToppingColor == Session.ToppingSizeTripleColor)
                            mintChosen = mintChosen + 3;

                        if (Session.currentItemPart == UserTypes.ItemParts.Whole)
                            Session.currentToppingCollection.currentToppings[index].RemovedFromWhole = false;
                    }
                }
                else
                {
                    if (btnTopping.BackColor == Session.ToppingColor)
                    {
                        blnSelected = true;

                        if (Session.currentItemPart != UserTypes.ItemParts.Whole)
                            if (Session.currentToppingCollection.currentToppings[index].WholeDefault && Session.currentToppingCollection.currentToppings[index].RemovedFromWhole)
                                return;

                        if (Session.currentToppingColor == Color.Black)
                        {
                            if (Session.currentToppingCollection.currentToppings[index].WholeDefault && Session.currentItemPart != UserTypes.ItemParts.Whole)
                            {
                                blnDefaultTopping = true;
                                strDefaultAmount = Session.currentToppingCollection.currentToppings[index].WholeDefaultAmount;


                                switch (strDefaultAmount)
                                {
                                    case "~":
                                        lngDefaultColor = Session.ToppingSizeLightColor;
                                        break;
                                    case "":
                                        lngDefaultColor = Session.ToppingSizeSingleColor;
                                        break;
                                    case "+":
                                        lngDefaultColor = Session.ToppingSizeExtraColor;
                                        break;
                                    case "2":
                                        lngDefaultColor = Session.ToppingSizeDoubleColor;
                                        break;
                                    case "3":
                                        lngDefaultColor = Session.ToppingSizeTripleColor;
                                        break;
                                }


                                btnTopping.BackColor = lngDefaultColor;
                                Session.currentToppingColor = lngDefaultColor;
                                btnTopping.BackgroundImage = null;
                                btnTopping.TextAlign = ContentAlignment.MiddleCenter;

                                blnSelected = false;

                                if (Session.currentToppingColor == Session.ToppingSizeLightColor)
                                    mintChosen = mintChosen + 1;
                                else if (Session.currentToppingColor == Session.ToppingSizeSingleColor)
                                    mintChosen = mintChosen + 1;
                                else if (Session.currentToppingColor == Session.ToppingSizeExtraColor)
                                    mintChosen = mintChosen + 2;
                                else if (Session.currentToppingColor == Session.ToppingSizeDoubleColor)
                                    mintChosen = mintChosen + 2;
                                else if (Session.currentToppingColor == Session.ToppingSizeTripleColor)
                                    mintChosen = mintChosen + 3;
                            }
                            else
                            {

                                btnTopping.BackColor = Session.ToppingSizeSingleColor;
                                Session.currentToppingColor = Session.ToppingSizeSingleColor;
                                mintChosen = mintChosen++;

                                if (Session.currentItemPart != UserTypes.ItemParts.Whole && Session.currentToppingCollection.currentToppings[index].SelectedOnWhole)
                                {
                                    btnTopping.BackgroundImage = null;
                                    btnTopping.TextAlign = ContentAlignment.MiddleCenter;
                                    blnSelected = false;

                                    switch (Session.currentToppingCollection.currentToppings[index].WholeSelectedAmount)
                                    {
                                        case "~":
                                            btnTopping.BackColor = Session.ToppingSizeLightColor;
                                            break;
                                        case "":
                                            btnTopping.BackColor = Session.ToppingSizeSingleColor;
                                            break;
                                        case "+":
                                            btnTopping.BackColor = Session.ToppingSizeExtraColor;
                                            break;
                                        case "2":
                                            btnTopping.BackColor = Session.ToppingSizeDoubleColor;
                                            break;
                                        case "3":
                                            btnTopping.BackColor = Session.ToppingSizeTripleColor;
                                            break;
                                    }
                                }

                                switch (Session.currentItemPart)
                                {
                                    case UserTypes.ItemParts.Whole:
                                        Session.currentToppingCollection.currentToppings[index].SelectedOnWhole = true;
                                        Session.currentToppingCollection.currentToppings[index].WholeSelectedAmount = strAmountCode;
                                        break;
                                    case UserTypes.ItemParts.FirstHalf:
                                        Session.currentToppingCollection.currentToppings[index].SelectedOnFirstHalf = true;
                                        Session.currentToppingCollection.currentToppings[index].FirstHalfSelectedAmount = strAmountCode;
                                        break;
                                    case UserTypes.ItemParts.SecondHalf:
                                        Session.currentToppingCollection.currentToppings[index].SelectedOnSecondHalf = true;
                                        Session.currentToppingCollection.currentToppings[index].SecondHalfSelectedAmount = strAmountCode;
                                        break;
                                }
                            }
                        }
                        else
                        {
                            btnTopping.BackColor = Session.currentToppingColor == Color.Gray ? Session.ToppingColor : Session.currentToppingColor;
                            btnTopping.BackgroundImage = null;
                            btnTopping.TextAlign = ContentAlignment.MiddleCenter;

                            if (Session.currentToppingColor == Session.ToppingSizeLightColor)
                                mintChosen = mintChosen + 1;
                            else if (Session.currentToppingColor == Session.ToppingSizeSingleColor)
                                mintChosen = mintChosen + 1;
                            else if (Session.currentToppingColor == Session.ToppingSizeExtraColor)
                                mintChosen = mintChosen + 2;
                            else if (Session.currentToppingColor == Session.ToppingSizeDoubleColor)
                                mintChosen = mintChosen + 2;
                            else if (Session.currentToppingColor == Session.ToppingSizeTripleColor)
                                mintChosen = mintChosen + 3;

                            switch (Session.currentItemPart)
                            {
                                case UserTypes.ItemParts.Whole:
                                    Session.currentToppingCollection.currentToppings[index].SelectedOnWhole = true;
                                    Session.currentToppingCollection.currentToppings[index].WholeSelectedAmount = strAmountCode;
                                    break;
                                case UserTypes.ItemParts.FirstHalf:
                                    Session.currentToppingCollection.currentToppings[index].SelectedOnFirstHalf = true;
                                    Session.currentToppingCollection.currentToppings[index].FirstHalfSelectedAmount = strAmountCode;
                                    break;
                                case UserTypes.ItemParts.SecondHalf:
                                    Session.currentToppingCollection.currentToppings[index].SelectedOnSecondHalf = true;
                                    Session.currentToppingCollection.currentToppings[index].SecondHalfSelectedAmount = strAmountCode;
                                    break;
                            }
                        }
                    }
                    else
                    {

                        if (btnTopping.BackColor == Session.ToppingSizeLightColor)
                            mintChosen = mintChosen - 1;
                        else if (btnTopping.BackColor == Session.ToppingSizeSingleColor)
                            mintChosen = mintChosen - 1;
                        else if (btnTopping.BackColor == Session.ToppingSizeExtraColor)
                            mintChosen = mintChosen - 2;
                        else if (btnTopping.BackColor == Session.ToppingSizeDoubleColor)
                            mintChosen = mintChosen - 2;
                        else if (btnTopping.BackColor == Session.ToppingSizeTripleColor)
                            mintChosen = mintChosen - 3;


                        if (Session.currentToppingColor == Color.Black)
                        {
                            if (Session.currentItemPart != UserTypes.ItemParts.Whole)
                            {
                                if (Session.currentToppingCollection.currentToppings[index].WholeDefault)
                                {
                                    btnTopping.BackgroundImage = Properties.Resources._97;
                                    btnTopping.TextAlign = ContentAlignment.BottomCenter;
                                    strAmountCode = "-";
                                    strDefaultAmount = Session.currentToppingCollection.currentToppings[index].WholeDefaultAmount;
                                    blnDefaultTopping = true;
                                    blnSelected = true;
                                    Session.currentToppingColor = Color.Gray; //Session.ToppingColor;
                                    btnTopping.BackColor = Session.currentToppingColor == Color.Gray ? Session.ToppingColor : Session.currentToppingColor;
                                }
                                else if (Session.currentToppingCollection.currentToppings[index].SelectedOnWhole)
                                {
                                    switch (Session.currentToppingCollection.currentToppings[index].WholeSelectedAmount)
                                    {
                                        case "~":
                                            if (btnTopping.BackColor == Session.ToppingSizeLightColor)
                                            {
                                                btnTopping.BackgroundImage = Properties.Resources._97;
                                                btnTopping.TextAlign = ContentAlignment.BottomCenter;
                                                strAmountCode = "-";
                                                Session.currentToppingColor = Color.Gray; //Session.ToppingColor;
                                                blnSelected = true;
                                            }
                                            else
                                            {
                                                strAmountCode = "";
                                                Session.currentToppingColor = Session.ToppingSizeLightColor;
                                                blnSelected = false;
                                            }
                                            break;
                                        case "":
                                        case " ":
                                            if (btnTopping.BackColor == Session.ToppingSizeSingleColor)
                                            {
                                                btnTopping.BackgroundImage = Properties.Resources._97;
                                                btnTopping.TextAlign = ContentAlignment.BottomCenter;
                                                strAmountCode = "-";
                                                Session.currentToppingColor = Color.Gray; //Session.ToppingColor;
                                                blnSelected = true;
                                            }
                                            else
                                            {
                                                strAmountCode = "";
                                                Session.currentToppingColor = Session.ToppingSizeSingleColor;
                                                blnSelected = false;
                                            }
                                            break;
                                        case "+":
                                            if (btnTopping.BackColor == Session.ToppingSizeExtraColor)
                                            {
                                                btnTopping.BackgroundImage = Properties.Resources._97;
                                                btnTopping.TextAlign = ContentAlignment.BottomCenter;
                                                strAmountCode = "-";
                                                Session.currentToppingColor = Color.Gray; //Session.ToppingColor;
                                                blnSelected = true;
                                            }
                                            else
                                            {
                                                strAmountCode = "";
                                                Session.currentToppingColor = Session.ToppingSizeExtraColor;
                                                blnSelected = false;
                                            }
                                            break;
                                        case "2":
                                            if (btnTopping.BackColor == Session.ToppingSizeDoubleColor)
                                            {
                                                btnTopping.BackgroundImage = Properties.Resources._97;
                                                btnTopping.TextAlign = ContentAlignment.BottomCenter;
                                                strAmountCode = "-";
                                                Session.currentToppingColor = Color.Gray; //Session.ToppingColor;
                                                blnSelected = true;
                                            }
                                            else
                                            {
                                                strAmountCode = "";
                                                Session.currentToppingColor = Session.ToppingSizeDoubleColor;
                                                blnSelected = false;
                                            }
                                            break;
                                        case "3":
                                            if (btnTopping.BackColor == Session.ToppingSizeTripleColor)
                                            {
                                                btnTopping.BackgroundImage = Properties.Resources._97;
                                                strAmountCode = "-";
                                                Session.currentToppingColor = Session.ToppingColor;
                                                blnSelected = true;
                                            }
                                            else
                                            {
                                                strAmountCode = "";
                                                Session.currentToppingColor = Session.ToppingSizeTripleColor;
                                                blnSelected = false;
                                            }
                                            break;
                                    }

                                    btnTopping.BackColor = Session.currentToppingColor == Color.Gray ? Session.ToppingColor : Session.currentToppingColor;
                                }
                                else
                                {
                                    blnSelected = false;
                                    btnTopping.BackColor = Session.ToppingColor;
                                    Session.currentToppingColor = Session.ToppingColor;
                                }
                            }
                            else
                            {
                                blnSelected = false;
                                btnTopping.BackColor = Session.ToppingColor;
                                Session.currentToppingColor = Session.ToppingColor;
                            }

                            switch (Session.currentItemPart)
                            {
                                case UserTypes.ItemParts.Whole:
                                    Session.currentToppingCollection.currentToppings[index].SelectedOnWhole = false;
                                    Session.currentToppingCollection.currentToppings[index].WholeSelectedAmount = "";
                                    break;
                                case UserTypes.ItemParts.FirstHalf:
                                    Session.currentToppingCollection.currentToppings[index].SelectedOnFirstHalf = false;
                                    Session.currentToppingCollection.currentToppings[index].FirstHalfSelectedAmount = "";
                                    break;
                                case UserTypes.ItemParts.SecondHalf:
                                    Session.currentToppingCollection.currentToppings[index].SelectedOnSecondHalf = false;
                                    Session.currentToppingCollection.currentToppings[index].SecondHalfSelectedAmount = "";
                                    break;
                            }
                        }
                        else
                        {
                            blnSelected = true;

                            btnTopping.BackColor = Session.currentToppingColor == Color.Gray ? Session.ToppingColor : Session.currentToppingColor;

                            if (Session.currentToppingColor == Session.ToppingSizeLightColor)
                                mintChosen = mintChosen + 1;
                            else if (Session.currentToppingColor == Session.ToppingSizeSingleColor)
                                mintChosen = mintChosen + 1;
                            else if (Session.currentToppingColor == Session.ToppingSizeExtraColor)
                                mintChosen = mintChosen + 2;
                            else if (Session.currentToppingColor == Session.ToppingSizeDoubleColor)
                                mintChosen = mintChosen + 2;
                            else if (Session.currentToppingColor == Session.ToppingSizeTripleColor)
                                mintChosen = mintChosen + 3;

                            //TO DO
                            switch (Session.currentItemPart)
                            {
                                case UserTypes.ItemParts.Whole:
                                    Session.currentToppingCollection.currentToppings[index].SelectedOnWhole = true;
                                    Session.currentToppingCollection.currentToppings[index].WholeSelectedAmount = strAmountCode;
                                    break;
                                case UserTypes.ItemParts.FirstHalf:
                                    Session.currentToppingCollection.currentToppings[index].SelectedOnFirstHalf = true;
                                    Session.currentToppingCollection.currentToppings[index].FirstHalfSelectedAmount = strAmountCode;
                                    break;
                                case UserTypes.ItemParts.SecondHalf:
                                    Session.currentToppingCollection.currentToppings[index].SelectedOnSecondHalf = true;
                                    Session.currentToppingCollection.currentToppings[index].SecondHalfSelectedAmount = strAmountCode;
                                    break;
                            }
                        }
                    }
                }

                int ButtonIndex = Session.currentToppingCollection.pizzaToppings.Find(x => x.ButtonCaption == btnTopping.Text).ButtonIndex;


                ////////////////////////////////
                if (btnTopping.BackColor == Session.ToppingColor)
                {
                    Session.cartToppings.Remove(Session.currentToppingCollection.currentToppings[index]);
                }
                else
                {
                    if (!Session.cartToppings.Contains(Session.currentToppingCollection.currentToppings[index]))
                    {
                        Session.cartToppings.Add(Session.currentToppingCollection.currentToppings[index]);
                    }
                }
                ////////////////////////////////////

                if (CartFunctions.ToppingChosen(btnTopping.Text, Convert.ToString(btnTopping.Tag), blnSelected, strAmountCode, (Session.currentItemPart == UserTypes.ItemParts.FirstHalf ? "1" : (Session.currentItemPart == UserTypes.ItemParts.SecondHalf ? "2" : "W")), blnDefaultTopping, strDefaultAmount, ButtonIndex, Session.currentToppingColor, mintChosen, selectedLineNumber))
                {
                    UpdateCartByMenutype();
                    RefreshCartUI();
                    CartControl();

                }

                SetSelectedToppingsCount();

                foreach (Control ctl in flowLayoutPanelToppingSizes.Controls)
                {
                    if (ctl.Name == "btnSingle")
                    {
                        ((Button)ctl).PerformClick();
                        break;
                    }
                }

                Session.currentToppingColor = Color.Black;

                if (Convert.ToInt32(uC_Customer_order_Header1.lblSelectedValue.Text) == Convert.ToInt32(uC_Customer_order_Header1.lblMaxValue.Text))
                {
                    if (Session.ProcessingCombo && Session.CurrentComboItem > 0)
                    {
                        pintCurrentOption_Group = 1;
                        HandleComboOptions();
                    }

                }


            }
            catch (Exception ex)
            {
                Logger.Trace("ERROR", "frmOrder-toppingsbuttonclick(): " + ex.Message, ex, true);
            }
        }

        private void btnOnHold_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session.cart != null && Session.cart.cartItems.Count > 0)
                {

                    if (lbltimedorder.Visible)
                    {
                        CustomMessageBox.Show(MessageConstant.AdvanceOrderOnHold, CustomMessageBox.Buttons.OK, CustomMessageBox.Icon.Exclamation);
                        return;
                    }

                    if (CustomMessageBox.Show(MessageConstant.PutCartOnHold, CustomMessageBox.Buttons.YesNo, CustomMessageBox.Icon.Question) == DialogResult.Yes)
                    {
                        if (String.IsNullOrEmpty(Session.cart.Customer.Name))
                        {
                            CustomMessageBox.Show(MessageConstant.CustomerMissing, CustomMessageBox.Buttons.OK, CustomMessageBox.Icon.Exclamation);
                            return;
                        }

                        Cart cartLocal = (new Cart()).GetCart();
                        cartLocal.cartHeader = Session.cart.cartHeader;
                        cartLocal.cartHeader.Action = "M";
                        CartFunctions.UpdateCustomer(cartLocal);
                        Session.cart = APILayer.Add2Cart(cartLocal);


                        btnOnHold.Enabled = false;
                        CartOnHoldRequest cartOnHoldRequest = new CartOnHoldRequest();
                        cartOnHoldRequest.CartId = Session.cart.cartHeader.CartId;
                        cartOnHoldRequest.Time = DateTime.Now; //DateTime.Now.ToString(); 
                        cartOnHoldRequest.CustomerName = Session.cart.Customer.Name;
                        cartOnHoldRequest.CustomerNumber = Session.cart.Customer.Phone_Number;
                        cartOnHoldRequest.OrderAmount = Convert.ToDecimal(Session.cart.cartHeader.Total);
                        cartOnHoldRequest.OrderTaker = Session.CurrentEmployee.LoginDetail.FirstName + " " + Session.CurrentEmployee.LoginDetail.LastName;
                        cartOnHoldRequest.Terminal = Session.ComputerName;
                        cartOnHoldRequest.IsActive = 1;
                        cartOnHoldRequest.OrderDate = Session.cart.cartHeader.Order_Date;

                        Session.cart = APILayer.InsertCartOnHold(cartOnHoldRequest);
                        RefreshCartUI();
                        var openFormOrder = Application.OpenForms.Cast<Form>().Where(x => x.Text.ToUpper() == "ORDER").FirstOrDefault();
                        if (openFormOrder != null)
                        {
                            openFormOrder.Show();
                        }
                        Session.IsTimerStarted = false;
                        Session.CurrentEmployee = null;
                        Session.FormClockOpened = false;
                        UserFunctions.GoToStartup(this);
                    }

                }
            }
            catch (Exception ex)
            {
                Logger.Trace("ERROR", "frmOrder-btnonhold_click(): " + ex.Message, ex, true);
            }
        }

        public void StartTimer()
        {
            if (!Session.IsTimerStarted)
            {
                uC_Customer_order_Header1.btnStart_Click();
            }
        }

        private void checkBoxVegOnly_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (checkBoxVegOnly.Checked == true)
                {
                    Session.VegOnlySelected = true;

                    foreach (Button btnVal in flowLayoutPanelMenuItems.Controls.OfType<Button>())
                    {
                        if (btnVal.FlatAppearance.BorderColor == Session.nonVegColor)
                        {
                            btnVal.Visible = false;
                        }
                    }
                    foreach (Button btnVal in flowLayoutPanelToppings.Controls.OfType<Button>())
                    {
                        if (btnVal.FlatAppearance.BorderColor == Session.nonVegColor)
                        {
                            btnVal.Visible = false;
                        }
                    }


                    int counter = Session.cart.cartItems.Count(x => x.MenuItemType == true);

                    if (counter > 0)
                    {
                        if (CustomMessageBox.Show(MessageConstant.RemoveNonVegItemsFromCart, CustomMessageBox.Buttons.YesNo, CustomMessageBox.Icon.Question) == DialogResult.Yes)
                        {
                            RemoveNonVegCart();
                        }
                    }

                }
                else
                {
                    Session.VegOnlySelected = false;
                    foreach (Button btnVal in flowLayoutPanelMenuItems.Controls.OfType<Button>())
                    {
                        try
                        {
                            if (btnVal.FlatAppearance.BorderColor == Session.nonVegColor)
                            {
                                btnVal.Visible = true;
                            }
                        }
                        catch (Exception exp)
                        {

                        }
                    }
                    foreach (Button btnVal in flowLayoutPanelToppings.Controls.OfType<Button>())
                    {
                        try
                        {
                            if (btnVal.FlatAppearance.BorderColor == Session.nonVegColor)
                            {
                                btnVal.Visible = true;
                            }
                        }
                        catch (Exception exp)
                        {

                        }
                    }
                    RefreshCartUI();
                }
            }
            catch (Exception ex)
            {
                Logger.Trace("ERROR", "frmOrder-checkboxvegonly_checkedchanged(): " + ex.Message, ex, true);
            }

            Button btnDynamic1 = MenuCategoryToClick(true);
            DynamicButtonClick(btnDynamic1, new EventArgs());

        }

        private void RemoveNonVegCart()
        {
            int counter = Session.cart.cartItems.Count(x => x.MenuItemType == true);
            if (counter > 0)
            {
                List<CartItem> nvegcartItems = new List<CartItem>();
                nvegcartItems = Session.cart.cartItems.Where(x => x.MenuItemType == true).ToList();

                foreach (CartItem item in nvegcartItems)
                {
                    CartItem cartItem = Session.cart.cartItems.Where(x => x.Menu_Code == item.Menu_Code && x.Description == item.Description && x.MenuItemType == true).FirstOrDefault();
                    int lineNumber = Session.cart.cartItems.IndexOf(cartItem) + 1;
                    if (CartFunctions.QuantityChange(false, -1, lineNumber, false))
                    {
                        counter--;
                        RefreshCartUI();
                    }
                }
            }

        }


        private void ItemPartChanged()
        {
            if (Session.currentToppingCollection != null && Session.currentToppingCollection.pizzaToppings != null && Session.currentToppingCollection.pizzaToppings.Count > 0)
            {

                //TO DO -- Use Enum everywhere
                string pstrPizzaPart = (Session.currentItemPart == UserTypes.ItemParts.FirstHalf ? "1" : (Session.currentItemPart == UserTypes.ItemParts.SecondHalf ? "2" : "W"));

                ResetToppings();

                if (Session.currentItemPart != UserTypes.ItemParts.Whole)
                {
                    foreach (PizzaTopping pizzaTopping in Session.currentToppingCollection.pizzaToppings)
                    {
                        if (pizzaTopping.ItemPart == "W")
                        {
                            HighlighTopping(pizzaTopping.ButtonIndex, pizzaTopping.ButtonColor, true);
                        }
                    }
                }


                foreach (PizzaTopping pizzaTopping in Session.currentToppingCollection.pizzaToppings)
                {
                    if (pizzaTopping.ItemPart == pstrPizzaPart)
                    {
                        if (pizzaTopping.DefaultTopping != "")
                        {
                            if (pizzaTopping.ButtonColor == Session.ToppingColor || pizzaTopping.ButtonColor == Color.Gray)
                            {
                                HighlighTopping(pizzaTopping.ButtonIndex, pizzaTopping.ButtonColor, true);
                            }
                            else
                            {
                                SetToppingDefault(GetMenuCodeFromButtonIndexTopping(pizzaTopping.ButtonIndex), pizzaTopping.ButtonColor);

                                HighlighTopping(pizzaTopping.ButtonIndex, pizzaTopping.ButtonColor, true);
                            }
                        }
                    }
                }


                foreach (PizzaTopping pizzaTopping in Session.currentToppingCollection.pizzaToppings)
                {
                    if (pizzaTopping.ItemPart == pstrPizzaPart)
                    {
                        if (pizzaTopping.DefaultTopping == "")
                        {
                            HighlighTopping(pizzaTopping.ButtonIndex, pizzaTopping.ButtonColor, true);
                        }
                    }
                    else if (pizzaTopping.ItemPart == "1" || pizzaTopping.ItemPart == "2")
                    {
                        //TO DO
                        //Call ctlToppings.UpdateChosenNoHighlight(ctlToppings.ButtonTag(xaPizza_Toppings(intRow, 0)), xaPizza_Toppings(intRow, 1))

                    }
                }

            }

            SetSelectedToppingsCount();

        }

        public void ResetToppings()
        {
            //TO DO
            //mintChosen = 0
            foreach (Button btn in flowLayoutPanelToppings.Controls.OfType<Button>())
            {
                if (btn.BackColor != Session.ToppingColor)
                    btn.BackColor = Session.ToppingColor;

                if (btn.BackgroundImage != null)
                {
                    btn.BackgroundImage = null;
                    btn.TextAlign = ContentAlignment.MiddleCenter;
                }
            }
        }

        private void HighlighTopping(int index, Color color, bool blnUpdateChosenCount)
        {
            try
            {
                Button btn = null;
                string Menu_Code = "";


                for (int i = 0; i <= flowLayoutPanelToppings.Controls.Count - 1; i++)
                {
                    if (i == index)
                    {
                        btn = ((Button)flowLayoutPanelToppings.Controls[i]);
                        btn.BackColor = color == Color.Gray ? Session.ToppingColor : color;
                        Menu_Code = btn.Tag.ToString();
                        break;
                    }
                }

                if (btn != null)
                {
                    if (color == Color.Gray)
                    {
                        btn.BackgroundImage = Properties.Resources._97;
                        btn.TextAlign = ContentAlignment.BottomCenter;

                        //TO DO
                        //            If blnUpdateChosenCount Then
                        //'-->
                        //    Select Case strDefaultAmount
                        //        Case "~"
                        //            mintChosen = mintChosen - 1
                        //        Case " "
                        //            mintChosen = mintChosen - 1
                        //        Case "+"
                        //            mintChosen = mintChosen - 2
                        //        Case "2"
                        //            mintChosen = mintChosen - 2
                        //        Case "3"
                        //            mintChosen = mintChosen - 3
                        //    End Select
                        //End If
                    }
                    else
                    {
                        if (btn.BackgroundImage != null)
                        {
                            btn.BackgroundImage = null;
                            btn.TextAlign = ContentAlignment.MiddleCenter;
                        }

                        //TO DO
                        //            If blnUpdateChosenCount Then
                        //'-->
                        //    If blnDefault Then
                        //        Select Case strDefaultAmount
                        //            Case "~"
                        //                Select Case lngColor
                        //                    Case cintLightColor
                        //                        mintChosen = mintChosen
                        //                    Case cintRed
                        //                        mintChosen = mintChosen
                        //                    Case cintExtraColor
                        //                        mintChosen = mintChosen + 1
                        //                    Case cintDoubleColor
                        //                        mintChosen = mintChosen + 1
                        //                    Case cintTripleColor
                        //                        mintChosen = mintChosen + 2
                        //                End Select
                        //            Case " "
                        //                Select Case lngColor
                        //                    Case cintLightColor
                        //                        mintChosen = mintChosen
                        //                    Case cintRed
                        //                        mintChosen = mintChosen
                        //                    Case cintExtraColor
                        //                        mintChosen = mintChosen + 1
                        //                    Case cintDoubleColor
                        //                        mintChosen = mintChosen + 1
                        //                    Case cintTripleColor
                        //                        mintChosen = mintChosen + 2
                        //                End Select
                        //            Case "+"
                        //                Select Case lngColor
                        //                    Case cintLightColor
                        //                        mintChosen = mintChosen - 1
                        //                    Case cintRed
                        //                        mintChosen = mintChosen - 1
                        //                    Case cintExtraColor
                        //                        mintChosen = mintChosen
                        //                    Case cintDoubleColor
                        //                        mintChosen = mintChosen
                        //                    Case cintTripleColor
                        //                        mintChosen = mintChosen + 1
                        //                End Select
                        //            Case "2"
                        //                Select Case lngColor
                        //                    Case cintLightColor
                        //                        mintChosen = mintChosen - 1
                        //                    Case cintRed
                        //                        mintChosen = mintChosen - 1
                        //                    Case cintExtraColor
                        //                        mintChosen = mintChosen
                        //                    Case cintDoubleColor
                        //                        mintChosen = mintChosen
                        //                    Case cintTripleColor
                        //                        mintChosen = mintChosen + 1
                        //                End Select
                        //            Case "3"
                        //                Select Case lngColor
                        //                    Case cintLightColor
                        //                        mintChosen = mintChosen - 2
                        //                    Case cintRed
                        //                        mintChosen = mintChosen - 2
                        //                    Case cintExtraColor
                        //                        mintChosen = mintChosen - 1
                        //                    Case cintDoubleColor
                        //                        mintChosen = mintChosen - 1
                        //                    Case cintTripleColor
                        //                        mintChosen = mintChosen
                        //                End Select
                        //        End Select
                        //    Else
                        //        Select Case lngColor
                        //            Case cintLightColor
                        //                mintChosen = mintChosen + 1
                        //            Case cintRed
                        //                mintChosen = mintChosen + 1
                        //            Case cintExtraColor
                        //                mintChosen = mintChosen + 2
                        //            Case cintDoubleColor
                        //                mintChosen = mintChosen + 2
                        //            Case cintTripleColor
                        //                mintChosen = mintChosen + 3
                        //        End Select
                        //    End If
                        //End If
                    }
                }

            }
            catch (Exception ex)
            {
                Logger.Trace("ERROR", "frmOrder-highlightopping(): " + ex.Message, ex, true);
            }
        }

        public void SetToppingDefault(string Menu_Code, Color color)
        {
            foreach (Button btn in flowLayoutPanelToppings.Controls.OfType<Button>())
            {
                if (btn.Tag != null && btn.Tag.ToString() == Menu_Code)
                {
                    btn.BackColor = color;

                    //TO DO -- based on color or Amount Code
                    //mintChosen = mintChosen + 1

                    int toppingIndex = Session.currentToppingCollection.currentToppings.FindIndex(x => x.Menu_Code == Menu_Code);

                    switch (Session.currentItemPart)
                    {
                        case UserTypes.ItemParts.FirstHalf:
                            Session.currentToppingCollection.currentToppings[toppingIndex].FirstHalfDefault = true;
                            Session.currentToppingCollection.currentToppings[toppingIndex].FirstHalfDefaultAmount = UserFunctions.GetAmountCodebyColor(color);
                            break;
                        case UserTypes.ItemParts.SecondHalf:
                            Session.currentToppingCollection.currentToppings[toppingIndex].SecondHalfDefault = true;
                            Session.currentToppingCollection.currentToppings[toppingIndex].SecondHalfDefaultAmount = UserFunctions.GetAmountCodebyColor(color);
                            break;
                        case UserTypes.ItemParts.Whole:
                            Session.currentToppingCollection.currentToppings[toppingIndex].WholeDefault = true;
                            Session.currentToppingCollection.currentToppings[toppingIndex].WholeDefaultAmount = UserFunctions.GetAmountCodebyColor(color);
                            break;
                    }
                }
            }
        }

        private string GetMenuCodeFromButtonIndexTopping(int index)
        {
            try
            {
                for (int i = 0; i <= flowLayoutPanelToppings.Controls.Count - 1; i++)
                {
                    if (i == index)
                    {
                        return ((Button)flowLayoutPanelToppings.Controls[i]).Tag.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Trace("ERROR", ex.Message, ex, true);
            }
            return "";
        }


        public void OrderCoupons()
        {
            lblWhere.Text = APILayer.GetCatalogText(LanguageConstant.cintCoupons);

            flowLayoutPanelMenuItems.Visible = true;
            flowLayoutPanelMenuItems.BringToFront();
            flowLayoutPanelMenuItems.Controls.Clear();
            int x = Constants.HorizontalSpace, y = Constants.VerticalSpace;
            string PreviousSelectedCoupons = "";

            pnl_Quantity.Visible = false;
            pnl_Quantity.SendToBack();
            tlpToppings.Visible = false;
            tlpToppings.SendToBack();
            flowLayoutPanelSpecialtyPizzas.Visible = false;
            flowLayoutPanelSpecialtyPizzas.SendToBack();
            Session.currentToppingCollection = null;
            uC_Customer_order_Header1.pnl_MinMax.Visible = false;
            uC_Customer_order_Header1.lblSelectedValue.Text = "0";

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
                if (item.Coupon_Code != "OPS004") //Exculde Remake coupon
                {
                    Button btnDynamic = new Button();

                    btnDynamic.Location = new System.Drawing.Point(x, y);
                    btnDynamic.Size = new System.Drawing.Size(Constants.MenuCardButtonWidthG, Constants.ButtonHeightG);
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


                    flowLayoutPanelMenuItems.Controls.Add(btnDynamic);

                    x += Constants.MenuCardButtonWidthG;
                    btnDynamic.Click += new EventHandler(this.DynamicButtonClick);
                }
            }
        }

        private void HandleCombos(string btnName)
        {

            ComboFunctions.GetComboCartSizeChange(btnName);
            RefreshCartUI();
            CartControl();


            ItemCombo itemCombo = Session.cart.itemCombos.Find(x => x.Combo_Group == Session.CurrentComboGroup);


            List<CatalogPOSComboMealItems> currentComboMealItems = APILayer.GetComboMealItems(itemCombo.Combo_Menu_Code, itemCombo.Combo_Size_Code);
            foreach (CatalogPOSComboMealItems catalogPOSComboMealItems in currentComboMealItems)
            {
                //if (!Session.comboMenuItems.Contains(catalogPOSComboMealItems)) Session.comboMenuItems.Add(catalogPOSComboMealItems);

                //if (!Session.comboMenuItems.Any(z => z.Menu_Code == catalogPOSComboMealItems.Menu_Code)) Session.comboMenuItems.Add(catalogPOSComboMealItems);
                if (!Session.comboMenuItems.Any(z => z.Combo_Menu_Code == catalogPOSComboMealItems.Combo_Menu_Code && z.Combo_Size_Code == catalogPOSComboMealItems.Combo_Size_Code && z.Menu_Code == catalogPOSComboMealItems.Menu_Code && z.Item_Number == catalogPOSComboMealItems.Item_Number)) Session.comboMenuItems.Add(catalogPOSComboMealItems);
            }


            List<CatalogPOSComboMealItemSizes> currentComboMealItemSizes = APILayer.GetComboMealItemSizes(itemCombo.Combo_Menu_Code, itemCombo.Combo_Size_Code);
            foreach (CatalogPOSComboMealItemSizes catalogPOSComboMealItemSizes in currentComboMealItemSizes)
            {
                //if (!Session.comboMenuItemSizes.Contains(catalogPOSComboMealItemSizes)) Session.comboMenuItemSizes.Add(catalogPOSComboMealItemSizes);

                //if (!Session.comboMenuItemSizes.Any(z => z.Menu_Code == catalogPOSComboMealItemSizes.Menu_Code && z.Size_Code == catalogPOSComboMealItemSizes.Size_Code)) Session.comboMenuItemSizes.Add(catalogPOSComboMealItemSizes);
                if (!Session.comboMenuItemSizes.Any(z => z.Combo_Menu_Code == catalogPOSComboMealItemSizes.Combo_Menu_Code && z.Combo_Size_Code == catalogPOSComboMealItemSizes.Combo_Size_Code && z.Menu_Code == catalogPOSComboMealItemSizes.Menu_Code && z.Size_Code == catalogPOSComboMealItemSizes.Size_Code && z.Item_Number == catalogPOSComboMealItemSizes.Item_Number)) Session.comboMenuItemSizes.Add(catalogPOSComboMealItemSizes);

            }


            if (!(currentComboMealItems != null && currentComboMealItems.Count > 0 && currentComboMealItemSizes != null && currentComboMealItemSizes.Count > 0))
            {
                ComboFunctions.RemoveInvalidComboFromCart(itemCombo);
                Button btnDynamic1 = MenuCategoryToClick(true);
                DynamicButtonClick(btnDynamic1, new EventArgs());
                return;
            }


            selectedLineNumber = (Session.cart.cartItems != null && Session.cart.cartItems.Count > 0) ? Session.cart.cartItems[Session.cart.cartItems.Count - 1].Line_Number + 1 : 1;
            ComboFunctions.AddComboSizeAndItemsToCart(itemCombo, currentComboMealItems, currentComboMealItemSizes);
            RefreshCartUI();
            CartControl();

            Session.CurrentComboItem = 0;
            foreach (CartItem cartItem in Session.cart.cartItems)
            {
                if (cartItem.Combo_Group == Session.CurrentComboGroup)
                {
                    if (cartItem.Combo_Prompt_Attributes || cartItem.Combo_Prompt_Menu_Item || cartItem.Combo_Prompt_Options || cartItem.Combo_Prompt_Size)
                    {
                        Session.CurrentComboItem = cartItem.Combo_Item_Number;
                        break;
                    }
                }
            }

            if (Session.CurrentComboItem == 0)
            {
                CurrentLineType = "";
                Button btnDynamic1 = MenuCategoryToClick(true);
                DynamicButtonClick(btnDynamic1, new EventArgs());
            }
            else
            {
                //Session.MenuCategoryButtonClicked = true;
                HandleComboMenuItems();
            }

        }

        private void HandleComboMenuItems()
        {
            if (Session.CurrentComboGroup <= 0) return;

            if (Session.cart.itemCombos == null || Session.cart.itemCombos.Count <= 0) return;


            ItemCombo itemCombo = Session.cart.itemCombos.Find(x => x.Combo_Group == Session.CurrentComboGroup);

            if (itemCombo == null) return;


            if (Session.CurrentComboItem == 0 || Session.CurrentComboItem > itemCombo.Number_Of_Combo_Items)
            {
                if (SystemSettings.settings.pblnShowCoupons)
                {
                    //TO DO
                    //HandleOrderLineCoupons()
                    return;
                }
                else
                {
                    CurrentLineType = "";
                    Button btnDynamic1 = MenuCategoryToClick(true);
                    DynamicButtonClick(btnDynamic1, new EventArgs());
                }

            }

            lblWhere.Text = APILayer.GetCatalogText(LanguageConstant.cintMenuItems);

            List<CatalogPOSComboMealItemsForButtons> catalogPOSComboMealItemsForButtons = APILayer.GetComboMealItemsForButtons(itemCombo.Combo_Menu_Code, itemCombo.Combo_Size_Code);

            if (catalogPOSComboMealItemsForButtons == null || catalogPOSComboMealItemsForButtons.Count <= 0) return;

            catalogPOSComboMealItemsForButtons = catalogPOSComboMealItemsForButtons.FindAll(x => x.Item_Number == Session.CurrentComboItem).ToList();


            CartItem cartItem = Session.cart.cartItems.Find(x => x.Combo_Group == Session.CurrentComboGroup && x.Combo_Item_Number == Session.CurrentComboItem);

            if (cartItem == null) return;

            selectedLineNumber = cartItem.Line_Number;

            if (Session.CurrentComboItem > 0)
            {
                if (/*Session.MenuCategoryButtonClicked &&*/ /* TO DO -- (Not pblnModifyingOrder Or(pblnModifyingOrder And frmOrder.blnModifyingByAdding)) And*/
                  CartClicked == false && cartItem.Combo_Prompt_Menu_Item == false && catalogPOSComboMealItemsForButtons.Count > 1)
                {
                    Session.selectedComboMenuItem = Session.comboMenuItems.Find(x => x.Combo_Menu_Code == itemCombo.Combo_Menu_Code && x.Combo_Size_Code == itemCombo.Combo_Size_Code && x.Item_Number == Session.CurrentComboItem && x.Default == true);
                    HandleComboMenuItemSizes();
                    return;
                }
                else
                {
                    if ((/*Session.MenuCategoryButtonClicked == false &&*/ CartClicked == false && catalogPOSComboMealItemsForButtons.Count == 1)
                       || (/*Session.MenuCategoryButtonClicked == false &&*/CartClicked == false && cartItem.Combo_Prompt_Menu_Item == false && catalogPOSComboMealItemsForButtons.Count > 1))
                    {
                        Session.selectedComboMenuItem = Session.comboMenuItems.Find(x => x.Combo_Menu_Code == itemCombo.Combo_Menu_Code && x.Combo_Size_Code == itemCombo.Combo_Size_Code && x.Item_Number == Session.CurrentComboItem && x.Default == true);
                        HandleComboMenuItemSizes();
                        return;
                    }
                    else
                    {
                        if (catalogPOSComboMealItemsForButtons != null && catalogPOSComboMealItemsForButtons.Count > 0)
                        {
                            CartClicked = false;
                            CreateMenuItemButtonsForComboMeal(catalogPOSComboMealItemsForButtons, cartItem.Menu_Code);
                        }
                        else
                        {
                            Session.selectedComboMenuItem = Session.comboMenuItems.Find(x => x.Combo_Menu_Code == itemCombo.Combo_Menu_Code && x.Combo_Size_Code == itemCombo.Combo_Size_Code && x.Item_Number == Session.CurrentComboItem && x.Default == true);
                            HandleComboMenuItemSizes();
                            return;
                        }

                    }
                }
            }

            //Session.MenuCategoryButtonClicked = false;

            CartControl();

        }

        private void CreateMenuItemButtonsForComboMeal(List<CatalogPOSComboMealItemsForButtons> catalogPOSComboMealItemsForButtons, string PreviousSelectedMenuCode)
        {
            flowLayoutPanelMenuItems.Visible = true;
            flowLayoutPanelMenuItems.BringToFront();
            flowLayoutPanelMenuItems.Controls.Clear();
            int x = Constants.HorizontalSpace, y = Constants.VerticalSpace;

            pnl_Quantity.Visible = false;
            pnl_Quantity.SendToBack();
            tlpToppings.Visible = false;
            tlpToppings.SendToBack();
            flowLayoutPanelSpecialtyPizzas.Visible = false;
            flowLayoutPanelSpecialtyPizzas.SendToBack();
            Session.currentToppingCollection = null;
            uC_Customer_order_Header1.pnl_MinMax.Visible = false;
            uC_Customer_order_Header1.lblSelectedValue.Text = "0";

            foreach (CatalogPOSComboMealItemsForButtons _catalogPOSComboMealItemsForButtons in catalogPOSComboMealItemsForButtons)
            {
                //if (!Session.catalogPOSComboMealItemsForButtons.Contains(_catalogPOSComboMealItemsForButtons)) Session.catalogPOSComboMealItemsForButtons.Add(_catalogPOSComboMealItemsForButtons);
                if (!Session.catalogPOSComboMealItemsForButtons.Any(z => z.Combo_Menu_Code == _catalogPOSComboMealItemsForButtons.Combo_Menu_Code && z.Combo_Size_Code == _catalogPOSComboMealItemsForButtons.Combo_Size_Code && z.Menu_Code == _catalogPOSComboMealItemsForButtons.Menu_Code && z.Item_Number == _catalogPOSComboMealItemsForButtons.Item_Number)) Session.catalogPOSComboMealItemsForButtons.Add(_catalogPOSComboMealItemsForButtons);

            }

            foreach (var item in catalogPOSComboMealItemsForButtons)
            {
                Button btnDynamic = new Button();

                btnDynamic.Location = new System.Drawing.Point(x, y);
                btnDynamic.Size = new System.Drawing.Size(Constants.MenuCardButtonWidthG, Constants.ButtonHeightG);

                btnDynamic.Name = item.Menu_Code;
                btnDynamic.Tag = "MenuItems";
                btnDynamic.Text = item.Order_Description;
                btnDynamic.Font = new Font(new FontFamily("Microsoft Sans Serif"), 8);

                if (item.Menu_Code == PreviousSelectedMenuCode)
                    btnDynamic.BackColor = Session.DefaultEntityColor;
                else
                    btnDynamic.BackColor = SystemColors.Control;

                btnDynamic.Margin = new Padding(0);

                flowLayoutPanelMenuItems.Controls.Add(btnDynamic);
                x += Constants.MenuCardButtonWidthG;
                btnDynamic.Click += new EventHandler(this.DynamicButtonClick);
                if (Session.VegOnlySelected)
                {
                    if (btnDynamic.FlatAppearance.BorderColor == Session.nonVegColor)
                    {
                        btnDynamic.Visible = false;
                    }
                }
                else
                {
                    if (btnDynamic.FlatAppearance.BorderColor == Session.nonVegColor)
                    {
                        btnDynamic.Visible = true;
                    }
                }
            }
            CurrentLineType = "M";
        }

        private void ComboChildMenuItemSelected(string btnName)
        {
            CartItem cartItem = Session.cart.cartItems.Find(x => x.Combo_Group == Session.CurrentComboGroup && x.Combo_Item_Number == Session.CurrentComboItem);

            //CatalogPOSComboMealItems comboMealItem = Session.comboMenuItems.Find(x => x.Combo_Menu_Code == cartItem.Combo_Menu_Code && x.Combo_Size_Code == cartItem.Combo_Size_Code && x.Menu_Code == cartItem.Menu_Code);
            CatalogPOSComboMealItems comboMealItem = Session.comboMenuItems.Find(x => x.Combo_Menu_Code == cartItem.Combo_Menu_Code && x.Combo_Size_Code == cartItem.Combo_Size_Code && x.Menu_Code == btnName && x.Item_Number == Session.CurrentComboItem);
            if (comboMealItem == null)
            {
                List<CatalogPOSComboMealItems> currentComboMealItems = APILayer.GetComboMealItems(cartItem.Combo_Menu_Code, cartItem.Combo_Size_Code);
                //if (currentComboMealItems != null && currentComboMealItems.Count > 0) comboMealItem = currentComboMealItems.Find(x => x.Menu_Code == cartItem.Menu_Code);
                if (currentComboMealItems != null && currentComboMealItems.Count > 0) comboMealItem = currentComboMealItems.Find(x => x.Menu_Code == btnName && x.Item_Number == Session.CurrentComboItem);
            }

            if (comboMealItem != null)
            {
                Session.selectedComboMenuItem = comboMealItem;
                ComboFunctions.ComboChildMenuItemChange(cartItem, btnName);
                RefreshCartUI();
                CartControl();
            }

            HandleComboMenuItemSizes();
        }

        private void HandleComboMenuItemSizes()
        {
            CartItem cartItem = Session.cart.cartItems.Find(x => x.Line_Number == selectedLineNumber);

            if (cartItem == null) return;

            ItemCombo itemCombo = Session.cart.itemCombos.Find(x => x.Combo_Group == Session.CurrentComboGroup);

            if (itemCombo == null) return;

            lblWhere.Text = cartItem.Menu_Description + " - " + APILayer.GetCatalogText(LanguageConstant.cintSizes);

            List<CatalogPOSComboMealItemSizes> comboMealItemSizes = Session.comboMenuItemSizes.FindAll(x => x.Combo_Menu_Code == itemCombo.Combo_Menu_Code && x.Combo_Size_Code == itemCombo.Combo_Size_Code && x.Menu_Code == cartItem.Menu_Code && x.Item_Number == Session.CurrentComboItem);
            if (comboMealItemSizes == null || comboMealItemSizes.Count <= 0)
            {
                List<CatalogPOSComboMealItemSizes> currentComboMealItemSizes = APILayer.GetComboMealItemSizes(itemCombo.Combo_Menu_Code, itemCombo.Combo_Size_Code);
                if (currentComboMealItemSizes != null && currentComboMealItemSizes.Count > 0) comboMealItemSizes = currentComboMealItemSizes.FindAll(x => x.Menu_Code == cartItem.Menu_Code && x.Item_Number == Session.CurrentComboItem);
            }

            if (comboMealItemSizes == null || comboMealItemSizes.Count <= 0) return;

            if (comboMealItemSizes.Count == 1)
            {
                UpdateSize(comboMealItemSizes[0].Size_Code);

                if (cartItem.NumberOfAttributes > 0)
                {
                    //TO DO
                    //Call HandleComboAttributes
                }
                else if (cartItem.NumberOfOptions > 0)
                {
                    HandleComboOptions();
                }
                else
                {
                    Session.CurrentComboItem = Session.CurrentComboItem + 1;

                    HandleComboMenuItems();
                }
                CartControl();
                return;
            }
            else
            {
                if (comboMealItemSizes.Count > 1)
                {
                    if (!cartItem.Combo_Prompt_Size  /*(And Not pblnModifyingOrder Or(pblnModifyingOrder And frmOrder.blnModifyingByAdding)) And Not pblnBack */ )
                    {
                        if (cartItem.Size_Code != "")
                        {
                            UpdateSize(cartItem.Size_Code);

                            if (cartItem.NumberOfAttributes > 0)
                            {
                                //TO DO
                                //Call HandleComboAttributes
                            }
                            else if (cartItem.NumberOfOptions > 0)
                            {
                                HandleComboOptions();
                            }
                            else
                            {
                                Session.CurrentComboItem = Session.CurrentComboItem + 1;

                                HandleComboMenuItems();
                            }
                            CartControl();
                            return;
                        }
                        else
                        {
                            foreach (CatalogPOSComboMealItemSizes _catalogPOSComboMealItemSizes in comboMealItemSizes)
                            {
                                if (_catalogPOSComboMealItemSizes.Default)
                                {
                                    UpdateSize(_catalogPOSComboMealItemSizes.Size_Code);

                                    if (cartItem.NumberOfAttributes > 0)
                                    {
                                        //TO DO
                                        //Call HandleComboAttributes
                                    }
                                    else if (cartItem.NumberOfOptions > 0)
                                    {
                                        HandleComboOptions();
                                    }
                                    else
                                    {
                                        Session.CurrentComboItem = Session.CurrentComboItem + 1;

                                        HandleComboMenuItems();
                                    }
                                    CartControl();
                                    return;
                                }
                            }
                        }
                    }
                    else
                    {
                        CreateMenuItemSizesForComboMeal(comboMealItemSizes, cartItem);
                    }
                }
                else
                {
                    if (cartItem.NumberOfAttributes > 0)
                    {
                        //TO DO
                        //Call HandleComboAttributes
                    }
                    else if (cartItem.NumberOfOptions > 0)
                    {
                        HandleComboOptions();
                    }
                    else
                    {
                        Session.CurrentComboItem = Session.CurrentComboItem + 1;

                        HandleComboMenuItems();
                    }
                    CartControl();
                    return;
                }
            }

            CartControl();
        }

        private void CreateMenuItemSizesForComboMeal(List<CatalogPOSComboMealItemSizes> comboMealItemSizes, CartItem cartItem)
        {
            flowLayoutPanelMenuItems.Visible = true;
            flowLayoutPanelMenuItems.BringToFront();
            flowLayoutPanelMenuItems.Controls.Clear();
            int x = Constants.HorizontalSpace, y = Constants.VerticalSpace;

            foreach (var itemSize in comboMealItemSizes)
            {
                Button btnDynamic = new Button();

                btnDynamic.Location = new System.Drawing.Point(x, y);
                btnDynamic.Size = new System.Drawing.Size(Constants.MenuCardButtonWidthG, Constants.ButtonHeightG);

                btnDynamic.Name = itemSize.Size_Code;
                btnDynamic.Tag = "MenuItemSizes";
                btnDynamic.Text = itemSize.Description;
                btnDynamic.Font = new Font(new FontFamily("Microsoft Sans Serif"), 8);
                btnDynamic.BackColor = SystemColors.Control;
                btnDynamic.Margin = new Padding(0);

                if (cartItem.Size_Code != "" && cartItem.Size_Code == itemSize.Size_Code)
                    btnDynamic.BackColor = Session.DefaultEntityColor;
                else
                    btnDynamic.BackColor = SystemColors.Control;


                flowLayoutPanelMenuItems.Controls.Add(btnDynamic);
                x += Constants.MenuCardButtonWidthG;
                btnDynamic.Click += new EventHandler(this.DynamicButtonClick);

            }
            CurrentLineType = "M";
        }

        private void UpdateSize(string Size_Code)
        {
            CurrentLineType = "O";
            CartFunctions.GetCart("MenuItemSizes", Size_Code, ref selectedLineNumber);
            RefreshCartUI();
            CartControl();
        }


        private void HandleComboOptions()
        {
            CartItem cartItem = Session.cart.cartItems.Find(x => x.Line_Number == selectedLineNumber);

            if (cartItem == null) return;

            if (cartItem.NumberOfOptions == 0 || pintCurrentOption_Group > 0 /*|| (UBound(pstrOption_Groups) <> 0 And UBound(pstrOption_Groups) <= pintCurrentOption_Group) */) //TO DO -- Only one Option group considered
            {
                pintCurrentOption_Group = 0;
                uC_Customer_order_Header1.lblMaxValue.Text = "0";
                uC_Customer_order_Header1.lblMinValue.Text = "0";
                uC_Customer_order_Header1.lblSelectedValue.Text = "0";

                Session.CurrentComboItem = Session.CurrentComboItem + 1;

                CurrentLineType = "M";
                HandleComboMenuItems();
            }
            else
            {
                //TO DO -- Specialty Pizza
                //        If pintProcessPoint = intSpecialtyPizzas Then
                //    blnCommingFromSpecialyPizzas = True
                //Else
                //    blnCommingFromSpecialyPizzas = False
                //End If
                uC_Customer_order_Header1.lblSelectedValue.Text = "0";

                PopulateToppings(cartItem.Menu_Code);
                CartFunctions.PopulateDefaultToppings(ref Session.cart, selectedLineNumber, Session.selectedComboMenuItem.Menu_Code,
                                                            "", Session.selectedComboMenuItem.Specialty_Pizza_Code, "");

                if (Session.currentToppingCollection.currentCatalogOptionGroups.Topping_Group)
                {
                    if ( /*(Not pblnModifyingOrder Or(pblnModifyingOrder And frmOrder.blnModifyingByAdding)) And Not pblnBack And*/ !cartItem.Combo_Prompt_Options)
                    {
                        ToppingsOkButtonClick(null, new EventArgs());
                    }
                    else
                    {
                        lblWhere.Text = cartItem.Menu_Description + " - " + Session.currentToppingCollection.currentCatalogOptionGroups.Description;
                        tlpToppings.Visible = true;
                        tlpToppings.BringToFront();
                        uC_Customer_order_Header1.pnl_MinMax.Visible = true;
                        btnSpec.Visible = false;
                    }
                }
                //else //TO DO -- ONly one Option Group considered

            }
        }

        private void MenuCatagoryPanelSizeAdjustment(int No_Of_Cataegories)
        {
            if (Session.businessUnits != null && Session.businessUnits.Count > 1)
            {
                flowLayoutPanelBUnit.Visible = true;

                if (((flowLayoutPanelMenuCatagories.Width - Constants.HorizontalSpace) / Constants.MenuCardButtonWidthG) < No_Of_Cataegories)
                {
                    flowLayoutPanelMenuCatagories.Location = new Point(flowLayoutPanelMenuCatagories.Location.X, 57);
                    flowLayoutPanelMenuCatagories.Size = new Size(flowLayoutPanelMenuCatagories.Size.Width, 118);
                    flowLayoutPanelMenuItems.Location = new Point(flowLayoutPanelMenuItems.Location.X, 173);
                }
                else
                {
                    flowLayoutPanelMenuCatagories.Location = new Point(flowLayoutPanelMenuCatagories.Location.X, 57);
                    flowLayoutPanelMenuCatagories.Size = new Size(flowLayoutPanelMenuCatagories.Size.Width, 59);
                    flowLayoutPanelMenuItems.Location = new Point(flowLayoutPanelMenuItems.Location.X, 114);
                }

                Session.MaxMenuItemsPerPage = ((flowLayoutPanelMenuItems.Width - Constants.HorizontalSpace) / Constants.MenuCardButtonWidthG) * (((flowLayoutPanelMenuItems.Location.Y == 173 ? flowLayoutPanelMenuItems.Height - 112 : flowLayoutPanelMenuItems.Height - 57) - Constants.VerticalSpace) / Constants.ButtonHeightG);

            }
            else
            {
                flowLayoutPanelBUnit.Visible = false;

                if (((flowLayoutPanelMenuCatagories.Width - Constants.HorizontalSpace) / Constants.MenuCardButtonWidthG) < No_Of_Cataegories)
                {
                    flowLayoutPanelMenuCatagories.Location = new Point(flowLayoutPanelMenuCatagories.Location.X, 0);
                    flowLayoutPanelMenuCatagories.Size = new Size(flowLayoutPanelMenuCatagories.Size.Width, 118);
                    flowLayoutPanelMenuItems.Location = new Point(flowLayoutPanelMenuItems.Location.X, 114);
                }
                else
                {
                    flowLayoutPanelMenuCatagories.Location = new Point(flowLayoutPanelMenuCatagories.Location.X, 0);
                    flowLayoutPanelMenuCatagories.Size = new Size(flowLayoutPanelMenuCatagories.Size.Width, 59);
                    flowLayoutPanelMenuItems.Location = new Point(flowLayoutPanelMenuItems.Location.X, 57);
                }

                Session.MaxMenuItemsPerPage = ((flowLayoutPanelMenuItems.Width - Constants.HorizontalSpace) / Constants.MenuCardButtonWidthG) * (((flowLayoutPanelMenuItems.Location.Y == 114 ? flowLayoutPanelMenuItems.Height - 57 : flowLayoutPanelMenuItems.Height) - Constants.VerticalSpace) / Constants.ButtonHeightG);
            }
        }

        private void PopulateMenuItems(int PageNo)
        {
            int x = Constants.HorizontalSpace, y = Constants.VerticalSpace;
            int startIndex = 0;
            int LastIndex = 0;
            bool CreateMoreButton = false;
            //bool showBackButton = false;

            List<CatalogMenuItems> catalogMenuItems = Session.menuItems.FindAll(z => z.Menu_Category_Code == Session.currentMenuCategoryCode);

            if (checkBoxVegOnly.Checked)
                catalogMenuItems = catalogMenuItems.FindAll(z => z.MenuItemType == false);

            Session.TotalPageMenuItems = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(catalogMenuItems.Count) / Session.MaxMenuItemsPerPage));

            if (Session.MaxMenuItemsPerPage < catalogMenuItems.Count)
                CreateMoreButton = true;

            if (PageNo == 1)
            {
                startIndex = 0;
                if (CreateMoreButton)
                    LastIndex = Session.MaxMenuItemsPerPage - 1;
                else
                    LastIndex = catalogMenuItems.Count;
            }
            else
            {
                //startIndex = Session.MaxMenuItemsPerPage - 1;
                //LastIndex = (catalogMenuItems.Count - startIndex) > Session.MaxMenuItemsPerPage - 1 ? (startIndex + Session.MaxMenuItemsPerPage - 1) : catalogMenuItems.Count;

                startIndex = (Session.MaxMenuItemsPerPage - 1) * (PageNo - 1);
                LastIndex = (catalogMenuItems.Count - startIndex) > (Session.MaxMenuItemsPerPage - 1) ? ((Session.MaxMenuItemsPerPage - 1) * (PageNo - 1)) + (Session.MaxMenuItemsPerPage - 1) : catalogMenuItems.Count;

                //showBackButton = true;
            }


            for (int i = startIndex; i < LastIndex; i++)
            {
                Button btnDynamic = new Button();

                btnDynamic.Location = new System.Drawing.Point(x, y);
                btnDynamic.Size = new System.Drawing.Size(Constants.MenuCardButtonWidthG, Constants.ButtonHeightG);

                btnDynamic.Name = catalogMenuItems[i].Menu_Code.Trim();
                btnDynamic.Tag = "MenuItems";
                btnDynamic.Text = catalogMenuItems[i].Order_Description.Trim();
                btnDynamic.Font = new Font(new FontFamily("Microsoft Sans Serif"), 8);

                btnDynamic.BackColor = SystemColors.Control;
                if (catalogMenuItems[i].MenuItemType == false)
                {
                    btnDynamic.FlatStyle = FlatStyle.Flat;
                    btnDynamic.FlatAppearance.BorderColor = Session.vegColor;
                    btnDynamic.FlatAppearance.BorderSize = 3;
                }
                else if (catalogMenuItems[i].MenuItemType == true)
                {
                    btnDynamic.FlatStyle = FlatStyle.Flat;
                    btnDynamic.FlatAppearance.BorderColor = Session.nonVegColor;
                    btnDynamic.FlatAppearance.BorderSize = 3;
                }
                else if (catalogMenuItems[i].MenuItemType == null)
                {
                    btnDynamic.BackColor = SystemColors.Control;
                }

                btnDynamic.Margin = new Padding(0);
                btnDynamic.Enabled = catalogMenuItems[i].Enabled ? catalogMenuItems[i].EnabledInv : catalogMenuItems[i].Enabled;
                if (catalogMenuItems[i].Menu_Item_Image != null)
                {
                    byte[] binaryData = Convert.FromBase64String(catalogMenuItems[i].Menu_Item_Image);
                    btnDynamic.Image = Image.FromStream(new MemoryStream(binaryData));
                    btnDynamic.TextAlign = ContentAlignment.BottomCenter;
                    btnDynamic.ImageAlign = ContentAlignment.TopCenter;
                }
                flowLayoutPanelMenuItems.Controls.Add(btnDynamic);
                x += Constants.MenuCardButtonWidthG;
                btnDynamic.Click += new EventHandler(this.DynamicButtonClick);
                if (Session.VegOnlySelected)
                {
                    if (btnDynamic.FlatAppearance.BorderColor == Session.nonVegColor)
                    {
                        btnDynamic.Visible = false;
                    }
                }
                else
                {
                    if (btnDynamic.FlatAppearance.BorderColor == Session.nonVegColor)
                    {
                        btnDynamic.Visible = true;
                    }
                }
            }

            if (CreateMoreButton)
            {
                Button btn = new Button();
                btn.Location = new System.Drawing.Point(x, y);
                btn.Size = new System.Drawing.Size(Constants.MenuCardButtonWidthG, Constants.ButtonHeightG);
                btn.Name = "btnMore";
                btn.Tag = PageNo;
                btn.Font = new Font(new FontFamily("Microsoft Sans Serif"), 8);
                btn.BackColor = SystemColors.Control;
                btn.Margin = new Padding(0);
                btn.TextAlign = ContentAlignment.BottomCenter;
                btn.TextImageRelation = TextImageRelation.ImageAboveText;
                //if (showBackButton)
                //{
                //    btn.Text = "Back";
                //    btn.BackgroundImage = Properties.Resources._32;
                //}
                //else
                //{
                btn.Text = "More";
                btn.BackgroundImage = Properties.Resources._33;
                //}

                btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
                flowLayoutPanelMenuItems.Controls.Add(btn);
                btn.Click += new EventHandler(this.MenuItemMoreButtonClick);
            }

            Session.currentMenuItemPageNo = PageNo;
        }

        private void MenuItemMoreButtonClick(object sender, EventArgs e)
        {
            try
            {
                flowLayoutPanelMenuItems.Visible = true;
                flowLayoutPanelMenuItems.BringToFront();
                flowLayoutPanelMenuItems.Controls.Clear();

                int CurrentPageNo = Convert.ToInt32(((Button)sender).Tag);
                int NextPageNo = 0;

                if (CurrentPageNo + 1 > Session.TotalPageMenuItems)
                    NextPageNo = 1;
                else
                    NextPageNo = CurrentPageNo + 1;

                PopulateMenuItems(NextPageNo);
            }
            catch (Exception ex)
            {
                Logger.Trace("ERROR", "frmOrder-menuitemmorebuttonclick(): " + ex.Message, ex, true);
            }
        }

        private Button MenuCategoryToClick(bool ClickonCurrentSelected, string MenuCategoryCode = "")
        {
            Button button = null;
            if (ClickonCurrentSelected && (!String.IsNullOrEmpty(MenuCategoryCode) || !String.IsNullOrEmpty(Session.currentMenuCategoryCode)))
            {
                if (MenuCategoryCode == "")
                {
                    Control[] ctrls = flowLayoutPanelMenuCatagories.Controls.Find(Session.currentMenuCategoryCode, true);
                    if(ctrls != null && ctrls.Length > 0)
                        button = (Button)ctrls[0];
                }
                    
                else
                    button = (Button)flowLayoutPanelMenuCatagories.Controls.Find(MenuCategoryCode, true)[0];
            }
            else
            {
                CatalogMenuCategory catalogMenuCategory = null;

                if (Session.menuCategories != null && Session.menuCategories.Count > 0)
                {
                    catalogMenuCategory = Session.menuCategories.Find(x => x.Default_Category == 1);

                    if (catalogMenuCategory == null && !String.IsNullOrEmpty(Session.currentMenuCategoryCode))
                    {
                        catalogMenuCategory = Session.menuCategories.Find(x => x.Menu_Category_Code == Session.currentMenuCategoryCode);
                    }

                    if (catalogMenuCategory == null)
                    {
                        catalogMenuCategory = Session.menuCategories[1];
                    }
                }

                if (flowLayoutPanelMenuCatagories.Controls != null && flowLayoutPanelMenuCatagories.Controls.Count > 0)
                {
                    if (catalogMenuCategory == null)
                    {
                        button = (Button)flowLayoutPanelMenuCatagories.Controls[1];
                    }
                    else
                    {
                        Control[] ctrls = flowLayoutPanelMenuCatagories.Controls.Find(catalogMenuCategory.Menu_Category_Code, true);
                        if (ctrls != null && ctrls.Length > 0)
                            button = (Button)ctrls[0];
                    }

                }
            }

            return button;
        }

        private void btn_Coupons_Click(object sender, EventArgs e)
        {
            try
            {
                if (selectedLineNumber > -1)
                {
                    //blnAfterTheFactCoupons = True
                    HandleOrderLineCoupons();
                }
            }
            catch (Exception ex)
            {
                Logger.Trace("ERROR", "frmOrder-btn_coupons_click(): " + ex.Message, ex, true);
            }
        }

        private void HandleOrderLineCoupons()
        {
            CartItem cartItem = Session.cart.cartItems.Find(x => x.Line_Number == selectedLineNumber);

            if (cartItem == null) return;

            lblWhere.Text = cartItem.Menu_Description + " - " + APILayer.GetCatalogText(LanguageConstant.cintCoupons);

            if (Session.ProcessingCombo)
            {
                ItemCombo itemCombo = Session.cart.itemCombos.Find(x => x.Combo_Group == Session.CurrentComboGroup);

                if (itemCombo == null) return;

                Menu_Item_Coupons(itemCombo.Combo_Menu_Code, itemCombo.Combo_Size_Code);
            }
            else
            {
                if (cartItem == null) return;

                Menu_Item_Coupons(cartItem.Menu_Code, cartItem.Size_Code);
            }
        }

        private void Menu_Item_Coupons(string Menu_code, string size_code)
        {
            string currentCouponCode = String.Empty;
            string menu_code_coupon = String.Empty;

            currentCouponCode = GetCurrentCouponCode();

            List<CatalogCoupons> currentCatalogCoupons = CouponFunctions.GetCouponsForCurrentCartItems(Menu_code, size_code);

            if (currentCatalogCoupons != null && currentCatalogCoupons.Count > 0)
            {
                foreach (CatalogCoupons _catalogCoupons in currentCatalogCoupons)
                {
                    if (!Session.orderLineCoupons.Any(z => z.Coupon_Code == _catalogCoupons.Coupon_Code)) Session.orderLineCoupons.Add(_catalogCoupons);
                }

                flowLayoutPanelMenuItems.Visible = true;
                flowLayoutPanelMenuItems.BringToFront();
                flowLayoutPanelMenuItems.Controls.Clear();
                int x = Constants.HorizontalSpace, y = Constants.VerticalSpace;

                pnl_Quantity.Visible = false;
                pnl_Quantity.SendToBack();
                tlpToppings.Visible = false;
                tlpToppings.SendToBack();
                flowLayoutPanelSpecialtyPizzas.Visible = false;
                flowLayoutPanelSpecialtyPizzas.SendToBack();
                Session.currentToppingCollection = null;
                uC_Customer_order_Header1.pnl_MinMax.Visible = false;
                uC_Customer_order_Header1.lblSelectedValue.Text = "0";

                CatalogCoupons catalogCouponsCross = new CatalogCoupons();
                catalogCouponsCross.Coupon_Code = "D";
                catalogCouponsCross.Description = "None";
                currentCatalogCoupons.Insert(0, catalogCouponsCross);

                foreach (var item in currentCatalogCoupons)
                {
                    Button btnDynamic = new Button();

                    btnDynamic.Location = new System.Drawing.Point(x, y);
                    btnDynamic.Size = new System.Drawing.Size(Constants.MenuCardButtonWidthG, Constants.ButtonHeightG);
                    btnDynamic.Name = item.Coupon_Code;
                    btnDynamic.Tag = "OrderLineCoupons";
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

                    if (item.Coupon_Code == currentCouponCode)
                        btnDynamic.BackColor = Session.DefaultEntityColor;


                    flowLayoutPanelMenuItems.Controls.Add(btnDynamic);

                    x += Constants.MenuCardButtonWidthG;
                    btnDynamic.Click += new EventHandler(this.DynamicButtonClick);
                }
            }
        }

        private string GetCurrentCouponCode()
        {
            if (Session.ProcessingCombo)
            {
                ItemCombo itemCombo = Session.cart.itemCombos.Find(x => x.Combo_Group == Session.CurrentComboGroup);

                if (itemCombo == null) return "";

                return itemCombo.Coupon_Code == null ? "" : itemCombo.Coupon_Code;
            }
            else
            {
                CartItem cartItem = Session.cart.cartItems.Find(x => x.Line_Number == selectedLineNumber);

                if (cartItem == null) return "";

                return cartItem.Coupon_Code == null ? "" : cartItem.Coupon_Code;
            }
        }

        private void HiglightMenuCategory(string menuCategoryCode)
        {
            if (!String.IsNullOrEmpty(menuCategoryCode))
            {
                Button btnDynamic = new Button();
                btnDynamic = (Button)flowLayoutPanelMenuCatagories.Controls.Find(menuCategoryCode, true)[0];
                BtnDynamic_LostFocus(btnDynamic, new EventArgs());
                btnDynamic.BackColor = Session.paytm_lightcolor;
            }
        }

        private void UpdateCartByMenutype()
        {

            if (Session.cartToppings != null && Session.cartToppings.Count > 0)
            {

                bool NonVegToppingExist = Session.cartToppings.Exists(x => x.MenuItemType == true);

                if (Session.currentToppingCollection != null) NonVegToppingExist = NonVegToppingExist || Session.currentToppingCollection.MenuItemType1stHalf || Session.currentToppingCollection.MenuItemType2ndHalf;

                if (NonVegToppingExist)
                {
                    try
                    {
                        string description = "";
                        if (Session.ProcessingCombo && Session.CurrentComboItem > 0)
                            description = Session.cart.cartItems.Find(x => x.Menu_Code == Session.selectedComboMenuItem.Menu_Code && x.Description.Contains(Session.selectedComboMenuItemSizes.Description)).Description;
                        else
                            description = Session.cart.cartItems.Find(x => x.Menu_Code == Session.selectedMenuItem.Menu_Code && x.Description.Contains(Session.selectedMenuItemSizes.Description)).Description;

                        UpdateCartItemBasedOnToppingRequest request = new UpdateCartItemBasedOnToppingRequest();
                        request.CartId = Session.cart.cartHeader.CartId;
                        request.Menu_Code = (Session.ProcessingCombo && Session.CurrentComboItem > 0) ? Session.selectedComboMenuItem.Menu_Code : Session.selectedMenuItem.Menu_Code;
                        request.Description = description;
                        request.MenuItemType = NonVegToppingExist;
                        request.LineNumber = Session.SelectedLineNumber;
                        APILayer.UpdateCartItemBasedOnTopping(request);

                        CartFunctions.GetCart("MenuCategories", "", ref selectedLineNumber);
                    }
                    catch (Exception ex)
                    {
                        Logger.Trace("ERROR", "frmOrder-updatecartbymenutype_1(): " + ex.Message, ex, true);
                    }

                }
                else
                {
                    try
                    {
                        string description = "";
                        if (Session.ProcessingCombo && Session.CurrentComboItem > 0)
                            description = Session.cart.cartItems.Find(x => x.Menu_Code == Session.selectedComboMenuItem.Menu_Code && x.Description.Contains(Session.selectedComboMenuItemSizes.Description)).Description;
                        else
                            description = Session.cart.cartItems.Find(x => x.Menu_Code == Session.selectedMenuItem.Menu_Code && x.Description.Contains(Session.selectedMenuItemSizes.Description)).Description;

                        UpdateCartItemBasedOnToppingRequest request = new UpdateCartItemBasedOnToppingRequest();
                        request.CartId = Session.cart.cartHeader.CartId;
                        request.Menu_Code = (Session.ProcessingCombo && Session.CurrentComboItem > 0) ? Session.selectedComboMenuItem.Menu_Code : Session.selectedMenuItem.Menu_Code;
                        request.Description = description;
                        request.MenuItemType = Convert.ToBoolean(Session.selectedMenuItem.MenuItemType) ? Convert.ToBoolean(Session.selectedMenuItem.MenuItemType) : false;
                        request.LineNumber = Session.SelectedLineNumber;
                        APILayer.UpdateCartItemBasedOnTopping(request);

                        CartFunctions.GetCart("MenuCategories", "", ref selectedLineNumber);
                    }
                    catch (Exception ex)
                    {
                        Logger.Trace("ERROR", "frmOrder-updatecartbymenutype_2(): " + ex.Message, ex, true);
                    }
                }
            }
            else
            {
                try
                {
                    string description = "";
                    if (Session.ProcessingCombo && Session.CurrentComboItem > 0)
                        description = Session.cart.cartItems.Find(x => x.Menu_Code == Session.selectedComboMenuItem.Menu_Code && x.Description.Contains(Session.selectedComboMenuItemSizes.Description)).Description;
                    else
                        description = Session.cart.cartItems.Find(x => x.Menu_Code == Session.selectedMenuItem.Menu_Code && x.Description.Contains(Session.selectedMenuItemSizes.Description)).Description;

                    UpdateCartItemBasedOnToppingRequest request = new UpdateCartItemBasedOnToppingRequest();
                    request.CartId = Session.cart.cartHeader.CartId;
                    request.Menu_Code = (Session.ProcessingCombo && Session.CurrentComboItem > 0) ? Session.selectedComboMenuItem.Menu_Code : Session.selectedMenuItem.Menu_Code;
                    request.Description = description;
                    request.MenuItemType = Session.selectedMenuItem.MenuItemType;
                    request.LineNumber = Session.SelectedLineNumber;
                    APILayer.UpdateCartItemBasedOnTopping(request);

                    CartFunctions.GetCart("MenuCategories", "", ref selectedLineNumber);
                }
                catch (Exception ex)
                {
                    Logger.Trace("ERROR", "frmOrder-updatecartbymenutype_3(): " + ex.Message, ex, true);
                }
            }

        }


        private void txt_Quantity_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnQty_OK_Click(this, new EventArgs());
            }
        }

        private void ClickonCurrentSelectedItemCatagory(string MenuCategoryCode)
        {
            if (CartClicked)
            {
                Button button = MenuCategoryToClick(true, MenuCategoryCode);
                DynamicButtonClick(button, new EventArgs());
                CartClicked = false;
            }
        }

        private void SetSelectedToppingsCount()
        {
            int ToppingChosen = 0;
            foreach (Control control in flowLayoutPanelToppings.Controls)
            {
                if (control.BackColor == Session.ToppingSizeLightColor)
                    ToppingChosen = ToppingChosen + 1;
                else if (control.BackColor == Session.ToppingSizeSingleColor)
                    ToppingChosen = ToppingChosen + 1;
                else if (control.BackColor == Session.ToppingSizeExtraColor)
                    ToppingChosen = ToppingChosen + 2;
                else if (control.BackColor == Session.ToppingSizeDoubleColor)
                    ToppingChosen = ToppingChosen + 2;
                else if (control.BackColor == Session.ToppingSizeTripleColor)
                    ToppingChosen = ToppingChosen + 3;
                else if (control.BackColor == Session.ToppingColor)
                {
                    if (control.BackgroundImage != null)
                        ToppingChosen = ToppingChosen - 1;
                }
            }

            uC_Customer_order_Header1.lblSelectedValue.Text = Convert.ToString(ToppingChosen);
        }

        public void SelectOrderType()
        {
            UserFunctions.AutoSelectOrderType(uC_CustomerOrderBottomMenu1);
        }

        private void frmOrder_Activated(object sender, EventArgs e)
        {
            if (Session.RefreshFromModifyForOrder || Session.RefreshFromHistoryForOrder || Session.RefreshFromRemakeForOrder)
            {
                UserFunctions.AutoSelectOrderType(uC_CustomerOrderBottomMenu1);

                if (Session.pblnModifyingOrder)
                {
                    OrderFunctions.LoadOrderDisplayOrderScreen(uC_Customer_OrderMenu, false, true, 0, 0);
                    btn_Minus.Enabled = Session.OrderReqField.btn_Minus;
                    btn_Plus.Enabled = Session.OrderReqField.btn_Plus;
                    btn_Instructions.Enabled = Session.OrderReqField.btn_Instructions;
                    btn_Up.Enabled = Session.OrderReqField.btn_Up;
                    btn_Down.Enabled = Session.OrderReqField.btn_Down;
                    btn_Coupons.Enabled = Session.OrderReqField.btn_Coupons;
                    btn_Quantity.Enabled = Session.OrderReqField.btn_Quantity;
                }

                CheckTrainningMode();


                Session.RefreshFromModifyForOrder = false;
                Session.RefreshFromHistoryForOrder = false;
                Session.RefreshFromRemakeForOrder = false;
            }
            else
            {
                if ((UserFunctions.GetCurrentSelectedOrderTypeOnForm(uC_CustomerOrderBottomMenu1) != Session.selectedOrderType) || Session.RemakeOrder)
                    UserFunctions.AutoSelectOrderType(uC_CustomerOrderBottomMenu1);

            }

            uC_Customer_OrderMenu.HandleHistoryButton(Session.handleHistorybutton);
            uC_Customer_OrderMenu.HandleRemakeButton(Session.handleRemakebutton);

            CartFunctions.FillCustomerToCart(ref Session.cart);
        }

        public void UpdateUseronHeader()
        {
            uC_Customer_order_Header1.LoadlabelText();
            uC_Customer_OrderMenu.SetAllbuttonText();
        }

        public void CloseToppings()
        {
            Button btnDynamic1 = MenuCategoryToClick(true);
            DynamicButtonClick(btnDynamic1, new EventArgs());
            //pnl_Quantity.Visible = false;
            //pnl_Quantity.SendToBack();
            //tlpToppings.Visible = false;
            //tlpToppings.SendToBack();
            //flowLayoutPanelSpecialtyPizzas.Visible = false;
            //flowLayoutPanelSpecialtyPizzas.SendToBack();
            //Session.currentToppingCollection = null;
            //uC_Customer_order_Header1.pnl_MinMax.Visible = false;
            //uC_Customer_order_Header1.lblSelectedValue.Text = "0";
        }

        private void SpecialtyItemsClick(object sender, EventArgs e)
        {
            try
            {
                bool blnSelected;
                int intCount;
                Color DefaultColor = SystemColors.Control;
                Button btnSpecialtyPizza = (Button)sender;

                if (Session.SpecialtyItems == null)
                    Session.SpecialtyItems = new UserTypes.SpecialtyItems();

                if (btnSpecialtyPizza.BackColor == DefaultColor)
                {
                    blnSelected = true;

                    //Deselected all of the buttons
                    foreach (Control control in flowLayoutPanelSpecialtyPizzas.Controls)
                        control.BackColor = DefaultBackColor;

                    btnSpecialtyPizza.BackColor = Session.DefaultEntityColor;

                    if (Session.currentItemPart == UserTypes.ItemParts.Whole)
                        Session.SpecialtyItems.SpecialtyCodeWhole = Convert.ToString(btnSpecialtyPizza.Name);
                    else if (Session.currentItemPart == UserTypes.ItemParts.FirstHalf)
                        Session.SpecialtyItems.SpecialtyCode1stHalf = Convert.ToString(btnSpecialtyPizza.Name);
                    else
                        Session.SpecialtyItems.SpecialtyCode2ndHalf = Convert.ToString(btnSpecialtyPizza.Name);

                }
                else
                {
                    blnSelected = false;

                    btnSpecialtyPizza.BackColor = DefaultBackColor;

                    if (Session.currentItemPart == UserTypes.ItemParts.Whole)
                        Session.SpecialtyItems.SpecialtyCodeWhole = "";
                    else if (Session.currentItemPart == UserTypes.ItemParts.Whole)
                        Session.SpecialtyItems.SpecialtyCode1stHalf = "";
                    else
                        Session.SpecialtyItems.SpecialtyCode2ndHalf = "";
                }

                CartFunctions.SpecialtyItemChosen(btnSpecialtyPizza.Text, Convert.ToString(btnSpecialtyPizza.Name), blnSelected, (Session.currentItemPart == UserTypes.ItemParts.FirstHalf ? "1" : (Session.currentItemPart == UserTypes.ItemParts.SecondHalf ? "2" : "W")), selectedLineNumber);

                //UpdateCartByMenutype(); //TO DO
                RefreshCartUI();
                CartControl();

                //SetSelectedToppingsCount(); //TO DO

                BtnSpecOk_Click(null, new EventArgs());

            }
            catch (Exception ex)
            {
                Logger.Trace("ERROR", "frmOrder-specialtyitemsclick(): " + ex.Message, ex, true);
            }
        }

        private void FillMenuTypesFromHalf()
        {
            selectedLineNumber = UserFunctions.ActualLineNumber(Session.cart, selectedLineNumber);

            CartItem currentCartItem = Session.cart.cartItems.Find(x => x.Line_Number == (selectedLineNumber > 0 ? selectedLineNumber : x.Line_Number));

            if (currentCartItem != null)
            {
                if (currentCartItem.Specialty_Pizza)
                {
                    Session.currentToppingCollection.MenuItemType1stHalf = false;
                    Session.currentToppingCollection.MenuItemType2ndHalf = false;
                }
                else
                {
                    CatalogSpecialtyPizzas SpecialtyPizza1stHalf = null;
                    CatalogSpecialtyPizzas SpecialtyPizza2ndHalf = null;

                    if (currentCartItem.itemSpecialtyPizzas != null && currentCartItem.itemSpecialtyPizzas.Count > 0)
                    {
                        string PizzaCode1stHalf = String.Empty;
                        string PizzaCode2ndHalf = String.Empty;

                        ItemSpecialtyPizza itemSpeciality = currentCartItem.itemSpecialtyPizzas.Find(x => x.Pizza_Part == "1");
                        PizzaCode1stHalf = itemSpeciality != null ? itemSpeciality.Menu_Code : String.Empty;
                        if (!String.IsNullOrEmpty(PizzaCode1stHalf))
                            SpecialtyPizza1stHalf = Session.SpecialtyPizzasList.Find(x => x.Menu_Code == PizzaCode1stHalf);

                        itemSpeciality = null;
                        itemSpeciality = currentCartItem.itemSpecialtyPizzas.Find(x => x.Pizza_Part == "2");
                        PizzaCode2ndHalf = itemSpeciality != null ? itemSpeciality.Menu_Code : String.Empty;
                        if (!String.IsNullOrEmpty(PizzaCode2ndHalf))
                            SpecialtyPizza2ndHalf = Session.SpecialtyPizzasList.Find(x => x.Menu_Code == PizzaCode2ndHalf);
                    }

                    if (SpecialtyPizza1stHalf != null) Session.currentToppingCollection.MenuItemType1stHalf = Convert.ToBoolean(SpecialtyPizza1stHalf.MenuItemType);
                    if (SpecialtyPizza2ndHalf != null) Session.currentToppingCollection.MenuItemType2ndHalf = Convert.ToBoolean(SpecialtyPizza2ndHalf.MenuItemType);
                }
            }
        }

        private void PopulateMenuCategories(int PageNo, ref string DefaultMenuCategoryCode)
        {
            int x = Constants.HorizontalSpace, y = Constants.VerticalSpace;
            int startIndex = 0;
            int LastIndex = 0;
            bool CreateMoreButton = false;
            //bool showBackButton = false;   
            int MaxCategoriesOnOnepage = ((flowLayoutPanelMenuCatagories.Width - Constants.HorizontalSpace) / Constants.MenuCardButtonWidthG) * 2;


            //List<CatalogMenuCategory> catalogMenuCategories = Session.menuCategories;
            List<CatalogMenuCategory> catalogMenuCategories;
            if (string.IsNullOrEmpty(Session.SelectedBusinessUnit))
                catalogMenuCategories = Session.menuCategories;
            else
                catalogMenuCategories = Session.menuCategories.FindAll(z => z.BusinessUnit == Session.SelectedBusinessUnit);

            if (flowLayoutPanelMenuCatagories.Size.Height > 59)
            {
                if (MaxCategoriesOnOnepage < catalogMenuCategories.Count)
                    CreateMoreButton = true;

                if (PageNo == 1)
                {
                    startIndex = 0;
                    if (CreateMoreButton)
                        LastIndex = MaxCategoriesOnOnepage - 1;
                    else
                        LastIndex = catalogMenuCategories.Count;
                }
                else
                {
                    startIndex = (MaxCategoriesOnOnepage - 1) * (PageNo - 1);
                    LastIndex = (catalogMenuCategories.Count - startIndex) > (MaxCategoriesOnOnepage - 1) ? ((MaxCategoriesOnOnepage - 1) * (PageNo - 1)) + (MaxCategoriesOnOnepage - 1) : catalogMenuCategories.Count;
                }
            }
            else
            {
                CreateMoreButton = false;
                startIndex = 0;
                LastIndex = catalogMenuCategories.Count;
            }

            for (int i = startIndex; i < LastIndex; i++)
            {
                Button btnDynamic = new Button();

                btnDynamic.Location = new System.Drawing.Point(x, y);
                btnDynamic.Size = new System.Drawing.Size(Constants.MenuCardButtonWidthG, Constants.ButtonHeightG);

                btnDynamic.Name = catalogMenuCategories[i].Menu_Category_Code.Trim();
                btnDynamic.Tag = "MenuCategories";
                btnDynamic.Text = catalogMenuCategories[i].Order_Description.ToUpper().Trim();
                btnDynamic.Font = new Font(new FontFamily("Microsoft Sans Serif"), 7);
                btnDynamic.BackColor = SystemColors.Control;
                btnDynamic.Margin = new Padding(0);
                btnDynamic.TextAlign = ContentAlignment.MiddleCenter;
                if (catalogMenuCategories[i].Default_Category == 1)
                {
                    DefaultMenuCategoryCode = catalogMenuCategories[i].Menu_Category_Code;
                    btnDynamic.BackColor = Session.paytm_lightcolor;
                }

                flowLayoutPanelMenuCatagories.Controls.Add(btnDynamic);
                btnDynamic.Click += new EventHandler(this.DynamicButtonClick);

                x += Constants.MenuCardButtonWidthG;
            }

            if (CreateMoreButton)
            {
                Button btn = new Button();
                btn.Location = new System.Drawing.Point(x, y);
                btn.Size = new System.Drawing.Size(Constants.MenuCardButtonWidthG, Constants.ButtonHeightG);
                btn.Name = "btnMore";
                btn.Tag = PageNo;
                btn.Font = new Font(new FontFamily("Microsoft Sans Serif"), 8);
                btn.BackColor = SystemColors.Control;
                btn.Margin = new Padding(0);
                btn.TextAlign = ContentAlignment.BottomCenter;
                btn.TextImageRelation = TextImageRelation.ImageAboveText;
                btn.Text = "More";
                btn.BackgroundImage = Properties.Resources._33;

                btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
                flowLayoutPanelMenuCatagories.Controls.Add(btn);
                btn.Click += MenuCategoryMoreButtonClick;
            }

        }

        private void MenuCategoryMoreButtonClick(object sender, EventArgs e)
        {
            flowLayoutPanelMenuCatagories.Controls.Clear();

            string defaultCatg = String.Empty;
            int CurrentPageNo = Convert.ToInt32(((Button)sender).Tag);
            int NextPageNo = 0;

            if (CurrentPageNo + 1 > Session.TotalPageMenuCategory)
                NextPageNo = 1;
            else
                NextPageNo = CurrentPageNo + 1;

            PopulateMenuCategories(NextPageNo, ref defaultCatg);
        }

        private void DyanmicBusinessUnit()
        {
            if (Session.businessUnits == null || Session.businessUnits.Count <= 1) return;

            flowLayoutPanelBUnit.Controls.Clear();
            flowLayoutPanelMenuCatagories.Controls.Clear();
            flowLayoutPanelMenuItems.Controls.Clear();
            int x = Constants.HorizontalSpace, y = Constants.VerticalSpace;
            string DefaultBusinessUnit = "";

            PopulateBussinessUnit(1, ref DefaultBusinessUnit);

            Button btnDynamic1 = new Button();
            if (!String.IsNullOrEmpty(DefaultBusinessUnit))
            {
                foreach (Control ctl in flowLayoutPanelBUnit.Controls)
                {
                    if (ctl.Name == DefaultBusinessUnit)
                    {
                        btnDynamic1 = (Button)ctl;
                        break;
                    }
                }
            }
            else
            {
                btnDynamic1 = (Button)flowLayoutPanelBUnit.Controls[0];
            }
            BusinessUnit_Click(btnDynamic1, new EventArgs());

            dgvCart.ClearSelection();
        }

        private void PopulateBussinessUnit(int PageNo, ref string DefaultBusinessUnit)
        {
            int x = Constants.HorizontalSpace, y = Constants.VerticalSpace;
            int startIndex = 0;
            int LastIndex = 0;
            bool CreateMoreButton = false;
            //bool showBackButton = false;   
            int MaxCategoriesOnOnepage = ((flowLayoutPanelMenuCatagories.Width - Constants.HorizontalSpace) / Constants.MenuCardButtonWidthG) * 2;


            List<CatalogBusinessUnit> catalogBusinessUnit = Session.businessUnits;

            if (flowLayoutPanelBUnit.Size.Height > 59)
            {
                if (MaxCategoriesOnOnepage < catalogBusinessUnit.Count)
                    CreateMoreButton = true;

                if (PageNo == 1)
                {
                    startIndex = 0;
                    if (CreateMoreButton)
                        LastIndex = MaxCategoriesOnOnepage - 1;
                    else
                        LastIndex = catalogBusinessUnit.Count;
                }
                else
                {
                    startIndex = (MaxCategoriesOnOnepage - 1) * (PageNo - 1);
                    LastIndex = (catalogBusinessUnit.Count - startIndex) > (MaxCategoriesOnOnepage - 1) ? ((MaxCategoriesOnOnepage - 1) * (PageNo - 1)) + (MaxCategoriesOnOnepage - 1) : catalogBusinessUnit.Count;
                }
            }
            else
            {
                CreateMoreButton = false;
                startIndex = 0;
                LastIndex = catalogBusinessUnit.Count;
            }

            for (int i = startIndex; i < LastIndex; i++)
            {
                Button btnDynamic = new Button();

                btnDynamic.Location = new System.Drawing.Point(x, y);
                btnDynamic.Size = new System.Drawing.Size(Constants.MenuCardButtonWidthG, Constants.ButtonHeightG);

                btnDynamic.Name = catalogBusinessUnit[i].BU_Code.Trim();
                btnDynamic.Tag = "BussinessUnits";
                btnDynamic.Text = catalogBusinessUnit[i].Order_Description.ToUpper().Trim();
                btnDynamic.Font = new Font(new FontFamily("Microsoft Sans Serif"), 7);
                btnDynamic.BackColor = SystemColors.Control;
                btnDynamic.Margin = new Padding(0);
                btnDynamic.TextAlign = ContentAlignment.MiddleCenter;
                DefaultBusinessUnit = catalogBusinessUnit[i].IsDefault ? catalogBusinessUnit[i].BU_Code.Trim() : "";
                flowLayoutPanelBUnit.Controls.Add(btnDynamic);
                btnDynamic.Click += BusinessUnit_Click;

                x += Constants.MenuCardButtonWidthG;
            }

            if (CreateMoreButton)
            {
                Button btn = new Button();
                btn.Location = new System.Drawing.Point(x, y);
                btn.Size = new System.Drawing.Size(Constants.MenuCardButtonWidthG, Constants.ButtonHeightG);
                btn.Name = "btnMore";
                btn.Tag = PageNo;
                btn.Font = new Font(new FontFamily("Microsoft Sans Serif"), 8);
                btn.BackColor = SystemColors.Control;
                btn.Margin = new Padding(0);
                btn.TextAlign = ContentAlignment.BottomCenter;
                btn.TextImageRelation = TextImageRelation.ImageAboveText;
                btn.Text = "More";
                btn.BackgroundImage = Properties.Resources._33;

                btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
                flowLayoutPanelBUnit.Controls.Add(btn);
                btn.Click += MenuCategoryMoreButtonClick;
            }

        }

        private void BusinessUnit_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;

            foreach (Button btnVal in flowLayoutPanelBUnit.Controls.OfType<Button>())
            {
                btnVal.BackColor = SystemColors.Control;
            }

            btn.BackColor = Session.DefaultEntityColor;

            if (Convert.ToString(btn.Tag).Trim() == "BussinessUnits")
            {
                Session.SelectedBusinessUnit = btn.Name;

                DyanmicCatalogCategories();
            }
        }

        public void AddItem(string ParentMenu, string btnName)
        {
            try
            {
                pnl_Quantity.Visible = false;
                pnl_Quantity.SendToBack();
                tlpToppings.Visible = false;
                tlpToppings.SendToBack();
                flowLayoutPanelSpecialtyPizzas.Visible = false;
                flowLayoutPanelSpecialtyPizzas.SendToBack();
                Session.currentToppingCollection = null;

                string Menu_Category_Code = Session.AllCatalogMenuItems.Find(x => x.Menu_Code == btnName).Menu_Category_Code;

                string Existing_Menu_Category_Code = Session.menuItems[0].Menu_Category_Code;

                if (Existing_Menu_Category_Code != Menu_Category_Code)
                {

                    List<CatalogMenuItems> currentMenuItems = APILayer.GetMenuItems(Menu_Category_Code, Session.selectedOrderType);
                    CartFunctions.RemoveItemsOrderTypeFromMenuItems(Session.selectedOrderType);

                    foreach (CatalogMenuItems catalogMenuItems in currentMenuItems)
                    {
                        if (!Session.menuItems.Any(z => z.Menu_Code == catalogMenuItems.Menu_Code)) Session.menuItems.Add(catalogMenuItems);
                    }
                }

                ClickEventProcess(ParentMenu, btnName);

            }
            catch (Exception ex)
            {
                Logger.Trace("ERROR", "frmOrder-AddItem(): " + ex.Message, ex, true);
            }
        }

        private void ItemwiseUpsell()
        {
            try
            {

                string SelectedAttributeCode = string.Empty;
                ItemwiseUpsellDatawithType itemwiseUpsellDatawithType = new ItemwiseUpsellDatawithType();

                if (CartFunctions.IsItemwiseUpsellAvailable(selectedLineNumber, ref itemwiseUpsellDatawithType))
                {
                    if (itemwiseUpsellDatawithType == null) return;

                    if (itemwiseUpsellDatawithType.AttributeList.Count > 0)
                    {
                        HighlightSelectedLineNumber(itemwiseUpsellDatawithType.LineNumber);

                        bool response = false;
                        string msg = CartFunctions.GetMessageTextForUpsell(itemwiseUpsellDatawithType.UpsellType);

                        if (itemwiseUpsellDatawithType.AttributeList.Count == 1)
                        {
                            SelectedAttributeCode = itemwiseUpsellDatawithType.AttributeList[0].Code;

                            if (itemwiseUpsellDatawithType.AttributeList[0].IsEnabled)
                            {
                                Session.PreventItemwiseUpsell = true;
                                if (CustomMessageBox.Show(msg.Replace("<<" + itemwiseUpsellDatawithType.UpsellType + ">>", itemwiseUpsellDatawithType.AttributeList[0].Description), CustomMessageBox.Buttons.YesNo, CustomMessageBox.Icon.Question) == DialogResult.Yes)
                                {
                                    response = true;
                                    UpsellPromptYesActions(itemwiseUpsellDatawithType, itemwiseUpsellDatawithType.AttributeList[0].Code);
                                }
                                Session.PreventItemwiseUpsell = false;
                            }

                        }
                        else
                        {
                            if (itemwiseUpsellDatawithType.AttributeList.Count != itemwiseUpsellDatawithType.AttributeList.FindAll(z => z.IsEnabled == false).Count)
                            {
                                Session.PreventItemwiseUpsell = true;

                                frmUpsellPrompt ObjfrmUpsellPrompt = new frmUpsellPrompt(itemwiseUpsellDatawithType);
                                ObjfrmUpsellPrompt.ShowDialog();

                                Session.PreventItemwiseUpsell = false;

                                if (!String.IsNullOrEmpty(ObjfrmUpsellPrompt.AttribteCode))
                                {
                                    SelectedAttributeCode = ObjfrmUpsellPrompt.AttribteCode;
                                    response = true;
                                    UpsellPromptYesActions(itemwiseUpsellDatawithType, ObjfrmUpsellPrompt.AttribteCode);
                                }
                            }
                        }

                        CartFunctions.RecordUpsellPrompts(itemwiseUpsellDatawithType.MenuCode, itemwiseUpsellDatawithType.Priority, response ? "YES" : "NO", itemwiseUpsellDatawithType.LineNumber);
                        CartFunctions.LogItemwiseUpsellHistory(itemwiseUpsellDatawithType, response ? "YES" : "NO", SelectedAttributeCode);

                        if (itemwiseUpsellDatawithType.UpsellType == "NEWITEM" || itemwiseUpsellDatawithType.UpsellType == "CONVERTTOCOMBO")
                            ItemwiseUpsell();
                    }
                }
            }
            catch (Exception ex)
            {

            }

        }


        private void UpsellPromptYesActions(ItemwiseUpsellDatawithType itemwiseUpsellDatawithType, string AttributeCode)
        {
            Session.PreventItemwiseUpsell = true;
            switch (itemwiseUpsellDatawithType.UpsellType)
            {
                case "NEWITEM":
                    AddItem("MenuItems", AttributeCode);
                    break;
                case "SIZECHANGE":
                    ClickEventProcess("MenuItemSizes", AttributeCode);
                    break;
                case "ADDTOPPING":
                    AddTopping(itemwiseUpsellDatawithType.LineNumber, itemwiseUpsellDatawithType.MenuCode, AttributeCode, itemwiseUpsellDatawithType.AttributeList.Find(x => x.Code == AttributeCode).Description);
                    break;
                case "CONVERTTOCOMBO":
                    ConvertToCombo(itemwiseUpsellDatawithType.LineNumber, itemwiseUpsellDatawithType.MenuCode, AttributeCode);
                    break;
            }
            Session.PreventItemwiseUpsell = false;
        }

        private void AddTopping(int LineNumber, string MenuCode, string ToppingCode, string ToppingDescription)
        {
            int rowIndex = -1;
            DataTable dt = (DataTable)dgvCart.DataSource;

            DataRow[] drs =  dt.Select("Line_Number = " + LineNumber + " AND Menu_Code = '" + MenuCode + "' AND LineType = 'O'");

            if (drs != null && drs.Length > 0)
                rowIndex = dt.Rows.IndexOf(drs[0]);

            if(rowIndex > -1)
            {
                dgvCart_CellClick(dgvCart, new DataGridViewCellEventArgs(1, rowIndex));

                Button btnTopping = new Button();
                btnTopping.Tag = ToppingCode;
                btnTopping.Text = ToppingDescription;
                ToppingsButtonClick(btnTopping);
                CloseToppingView();
            }

        }

        private void ConvertToCombo(int LineNumber, string MenuCode, string ComboMenuCode)
        {
            selectedLineNumber = LineNumber;
            QuantityChange(false, -1);
            AddItem("MenuItems", ComboMenuCode);
        } 
        
        private void HighlightSelectedLineNumber(int LineNumber)
        {
            try
            {
                if (dgvCart != null && dgvCart.Rows.Count > 0)
                {
                    foreach (DataGridViewRow row in dgvCart.Rows)
                    {
                        if (Convert.ToInt32(Convert.ToString(row.Cells["Line_Number"].Value)) == LineNumber)
                        {
                            row.Selected = true;
                            break;
                        }
                    }
                }
            }
            catch(Exception ex)
            {

            }
            
        }

    }
}


