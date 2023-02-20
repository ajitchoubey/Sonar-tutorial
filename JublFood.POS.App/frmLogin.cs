using Jublfood.AppLogger;
using JublFood.POS.App.API;
using JublFood.POS.App.BusinessLayer;
using JublFood.POS.App.Cache;
using JublFood.POS.App.Class;
using JublFood.POS.App.Models;
using JublFood.POS.App.Models.Employee;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using System.Threading;
using System.Diagnostics;
using System.Drawing;

namespace JublFood.POS.App
{
    public partial class frmLogin : Form
    {
        private const int CP_NOCLOSE_BUTTON = 0x200;

        private bool ExitApplication = false;
        public bool RequirePassword { get; set; }
        public bool SpecialAccess { get; set; }
        public bool CardSwiped { get; set; }
        public bool FingerprintScanned { get; set; }
        public bool RequireSpecialAccess { get; set; }
        public bool KeyboardUsed { get; set; }
        public string strSaveUserId = string.Empty;
        public string strUserID = string.Empty;
        public int eodReturnValue = 0;
        public bool ALT_F4 = false;
        public int Status { get; set; }

        //BAL objBAL;
        
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams myCp = base.CreateParams;
                myCp.ClassStyle = myCp.ClassStyle | CP_NOCLOSE_BUTTON;
                return myCp;
            }
        }
        public frmLogin()
        {
            this.ControlBox = false;
            InitializeComponent();
            
            //Session.LoggedOut = false;
            //Session._LocationCode = JublFood.Settings.Settings.GetLocationCodeTableSettings(ConfigurationManager.ConnectionStrings["StoreDB"].ConnectionString.ToString()).LocationCode;
            //UserFunctions.Setup();
            AddAllButtonText();
            SetButtonText();
        }
        private void AddAllButtonText()
        {
            //DataTable dtButtonText;
            //objBAL = new BAL();
            //dtButtonText = objBAL.GetAllButtonText();
            Common.DictAllButtonText = new Dictionary<int, string>();
            try
            {
                for (int i=0; i< Session.catalogAllButtonText.Count; i++)
                {

                    Common.DictAllButtonText.Add(Convert.ToInt32(Session.catalogAllButtonText[i].Key_Field), Convert.ToString(Session.catalogAllButtonText[i].Modified_Text));
                }
            }
            catch (Exception ex)
            {
                Logger.Trace("ERROR", ex.Message, ex, true);
            }

        }
        private void SetButtonText()
        {
            string labelText = null;

            if (Common.DictAllButtonText.TryGetValue(LanguageConstant.cintOK, out labelText))
            {
                btn_OK.Text = labelText;
            }
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            if (!Session.pblnModifyingOrder)
            {
                Session.IsTimerStarted = false;
                txtUserID.Text = string.Empty;
                Session.CurrentEmployee = null;
                Session.LoginPassword = null;
                Session.UserID = string.Empty;
                Session.FormClockOpened = true;
                //Password = string.Empty;
                //UserId = string.Empty;
                
            }
            ExitApplication = true;
            this.Close();
            //for (int index = Application.OpenForms.Count - 1; index >= 0; index--)
            //{
            //    Application.OpenForms[index].Close();
            //}

            //Session.FormClockOpened = false;
            //frmCustomer frmCust = new frmCustomer();
            //frmCust.Show();
            //frmLogin frm = new frmLogin();
            //frm.ShowDialog();
        }

        private void btn6_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            txtUserID.Text = txtUserID.Text + btn.Text;
        }

        private void btn_Back_Click(object sender, EventArgs e)
        {
            if (txtUserID.Text.Length > 0)
            {
                txtUserID.Text = txtUserID.Text.Remove(txtUserID.Text.Length - 1);
            }
        }

        private void btn_OK_Click(object sender, EventArgs e)
        {
            try
            {
                if (!PreDocker.EODHasBeenRan(Session._LocationCode))
                {
                    return;
                }

                if (txtUserID.Text == string.Empty)
                {
                    CustomMessageBox.Show(MessageConstant.EnterUserID, CustomMessageBox.Buttons.OK, CustomMessageBox.Icon.Warning);
                    return;
                }

                if (String.IsNullOrWhiteSpace(txtUserID.Text))
                {
                    Session.UserID = Convert.ToString(txtUserID.Text).Trim();
                    strUserID = Session.UserID;
                }

                CheckEmployeeRequest checkEmployeeRequest = new CheckEmployeeRequest();
                checkEmployeeRequest.UserId = txtUserID.Text;
                checkEmployeeRequest.LocationCode = Session._LocationCode;
                checkEmployeeRequest.SystemDate = Session.SystemDate;

                GetEmployeeInfoResponse getEmployeeInfoResponse = new GetEmployeeInfoResponse();
                getEmployeeInfoResponse = APILayer.EmployeeInfo(APILayer.CallType.POST, checkEmployeeRequest);

                //EmployeeLoginRequest loginRequest = new EmployeeLoginRequest();
                //loginRequest.Password = (Session.CurrentEmployee != null && Session.CurrentEmployee.LoginDetail != null && Session.CurrentEmployee.LoginDetail.UserID == Session.UserID) ? Session.LoginPassword : Constants.DefaultPassword;
                //loginRequest.LocationCode = Session._LocationCode;
                //loginRequest.SystemDate = Session.SystemDate;
                //loginRequest.Source = Constants.Source;
                //loginRequest.EmployeeCode = txtUserID.Text;

                //LoginResult loginResult = APILayer.ValidateLogin(APILayer.CallType.POST, loginRequest);

                if (getEmployeeInfoResponse == null || getEmployeeInfoResponse.Result == null || getEmployeeInfoResponse.Result.EmployeeInfo == null || string.IsNullOrEmpty(getEmployeeInfoResponse.Result.EmployeeInfo.EmployeeCode))
                {
                    txtUserID.Text = string.Empty;
                    CustomMessageBox.Show(MessageConstant.InvalidUser, CustomMessageBox.Buttons.OK, CustomMessageBox.Icon.Warning);
                    return;
                }

                string hrisActiveSetting = string.Empty;
                string biometricSetting = string.Empty;
                bool blnFailedPassword = false;
                EmployeeLoginRequest loginRequest = new EmployeeLoginRequest();
                CheckBiometricDataRequest checkBiometricDataRequest = new CheckBiometricDataRequest();
                CheckBiometricDataResponse checkBiometricDataResponse = new CheckBiometricDataResponse();

                loginRequest.UserId = txtUserID.Text;
                Session.UserID = txtUserID.Text;
                loginRequest.Password = (Session.CurrentEmployee != null && Session.CurrentEmployee.LoginDetail != null && Session.CurrentEmployee.LoginDetail.UserID == Session.UserID && !string.IsNullOrWhiteSpace(Session.LoginPassword)) ? Session.LoginPassword : Constants.DefaultPassword;
                loginRequest.LocationCode = Session._LocationCode;
                loginRequest.SystemDate = Session.SystemDate;
                loginRequest.Source = Constants.Source;
                loginRequest.EmployeeCode = txtUserID.Text;



                //starting point
                if (IsTechnicalSupport(loginRequest))
                {
                    if (RequirePassword && CardSwiped)
                        RequirePassword = false;
                    else if (FingerprintScanned && !SpecialAccess)
                        RequirePassword = false;

                    if (!CardSwiped && !FingerprintScanned)
                        KeyboardUsed = true;

                    if (SystemSettings.GetSettingValue("RequirePasswordAtPOSLogin", Session._LocationCode).Equals("1"))
                        RequirePassword = true;

                    hrisActiveSetting = SystemSettings.GetSettingValue("HRIS_Active", Session._LocationCode);
                    biometricSetting = SystemSettings.GetSettingValue("HRIS_Biometric", Session._LocationCode);

                    if (RequirePassword && string.IsNullOrEmpty(strSaveUserId.Trim()) && biometricSetting.Equals("0"))
                    {
                        if (hrisActiveSetting.Equals("1"))
                        {
                            if (!PromptForPasswordReset(loginRequest))
                            {
                                PromptForPassword(loginRequest);
                            }
                            else
                            {
                                PromptForPassword(loginRequest);
                                strSaveUserId = string.Empty;
                            }
                        }
                        else
                            PromptForPassword(loginRequest);

                        txtUserID.Text = string.Empty;
                    }
                    else
                    {
                        if (!RequirePassword && string.IsNullOrEmpty(strSaveUserId.Trim()))
                        {
                            if (hrisActiveSetting.Equals("1"))
                                PromptForPasswordReset(loginRequest);
                        }

                        blnFailedPassword = ValidatePassword(loginRequest);

                        if (!blnFailedPassword || Session.CurrentEmployee == null || Session.CurrentEmployee.LoginDetail == null)
                        {
                            CustomMessageBox.Show(MessageConstant.InvalidLogin, CustomMessageBox.Buttons.OK, CustomMessageBox.Icon.Warning);
                            strSaveUserId = string.Empty;
                            txtUserID.Text = string.Empty;
                            Session.UserID = string.Empty;
                            Session.LoginPassword = string.Empty;
                            return;
                        }

                        if (Session.CurrentEmployee.LoginDetail.DateShiftNumber.Equals(0) && !string.IsNullOrEmpty(Session.CurrentEmployee.LoginDetail.EmployeeCode))
                        {

                            if (hrisActiveSetting.Equals("1") && biometricSetting.Equals("1"))
                            {
                                checkBiometricDataRequest.EmployeeCode = loginRequest.EmployeeCode;
                                checkBiometricDataRequest.LocationCode = loginRequest.LocationCode;
                                checkBiometricDataRequest.POSDate = Convert.ToDateTime(loginRequest.SystemDate.ToString("yyyy-MM-dd HH:mm:ss"));//loginRequest.SystemDate;
                                checkBiometricDataRequest.UserId = loginRequest.UserId;
                                checkBiometricDataRequest.FormTimeClock = false;
                                checkBiometricDataRequest.Source = Constants.Source;

                                if (!APILayer.CheckSystemUser(APILayer.CallType.POST, loginRequest))
                                {
                                    checkBiometricDataResponse = APILayer.CheckBiometricPunchData(APILayer.CallType.POST, checkBiometricDataRequest);

                                    if (checkBiometricDataResponse != null && checkBiometricDataResponse.Result != null && !string.IsNullOrEmpty(checkBiometricDataResponse.Result.ResponseStatus))
                                    {
                                        if (checkBiometricDataResponse.Result.ResponseStatusCode == "0")
                                        {
                                            if (checkBiometricDataResponse.Result.ResponseStatus == MessageConstant.InvalidLogin || checkBiometricDataResponse.Result.ResponseStatus == MessageConstant.BioMetricPunchNotFound)
                                            {
                                                CustomMessageBox.Show(checkBiometricDataResponse.Result.ResponseStatus, CustomMessageBox.Buttons.OK, CustomMessageBox.Icon.Warning);
                                                //Commented by Vikas Saraswat
                                                //for (int i = Application.OpenForms.Count - 1; i >= 0; i--)
                                                //{
                                                //    Application.OpenForms[i].Close();
                                                //}
                                                Session.CurrentEmployee = null;
                                                Session.LoginPassword = string.Empty;
                                                Session.UserID = string.Empty;
                                                //Commented by Vikas Saraswat
                                                //frmCustomer frmCust = new frmCustomer();
                                                //frmCust.Show();
                                                //frmLogin frm = new frmLogin();
                                                //frm.ShowDialog();
                                                return;
                                            }

                                            CustomMessageBox.Show(checkBiometricDataResponse.Result.ResponseStatus, CustomMessageBox.Buttons.OK, CustomMessageBox.Icon.Warning);
                                        }
                                        else
                                        {
                                            if (checkBiometricDataResponse.Result.BiometricData != null && checkBiometricDataResponse.Result.BiometricData.OpenClockInUser)
                                            {
                                                Session.LandFromLoginToTimeclock = true;
                                                ClockInUser();
                                            }
                                        }
                                    }
                                    else
                                    {
                                        CustomMessageBox.Show(MessageConstant.BioMetricPunchNotFound, CustomMessageBox.Buttons.OK, CustomMessageBox.Icon.Warning);
                                        return;
                                    }
                                }
                                else
                                {
                                    PromptForPassword(loginRequest);
                                }
                            }
                            else
                            {
                                ClockInUser();
                                if (!string.IsNullOrWhiteSpace(loginRequest.Password) && loginRequest.Password != Session.LoginPassword)
                                    loginRequest.Password = Session.LoginPassword;
                            }
                        }
                        else
                        {

                            //if (!RequirePassword && string.IsNullOrEmpty(strSaveUserId.Trim()))
                            //{
                            //    if (hrisActiveSetting.Equals("1"))
                            //    {
                            //        if (!PromptForPasswordReset(loginRequest))
                            //            PromptForPassword(loginRequest);
                            //        else
                            //            strSaveUserId = string.Empty;
                            //    }
                            //    else
                            //        PromptForPassword(loginRequest);
                            //}

                            CheckRecordRequest checkRecordRequest = new CheckRecordRequest();
                            checkRecordRequest.LocationCode = Session.CurrentEmployee.LoginDetail.LocationCode;
                            checkRecordRequest.UserID = Session.CurrentEmployee.LoginDetail.UserID;
                            checkRecordRequest.WorkstationID = Session._WorkStationID;
                            checkRecordRequest.Source = Constants.Source;
                            checkRecordRequest.Flag = 0;

                            int checkRecord = APILayer.CheckRecord(APILayer.CallType.POST, checkRecordRequest);

                            if (checkRecord == 0)
                            {
                                if (Session.CurrentEmployee.LoginDetail.DateShiftNumber != 0 && !string.IsNullOrEmpty(Session.CurrentEmployee.LoginDetail.EmployeeCode))
                                {
                                    APILayer.ReturnDriver(APILayer.CallType.POST, loginRequest);
                                }
                            }
                            else
                            {
                                CustomMessageBox.Show(MessageConstant.UserAlreadyLoggedIn, CustomMessageBox.Buttons.OK, CustomMessageBox.Icon.Warning);
                                txtUserID.Text = string.Empty;
                            }
                        }
                    }
                }

                if (Session.GoToStartUp)
                {
                    UserFunctions.GoToStartup(this);
                }
                else
                {
                    if (Session.CurrentEmployee == null || Session.CurrentEmployee.LoginDetail == null)
                    {
                        txtUserID.Text = string.Empty;
                    }

                    if (!string.IsNullOrEmpty(loginRequest.UserId) && (!string.IsNullOrWhiteSpace(loginRequest.Password) && loginRequest.Password.Trim() != Constants.DefaultPassword && !string.IsNullOrWhiteSpace(Session.LoginPassword)))
                    {
                        int status = APILayer.ValidateLogin(APILayer.CallType.POST, loginRequest);
                        if (status != 1)
                        {
                            Session.FormClockOpened = false;

                            CustomMessageBox.Show(MessageConstant.InvalidLogin, CustomMessageBox.Buttons.OK, CustomMessageBox.Icon.Warning);
                            txtUserID.Text = string.Empty;
                            return;
                        }
                    }

                    Session.LandFromLoginToTimeclock = false;
                    Session.FormClockOpened = true;

                    if (!SpecialAccess || (SpecialAccess && this.Text != APILayer.GetCatalogText(LanguageConstant.cintSpecialAccess)))
                    {
                        if (!Session.pblnModifyingOrder)
                        {
                            frmCustomer openFormCustomer = (frmCustomer)Application.OpenForms.Cast<Form>().Where(x => x.Text.ToUpper() == "CUSTOMER").FirstOrDefault();
                            frmCustomer objfrmCustomer = new frmCustomer();
                            objfrmCustomer.Show();
                            openFormCustomer.Close();
                        }
                    }

                    ExitApplication = true;
                    this.Close();
                }

                strSaveUserId = string.Empty;
            }
            catch(Exception ex)
            {
                Logger.Trace("ERROR", "fromLogin-btn_Ok_Click(): " + ex.Message, ex, true);
            }
        }

        private void txtUserID_TextChanged(object sender, EventArgs e)
        {
            
            if (txtUserID.Text.Length > 0)
            {
                //btn_Back.Enabled = true;
                btn_OK.Enabled = true;

            }
            else
            {
                //btn_Back.Enabled = false;
                btn_OK.Enabled = false;
            }
        }

        private void txtUserID_KeyPress(object sender, KeyPressEventArgs e)
        {
           e.Handled = !Char.IsNumber(e.KeyChar) && e.KeyChar != 8;
        }

        private void txtUserID_Enter(object sender, EventArgs e)
        {
            TextBox txt_Input = (TextBox)sender;
            uC_KeyBoardNumeric.txtUserID = txt_Input;
            uC_KeyBoardNumeric.txtUserID.MaxLength = 8;
        }

        private bool IsTechnicalSupport(EmployeeLoginRequest loginRequest)
        {
            bool blnResult;
            bool blnSuccess = true;

            blnResult = APILayer.IsTechnicalSupport(APILayer.CallType.POST, loginRequest);

            if (SpecialAccess && blnResult)
            {
                
                if (Session.CurrentEmployee !=null && Session.CurrentEmployee.LoginDetail != null && Session.CurrentEmployee.LoginDetail.UserID == txtUserID.Text)
                {
                    GetTextRequest getTextRequest = new GetTextRequest();
                    getTextRequest.LocationCode = Session._LocationCode;
                    getTextRequest.LanguageCode = Constants.LanguageCode;
                    getTextRequest.KeyField = Constants.InvalidLogin;

                    CustomMessageBox.Show(APILayer.GetText(APILayer.CallType.POST, getTextRequest), CustomMessageBox.Buttons.OK, CustomMessageBox.Icon.Exclamation);
                    strUserID = string.Empty;
                    this.txtUserID.Text = string.Empty;
                    blnSuccess = false;
                }
               
            }

            if (blnSuccess && blnResult)
            {
                RequirePassword = true;
                SpecialAccess = true;
            }

            return blnSuccess;
        }

        private bool PromptForPasswordReset(EmployeeLoginRequest loginRequest)
        {
            bool promptForPasswordResetResult = false;
            int employeePasswordValue = 0;
            CheckBiometricDataRequest checkBiometricDataRequest = new CheckBiometricDataRequest();
            CheckBiometricDataResponse checkBiometricDataResponse = new CheckBiometricDataResponse();

            strSaveUserId = strUserID;

            CheckEmployeeRequest checkEmployeeRequest = new CheckEmployeeRequest();
            checkEmployeeRequest.LocationCode = Session._LocationCode;
            checkEmployeeRequest.SystemDate = Session.SystemDate;
            checkEmployeeRequest.UserId = loginRequest.EmployeeCode;

            
            APILayer.CheckPasswordExpiry(APILayer.CallType.POST, checkEmployeeRequest);

            checkBiometricDataRequest.LocationCode = loginRequest.LocationCode;
            checkBiometricDataRequest.EmployeeCode = loginRequest.EmployeeCode;
            checkBiometricDataRequest.Source = Constants.Source;
            checkBiometricDataRequest.FormTimeClock = false;
            checkBiometricDataRequest.POSDate = Session.SystemDate; //Convert.ToDateTime(SystemSettings.settings.pdtmServerDateTime);// DateTime.Now;
            checkBiometricDataRequest.UserId = loginRequest.UserId;

            employeePasswordValue = APILayer.CheckEmployeePasswordReset(APILayer.CallType.POST, checkBiometricDataRequest);

            if (employeePasswordValue > 0)
            {
                Session.PasswordResetValue = employeePasswordValue;

                var frmUserID = Application.OpenForms.Cast<Form>().Where(x => x.Text == "User ID").FirstOrDefault();
                if (frmUserID != null)
                {
                    frmUserID.Hide();
                    //openFormCustomer = null;
                }

                if (Session.FormClockOpened)
                {
                    Session.TimeClockUserID = Session.UserID;
                    Session.TimeClockEmployeeCode = loginRequest.EmployeeCode;
                    Session.EmployeeCode = loginRequest.EmployeeCode;
                }

                promptForPasswordResetResult = true;
                frmPasswordChange frmPasswordChange = new frmPasswordChange();
                frmPasswordChange.ShowDialog();
                //frmPasswordChange.Focus();
                //this.Hide();
            }

            return promptForPasswordResetResult;

        }

        private void PromptForPassword(EmployeeLoginRequest loginRequest)
        {
            string hrisActiveSetting = string.Empty;
            strSaveUserId = strUserID;
            strUserID = string.Empty;
            this.txtUserID.Text = string.Empty;

            CheckBiometricDataRequest checkBiometricDataRequest = new CheckBiometricDataRequest();
            checkBiometricDataRequest.LocationCode = Session._LocationCode;
            checkBiometricDataRequest.EmployeeCode = txtUserID.Text;
            checkBiometricDataRequest.Source = Constants.Source;
            checkBiometricDataRequest.FormTimeClock = false;
            checkBiometricDataRequest.POSDate = Session.SystemDate;// Convert.ToDateTime(SystemSettings.settings.pdtmServerDateTime);//DateTime.Now;

            //If GetHRISSetting() Then
            //SettingsTable settings = JublFood.Settings.Settings.GetSettingsTable(ConfigurationManager.ConnectionStrings["StoreDB"].ConnectionString.ToString(), "HRIS_Active");
            //if (settings != null)
            //{
            hrisActiveSetting = SystemSettings.GetSettingValue("HRIS_Active", Session._LocationCode);
            //}
            if (hrisActiveSetting == "1")
            {
                CheckEmployeeRequest checkEmployeeRequest = new CheckEmployeeRequest();
                checkEmployeeRequest.LocationCode = Session._LocationCode;
                checkEmployeeRequest.SystemDate = Session.SystemDate;
                checkEmployeeRequest.UserId = loginRequest.EmployeeCode;

                APILayer.CheckPasswordExpiry(APILayer.CallType.POST, checkEmployeeRequest);
            }
            //End If

            //Open password form
            if (string.IsNullOrWhiteSpace(Session.LoginPassword) || (Session.CurrentEmployee != null && Session.CurrentEmployee.LoginDetail != null && Session.CurrentEmployee.LoginDetail.UserID != Session.UserID))
            {
                frmPassword objfrmPassword = new frmPassword();
                objfrmPassword.ShowDialog();
                
                if(Session.CurrentEmployee == null || Session.CurrentEmployee.LoginDetail == null)
                    Session.LoginPassword = objfrmPassword.TypedPassword;

                loginRequest.Password = (Session.CurrentEmployee != null && Session.CurrentEmployee.LoginDetail != null && Session.CurrentEmployee.LoginDetail.UserID == Session.UserID && !string.IsNullOrWhiteSpace(Session.LoginPassword)) ? Session.LoginPassword : objfrmPassword.TypedPassword;
                Session.LoginPassword = loginRequest.Password;
                objfrmPassword.Dispose();


                if (string.IsNullOrWhiteSpace(Session.LoginPassword))
                {
                    CustomMessageBox.Show(MessageConstant.EnterPassword, CustomMessageBox.Buttons.OK, CustomMessageBox.Icon.Warning);
                    return;
                }
            }

        }

        private bool PromptForPasswordSingleRole(EmployeeLoginRequest loginRequest)
        {
            //strSaveUserId = strUserID;
            //strUserID = string.Empty;
            bool passwordValidated = false;
            string hrisActiveSetting = string.Empty;
            //this.txtUserID.Text = string.Empty;

            CheckBiometricDataRequest checkBiometricDataRequest = new CheckBiometricDataRequest();
            checkBiometricDataRequest.LocationCode = Session._LocationCode;
            checkBiometricDataRequest.EmployeeCode = txtUserID.Text;
            checkBiometricDataRequest.Source = Constants.Source;
            checkBiometricDataRequest.FormTimeClock = false;
            checkBiometricDataRequest.POSDate = Session.SystemDate; //Convert.ToDateTime(SystemSettings.settings.pdtmServerDateTime);// DateTime.Now;

            //If GetHRISSetting() Then
            //loginRequest.UserId = strSaveUserId;
            //SettingsTable settings = JublFood.Settings.Settings.GetSettingsTable(ConfigurationManager.ConnectionStrings["StoreDB"].ConnectionString.ToString(), "HRIS_Active");
            //if (settings != null)
            //{
            hrisActiveSetting = SystemSettings.GetSettingValue("HRIS_Active", Session._LocationCode);
            //}
            if (hrisActiveSetting == "1")
            {
                CheckEmployeeRequest checkEmployeeRequest = new CheckEmployeeRequest();
                checkEmployeeRequest.LocationCode = Session._LocationCode;
                checkEmployeeRequest.SystemDate = Session.SystemDate;
                checkEmployeeRequest.UserId = loginRequest.EmployeeCode;

                APILayer.CheckPasswordExpiry(APILayer.CallType.POST, checkEmployeeRequest);
            }

            //End If

            //Open password form
            //if (Password == string.Empty)
            //{
            frmPassword objfrmPassword = new frmPassword();
            objfrmPassword.ShowDialog();
            
            if (Session.CurrentEmployee == null || Session.CurrentEmployee.LoginDetail == null)
                Session.LoginPassword = objfrmPassword.TypedPassword;

            loginRequest.Password = (Session.CurrentEmployee != null && Session.CurrentEmployee.LoginDetail != null && Session.CurrentEmployee.LoginDetail.UserID == Session.UserID && !string.IsNullOrWhiteSpace(Session.LoginPassword)) ? Session.LoginPassword : objfrmPassword.TypedPassword;

            objfrmPassword.Dispose();


            if (string.IsNullOrWhiteSpace(Session.LoginPassword))
            {
                CustomMessageBox.Show(MessageConstant.EnterPassword, CustomMessageBox.Buttons.OK, CustomMessageBox.Icon.Warning);
                txtUserID.Text = string.Empty;
                return false;
            }

            bool status = EmployeeFunctions.ValidatePassword();
            if (status)
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
                //var openFormTIMECLOCK = Application.OpenForms.Cast<Form>().Where(x => x.Text.ToUpper() == "TIME CLOCK").FirstOrDefault();
                //if (openFormTIMECLOCK != null)
                //{
                //    openFormTIMECLOCK.Show();
                //    //openFormCustomer = null;
                //}
                //CustomMessageBox.Show(MessageConstant.LoginFailed, CustomMessageBox.Buttons.OK, CustomMessageBox.Icon.Error);
                txtUserID.Text = string.Empty;
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

        private void frmLogin_Load(object sender, EventArgs e)
        {
            this.ActiveControl = txtUserID;
            Session.selectedMenuItems = new Dictionary<string, bool?>();
            Console.WriteLine("TimeStamp Login :" + DateTime.Now);

            SplashThread.StopSplashThread();
            this.TopMost = true;
            this.TopMost = false;
            this.Activate();
            //this.Cursor = new Cursor(Cursor.Current.Handle);
            //Cursor.Position = new Point(Cursor.Position.X - 50, Cursor.Position.Y - 50);
            ////Cursor.Clip = new Rectangle(this.Location, this.Size);
        }

        public bool ValidatePassword(EmployeeLoginRequest loginRequest)
        {
            bool blnFailedPassword = false;
            
            if (loginRequest.Password == string.Empty)
                loginRequest.Password = Constants.DefaultPassword;

            if (APILayer.ValidateLogin(APILayer.CallType.POST, loginRequest) == 1)
                blnFailedPassword = true;

            if (RequirePassword)
            {
                if (Session.CurrentEmployee != null && Session.CurrentEmployee.LoginDetail != null && string.IsNullOrEmpty(Session.CurrentEmployee.LoginDetail.EmployeeCode))
                {
                    blnFailedPassword = true;

                    CheckRecordRequest checkRecordRequest = new CheckRecordRequest();
                    checkRecordRequest.LocationCode = Session.CurrentEmployee.LoginDetail.LocationCode;
                    checkRecordRequest.UserID = Session.CurrentEmployee.LoginDetail.UserID;
                    checkRecordRequest.WorkstationID = Session._WorkStationID;
                    checkRecordRequest.Source = Constants.Source;
                    checkRecordRequest.Flag = 1;

                    APILayer.CheckRecord(APILayer.CallType.POST, checkRecordRequest);

                }
            }

            return blnFailedPassword;
        }

        public void ClockInUser()
        {
            if (TimeClock())
            {
                Session.FormClockOpened = true;
                Session.TimeClockUserID = Session.UserID;
                Session.TimeClockEmployeeCode = Session.UserID;
                Session.EmployeeCode = Session.UserID;
            }
            else
            {
                if (!Session.GoToStartUp)
                {
                    GetTextRequest getTextRequest = new GetTextRequest();
                    getTextRequest.LocationCode = Session._LocationCode;
                    getTextRequest.LanguageCode = Constants.LanguageCode;
                    getTextRequest.KeyField = LanguageConstant.cintMSGIDNotClockedIn;

                    CustomMessageBox.Show(APILayer.GetText(APILayer.CallType.POST, getTextRequest), CustomMessageBox.Buttons.OK, CustomMessageBox.Icon.Exclamation);
                }
            }
        }

        private bool TimeClock()
        {
            bool timeClock = false;

            //# If MakeLine <> 1 And Device_Spooler <> 1 And CallerID <> 1 Then
            //    ' Display the time clock screen
            //#If Route_Station <> 1 Then
            //    If pudtSystem_Settings.pblnTrainingMode = True Then
            //        TimeClock = True
            //    Else
            //#End If

            Session.TimeClockUserID = txtUserID.Text;
            Session.FormClockOpened = false;
             frmTimeClock frmTimeClock = new frmTimeClock();

            if (FingerprintScanned)
                frmTimeClock.blnFingerprintScanned = true;

            frmTimeClock.ShowDialog();
           
            this.Hide();
            timeClock = frmTimeClock.bClocked;
            return timeClock;
        }

        private void frmLogin_FormClosing(object sender, FormClosingEventArgs e)
        {
            //e.Cancel = true;
            if (ExitApplication)
            {
                e.Cancel = false;
                return;
            }
            else if (ALT_F4)
            {
                e.Cancel = true;
                return;
            }
            //for (int index = Application.OpenForms.Count - 1; index >= 0; index--)
            //{
            //    Application.OpenForms[index].Close();
            //}

            //Session.FormClockOpened = false;
            //frmCustomer frmCust = new frmCustomer();
            //frmCust.Show();
            //frmLogin frm = new frmLogin();
            //frm.ShowDialog();

        }

        private void frmLogin_FormClosed(object sender, FormClosedEventArgs e)
        {

        }

        private void frmLogin_KeyDown(object sender, KeyEventArgs e)
        {
            ALT_F4 = (e.KeyCode.Equals(Keys.F4) && e.Alt == true);
        }

        private void frmLogin_KeyPress(object sender, KeyPressEventArgs e)
        {
            //ALT_F4 = (e.KeyChar.Equals(Keys.F4) && e.KeyChar.Equals(Keys.Alt));
        }

        private void frmLogin_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            //ALT_F4 = (e.KeyCode.Equals(Keys.F4) && e.Alt == true);
        }

        private void pnl_Login_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            //ALT_F4 = (e.KeyCode.Equals(Keys.F4) && e.Alt == true);
        }
        
    }
}
