using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JublFood.POS.App.Models.Catalog
{
    public class CashDrawerInfoResponse
    {
        public string ResponseStatus { get; set; }
        public string ResponseStatusCode { get; set; }
        public List<CashDrawerInfoDto> cashDrawerInfo { get; set; }
    }
}
