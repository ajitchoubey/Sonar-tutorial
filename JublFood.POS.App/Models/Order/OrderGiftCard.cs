using System;

namespace JublFood.POS.App.Models.Order
{
    public class OrderGiftCard
    {
        public string Location_Code { get; set; }
        public int Order_Number { get; set; }
        public DateTime Order_Date { get; set; }
        public int Payment_Line_Number { get; set; }
        public int Gift_Card_Transaction_Type { get; set; }
        public Byte Gift_Card_Account { get; set; }
        public Byte Gift_Card_Expiration { get; set; }
        public decimal Gift_Card_Amount { get; set; }
        public decimal Gift_Card_Tip { get; set; }
        public string Gift_Card_Approval { get; set; }
        public string Transaction_Number { get; set; }
        public string Security_Code { get; set; }
        public int Credit_Loss { get; set; }
        public int Entry_Method { get; set; }
        public DateTime Settlement_Date { get; set; }
        public int Action_Code { get; set; }
        public string Return_Code { get; set; }
        public string Response_Code { get; set; }
        public string Reference_Number { get; set; }
        public string Batch_Number { get; set; }
        public string Retrieval_Reference_Code { get; set; }
        public string Card_Present_Value { get; set; }
        public string Response_Text { get; set; }
        public string Comment { get; set; }
        public string Internal_Seq_Number { get; set; }
        public int Trans_Item_Number { get; set; }
        public string ACI { get; set; }
        public decimal Est_Tip_Amount { get; set; }
        public string Result_Code { get; set; }
        public string Net_ID { get; set; }
        public string Card_ID_Code { get; set; }
        public string Acct_Data_Source { get; set; }
        public decimal Gift_Card_Balance { get; set; }
        public string Added_By { get; set; }
        public DateTime Added { get; set; }
    }
}
