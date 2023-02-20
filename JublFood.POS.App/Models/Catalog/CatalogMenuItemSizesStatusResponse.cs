using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JublFood.POS.App.Models.Catalog
{
    public class CatalogMenuItemSizesStatusResponse
    {
        public string ResponseStatus { get; set; }
        public string ResponseStatusCode { get; set; }
        public List<CatalogMenuItemSizesStatus> catalogMenuItemSizesStatus { get; set; }

    }

    public class CatalogMenuItemSizesStatus
    {
        public string Location_Code { get; set; }
        public string Menu_Code { get; set; }
        public string Size_Code { get; set; }
        public bool IsEnabled { get; set; }
    }
}
