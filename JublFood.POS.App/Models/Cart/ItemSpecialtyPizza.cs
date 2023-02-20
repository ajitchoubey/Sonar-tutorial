using System;

namespace JublFood.POS.App.Models.Cart
{
    public class ItemSpecialtyPizza
    {
        public string CartId { get; set; }
        public string Location_Code{ get; set; }
        public long Order_Number{ get; set; }
        public DateTime Order_Date{ get; set; }
        public int Line_Number{ get; set; }
        public int Sequence{ get; set; }
        public string Menu_Option_Group_Code{ get; set; }
        public string Pizza_Part{ get; set; }
        public string Specialty_Pizza_Code{ get; set; }
        public string Added_By{ get; set; }
        public string Menu_Code{ get; set; }
    }
}
