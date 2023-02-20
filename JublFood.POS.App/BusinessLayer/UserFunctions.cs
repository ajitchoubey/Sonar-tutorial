using Jublfood.AppLogger;
using JublFood.POS.App.API;
using JublFood.POS.App.Cache;
using JublFood.POS.App.Class;
using JublFood.POS.App.Models.Cart;
using JublFood.POS.App.Models.Catalog;
using JublFood.POS.App.Models.Customer;
using JublFood.POS.App.Models.Employee;
using JublFood.POS.App.Models.Order;
using JublFood.POS.App.Models.Payment;
using JublFood.POS.App.Models.Printing;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using JublFood.POS.App;
using System.Management;
using System.Diagnostics;
using System.IO;

namespace JublFood.POS.App.BusinessLayer
{
    public static class UserFunctions
    {
        //public const int cintPassword = 114;

        //public static void Setup()
        //{
        //    SystemSettings.LoadSettings("");
        //    Session._LocationCode = SystemSettings.LocationCodes.LocationCode;
        //    Session.SystemDate = Convert.ToDateTime(SystemSettings.LocationCodes.SystemDate);
        //    Session._WorkStationID = Convert.ToInt32(SystemSettings.WorkStationSettings.plngWorkstation_ID);
        //    Session.ComputerName = Session.ComputerName;
        //}

        public static void CheckSetup()
        {
            SystemSettings.CheckSettings(Session._LocationCode);
            if (Session._LocationCode == null || Session._LocationCode == "")
            {
                Session._LocationCode = SystemSettings.LocationCodes.LocationCode;
                Session.SystemDate = Convert.ToDateTime(SystemSettings.LocationCodes.SystemDate);
                Session._WorkStationID = Convert.ToInt32(SystemSettings.WorkStationSettings.plngWorkstation_ID);
                //Session.ComputerName = Dns.GetHostName();
                
            }

        }

        public static void GetCartUpsell(CatalogUpsellMenu catalogUpsellMenu, int quantity)
        {
            CheckCart();
            Cart cartLocal = (new Cart().GetCart());
            CartItem cartItemLocal = new CartItem();
            cartItemLocal.Location_Code = catalogUpsellMenu.location_code;
            cartItemLocal.Order_Date = Session.SystemDate;
            cartItemLocal.Line_Number = 1;
            cartItemLocal.Sequence = 1;
            cartItemLocal.Size_Code = catalogUpsellMenu.Size_Code;
            cartItemLocal.Action = "A";
            cartItemLocal.Price = catalogUpsellMenu.price;
            cartItemLocal.Menu_Price = catalogUpsellMenu.price;
            cartItemLocal.Menu_Price = catalogUpsellMenu.price;
            cartItemLocal.Menu_Price2 = catalogUpsellMenu.price;
            cartItemLocal.Base_Price = cartItemLocal.Menu_Price;
            cartItemLocal.Base_Price2 = cartItemLocal.Menu_Price2;

            cartItemLocal.Menu_Description = catalogUpsellMenu.order_description;
            cartItemLocal.Menu_Code = catalogUpsellMenu.Menu_Code;
            cartItemLocal.Menu_Category_Code = catalogUpsellMenu.Menu_Category_Code;
            cartItemLocal.Size_Description = catalogUpsellMenu.description;
            cartItemLocal.Quantity = quantity;
            cartItemLocal.Added_By = Session.CurrentEmployee.LoginDetail.EmployeeCode;
            cartLocal.cartItems.Add(cartItemLocal);

            cartLocal.cartHeader = Session.cart.cartHeader;
            CartFunctions.UpdateCustomer(cartLocal);
            Session.cart = APILayer.Add2Cart(cartLocal);
        }
        
        

        public static void CheckCart()
        {
            if (Session.cart == null)
            {
                Session.cart = (new Cart()).GetCart();
            }
        }



        public static int GetMaximumButtonBestFit(int ContainterWidth, int ContianerHeigth, int ButtonWidth, int ButtonHeight, int HorizontalSpace, int VerticalSpace)
        {
            //int HButtons = ContainterWidth / (ButtonWidth + HorizontalSpace);
            //int VButtons = ContianerHeigth / (ButtonHeight + VerticalSpace);
            int HButtons = ContainterWidth / ButtonWidth;
            int VButtons = ContianerHeigth / ButtonHeight;

            return (HButtons * VButtons);
        }

