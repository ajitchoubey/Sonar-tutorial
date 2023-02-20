using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JublFood.POS.App.Models.Printing
{
    public class PrintAbandedOrderRequest
    {
        public string Location_Codes { get; set; }
        public long Workstation_Id { get; set; }
        public DateTime Order_Date { get; set; }
        public long Order_number { get; set; }
        public string Emp_Code { get; set; }
       
    }
}
