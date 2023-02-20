using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JublFood.POS.App.Models.Catalog
{
    public class CatalogPOSMenuItemUpsellDisItem
    {
        public string Menu_Code { get; set; }
        public string Size_Code { get; set; }
        public decimal Discount { get; set; }
    }
}
