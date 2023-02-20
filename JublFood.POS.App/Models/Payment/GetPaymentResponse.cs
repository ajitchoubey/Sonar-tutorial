namespace JublFood.POS.App.Models.Payment
{
    public class GetPaymentResponse
    {
        public string ResponseStatus { get; set; }
        public string ResponseMessage { get; set; }
        public int? TransactionStatusCode { get; set; }
    }
}
