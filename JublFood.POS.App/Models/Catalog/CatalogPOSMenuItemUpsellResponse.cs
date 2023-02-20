using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JublFood.POS.App.Models.Catalog
{
    public class CatalogPOSMenuItemUpsellResponse
    {
        public string ResponseStatus { get; set; }
        public string ResponseStatusCode { get; set; }
        public CatalogPOSMenuItemUpsell catalogPOSMenuItemUpsell { get; set; }
    }
}
