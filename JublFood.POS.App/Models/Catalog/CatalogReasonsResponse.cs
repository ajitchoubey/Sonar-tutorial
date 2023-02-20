using System.Collections.Generic;

namespace JublFood.POS.App.Models.Catalog
{
    public class CatalogReasonsResponse
    {
        public string ResponseStatus { get; set; }
        public string ResponseStatusCode { get; set; }

        public List<CatalogReasons> catalogReasons { get; set; }

    }

    public class CatalogReasons
    {
        public string Location_Code { get; set; }
        public int Language_Code { get; set; }
        public int Reason_ID { get; set; }
        public int Reason_Group_Code { get; set; }

        public string System_Text { get; set; }
        public bool Active { get; set; }
        public int Display_Order { get; set; }
    }
}
