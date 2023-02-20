using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JublFood.POS.App.Models.Payment
{
    public class CreditCardTrackingResponse
    {
        public string Response_Status { get; set; }
        public string Response_Message { get; set; }
        public int Transaction_Number { get; set; }
    }
}
