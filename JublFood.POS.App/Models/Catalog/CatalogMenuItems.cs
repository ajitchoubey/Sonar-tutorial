using System;

namespace JublFood.POS.App.Models.Catalog
{
    public class CatalogMenuItems
    {        
        public String Location_Code { get; set; }
        public String Menu_Category_Code { get; set; }
        public String Menu_Code { get; set; }
        public String Description { get; set; }
        public String Order_Description { get; set; }
        public Int32 Display_Order { get; set; }        
        public Nullable<Int16> Taxable { get; set; }
        public Nullable<Boolean> Royalty_YN { get; set; }
        public Nullable<Boolean> Prepared_YN { get; set; }
        public Nullable<Boolean> Pizza_YN { get; set; }
        public Nullable<Boolean> Specialty_Pizza { get; set; }
        public Nullable<Boolean> Text_Color { get; set; }
        public Boolean Prompt_For_Size { get; set; }
        public Nullable<Boolean> Combo_Meal { get; set; }
        public Int16 Menu_Item_Type_Code { get; set; }
        public string Menu_Item_Image { get; set; }
        public Nullable<Boolean> Print_Nutritional_Label { get; set; }
        public Nullable<Boolean> MenuItemType { get; set; }

        public Int32 Kitchen_Device_Count { get; set; }
        public String Specialty_Pizza_Code { get; set; }
        public String Orig_Menu_Code { get; set; }
        public Int32 NumberOfSizes { get; set; }
        public Int32 NumberOfAttributes { get; set; }
        public Int32 NumberOfOptions { get; set; }
        public bool Enabled { get; set; }
        public string Order_Type_Code { get; set; }
        public bool EnabledInv { get; set; }
    }
}
