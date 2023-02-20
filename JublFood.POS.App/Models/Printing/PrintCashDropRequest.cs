using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JublFood.POS.App.Models.Printing
{
    public class PrintCashDropRequest
    {
        public string Location_Codes { get; set; }
        public long Workstation_Id { get; set; }
        public string Emp_Code { get; set; }
        public string Manager_Code { get; set; }
        public decimal Amount { get; set; }
        public Boolean blnCashDrop { get; set; }
    }
}
