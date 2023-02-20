using System;

namespace JublFood.POS.App.Models.Order
{
    public class OrderLineCombo
    {
        public string Location_Code { get; set; }
        public long Order_Number { get; set; }
        public DateTime Order_Date { get; set; }
        public string Combo_Menu_Code { get; set; }
        public string Combo_Size_Code { get; set; }
        public string Combo_Menu_Description { get; set; }
        public string Combo_Size_Description { get; set; }
        public decimal Combo_Price { get; set; }
        public int Combo_Group { get; set; }
        public int Combo_Quantity { get; set; }
        public long Combo_Menu_Type_ID { get; set; }
        public string Combo_Menu_Category_Code { get; set; }
        public int Number_Of_Combo_Items { get; set; }
        public Boolean Prompt_For_Size { get; set; }
        public string Coupon_Code { get; set; }
        public Boolean Coupon_Taxable { get; set; }
        public int Coupon_Type_Code { get; set; }
        public Boolean Coupon_Adjustment { get; set; }
        public decimal Coupon_Amount { get; set; }
        public decimal Coupon_Min_Price { get; set; }
        public string Coupon_Description { get; set; }
        public decimal Combo_Adjustment { get; set; }
    }
}