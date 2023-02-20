using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JublFood.POS.App.Models.Customer
{
    public class CustomerReqField
    {
        public bool ltxtSuite { get; set; }
        public bool ltxtPhone_Ext { get; set; }
        public bool cmdNewExt { get; set; }
        public bool txtName { get; set; }
        public bool txtSuite { get; set; }
        public bool ltxtCity { get; set; }
        public string tdbmPhone_Number { get; set; }
       
    }
}
