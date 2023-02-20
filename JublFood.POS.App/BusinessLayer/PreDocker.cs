using Jublfood.AppLogger;
using JublFood.POS.App.API;
using JublFood.POS.App.Cache;
using JublFood.POS.App.Class;
using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace JublFood.POS.App.BusinessLayer
{
    public static class PreDocker
    {
        public static int bytMinPhoneDigits = 0;
        public static int bytMaxPhoneDigits = 0;
        public static string WorkstationName { get; set; }

        public static bool LoadPOS()
        {
            bool TmpLoadPOS = false;
            bool EODRunning = false;
            try
            {                
                LogCheck();
                
                (new VersionControl.VersionValidator()).ValidateVersion("Application", Assembly.GetEntryAssembly().GetName().Name.ToString(), Assembly.GetEntryAssembly().GetName().Version.ToString());

                if (!APIValidation()) return false;

                WorkstationName = GetPCName();

                if (JublFood.Settings.Settings.CheckSetupDone())
                {
                    Session._LocationCode = JublFood.Settings.Settings.WorkStationDefualtLocationCode(WorkstationName);
                    if (Session._LocationCode == "")
                    {
                        JublFood.Settings.Settings.InsertWorkstationByName(WorkstationName);
                        Session._LocationCode = JublFood.Settings.Settings.WorkStationDefualtLocationCode(WorkstationName);
                    }

                    //SystemSettings.LocationCodes = Settings.Settings.GetLocationCodeTableSettings(Session._LocationCode);
                    SystemSettings.LocationCodes = Settings.Settings.GetLocationCodeTableSettings();

                    SystemSettings.WorkStationSettings = Settings.Settings.GetWorkStationSettingbyName(WorkstationName);
                    SystemSettings.appControl = Settings.Settings.GetAppControlTableSettings(Session._LocationCode);
                    SystemSettings.settings = Settings.Settings.GetApplicatonSettings(Session._LocationCode);


                    Session.SystemDate = Convert.ToDateTime(SystemSettings.LocationCodes.SystemDate);
                    Session._WorkStationID = Convert.ToInt32(SystemSettings.WorkStationSettings.plngWorkstation_ID);
                    Session.ComputerName = WorkstationName;

                    Session.WorkstationIP = APILayer.GetWorkstationIP(Session._WorkStationID);
                    if (string.IsNullOrEmpty(SystemSettings.GetSettingValue("CashDrawerTimeOut", Session._LocationCode)))
                    {
                        Session.CashDrawerTimeOut = 1;
                    }
                    else
                    {
                        Session.CashDrawerTimeOut = Convert.ToInt32(SystemSettings.GetSettingValue("CashDrawerTimeOut", Session._LocationCode));
                    }
                    if (string.IsNullOrEmpty(SystemSettings.GetSettingValue("CashDrawerOpenCloseReasonID", Session._LocationCode)))
                    {
                        Session.CashDrawerOpenCloseReasonID = 0;
                    }
                    else
                    {
                        Session.CashDrawerOpenCloseReasonID = Convert.ToInt32(SystemSettings.GetSettingValue("CashDrawerOpenCloseReasonID", Session._LocationCode));
                    }



                    string pstrPhoneMask = SystemSettings.appControl.PhoneMask;

                    int count = pstrPhoneMask.Count(f => f == '#');

                    Session.MaxPhoneDigits = count;
                    Session.MinPhoneDigits = count;

                    if (Settings.Settings.GetSettingValue(Session._LocationCode, "EODRunning").ToUpper() == "TRUE")
                    {
                        EODRunning = true;
                    }
                    if (EODRunning == true)
                    {

                    }
                    if (EODHasBeenRan(Session._LocationCode))
                    {
                        TmpLoadPOS = true;
                    }
                }
                else
                {
                    TmpLoadPOS = false;
                }

            }
            catch (Exception ex)
            {
                Logger.Trace("ERROR", "PreDocker-LoadPOS(): " + ex.Message, ex, true);
                TmpLoadPOS = true;
            }

            return TmpLoadPOS;
        }

        public static string GetPCName()
        {
            if (!string.IsNullOrWhiteSpace(System.Environment.GetEnvironmentVariable("CLIENTNAME")))
                return System.Environment.GetEnvironmentVariable("CLIENTNAME");
            else
            {
                if (!string.IsNullOrWhiteSpace(System.Environment.GetEnvironmentVariable("COMPUTERNAME")) && System.Environment.GetEnvironmentVariable("COMPUTERNAME").ToUpper() != "CONSOLE")
                {
                    return Convert.ToString(System.Environment.GetEnvironmentVariable("COMPUTERNAME"));
                }
                else
                {
                    return System.Environment.GetEnvironmentVariable("CLIENTNAME");
                }
            }
        }

        public static bool EODHasBeenRan(string locationCode)
        {
            bool TmpEODHasBeenRan = false;
            int EODRanValue = 0;
            EODRanValue = Settings.Settings.EODHasBeenRan(locationCode);
            switch (EODRanValue)
            {
                case 0:
                    TmpEODHasBeenRan = true;
                    break;
                case 1:
                    if (OverrideProcessingDate())
                    {
                        TmpEODHasBeenRan = true;
                    }
                    else
                    {
                        CustomMessageBox.Show(MessageConstant.EODRunning, CustomMessageBox.Buttons.OK, CustomMessageBox.Icon.Warning);
                        TmpEODHasBeenRan = false;
                    }
                    break;
                case 2:
                    CustomMessageBox.Show(MessageConstant.EODNotDone, CustomMessageBox.Buttons.OK, CustomMessageBox.Icon.Warning);

                    TmpEODHasBeenRan = false;
                    break;
                default:
                    TmpEODHasBeenRan = false;
                    break;

            }
            return TmpEODHasBeenRan;
        }

        private static bool OverrideProcessingDate()
        {
            bool TmpOverrideProcessingDate = false;
            try
            {
                if (SystemSettings.LocationCodes.OverrideProcessingDate == 1)
                {
                    TmpOverrideProcessingDate = true;
                }
                else
                {
                    TmpOverrideProcessingDate = false;
                }
            }
            catch (Exception ex)
            {
                TmpOverrideProcessingDate = false;
                Logger.Trace("ERROR", "PreDocker-OverrideProcessingDate(): " + ex.Message, ex, true);

            }
            return TmpOverrideProcessingDate;
        }

        private static bool APIValidation()
        {
            string ReturnValue = "";
            ReturnValue = APILayer.TestAPI(UserTypes.APIType.Cart);
            if (ReturnValue.ToUpper() != "SUCCESS")
            {
                CustomMessageBox.Show("Cart API not working :" + ReturnValue, CustomMessageBox.Buttons.OK, CustomMessageBox.Icon.Error);
                return false;
            }
            ReturnValue = APILayer.TestAPI(UserTypes.APIType.Catalog);
            if (ReturnValue.ToUpper() != "SUCCESS")
            {
                CustomMessageBox.Show("Catalog API not working :" + ReturnValue, CustomMessageBox.Buttons.OK, CustomMessageBox.Icon.Error);
                return false;
            }
            ReturnValue = APILayer.TestAPI(UserTypes.APIType.Customer);
            if (ReturnValue.ToUpper() != "SUCCESS")
            {
                CustomMessageBox.Show("Customer API not working :" + ReturnValue, CustomMessageBox.Buttons.OK, CustomMessageBox.Icon.Error);
                return false;
            }
            ReturnValue = APILayer.TestAPI(UserTypes.APIType.Order);
            if (ReturnValue.ToUpper() != "SUCCESS")
            {
                CustomMessageBox.Show("Order API not working :" + ReturnValue, CustomMessageBox.Buttons.OK, CustomMessageBox.Icon.Error);
                return false;
            }
            ReturnValue = APILayer.TestAPI(UserTypes.APIType.Employee);
            if (ReturnValue.ToUpper() != "SUCCESS")
            {
                CustomMessageBox.Show("Employee API not working :" + ReturnValue, CustomMessageBox.Buttons.OK, CustomMessageBox.Icon.Error);
                return false;
            }
            ReturnValue = APILayer.TestAPI(UserTypes.APIType.Payment);
            if (ReturnValue.ToUpper() != "SUCCESS")
            {
                CustomMessageBox.Show("Payment API not working :" + ReturnValue, CustomMessageBox.Buttons.OK, CustomMessageBox.Icon.Error);
                return false;
            }
            ReturnValue = APILayer.TestAPI(UserTypes.APIType.Printing);
            if (ReturnValue.ToUpper() != "SUCCESS")
            {
                CustomMessageBox.Show("Printing API not working :" + ReturnValue, CustomMessageBox.Buttons.OK, CustomMessageBox.Icon.Error);
                return false;
            }

            return true;

        }

        private static void LogCheck()
        {
            try
            {
                Logger.Trace("INFO", "JublFood.POS.App STARTED logged through AppLogger", null, false);                
            }
            catch (Exception ex)
            {                
                UserFunctions.AppendToLog("LogCheck - Error in AppLogger", ex.Message, ex.StackTrace);
            }

            try
            {
                InstanceLogger();
                log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                log.Info("JublFood.POS.App STARTED  logged through Local Log4Net");                
                InstanceLogger();
            }
            catch (Exception ex1)
            {                
                UserFunctions.AppendToLog("LogCheck - Error in Local Log4Net", ex1.Message, ex1.StackTrace);
            }
        }

        private static void InstanceLogger()
        {
            log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                        
            StringBuilder sb = new StringBuilder();

            foreach (log4net.Util.LogLog m in logger.Logger.Repository.ConfigurationMessages)
            {
                sb.AppendLine(m.Message);
            }

            UserFunctions.AppendToLog("InstanceLogger", "String messages: " + sb.ToString(),"");

        }

    }

}