        public static Color GetColorbyAmountCode(string AmountCode)
        {
            if (AmountCode == "~")
                return Session.ToppingSizeLightColor;
            else if (AmountCode == "")
                return Session.ToppingSizeSingleColor;
            else if (AmountCode == "+")
                return Session.ToppingSizeExtraColor;
            else if (AmountCode == "2")
                return Session.ToppingSizeDoubleColor;
            else if (AmountCode == "3")
                return Session.ToppingSizeTripleColor;
            else if (AmountCode == "-")
                return Color.Gray;
            else
                return Session.ToppingSizeSingleColor;
        }                 

        public static void AutoSelectOrderType(UC_CustomerOrderBottomMenu uC_CustomerOrderBottomMenu)
        {
           Session.OrderTypeAutoSelect = true;
            if (!String.IsNullOrEmpty(Session.selectedOrderType))
            {
                foreach (Control control in uC_CustomerOrderBottomMenu.Controls)
                {
                    if (control.GetType() == typeof(FlowLayoutPanel))
                    {
                        foreach (Control _control in control.Controls)
                        {
                            if (Convert.ToString(_control.Tag) == Session.selectedOrderType)
                                ((Button)_control).PerformClick();
                        }
                    }
                }
            }
            Session.OrderTypeAutoSelect = false;
        }

        //public static void GetEstimatedTimes()
        //{
        //    Session.EstimatedDeliveryTime = APILayer.GetEstimatedDeliveryTime();
        //    Session.EstimatedCarryOutTime = APILayer.GetEstimatedCarryOutTime();
        //}        

        public static void GoToStartup(Form Currentform)
        {
            try
            {
                Session.IsTimerStarted = false;                
                Form[] forms = Application.OpenForms.Cast<Form>().ToArray();
                foreach (Form thisForm in forms)
                {
                    if (thisForm.Name != Currentform.Name /*|| Currentform.Name == "frmCustomer"*/)
                        thisForm.Close();
                }
                ClearSession();                                
                new Program.MultiFormContext(Currentform, new frmCustomer(), new frmLogin());                
                //Currentform.Close();
            }
            catch (Exception ex)
            {
                Logger.Trace("ERROR", "UserFunctions-GoToStartup(): " + ex.Message, ex, true);
            }
        }
                
