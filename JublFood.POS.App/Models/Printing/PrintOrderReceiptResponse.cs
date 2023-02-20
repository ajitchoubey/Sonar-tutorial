namespace JublFood.POS.App.Models.Printing
{
    public class PrintOrderReceiptResponse
    {
        public PrintOrderReceiptResult Result { get; set; }
    }

    public class PrintOrderReceiptResult
    {
        public string ResponseStatusCode { get; set; }
        public string ResponseStatus { get; set; }
    }
}
