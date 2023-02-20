using System;
using System.Collections.Generic;

namespace JublFood.POS.App.Models.Catalog
{
    public class CatalogUpsellMenuResponse
    {
        public string ResponseStatus { get; set; }
        public string ResponseStatusCode { get; set; }

        public List<CatalogUpsellMenu> catalogUpsellMenu { get; set; }
    }

    public class CatalogUpsellMenu
    {

        public string location_code { get; set; }
        public string Menu_Code { get; set; }
        public string Size_Code { get; set; }
        public string Menu_Category_Code { get; set; }
        public string order_description { get; set; }
        public decimal price { get; set; }
        public string description { get; set; }
        public Nullable<Boolean> MenuItemType { get; set; }
        public string Coupon_Code { get; set; }
        public string Coupon_Description { get; set; }
        public decimal amount { get; set; }


    }

}
