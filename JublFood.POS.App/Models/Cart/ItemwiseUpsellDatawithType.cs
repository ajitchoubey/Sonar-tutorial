using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JublFood.POS.App.Models.Cart
{
    public class ItemwiseUpsellDatawithType
    {
        public string UpsellType { get; set; }
        public string MenuCode { get; set; }
        public int Priority { get; set; }
        public int LineNumber { get; set; }
        public List<attribute> AttributeList { get; set; }
    }

    public class attribute
    {
        public string Code { get; set; }
        public string Description { get; set; }
        public bool IsEnabled { get; set; }
    }
}
