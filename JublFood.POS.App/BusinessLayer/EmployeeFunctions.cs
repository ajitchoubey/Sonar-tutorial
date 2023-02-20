using JublFood.POS.App.API;
using JublFood.POS.App.Cache;
using JublFood.POS.App.Class;
using JublFood.POS.App.Models.Employee;
using System.Windows.Forms;

namespace JublFood.POS.App.BusinessLayer
{
    public static class EmployeeFunctions
    {
        
        public static bool MatchEmployeePassword()
        {
            bool blnMatch = false;
            frmPassword frmPassword = new frmPassword();
            frmPassword.ShowDialog();




            if(frmPassword.TypedPassword==Session.CurrentEmployee.LoginDetail.Password)
            //if (frmPassword.TypedPassword == Session.LoginPassword) 
            {
                Session.MatchPassword = true;
                blnMatch = true;
            }

            return blnMatch;
        }

        public static bool TechnicalSupport(string pstrDefault_Location_Code, string Employee_code)
        {
            EmployeeLoginRequest loginRequest = new EmployeeLoginRequest();
            loginRequest.EmployeeCode = Employee_code;
            loginRequest.LocationCode = pstrDefault_Location_Code;
            loginRequest.Password = (Session.CurrentEmployee != null && Session.CurrentEmployee.LoginDetail != null && Session.CurrentEmployee.LoginDetail.UserID == Session.UserID) ? Session.LoginPassword : Constants.DefaultPassword;
            loginRequest.Source = Constants.Source;
            loginRequest.UserId = Employee_code;
            loginRequest.SystemDate = Session.SystemDate;

            return APILayer.IsTechnicalSupport(APILayer.CallType.POST, loginRequest); 
        }
        public static bool ValidatePassword()
        {
            frmPassword frmPassword = new frmPassword();
            frmPassword.ShowDialog();
            EmployeeLoginRequest loginRequest = new EmployeeLoginRequest();

            loginRequest.UserId = Session.UserID;
            loginRequest.Password = frmPassword.TypedPassword;
            loginRequest.LocationCode = Session._LocationCode;
            loginRequest.SystemDate = Session.SystemDate;
            loginRequest.Source = Constants.Source;
            loginRequest.EmployeeCode = Session.UserID;

            int status = APILayer.ValidateLogin(APILayer.CallType.POST, loginRequest);
            if (status == 0)
            {
                CustomMessageBox.Show(MessageConstant.InvalidLogin, CustomMessageBox.Buttons.OK, CustomMessageBox.Icon.Info);
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
