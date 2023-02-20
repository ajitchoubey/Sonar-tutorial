using System.Collections.Generic;

namespace JublFood.POS.App.Models.Customer
{
    public class StreetLookUpResponse
    {
        public StreetLookUpResult Result { get; set; }
    }

    public class StreetLookUpResult
    {
        public string ResponseStatus { get; set; }
        public string ResponseStatusCode { get; set; }
        public List<StreetLookUp> Streets { get; set; }
    }

    public class StreetLookUp
    {
        public int StreetCode { get; set; }
        public string Street { get; set; }
        public int CityCode { get; set; }
        public string CityName { get; set; }
        public string RegionName { get; set; }
        public string PostalCode { get; set; }
        public float TaxRate1 { get; set; }
        public float TaxRate2 { get; set; }
        public string StreetName { get; set; }
        
    }

}
