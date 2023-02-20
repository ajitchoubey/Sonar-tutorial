using System;
using System.Collections.Generic;

namespace JublFood.POS.App.Models.Cart
{
    public class CartItem
    {
        public string CartId { get; set; }
        public string Location_Code { get; set; }
        public long Order_Number { get; set; }
        public DateTime Order_Date { get; set; }
        public int Line_Number { get; set; }
        public int Sequence { get; set; }
        public int Order_Line_Status_Code { get; set; }
        public DateTime Oven_Time { get; set; }
        public float Quantity { get; set; }
        public string Combo_Menu_Code { get; set; }
        public string Combo_Size_Code { get; set; }
        public int Combo_Group { get; set; }
        public long Menu_Type_ID { get; set; }
        public string Menu_Category_Code { get; set; }
        public string Menu_Code { get; set; }
        public string Size_Code { get; set; }
        public string Instruction_String { get; set; }
        public string Coupon_Code { get; set; }
        public bool Coupon_Taxable { get; set; }
        public int Coupon_Type_Code { get; set; }
        public bool Coupon_Adjustment { get; set; }
        public Decimal Coupon_Amount { get; set; }
        public Decimal Coupon_Min_Price { get; set; }
        public Decimal Price { get; set; }
        public Decimal Menu_Price { get; set; }
        public Decimal Menu_Price2 { get; set; }
        public Decimal Bottle_Deposit { get; set; }
        public bool Deleted { get; set; }
        public string Topping_Codes { get; set; }
        public string Topping_Descriptions { get; set; }
        public bool Calculate_IFC { get; set; }
        public int Doubles_Group { get; set; }
        public DateTime Order_Saved_Time { get; set; }
        public int Modifying { get; set; }
        public string Orig_Menu_Code { get; set; }
        public Decimal Order_Coupon_Amount { get; set; }
        public Decimal Order_Line_Coupon_Amount { get; set; }
        public Decimal Order_Line_No_Tax_Discount { get; set; }
        public Decimal Order_Line_Tax_Discount { get; set; }
        public Decimal Order_No_Tax_Discount { get; set; }
        public Decimal Order_Tax_Discount { get; set; }
        public Decimal Credit_Discount { get; set; }
        public Decimal Order_Line_Taxable_Sale1 { get; set; }
        public Decimal Order_Line_Taxable_Sale2 { get; set; }
        public Decimal Order_Line_Non_Taxable_Sale { get; set; }
        public Decimal Order_Line_Tax1 { get; set; }
        public Decimal Order_Line_Tax2 { get; set; }
        public Decimal Order_Line_Total { get; set; }
        public string Added_By { get; set; }
        public Decimal Adjustment { get; set; }
        public string Coupon_Description { get; set; }
        public bool Manual { get; set; }
        public string Menu_Description { get; set; }
        public int Menu_Item_Taxable { get; set; }
        public bool Prepared { get; set; }
        public bool Pizza { get; set; }
        public string Size_Description { get; set; }
        public Decimal SubTotal { get; set; }
        public int Kitchen_Device_Count { get; set; }
        public bool Item_Modified { get; set; }
        public bool New_Item { get; set; }
        public bool Topping_Group_Price_By_Number { get; set; }
        public string Topping_Group_Code { get; set; }
        public bool Send_To_Kitchen_Display { get; set; }
        public Decimal Combo_Discount { get; set; }
        public Decimal Combo_MinAmount { get; set; }
        public Decimal Combo_MaxAmount { get; set; }
        public int Combo_Item_Number { get; set; }
        public bool Prompt_For_Size { get; set; }
        public string Topping_String { get; set; }
        public bool Order_Line_Complete { get; set; }
        public bool Combo_Prompt_Menu_Item { get; set; }
        public bool Combo_Prompt_Size { get; set; }
        public bool Combo_Prompt_Options { get; set; }
        public bool Combo_Prompt_Attributes { get; set; }
        public bool Size_Chosen { get; set; }
        public bool Menu_Item_Choosen { get; set; }
        public Decimal Base_Price { get; set; }
        public Decimal Base_Price2 { get; set; }
        public float Topping_Count { get; set; }
        public bool Doubles_Bypassed { get; set; }
        public int NumberOfSizes { get; set; }
        public int NumberOfAttributes { get; set; }
        public int NumberOfOptions { get; set; }
        public bool Price_By_Weight { get; set; }
        public float Tare_Weight { get; set; }
        public int Menu_Item_Type_Code { get; set; }
        public bool Open_Value_Card { get; set; }
        public Decimal Min_Amount_Open_Value_Card { get; set; }
        public Decimal Max_Amount_Open_Value_Card { get; set; }
        public bool Print_Nutritional_Label { get; set; }
        public int itemlist { get; set; }
        public int qty { get; set; }
        public int Totalqty { get; set; }
        public string Description { get; set; }
       
        public string Action { get; set; }
        public bool Specialty_Pizza { get; set; }
        public string Specialty_Pizza_Code { get; set; }
        public bool PromptDoubles { get; set; }
        public bool isEDVCoupon { get; set; }
        public bool isUpsellCoupon { get; set; }

        public List<ItemAttributeGroup> itemAttributeGroups { get; set; }
        public List<ItemAttribute> itemAttributes { get; set; }        
        public List<ItemGiftCard> itemGiftCards { get; set; }
        public List<ItemOptionGroup> itemOptionGroups { get; set; }
        public List<ItemOption> itemOptions { get; set; }
        public List<ItemReason> itemReasons { get; set; }
        public List<ItemSpecialtyPizza> itemSpecialtyPizzas { get; set; }
        public ItemUDT itemUDT { get; set; }
        public Nullable<Boolean> MenuItemType { get; set; }

    }
}