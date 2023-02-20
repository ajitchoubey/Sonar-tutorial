using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JublFood.POS.App.Models.Order
{
    public class CashDrawerLockUnlockRequest
    {
        public string workStationName { get; set; }
        public int workstationID { get; set; }
        public string employeeID { get; set; }
        public bool lockStatus { get; set; }
        public int reasonID { get; set; }
        public int reasonGrp { get; set; }
    }
}
