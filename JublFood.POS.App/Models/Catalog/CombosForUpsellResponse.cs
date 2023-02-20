using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JublFood.POS.App.Models.Catalog
{
    public class CombosForUpsellResponse
    {
        public string ResponseStatus { get; set; }
        public string ResponseStatusCode { get; set; }

        public List<CombosForUpsell> combosForUpsell { get; set; }
    }

    public class CombosForUpsell
    {
        public string Combo_Menu_Code { get; set; }
        public string Combo_Size_Code { get; set; }
        public string Combo_Description { get; set; }
        public string Combo_Size_Description { get; set; }
    }
}
