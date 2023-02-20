using System.Configuration;

namespace JublFood.POS.App.Cache
{
    public static class SystemSettings
    {
        public static Settings.Models.AppControl appControl { get; set; }
        public static Settings.Models.SettingsModel settings { get; set; }
        public static Settings.Models.LocationCodes LocationCodes { get; set; }
        public static Settings.Models.WorkStationSettings WorkStationSettings { get; set; }
        //public static Settings.Models.SettingsTable SettingsTable { get; set; }

        public static void CheckSettings(string LocationCode)
        {
            if (appControl == null || settings == null)
                LoadSettings(LocationCode);
            else
            {
                if (LocationCodes.LocationCode != LocationCode)
                    LoadSettings(LocationCode);
            }
        }

        public static void LoadSettings(string LocationCode)
        {
            string ConnectionString = ConfigurationManager.ConnectionStrings["StoreDB"].ConnectionString.ToString();
            LocationCodes = Settings.Settings.GetLocationCodeTableSettings(LocationCode); 
            if (LocationCode == null || LocationCode == "")
                LocationCode = LocationCodes.LocationCode;
            appControl = Settings.Settings.GetAppControlTableSettings(LocationCode); 
            settings = Settings.Settings.GetApplicatonSettings(LocationCode);
            WorkStationSettings = Settings.Settings.GetWorkStationSettingbyName(Session.ComputerName);
        }


        public static string GetSettingValue(string Setting_Name, string LocationCode)
        {
            if (Settings.Settings.GetSettingsTable(Setting_Name, LocationCode) != null)
            {
                return Settings.Settings.GetSettingsTable(Setting_Name, LocationCode).SettingValue;
            }
            else
            {
                return string.Empty;
            }
        }
    }
}
