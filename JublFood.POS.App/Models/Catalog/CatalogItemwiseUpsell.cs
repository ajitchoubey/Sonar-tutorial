using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JublFood.POS.App.Models.Catalog
{
    public class CatalogItemwiseUpsell
    {
		public long ID { get; set; }
		public string Location_Code { get; set; }
		public string Menu_Code { get; set; }
		public string Upsell_Type { get; set; }
		public int Upsell_RuleId { get; set; }
		public bool Status { get; set; }
		public int Priority { get; set; }
		public string Action { get; set; }
	}
}
