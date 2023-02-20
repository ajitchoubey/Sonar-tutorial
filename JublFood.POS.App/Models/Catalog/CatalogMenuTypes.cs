namespace JublFood.POS.App.Models.Catalog
{
    public class CatalogMenuTypes
    {
        public string Location_Code { get; set; }
        public int Menu_Type_ID { get; set; }
        public string Menu_Type_Code { get; set; }
        public string Description { get; set; }
        public string Order_Description { get; set; }
        public int Display_Order { get; set; }        
        public string Menu_Type_Image { get; set; }
        public string Default_Menu_Category_Code { get; set; }        
        public int Default_Menu_Type { get; set; }
    }
}
