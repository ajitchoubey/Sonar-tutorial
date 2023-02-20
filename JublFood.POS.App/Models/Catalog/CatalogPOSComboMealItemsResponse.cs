using System.Collections.Generic;

namespace JublFood.POS.App.Models.Catalog
{
    public class CatalogPOSComboMealItemsResponse
    {
        public string ResponseStatus { get; set; }
        public string ResponseStatusCode { get; set; }
        public List<CatalogPOSComboMealItems> catalogPOSComboMealItems { get; set; }
    }
    public class CatalogPOSComboMealItems
    {
        public string Location_Code { get; set; }
        public string Combo_Menu_Code { get; set; }
        public string Combo_Size_Code { get; set; }
        public string Menu_Code { get; set; }
        public int Item_Number { get; set; }
        public bool Default { get; set; }
        public int Quantity { get; set; }
        public decimal Discount { get; set; }
        public decimal MinAmount { get; set; }
        public decimal MaxAmount { get; set; }
        public string Order_Description { get; set; }
        public int Menu_Item_Display_Order { get; set; }
        public int Taxable { get; set; }
        public bool Royalty_YN { get; set; }
        public bool Prepared_YN { get; set; }
        public bool Pizza_YN { get; set; }
        public bool Specialty_Pizza { get; set; }
        public int Kitchen_Device_Count { get; set; }
        public int Menu_Item_Image { get; set; }
        public bool Text_Color { get; set; }
        public bool Print_Label { get; set; }
        public bool Print_Ticket { get; set; }
        public bool Prompt_For_Size { get; set; }
        public string Specialty_Pizza_Code { get; set; }
        public string Orig_Menu_Code { get; set; }
        public bool Prompt_For_Menu_Item { get; set; }
        public bool Prompt_For_Sizes { get; set; }
        public bool Prompt_For_Options { get; set; }
        public bool Prompt_For_Attributes { get; set; }
        public int NumberOfSizes { get; set; }
        public int NumberOfAttributes { get; set; }
        public int NumberOfOptions { get; set; }
        public bool Print_Nutritional_Label { get; set; }
        public bool MenuItemType { get; set; }
    }
}
