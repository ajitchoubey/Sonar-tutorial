
namespace JublFood.POS.App.Class
{
    public static class MessageConstant
    {
        public const string AppTitle = "Hustle Pos";
        public const string EnterUserID = "Please enter User Id";
        public const string EODRunning = "An order cannot be taken until EOD is run or until the Override Processing Date flag is turned on";
        public const string EODNotDone = "The server's system date does not match the processing date";
        public const string InvalidLogin = "Invalid Login";
        public const string AreYouSure = "Are you sure?";
        public const string DiscardCart = "Do you want to discard current cart?";
        public const string CloseWindow = "Do you want to close?";
        public const string EnterCashDrop = "Please enter Cash Drop!";
        public const string CashDropSuccess= "Employee Cash Drop Saved";
        public const string SelectReason = "Please select reason";
        public const string OrderNotCompleted = "Order is not completed";
        public const string StreetNotExists = "Street does not exist in database";
        public const string EnterCustomerName = "Please enter Customer Name";
        public const string EnterCustomerSuite = "Please enter Customer Suite";
        public const string EnterCustomerCompany = "Please enter Customer Company Name";
        public const string EnterPassword = "Please enter Password";
        public const string UserAlreadyLoggedIn = "This User is already login on different Machine";
        public const string PutCartOnHold = "Do you want to put current cart on hold?";
        public const string RemoveNonVegItemsFromCart = "Do you want to remove all non veg items from cart?";
        public const string UnableToUpdatePassword = "Unable to update the password. Please try again!";
        public const string EnterCurrentPassword = "Please enter Current Password";
        public const string EnterNewPassword = "Please enter New Password";
        public const string CurrentPasswordWrong = "Current password is wrong";
        public const string NewAndOldPwdSame = "New password cannot be same as old password";
        public const string NewAndCurrentPwdSame = "New Password cannot be same as current password";
        public const string NewAndConfirmPwdNotMatched = "New Password and Confirm Password does not match";
        public const string PasswordContainSameChars = "Password cannot contain same characters";
        public const string PasswordCantSeq = "Password cannot be sequential";
        public const string InvalidUser = "Not a Valid User";
        public const string UserNotClockedIn = "User Not Clocked In!";
        public const string UserAlreadyClockedIn = "User ID: [UserID] is Already Clocked In";
        public const string ClockedOutFromManagement = "Please Clock Out The Employee From Management!";
        public const string EnterAmount = "Please Enter Amount";
        public const string AmountNotGreaterThanOrderAmount = "Amount should not greater order amount";
        public const string InvalidAmount = "Invalid Amount";
        public const string InvalidReq = "Invalid Request";
        public const string AmountStillDue = "Amount Still Due [Amount] Do You Wish To Continue ?";
        public const string BioMetricPunchNotFound = "Biometric time-in not found. Please contact store manager.";
        public const string InternalErrorApplication = "Something went wrong, Exiting..";
        public const string PasswordChanged = "Password changed sucessfully!";
        public const string EmployeePositionNotSelected = "Please select employee position";
        public const string AllYellowFieldNotSelected = "All required(yellow) Fileds must be entered ! ";
        public const string AllowODCChangeMSG = "Are you sure about GST slab applied on this ODC event?";
        public const string ExeNotExist = "Integration executable does not exist";
        public const string CustomerMissing = "Customer Name is missing.";
        public const string InvalidDate = "Order Date in not valid";
        public const string InvalidTimedOrderDate = "Timed Order Date Time is passed! Do you want to reset order date?";
        public const string InvalidPassowrdLength = "New Password length should be atleast 4 characters.";
        public const string TimedOrderValidation = "Timed orders cannot be less than current time";
        public const string ReprintLimit = "Print is not allowed. No of click print is Exceeded !";
        public const string ReprintTimeLimit = "RePrint time limit reached !";
        public const string PayModeNotworking = "Payment mode is not accessable !";
        //public const string EnableToAddReplace = "Some Items of 'History Order' are not enabled for Order Type selected on Cart. Enable to Add/Replace.";
        public const string AdvanceOrderOnHold = "Advance Orders are not allowed to put On Hold.";
        public const string NotValidMessage = "is not valid";
        public const string EnableToAddReplace = "These Items are not enabled for selected Order Type:";
        public const string AbandedOrderModify = "Abandoned Orders Cannot Be Modified.";
        public const string ODCMinOrderAmount = "ODC Order amount should be more than : ";
        public const string OutOfStock = "Following Items selected in cart are Out of Stock, Please remove them from cart to proceed.";
        public const string UpsellPromptNewItem = "Would you like to add <<NEWITEM>>? ";
        public const string UpsellPromptSizeChange = "Would you like to upgrade to size <<SIZECHANGE>>? ";
        public const string UpsellPromptAddTopping = "Would you like to add <<ADDTOPPING>>? ";
        public const string UpsellPromptConvertToCombo = "Would you like to convert to <<CONVERTTOCOMBO>> combo?";
        public const string SelectAtleastoneOption = "Please select at least one option.";
        public const string CashDrowerLocked = "Cash Drawer is locked, Please unlock to complete the order";
    }
}
