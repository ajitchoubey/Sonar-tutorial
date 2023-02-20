using System;

namespace JublFood.POS.App.Models.Cart
{
    public class ItemOption
    {
        public string CartId { get; set; }
        public string Location_Code{ get; set; }
        public long Order_Number{ get; set; }
        public DateTime Order_Date{ get; set; }
        public int Line_Number{ get; set; }
        public int Sequence{ get; set; }
        public string Menu_Option_Group_Code{ get; set; }
        public string Menu_Code{ get; set; }
        public string Size_Code{ get; set; }
        public string Pizza_Part{ get; set; }
        public string Amount_Code{ get; set; }
        public string Added_By{ get; set; }
        public int Index{ get; set; }
        public string Menu_Description{ get; set; }
        public string Description{ get; set; }
        public string Default_Topping{ get; set; }
        public bool Topping_group{ get; set; }
        public int Kitchen_Display_Order{ get; set; }
        public string Topping_Description{ get; set; }
        public int Group_Sort_Order{ get; set; }
        public string Default_Amount_Code{ get; set; }
    }
}
