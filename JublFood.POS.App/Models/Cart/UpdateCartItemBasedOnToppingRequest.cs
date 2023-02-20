using System;

namespace JublFood.POS.App.Models.Cart
{
    public class UpdateCartItemBasedOnToppingRequest
    {
        public string CartId { get; set; }
        public string Menu_Code { get; set; }
        public string Description { get; set; }
        public Nullable<Boolean> MenuItemType { get; set; }
        public int LineNumber { get; set; }
    }
}
