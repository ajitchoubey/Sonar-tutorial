using System;

namespace JublFood.POS.App.Models.Catalog
{
    public class CatalogItemsRequest
    {
        public String Location_Code { get; set; }
        public Int32 Menu_Type_ID { get; set; }
        public String Menu_Category_Code { get; set; }
    }
}
