using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JublFood.POS.App.Models.Catalog
{
   public class CatalogBusinessUnit
    {
        public string Location_Code { get; set; }
        public string BU_Code { get; set; }
        public string Description { get; set; }
        public string Order_Description { get; set; }
        public Int32 Display_Order { get; set; }
        public bool Visible { get; set; }
        public bool IsDefault { get; set; }
        public string BU_Image { get; set; }

    }
}
