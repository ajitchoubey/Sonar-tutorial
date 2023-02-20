namespace JublFood.POS.App.Models.Order
{
    public class TicketFields
    {       
        public string LineType { get; set; }
        public string ItemCode { get; set; }
        public string Quantity { get; set; }
        public string ItemDescription { get; set; }
        public string Price { get; set; }
        public string DoublesGroup { get; set; }
        public int OrderLine_Line_Number { get; set; }
        public string Group_Code { get; set; }
        public int Combo_Group { get; set; }
        public int Combo_Item_Number { get; set; }
        public bool Line_Complete { get; set; }
        public string Order_Line_Adjustments { get; set; }

    }
}
