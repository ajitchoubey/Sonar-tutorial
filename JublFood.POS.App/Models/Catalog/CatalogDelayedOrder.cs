namespace JublFood.POS.App.Models.Catalog
{
    public class CatalogDelayedOrder
    {
        public bool IsAfterMidnight { get; set; }
        public bool IsStoreOpened { get; set; }
    }
    public class CatalogDelayedOrderResponse
    {
        public string ResponseStatus { get; set; }
        public string ResponseStatusCode { get; set; }

        public CatalogDelayedOrder DelayedOrder { get; set; }
    }

}
