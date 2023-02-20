using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JublFood.POS.App.Models.Catalog
{
    public class CatalogPOSMenuItemUpsell
    {
        public int Primary_Item_Count { get; set; }
        public int Secondary_Item_Count { get; set; }
        public decimal Discount_Limit { get; set; }
    }
}
