using System;

namespace JublFood.POS.App.Models.Catalog
{
    public class CatalogSpecialtyPizzas
    {
        public String Location_Code { get; set; }
        public String Menu_Code { get; set; }
        public String Menu_Category_Code { get; set; }
        public String Description { get; set; }
        public String Order_Description { get; set; }
        public Int32 Display_Order { get; set; }
        public String Menu_Category_Display_Order { get; set; }
        public Nullable<Int16> Taxable { get; set; }
        public Nullable<Boolean> Royalty_YN { get; set; }
        public Nullable<Boolean> Prepared_YN { get; set; }
        public Nullable<Boolean> Pizza_YN { get; set; }
        public Nullable<Boolean> Specialty_Pizza { get; set; }
        public Int32 Kitchen_Device_Count { get; set; }
        public Int16 Status_Code { get; set; }
        public string Menu_Item_Image { get; set; }
        public Nullable<Boolean> Text_Color { get; set; }
        public Nullable<Boolean> Print_Label { get; set; }
        public Boolean Track_Employee_Sales { get; set; }        
        public String Specialty_Pizza_Code { get; set; }
        public Decimal Price { get; set; }
        public Decimal Price2 { get; set; }
        public Nullable<Boolean> Print_Nutritional_Label { get; set; }
        public Nullable<Boolean> MenuItemType { get; set; }
    }
}
