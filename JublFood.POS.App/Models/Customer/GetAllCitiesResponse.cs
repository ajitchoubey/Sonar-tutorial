using System;
using System.Collections.Generic;

namespace JublFood.POS.App.Models.Customer
{
    public class GetAllCitiesResponse
    {
        public GetAllCitiesResult Result { get; set; }
    }

    public class GetAllCitiesResult
    {
        public string ResponseStatus { get; set; }
        public string ResponseStatusCode { get; set; }
        public List<GetAllCities> Cities { get; set; }
    }

    public class GetAllCities
    {
        public string LocationCode { get; set; }
        public int CityCode { get; set; }
        public string CityName { get; set; }
        public int RegionCode { get; set; }
        public float TaxRate1 { get; set; }
        public float TaxRate2 { get; set; }
        public string RegionAbbr { get; set; }
        public string RegionName { get; set; }
        public string AddedBy { get; set; }
        public DateTime Added { get; set; }
    }
}
