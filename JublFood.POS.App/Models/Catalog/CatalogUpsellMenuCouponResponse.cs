using System.Collections.Generic;

namespace JublFood.POS.App.Models.Catalog
{
    public class CatalogUpsellMenuCouponResponse
    {

        public string ResponseStatus { get; set; }
        public string ResponseStatusCode { get; set; }

        public List<CatalogUpsellMenuCoupon> catalogUpsellMenuCoupon { get; set; }

    }
    public class CatalogUpsellMenuCoupon
    {
        public string Coupon_Code { get; set; }
        public string description { get; set; }
        public decimal amount { get; set; }
    }
}
