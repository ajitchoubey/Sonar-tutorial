using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JublFood.POS.App.Models.Catalog
{
    public class CatalogMenuItemEDVCoupon
    {
        public string Location_Code { get; set; }
        public string Menu_Code { get; set; }
        public string Size_Code { get; set; }
        public string Coupon_Code { get; set; }
        public int Total_Item_Count { get; set; }
        public decimal Amount { get; set; }
    }
}
