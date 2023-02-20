using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JublFood.POS.App.Models.Catalog
{
    public class CatalogDefaultToppings
    {
        public String Sub_Menu_Code { get; set; }
        public String Size_Code { get; set; }
        public String Amount_Code { get; set; }
        public String Order_Description { get; set; }
        public bool Cheese { get; set; }
        public bool Sauce { get; set; }
        public String Kitchen_Display_Order { get; set; }
        public String Added_By { get; set; }
        public DateTime Added { get; set; }
        public String Default_Item { get; set; }
        public String Item_Part { get; set; }
    }
}
