using System.Collections.Generic;

namespace JublFood.POS.App.Models.Employee
{
    public class EmployeeLoginResponse
    {
        public EmployeeResult Result { get; set; }
    }

    public class EmployeeResult
    {
        public string ResponseStatusCode { get; set; }
        public string ResponseStatus { get; set; }
        public LoginResult LoginDetail { get; set; }
    }

    public class LoginResult
    {
        public string LocationCode { get; set; }
        public string EmployeeCode { get; set; }
        public string UserID { get; set; }
        public string Password { get; set; }
        public byte DateShiftNumber { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public bool RightHanded { get; set; }
        public int LanguageCode { get; set; }
        public string OldPassword { get; set; }

        //Access Properties
        public bool blnChangeCustomerType { get; set; }
        public bool blnGiveCredit { get; set; }
        public bool blnVariableCoupons { get; set; }
        public bool blnChangePayType { get; set; }
        public bool blnStationSettings { get; set; }
        public bool blnTaxExempt { get; set; }
        public bool blnVoidOrder { get; set; }
        public bool blnPayNow { get; set; }
        public bool blnRealOrder { get; set; }
        public bool blnReduceOrder { get; set; }
        public bool blnAllowOutOfArea { get; set; }
        public bool blnEditLabels { get; set; }
        public bool blnManagementNewBank { get; set; }
        public bool blnDriverStationNewBank { get; set; }
        public bool blnDeliveryPOSNewBank { get; set; }
        public bool blnTrainingMode { get; set; }
        public bool blnManagerModify { get; set; }
        public bool blnTillFunctions { get; set; }
        public bool blnNoSale { get; set; }
        public bool blnReqPasswordCompletedOrders { get; set; }
        public bool blnAddNewStreet { get; set; }
        public bool blnRouteStationSettings { get; set; }
        public bool blnDeliverySummary { get; set; }
        public bool blnExceptions { get; set; }
        public bool blnBadOrders { get; set; }
        public bool blnUnassignOrders { get; set; }
        public bool blnRouteNonDelivery { get; set; }
        public bool blnRouteStationOutTheDoor { get; set; }
        public bool blnUseProtectedCoupon { get; set; }
        public bool blnAllowOrdersBeingModified { get; set; }
        public bool blnAddRemoveDeliveryFee { get; set; }
        public bool blnAuditReport { get; set; }
        public bool blnModifyOpenOrders { get; set; }
        public bool blnAbandonOrder { get; set; }
        public bool blnChangeEmployeeSwipeCard { get; set; }
        public bool blnCashDrops { get; set; }
        public bool blnCCVoiceAuth { get; set; }
        public bool blnOverrideForcedOrderSelection { get; set; }
        public bool blnViewAllOrders { get; set; }
        public bool blnRemoveActivatedGiftCards { get; set; }
        public bool blnDeleteCallerIDLog { get; set; }

        ///
    }
}
