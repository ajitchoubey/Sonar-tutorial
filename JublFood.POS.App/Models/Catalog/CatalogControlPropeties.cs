using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JublFood.POS.App.Models.Catalog
{
    public class CatalogControlPropeties
    {
        public string Location_Code { get; set; }
        public string Form { get; set; }
        public string Control { get; set; }
        public Int32 X_Left { get; set; }
        public Int32 Y_Top { get; set; }
        public Int32 Tab_Order { get; set; }
        public Boolean Visibility { get; set; }
        public Boolean Required { get; set; }
        public Int32 Width { get; set; }
        public Int32 Height { get; set; }
        public Int32 MaxLength { get; set; }
        public Int32 MaxLengthCap { get; set; }
        public Int32 Lookup { get; set; }
        public string Label_Name { get; set; }
    }
}
