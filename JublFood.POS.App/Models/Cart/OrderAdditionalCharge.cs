using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JublFood.POS.App.Models.Cart
{
    public class OrderAdditionalCharge
    {
        public string CartId { get; set; }        
        public string Location_Code { get; set; }
        public DateTime Order_Date { get; set; }        
        public long Order_Number { get; set; }        
        public int Line_Number { get; set; }        
        public int Sequence { get; set; }        
        public int Charge_Id { get; set; }
        public bool IncludeInDeliveryFee { get; set; }
        public decimal Amount { get; set; }
        public int Tax_Category_ID { get; set; }
        public float Tax_1_Rate { get; set; }
        public decimal Tax1_Amount { get; set; }
        public float Tax_2_Rate { get; set; }
        public decimal Tax2_Amount { get; set; }
        public float Tax_3_Rate { get; set; }
        public decimal Tax3_Amount { get; set; }
        public float Tax_4_Rate { get; set; }
        public decimal Tax4_Amount { get; set; }
        public decimal Total { get; set; }
        public string Added_By { get; set; }
        public string Action { get; set; }
        public string ChargeDesc { get; set; }

    }
}
