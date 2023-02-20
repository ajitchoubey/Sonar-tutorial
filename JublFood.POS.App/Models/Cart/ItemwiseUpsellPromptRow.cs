using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JublFood.POS.App.Models.Cart
{
    public class ItemwiseUpsellPromptRow
    {
        public string Menu_Code { get; set; }
        public int Priority { get; set; }
        public string Action { get; set; }
        public int LineNumber { get; set; }
    }
}
