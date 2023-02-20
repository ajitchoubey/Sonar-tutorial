using JublFood.POS.App.API;
using JublFood.POS.App.Cache;
using JublFood.POS.App.Models.Cart;
using JublFood.POS.App.Models.Catalog;
using JublFood.POS.App.Models.Employee;
using System;
using System.Collections.Generic;
using System.Linq;

namespace JublFood.POS.App.BusinessLayer
{
    public static class CouponFunctions
    {
        public static bool ApplyOrderLineCoupon(string btnName)
        {
            int no_of_coupons = 0;
            int no_of_coupon_apply = 0;
            int no_of_coupons_count = 0;
            int count_coupon = 0;
            string menu_item_coupon = "";
            decimal couponapply = 0;
            List<CatalogMenuItemEDVCoupon> catalogMenuItemEDVCouponsList = new List<CatalogMenuItemEDVCoupon>();
            bool blnLoginSuccessful = false;
            EmployeeResult oldEmployeeResult;
            decimal InputVariableCouponAmount;
            CatalogCoupons selectedOrderLineCoupon = null;
            bool SetApplyCoupon = false;

            selectedOrderLineCoupon = Session.orderLineCoupons.Find(x => x.Coupon_Code == btnName);

            //EDV
            if (APILayer.IsCouponExistinCouponRule(btnName))
            {
                foreach (CartItem cartItem in Session.cart.cartItems)
                {
                    if (APILayer.GetAnyItemCountCouponRule(cartItem.Coupon_Code) == 2)
                    {
                        cartItem.Coupon_Code = "";
                        cartItem.Coupon_Description = "";
                        cartItem.Coupon_Amount = 0;

                        cartItem.Coupon_Adjustment = false;
                        cartItem.Coupon_Min_Price = 0;
                        cartItem.Coupon_Taxable = false;
                        cartItem.Order_Line_No_Tax_Discount = 0;
                        cartItem.Order_Line_Coupon_Amount = 0;
                        cartItem.Order_Line_Tax_Discount = 0;
                        cartItem.isEDVCoupon = false;
                        cartItem.isUpsellCoupon = false;
                    }

                }

                Session.cart.cartHeader.aapplyCoupon = btnName;

                foreach (CartItem cartItem in Session.cart.cartItems)
                {
                    if (cartItem.Combo_Group == 0)
                    {
                        List<CatalogMenuItemEDVCoupon> catalogMenuItemEDVCoupons = APILayer.GetMenuItemEDVCoupons(btnName, cartItem.Menu_Code, cartItem.Size_Code);
                        if (catalogMenuItemEDVCoupons != null && catalogMenuItemEDVCoupons.Count > 0)
                        {
                            no_of_coupons = no_of_coupons + (Convert.ToInt32(cartItem.Quantity) * catalogMenuItemEDVCoupons.Count);
                            no_of_coupon_apply = catalogMenuItemEDVCoupons[0].Total_Item_Count;
                            foreach (CatalogMenuItemEDVCoupon catalogMenuItemEDVCoupon in catalogMenuItemEDVCoupons)
                                catalogMenuItemEDVCouponsList.Add(catalogMenuItemEDVCoupon);
                        }
                    }
                }

                if (no_of_coupon_apply == 0)
                    no_of_coupon_apply = 1;

                if (no_of_coupons % no_of_coupon_apply == 0)
                    no_of_coupons_count = no_of_coupons;
                else
                    no_of_coupons_count = no_of_coupons - 1;

                count_coupon = 0;
                         
                if(SystemSettings.GetSettingValue("PrioritytoCostlier", Session._LocationCode) == "1")
                    Session.cart.cartItems = Session.cart.cartItems.OrderByDescending(z => z.Menu_Price).ToList();
                else
                    Session.cart.cartItems = Session.cart.cartItems.OrderBy(z => z.Menu_Price).ToList();

                foreach (CartItem cartItem in Session.cart.cartItems)
                {
                    List<CatalogMenuItemEDVCoupon> catalogMenuItemEDVCoupons = catalogMenuItemEDVCouponsList.FindAll(x => x.Coupon_Code == btnName && x.Menu_Code == cartItem.Menu_Code && x.Size_Code == cartItem.Size_Code);
                    if (catalogMenuItemEDVCoupons != null && catalogMenuItemEDVCoupons.Count > 0)
                    {
                        if (count_coupon < no_of_coupons_count)
                        {
                            count_coupon = count_coupon + 1;
                            cartItem.Coupon_Code = selectedOrderLineCoupon.Coupon_Code;
                            cartItem.Coupon_Description = selectedOrderLineCoupon.Description;

                            cartItem.Coupon_Type_Code = selectedOrderLineCoupon.Coupon_Type_Code;
                            cartItem.Coupon_Taxable = selectedOrderLineCoupon.Taxable;
                            cartItem.Coupon_Adjustment = selectedOrderLineCoupon.Adjustment;
                            cartItem.Coupon_Min_Price = selectedOrderLineCoupon.Min_Price;
                            cartItem.Coupon_Amount = catalogMenuItemEDVCoupons[0].Amount; //selectedOrderLineCoupon.Amount;
                            cartItem.isEDVCoupon = true;
                            cartItem.isUpsellCoupon = false;
                        }
                    }
                }

                Session.cart.cartItems = Session.cart.cartItems.OrderBy(z => z.Line_Number).ToList();

                return true;
            }
            else
            {
                bool any_item = false;
                foreach (CartItem cartItem in Session.cart.cartItems)
                {
                    if (APILayer.GetAnyItemCountCouponRule(btnName) == 1)
                        any_item = true;

                }

                foreach (CartItem cartItem in Session.cart.cartItems)
                {
                    if (APILayer.IsCouponExistinCouponRule(cartItem.Menu_Code) && any_item == false)
                    {
                        cartItem.Coupon_Code = "";
                        cartItem.Coupon_Description = "";
                        cartItem.Coupon_Amount = 0;

                        cartItem.Coupon_Adjustment = false;
                        cartItem.Coupon_Min_Price = 0;
                        cartItem.Coupon_Taxable = false;
                        cartItem.Order_Line_No_Tax_Discount = 0;
                        cartItem.Order_Line_Coupon_Amount = 0;
                        cartItem.Order_Line_Tax_Discount = 0;
                        cartItem.isEDVCoupon = false;
                        cartItem.isUpsellCoupon = false;
                    }
                }

                if (APILayer.IsCouponExistinCouponRuleEngine(btnName))
                {
                    menu_item_coupon = "";

                    foreach (CartItem cartItem in Session.cart.cartItems)
                    {
                        if (cartItem.Combo_Group == 0 && cartItem.Quantity > 0)
                            menu_item_coupon = menu_item_coupon + cartItem.Menu_Code + "|" + cartItem.Size_Code + "|" + cartItem.Quantity.ToString() + ";";
                    }

                    CatalogPOSMenuItemUpsell catalogPOSMenuItemUpsell = APILayer.POSMenuItemUpsell(menu_item_coupon, btnName);
                    if (catalogPOSMenuItemUpsell != null)
                        couponapply = catalogPOSMenuItemUpsell.Discount_Limit;

                    foreach (CartItem cartItem in Session.cart.cartItems)
                    {
                        if (cartItem.Combo_Group == 0)
                        {
                            if (catalogPOSMenuItemUpsell.Primary_Item_Count > 0 && catalogPOSMenuItemUpsell.Secondary_Item_Count > 0)
                            {
                                List<CatalogPOSMenuItemUpsellDisItem> catalogPOSMenuItemUpsellDisItems = APILayer.POSMenuItemUpsellDisItem(menu_item_coupon, btnName, cartItem.Menu_Code, cartItem.Size_Code);
                                if (catalogPOSMenuItemUpsellDisItems != null && catalogPOSMenuItemUpsellDisItems.Count > 0 && couponapply > 0)
                                {
                                    cartItem.Coupon_Code = selectedOrderLineCoupon.Coupon_Code;
                                    cartItem.Coupon_Description = selectedOrderLineCoupon.Description;

                                    cartItem.Coupon_Type_Code = selectedOrderLineCoupon.Coupon_Type_Code;
                                    cartItem.Coupon_Taxable = selectedOrderLineCoupon.Taxable;
                                    cartItem.Coupon_Adjustment = selectedOrderLineCoupon.Adjustment;
                                    cartItem.Coupon_Min_Price = selectedOrderLineCoupon.Min_Price;
                                    cartItem.Coupon_Amount = catalogPOSMenuItemUpsellDisItems[0].Discount;
                                    cartItem.itemlist = Convert.ToInt32(catalogPOSMenuItemUpsell.Discount_Limit);
                                    if (couponapply >= Convert.ToDecimal(cartItem.Quantity))
                                        cartItem.qty = Convert.ToInt32(cartItem.Quantity);
                                    else
                                        cartItem.qty = Convert.ToInt32(couponapply);
                                    couponapply = couponapply - Convert.ToDecimal(cartItem.Quantity);
                                    cartItem.Totalqty = 0;
                                    cartItem.isEDVCoupon = false;
                                    cartItem.isUpsellCoupon = true;
                                }
                            }
                        }
                    }

                    return true;

                }
                else
                {
                    return false;
                }

            }
        }

