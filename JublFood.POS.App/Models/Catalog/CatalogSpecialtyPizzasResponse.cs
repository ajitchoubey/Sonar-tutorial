using System.Collections.Generic;

namespace JublFood.POS.App.Models.Catalog
{
    public class CatalogSpecialtyPizzasResponse
    {
        public string ResponseStatus { get; set; }
        public string ResponseStatusCode { get; set; }
        public List<CatalogSpecialtyPizzas> catalogSpecialtyPizzas { get; set; }
    }
}
