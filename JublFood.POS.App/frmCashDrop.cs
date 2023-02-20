using Jublfood.AppLogger;
using JublFood.POS.App.API;
using JublFood.POS.App.BusinessLayer;
using JublFood.POS.App.Cache;
using JublFood.POS.App.Class;
using JublFood.POS.App.Models.Catalog;
using JublFood.POS.App.Models.Employee;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Windows.Forms;

namespace JublFood.POS.App
{
    public partial class frmCashDrop : Form
    {        
        public static bool CashDrop;
        public frmCashDrop()
        {
            InitializeComponent();
            uc_KeyBoardNumeric.ChangeButtonColor(DefaultBackColor);
            uc_KeyBoardNumeric.txtUserID = tdbnAmount;
            SetControlText();
            LoadClockInEmployee();
            CashDrop = true;
            cmdCurrencySelect.Enabled = false;
        }
        List<Employee> listEmployee;
        int Index;
        int TotalEmployee;
        
        private void SetControlText()
        {
            //BAL obj = new BAL();
            //List<FormField> listFormField = obj.GetControlText("frmCashDrop");
            Session.catalogControlText = APILayer.GetControlText("frmCashDrop");
            foreach (Control ctl in this.pnl_CashDrop.Controls)
            {
                if (ctl is Label)
                {
                    foreach (CatalogControlText formField in Session.catalogControlText)
                    {
                        if (ctl.Name.Substring(4, ctl.Name.Length - 4) == formField.Field_Name)
                        {
                            ctl.Text = formField.text;
                        }
                    }
                }
            }
        }

        private void cmdPrevious_Click(object sender, EventArgs e)
        {
            Index = Index - 1;
            if(Index==0)
            {
                cmdPrevious.Enabled = false;
                cmdNext.Enabled = true;
            }
            else
            {
                cmdPrevious.Enabled = true;
            }
            GenrateButtonForEmployee();
        }

        private void cmdNext_Click(object sender, EventArgs e)
        {
            Index = Index + 1;
            cmdPrevious.Enabled = true;
            if(Index==TotalEmployee)
            {
                cmdNext.Enabled = false;
                cmdPrevious.Enabled = true;
            }
            else
            {
                cmdNext.Enabled = true;
            }
            GenrateButtonForEmployee();
        }
        Button btn = null;
        private void LoadClockInEmployee()
        {
            try
            {
                ltxtCurrencyCode.Text = "R";//TO DO
                cmdOk.Enabled = false;


                string response = APILayer.GetClockedInEmployees(Session._LocationCode, Session.SystemDate);
                JToken entireJson = JToken.Parse(response);
                if (entireJson["routeClockIn"] != null || entireJson["routeClockIn"].ToString() != "")
                {
                    TotalEmployee = entireJson["routeClockIn"].Count<object>();
                    listEmployee = entireJson["routeClockIn"].ToObject<List<Employee>>();
                    if (TotalEmployee >= 3)
                    {
                        cmdNext.Enabled = true;
                        //GenrateButtonForEmployee();
                    }

                    JArray inner = entireJson["routeClockIn"].Value<JArray>();
                    foreach (var item in inner)
                    {
                        JProperty EmployeeCodeName = item.First.Value<JProperty>();
                        JProperty EmployeeName = item.Last.Value<JProperty>();
                        var getEmpCode = EmployeeCodeName.First;
                        var getEmpName = EmployeeName.Last;
                        btn = new Button();
                        btn.Text = getEmpName.ToString();
                        btn.Name = getEmpCode.ToString();
                        btn.Margin = new Padding(0);
                        btn.Size = new Size(68, 55);
                        btn.Click += new EventHandler(btn_Employee_Click);
                        flpanel_Employee.Controls.Add(btn);

                    }
                }
            }
            catch(Exception ex)
            {
                Logger.Trace("ERROR", "Method-LoadClockInEmployee(): " + ex.Message, ex, true);
            }         
        }
      
       private void GenrateButtonForEmployee()
        {
            try
            {
                flpanel_Employee.Controls.Clear();

                for (int i = Index; i < 2 + Index; i++)
                {
                    btn = new Button();

                    btn.Text = listEmployee[i].name;
                    btn.Name = listEmployee[i].employee_Code;
                    btn.Margin = new Padding(0);
                    btn.Size = new Size(68, 55);
                    btn.Click += new EventHandler(btn_Employee_Click);
                    flpanel_Employee.Controls.Add(btn);

                }
            }
            catch
            {

            }
         
        }
       
        private void cmdClose_Click(object sender, EventArgs e)
        {
            if (CustomMessageBox.Show(MessageConstant.CloseWindow, CustomMessageBox.Buttons.YesNo, CustomMessageBox.Icon.Question) == System.Windows.Forms.DialogResult.Yes)
                this.Close();
            
        }

        private void uc_KeyBoardNumeric_Load(object sender, EventArgs e)
        {

        }
        static string Employee_Code=string.Empty;
        protected void btn_Employee_Click(object sender,EventArgs e)
        {
            string Emp_Code = string.Empty;
            foreach (Button btnCtl in flpanel_Employee.Controls)
            {
                btnCtl.BackColor = DefaultBackColor;
            }
            if (string.IsNullOrEmpty(tdbnAmount.Text))
            
                cmdOk.Enabled = false;
            

            else
            
                cmdOk.Enabled = true;

                Button btn = (Button)sender;
                 Employee_Code = btn.Name;
                btn.BackColor = Color.PapayaWhip;
           
        }

