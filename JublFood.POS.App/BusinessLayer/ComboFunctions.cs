using Jublfood.AppLogger;
using JublFood.POS.App.API;
using JublFood.POS.App.Cache;
using JublFood.POS.App.Class;
using JublFood.POS.App.Models.Cart;
using JublFood.POS.App.Models.Catalog;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace JublFood.POS.App.BusinessLayer
{
    public static class ComboFunctions
    {
        public static bool IsComboMealSelected(string ParentMenu)
        {
            if (ParentMenu == "MenuItemSizes")
            {
                if (Session.selectedMenuItem != null && Convert.ToBoolean(Session.selectedMenuItem.Combo_Meal) && Session.ProcessingCombo && Session.CurrentComboItem == 0)
                    return true;
            }

            return false;
        }

        public static void AddCombotoCartUI(ref DataTable dtCart, Cart cart)
        {
            if (dtCart != null)
            {
                List<CartIndex> cartIndices = null;
                CartIndex cartIndex = null;
                if (cart != null && cart.itemCombos != null && cart.itemCombos.Count > 0)
                {
                    if (dtCart.Rows.Count == 0)
                    {
                        for (int i = 0; i <= cart.itemCombos.Count - 1; i++)
                        {
                            cartIndex = new CartIndex();
                            cartIndex.Index = i;
                            cartIndex.LineNumber = i + 1;
                            cartIndices = new List<CartIndex>();
                            cartIndices.Add(cartIndex);
                            InsertCombotoCartDataTable(cart.itemCombos[i], cartIndices, ref dtCart, cart);
                        }
                    }
                    else
                    {
                        cartIndices = GetComboIndices(dtCart);
                        if (cartIndices.Count > 0)
                        {
                            InsertCombotoCartDataTable(null, cartIndices, ref dtCart, cart);
                        }
                        else
                        {
                            for (int i = 0; i <= cart.itemCombos.Count - 1; i++)
                            {
                                cartIndex = new CartIndex();
                                cartIndex.Index = dtCart.Rows.Count;
                                cartIndex.LineNumber = Convert.ToInt32(dtCart.Rows[dtCart.Rows.Count - 1]["Line_Number"]) + 1;
                                cartIndices = new List<CartIndex>();
                                cartIndices.Add(cartIndex);
                                InsertCombotoCartDataTable(cart.itemCombos[i], cartIndices, ref dtCart, cart);
                            }
                        }
                    }
                }
            }
        }

        private static List<CartIndex> GetComboIndices(DataTable dtCart)
        {
            bool ComboParentFound = false;

            List<CartIndex> Indices = new List<CartIndex>();
            for (int i = dtCart.Rows.Count - 1; i >= 0; i--)
            {
                if (Convert.ToInt32(dtCart.Rows[i]["Combo_Group"]) > 0 && (Indices.FindIndex(x => x.Combo_Group == Convert.ToInt32(dtCart.Rows[i]["Combo_Group"])) < 0))
                {
                    ComboParentFound = false;
                    for (int j = i; j >= 0; j--)
                    {
                        if (Convert.ToInt32(dtCart.Rows[i]["Combo_Group"]) != Convert.ToInt32(dtCart.Rows[j]["Combo_Group"]) && (Indices.FindIndex(x => x.Combo_Group == Convert.ToInt32(dtCart.Rows[i]["Combo_Group"])) < 0))
                        {
                            CartIndex cartIndex = new CartIndex();
                            cartIndex.Index = j + 1;
                            cartIndex.LineNumber = Convert.ToInt32(dtCart.Rows[j + 1]["Line_Number"]);
                            cartIndex.Combo_Group = Convert.ToInt32(dtCart.Rows[i]["Combo_Group"]);
                            if (Indices.FindIndex(x => x.Combo_Group == cartIndex.Combo_Group) < 0) Indices.Add(cartIndex);
                            ComboParentFound = true;
                        }
                    }

                    if (!ComboParentFound)
                    {
                        CartIndex cartIndex = new CartIndex();
                        cartIndex.Index = 0;
                        cartIndex.LineNumber = 1;
                        cartIndex.Combo_Group = Convert.ToInt32(dtCart.Rows[i]["Combo_Group"]);
                        if (Indices.FindIndex(x => x.Combo_Group == cartIndex.Combo_Group) < 0) Indices.Add(cartIndex);
                    }
                }
            }

            return Indices.OrderBy(x => x.Index).ToList();
        }

        private static void InsertCombotoCartDataTable(ItemCombo itemCombo, List<CartIndex> Indices, ref DataTable dtCart, Cart cart)
        {
            if (itemCombo == null)
            {
                for (int i = Indices.Count - 1; i >= 0; i--)
                {
                    itemCombo = cart.itemCombos.Find(x => x.Combo_Group == Indices[i].Combo_Group);

                    DataRow dr = dtCart.NewRow();
                    dr["CartId"] = itemCombo.CartId;
                    dr["Line_Number"] = Indices[i].LineNumber;
                    dr["Menu_Category_Code"] = itemCombo.Combo_Menu_Category_Code;
                    dr["Menu_Code"] = itemCombo.Combo_Menu_Code;
                    dr["Size_Code"] = itemCombo.Combo_Size_Code;
                    dr["Qty"] = itemCombo.Combo_Quantity;
                    dr["Item"] = itemCombo.Combo_Description;
                    dr["VegNVegColor"] = null;
                    dr["Price"] = String.Format(Session.DisplayFormat, itemCombo.Combo_Price);
                    dr["LineType"] = "B";
                    dr["Combo_Group"] = itemCombo.Combo_Group;
                    dr["Combo_Item_Number"] = 0;

                    dtCart.Rows.InsertAt(dr, Indices[i].Index);
                }
            }
            else
            {
                for (int i = Indices.Count - 1; i >= 0; i--)
                {
                    DataRow dr = dtCart.NewRow();
                    dr["CartId"] = itemCombo.CartId;
                    dr["Line_Number"] = Indices[i].LineNumber;
                    dr["Menu_Category_Code"] = itemCombo.Combo_Menu_Category_Code;
                    dr["Menu_Code"] = itemCombo.Combo_Menu_Code;
                    dr["Size_Code"] = itemCombo.Combo_Size_Code;
                    dr["Qty"] = itemCombo.Combo_Quantity;
                    dr["Item"] = itemCombo.Combo_Description;
                    dr["VegNVegColor"] = null;
                    dr["Price"] = String.Format(Session.DisplayFormat, itemCombo.Combo_Price);
                    dr["LineType"] = "B";
                    dr["Combo_Group"] = itemCombo.Combo_Group;
                    dr["Combo_Item_Number"] = 0;

                    dtCart.Rows.InsertAt(dr, Indices[i].Index);
                }
            }
        }

        public static void GetComboCartSizeChange(string btnName)
        {
            ItemCombo itemCombo = new ItemCombo();
            itemCombo = Session.cart.itemCombos.Find(x => x.Combo_Group == Session.CurrentComboGroup);

            if (itemCombo.Combo_Size_Code == btnName)
                return;

            CartFunctions.CheckCart();
            Cart cartLocal = (new Cart().GetCart());

            if (itemCombo.Combo_Size_Code != "")
            {
                foreach (CartItem cartItem in Session.cart.cartItems)
                {
                    if (cartItem.Combo_Group == itemCombo.Combo_Group)
                    {
                        cartItem.Action = "D";
                        cartLocal.cartItems.Add(cartItem);
                    }
                }
            }

            Session.selectedMenuItemSizes = Session.menuItemSizes.Find(x => x.Size_Code == btnName && x.Menu_Code == Session.selectedMenuItem.Menu_Code);

            itemCombo.Combo_Size_Code = Session.selectedMenuItemSizes.Size_Code;
            itemCombo.Combo_Size_Description = Session.selectedMenuItemSizes.Description;
            itemCombo.Combo_Price = Convert.ToDecimal(Session.selectedMenuItemSizes.Price);
            itemCombo.Action = "M";

            cartLocal.itemCombos.Add(itemCombo);

            cartLocal.Customer = Session.cart.Customer;

            cartLocal.cartHeader.CartId = Session.cart.cartHeader.CartId;
            if (Session.cart.cartHeader.ctlAddressCity != cartLocal.Customer.City.Trim())
            {
                cartLocal.cartHeader = Session.cart.cartHeader;
                cartLocal.cartHeader.Action = "M";
                cartLocal.cartHeader.ctlAddressCity = cartLocal.Customer.City.Trim();
            }
            CartFunctions.UpdateCustomer(cartLocal);
            Session.cart = APILayer.Add2Cart(cartLocal);
        }

        public static void RemoveInvalidComboFromCart(ItemCombo itemCombo)
        {
            CartFunctions.CheckCart();
            Cart cartLocal = (new Cart().GetCart());

            itemCombo.Action = "D";
            cartLocal.itemCombos.Add(itemCombo);

            cartLocal.Customer = Session.cart.Customer;

            cartLocal.cartHeader.CartId = Session.cart.cartHeader.CartId;
            if (Session.cart.cartHeader.ctlAddressCity != cartLocal.Customer.City.Trim())
            {
                cartLocal.cartHeader = Session.cart.cartHeader;
                cartLocal.cartHeader.Action = "M";
                cartLocal.cartHeader.ctlAddressCity = cartLocal.Customer.City.Trim();
            }
            CartFunctions.UpdateCustomer(cartLocal);
            Session.cart = APILayer.Add2Cart(cartLocal);
        }

        public static void AddComboSizeAndItemsToCart(ItemCombo itemCombo, List<CatalogPOSComboMealItems> catalogPOSComboMealItems, List<CatalogPOSComboMealItemSizes> catalogPOSComboMealItemSizes)
        {
            decimal curComboPrice = 0;
            CartFunctions.CheckCart();
            Cart cartLocal = (new Cart().GetCart());

            itemCombo.Number_Of_Combo_Items = 0;

            foreach (CatalogPOSComboMealItems _catalogPOSComboMealItems in catalogPOSComboMealItems)
            {
                if (_catalogPOSComboMealItems.Default)
                {
                    foreach (CatalogPOSComboMealItemSizes _catalogPOSComboMealItemSizes in catalogPOSComboMealItemSizes)
                    {
                        if (_catalogPOSComboMealItemSizes.Default && _catalogPOSComboMealItemSizes.Menu_Code == _catalogPOSComboMealItems.Menu_Code
                         && _catalogPOSComboMealItemSizes.Item_Number == _catalogPOSComboMealItems.Item_Number)
                        {
                            CartItem cartItemLocal = new CartItem();

                            cartItemLocal.Location_Code = itemCombo.Location_Code;
                            cartItemLocal.Order_Date = itemCombo.Order_Date;
                            cartItemLocal.Order_Number = itemCombo.Order_Number;
                            cartItemLocal.Line_Number = 1;
                            cartItemLocal.Sequence = 1;
                            cartItemLocal.Combo_Group = Session.CurrentComboGroup;
                            cartItemLocal.Combo_Menu_Code = itemCombo.Combo_Menu_Code;
                            cartItemLocal.Combo_Size_Code = itemCombo.Combo_Size_Code;
                            cartItemLocal.Menu_Type_ID = Session._MenuTypeID;
                            cartItemLocal.Menu_Code = _catalogPOSComboMealItems.Menu_Code;
                            cartItemLocal.Size_Code = _catalogPOSComboMealItemSizes.Size_Code;
                            cartItemLocal.Bottle_Deposit = _catalogPOSComboMealItemSizes.Bottle_Deposit;
                            cartItemLocal.Menu_Category_Code = itemCombo.Combo_Menu_Category_Code;
                            cartItemLocal.Menu_Description = _catalogPOSComboMealItems.Order_Description;
                            cartItemLocal.Kitchen_Device_Count = _catalogPOSComboMealItems.Kitchen_Device_Count;
                            cartItemLocal.Menu_Item_Taxable = _catalogPOSComboMealItems.Taxable;
                            cartItemLocal.Menu_Price = _catalogPOSComboMealItemSizes.Price;
                            cartItemLocal.Menu_Price2 = _catalogPOSComboMealItemSizes.Price2;
                            cartItemLocal.Price = (_catalogPOSComboMealItemSizes.Price - _catalogPOSComboMealItems.Discount) * Convert.ToDecimal(itemCombo.Combo_Quantity);
                            cartItemLocal.Combo_Discount = _catalogPOSComboMealItems.Discount;
                            cartItemLocal.Combo_MinAmount = _catalogPOSComboMealItems.MinAmount;
                            cartItemLocal.Combo_MaxAmount = _catalogPOSComboMealItems.MaxAmount;
                            cartItemLocal.Combo_Item_Number = _catalogPOSComboMealItems.Item_Number;
                            cartItemLocal.Prompt_For_Size = _catalogPOSComboMealItems.Prompt_For_Size;
                            cartItemLocal.Base_Price = cartItemLocal.Menu_Price;
                            cartItemLocal.Base_Price2 = cartItemLocal.Menu_Price2;
                            if (cartItemLocal.Price < _catalogPOSComboMealItems.MinAmount)
                                cartItemLocal.Price = _catalogPOSComboMealItems.MinAmount;
                            else if (cartItemLocal.Price > _catalogPOSComboMealItems.MaxAmount)
                                cartItemLocal.Price = _catalogPOSComboMealItems.MaxAmount;
                            cartItemLocal.Combo_Prompt_Attributes = _catalogPOSComboMealItems.Prompt_For_Attributes;
                            cartItemLocal.Combo_Prompt_Menu_Item = _catalogPOSComboMealItems.Prompt_For_Menu_Item;
                            cartItemLocal.Combo_Prompt_Options = _catalogPOSComboMealItems.Prompt_For_Options;
                            cartItemLocal.Combo_Prompt_Size = _catalogPOSComboMealItems.Prompt_For_Sizes;
                            cartItemLocal.Price_By_Weight = _catalogPOSComboMealItemSizes.Price_By_Weight;
                            cartItemLocal.Tare_Weight = _catalogPOSComboMealItemSizes.Tare_Weight;
                            cartItemLocal.Prepared = _catalogPOSComboMealItems.Prepared_YN;
                            cartItemLocal.Pizza = Convert.ToBoolean(_catalogPOSComboMealItems.Pizza_YN);
                            cartItemLocal.Size_Description = _catalogPOSComboMealItemSizes.Description;
                            cartItemLocal.Quantity = Convert.ToSingle(itemCombo.Combo_Quantity);
                            cartItemLocal.Added_By = Session.CurrentEmployee.LoginDetail.EmployeeCode;
                            cartItemLocal.Orig_Menu_Code = _catalogPOSComboMealItems.Orig_Menu_Code;
                            cartItemLocal.Order_Line_Total = 0;                                                         
                            cartItemLocal.Order_Line_Complete = false;
                            if (cartItemLocal.Kitchen_Device_Count > 0)
                                cartItemLocal.Order_Line_Status_Code = 1;
                            else
                                cartItemLocal.Order_Line_Status_Code = 2;
                            cartItemLocal.NumberOfSizes = _catalogPOSComboMealItems.NumberOfSizes;
                            cartItemLocal.NumberOfAttributes = _catalogPOSComboMealItems.NumberOfAttributes;
                            cartItemLocal.NumberOfOptions = _catalogPOSComboMealItems.NumberOfOptions;
                            cartItemLocal.Menu_Item_Type_Code = _catalogPOSComboMealItemSizes.Menu_Item_Type_Code;
                            cartItemLocal.Print_Nutritional_Label = Convert.ToBoolean(_catalogPOSComboMealItemSizes.Print_Nutritional_Label);
                            cartItemLocal.Specialty_Pizza = Convert.ToBoolean(_catalogPOSComboMealItems.Specialty_Pizza);
                            cartItemLocal.Specialty_Pizza_Code = _catalogPOSComboMealItems.Specialty_Pizza_Code;
                            cartItemLocal.Topping_String = "";
                            cartItemLocal.Instruction_String = "";
                            cartItemLocal.Coupon_Code = "";
                            cartItemLocal.Topping_Codes = "";
                            cartItemLocal.Topping_Descriptions = "";
                            cartItemLocal.Coupon_Description = "";
                            cartItemLocal.MenuItemType = _catalogPOSComboMealItems.MenuItemType;
                            cartItemLocal.Action = "A";

                            if (cartItemLocal.Combo_Prompt_Attributes == false && cartItemLocal.Combo_Prompt_Menu_Item == false &&
                                cartItemLocal.Combo_Prompt_Options == false && cartItemLocal.Combo_Prompt_Size == false)
                            {
                                cartItemLocal.Menu_Item_Choosen = true;
                                cartItemLocal.Size_Chosen = true;
                                cartItemLocal.Order_Line_Complete = true;
                            }
                            else
                            {
                                if (cartItemLocal.Combo_Prompt_Menu_Item == false)
                                    cartItemLocal.Menu_Item_Choosen = true;
                                else
                                    cartItemLocal.Menu_Item_Choosen = false;

                                if (cartItemLocal.Combo_Prompt_Size == false)
                                {
                                    cartItemLocal.Size_Chosen = true;
                                }
                                else
                                {
                                    if (cartItemLocal.NumberOfSizes > 1)
                                        cartItemLocal.Size_Chosen = false;
                                    else
                                        cartItemLocal.Size_Chosen = true;
                                }

                                if ((cartItemLocal.Combo_Prompt_Attributes && cartItemLocal.NumberOfAttributes > 1) ||
                                   (cartItemLocal.Combo_Prompt_Options && cartItemLocal.NumberOfOptions > 1) ||
                                        cartItemLocal.Menu_Item_Choosen == false || cartItemLocal.Size_Chosen == false)
                                    cartItemLocal.Order_Line_Complete = false;
                                else
                                    cartItemLocal.Order_Line_Complete = true;
                            }

                            cartLocal.cartItems.Add(cartItemLocal);

                            curComboPrice = curComboPrice + cartItemLocal.Price;

                            itemCombo.Number_Of_Combo_Items = itemCombo.Number_Of_Combo_Items + 1;

                            break;
                        }
                    }
                }
            }

            itemCombo.Combo_Price = itemCombo.Combo_Price + curComboPrice;
            itemCombo.Action = "M";
            cartLocal.itemCombos.Add(itemCombo);


            ////Customer
            cartLocal.Customer = Session.cart.Customer;

            cartLocal.cartHeader = Session.cart.cartHeader;
            if (Session.cart.cartHeader.ctlAddressCity != cartLocal.Customer.City.Trim())
            {
                cartLocal.cartHeader = Session.cart.cartHeader;
                cartLocal.cartHeader.Action = "M";
                cartLocal.cartHeader.ctlAddressCity = cartLocal.Customer.City.Trim();
            }
            CartFunctions.UpdateCustomer(cartLocal);
            Session.cart = APILayer.Add2Cart(cartLocal);
        }

        public static bool IsComboChildMenuSelected(string ParentMenu)
        {
            if (ParentMenu == "MenuItems")
            {
                if (Session.ProcessingCombo && Session.CurrentComboItem > 0)
                    return true;
            }

            return false;
        }

        public static void ComboChildMenuItemChange(CartItem cartItem, string btnName)
        {
            CartFunctions.CheckCart();
            Cart cartLocal = (new Cart().GetCart());

            //CatalogPOSComboMealItemsForButtons _catalogPOSComboMealItemsForButtons = Session.catalogPOSComboMealItemsForButtons.Find(x => x.Menu_Code == btnName);
            CatalogPOSComboMealItemsForButtons _catalogPOSComboMealItemsForButtons = Session.catalogPOSComboMealItemsForButtons.Find(x => x.Combo_Menu_Code == cartItem.Combo_Menu_Code && x.Combo_Size_Code == cartItem.Combo_Size_Code && x.Menu_Code == btnName && x.Item_Number == Session.CurrentComboItem);

            cartItem.Menu_Code = _catalogPOSComboMealItemsForButtons.Menu_Code;
            cartItem.Menu_Description = _catalogPOSComboMealItemsForButtons.Order_Description;
            cartItem.Prepared = _catalogPOSComboMealItemsForButtons.Prepared_YN;
            cartItem.Menu_Item_Taxable = _catalogPOSComboMealItemsForButtons.Taxable;
            cartItem.Pizza = _catalogPOSComboMealItemsForButtons.Pizza_YN;
            cartItem.Kitchen_Device_Count = _catalogPOSComboMealItemsForButtons.Kitchen_Device_Count;
            cartItem.Orig_Menu_Code = _catalogPOSComboMealItemsForButtons.Menu_Code;
            cartItem.Menu_Item_Choosen = true;
            if (cartItem.Kitchen_Device_Count > 0)
                cartItem.Order_Line_Status_Code = 1;
            else
                cartItem.Order_Line_Status_Code = 2;
            cartItem.NumberOfSizes = _catalogPOSComboMealItemsForButtons.NumberOfSizes;
            cartItem.NumberOfAttributes = _catalogPOSComboMealItemsForButtons.NumberOfAttributes;
            cartItem.NumberOfOptions = _catalogPOSComboMealItemsForButtons.NumberOfOptions;
            cartItem.Menu_Item_Type_Code = _catalogPOSComboMealItemsForButtons.Menu_Item_Type_Code;
            cartItem.Print_Nutritional_Label = _catalogPOSComboMealItemsForButtons.Print_Nutritional_Label;
            cartItem.MenuItemType = _catalogPOSComboMealItemsForButtons.MenuItemType;
            cartItem.Action = "M";

            cartLocal.cartItems.Add(cartItem);

            cartLocal.Customer = Session.cart.Customer;

            cartLocal.cartHeader.CartId = Session.cart.cartHeader.CartId;
            if (Session.cart.cartHeader.ctlAddressCity != cartLocal.Customer.City.Trim())
            {
                cartLocal.cartHeader = Session.cart.cartHeader;
                cartLocal.cartHeader.Action = "M";
                cartLocal.cartHeader.ctlAddressCity = cartLocal.Customer.City.Trim();
            }
            CartFunctions.UpdateCustomer(cartLocal);
            Session.cart = APILayer.Add2Cart(cartLocal);
        }

        public static void ComboMenuItemsCheck(string Menu_Code)
        {
            if (Session.CurrentComboGroup <= 0) return;
            if (Session.cart.itemCombos == null || Session.cart.itemCombos.Count <= 0) return;
            ItemCombo itemCombo = Session.cart.itemCombos.Find(x => x.Combo_Group == Session.CurrentComboGroup);
            if (itemCombo == null) return;

            Session.selectedComboMenuItem = Session.comboMenuItems.Find(x => x.Combo_Menu_Code == itemCombo.Combo_Menu_Code && x.Combo_Size_Code == itemCombo.Combo_Size_Code && x.Menu_Code == Menu_Code && x.Item_Number == Session.CurrentComboItem);

            if (Session.selectedComboMenuItem == null)
            {
                List<CatalogPOSComboMealItems> currentComboMealItems = APILayer.GetComboMealItems(itemCombo.Combo_Menu_Code, itemCombo.Combo_Size_Code);
                foreach (CatalogPOSComboMealItems catalogPOSComboMealItems in currentComboMealItems)
                {                    
                    if (!Session.comboMenuItems.Any(z => z.Combo_Menu_Code == catalogPOSComboMealItems.Combo_Menu_Code && z.Combo_Size_Code == catalogPOSComboMealItems.Combo_Size_Code && z.Menu_Code == catalogPOSComboMealItems.Menu_Code && z.Item_Number == catalogPOSComboMealItems.Item_Number)) Session.comboMenuItems.Add(catalogPOSComboMealItems);
                }

                Session.selectedComboMenuItem = Session.comboMenuItems.Find(x => x.Combo_Menu_Code == itemCombo.Combo_Menu_Code && x.Combo_Size_Code == itemCombo.Combo_Size_Code && x.Menu_Code == Menu_Code && x.Item_Number == Session.CurrentComboItem);
            }
        }

        public static void ComboMenuItemSizesCheck(string Menu_Code, string Size_Code)
        {
            if (Session.CurrentComboGroup <= 0) return;
            if (Session.cart.itemCombos == null || Session.cart.itemCombos.Count <= 0) return;
            ItemCombo itemCombo = Session.cart.itemCombos.Find(x => x.Combo_Group == Session.CurrentComboGroup);
            if (itemCombo == null) return;

            Session.selectedComboMenuItemSizes = Session.comboMenuItemSizes.Find(x => x.Combo_Menu_Code == itemCombo.Combo_Menu_Code && x.Combo_Size_Code == itemCombo.Combo_Size_Code && x.Menu_Code == Menu_Code && x.Size_Code == Size_Code && x.Item_Number == Session.CurrentComboItem);

            if (Session.selectedComboMenuItemSizes == null)
            {
                List<CatalogPOSComboMealItemSizes> currentComboMealItemSizes = APILayer.GetComboMealItemSizes(itemCombo.Combo_Menu_Code, itemCombo.Combo_Size_Code);
                foreach (CatalogPOSComboMealItemSizes catalogPOSComboMealItemSizes in currentComboMealItemSizes)
                {
                    //if (!Session.comboMenuItemSizes.Any(z => z.Menu_Code == catalogPOSComboMealItemSizes.Menu_Code && z.Size_Code == catalogPOSComboMealItemSizes.Size_Code)) Session.comboMenuItemSizes.Add(catalogPOSComboMealItemSizes);
                    if (!Session.comboMenuItemSizes.Any(z => z.Combo_Menu_Code == catalogPOSComboMealItemSizes.Combo_Menu_Code && z.Combo_Size_Code == catalogPOSComboMealItemSizes.Combo_Size_Code && z.Menu_Code == catalogPOSComboMealItemSizes.Menu_Code && z.Size_Code == catalogPOSComboMealItemSizes.Size_Code && z.Item_Number == catalogPOSComboMealItemSizes.Item_Number)) Session.comboMenuItemSizes.Add(catalogPOSComboMealItemSizes);
                }

                Session.selectedComboMenuItemSizes = Session.comboMenuItemSizes.Find(x => x.Combo_Menu_Code == itemCombo.Combo_Menu_Code && x.Combo_Size_Code == itemCombo.Combo_Size_Code && x.Menu_Code == Menu_Code && x.Size_Code == Size_Code && x.Item_Number == Session.CurrentComboItem);
            }
        }

        public static void FillComboPropertiestoCartItemHistoryModify(ref Cart cart)
        {
            if (cart.itemCombos == null || cart.itemCombos.Count <= 0) return;

            foreach (CartItem cartItem in cart.cartItems)
            {
                if (cartItem.Combo_Group > 0 )
                {
                    ItemCombo itemCombo = cart.itemCombos.Find(x => x.Combo_Group == cartItem.Combo_Group);
                    if (itemCombo != null)
                    {
                        CatalogPOSComboMealItems ComboMenuItem = Session.comboMenuItems.Find(x => x.Combo_Menu_Code == itemCombo.Combo_Menu_Code && x.Combo_Size_Code == itemCombo.Combo_Size_Code && x.Menu_Code == cartItem.Menu_Code && x.Item_Number == cartItem.Combo_Item_Number);

                        if (ComboMenuItem == null)
                        {
                            List<CatalogPOSComboMealItems> ComboMealItems = APILayer.GetComboMealItems(itemCombo.Combo_Menu_Code, itemCombo.Combo_Size_Code);
                            foreach (CatalogPOSComboMealItems catalogPOSComboMealItems in ComboMealItems)
                            {
                                if (!Session.comboMenuItems.Any(z => z.Combo_Menu_Code == catalogPOSComboMealItems.Combo_Menu_Code && z.Combo_Size_Code == catalogPOSComboMealItems.Combo_Size_Code && z.Menu_Code == catalogPOSComboMealItems.Menu_Code && z.Item_Number == catalogPOSComboMealItems.Item_Number)) Session.comboMenuItems.Add(catalogPOSComboMealItems);
                            }
                            ComboMenuItem = Session.comboMenuItems.Find(x => x.Combo_Menu_Code == itemCombo.Combo_Menu_Code && x.Combo_Size_Code == itemCombo.Combo_Size_Code && x.Menu_Code == cartItem.Menu_Code && x.Item_Number == cartItem.Combo_Item_Number);
                        }

                        cartItem.Combo_Prompt_Menu_Item = ComboMenuItem.Prompt_For_Menu_Item;
                        cartItem.Combo_Prompt_Size = ComboMenuItem.Prompt_For_Size;
                        cartItem.Combo_Prompt_Attributes = ComboMenuItem.Prompt_For_Attributes;
                        cartItem.Combo_Prompt_Options = ComboMenuItem.Prompt_For_Options;
                    }
                }
            }
        }

        public static bool IsCombosAvailableForUpsell(string MenuCode1, string SizeCode1, string MenuCode2 = "", string SizeCode2 = "")
        {
            Session.CombosAvailableForUpsell = APILayer.GetCombosForUpsell(MenuCode1, SizeCode1, MenuCode2, SizeCode2);

            if (Session.CombosAvailableForUpsell != null && Session.CombosAvailableForUpsell.Count > 0)
                return true;
            else
                return false;
        }
    }
}