        public static void ClearSession()
        {
            Session._MenuTypeID = 0;
            Session.RemakeOrder = false;
            Session.currentMenuCategoryCode = string.Empty;
            Session.cart = null;
            if (Session.menuItems != null) Session.menuItems.Clear();
            if (Session.menuItemSizes != null) Session.menuItemSizes.Clear();
            Session.selectedMenuItem = new CatalogMenuItems();
            Session.selectedMenuItemSizes = new CatalogMenuItemSizes();
            Session.currentToppingSize = new UserTypes.ToppingSizes();
            Session.currentToppingCollection = null;
            Session.responsePayment = new ResponsePayment();
            Session.selectedOrderType = string.Empty;
            Session.CurrentEmployee = new EmployeeResult();
            Session.LoginPassword = string.Empty;
            //Session.cart.Customer = new Customer();
            Session.CustomerProfileCollection = new GetCustomerProfile();
            Session.FromOrder = false;
            Session.UserID = string.Empty;
            Session.EmployeeCode = string.Empty;
            Session.PasswordResetValue = 0;
            Session.FormPayment = false;
            Session.ClockedTimeIn = DateTime.MinValue;
            Session.Upsellcnt = 0;
            Session.Upsellpayment = 0;
            Session.TicketCollection = null;
            Session.CurrentElapsedTime = string.Empty;
            Session.currentOrderResponse = null;
            Session.IDClockedIN = false;
            Session.currentItemPart = UserTypes.ItemParts.Whole;
            Session.currentToppingSizeCode = string.Empty;
            Session.currentToppingColor = Color.Black;
            Session.pblnModifyDelayedTime = false;
            Session.pblnModifyingOrder = false;
            Session.IsCallerIDClicked = false;
            Session.VegOnlySelected = false;
            Session.MenuItemType = null;
            Session.selectedMenuItems = null;
            Session.cartToppings = null;
            Session.SelectedLineNumber = 0;
            Session.ProcessingCombo = false;
            Session.CurrentComboItem = 0;
            Session.ComboGroup = 0;
            Session.CurrentComboGroup = 0;
            if (Session.comboMenuItems != null) Session.comboMenuItems.Clear();
            if (Session.comboMenuItemSizes != null) Session.comboMenuItemSizes.Clear();
            Session.selectedComboMenuItem = new CatalogPOSComboMealItems();
            Session.selectedComboMenuItemSizes = new CatalogPOSComboMealItemSizes();
            //Session.MenuCategoryButtonClicked = false;
            if (Session.catalogPOSComboMealItemsForButtons != null) Session.catalogPOSComboMealItemsForButtons.Clear();
            Session.originalcart = new Cart();
            Session.originalresponsePayment = new ResponsePayment();
            Session.pblnOpenOrder = false;
            Session.blnCheckLine = false;
            Session.pblnNewOrderTime = false;
            Session.pblnAddModPrepared = false;
            Session.pblnOrderModifications = false;
            Session.pintSendOption = 0;
            Session.pblnCashingOutAfter = false;
            //Session.MaxPhoneDigits = 0;
            Session.responseMsg = string.Empty;
            Session.MaxMenuItemsPerPage = 0;
            Session.currentMenuItemPageNo = 0;
            //if (Session.menuCategories != null) Session.menuCategories.Clear();
            if (Session.orderLineCoupons != null) Session.orderLineCoupons.Clear();
            Session.handleHistorybutton = false;
            Session.IsPayClick = false;
            Session.pblnModifyPrint = false;
            Session.ODC_Tax = false;
            Session.CartInitiated = false;
            Session.GoToStartUp = false;
            Session.PriorityCustomer = string.Empty;
            Session.StreetsAPIResponse = null;
            Session.CustomerStreets = null;
            Session.SpecialtyItems = new UserTypes.SpecialtyItems();
            Session.SpecialtyPizzasList = null;
            Session.TotalPageMenuCategory = 0;
            Session.SelectedBusinessUnit = string.Empty;
            Session.TotalPageMenuItems = 0;
            Session.selectedSourceName = "";                       
            Session.AggregatorGSTValue = 0;            
            if (Session.CombosAvailableForUpsell != null) Session.CombosAvailableForUpsell.Clear();
            Session.UpsellPopupCount = 0;
            Session.UpsellPopupLastResponse = string.Empty;
            if (Session.itemwiseUpsellPromptMatrix != null) Session.itemwiseUpsellPromptMatrix.Clear();
            Session.cashDrawerReasonsForOrder = new List<CashDrawerReason>();
            if (Session.itemwiseUpsellHistory != null) Session.itemwiseUpsellHistory.Clear();
            Session.PreventItemwiseUpsell = false;
            if (Session.LineNumbersFromHistory != null) Session.LineNumbersFromHistory.Clear();
        }
        
        public static int OrderTimeinSeconds()
        {
            int OrderTime = 0;
            if (!String.IsNullOrEmpty(Session.CurrentElapsedTime) && Session.CurrentElapsedTime.Contains(":"))
            {
                string[] arr = Session.CurrentElapsedTime.Split(':');
                if (arr.Length == 2)
                {
                    OrderTime = (Convert.ToInt32(arr[0].Trim()) * 60) + Convert.ToInt32(arr[1].Trim());
                }
            }
            return OrderTime;
        }              

        public static int ActualLineNumber(Cart cart,  int SelectedLineNumber)
        {
            if (SelectedLineNumber > 0)
                return SelectedLineNumber;

            if (cart != null && cart.cartItems != null && cart.cartItems.Count > 0)
            {
                return cart.cartItems[cart.cartItems.Count - 1].Line_Number;
            }
            else
            {
                return SelectedLineNumber;
            }
        }
        
        public static string LineBreak(string strString)
        {
            try
            {
                if (strString.Length < 30) return strString;

                string strHold;
                int intPOS;

                strHold = "";

                do
                {
                    intPOS = strString.Substring(0, 30).LastIndexOf(',');

                    if (intPOS < 0) return strString;

                    if (intPOS == 0)
                    {
                        intPOS = strString.Substring(0, 30).LastIndexOf('/') - 1;
                        if (intPOS == -1)
                            return strString;
                        else if (intPOS == 0 && strString.Length > 30)
                            intPOS = 29;
                    }

                    strHold = strHold + strString.Substring(0, intPOS+1) + "\r\n";
                    strString = strString.Substring(intPOS + 1);
                }
                while (strString.Length > 30);

                strHold = strHold + strString;
                strString = strHold;

            }
            catch (Exception ex)
            {
                Logger.Trace("ERROR", "UserFunctions-LineBreak(): " + ex.Message, ex, true);
            }
            return strString;
        }        
        
