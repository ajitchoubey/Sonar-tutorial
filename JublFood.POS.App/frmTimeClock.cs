using Jublfood.AppLogger;
using JublFood.POS.App.API;
using JublFood.POS.App.BusinessLayer;
using JublFood.POS.App.Cache;
using JublFood.POS.App.Class;
using JublFood.POS.App.Models;
using JublFood.POS.App.Models.Catalog;
using JublFood.POS.App.Models.Employee;
using JublFood.POS.App.Models.Order;
using JublFood.POS.App.Models.Printing;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace JublFood.POS.App
{
    public partial class frmTimeClock : Form
    {
        private const int CP_NOCLOSE_BUTTON = 0x200;
        private bool ALT_F4 = false;
        private string strHoldEmployeeID { get; set; }

        private const string mPrtDblWide = "|2C";
        private const string mPrtSingle = "|1C";
        private const string mPrtCenter = "|cA";
        private const string mPrtRight = "|rA";
        private const string mPrtNormal = "|N";
        private const string mPrtFeedNCut = "|fP";
        private const string mPrtAltColor = "|rC";
        private const string mPrtBold = "|bC";
        private const string mprt = "ac";

        private static string Password = string.Empty;

        private bool bSwiped;
        public bool bClocked { get; set; }

        public string ServerDateTime { get; set; }

        public List<EmployeePosition> EmployeePositions { get; set; }
        public TimeClockGetEmpClockedIn employeeInfo { get; set; }
        public bool blnFingerprintScanned { get; set; }
        public PrintField PrintField { get; set; }
        public OrderTypeOptions OrderTypeOptions { get; set; }
        public List<DeviceSetting> ObjDeviceSettings { get; set; }
        Dictionary<string, string> Dict = new Dictionary<string, string>();

        public static string cintClockIn = string.Empty;
        public static string cintClockOut = string.Empty;

        TimeClockGetEmpClockedInResponse timeClockGetEmpClockedInRes = null;

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams myCp = base.CreateParams;
                myCp.ClassStyle = myCp.ClassStyle | CP_NOCLOSE_BUTTON;
                return myCp;
            }
        }
        public frmTimeClock()
        {
            InitializeComponent();
            SetControlText();
            SetButtonText();
        }
        private void SetButtonText()
        {
            string labelText = null;
            if (Common.DictAllButtonText.TryGetValue(LanguageConstant.cintStatistics, out labelText))
            {
                cmdStatistics.Text = labelText;
            }
            if (Common.DictAllButtonText.TryGetValue(LanguageConstant.cintClockIn, out labelText))
            {
                cmdComplete.Text = labelText;
            }
            //if (Common.DictAllButtonText.TryGetValue(LanguageConstant.cintChangeLogin, out labelText))
            //{
            //    cmdChangePassword.Text = labelText;
            //}
            if (Common.DictAllButtonText.TryGetValue(LanguageConstant.cintCancel, out labelText))
            {
                cmdExit.Text = labelText;
            }
            if (Common.DictAllButtonText.TryGetValue(LanguageConstant.cintOK, out labelText))
            {
                btn_OK.Text = labelText;
            }
        }
        private void SetControlText()
        {
            //BAL obj = new BAL();
            //List<BusinessLayer.FormField> listFormField = obj.GetControlText("frmtimeclock");
            Session.catalogControlText = APILayer.GetControlText("frmtimeclock");
            foreach (Control ctl in pnl_TimeClock.Controls)
            {
                if (ctl is Panel || ctl is GroupBox)
                {
                    foreach (Control ctlChild in ctl.Controls)
                    {
                        if (ctlChild is Label)
                        {
                            foreach (CatalogControlText formField in Session.catalogControlText)
                            {

                                if (ctlChild.Name.Substring(4, ctlChild.Name.Length - 4) == formField.Field_Name)
                                {
                                    ctlChild.Text = formField.text;
                                    break;
                                }
                            }
                        }
                    }
                }
                else if (ctl is Label)
                {
                    foreach (CatalogControlText formField in Session.catalogControlText)
                    {
                        if (ctl.Name.Substring(4, ctl.Name.Length - 4) == formField.Field_Name)
                        {
                            ctl.Text = formField.text;
                            break;
                        }
                    }
                }

            }


        }
        private void btn2_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            if (txtUserID.Enabled && txtUserID.Focus())
                txtUserID.Text = txtUserID.Text + btn.Text;
            else if (tdbnBegin_Odometer.Enabled && tdbnBegin_Odometer.Focus() && tdbnBegin_Odometer.Text.Length <=6)
                tdbnBegin_Odometer.Text = tdbnBegin_Odometer.Text + btn.Text;
        }

        private void btn_Back_Click(object sender, EventArgs e)
        {
            if (txtUserID.Text.Length > 0)
            {
                txtUserID.Text = txtUserID.Text.Remove(txtUserID.Text.Length - 1);
            }
        }

        private void txtUserID_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !Char.IsNumber(e.KeyChar) && e.KeyChar != 8;

            if (cmdComplete.Text == "Clock Out")
            {
                txtUserID.ReadOnly = true;
            }
            else
            {
                txtUserID.ReadOnly = false;
            }
            e.Handled = !Char.IsNumber(e.KeyChar) && e.KeyChar != 8;
        }

        private void frmTimeClock_Load(object sender, EventArgs e)
        {
            GetTextRequest getTextRequest = new GetTextRequest();
            getTextRequest.LocationCode = Session._LocationCode;
            getTextRequest.LanguageCode = Constants.LanguageCode;

            if (string.IsNullOrEmpty(cintClockIn))
            {
                getTextRequest.KeyField = LanguageConstant.cintClockIn;
                cintClockIn = APILayer.GetText(APILayer.CallType.POST, getTextRequest);
                cmdChangePassword.Enabled = false;
            }
            if (string.IsNullOrEmpty(cintClockOut))
            {
                getTextRequest.KeyField = LanguageConstant.cintClockOut;
                cintClockOut = APILayer.GetText(APILayer.CallType.POST, getTextRequest);
               // cmdChangePassword.Enabled = true;
            }

            if (!Session.FormClockOpened && Session.CurrentEmployee != null && Session.CurrentEmployee.LoginDetail != null)
            {
                lblName.Text = Session.CurrentEmployee.LoginDetail.LastName + ", " + Session.CurrentEmployee.LoginDetail.FirstName;
                strHoldEmployeeID = Session.CurrentEmployee.LoginDetail.EmployeeCode;
                btn_OK_Click(sender, e);
            }
            else
            {
                lblName.Text = string.Empty;
                cmdComplete.Enabled = false;
                cmdStatistics.Enabled = false;
                cmdComplete.Text = cintClockIn;
                lblAction.Text = cintClockIn;
                btn_OK.Enabled = false;
                tdbnBegin_Odometer.Enabled = false;
                tdbnEnd_Odometer.Enabled = false;
                tdbnBank.Text = "0.00";
            }
        }

        private void btn_OK_Click(object sender, EventArgs e)
        {
            if (!Session.FormClockOpened)
                txtUserID.Text = Session.UserID;
            else if (txtUserID.Text == string.Empty && Session.CurrentEmployee != null && Session.CurrentEmployee.LoginDetail != null)
                txtUserID.Text = Session.CurrentEmployee.LoginDetail.UserID;

            Session.TimeClockUserID = txtUserID.Text;
            Session.TimeClockEmployeeCode = txtUserID.Text;
            Session.UserID = txtUserID.Text;
            Session.EmployeeCode = txtUserID.Text;


            CheckEmployeeRequest checkEmployeeRequest = new CheckEmployeeRequest();
            checkEmployeeRequest.UserId = txtUserID.Text;
            checkEmployeeRequest.LocationCode = Session._LocationCode;
            checkEmployeeRequest.SystemDate = Session.SystemDate;                      


            bool blnDatesMatch = false;

            GetEmployeeInfoResponse getEmployeeInfoResponse = new GetEmployeeInfoResponse();
            getEmployeeInfoResponse = APILayer.EmployeeInfo(APILayer.CallType.POST, checkEmployeeRequest);

            if (getEmployeeInfoResponse != null && getEmployeeInfoResponse.Result != null && getEmployeeInfoResponse.Result.EmployeeInfo != null && !string.IsNullOrEmpty(getEmployeeInfoResponse.Result.EmployeeInfo.EmployeeCode))
            {
                lblName.Text = getEmployeeInfoResponse.Result.EmployeeInfo.LastName + ", " + getEmployeeInfoResponse.Result.EmployeeInfo.FirstName;
            }
            else
            {
                txtUserID.Text = string.Empty;
                CustomMessageBox.Show(MessageConstant.InvalidUser, CustomMessageBox.Buttons.OK, CustomMessageBox.Icon.Warning);
                return;
            }


            EmployeeLoginRequest loginRequest = new EmployeeLoginRequest();
            loginRequest.UserId = txtUserID.Text;
            Session.UserID = txtUserID.Text;
            loginRequest.Password = !string.IsNullOrEmpty(Session.LoginPassword) ? Session.LoginPassword : Constants.DefaultPassword;
            loginRequest.LocationCode = Session._LocationCode;
            loginRequest.SystemDate = Session.SystemDate;//Convert.ToDateTime(SystemSettings.settings.pdtmServerDateTime);//DateTime.Now;//Convert.ToDateTime(SystemSettings.settings.pdtmServerDateTime).Date;
            loginRequest.Source = Constants.Source;
            loginRequest.EmployeeCode = txtUserID.Text;

            if (!APILayer.CheckSystemUser(APILayer.CallType.POST, loginRequest))
            {
                if (Session.FormClockOpened)
                {
                    if (string.IsNullOrEmpty(lblName.Text))
                    {
                        txtUserID.Text = string.Empty;
                        CustomMessageBox.Show(MessageConstant.InvalidUser, CustomMessageBox.Buttons.OK, CustomMessageBox.Icon.Warning);
                        return;
                    }
                }

                blnDatesMatch = true;

                timeClockGetEmpClockedInRes = new TimeClockGetEmpClockedInResponse();
                timeClockGetEmpClockedInRes = APILayer.TimeClockGetEmpClockedIn(APILayer.CallType.POST, checkEmployeeRequest);

                if (timeClockGetEmpClockedInRes == null || timeClockGetEmpClockedInRes.Result == null || timeClockGetEmpClockedInRes.Result.TimeClockGetEmpClockedIn == null || string.IsNullOrEmpty(timeClockGetEmpClockedInRes.Result.TimeClockGetEmpClockedIn.EmployeeCode))
                {
                    Session.IDClockedIN = false;

                    //string maxShift = APILayer.EmployeeMaxShiftNumber(APILayer.CallType.POST, checkEmployeeRequest);
                    int currentShiftNumber = APILayer.GetTimeClockCurrentDateShiftNumber(APILayer.CallType.POST, checkEmployeeRequest) + 1;

                    ValidateShiftOverlapsRequest validateShiftOverlapsRequest = new ValidateShiftOverlapsRequest();
                    validateShiftOverlapsRequest.EmployeeCode = getEmployeeInfoResponse.Result.EmployeeInfo.EmployeeCode;
                    validateShiftOverlapsRequest.LocationCode = Session._LocationCode;
                    validateShiftOverlapsRequest.ProcessingDate = Convert.ToDateTime(Session.SystemDate);
                    validateShiftOverlapsRequest.Shift = currentShiftNumber;
                    validateShiftOverlapsRequest.TimeIn = Convert.ToDateTime(SystemSettings.settings.pdtmServerDateTime);//Convert.ToDateTime(ServerDateTime);
                    validateShiftOverlapsRequest.TimeOut = Session.blankDate;
                    validateShiftOverlapsRequest.Source = Constants.Source;

                    if (!ValidateShiftOverlaps(false, validateShiftOverlapsRequest))
                    {
                        lblName.Text = string.Empty;
                        btn_OK.Enabled = false;
                        //cmdComplete.Enabled = false;
                        //cmdStatistics.Enabled = false;
                        lblAction.Text = cintClockOut;

                        GetTextRequest getTextRequest = new GetTextRequest();
                        getTextRequest.LocationCode = Session._LocationCode;
                        getTextRequest.LanguageCode = Constants.LanguageCode;
                        getTextRequest.KeyField = LanguageConstant.cintMSGTimeInOverlaps;
                        CustomMessageBox.Show(APILayer.GetText(APILayer.CallType.POST, getTextRequest), CustomMessageBox.Buttons.OK, CustomMessageBox.Icon.Exclamation);
                    }
                    else
                    {
                        lblAction.Text = cintClockIn;
                        LoadPositionButtons();
                    }
                }
                else
                {
                    Session.IDClockedIN = true;
                    lblAction.Text = cintClockOut;
                    cmdComplete.Text = cintClockOut;
                    cmdChangePassword.Enabled = true;
                    LoadPositionButtons();

                    //cmdComplete.Enabled = true;
                    //cmdStatistics.Enabled = true;             
                }
            }
            else
            {
                cmdComplete.Enabled = false;
                cmdChangePassword.Enabled = false;
            }

            btn_OK.Enabled = false;
        }

        private void txtUserID_TextChanged(object sender, EventArgs e)
        {
            if (txtUserID.Text.Length > 0 || (txtUserID.Text.Trim() != Session.UserID))
            {
                btn_OK.Enabled = true;
            }
            else
            {
                btn_OK.Enabled = false;
            }
        }

        private void cmdExit_Click(object sender, EventArgs e)
        {
            bClocked = false;

            if (!Session.IDClockedIN && !Session.FormClockOpened)
            {
                //var openFormTimeClock = Application.OpenForms.Cast<Form>().Where(x => x.Text.ToUpper() == "TIME CLOCK").FirstOrDefault();
                //if (openFormTimeClock != null)
                //{
                //    openFormTimeClock.Show();
                //    //openFormCustomer = null;
                //}

                DialogResult dialogResult = CustomMessageBox.Show(MessageConstant.UserNotClockedIn, CustomMessageBox.Buttons.OKCancel, CustomMessageBox.Icon.Exclamation);
                if (dialogResult == DialogResult.OK)
                {
                    Session.GoToStartUp = true;
                    Session.UserID = string.Empty;
                    Session.TimeClockUserID = string.Empty;
                    Session.TimeClockEmployeeCode = string.Empty;
                    Session.CurrentEmployee = null;
                    Session.FormClockOpened = false;
                    //UserFunctions.GoToStartup(this);
                    this.Close();
                }
            }
            else
            {
                ALT_F4 = false;
                this.Close();
            }
        }

        private void txtUserID_Enter(object sender, EventArgs e)
        {
            TextBox txt_Input = (TextBox)sender;
            uC_KeyBoardNumeric.txtUserID = txt_Input;
            uC_KeyBoardNumeric.txtUserID.MaxLength = 8;
        }

        private void cmdComplete_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmdComplete.Text == cintClockOut)
                {
                    CustomMessageBox.Show(MessageConstant.ClockedOutFromManagement, CustomMessageBox.Buttons.OK, CustomMessageBox.Icon.Warning);
                    this.Close();
                    return;
                }

                if (cmdComplete.Text == cintClockIn && lblShiftNumber.Text == string.Empty)
                {
                    CustomMessageBox.Show(MessageConstant.EmployeePositionNotSelected, CustomMessageBox.Buttons.OK, CustomMessageBox.Icon.Warning);
                    return;
                }


                string hrisActiveSetting = SystemSettings.GetSettingValue("HRIS_Active", Session._LocationCode);
                string hrisBiometricSetting = hrisBiometricSetting = SystemSettings.GetSettingValue("HRIS_Biometric", Session._LocationCode); ;

                if (cmdComplete.Text == cintClockIn)
                {
                    CheckEmployeeRequest checkEmployeeAlreadyLogin = new CheckEmployeeRequest();
                    checkEmployeeAlreadyLogin.LocationCode = Session._LocationCode;
                    checkEmployeeAlreadyLogin.SystemDate = Session.SystemDate;
                    checkEmployeeAlreadyLogin.UserId = txtUserID.Text;

                    if (APILayer.CheckEmployeeAlreadyLogin(APILayer.CallType.POST, checkEmployeeAlreadyLogin))
                    {
                        CustomMessageBox.Show(MessageConstant.UserAlreadyClockedIn.Replace("[UserID]", checkEmployeeAlreadyLogin.UserId), CustomMessageBox.Buttons.OK, CustomMessageBox.Icon.Exclamation);
                        return;
                    }

                    if (hrisActiveSetting.Equals("1") && hrisBiometricSetting.Equals("1"))
                    {
                        CheckBiometricDataRequest checkBiometricDataRequest = new CheckBiometricDataRequest();

                        if (Session.CurrentEmployee == null || Session.CurrentEmployee.LoginDetail == null)
                        {
                            GetEmployeeCodeRequest getEmployeeCodeRequest = new GetEmployeeCodeRequest();
                            getEmployeeCodeRequest.LocationCode = Session._LocationCode;
                            getEmployeeCodeRequest.UserID = txtUserID.Text;

                            GetEmployeeCodeResponse responseEmpCode = APILayer.GetEmployeeCode(APILayer.CallType.POST, getEmployeeCodeRequest);
                            if (responseEmpCode != null && responseEmpCode.Result != null && responseEmpCode.Result.EmployeeCodeDetail != null)
                            {
                                checkBiometricDataRequest.EmployeeCode = responseEmpCode.Result.EmployeeCodeDetail.EmployeeCode;
                                checkBiometricDataRequest.UserId = responseEmpCode.Result.EmployeeCodeDetail.EmployeeCode;
                            }
                        }
                        else
                        {
                            checkBiometricDataRequest.EmployeeCode = txtUserID.Text;
                            checkBiometricDataRequest.UserId = txtUserID.Text;
                        }

                        checkBiometricDataRequest.LocationCode = Session._LocationCode;
                        checkBiometricDataRequest.POSDate = Session.SystemDate;
                        checkBiometricDataRequest.FormTimeClock = true;
                        checkBiometricDataRequest.Source = Constants.Source;

                        if (!Session.FormClockOpened)
                            checkBiometricDataRequest.FormTimeClock = false;

                        CheckBiometricDataResponse checkBiometricDataResponse = new CheckBiometricDataResponse();
                        checkBiometricDataResponse = APILayer.CheckBiometricPunchData(APILayer.CallType.POST, checkBiometricDataRequest);

                        if (checkBiometricDataResponse != null && checkBiometricDataResponse.Result != null)
                        {
                            if (checkBiometricDataResponse.Result.ResponseStatusCode == "0")
                            {
                                CustomMessageBox.Show(checkBiometricDataResponse.Result.ResponseStatus, CustomMessageBox.Buttons.OK, CustomMessageBox.Icon.Warning);
                                this.Close();
                                return;
                            }
                        }
                    }
                }

                EmployeeLoginRequest employeeLogin = new EmployeeLoginRequest();
                employeeLogin.EmployeeCode = txtUserID.Text;
                employeeLogin.UserId = txtUserID.Text;
                employeeLogin.Password = !string.IsNullOrEmpty(Session.LoginPassword) ? Session.LoginPassword : Constants.DefaultPassword;
                employeeLogin.LocationCode = Session._LocationCode;
                employeeLogin.SystemDate = Session.SystemDate;
                employeeLogin.Source = Constants.Source;

                if (!bSwiped && !blnFingerprintScanned)
                {
                    if (cmdComplete.Text == cintClockIn)
                    {
                        if (hrisActiveSetting == "1")
                        {
                            if (!PromptForPasswordReset(employeeLogin))
                            {
                                if (!PromptForPassword(employeeLogin))
                                {
                                    return;
                                }
                            }
                            else
                            {
                                CheckEmployeeRequest checkEmployeeRequest = new CheckEmployeeRequest();
                                checkEmployeeRequest.LocationCode = Session._LocationCode;
                                checkEmployeeRequest.SystemDate = Session.SystemDate;
                                checkEmployeeRequest.UserId = txtUserID.Text;

                                APILayer.CheckPasswordExpiry(APILayer.CallType.POST, checkEmployeeRequest);
                                if (!PromptForPassword(employeeLogin))
                                {
                                    return;
                                }
                            }
                        }
                        else
                        {
                            if (!PromptForPassword(employeeLogin))
                            {
                                return;
                            }
                        }
                    }
                    else
                    {
                        if (hrisActiveSetting == "1")
                        {
                            CheckEmployeeRequest checkEmployeeRequest = new CheckEmployeeRequest();
                            checkEmployeeRequest.LocationCode = Session._LocationCode;
                            checkEmployeeRequest.SystemDate = Session.SystemDate;
                            checkEmployeeRequest.UserId = txtUserID.Text;

                            APILayer.CheckPasswordExpiry(APILayer.CallType.POST, checkEmployeeRequest);
                        }

                        if (!PromptForPassword(employeeLogin))
                        {
                            return;
                        }
                    }
                }

                if (!APILayer.IsTechnicalSupport(APILayer.CallType.POST, employeeLogin) && !APILayer.IsAdministrator(APILayer.CallType.POST, employeeLogin))
                {
                    if (string.IsNullOrEmpty(lblTime_Out.Text))
                    {
                        CheckRecordRequest checkRecordRequest = new CheckRecordRequest();
                        checkRecordRequest.LocationCode = Session._LocationCode;
                        checkRecordRequest.UserID = (Session.CurrentEmployee != null && Session.CurrentEmployee.LoginDetail != null) ? Session.CurrentEmployee.LoginDetail.UserID : txtUserID.Text;
                        checkRecordRequest.WorkstationID = Session._WorkStationID;
                        checkRecordRequest.Source = Constants.Source;
                        checkRecordRequest.Flag = 0;
                        APILayer.CheckRecord(APILayer.CallType.POST, checkRecordRequest);

                        TimeClockClockInOutRequest timeClockClockInOutRequest = new TimeClockClockInOutRequest();
                        timeClockClockInOutRequest.LocationCode = Session._LocationCode;
                        timeClockClockInOutRequest.EmployeeCode = employeeInfo.EmployeeCode;
                        timeClockClockInOutRequest.PositionCode = employeeInfo.PositionCode;
                        timeClockClockInOutRequest.SystemDate = employeeInfo.SystemDate.Date;
                        timeClockClockInOutRequest.PositionShiftNumber = employeeInfo.PositionShiftNumber;
                        timeClockClockInOutRequest.DateShiftNumber = employeeInfo.DateShiftNumber;
                        timeClockClockInOutRequest.TillStartingAmount = employeeInfo.TillStartingAmount;
                        timeClockClockInOutRequest.BeginOdometer = string.IsNullOrWhiteSpace(tdbnBegin_Odometer.Text) ? 0 : Convert.ToUInt64(tdbnBegin_Odometer.Text);
                        timeClockClockInOutRequest.EndOdometer = string.IsNullOrWhiteSpace(tdbnEnd_Odometer.Text) ? 0 : Convert.ToUInt64(tdbnEnd_Odometer.Text); ;
                        timeClockClockInOutRequest.Source = Constants.Source;
                        timeClockClockInOutRequest.AddedBy = string.IsNullOrEmpty(strHoldEmployeeID) ? employeeInfo.EmployeeCode : strHoldEmployeeID;
                        Session.ClockedTimeIn = timeClockClockInOutRequest.SystemDate;

                        //TimeClockClockIn
                        APILayer.TimeClockClockIn(APILayer.CallType.POST, timeClockClockInOutRequest);

                        if (hrisBiometricSetting.Equals("1"))
                        {
                            //UpdateClockInAndOutDataBiometric
                            UpdateClockInAndOutDataBiometricRequest updateClockInAndOutDataBiometricRequest = new UpdateClockInAndOutDataBiometricRequest();
                            updateClockInAndOutDataBiometricRequest.LocationCode = Session._LocationCode;
                            updateClockInAndOutDataBiometricRequest.EmployeeCode = employeeInfo.EmployeeCode;
                            updateClockInAndOutDataBiometricRequest.DateShiftNumber = employeeInfo.DateShiftNumber;
                            updateClockInAndOutDataBiometricRequest.PositionCode = employeeInfo.PositionCode;
                            updateClockInAndOutDataBiometricRequest.SystemDate = employeeInfo.SystemDate;
                            updateClockInAndOutDataBiometricRequest.PunchType = "IN";
                            updateClockInAndOutDataBiometricRequest.Source = Constants.Source;

                            APILayer.UpdateClockInAndOutDataBiometric(APILayer.CallType.POST, updateClockInAndOutDataBiometricRequest);
                        }

                        bClocked = true;

                        TimeClockConfirmationRequest timeClockConfirmationRequest = new TimeClockConfirmationRequest();
                        timeClockConfirmationRequest.LocationCode = Session._LocationCode;
                        timeClockConfirmationRequest.EmployeeCode = (Session.CurrentEmployee != null && Session.CurrentEmployee.LoginDetail != null) ? Session.CurrentEmployee.LoginDetail.UserID : txtUserID.Text;
                        timeClockConfirmationRequest.LanguageCode = Constants.LanguageCode;
                        timeClockConfirmationRequest.WorkStationID = Session._WorkStationID;
                        timeClockConfirmationRequest.Source = Constants.Source;

                        GetWorkstationDeviceOptionsResponse getWorkstationDeviceOptionsResponse = new GetWorkstationDeviceOptionsResponse();
                        getWorkstationDeviceOptionsResponse = APILayer.GetWorkstationDeviceOptions(APILayer.CallType.POST, timeClockConfirmationRequest);
                        if (getWorkstationDeviceOptionsResponse != null && getWorkstationDeviceOptionsResponse.Result != null && getWorkstationDeviceOptionsResponse.Result.WorkstationDeviceOptions != null)
                        {
                            if (getWorkstationDeviceOptionsResponse.Result.WorkstationDeviceOptions.PrintTimeClockConfirmation > 0)
                            {
                                HandleTimeClockConfirmationPrinting(Session._LocationCode, Session._WorkStationID, Constants.LanguageCode, employeeInfo.EmployeeCode, Convert.ToString(employeeInfo.TimeIn), string.Empty, employeeInfo.Position);
                            }
                        }
                    }
                    else
                    {
                        if (employeeInfo.TimeIn > employeeInfo.TimeOut)
                        {
                            GetTextRequest getTextRequest = new GetTextRequest();
                            getTextRequest.LocationCode = Session._LocationCode;
                            getTextRequest.LanguageCode = Constants.LanguageCode;
                            getTextRequest.KeyField = LanguageConstant.cintMSGTimeInGreaterThanTimeOut;
                            CustomMessageBox.Show(APILayer.GetText(APILayer.CallType.POST, getTextRequest), CustomMessageBox.Buttons.OK, CustomMessageBox.Icon.Exclamation);
                        }
                        else
                        {
                            CustomMessageBox.Show(MessageConstant.ClockedOutFromManagement, CustomMessageBox.Buttons.OK, CustomMessageBox.Icon.Warning);
                            this.Close();
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                Logger.Trace("ERROR", "frmTimeClock-cmComplete_click(): " + ex.Message, ex, true);
            }
        }


        public bool ValidateShiftOverlaps(bool warning, ValidateShiftOverlapsRequest validateShiftOverlapsRequest)
        {
            bool validate = false;

            if (validateShiftOverlapsRequest.TimeIn > validateShiftOverlapsRequest.TimeOut && validateShiftOverlapsRequest.TimeOut != Session.blankDate)
            {
                if (warning)
                {
                    GetTextRequest getTextRequest = new GetTextRequest();
                    getTextRequest.LocationCode = Session._LocationCode;
                    getTextRequest.LanguageCode = Constants.LanguageCode;
                    getTextRequest.KeyField = LanguageConstant.cintMSGTimeInGreaterThanTimeOut;
                    string msgTimeInGreaterThanTimeOut = APILayer.GetText(APILayer.CallType.POST, getTextRequest);

                    CustomMessageBox.Show(msgTimeInGreaterThanTimeOut, CustomMessageBox.Buttons.OK, CustomMessageBox.Icon.Exclamation);
                }
                validate = false;
            }
            else
            {
                if (APILayer.ValidateShiftOverlaps(APILayer.CallType.POST, validateShiftOverlapsRequest) > 0)
                {
                    validate = true;
                }
                if (!validate && warning)
                {
                    GetTextRequest getTextRequest = new GetTextRequest();
                    getTextRequest.LocationCode = Session._LocationCode;
                    getTextRequest.LanguageCode = Constants.LanguageCode;
                    getTextRequest.KeyField = LanguageConstant.cintMSGYearRequired;
                    string msgShiftOverlaps = APILayer.GetText(APILayer.CallType.POST, getTextRequest);

                    CustomMessageBox.Show(msgShiftOverlaps, CustomMessageBox.Buttons.OK, CustomMessageBox.Icon.Exclamation);
                }
            }
            return validate;
        }

        public void HandleTimeClockConfirmationPrinting(string locationCode, int workStationID, int languageCode, string employeeCode, string timeIn, string timeOut, string position)
        {
            try
            {
                TimeClockConfirmationRequest timeClockConfirmationRequest = new TimeClockConfirmationRequest();
                timeClockConfirmationRequest.LocationCode = locationCode;
                timeClockConfirmationRequest.EmployeeCode = employeeCode;
                timeClockConfirmationRequest.LanguageCode = languageCode;
                timeClockConfirmationRequest.WorkStationID = workStationID;
                timeClockConfirmationRequest.Source = Constants.Source;

                GetPrinterSettings(timeClockConfirmationRequest);
                GetDeviceSettings(timeClockConfirmationRequest);

                PrintTimeClockConfirmationRequest printTimeClockConfirmationRequest = new PrintTimeClockConfirmationRequest();
                printTimeClockConfirmationRequest.LocationCode = locationCode;
                printTimeClockConfirmationRequest.EmployeeCode = employeeCode;
                printTimeClockConfirmationRequest.LineDisplayRow = 0;
                printTimeClockConfirmationRequest.LineDisplayColumn = 0;
                printTimeClockConfirmationRequest.ObjDeviceSettings = ObjDeviceSettings;
                printTimeClockConfirmationRequest.Source = Constants.Source;

                foreach (DeviceSetting device in ObjDeviceSettings)
                {
                    if (device.DeviceTypeCode == 2)
                    {
                        if (device.TimeClockConfirmation)
                        {
                            PrintTimeClockConfirmation(printTimeClockConfirmationRequest);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Trace("ERROR", "frmTimeClock-handletimeclockconformationprinting(): " + ex.Message, ex, true);
                throw;
            }
        }

        public void GetPrinterSettings(TimeClockConfirmationRequest timeClockConfirmationRequest)
        {
            try
            {
                GetPrinterSettingsResponse getPrinterSettingsResponse = APILayer.GetPrinterSettings(APILayer.CallType.POST, timeClockConfirmationRequest);

                if (getPrinterSettingsResponse != null && getPrinterSettingsResponse.Result != null && getPrinterSettingsResponse.Result.PrintField != null)
                {
                    PrintField = getPrinterSettingsResponse.Result.PrintField;
                }

                //GetOrderTypeOptionsResponse getOrderTypeOptionsResponse = APILayer.GetOrderTypeOptions(APILayer.CallType.POST, timeClockConfirmationRequest);

                //if (getOrderTypeOptionsResponse != null && getOrderTypeOptionsResponse.Result != null && getOrderTypeOptionsResponse.Result.OrderTypeOptions != null)
                //{
                //    OrderTypeOptions = getOrderTypeOptionsResponse.Result.OrderTypeOptions;
                //}
            }
            catch (Exception ex)
            {
                Logger.Trace("ERROR", "frmTimeClock-getprintersettings(): " + ex.Message, ex, true);
                throw;
            }


        }

        public void GetDeviceSettings(TimeClockConfirmationRequest timeClockConfirmationRequest)
        {
            try
            {
                TimeClockConfirmationResponse timeClockConfirmationResponse = APILayer.TimeClockConfirmation(APILayer.CallType.POST, timeClockConfirmationRequest);

                if (timeClockConfirmationResponse != null && timeClockConfirmationResponse.Result != null && timeClockConfirmationResponse.Result.DeviceSettings != null)
                {
                    ObjDeviceSettings = timeClockConfirmationResponse.Result.DeviceSettings;
                }
            }
            catch (Exception ex)
            {
                Logger.Trace("ERROR", "frmTimeClock-getdevicesettings(): " + ex.Message, ex, true);
            }
        }

        public void PrintTimeClockConfirmation(PrintTimeClockConfirmationRequest printTimeClockConfirmationRequest)
        {
            try
            {
                int intRow = 0;
                string strData = string.Empty;
                byte bytCount;
                string strFrame = string.Empty;
                string ESC = string.Empty;
                StringBuilder stringBuilder = new StringBuilder();

                ESC = Convert.ToChar(27).ToString();

                stringBuilder.Append(strData + ESC + mPrtCenter + PrintField.StoreName + Environment.NewLine);
                strData = strData + ESC + mPrtCenter + PrintField.StoreName + Environment.NewLine;

                stringBuilder.Append(stringBuilder + string.Empty + Environment.NewLine);
                strData = strData + string.Empty + Environment.NewLine;

                string fullName = "";//APILayer.GetText(APILayer.CallType.POST, getTextRequest);objPOSSettings.Employee_Full_Name(strLocationCode, strEmployee_Code)
                foreach (DeviceSetting deviceSetting in ObjDeviceSettings)
                {
                    strData = strData + ESC + mPrtNormal + ESC + mPrtDblWide + Pad(fullName, deviceSetting.ReceiptWidth) + Environment.NewLine;
                }


                GetTextRequest getTextRequest = new GetTextRequest();
                getTextRequest.LocationCode = Session._LocationCode;
                getTextRequest.LanguageCode = Constants.LanguageCode;
                getTextRequest.KeyField = LanguageConstant.cintTimeIn;
                string strTimeIn = APILayer.GetText(APILayer.CallType.POST, getTextRequest);
                strData = strData + ESC + mPrtSingle + Pad(strTimeIn, 10) + " : " + Environment.NewLine;

                getTextRequest.KeyField = LanguageConstant.cintTimeOut;
                string strTimeOut = APILayer.GetText(APILayer.CallType.POST, getTextRequest);
                strData = strData + ESC + mPrtSingle + Pad(strTimeOut, 10) + " : " + strTimeOut + Environment.NewLine;

                getTextRequest.KeyField = LanguageConstant.cintPosition;
                string strPosition = APILayer.GetText(APILayer.CallType.POST, getTextRequest);
                strData = strData + ESC + mPrtSingle + Pad(strPosition, 10) + " : " + strPosition + Environment.NewLine;

                foreach (DeviceSetting deviceSetting in ObjDeviceSettings)
                {
                    strData = strData + ESC + mPrtCenter + ESC + mPrtSingle + PrintField.Workstation_Name + " - " + PrintField.PrinterName + Environment.NewLine;
                }

                strData = strData + ESC + mPrtFeedNCut;

                printTimeClockConfirmationRequest.SendText = strData;


                //APILayer.PrintTimeClockConfirmation(APILayer.CallType.POST, printTimeClockConfirmationRequest);
            }
            catch (Exception ex)
            {
                Logger.Trace("ERROR", "frmTimeClock-printtimeclockconfirmation(): " + ex.Message, ex, true);
                throw;
            }
        }

        public string Pad(string strString, int intLength, bool blnRight = false)
        {
            string result = string.Empty;

            strString = strString.Replace("  ", " ");
            result = strString;

            if (strString.Length > intLength)
            {
                result = strString.PadLeft(intLength);
            }
            else if (strString.Length < intLength)
            {
                if (blnRight)
                {
                    result = strString.PadRight(intLength - strString.Length);
                }
                else
                {
                    result = strString + strString.PadRight(intLength - strString.Length);
                }
            }

            return result;
        }

        private void LoadPositionButtons()
        {
            txtUserID.Enabled = false;
            uC_KeyBoardNumeric.Enabled = false;
            btn_OK.Enabled = false;

            EmployeeLoginRequest loginRequest = new EmployeeLoginRequest();
            string btnPositionSelect = string.Empty;
            if (Session.FormClockOpened)
            {
                loginRequest.UserId = !string.IsNullOrWhiteSpace(Session.TimeClockUserID) ? Session.TimeClockUserID : txtUserID.Text;
            }

            loginRequest.UserId = Session.TimeClockUserID;
            loginRequest.Password = !string.IsNullOrEmpty(Session.LoginPassword) ? Session.LoginPassword : Constants.DefaultPassword;
            loginRequest.LocationCode = Session._LocationCode;
            loginRequest.SystemDate = Session.SystemDate;
            loginRequest.Source = Constants.Source;
            loginRequest.EmployeeCode = Session.TimeClockUserID;

            GetEmployeePositionResponse getEmployeePositions = APILayer.GetEmployeePositions(APILayer.CallType.POST, loginRequest);

            if (getEmployeePositions != null && getEmployeePositions.Result != null && getEmployeePositions.Result.employeePositions != null && getEmployeePositions.Result.employeePositions.Count > 0)
            {
                EmployeePositions = getEmployeePositions.Result.employeePositions;
                foreach (EmployeePosition position in getEmployeePositions.Result.employeePositions)
                {
                    if (!string.IsNullOrEmpty(position.Position) && !string.IsNullOrEmpty(position.PositionCode))
                    {
                        Dict.Add(position.PositionCode, position.Position);
                    }
                }

                if (Dict != null && Dict.Count > 0)
                {
                    int intialXAxis = 268;//324;
                    foreach (KeyValuePair<string, string> entry in Dict)
                    {
                        if (Dict.Count == 1)
                            btnPositionSelect = entry.Key;
                        Button btn = new Button();
                        btn.Name = entry.Key;
                        btn.Text = entry.Value;
                        btn.Size = new Size(68, 55);
                        btn.Location = new Point(intialXAxis, 0);
                        intialXAxis = intialXAxis + 66;//83;
                        btn.BackColor = Color.PeachPuff;
                        btn.Image = Properties.Resources.Role;
                        btn.ImageAlign = ContentAlignment.TopCenter;
                        btn.TextAlign = ContentAlignment.BottomCenter;
                        //btn.ForeColor = Color.Red;
                        btn.Font = new Font("Microsoft Sans Serif", 9, FontStyle.Regular);
                        if (cmdComplete.Text == cintClockOut)
                            btn.Enabled = false;
                        btn.Click += new EventHandler(btn_Positions_Click);
                        this.Controls.Add(btn);
                    }          
                }

               
                
                if(timeClockGetEmpClockedInRes != null && timeClockGetEmpClockedInRes.Result != null && timeClockGetEmpClockedInRes.Result.TimeClockGetEmpClockedIn != null && !string.IsNullOrWhiteSpace(timeClockGetEmpClockedInRes.Result.TimeClockGetEmpClockedIn.PositionCode))
                {
                    btnPositionSelect = timeClockGetEmpClockedInRes.Result.TimeClockGetEmpClockedIn.PositionCode;
                }

                if (!string.IsNullOrEmpty(btnPositionSelect))
                {
                    Button btnPosition = new Button();
                    btnPosition = (Button)Controls.Find(btnPositionSelect, true)[0];
                    btn_Positions_Click(btnPosition, new EventArgs());
                }

                Dict.Clear();
            }
            else
            {
                GetTextRequest getTextRequest = new GetTextRequest();
                getTextRequest.LocationCode = Session._LocationCode;
                getTextRequest.LanguageCode = Constants.LanguageCode;
                getTextRequest.KeyField = LanguageConstant.cintMSGErrorEmployeeHasNoPosition;
                CustomMessageBox.Show(APILayer.GetText(APILayer.CallType.POST, getTextRequest), CustomMessageBox.Buttons.OK, CustomMessageBox.Icon.Exclamation);
            }

        }

        private void btn_Positions_Click(object sender, EventArgs e)
        {
            BtnDynamic_LostFocus();

            Button btn = (Button)sender;

            EmployeePosition position = EmployeePositions.Where(x => x.Position.ToUpper() == btn.Text.ToUpper()).FirstOrDefault();

            if (position != null)
            {
                cmdComplete.Enabled = true;
                //cmdStatistics.Enabled = true;
                //btn.BackColor = Color.Red;
                btn.BackColor = Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));

                PopulateEmployeeDetails(cmdStatistics.Text, position);
            }
        }

        private bool PromptForPasswordReset(EmployeeLoginRequest loginRequest)
        {
            bool promptForPasswordResetResult = false;
            int employeePasswordValue = 0;
            CheckBiometricDataRequest checkBiometricDataRequest = new CheckBiometricDataRequest();
            CheckBiometricDataResponse checkBiometricDataResponse = new CheckBiometricDataResponse();

            CheckEmployeeRequest checkEmployeeRequest = new CheckEmployeeRequest();
            checkEmployeeRequest.LocationCode = Session._LocationCode;
            checkEmployeeRequest.SystemDate = Session.SystemDate;
            checkEmployeeRequest.UserId = loginRequest.EmployeeCode;

            APILayer.CheckPasswordExpiry(APILayer.CallType.POST, checkEmployeeRequest);

            checkBiometricDataRequest.LocationCode = Session._LocationCode;
            checkBiometricDataRequest.EmployeeCode = txtUserID.Text;
            checkBiometricDataRequest.Source = Constants.Source;
            checkBiometricDataRequest.FormTimeClock = true;
            checkBiometricDataRequest.POSDate = Session.SystemDate; //Convert.ToDateTime(SystemSettings.settings.pdtmServerDateTime); // DateTime.Now;
            checkBiometricDataRequest.UserId = txtUserID.Text;

            employeePasswordValue = APILayer.CheckEmployeePasswordReset(APILayer.CallType.POST, checkBiometricDataRequest);

            if (employeePasswordValue > 0)
            {
                Session.TimeClockPasswordResetValue = employeePasswordValue;
                promptForPasswordResetResult = true;
                frmPasswordChange frmPasswordChange = new frmPasswordChange();
                frmPasswordChange.ShowDialog();
                this.Hide();
            }

            return promptForPasswordResetResult;

        }

        private bool PromptForPassword(EmployeeLoginRequest loginRequest)
        {
            bool passwordValidated = false;
            string hrisActiveSetting = string.Empty;

            CheckBiometricDataRequest checkBiometricDataRequest = new CheckBiometricDataRequest();
            checkBiometricDataRequest.LocationCode = Session._LocationCode;
            checkBiometricDataRequest.EmployeeCode = txtUserID.Text;
            checkBiometricDataRequest.Source = Constants.Source;
            checkBiometricDataRequest.FormTimeClock = false;
            checkBiometricDataRequest.POSDate = Session.SystemDate; //Convert.ToDateTime(SystemSettings.settings.pdtmServerDateTime);// DateTime.Now;


            hrisActiveSetting = SystemSettings.GetSettingValue("HRIS_Active", Session._LocationCode);

            if (hrisActiveSetting == "1")
            {
                CheckEmployeeRequest checkEmployeeRequest = new CheckEmployeeRequest();
                checkEmployeeRequest.LocationCode = Session._LocationCode;
                checkEmployeeRequest.SystemDate = Session.SystemDate;
                checkEmployeeRequest.UserId = loginRequest.EmployeeCode;

                APILayer.CheckPasswordExpiry(APILayer.CallType.POST, checkEmployeeRequest);
            }

            frmPassword objfrmPassword = new frmPassword();
            objfrmPassword.ShowDialog();
            Password = objfrmPassword.TypedPassword;
            loginRequest.Password = Password;
            Session.LoginPassword = Password;
            objfrmPassword.Dispose();


            if (Password == string.Empty)
            {
                CustomMessageBox.Show(MessageConstant.EnterPassword, CustomMessageBox.Buttons.OK, CustomMessageBox.Icon.Warning);
                return false;
            }

            int status = APILayer.ValidateLogin(APILayer.CallType.POST, loginRequest);
            if (status == 1)
            {
                //frmCustomer objfrmCustomer = new frmCustomer();
                //objfrmCustomer.Show();
                //openFormCustomer.Show();
                passwordValidated = true;
                this.Hide();
                //var openFormCustomer = Application.OpenForms.Cast<Form>().Where(x => x.Text.ToUpper() == "CUSTOMER").FirstOrDefault();
                //if (openFormCustomer != null)
                //{
                //    openFormCustomer.Close();
                //    //openFormCustomer = null;
                //}
                //frmCustomer objfrmCustomer = new frmCustomer();
                //objfrmCustomer.Show();
                //openFormCustomer.Show();
                //this.Hide();
            }
            else
            {
                passwordValidated = false;
                var openFormTIMECLOCK = Application.OpenForms.Cast<Form>().Where(x => x.Text.ToUpper() == "TIME CLOCK").FirstOrDefault();
                if (openFormTIMECLOCK != null)
                {
                    openFormTIMECLOCK.Show();
                    //openFormCustomer = null;
                }
                CustomMessageBox.Show(MessageConstant.InvalidLogin, CustomMessageBox.Buttons.OK, CustomMessageBox.Icon.Warning);
                //txtUserID.Text = string.Empty;
                //CustomMessageBox.Show(MessageConstant.LoginFailed, CustomMessageBox.Buttons.OK, CustomMessageBox.Icon.Error);
                //txtUserID.Text = string.Empty;
                //this.Show();
                //var openFormLogin = Application.OpenForms.Cast<Form>().Where(x => x.Text.ToUpper() == "USERID").FirstOrDefault();
                //if (openFormLogin != null)
                //{
                //    openFormLogin.Show();
                //    //openFormCustomer = null;
                //}
            }
            //}

            return passwordValidated;

        }

        private void frmTimeClock_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (ALT_F4)
            {
                ALT_F4 = false;
                e.Cancel = true;
                return;
            }
        }

        private void frmTimeClock_KeyDown(object sender, KeyEventArgs e)
        {
            ALT_F4 = (e.KeyCode.Equals(Keys.F4) && e.Alt == true);
        }

        private void BtnDynamic_LostFocus()
        {
            foreach (EmployeePosition btnPostition in EmployeePositions)
            {
                Button btnDynamic = new Button();
                btnDynamic = (Button)Controls.Find(btnPostition.PositionCode, true)[0];
                btnDynamic.BackColor = Color.PeachPuff;
            }
        }

        private void cmdStatistics_Click(object sender, EventArgs e)
        {
            if (pnl_Statistics.Visible == false)
            {
                pnl_Statistics.Location = new Point(308, 40);
                pnl_Statistics.Size = new Size(354, 304);
                pnl_Statistics.BringToFront();
                pnl_Statistics.Visible = true;
                cmdStatistics.Text = "Time Clock";
                //PopulateEmployeeDetails(cmdStatistics.Text, null, null);
            }
            else
            {
                cmdStatistics.Text = "Statistics";
                pnl_Statistics.SendToBack();
                pnl_Statistics.Visible = false;
            }
        }

        private void txtUserID_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btn_OK_Click(this, new EventArgs());
            }
        }

        private void PopulateEmployeeDetails(string panelType, EmployeePosition position)
        {
            if (Session.CurrentEmployee != null && Session.CurrentEmployee.LoginDetail != null && (string.IsNullOrEmpty(txtUserID.Text) || txtUserID.Text == Session.CurrentEmployee.LoginDetail.EmployeeCode))
                lblName.Text = Session.CurrentEmployee.LoginDetail.LastName + "," + Session.CurrentEmployee.LoginDetail.FirstName;
            else
            {
                CheckEmployeeRequest checkEmployeeRequest = new CheckEmployeeRequest();
                checkEmployeeRequest.UserId = txtUserID.Text;
                checkEmployeeRequest.LocationCode = Session._LocationCode;
                checkEmployeeRequest.SystemDate = Session.SystemDate;

                GetEmployeeInfoResponse getEmployeeInfoResponse = new GetEmployeeInfoResponse();
                getEmployeeInfoResponse = APILayer.EmployeeInfo(APILayer.CallType.POST, checkEmployeeRequest);

                if (getEmployeeInfoResponse != null && getEmployeeInfoResponse.Result != null && getEmployeeInfoResponse.Result.EmployeeInfo != null)
                {
                    lblName.Text = getEmployeeInfoResponse.Result.EmployeeInfo.LastName + "" + getEmployeeInfoResponse.Result.EmployeeInfo.FirstName;
                }
            }
            
            if (position != null)
            {
                employeeInfo = new TimeClockGetEmpClockedIn();
                employeeInfo.LocationCode = Session._LocationCode;
                employeeInfo.SystemDate = Session.SystemDate;
                employeeInfo.PositionCode = position.PositionCode;
                employeeInfo.Position = position.Position;
                employeeInfo.Driver = position.Driver ? 1 : 0;
                employeeInfo.Manager = position.Manager;
                employeeInfo.Inside = position.Inside;
                employeeInfo.RequireTill = position.RequireTill;

                if (timeClockGetEmpClockedInRes != null && timeClockGetEmpClockedInRes.Result != null && timeClockGetEmpClockedInRes.Result.TimeClockGetEmpClockedIn != null && !string.IsNullOrEmpty(timeClockGetEmpClockedInRes.Result.TimeClockGetEmpClockedIn.EmployeeCode))
                {
                    employeeInfo.EmployeeCode = timeClockGetEmpClockedInRes.Result.TimeClockGetEmpClockedIn.EmployeeCode;
                    employeeInfo.TimeIn = timeClockGetEmpClockedInRes.Result.TimeClockGetEmpClockedIn.TimeIn;
                    employeeInfo.PositionShiftNumber = timeClockGetEmpClockedInRes.Result.TimeClockGetEmpClockedIn.PositionShiftNumber;
                    employeeInfo.DateShiftNumber = timeClockGetEmpClockedInRes.Result.TimeClockGetEmpClockedIn.DateShiftNumber;
                }
                else
                {
                    employeeInfo.EmployeeCode = txtUserID.Text;
                    employeeInfo.TimeIn = DateTime.Now;

                    CheckEmployeeRequest checkEmployeeRequest = new CheckEmployeeRequest();
                    checkEmployeeRequest.LocationCode = Session._LocationCode;
                    checkEmployeeRequest.UserId = employeeInfo.EmployeeCode;
                    checkEmployeeRequest.PositionCode = employeeInfo.PositionCode;
                    checkEmployeeRequest.SystemDate = employeeInfo.SystemDate.Date;

                    employeeInfo.PositionShiftNumber = (byte)(APILayer.GetTimeClockCurrentPositionShiftNumber(APILayer.CallType.POST, checkEmployeeRequest) + 1);
                    employeeInfo.DateShiftNumber = (byte)(APILayer.GetTimeClockCurrentDateShiftNumber(APILayer.CallType.POST, checkEmployeeRequest) + 1);
                }           

                if (employeeInfo.RequireTill)
                {
                    if (employeeInfo.PositionShiftNumber == 1)
                    {
                        employeeInfo.TillStartingAmount = position.TillStartingAmount;
                    }
                    else if (position.TillStartingAmount > 0)
                    {
                        employeeInfo.TillStartingAmount = 0;
                    }
                }
            }
            
            if (panelType.ToUpper() == "STATISTICS")
            {
                lblShiftNumber.Text = employeeInfo.DateShiftNumber.ToString();
                lblSystem_Date.Text = employeeInfo.SystemDate.ToString("yyyy-MM-dd");
                lblTime_In.Text = employeeInfo.TimeIn.ToString("yyyy-MM-dd HH:mm");
                lblTime_Out.Text = string.Empty;
                
                bool isDriver = employeeInfo.Driver == 1 ? true : false;

                if (isDriver || employeeInfo.Manager)
                {
                    uC_KeyBoardNumeric.Enabled = true;
                    btn_OK.Enabled = true;
                    groupBox2.Enabled = true;
                    tdbnBegin_Odometer.Text = employeeInfo.BeginOdometer > 0 ? Convert.ToString(employeeInfo.BeginOdometer) : string.Empty;

                    if (cmdComplete.Text == cintClockOut)
                    {
                        tdbnBegin_Odometer.Enabled = false;
                        tdbnEnd_Odometer.Text = employeeInfo.EndOdometer > 0 ? Convert.ToString(employeeInfo.EndOdometer) : string.Empty;
                        //if (tdbnBegin_Odometer.Text != string.Empty && employeeInfo.BeginOdometer >= 0)
                        //{
                        //    tdbnEnd_Odometer.Enabled = true;
                        //    tdbnEnd_Odometer.Focus();
                        //}
                    }
                    if (cmdComplete.Text == cintClockIn)
                    {
                        tdbnBegin_Odometer.Enabled = true;
                        tdbnBegin_Odometer.Focus();
                        tdbnEnd_Odometer.Enabled = false;
                    }
                }
                else
                {
                    uC_KeyBoardNumeric.Enabled = false;
                    btn_OK.Enabled = false;
                    groupBox2.Enabled = false;
                    tdbnBegin_Odometer.Text = string.Empty;
                    tdbnEnd_Odometer.Text = string.Empty;
                    tdbnBegin_Odometer.Enabled = false;
                    tdbnEnd_Odometer.Enabled = false;
                }

                tdbnBank.Text = Convert.ToString(employeeInfo.TillStartingAmount);
            }
        }

        private void cmdChangePassword_Click(object sender, EventArgs e)
        {
            Session.UserID = txtUserID.Text;
            frmPasswordChange frmPasswordChange = new frmPasswordChange(true, true);
            frmPasswordChange.ShowDialog();
        }

        private void tdbnBegin_Odometer_Enter(object sender, EventArgs e)
        {
            TextBox txt_Input = (TextBox)sender;
            uC_KeyBoardNumeric.txtUserID = txt_Input;
            uC_KeyBoardNumeric.txtUserID.MaxLength = 6;
        }

        private void tdbnBegin_Odometer_KeyPress(object sender, KeyPressEventArgs e)
        {
           e.Handled = !Char.IsNumber(e.KeyChar) && e.KeyChar != 8;
        }

        private void tdbnEnd_Odometer_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !Char.IsNumber(e.KeyChar) && e.KeyChar != 8;
        }

        private void frmTimeClock_Activated(object sender, EventArgs e)
        {
            //tdbnBegin_Odometer.Focus();
        }
    }
}
