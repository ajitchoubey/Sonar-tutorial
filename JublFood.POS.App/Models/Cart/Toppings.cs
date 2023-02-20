using System;

namespace JublFood.POS.App.Models.Cart
{
    public class Topping
    {
        public String Menu_Code { get; set; }
        public String Size_Code { get; set; }
        public String Order_Description { get; set; }
        public String Topping_Code { get; set; }
        public String Menu_Item_Image { get; set; }
        public String Text_Color { get; set; }
        public String Kitchen_Display_Order { get; set; }
        public String Amount_Code { get; set; }
        public String Default_Item { get; set; }
        public String Item_Part { get; set; }
        public bool Default { get; set; }
        public Nullable<Boolean> MenuItemType { get; set; }



        public bool WholeDefault { get; set; }
        public string WholeDefaultAmount { get; set; }

        public bool FirstHalfDefault { get; set; }
        public string FirstHalfDefaultAmount { get; set; }

        public bool SecondHalfDefault { get; set; }
        public string SecondHalfDefaultAmount { get; set; }


        public bool SelectedOnWhole { get; set; }
        public string WholeSelectedAmount { get; set; }

        public bool SelectedOnFirstHalf { get; set; }
        public string FirstHalfSelectedAmount { get; set; }

        public bool SelectedOnSecondHalf { get; set; }
        public string SecondHalfSelectedAmount { get; set; }


        public bool RemovedFromWhole { get; set; }
    }
}
