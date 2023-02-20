using System;

namespace JublFood.POS.App.Models.Cart
{
    public class ItemGiftCard
    {
        public string CartId { get; set; }
        public string Location_Code{ get; set; }
        public long Order_Number{ get; set; }
        public DateTime Order_Date{ get; set; }
        public int Line_Number{ get; set; }
        public int Sequence{ get; set; }
        public byte Gift_Card_Sequence{ get; set; }
        public byte Gift_Card_Transaction_Type{ get; set; }
        public string Gift_Card_Account{ get; set; }
        public Double Gift_Card_Amount{ get; set; }
        public string Gift_Card_Approval{ get; set; }
        public string Transaction_Number{ get; set; }
        public byte Entry_Method{ get; set; }
        public byte Action_Code{ get; set; }
        public string Return_Code{ get; set; }
        public string Response_Code{ get; set; }
        public string Reference_Number{ get; set; }
        public string Batch_Number{ get; set; }
        public string Retrieval_Reference_Code{ get; set; }
        public string Response_Text{ get; set; }
        public string Comment{ get; set; }
        public string Internal_Seq_Number{ get; set; }
        public string Result_Code{ get; set; }
        public string Added_By{ get; set; }
        public bool New_Gift_Card{ get; set; }
        public bool Gift_Card_Processed{ get; set; }
        public string Track1Data{ get; set; }
        public string Track2Data{ get; set; }
    }
}
