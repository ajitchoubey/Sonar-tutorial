using JublFood.Order.Models;
using JublFood.POS.App.Class;
using JublFood.POS.App.Models;
using JublFood.POS.App.Models.Cart;
using JublFood.POS.App.Models.Catalog;
using JublFood.POS.App.Models.Customer;
using JublFood.POS.App.Models.Employee;
using JublFood.POS.App.Models.Order;
using JublFood.POS.App.Models.Payment;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace JublFood.POS.App.Cache
{
    public static class Session
    {
        public static DateTime blankDate = Convert.ToDateTime("01/01/1899");
        public static string _LocationCode { get; set; }//"DPI66585";        
        public static int _WorkStationID;
        public static int _MenuTypeID;
        public static string currentMenuCategoryCode;
        public static Color ToppingSizeLightColor = Color.AntiqueWhite;
        public static Color ToppingSizeSingleColor = Color.LightCoral;
        public static Color ToppingSizeExtraColor = Color.PaleGreen;
        public static Color ToppingSizeDoubleColor = Color.Cyan;
        public static Color ToppingSizeTripleColor = Color.CornflowerBlue;
        public static Color ToppingColor = SystemColors.Control;
        public static Color paytm_lightcolor = Color.FromArgb(0, 185, 241);
        public static Color vegColor = Color.Green;
        public static Color nonVegColor = Color.Red;

        public static Cart cart;
        public static Cart originalcart;
        public static List<CatalogMenuItems> menuItems;
        public static List<CatalogMenuItemSizes> menuItemSizes;
        public static CatalogMenuItems selectedMenuItem;
        public static CatalogMenuItemSizes selectedMenuItemSizes;
        public static UserTypes.ToppingSizes currentToppingSize;
        public static UserTypes.ToppingCollection currentToppingCollection;
        public static ResponsePayment responsePayment;

        public static ResponsePayment originalresponsePayment;

        public static string selectedOrderType = "";
        //public static string selectedAddressType = "";
        public static CatalogCartCaptions CatalogCartCaptions;
        public static string DisplayFormat = "{0:0.00}"; //TO DO //// From Settings
        public static EmployeeResult CurrentEmployee;
        public static string LoginPassword { get; set; }
        //public static SaveCustomerRequest CustomerCollection;
        public static GetCustomerProfile CustomerProfileCollection;
        public static bool FromOrder { get; set; }
        public static string UserID { get; set; }

        public static string TimeClockUserID { get; set; }
        public static string EmployeeCode { get; set; }
        public static string TimeClockEmployeeCode { get; set; }
        public static int TimeClockPasswordResetValue { get; set; }
        public static int PasswordResetValue { get; set; }
        public static string ComputerName { get; set; }
        public static bool FormPayment;        
        public static DateTime SystemDate { get; set; }
        public static int Upsellcnt { get; set; }
        public static int Upsellpayment { get; set; }
        public static DateTime ClockedTimeIn { get; set; }
        public static bool FormClockOpened { get; set; }
        public static bool IsTimerStarted { get; set; }
        public static string CurrentElapsedTime { get; set; }
        public static string PriorityCustomer { get; set; }
        public static List<TicketFields> TicketCollection;
        public static bool LandFromLoginToTimeclock { get; set; }
        public static Color TrainningModeColor = Color.DarkBlue;
        public static Color NormalModeColor = Color.Teal;
        public static OrderResponseData currentOrderResponse;
        public static bool IDClockedIN { get; set; }
        public static UserTypes.ItemParts currentItemPart;
        public static string currentToppingSizeCode;
        public static Color currentToppingColor = Color.Black;
        public static bool pblnModifyDelayedTime { get; set; }
        public static bool pblnModifyingOrder { get; set; }
        public static bool pblnShowModifyScreen { get; set; }
        public static bool IsCallerIDClicked { get; set; }
        public static bool ExitApplication { get; set; }
        public static bool VegOnlySelected { get; set; }
        public static Nullable<Boolean> MenuItemType { get; set; }
        public static Dictionary<string, Nullable<Boolean>> selectedMenuItems { get; set; }
        public static List<Topping> cartToppings { get; set; }
        public static int SelectedLineNumber { get; set; }
        public static List<CatalogCoupons> OrderCoupons;
        public static CatalogCoupons selectedOrderCoupon;
        public static bool ProcessingCombo;
        public static int CurrentComboItem;
        public static int ComboGroup;
        public static int CurrentComboGroup;
        public static List<CatalogPOSComboMealItems> comboMenuItems;
        public static List<CatalogPOSComboMealItemSizes> comboMenuItemSizes;
        public static CatalogPOSComboMealItems selectedComboMenuItem;
        public static CatalogPOSComboMealItemSizes selectedComboMenuItemSizes;
        //public static bool MenuCategoryButtonClicked;
        public static List<CatalogPOSComboMealItemsForButtons> catalogPOSComboMealItemsForButtons;
        public static Color DefaultEntityColor = Color.LightCoral;
        public static OriginalOrderInfo OriginalOrderInfo;
        public static bool ordermodifyform { get; set; }
        public static bool pblnOpenOrder { get; set; }
        public static bool pblnModifyPrint { get; set; }
        public static bool blnCheckLine { get; set; }
        public static bool pblnNewOrderTime { get; set; }
        public static bool pblnAddModPrepared { get; set; }
        public static bool pblnOrderModifications { get; set; }
        public static int pintSendOption { get; set; }
        public static bool pblnCashingOutAfter { get; set; }
        public static int MaxPhoneDigits { get; set; }

        public static string responseMsg { get; set; }
        public static int MaxMenuItemsPerPage;
        public static int currentMenuItemPageNo;
        public static bool m_blnNotesExist { get; set; }
        public static bool m_blnDriverCommentsExist { get; set; }
        public static decimal pcurOriginalTotal { get; set; }
        public static decimal pcurCreditBalanceHolding { get; set; }

        public static bool handleHistorybutton { get; set; }
        public static bool RemakeOrder { get; set; }

        public static List<CatalogCoupons> orderLineCoupons;
        public static bool IsPayClick { get; set; }

        public static List<CatalogOrderPayTypeCodes> orderPayTypeCodes { get; set; }

        public static bool pblnChargeOrder { get; set; }
        public static int MinPhoneDigits { get; set; }

        public static CustomerReqField CustomerReqField { get; set; }
        public static OrderReqField OrderReqField { get; set; }
        public static bool ODC_Tax;
        public static bool OrderTypeAutoSelect;



        public static bool MatchPassword { get; set; }
        public static string blnmodify { get; set; }
        public static bool handleRemakebutton { get; set; }

        public static bool RefreshFromModifyForOrder { get; set; }
        public static bool RefreshFromHistoryForOrder { get; set; }
        public static bool RefreshFromRemakeForOrder { get; set; }
        public static bool RefreshFromModifyForCustomer { get; set; }
        public static bool RefreshFromHistoryForCustomer { get; set; }
        public static bool RefreshFromRemakeForCustomer { get; set; }
        public static DateTime StartTime;
        public static bool handleModify {get; set;}

        public static List<GetAllCities> CitiesAPIResponse { get; set; }
        public static List<StreetLookUp> StreetsAPIResponse { get; set; }
        public static List<ListViewItem> CustomerStreets { get; set; }

        public static bool CartInitiated { get; set; }

        public static bool GoToStartUp { get; set; }

        public static UserTypes.SpecialtyItems SpecialtyItems;
        public static List<CatalogSpecialtyPizzas> SpecialtyPizzasList;

        #region MemoryStore  
        //Should not be cleared on ClearSession
        public static List<CatalogText> catalogTexts { get; set; }
        public static List<CatalogText> ToppingSizes { get; set; }
        public static List<CatalogText> SpecialtyPizzaText { get; set; }
        public static List<CatalogText> ItemParts { get; set; }
        public static CatalogCartCaptions CartCaptions { get; set; }
        public static List<CatalogMenuTypes> MenuTypes { get; set; }
        public static List<CatalogMenuCategory> menuCategories { get; set; }
        public static List<CatalogMenuItems> AllCatalogMenuItems { get; set; }
        public static int EstimatedDeliveryTime { get; set; }
        public static int EstimatedCarryOutTime { get; set; }
        public static string UpsellReminder { get; set; }
        public static List<CatalogOptionGroups> catalogOptionGroups { get; set; }
        public static List<CatalogMenuItemSizes> AllCatalogMenuItemSizes { get; set; }
        public static List<OrderType> OrderType { get; set; }
        public static List<CatalogAddressTypes> CatalogAddressTypes { get;  set; }
        public static List<CatalogControlPropeties> catalogControlPropeties { get;  set; }
        public static List<CatalogControlText> catalogControlText { get; set; }
        public static List<CatalogAllButtonText> catalogAllButtonText { get; set; }
        public static StreetLookUpResponse StreetLookUpResponseAll { get; set; }
        public static CatalogOrderPayTypeCodeResponse catalogOrderPayTypeCodeResponse { get; set; }        
        public static List<CatalogBusinessUnit> businessUnits { get; set; }
        public static CatalogUpsellData catalogUpsellData { get; set; }
        #endregion

        public static int TotalPageMenuCategory { get; set; }
        public static string SelectedBusinessUnit { get; set; }
        public static int TotalPageMenuItems { get; set; }
        public static List<CatalogSourceName> SourceName { get; set; }
        public static string selectedSourceName { get; set; }       
        public static decimal AggregatorGSTValue { get; set; }
        public static List<CombosForUpsell> CombosAvailableForUpsell { get; set; }
        public static int UpsellPopupCount { get; set; }
        public static string UpsellPopupLastResponse { get; set; }
        public static List<ItemwiseUpsellPromptRow> itemwiseUpsellPromptMatrix { get; set; }
        public static bool CashDrawerOpen { get; set; }
        public static string WorkstationIP { get; set; }
        public static int CashDrawerTimeOut { get; set; }
        public static int CashDrawerOpenCloseReasonID { get; set; }
        public static List<CashDrawerReason> cashDrawerReasonsForOrder { get; set; }
        public static List<ItemwiseUpsellHistory> itemwiseUpsellHistory { get; set; }
        public static bool PreventItemwiseUpsell { get; set; }
        public static List<int> LineNumbersFromHistory { get; set; }
}

}
