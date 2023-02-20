using System;

namespace JublFood.POS.App.Models.Printing
{
    public class PrintOrderReceiptGeneralRequest
    {
        public string LocationCode { get; set; }
        public DateTime Order_Date { get; set; }
        public long Order_number { get; set; }
       
        public bool blnModifying { get; set; }
        
        public bool blnOrderModifications { get; set; }
    }
}
