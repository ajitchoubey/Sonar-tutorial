using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JublFood.POS.App.Models.Catalog
{
    public class CatalogUpsellDataResponse
    {
        public string ResponseStatus { get; set; }
        public string ResponseStatusCode { get; set; }
        public CatalogUpsellData catalogUpsellData { get; set; }

    }

    public class CatalogUpsellData
    {
        public List<CatalogItemwiseUpsell> catalogItemwiseUpsell { get; set; }
        public List<CatalogUpsellNewItem> catalogUpsellNewItem { get; set; }
        public List<CatalogUpsellSizeChange> catalogUpsellSizeChange { get; set; }
        public List<CatalogUpsellAddTopping> catalogUpsellAddTopping { get; set; }
        public List<CatalogUpsellCombo> catalogUpsellCombo { get; set; }
        public List<CatalogUpsellExcludeItem> catalogUpsellExcludeItem { get; set; }
    }
}