        public static string GetAmountCodebyColor(Color color)
        {
            if (color == Session.ToppingSizeLightColor)
                return "~";
            else if (color == Session.ToppingSizeSingleColor)
                return "";
            else if (color == Session.ToppingSizeExtraColor)
                return "+";
            else if (color == Session.ToppingSizeDoubleColor)
                return "2";
            else if (color == Session.ToppingSizeTripleColor)
                return "3";
            else if (color == Session.ToppingColor)
                return "-";
            else
                return "";
        }

        public static int GetDivider()
        {
            return Convert.ToInt32(Math.Pow(10, (Session.DisplayFormat.Length - 1 - Session.DisplayFormat.IndexOf('.') - 1)));
        }

        public static string GetCurrentSelectedOrderTypeOnForm(Control uC_CustomerOrderBottomMenu)
        {
            foreach (Control control in uC_CustomerOrderBottomMenu.Controls)
            {
                if (control.GetType() == typeof(FlowLayoutPanel))
                {
                    foreach (Control _control in control.Controls)
                    {
                        if (_control.BackColor == Session.DefaultEntityColor)
                            return Convert.ToString(_control.Tag);
                    }
                }
            }

            return "";
        }

        public static string GetProcessOwner(int processId)
        {
            string query = "Select * From Win32_Process Where ProcessID = " + processId;
            ManagementObjectSearcher managementObjectSearcher = new ManagementObjectSearcher(query);
            ManagementObjectSearcher moSearcher = new ManagementObjectSearcher(query);
            ManagementObjectCollection moCollection = moSearcher.Get();

            foreach (ManagementObject mo in moCollection)
            {
                string[] args = new string[] { string.Empty };
                int returnVal = Convert.ToInt32(mo.InvokeMethod("GetOwner", args));
                if (returnVal == 0)
                    return args[0];
            }

            return "N/A";
        }

        public static void OpenOneCustomer(string arguments)
        {
            try
            {
                string oneCustomerSetting = SystemSettings.GetSettingValue("OneCustomer", Session._LocationCode);
                if (oneCustomerSetting.ToLower() == "true")
                {
                    ProcessStartInfo start = new ProcessStartInfo();
                    var folderPath = Application.StartupPath + "\\DigitalExe\\";
                    string newPath = Path.GetFullPath(folderPath + "SingleCustomer.exe");
                    start.FileName = newPath;

                    foreach (Process process in Process.GetProcessesByName("SingleCustomer"))
                    {
                        string UserName = GetProcessOwner(process.Id);
                        if (Environment.UserName.ToLower() == UserName.ToLower())
                        {
                            process.Kill();
                        }
                    }

                    if (Session.cart != null && Session.cart.Customer != null && !string.IsNullOrEmpty(Session.cart.Customer.Phone_Number))
                    {
                        start.Arguments = arguments; /*Session.cart.Customer.Phone_Number + " " + 3;*/
                        start.UseShellExecute = false;
                        using (Process proc = Process.Start(start))
                        {
                            //proc.Start();
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                Logger.Trace("ERROR", "UserFunctions-OpenOneCustomer(): " + ex.Message, ex, true);
            }
        }

        public static void AppendToLog(string err_event, string _errmessage, string _errStackTrace)
        {
            string _path = @"D:\";
            if (!Directory.Exists(_path))
                _path = @"E:\";

            if (!Directory.Exists(_path))
                _path = @"C:\";
            _path = _path + @"\ApplicationLogs\JublFood.POS.App\";
            if (!Directory.Exists(_path))
            {
                Directory.CreateDirectory(_path);
            }
            _path = _path + "\\Log_" + DateTime.Now.ToString("yyyyMMdd") + ".txt";

            int strlen = _errStackTrace.Length;
            //if (strlen > 100)
            //{
            //    strlen = 100;
            //}
            _errStackTrace = _errStackTrace.Substring(0, strlen);
            if (!File.Exists(_path))
            {
                var myFile = File.Create(_path);
                myFile.Close();

                TextWriter tw = new StreamWriter(_path);
                tw.WriteLine("Error Event : " + err_event + ", Error Message : " + _errmessage + ", Error Stack : " + _errStackTrace + ", " + DateTime.Now);
                tw.WriteLine("----------------------------------------------------------------------------------------------------------------------------");
                tw.Close();
            }
            else if (File.Exists(_path))
            {
                TextWriter tw = new StreamWriter(_path, true);
                tw.WriteLine("Error Event : " + err_event + ", Error Message : " + _errmessage + ", Error Stack : " + _errStackTrace + ", " + DateTime.Now);
                tw.WriteLine("----------------------------------------------------------------------------------------------------------------------------");
                tw.Close();
            }

        }
    }
}
