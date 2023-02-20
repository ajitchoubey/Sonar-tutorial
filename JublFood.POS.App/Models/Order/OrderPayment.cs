using System;

namespace JublFood.POS.App.Models.Order
{
    public class OrderPayment
    {
        public string CartId { get; set; }
        public string Location_Code { get; set; }
        public long Order_Number { get; set; }
        public DateTime Order_Date { get; set; }
        public int Payment_Line_Number { get; set; }
        public int Sequence { get; set; }
        public int Order_Pay_Type_Code { get; set; }
        public decimal Amount_Tendered { get; set; }
        public decimal Payment_Amount { get; set; }
        public decimal Change_Due { get; set; }
        public decimal Double_Amount { get; set; }
        public string Double_Code { get; set; }
        public string Check_Info { get; set; }
        public bool Deleted { get; set; }
        public string CashOut_ID { get; set; }
        public int Cash_Out_Shift { get; set; }
        public DateTime CashOut_Time { get; set; }
        public string Added_By { get; set; }
        public bool Data_Changed { get; set; }
        public bool Data_Processed { get; set; }
        public bool Process_Failed { get; set; }
        public string CardNumber { get; set; }
        public string CardExpiration { get; set; }
        public string ApprovalCode { get; set; }
        public decimal CreditCardAmount { get; set; }
        public int CreditCardID { get; set; }
        public string CreditCardDescription { get; set; }
        public string NameOnCard { get; set; }
        public string Track1Data { get; set; }
        public string Track2Data { get; set; }
        public string AVSStreet { get; set; }
        public string PostalCode { get; set; }
        public string CVV2 { get; set; }
        public bool NewPayment { get; set; }
        public decimal Tip { get; set; }
        public int Transaction_ID { get; set; }
        public bool DriverCheckIN { get; set; }
        public bool ManagerCheckIN { get; set; }

        // added
        public decimal Currency_Amount { get; set; }
        public string Currency_Code { get; set; }
        public int Paid { get; set; }
        public int Credit_Card_Code { get; set; }
        public string Action { get; set; }

        public string RRNumber { get; set; }
        public DateTime TransactionTime { get; set; }
    }
}
