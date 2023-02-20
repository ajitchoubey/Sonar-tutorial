using System;

namespace JublFood.POS.App.Models.Cart
{
    public class ItemAttributeGroup
    {
        public string CartId { get; set; }
        public string Location_Code{ get; set; }
        public long Order_Number{ get; set; }
        public DateTime Order_Date{ get; set; }
        public int Line_Number{ get; set; }
        public int Sequence{ get; set; }
        public string Attribute_Group_Code{ get; set; }
        public string Attribute_Group_Description{ get; set; }
        public bool Attribute_Group_Complete{ get; set; }
        public int Display_Order{ get; set; }
    }
}
