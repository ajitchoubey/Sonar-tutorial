using System;

namespace JublFood.POS.App.Models.Catalog
{
          
    public class CatalogMenuItemSizes
    {
        public String Location_Code { get; set; }
        public String Menu_Code { get; set; }
        public String Size_Code { get; set; }
        public String Description { get; set; }
        public Int16 Display_Order { get; set; }
        public Int16 Status_Code { get; set; }
        public Decimal Bottle_Deposit { get; set; }
        public Decimal Price { get; set; }
        public Decimal Price2 { get; set; }
        public String Barcode { get; set; }
        public Boolean Default_Size { get; set; }
        public Boolean Price_By_Weight { get; set; }
        public double Tare_Weight { get; set; }
        public Boolean Open_Value_Card { get; set; }
        public Decimal Min_Amount_Open_Value_Card { get; set; }
        public Decimal Max_Amount_Open_Value_Card { get; set; }
        public bool Enabled { get; set; }
        public string Order_Type_Code { get; set; }
        public bool EnabledInv { get; set; }
    }
}
