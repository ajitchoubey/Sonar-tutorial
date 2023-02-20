using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JublFood.POS.App.Models.Order
{
    public class CashDrawerReason
    {
        public int Reason_Group_Code { get; set; }
        public int Reason_ID { get; set; }
        public byte iStatus { get; set; }
        public string Added_By { get; set; }
        public int Workstation_id { get; set; }
        public Int64 Order_Number { get; set; }
    }
}
