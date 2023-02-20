using System;

namespace JublFood.POS.App.Models.Order
{
    public class OrderReason
    {
        public string CartId { get; set; }
        public string Location_Code { get; set; }
        public long Order_Number { get; set; }
        public DateTime Order_Date { get; set; }
        public long Reason_Sequence { get; set; }
        public long Reason_Group_Code { get; set; }
        public long Reason_ID { get; set; }
        public string Other_Information { get; set; }
        public bool Deleted { get; set; }
        public string Added_By { get; set; }
        public string Reason_Description { get; set; }
        public string Action { get; set; }
    }
}
