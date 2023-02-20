using System;

namespace JublFood.POS.App.Models.Customer
{
    public class Customer
    {
        public string CartId { get; set; }
        public string Location_Code { get; set; }
        public string Phone_Number{ get; set; }
        public string Phone_Ext{ get; set; }
        public int Customer_Code{ get; set; }
        public string Name{ get; set; }
        public string Company_Name{ get; set; }
        public string Street_Number{ get; set; }
        public int Street_Code{ get; set; }
        public int Cross_Street_Code{ get; set; }
        public string Suite{ get; set; }
        public string Address_Line_2{ get; set; }
        public string Address_Line_3{ get; set; }
        public string Address_Line_4{ get; set; }
        public string Mailing_Address{ get; set; }
        public string Postal_Code{ get; set; }
        public string Plus4{ get; set; }
        public string Cart{ get; set; }
        public string Delivery_Point_Code{ get; set; }
        public string Walk_Sequence{ get; set; }
        public string Address_Type{ get; set; }
        public float Set_Discount{ get; set; }
        public bool Tax_Exempt1{ get; set; }
        public string Tax_ID1{ get; set; }
        public bool Tax_Exempt2{ get; set; }
        public string Tax_ID2{ get; set; }
        public bool Tax_Exempt3{ get; set; }
        public string Tax_ID3{ get; set; }
        public bool Tax_Exempt4{ get; set; }
        public string Tax_ID4{ get; set; }
        public bool Accept_Checks{ get; set; }
        public bool Accept_Credit_Cards{ get; set; }
        public bool Accept_Gift_Cards{ get; set; }
        public bool Accept_Charge_Account{ get; set; }
        public bool Accept_Cash{ get; set; }
        public double Finance_Charge_Rate{ get; set; }
        public decimal Credit_Limit{ get; set; }
        public decimal Credit{ get; set; }
        public int Payment_Terms{ get; set; }
        public DateTime First_Order_Date{ get; set; }
        public DateTime Last_Order_Date{ get; set; }
        public string Added_By{ get; set; }
        public string Comments{ get; set; }
        public string DriverComments{ get; set; }
        public bool DriverCommentsAddUpdateDelete{ get; set; }
        public string Manager_Notes{ get; set; }
        public int Customer_City_Code{ get; set; }
        public string Customer_Street_Name{ get; set; }
        public bool HotelorCollege{ get; set; }
        public string City{ get; set; }
        public string Region{ get; set; }
        public double TaxRate1{ get; set; }
        public double TaxRate2{ get; set; }
        public string Cross_Street{ get; set; }
        public bool NoteAddUpdateDelete{ get; set; }
        public string gstin_number{ get; set; }
        public DateTime date_of_birth{ get; set; }
        public DateTime anniversary_date{ get; set; }
        public String Action { get; set; }
    }
}