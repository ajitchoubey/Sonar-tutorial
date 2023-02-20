using System.Collections.Generic;

namespace JublFood.POS.App.Models.Catalog
{
    public class CatalogOrderPayTypeCodeResponse
    {
        public string ResponseStatus { get; set; }
        public string ResponseStatusCode { get; set; }
        public List<CatalogOrderPayTypeCodes> catalogOrderPayTypeCodesForCash { get; set; }
        public List<CatalogOrderPayTypeCodes> catalogOrderPayTypeCodesForDigital { get; set; }
        public List<CatalogOrderPayTypeCodes> catalogOrderPayTypeCodesForCheque { get; set; }
    }
    public class CatalogOrderPayTypeCodes
    {
        public string Location_Code { get; set; }
        public int Language_Code { get; set; }
        public int Order_Pay_Type_Code { get; set; }
        public string Order_Pay_English_Description { get; set; }
        public string Order_Pay_Description { get; set; }
        public int Credit_Card_Code { get; set; }
        public bool Active { get; set; }
        public int Sales_Pay_ID { get; set; }
        public int Credit_Card_ID { get; set; }
        public string Credit_Card_Default_Description { get; set; }
        public string Credit_card_Modified_Description { get; set; }
        public string Vendor_Exe_Name { get; set; }
        public int Vendor_Time_Out { get; set; }
        public string Vendor_Integration_Status { get; set; }
        public int Vendor_Calling_Mode { get; set; }
        public string Sales_Pay_Head { get; set; }
        public bool Sales_Pay_Head_Status { get; set; }
        public bool Sales_Pay_Head_Editable { get; set; }
        public int Sales_Pay_Head_Display_Sequence { get; set; }
        public bool CashRegAllow { get; set; }

    }
}
