using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JublFood.POS.App.Models.Order
{
    
    class CustomerOrderRemakeResponse
    {
        public string ResponseStatus { get; set; }
        public string ResponseStatusCode { get; set; }

        public List<CustomerOrderRemake> customerOrderRemake { get; set; }
    }
    public class CustomerOrderRemake
    {
        public long Order_Number { get; set; }
        public DateTime Order_Date { get; set; }
        public string Order_Type { get; set; }
    }
}