        public static List<string> GetAppliedEDVCoupons()
        {
            List<string> EDVCoupons = new List<string>();

            if (Session.cart != null && Session.cart.cartItems != null && Session.cart.cartItems.Count > 0)
            {
                List<CartItem> SelectedCartItems = Session.cart.cartItems.FindAll(x => x.isEDVCoupon == true);
                if (SelectedCartItems != null && SelectedCartItems.Count > 0)
                {
                    foreach (CartItem cartItem in SelectedCartItems)
                        if (!EDVCoupons.Contains(cartItem.Coupon_Code)) EDVCoupons.Add(cartItem.Coupon_Code);
                }
            }

            return EDVCoupons;
        }

        public static List<string> GetAppliedUpsellCoupons()
        {
            List<string> UpsellCoupons = new List<string>();

            if (Session.cart != null && Session.cart.cartItems != null && Session.cart.cartItems.Count > 0)
            {
                List<CartItem> SelectedCartItems = Session.cart.cartItems.FindAll(x => x.isUpsellCoupon == true);
                if (SelectedCartItems != null && SelectedCartItems.Count > 0)
                {
                    foreach (CartItem cartItem in SelectedCartItems)
                        if (!UpsellCoupons.Contains(cartItem.Coupon_Code)) UpsellCoupons.Add(cartItem.Coupon_Code);
                }
            }

            return UpsellCoupons;
        }

