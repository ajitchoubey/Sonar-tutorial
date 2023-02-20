using System.Collections.Generic;

namespace JublFood.POS.App.Models.Catalog
{
    public class CatalogOrderTypesResponse
    {
        public string ResponseStatus { get; set; }
        public string ResponseStatusCode { get; set; }
        public List<CatalogOrderTypes> catalogOrderTypes { get; set; }
    }
}
