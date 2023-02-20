using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JublFood.POS.App.Models.Catalog
{
    class CashDrorStatusRequest
    {
        public String Workstation_Name { get; set; }
        public String Employee_Code { get; set; }
        public Int16 Flag { get; set; }

    }
}
