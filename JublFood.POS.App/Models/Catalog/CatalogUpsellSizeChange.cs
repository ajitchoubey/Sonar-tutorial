using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JublFood.POS.App.Models.Catalog
{
    public class CatalogUpsellSizeChange
    {
        public long ID { get; set; }
        public string Location_Code { get; set; }
        public int Upsell_RuleId { get; set; }
        public string Current_Size_Code { get; set; }
        public string New_Size_Code { get; set; }
        public bool Status { get; set; }
    }
}
