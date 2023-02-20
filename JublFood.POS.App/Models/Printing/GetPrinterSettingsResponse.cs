namespace JublFood.POS.App.Models.Printing
{
    public class GetPrinterSettingsResponse
    {
        public GetPrinterSettingsResult Result { get; set; }        
    }

    public class GetPrinterSettingsResult
    {
        public string ResponseStatusCode { get; set; }
        public string ResponseStatus { get; set; }
        public PrintField PrintField { get; set; }
    }

    public class PrintField
    {
        public string StorePhone { get; set; }
        public string LicensedCompany { get; set; }
        public string ReceiptMessageTop { get; set; }
        public string ReceiptMessageBottom { get; set; }
        public int QuantityforSingleSlip { get; set; }
        public int ItemsPerLabel { get; set; }
        public bool MakeLinePrintersExist { get; set; }
        public string StoreName { get; set; }
        public string Workstation_Name { get; set; }
        public string PrinterName { get; set; }
    }
}
