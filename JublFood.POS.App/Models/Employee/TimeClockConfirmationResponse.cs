using System;
using System.Collections.Generic;

namespace JublFood.POS.App.Models.Employee
{
    public class TimeClockConfirmationResponse
    {
        public TimeClockConfirmationResult Result { get; set; }
    }

    public class TimeClockConfirmationResult
    {
        public string ResponseStatusCode { get; set; }
        public string ResponseStatus { get; set; }
        public bool TimeClockConfirmation { get; set; }
        public List<DeviceSetting> DeviceSettings { get; set; }
    }

    public class DeviceSetting
    {
        public string LocationCode { get; set; }
        public int DeviceID { get; set; }
        public int WorkstationID { get; set; }
        public string Description { get; set; }
        public int DeviceTypeCode { get; set; }
        public int ZebraHeatSetting { get; set; }
        public int NumberOfCopies { get; set; }
        public int ReceiptWidth { get; set; }
        public int PortTypeCode { get; set; }
        public int PortName { get; set; }
        public bool KPShowCodes { get; set; }
        public int KPItemVisibility { get; set; }
        public bool KitchenReceipt { get; set; }
        public bool OrderReceipt { get; set; }
        public bool SignatureReceipt { get; set; }
        public bool MenuItemReceipt { get; set; }
        public bool TillReceipt { get; set; }
        public bool MapDirections { get; set; }
        public bool VoidBadReceipt { get; set; }
        public bool CashDropReceipt { get; set; }
        public bool CashOutReceipt { get; set; }
        public bool ShowDiscountsReceipt { get; set; }
        public bool ShowPrinterInformation { get; set; }
        public bool MenuItemLabel { get; set; }
        public bool VoidBadLabel { get; set; }
        public bool CashDropLabel { get; set; }
        public bool DriverLabel { get; set; }
        public bool TaxLabel { get; set; }
        public bool ShowCodesLabel { get; set; }
        public bool CustomerLabel { get; set; }
        public int MakeLineCode { get; set; }
        public int DeviceInterfaceTypeCode { get; set; }
        public bool NutritionalLabel { get; set; }
        public bool TimeClockConfirmation { get; set; }
        public bool PreparationLabel { get; set; }
        public bool PopDrawerCharge_Account { get; set; }
        public bool PopDrawerCheck { get; set; }
        public bool PopDrawerCredit_Card { get; set; }
        public bool PopDrawerGift_Card { get; set; }
        public bool PreparationReceipt { get; set; }
        public bool CheckOutReceipt { get; set; }
        public string AddedBy { get; set; }
        public DateTime Added { get; set; }
        public bool AbandedReceipt { get; set; }
        public bool ReprintReceipt { get; set; }
        public bool PrintDelivery { get; set; }
        public bool PrintCarryOut { get; set; }
        public bool PrintDineIn { get; set; }
        public bool PrintPickUp { get; set; }
    }
}
