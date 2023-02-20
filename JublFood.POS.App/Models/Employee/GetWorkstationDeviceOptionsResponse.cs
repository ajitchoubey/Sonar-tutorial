namespace JublFood.POS.App.Models.Employee
{
    public class GetWorkstationDeviceOptionsResponse
    {
        public WorkstationDeviceOptionsResult Result { get; set; }
    }    

    public class WorkstationDeviceOptionsResult
    {
        public string ResponseStatusCode { get; set; }
        public string ResponseStatus { get; set; }
        public WorkstationDeviceOptions WorkstationDeviceOptions { get; set; }
    }

    public class WorkstationDeviceOptions
    {
        public int ReceiptPrinter { get; set; }
        public int PrintReceipts { get; set; }
        public int PrintReceiptDelivery { get; set; }
        public int PrintReceiptCarryOut { get; set; }
        public int PrintReceiptPickUp { get; set; }
        public int PrintReceiptDineIn { get; set; }
        public int LabelPrinter { get; set; }
        public int PrintLabels { get; set; }
        public int PrintLabelDelivery { get; set; }
        public int PrintLabelCarryOut { get; set; }
        public int PrintLabelPickUp { get; set; }
        public int PrintLabelDineIn { get; set; }
        public int PrintCashDropReceipt { get; set; }
        public int PrintCashDropLabel { get; set; }
        public int PrintMapDirections { get; set; }
        public int PrintCashOutReceipt { get; set; }
        public int CashDrawer { get; set; }
        public int PrintKitchenReceipt { get; set; }
        public int TicketPrinter { get; set; }
        public int PrintNutritionalLabel { get; set; }
        public int PrintTimeClockConfirmation { get; set; }
        public int PrintPreparationLabel { get; set; }
        public int PopDrawerChargeAccount { get; set; }
        public int PopDrawerCheck { get; set; }
        public int PopDrawerCreditCard { get; set; }
        public int PopDrawerGiftCard { get; set; }
        public int PrintPreparationReceipt { get; set; }
        public int PrintCheckOutReceipt { get; set; }
    }
}
