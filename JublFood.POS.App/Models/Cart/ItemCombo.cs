using System;

namespace JublFood.POS.App.Models.Cart
{
    public class ItemCombo
    {
        public string CartId { get; set; }
        public string Location_Code { get; set; }
        public long Order_Number { get; set; }
        public DateTime Order_Date { get; set; }
        public string Combo_Menu_Code { get; set; }
        public string Combo_Size_Code { get; set; }
        public string Combo_Menu_Description { get; set; }
        public string Combo_Size_Description { get; set; }
        public decimal Combo_Price { get; set; }
        public int Combo_Group { get; set; }
        public double Combo_Quantity { get; set; }
        public long Combo_Menu_Type_ID { get; set; }
        public string Combo_Menu_Category_Code { get; set; }
        public int Number_Of_Combo_Items { get; set; }
        public bool Prompt_For_Size { get; set; }
        public string Coupon_Code { get; set; }
        public bool Coupon_Taxable { get; set; }
        public int Coupon_Type_Code { get; set; }
        public bool Coupon_Adjustment { get; set; }
        public float Coupon_Amount { get; set; }
        public float Coupon_Min_Price { get; set; }
        public string Coupon_Description { get; set; }
        public float Combo_Adjustment { get; set; }
        public string Action { get; set; }
        public string Combo_Description { get; set; }
    }
}
