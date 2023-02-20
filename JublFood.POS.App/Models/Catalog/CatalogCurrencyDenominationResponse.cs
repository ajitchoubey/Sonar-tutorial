using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JublFood.POS.App.Models.Catalog
{
    class CatalogCurrencyDenominationResponse
    {
        public string ResponseStatus { get; set; }
        public string ResponseStatusCode { get; set; }
        public List<CatalogCurrencyDenomination> catalogCurrencyDenomination { get; set; }

    }
    public class CatalogCurrencyDenomination
    {
        public string Location_Code { get; set; }
        public string Currency_Code { get; set; }
        public string Bill_Code { get; set; }
        public string Order_Description { get; set; }
        public decimal Amount { get; set; }
        public int Display_Order { get; set; }

    }

}
