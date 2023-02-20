using JublFood.POS.App.API;
using JublFood.POS.App.Cache;
using JublFood.POS.App.Models;
using JublFood.POS.App.Models.Printing;
using System;

namespace JublFood.POS.App.BusinessLayer
{
    public static class PrintFunctions
    {
        public static String System_Error_Message = "Please contact to administrator";
        public  static void PrintReceipt(long Order_Number, DateTime Order_Date,Boolean ismodifying,Boolean blnordermodifying)
        {
            PrintOrderReceiptResponse orderReceiptResponse = new PrintOrderReceiptResponse();
            PrintOrderReceiptGeneralRequest printOrderReceipt = new PrintOrderReceiptGeneralRequest();
            printOrderReceipt.LocationCode = Session._LocationCode;
            printOrderReceipt.Order_number = Order_Number;
            printOrderReceipt.Order_Date = Order_Date;
            printOrderReceipt.blnModifying = ismodifying;
            printOrderReceipt.blnOrderModifications = blnordermodifying;
            orderReceiptResponse = APILayer.PrintReceiptGeneral(printOrderReceipt);
            

        }

        public static void PrintCashDrop(string Location_Code, long Workstation_Id, string Emp_Code, string Manager_Code,decimal Amount, Boolean blnCashDrop)
        {
            PrintCashDropRequest CashDropRequest = new PrintCashDropRequest();
            CashDropRequest.Location_Codes = Location_Code;
            CashDropRequest.Workstation_Id = Workstation_Id;
            CashDropRequest.Emp_Code = Emp_Code;
            CashDropRequest.Manager_Code = Manager_Code;
            CashDropRequest.Amount = Amount;
            CashDropRequest.blnCashDrop = blnCashDrop;
            bool CashDropResponse = APILayer.PrintCashDropResponse(CashDropRequest);
        }

        public static void PrintAbandedOrder(string Location_Code, long Workstation_Id, DateTime Order_Date, long Order_Number, string Emp_Code)
        {
            PrintAbandedOrderRequest AbandedOrderRequest = new PrintAbandedOrderRequest();
            AbandedOrderRequest.Location_Codes = Location_Code;
            AbandedOrderRequest.Workstation_Id = Workstation_Id;
            AbandedOrderRequest.Order_Date = Order_Date;
            AbandedOrderRequest.Order_number = Order_Number;
            AbandedOrderRequest.Emp_Code = Emp_Code;
            
            APILayer.PrintAbandedOrderResponse(AbandedOrderRequest);
        }
        public static void HandlePrinting()
        {
            PrintOrderReceiptRequest printOrderReceiptRequest = new PrintOrderReceiptRequest();
            APILayer.ReceiptPrinting(printOrderReceiptRequest);
        }

    }
}
