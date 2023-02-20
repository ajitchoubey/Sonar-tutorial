namespace JublFood.POS.App.Models.Customer
{
    public class SaveCustomerResponse
    {
        public SaveCustomerResult Result { get; set; }
    }

    public class SaveCustomerResult
    {
        public string ResponseStatus { get; set; }
        public string ResponseStatusCode { get; set; }

    }
}
