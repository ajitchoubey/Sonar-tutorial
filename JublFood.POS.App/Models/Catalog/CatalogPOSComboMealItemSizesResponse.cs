using System.Collections.Generic;

namespace JublFood.POS.App.Models.Catalog
{
    class CatalogPOSComboMealItemSizesResponse
    {
        public string ResponseStatus { get; set; }
        public string ResponseStatusCode { get; set; }
        public List<CatalogPOSComboMealItemSizes> catalogPOSComboMealItemSizes { get; set; }
    }

    public class CatalogPOSComboMealItemSizes
    {
        public string Location_Code { get; set; }
        public string Combo_Menu_Code { get; set; }
        public string Combo_Size_Code { get; set; }
        public string Menu_Code { get; set; }
        public string Size_Code { get; set; }
        public bool Default { get; set; }
        public string Description { get; set; }
        public int Size_Display_Order { get; set; }
        public decimal Bottle_Deposit { get; set; }
        public decimal Price { get; set; }
        public decimal Price2 { get; set; }
        public string Barcode { get; set; }
        public bool Default_Size { get; set; }
        public int Item_Number { get; set; }
        public bool Price_By_Weight { get; set; }
        public int Menu_Item_Type_Code { get; set; }
        public bool Print_Nutritional_Label { get; set; }
        public float Tare_Weight { get; set; }
    }
}
