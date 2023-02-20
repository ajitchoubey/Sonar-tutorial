using System.Collections.Generic;

namespace JublFood.POS.App.Models.Catalog
{
    public class CatlogToppingDescriptonCodeResponse
    {
        public string ResponseStatus { get; set; }
        public string ResponseStatusCode { get; set; }
        public List< CatlogToppingDescriptonCode> catlogToppingDescriptonCode { get; set; }

    }
    public class CatlogToppingDescriptonCode
    {
        public string SpecialtyPizzas { get; set; }
        public string Toppings { get; set; }
        public string ToppingsDescription { get; set; }
    }
}
