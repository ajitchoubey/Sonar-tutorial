using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JublFood.POS.App.Models.Catalog
{
    public class CatalogMenuItemStatusResponse
    {
        public string ResponseStatus { get; set; }
        public string ResponseStatusCode { get; set; }
        public List<CatalogMenuItemStatus> catalogMenuItemStatus { get; set; }
        
    }

    public class CatalogMenuItemStatus
    {
        public string Location_Code { get; set; }
        public string Menu_Code { get; set; }
        public bool IsEnabled { get; set; }
    }
}