        public static List<CatalogCoupons> GetCouponsForCurrentCartItems(string Menu_code = "", string size_code = "")
        {
            string menu_code_coupon = "";
            List<CatalogCoupons> currentCatalogCoupons = null;
            if (Session.cart != null && Session.cart.cartItems != null && Session.cart.cartItems.Count > 0)
            {
                foreach (CartItem cartItem in Session.cart.cartItems)
                {
                    if (cartItem.Quantity > 0)
                    {
                        if (Menu_code == "" && size_code == "")
                            menu_code_coupon = menu_code_coupon + cartItem.Menu_Code + "|" + cartItem.Size_Code + "|" + cartItem.Quantity + "|" + cartItem.Menu_Code + "|" + cartItem.Size_Code + "|" + cartItem.Coupon_Code + "|0;";
                        else
                            menu_code_coupon = menu_code_coupon + cartItem.Menu_Code + "|" + cartItem.Size_Code + "|" + cartItem.Quantity + "|" + Menu_code + "|" + size_code + "|" + cartItem.Coupon_Code + "|0;";
                    }
                }

                currentCatalogCoupons = APILayer.GetOrderLineCoupons(menu_code_coupon);
            }
            return currentCatalogCoupons;
        }

        public static void SetEDVUpsellFlagtoCartItemModify(ref CartItem cartItem)
        {
            if (!String.IsNullOrEmpty(cartItem.Coupon_Code))
            {
                if (APILayer.IsCouponExistinCouponRule(cartItem.Coupon_Code))
                    cartItem.isEDVCoupon = true;
                else if (APILayer.IsCouponExistinCouponRuleEngine(cartItem.Coupon_Code))
                    cartItem.isUpsellCoupon = true;
            }
        }

