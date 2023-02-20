using System;

namespace JublFood.POS.App.Models.Catalog
{
    public class CatalogOptionGroups
    {
        public String Menu_Option_Group_Code { get; set; }
        public String Description { get; set; }
        public Int32 Max_To_Choose { get; set; }
        public Int32 Min_To_Choose { get; set; }
        public bool Topping_Group { get; set; }
        public bool Item_Specific_Price { get; set; }
        public bool Require_Choice { get; set; }
        public bool Display_Half_Buttons { get; set; }
        public string Menu_Code { get; set; }
    }
}