        private void btn_Dot_Click(object sender, EventArgs e)
        {
            tdbnAmount.Text = tdbnAmount.Text + btn_Dot.Text;
        }

        private void cmdPlusMinus_Click(object sender, EventArgs e)
        {
            if(cmdPlusMinus.Tag.ToString()=="-")
            {
                tdbnAmount.Text = "-" + tdbnAmount.Text;
                tdbnAmount.ForeColor = Color.Red;
                cmdPlusMinus.Image = Properties.Resources._62;
                cmdPlusMinus.Tag = "+";
            }
            else  if (cmdPlusMinus.Tag.ToString() == "+")
            {
                tdbnAmount.Text = tdbnAmount.Text.Replace("-","");
                tdbnAmount.ForeColor = Color.Black;
                cmdPlusMinus.Image = Properties.Resources._63;
                cmdPlusMinus.Tag = "-";
            }
        }
        
        private void cmdOk_Click(object sender, EventArgs e)
        {
            EmployeeResult oldLoginEmployee;

            try
            {
               
                frmLogin frmLogin = new frmLogin();
                
                bool mblnCashDrop;
                string strCurrencyFormat;
                bool blnLoginSuccessful;
                string SpecialAccessEmployeeCode = string.Empty;

                if (!string.IsNullOrEmpty(Session.CurrentEmployee.LoginDetail.EmployeeCode) && Session.CurrentEmployee.LoginDetail.blnCashDrops)
                {
                    if (EmployeeFunctions.MatchEmployeePassword())
                    {
                        blnLoginSuccessful = true;
                        SpecialAccessEmployeeCode = Session.CurrentEmployee.LoginDetail.EmployeeCode;
                    }
                    else
                    {
                        blnLoginSuccessful = false;

                    }
                   

                }
                else
                {
                    mblnCashDrop = true;
                    oldLoginEmployee = Session.CurrentEmployee;
                    frmLogin.SpecialAccess = true;
                    frmLogin.Text = APILayer.GetCatalogText(LanguageConstant.cintSpecialAccess);
                    frmLogin.RequirePassword = true;
                    frmLogin.ShowDialog();

                    mblnCashDrop = false;

                    if (Session.CurrentEmployee != null && Session.CurrentEmployee.LoginDetail != null)
                    {
                        if (!string.IsNullOrEmpty(Session.CurrentEmployee.LoginDetail.EmployeeCode) && Session.CurrentEmployee.LoginDetail.blnCashDrops)
                        {
                            blnLoginSuccessful = true;
                            SpecialAccessEmployeeCode = Session.CurrentEmployee.LoginDetail.EmployeeCode;
                        }
                        else
                        {
                            blnLoginSuccessful = false;
                            CustomMessageBox.Show(APILayer.GetCatalogText(LanguageConstant.cintMSGInsufficientPrivileges), CustomMessageBox.Buttons.OK, CustomMessageBox.Icon.Warning);
                            //return;
                        }
                    }
                    else
                        blnLoginSuccessful = false;

                    Session.CurrentEmployee = oldLoginEmployee;
                }

                if (!string.IsNullOrEmpty(SpecialAccessEmployeeCode))
                {
                    if (string.IsNullOrEmpty(tdbnAmount.Text))
                    {
                        CustomMessageBox.Show(MessageConstant.EnterCashDrop, CustomMessageBox.Buttons.OK, CustomMessageBox.Icon.Warning);
                        tdbnAmount.Focus();
                        return;
                    }

                    string Response = APILayer.EmployeeCashDrop(Session._LocationCode, Session.SystemDate, Employee_Code, Convert.ToDecimal(tdbnAmount.Text), SpecialAccessEmployeeCode);
                    if (Response == "1")
                    {
                        CustomMessageBox.Show(MessageConstant.CashDropSuccess, CustomMessageBox.Buttons.OK, CustomMessageBox.Icon.Info);
                        PrintFunctions.PrintCashDrop(Session._LocationCode, Session._WorkStationID, Employee_Code, SpecialAccessEmployeeCode,Convert.ToDecimal(tdbnAmount.Text),true );
                        tdbnAmount.Text = "";
                        Employee_Code = "";
                        cmdOk.Enabled = false;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Trace("ERROR", "frmCashDrop-cmdOk_Click(): " + ex.Message, ex, true);
            }
        }

       

        private void tdbnAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8 && e.KeyChar != 46))
                {
                    e.Handled = true;
                    return;
                }

                // checks to make sure only 1 decimal is allowed
                if (e.KeyChar == 46)
                {
                    if ((sender as TextBox).Text.IndexOf(e.KeyChar) != -1)
                        e.Handled = true;
                }

                
                int Amount = Convert.ToInt32(tdbnAmount.Text);
                if (string.IsNullOrEmpty(Employee_Code))
                {
                    cmdOk.Enabled = false;
                }
                else
                {
                    if (Amount > 0)
                        cmdOk.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                Logger.Trace("ERROR", "frmCashDrop-tdbnAmount_KeyPress(): " + ex.Message, ex, true);
            }
        }        
    }

    public class Employee
    {
     public int SrNo;
    public string employee_Code;
    public string name;

}
}
