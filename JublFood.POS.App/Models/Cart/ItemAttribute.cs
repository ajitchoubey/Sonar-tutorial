using System;

namespace JublFood.POS.App.Models.Cart
{
    public class ItemAttribute
    {
        public string CartId { get; set; }
        public string Location_Code{ get; set; }
        public long Order_Number{ get; set; }
        public DateTime Order_Date{ get; set; }
        public int Line_Number{ get; set; }
        public int Sequence{ get; set; }
        public string Attribute_Group_Code{ get; set; }
        public string Attribute_Code{ get; set; }
        public string Added_By{ get; set; }
        public int Index{ get; set; }
        public string Attribute_Description{ get; set; }
        public int Menu_Item_Attribute_Groups_Display_Order_Display_Order{ get; set; }
        public int Attribute_Display_Order{ get; set; }
    }
}
