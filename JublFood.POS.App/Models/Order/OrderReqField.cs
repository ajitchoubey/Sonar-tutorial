using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JublFood.POS.App.Models.Order
{
   public class OrderReqField
    {
        public bool btn_Minus { get; set; }
        public bool btn_Plus { get; set; }
        public string Text { get; set; }
        public bool btn_Instructions { get; set; }
        public bool btn_Up { get; set; }
        public bool btn_Down { get; set; }
        public bool btn_Coupons { get; set; }
        public bool btn_Quantity { get; set; }
        
    }
}
