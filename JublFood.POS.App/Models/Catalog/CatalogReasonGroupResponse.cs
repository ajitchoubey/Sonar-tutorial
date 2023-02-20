using System.Collections.Generic;

namespace JublFood.POS.App.Models.Catalog
{
    public class CatalogReasonGroupResponse
    {
        public string ResponseStatus { get; set; }
        public string ResponseStatusCode { get; set; }

        public List<CatalogReasonGroup> catalogReasonGroup { get; set; }
    }

    public class CatalogReasonGroup
    {
        public string Location_Code { get; set; }
        public int Language_Code { get; set; }
        public int Reason_Group_Code { get; set; }

        public string Default_Description { get; set; }
        public string Modified_Description { get; set; }
        public bool Tracking_Group { get; set; } 

    }
}
