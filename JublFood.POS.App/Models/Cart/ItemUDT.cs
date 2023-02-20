using System;

namespace JublFood.POS.App.Models.Cart
{
    public class ItemUDT
    {
        public string CartId { get; set; }
        public string Location_Code{ get; set; }
        public long Order_Number{ get; set; }
        public DateTime Order_Date{ get; set; }
        public int Line_Number{ get; set; }
        public int Sequence{ get; set; }
        public long Tax_Category_ID{ get; set; }
        public bool Taxable_By_Margin{ get; set; }
        public bool Tax_1_Discounted{ get; set; }
        public float Tax_1_Rate{ get; set; }
        public decimal Tax_1_Item_Min_Amount{ get; set; }
        public decimal Tax_1_Taxable_Amount{ get; set; }
        public bool Tax_2_Discounted{ get; set; }
        public float Tax_2_Rate{ get; set; }
        public decimal Tax_2_Item_Min_Amount { get; set; }
        public decimal Tax_2_Taxable_Amount { get; set; }
        public bool Tax_3_Discounted{ get; set; }
        public float Tax_3_Rate{ get; set; }
        public decimal Tax_3_Item_Min_Amount { get; set; }
        public decimal Tax_3_Taxable_Amount { get; set; }
        public bool Tax_4_Discounted{ get; set; }
        public float Tax_4_Rate{ get; set; }
        public decimal Tax_4_Item_Min_Amount { get; set; }
        public decimal Tax_4_Taxable_Amount { get; set; }
        public decimal Order_Line_Taxable_Sale1 { get; set; }
        public decimal Order_Line_Taxable_Sale2 { get; set; }
        public decimal Order_Line_Taxable_Sale3 { get; set; }
        public decimal Order_Line_Taxable_Sale4 { get; set; }
        public decimal Order_Line_Non_Taxable_Sale1 { get; set; }
        public decimal Order_Line_Non_Taxable_Sale2 { get; set; }
        public decimal Order_Line_Non_Taxable_Sale3 { get; set; }
        public decimal Order_Line_Non_Taxable_Sale4 { get; set; }
        public decimal Order_Line_Tax1 { get; set; }
        public decimal Order_Line_Tax2 { get; set; }
        public decimal Order_Line_Tax3 { get; set; }
        public decimal Order_Line_Tax4 { get; set; }
        public string Added_By{ get; set; }
    }
}
