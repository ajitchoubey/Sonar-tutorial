using System.Collections.Generic;

namespace JublFood.POS.App.Models.Catalog
{
    public class CatalogMenuCategoryResponse
    {
        public string ResponseStatus { get; set; }
        public string ResponseStatusCode { get; set; }
        public List<CatalogMenuCategory> catalogMenuCategories { get; set; }
    }
}
