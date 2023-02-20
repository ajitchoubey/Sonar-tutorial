namespace JublFood.POS.App.Models.Catalog
{
    public class SpecialInfoResponse
    {
        public string ResponseStatus { get; set; }
        public string ResponseStatusCode { get; set; }
      public    SpecialInformation specialInformation { get; set; }

    }
    public class SpecialInformation
    {
        public string Notes { get; set; }
    }
}
