using System;

namespace JublFood.POS.App.Models.Catalog
{
    public class CatalogToppings
    {
        public String Menu_Code { get; set; }
        public String Size_Code { get; set; }
        public String Order_Description { get; set; }
        public String Topping_Code { get; set; }
        public String Menu_Item_Image { get; set; }
        public String Text_Color { get; set; }
        public String Kitchen_Display_Order { get; set; }
        public String Amount_Code { get; set; }
        public String Default_Item { get; set; }
        public String Item_Part { get; set; }
        public bool Default { get; set; }
        public Nullable<Boolean> MenuItemType { get; set; }
    }
}
