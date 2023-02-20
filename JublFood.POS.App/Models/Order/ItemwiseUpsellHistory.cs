using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JublFood.POS.App.Models.Order
{
    public class ItemwiseUpsellHistory
    {
        public string Location_Code { get; set; }
        public DateTime Order_Date { get; set; }
        public Int64 Order_Number { get; set; }
        public int Line_Number { get; set; }
        public string Upsell_Type { get; set; }
        public string Menu_Code { get; set; }
        public string Size_Code { get; set; }
        public decimal Price { get; set; }
        public int New_Line_Number { get; set; }
        public string New_Menu_Code { get; set; }
        public string New_Size_Code { get; set; }        
        public string Topping_Code { get; set; }
        public string Action { get; set; }               
        public string Added_By { get; set; }

    }
}
