namespace JublFood.POS.App.Models.Customer
{
    public class DeleteCallRequest
    {
        public string LocationCode { get; set; }
        public int Line { get; set; }
        public string PhoneNumber { get; set; }

    }
}
