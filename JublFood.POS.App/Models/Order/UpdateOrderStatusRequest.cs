using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JublFood.POS.App.Models.Order
{
   public class UpdateOrderStatusRequest
    {
        public string Location_Code { get; set; }
        public long Order_Number { get; set; }
        public DateTime Order_Date { get; set; }
        public string Employee_Code { get; set; }
        public bool Order_Status { get; set; }
    }
}
