using System.Collections.Generic;

namespace JublFood.POS.App.Models.Catalog
{
    public class CatalogMenuItemsResponse
    {
        public string ResponseStatus { get; set; }
        public string ResponseStatusCode { get; set; }
        public List<CatalogMenuItems> catalogMenuItems { get; set; }
    }
}
