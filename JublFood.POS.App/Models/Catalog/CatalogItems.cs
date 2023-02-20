using System;

namespace JublFood.POS.App.Models.Catalog
{
    public class CatalogItems
    {
        public string Location_Code { get; set; }
        public string Menu_Category_Code { get; set; }
        public string Description { get; set; }
        public string Order_Description { get; set; }
        public Int32 Display_Order { get; set; }
        public Int16  Remote_Order_Availability { get; set; }
        public Boolean Visible { get; set; }
        public Int32 Default_Category { get; set; }
    }
}
