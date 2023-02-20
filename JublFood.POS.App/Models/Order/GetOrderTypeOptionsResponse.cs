namespace JublFood.POS.App.Models.Order
{
    public class GetOrderTypeOptionsResponse
    {
        public OrderTypeOptionsResult Result { get; set; }
    }   

    public class OrderTypeOptionsResult
    {
        public string ResponseStatusCode { get; set; }
        public string ResponseStatus { get; set; }
        public OrderTypeOptions OrderTypeOptions { get; set; }
    }

    public class OrderTypeOptions
    {
        public string OrderTypeCode { get; set; }
        public string OrderTypeDescription { get; set; }
        public int PrintLabelEventCode { get; set; }
        public string PrintLabelEventDescription { get; set; }
        public int PrintReceiptEventCode { get; set; }
        public string PrintReceiptEventDescription { get; set; }
    }
}