        public static void EDVUpsellRecalculationOnItemAdd()
        {
            Cart cartLocal = (new Cart().GetCart());
            List<string> EDVCoupons = CouponFunctions.GetAppliedEDVCoupons();
            List<string> UpsellCoupons = CouponFunctions.GetAppliedUpsellCoupons();

            if (EDVCoupons.Count > 0 || UpsellCoupons.Count > 0)
            {
                foreach (CartItem cartItem in Session.cart.cartItems)
                {
                    if (cartItem.isEDVCoupon || cartItem.isUpsellCoupon)
                    {
                        cartItem.Coupon_Code = "";
                        cartItem.Coupon_Description = "";
                        cartItem.Coupon_Amount = 0;

                        cartItem.Coupon_Adjustment = false;
                        cartItem.Coupon_Min_Price = 0;
                        cartItem.Coupon_Taxable = false;
                        cartItem.Order_Line_No_Tax_Discount = 0;
                        cartItem.Order_Line_Coupon_Amount = 0;
                        cartItem.Order_Line_Tax_Discount = 0;
                        cartItem.isEDVCoupon = false;
                        cartItem.isUpsellCoupon = false;
                    }
                }

                List<CatalogCoupons> currentCatalogCoupons = CouponFunctions.GetCouponsForCurrentCartItems();

                if (currentCatalogCoupons != null && currentCatalogCoupons.Count > 0)
                {
                    foreach (string EDV in EDVCoupons)
                    {
                        if (currentCatalogCoupons.FindIndex(x => x.Coupon_Code == EDV) > -1) CouponFunctions.ApplyOrderLineCoupon(EDV);

                        cartLocal.cartItems = Session.cart.cartItems;
                        foreach (CartItem cartItem1 in cartLocal.cartItems)
                            cartItem1.Action = cartItem1.Action != "D" ? "M" : cartItem1.Action;

                        cartLocal.Customer = Session.cart.Customer;
                        cartLocal.cartHeader = Session.cart.cartHeader;
                        cartLocal.cartHeader.Action = "M";
                        CartFunctions.UpdateCustomer(cartLocal);
                        Session.cart = APILayer.Add2Cart(cartLocal);
                    }

                    foreach (string Upsell in UpsellCoupons)
                    {
                        if (currentCatalogCoupons.FindIndex(x => x.Coupon_Code == Upsell) > -1) CouponFunctions.ApplyOrderLineCoupon(Upsell);

                        cartLocal.cartItems = Session.cart.cartItems;
                        foreach (CartItem cartItem1 in cartLocal.cartItems)
                            cartItem1.Action = cartItem1.Action != "D" ? "M" : cartItem1.Action;

                        cartLocal.Customer = Session.cart.Customer;
                        cartLocal.cartHeader = Session.cart.cartHeader;
                        cartLocal.cartHeader.Action = "M";
                        CartFunctions.UpdateCustomer(cartLocal);
                        Session.cart = APILayer.Add2Cart(cartLocal);
                    }
                }
            }
        }


        public static void AddUpsellCouponFromUpsellScreen(string Coupon_Code,string Menu_Code, string Size_Code)
        {

            if (Session.orderLineCoupons == null || Session.orderLineCoupons.Count <= 0)
            {
                List<CatalogCoupons> currentCatalogCoupons = CouponFunctions.GetCouponsForCurrentCartItems(Menu_Code, Size_Code);

                if (currentCatalogCoupons != null && currentCatalogCoupons.Count > 0)
                {
                    foreach (CatalogCoupons _catalogCoupons in currentCatalogCoupons)
                    {
                        if (!Session.orderLineCoupons.Any(z => z.Coupon_Code == _catalogCoupons.Coupon_Code)) Session.orderLineCoupons.Add(_catalogCoupons);
                    }
                }
            }


                Cart cartLocal = (new Cart().GetCart());

            if (Coupon_Code != "")
            {
                List<CatalogCoupons> currentCatalogCoupons = CouponFunctions.GetCouponsForCurrentCartItems();

                if (currentCatalogCoupons != null && currentCatalogCoupons.Count > 0)
                {
                    if (currentCatalogCoupons.FindIndex(x => x.Coupon_Code == Coupon_Code) > -1) CouponFunctions.ApplyOrderLineCoupon(Coupon_Code);

                    cartLocal.cartItems = Session.cart.cartItems;
                    foreach (CartItem cartItem1 in cartLocal.cartItems)
                        cartItem1.Action = cartItem1.Action != "D" ? "M" : cartItem1.Action;

                    cartLocal.Customer = Session.cart.Customer;
                    cartLocal.cartHeader = Session.cart.cartHeader;
                    cartLocal.cartHeader.Action = "M";
                    CartFunctions.UpdateCustomer(cartLocal);
                    Session.cart = APILayer.Add2Cart(cartLocal);

                }
            }
        }

    }
}
