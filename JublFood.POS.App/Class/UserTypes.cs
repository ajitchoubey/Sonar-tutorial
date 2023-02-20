using JublFood.POS.App.Models;
using JublFood.POS.App.Models.Cart;
using JublFood.POS.App.Models.Catalog;
using System.Collections.Generic;

namespace JublFood.POS.App.Class
{
    public static class UserTypes
    {
        public enum ToppingSizes
        {
            Light,
            Single,
            Extra,
            Double,
            Triple
        }

        public enum OrderCompletionState
        {
            OrderComplete,
            OrderNotComplete,
            OpenOrder,
            OpenCustomer
        }

        public enum ReasonGroupID
        {
            BadOrder = 1,
            VoidOrder = 2,
            AbandonOrder = 3,
            Coupon = 4,
            CookingInstruction = 5,
            Remake = 11,
            CashDrawerLockUnlock = 13,
            CashDrawerOpen = 14

        }

        public class ToppingCollection
        {
            //public List<CatalogToppings> currentToppings { get; set; }
           public List<Topping> currentToppings { get; set; }
            public int MaxButtonsPerPage { get; set; }
            public int TotalPages { get; set; }
            public int currentPage { get; set; }
            public CatalogOptionGroups currentCatalogOptionGroups { get; set; }
            public List<PizzaTopping> pizzaToppings { get; set; }
            public bool MenuItemType1stHalf { get; set; }
            public bool MenuItemType2ndHalf { get; set; }
        }

        public enum ItemParts
        {
            Whole,
            FirstHalf,
            SecondHalf
        }

        public const string cstrLight_Code = "~";
        public const string cstrExtra_Code = "+";
        public const string cstrDouble_Code = "2";
        public const string cstrTriple_Code = "3";

        public const string TabSpace = "    ";
        public const string ItemReasonPrefix = "»";

        public enum APIType
        {
            Cart,
            Catalog,
            Customer,
            Order,
            Employee,
            Payment,
            Printing
        }

        public class SpecialtyItems
        {
            public string SpecialtyCodeWhole { get; set; }
            public string SpecialtyCode1stHalf { get; set; }
            public string SpecialtyCode2ndHalf { get; set; }            
        }
    }

    public class CartIndex
    {
        public int Index { get; set; }
        public int LineNumber { get; set; }
        public int Combo_Group { get; set; }
    }

    

}
