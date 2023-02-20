using Jublfood.AppLogger;
using JublFood.POS.App.API;
using JublFood.POS.App.Cache;
using JublFood.POS.App.Class;
using JublFood.POS.App.Models.Employee;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace JublFood.POS.App
{
    public partial class frmPasswordChange : Form
    {
        private const int CP_NOCLOSE_BUTTON = 0x200;
        private bool ALT_F4 = false;
        public EmployeeDetail EmpDetail { get; set; }
        public bool promptSuccessMessage = false;

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams myCp = base.CreateParams;
                myCp.ClassStyle = myCp.ClassStyle | CP_NOCLOSE_BUTTON;
                return myCp;
            }
        }
        public frmPasswordChange()
        {
            InitializeComponent();
        }

        public frmPasswordChange(bool showCloseButton, bool promptMessage)
        {
            InitializeComponent();
            btnClose.Visible = showCloseButton;
            promptSuccessMessage = promptMessage;
        }

        private void txt_CurrentPassword_Enter(object sender, EventArgs e)
        {
            TextBox txt_Input = (TextBox)sender;
            UC_KeyBoard.txt_Input = txt_Input;

            ((TextBox)sender).BackColor = Color.Yellow;
        }

        private void btn_enter_Click(object sender, EventArgs e)    
        {
            if (!ValidatePassword())
            {
                return;
            }

            EmployeeLoginRequest changePasswordRequest = new EmployeeLoginRequest();
            changePasswordRequest.LocationCode = Session._LocationCode;//Session.CurrentEmployee.LoginDetail.LocationCode;
            changePasswordRequest.Password = txt_NewPassword.Text;
            changePasswordRequest.UserId = Session.UserID;
            changePasswordRequest.Source = Constants.Source;
            changePasswordRequest.SystemDate = Session.SystemDate;
            changePasswordRequest.EmployeeCode = Session.EmployeeCode;

            EmployeeResponse empLoginResponse =  APILayer.UpdateEmployeePassword(APILayer.CallType.POST, changePasswordRequest);
            if (empLoginResponse != null &&  empLoginResponse.ResponseStatusCode != "0")
            {
                this.Close();

                ClockInMagCardRequest clockInMagCardRequest = new ClockInMagCardRequest();
                clockInMagCardRequest.LocationCode = Session._LocationCode;
                clockInMagCardRequest.Source = Constants.Source;
                clockInMagCardRequest.Track1Data = "";
                clockInMagCardRequest.Track2Data = "";
                clockInMagCardRequest.Track3Data = "";

                APILayer.ClockInMagCard(APILayer.CallType.POST, clockInMagCardRequest);
                
                if (promptSuccessMessage)
                    CustomMessageBox.Show(MessageConstant.PasswordChanged, CustomMessageBox.Buttons.OK, CustomMessageBox.Icon.Info);
            }
            else
            {
                CustomMessageBox.Show(MessageConstant.UnableToUpdatePassword, CustomMessageBox.Buttons.OK, CustomMessageBox.Icon.Warning);
                return;
            }
        }

        private bool ValidatePassword()
        {
            bool validatePassword = true;
            try
            {
                if (string.IsNullOrEmpty(txt_CurrentPassword.Text.Trim()))
                {
                    validatePassword = false;
                    CustomMessageBox.Show(MessageConstant.EnterCurrentPassword, CustomMessageBox.Buttons.OK, CustomMessageBox.Icon.Warning);
                    return validatePassword;
                }
                if (string.IsNullOrEmpty(txt_NewPassword.Text.Trim()))
                {
                    validatePassword = false;
                    CustomMessageBox.Show(MessageConstant.EnterNewPassword, CustomMessageBox.Buttons.OK, CustomMessageBox.Icon.Warning);
                    return validatePassword;
                }

                if (txt_NewPassword.Text.Trim().Length < 4)
                {
                    validatePassword = false;
                    CustomMessageBox.Show(MessageConstant.InvalidPassowrdLength, CustomMessageBox.Buttons.OK, CustomMessageBox.Icon.Warning);
                    return validatePassword;
                }

                if (txt_CurrentPassword.Text.Trim() != EmpDetail.Password)
                {
                    validatePassword = false;
                    CustomMessageBox.Show(MessageConstant.CurrentPasswordWrong, CustomMessageBox.Buttons.OK, CustomMessageBox.Icon.Warning);
                    return validatePassword;
                }
                if (txt_NewPassword.Text.Trim() == EmpDetail.OldPassword)
                {
                    validatePassword = false;
                    CustomMessageBox.Show(MessageConstant.NewAndOldPwdSame, CustomMessageBox.Buttons.OK, CustomMessageBox.Icon.Warning);
                    return validatePassword;
                }
                if (txt_NewPassword.Text.Trim() == txt_CurrentPassword.Text.Trim())
                {
                    validatePassword = false;
                    CustomMessageBox.Show(MessageConstant.NewAndCurrentPwdSame, CustomMessageBox.Buttons.OK, CustomMessageBox.Icon.Warning);
                    return validatePassword;
                }
                if (txt_NewPassword.Text.Trim() != txt_ConfirmPassword.Text.Trim())
                {
                    validatePassword = false;
                    CustomMessageBox.Show(MessageConstant.NewAndConfirmPwdNotMatched, CustomMessageBox.Buttons.OK, CustomMessageBox.Icon.Warning);
                    return validatePassword;
                }

                if (Common.IsDigitsOnly(txt_NewPassword.Text.Trim()))
                {
                    int firstDigit = Convert.ToInt32(txt_NewPassword.Text.Trim().Substring(0, 1));
                    int secondDigit = Convert.ToInt32(txt_NewPassword.Text.Trim().Substring(1, 1));
                    int thirdDigit = Convert.ToInt32(txt_NewPassword.Text.Trim().Substring(2, 1));
                    int fourthDigit = Convert.ToInt32(txt_NewPassword.Text.Trim().Substring(3, 1));

                    if (firstDigit == secondDigit && firstDigit == thirdDigit && firstDigit == fourthDigit)
                    {
                        validatePassword = false;
                        CustomMessageBox.Show(MessageConstant.PasswordContainSameChars, CustomMessageBox.Buttons.OK, CustomMessageBox.Icon.Warning);
                        return validatePassword;
                    }

                    if (firstDigit + 1 == secondDigit && secondDigit + 1 == thirdDigit && thirdDigit + 1 == fourthDigit)
                    {
                        validatePassword = false;
                        CustomMessageBox.Show(MessageConstant.PasswordCantSeq, CustomMessageBox.Buttons.OK, CustomMessageBox.Icon.Warning);
                        return validatePassword;
                    }

                    if (firstDigit - 1 == secondDigit && secondDigit - 1 == thirdDigit && thirdDigit - 1 == fourthDigit)
                    {
                        validatePassword = false;
                        CustomMessageBox.Show(MessageConstant.PasswordCantSeq, CustomMessageBox.Buttons.OK, CustomMessageBox.Icon.Warning);
                        return validatePassword;
                    }
                }

                if (Common.AllCharactersSame(txt_NewPassword.Text.Trim()))
                {
                    validatePassword = false;
                    CustomMessageBox.Show(MessageConstant.PasswordContainSameChars, CustomMessageBox.Buttons.OK, CustomMessageBox.Icon.Warning);
                    return validatePassword;
                }
            }
            catch (Exception ex)
            {
                Logger.Trace("ERROR", "frmPasswordChange-ValidatePassword(): " + ex.Message, ex, true);
                validatePassword = false;
                CustomMessageBox.Show(MessageConstant.UnableToUpdatePassword, CustomMessageBox.Buttons.OK, CustomMessageBox.Icon.Warning);
                return validatePassword;
            }

            return validatePassword;
        }

        private void txt_CurrentPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            //e.Handled = !Char.IsNumber(e.KeyChar) && e.KeyChar != 8;
            //e.Handled = e.KeyChar != 8;
        }

        private void txt_NewPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            //e.Handled = !Char.IsNumber(e.KeyChar) && e.KeyChar != 8;
            //e.Handled = e.KeyChar != 8;
        }

        private void txt_ConfirmPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            //e.Handled = !Char.IsNumber(e.KeyChar) && e.KeyChar != 8;
        }

        private void frmPasswordChange_Load(object sender, EventArgs e)
        {

            GetEmployeeCodeRequest getEmployeeCodeRequest = new GetEmployeeCodeRequest();
            getEmployeeCodeRequest.LocationCode = Session._LocationCode;//lblUserId.Text;
            getEmployeeCodeRequest.UserID = Session.UserID;//lblUserId.Text;

            if (Session.FormClockOpened)
            {
                getEmployeeCodeRequest.UserID = Session.TimeClockUserID;
            }

            //GetEmployeeCodeResponse responseEmpCode = APILayer.GetEmployeeCode(APILayer.CallType.POST, getEmployeeCodeRequest);
            //if (responseEmpCode != null && responseEmpCode.Result != null && responseEmpCode.Result.EmployeeCodeDetail != null)
            //{
            //    if (Session.FormClockOpened)
            //    {
            //        Session.TimeClockEmployeeCode = responseEmpCode.Result.EmployeeCodeDetail.EmployeeCode;
            //    }
            //    else
            //    {
            //        Session.EmployeeCode = responseEmpCode.Result.EmployeeCodeDetail.EmployeeCode;
            //    }
            //}


            EmployeeLoginRequest loginRequest = new EmployeeLoginRequest();
            loginRequest.UserId = Session.UserID;//lblUserId.Text;
            loginRequest.Password = Constants.DefaultPassword;
            loginRequest.LocationCode = Session._LocationCode;
            loginRequest.SystemDate = Session.SystemDate;
            loginRequest.Source = Constants.Source;
            loginRequest.EmployeeCode = string.IsNullOrEmpty(Session.EmployeeCode) ? Session.UserID : Session.EmployeeCode;

            if (Session.FormClockOpened)
            {
                loginRequest.UserId = Session.TimeClockUserID;
                loginRequest.EmployeeCode = Session.TimeClockEmployeeCode;
            }

            GetEmployeeDetailsResponse response = APILayer.GetEmployeeDetails(APILayer.CallType.POST, loginRequest);
            if (response != null && response.Result != null && response.Result.EmployeeDetail != null)
            {
                EmpDetail = response.Result.EmployeeDetail;
                if (Session.FormClockOpened)
                {
                    this.lblUserId.Text = Session.TimeClockUserID;
                }
                else
                {
                    this.lblUserId.Text = EmpDetail.UserID;
                }

                this.lalblEmployeenamebel1.Text = EmpDetail.LastName + ", "+ EmpDetail.FirstName;

                if (Session.PasswordResetValue == 1)
                {
                    this.txt_CurrentPassword.Text = EmpDetail.Password;
                    this.txt_CurrentPassword.Enabled = false;
                    this.txt_CurrentPassword.BackColor = Color.White;
                    this.ActiveControl = this.txt_NewPassword;
                }

                if (Session.FormClockOpened)
                {
                    if (Session.TimeClockPasswordResetValue == 1)
                    {
                        this.txt_CurrentPassword.Text = EmpDetail.Password;
                        this.txt_CurrentPassword.Enabled = false;
                        this.txt_CurrentPassword.BackColor = Color.White;
                        this.ActiveControl = this.txt_NewPassword;
                    }
                }
            }
        }

        private void txt_CurrentPassword_Leave(object sender, EventArgs e)
        {
            ((TextBox)sender).BackColor = Color.White;
        }

        private void txt_NewPassword_Leave(object sender, EventArgs e)
        {
            ((TextBox)sender).BackColor = Color.White;
        }

        private void txt_ConfirmPassword_Leave(object sender, EventArgs e)
        {
            ((TextBox)sender).BackColor = Color.White;
        }

        private void frmPasswordChange_FormClosing(object sender, FormClosingEventArgs e)
        {
            //e.Cancel = true;
            if (ALT_F4)
            {
                ALT_F4 = false;
                e.Cancel = true;
                return;
            }
        }

        private void frmPasswordChange_KeyDown(object sender, KeyEventArgs e)
        {
            ALT_F4 = (e.KeyCode.Equals(Keys.F4) && e.Alt == true);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            txt_CurrentPassword.Text = string.Empty;
            txt_NewPassword.Text = string.Empty;
            txt_ConfirmPassword.Text = string.Empty;
            this.Close();
        }
    }
}
