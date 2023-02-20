using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JublFood.POS.App.Models.Catalog
{
    public class CashDrawerInfoDto
    {
        public string Location_code { get; set; }
        public string Workstation_Name { get; set; }

        public int device_id { get; set; }
        public bool cash_register_status { get; set; }
        public bool cash_register_lock { get; set; }

        public string Cash_Register_Locked_By { get; set; }
    }
}
