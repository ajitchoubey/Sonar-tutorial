using System;

namespace JublFood.POS.App.Models.Order
{
    public class OrderUDT
    {
        public string CartId { get; set; }
        public string Com_Code{ get; set; }
        public string Location_Code{ get; set; }
        public DateTime Order_Date{ get; set; }
        public long Order_Number{ get; set; }
        public int city_code{ get; set; }
        public string Postal_Code{ get; set; }
        public bool City_Based_Tax{ get; set; }
        public bool Tax_1_Min_Order_Tax{ get; set; }
        public decimal Tax_1_Min_Order_Amount{ get; set; }
        public bool Tax_1_Compound_Tax_2{ get; set; }
        public bool Tax_1_Compound_Tax_3{ get; set; }
        public bool Tax_1_Compound_Tax_4{ get; set; }
        public bool Tax_2_Min_Order_Tax{ get; set; }
        public decimal Tax_2_Min_Order_Amount{ get; set; }
        public bool Tax_2_Compound_Tax_1{ get; set; }
        public bool Tax_2_Compound_Tax_3{ get; set; }
        public bool Tax_2_Compound_Tax_4{ get; set; }
        public bool Tax_3_Min_Order_Tax{ get; set; }
        public decimal Tax_3_Min_Order_Amount{ get; set; }
        public bool Tax_3_Compound_Tax_1{ get; set; }
        public bool Tax_3_Compound_Tax_2{ get; set; }
        public bool Tax_3_Compound_Tax_4{ get; set; }
        public bool Tax_4_Min_Order_Tax{ get; set; }
        public decimal Tax_4_Min_Order_Amount{ get; set; }
        public bool Tax_4_Compound_Tax_1{ get; set; }
        public bool Tax_4_Compound_Tax_2{ get; set; }
        public bool Tax_4_Compound_Tax_3{ get; set; }
        public long Delivery_Fee_Category_ID{ get; set; }
        public float Delivery_Fee_Rate_1{ get; set; }
        public float Delivery_Fee_Rate_2{ get; set; }
        public float Delivery_Fee_Rate_3{ get; set; }
        public float Delivery_Fee_Rate_4{ get; set; }
        public decimal Sales_Tax1{ get; set; }
        public decimal Sales_Tax2 { get; set; }
        public decimal Sales_Tax3 { get; set; }
        public decimal Sales_Tax4 { get; set; }
        public decimal Taxable_Sales1 { get; set; }
        public decimal Taxable_Sales2 { get; set; }
        public decimal Taxable_Sales3 { get; set; }
        public decimal Taxable_Sales4 { get; set; }
        public decimal Non_Taxable_Sales1 { get; set; }
        public decimal Non_Taxable_Sales2 { get; set; }
        public decimal Non_Taxable_Sales3 { get; set; }
        public decimal Non_Taxable_Sales4 { get; set; }
        public decimal Delivery_Fee_Tax1 { get; set; }
        public decimal Delivery_Fee_Tax2 { get; set; }
        public decimal Delivery_Fee_Tax3 { get; set; }
        public decimal Delivery_Fee_Tax4 { get; set; }
        public string Added_By{ get; set; }
        public String Action { get; set; }
    }
}
