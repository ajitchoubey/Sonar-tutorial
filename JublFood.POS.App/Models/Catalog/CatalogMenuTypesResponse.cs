using System.Collections.Generic;

namespace JublFood.POS.App.Models.Catalog
{
    public class CatalogMenuTypesResponse
    {
        public string ResponseStatus { get; set; }
        public string ResponseStatusCode { get; set; }
        public List<CatalogMenuTypes> catalogMenuTypes { get; set; }
    }
}
