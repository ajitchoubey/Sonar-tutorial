using System.Drawing;

namespace JublFood.POS.App.Models.Cart
{
    public class PizzaTopping
    {
        public int ButtonIndex { get; set; }
        public Color ButtonColor { get; set; }
        public string ItemPart { get; set; }
        public string DefaultTopping { get; set; }
        public string ButtonCaption { get; set; }
        public int KitchenDisplayOrder { get; set; }
        public int ItemPartInt { get; set; }
        public string ToppingCode { get; set; }
        
    }
}
