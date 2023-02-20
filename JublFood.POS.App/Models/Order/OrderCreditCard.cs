using System;

namespace JublFood.POS.App.Models.Order
{
    public class OrderCreditCard
    {
        public string CartId { get; set; }
        public string Location_Code { get; set; }
        public long Order_Number { get; set; }
        public DateTime Order_Date { get; set; }
        public int Payment_Line_Number { get; set; }
        public string Credit_Card_ID { get; set; }
        public int Credit_Card_Transaction_Type { get; set; }
        public string Credit_Card_Account { get; set; }
        public string Credit_Card_Expiration { get; set; }
        public string CVV2 { get; set; }
        public string Credit_Card_Track_1_Data { get; set; }
        public string Credit_Card_Track_2_Data { get; set; }
        public string Credit_Card_Track_3_Data { get; set; }
        public decimal Credit_Card_Amount { get; set; }
        public decimal Credit_Card_Tip { get; set; }
        public string Credit_Card_Approval { get; set; }
        public string Transaction_Number { get; set; }
        public string Name_On_Card { get; set; }
        public string AVS_Street { get; set; }
        public string Postal_Code { get; set; }
        public string Security_Code { get; set; }
        // public int Credit_Loss { get; set; }
        public int Entry_Method { get; set; }
        public DateTime Settlement_Date { get; set; }
        public int Action_Code { get; set; }
        public string Return_Code { get; set; }
        public string Response_Code { get; set; }
        public string Reference_Number { get; set; }
        public string Batch_Number { get; set; }
        public string Retrieval_Reference_Code { get; set; }
        public string AVS_Result_Code { get; set; }
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
        public string CVV2_Result_Code { get; set; }
        public string Added_By { get; set; }
        public DateTime Added { get; set; }
        public string Action { get; set; }

    }
}
