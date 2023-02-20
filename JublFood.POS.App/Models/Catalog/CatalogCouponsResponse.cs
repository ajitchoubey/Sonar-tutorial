using System;
using System.Collections.Generic;

namespace JublFood.POS.App.Models.Catalog
{
    public class CatalogCouponsResponse
    {
        public string ResponseStatus { get; set; }
        public string ResponseStatusCode { get; set; }

        public List<CatalogCoupons> catalogCoupons { get; set; }
    }


    public class CatalogCoupons
    {
        public string Location_Code { get; set; }
        public string Coupon_Code { get; set; }
        public string Description { get; set; }
        public int Coupon_Type_Code { get; set; }
        public decimal Amount { get; set; }
        public bool Adjustment { get; set; }
        public bool Taxable { get; set; }
        public int Status_Code { get; set; }
        public DateTime Begin_Date { get; set; }
        public DateTime End_Date { get; set; }
        public int Display_Order { get; set; }
        public bool Any_Item { get; set; }
        public bool Entire_Order { get; set; }
        public decimal Min_Price { get; set; }
        public bool Protect_Coupon { get; set; }
        public int Remote_Order_Availability { get; set; }
        public bool Gift_Card_Activation_Discount { get; set; }
        public string Added_By { get; set; }
        public DateTime Added { get; set; }

    }
}
