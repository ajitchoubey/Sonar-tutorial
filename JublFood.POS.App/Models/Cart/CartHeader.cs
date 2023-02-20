using System;

namespace JublFood.POS.App.Models.Cart
{
    public class CartHeader
    {
        public string CartId { get; set; }        
        public String LocationCode { get; set; }
        public long Order_Number { get; set; }
        public DateTime Order_Date { get; set; }
        public long Old_Order_Number { get; set; }
        public bool Being_Modified { get; set; }
        public String Modifying { get; set; }
        public long Customer_Code { get; set; }
        public String Customer_Room { get; set; }
        public String Customer_Name { get; set; }
        public String Comments { get; set; }
        public DateTime Actual_Order_Date { get; set; }
        public int Order_Status_Code { get; set; }
        public String Order_Type_Code { get; set; }
        public bool Pay_Now { get; set; }
        public DateTime Order_Saved { get; set; }
        public int Order_Time { get; set; }
        public Decimal Sales_Tax1 { get; set; }
        public Decimal Sales_Tax2 { get; set; }
        public Decimal Credit { get; set; }
        public Decimal Coupon_Total { get; set; }
        public Decimal SubTotal { get; set; }
        public String Coupon_Code { get; set; }
        public bool Coupon_Taxable { get; set; }
        public int Coupon_Type_Code { get; set; }
        public bool Coupon_Adjustment { get; set; }
        public Decimal Coupon_Amount { get; set; }
        public DateTime Route_Time { get; set; }
        public String Driver_ID { get; set; }
        public String Driver_Shift { get; set; }
        public DateTime Return_Time { get; set; }
        public DateTime Delivery_Time { get; set; }
        public Decimal Delivery_Fee { get; set; }
        public Decimal Taxable_Sales1 { get; set; }
        public Decimal Taxable_Sales2 { get; set; }
        public Decimal Non_Taxable_Sales { get; set; }
        public int Number_In_Party { get; set; }
        public String Computer_Name { get; set; }
        public Decimal Change_Due { get; set; }

        public Decimal Final_Total { get; set; }
        public Decimal Order_Line_Adjustments { get; set; }
        public Decimal Order_Adjustments { get; set; }
        public Decimal Order_Line_Coupon_Total { get; set; }
        public Decimal Order_Coupon_Total { get; set; }
        public Decimal Delivery_Fee_Tax1 { get; set; }
        public Decimal Delivery_Fee_Tax2 { get; set; }

        public String Added_By { get; set; }
        public String Credit_Card_Name { get; set; }
        public DateTime Delayed_Date { get; set; }
        public int Delayed_Order { get; set; }
        public DateTime Start_DateTime { get; set; }
        public Double TaxRate1 { get; set; }
        public Double TaxRate2 { get; set; }
        public Decimal Total { get; set; }
        public bool Delayed_Same_Day { get; set; }
        public DateTime Kitchen_Display_Time { get; set; }
        public int Current_Sequence { get; set; }
        public String Order_Taker_ID { get; set; }
        public String Order_Taker_Shift { get; set; }
        public String Tent_Number { get; set; }
        public bool Order_Lines_Modified { get; set; }
        public bool Delivery_Fee_Removed { get; set; }
        public Decimal Bottle_Deposit { get; set; }
        public DateTime Closed_Order_Time { get; set; }
        public long Workstation_ID { get; set; }
        public String Secure_Coupon_ID { get; set; }
        public String ROI_Customer { get; set; }
        public String Instruction { get; set; }
        public String Types { get; set; }
        public String Device_Type { get; set; }
        public String Platform { get; set; }
        public String Browser { get; set; }
        public String Payment_Gateway { get; set; }
        public String CustomField1 { get; set; }
        public String CustomField2 { get; set; }
        public String CustomField3 { get; set; }
        public String CustomField4 { get; set; }
        public String CustomField5 { get; set; }
        public String CustomField7 { get; set; }
        public String CustomField8 { get; set; }
        public String CustomField9 { get; set; }
        public String CustomField10 { get; set; }
        public int Is_Advance { get; set; }
        public String OTS_Number { get; set; }
        public String Action { get; set; }
        public string ctlAddressCity { get; set; }
        public string aapplyCoupon { get; set; }
        public bool ODC_Tax { get; set; }
        public bool Processed { get; set; }
        public bool IsRemake { get; set; }
        public string OriginalLocationCode { get; set; }
    }
}