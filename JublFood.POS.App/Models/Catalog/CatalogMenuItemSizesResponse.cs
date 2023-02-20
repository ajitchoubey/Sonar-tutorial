using System.Collections.Generic;

namespace JublFood.POS.App.Models.Catalog
{
    public class CatalogMenuItemSizesResponse
    {
        public string ResponseStatus { get; set; }
        public string ResponseStatusCode { get; set; }
        public List<CatalogMenuItemSizes>  catalogMenuItemSizes { get; set; }
    }
}
