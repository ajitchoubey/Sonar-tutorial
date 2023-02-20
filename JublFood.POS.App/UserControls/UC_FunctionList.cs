using Jublfood.AppLogger;
using JublFood.POS.App.API;
using JublFood.POS.App.BusinessLayer;
using JublFood.POS.App.Cache;
using JublFood.POS.App.Class;
using JublFood.POS.App.Models.Employee;
using System;
using System.Linq;
using System.Windows.Forms;

namespace JublFood.POS.App
{
    public partial class UC_FunctionList : UserControl
    {
        public UC_FunctionList()
        {
            InitializeComponent();
            SetButtonText();
        }


        private void cmdCashdrop_Click(object sender, EventArgs e)
        {
            frmCashDrop frmCashDrop = new frmCashDrop();
            frmCashDrop.ShowDialog();
        }

        private void SetButtonText()
        {
            string labelText = null;
            if (Common.DictAllButtonText.TryGetValue(LanguageConstant.cintTrainingMode, out labelText))
            {
                cmdTraining.Text = labelText;
            }
            if (Common.DictAllButtonText.TryGetValue(LanguageConstant.cintCashDrop, out labelText))
            {
                cmdCashDrop.Text = labelText;
            }
            if (Common.DictAllButtonText.TryGetValue(LanguageConstant.cintPrintCheckout, out labelText))
            {
                cmdPrintCheckout.Text = labelText;
            }

            if (Common.DictAllButtonText.TryGetValue(LanguageConstant.cintTaxExempt, out labelText))
            {
                cmdTaxExempt.Text = labelText;
            }
            if (Common.DictAllButtonText.TryGetValue(LanguageConstant.cintVoid, out labelText))
            {
                cmdVoid.Text = labelText;
            }

            if (Common.DictAllButtonText.TryGetValue(LanguageConstant.cintAddDeliveryFee, out labelText))
            {
                cmdDeliveryFee.Text = labelText;
            }
            if (Common.DictAllButtonText.TryGetValue(LanguageConstant.cintUseCredit, out labelText))
            {
                cmdUseCredit.Text = labelText;
            }
            if (Common.DictAllButtonText.TryGetValue(LanguageConstant.cintGiveCredit, out labelText))
            {
                cmdGiveCredit.Text = labelText;
            }
            //if (Common.DictAllButtonText.TryGetValue(LanguageConstant.cintGiveCredit, out labelText))
            //{
            //    cmdUseDebit.Text = labelText;
            //}
        }

        private void cmdUseCredit_Click(object sender, EventArgs e)
        {
            frmPassword frmPassword = new frmPassword(new frmCredit());
            frmPassword.ShowDialog();
        }

        private void cmdUseDebit_Click(object sender, EventArgs e)
        {
            frmPassword frmPassword = new frmPassword(new frmDebit());
            frmPassword.ShowDialog();
        }

        private void cmdTraining_Click(object sender, EventArgs e)
        {
            EmployeeResult oldLoginEmployee;
            Boolean blnLoginSuccessful;
            bool blnTurnOn;
            string message = string.Empty;
            DialogResult result;
            frmLogin frmLogin = new frmLogin();
            if (!string.IsNullOrEmpty(Session.CurrentEmployee.LoginDetail.EmployeeCode) && Session.CurrentEmployee.LoginDetail.blnTrainingMode)
            {

                if (SystemSettings.settings.pblnRequirePasswordForSpecialAccess)
                {
                    bool status = EmployeeFunctions.MatchEmployeePassword();
                    if (status)
                        blnLoginSuccessful = true;
                    else
                        blnLoginSuccessful = false;
                }
                else
                    blnLoginSuccessful = true;
                if (blnLoginSuccessful)
                {
                    if (!SystemSettings.settings.pblnTrainingMode)
                    {                        
                            Common.DictAllButtonText.TryGetValue(LanguageConstant.cintMSGTrainingMode, out message);
                            result = CustomMessageBox.Show(message, CustomMessageBox.Buttons.YesNo, CustomMessageBox.Icon.Question);

                        
                        if (result == DialogResult.Yes)
                        {
                         SystemSettings.settings.pblnTrainingMode = true;
                          SetColor();
                        }
                       
                    }
                    else
                    {
                        Common.DictAllButtonText.TryGetValue(LanguageConstant.cintMSGTrainingModeOff, out message);
                        result = CustomMessageBox.Show(message, CustomMessageBox.Buttons.YesNo, CustomMessageBox.Icon.Question);

                        if (result == DialogResult.Yes)
                        {
                            SystemSettings.settings.pblnTrainingMode = false;
                            SetColor();
                        }
                    }
                }
            }
            else
            {
                oldLoginEmployee = Session.CurrentEmployee;
                frmLogin.SpecialAccess = true;
                frmLogin.Text = APILayer.GetCatalogText(LanguageConstant.cintSpecialAccess);
                frmLogin.RequirePassword = true;
                frmLogin.ShowDialog();

                if (Session.CurrentEmployee != null && Session.CurrentEmployee.LoginDetail != null)
                {
                    if (!string.IsNullOrEmpty(Session.CurrentEmployee.LoginDetail.EmployeeCode) && Session.CurrentEmployee.LoginDetail.blnTrainingMode)
                    {
                        blnLoginSuccessful = true;

                        if (!SystemSettings.settings.pblnTrainingMode)
                        {
                            Common.DictAllButtonText.TryGetValue(LanguageConstant.cintMSGTrainingMode, out message);
                            result = CustomMessageBox.Show(message, CustomMessageBox.Buttons.YesNo, CustomMessageBox.Icon.Question);

                            if (result == DialogResult.Yes)
                            {
                                SystemSettings.settings.pblnTrainingMode = true;
                                SetColor();
                            }

                        }
                        else
                        {
                            Common.DictAllButtonText.TryGetValue(LanguageConstant.cintMSGTrainingModeOff, out message);
                            result = CustomMessageBox.Show(message, CustomMessageBox.Buttons.YesNo, CustomMessageBox.Icon.Question);

                            if (result == DialogResult.Yes)
                            {
                                SystemSettings.settings.pblnTrainingMode = false;
                                SetColor();
                            }
                        }

                    }
                    else
                    {
                        blnLoginSuccessful = false;
                        CustomMessageBox.Show(APILayer.GetCatalogText(LanguageConstant.cintMSGInsufficientPrivileges), CustomMessageBox.Buttons.OK, CustomMessageBox.Icon.Warning);
                        return;
                    }
                }

                Session.CurrentEmployee = oldLoginEmployee;
            }
            
        }

        private void btnPutOnHold_Click(object sender, EventArgs e)
        {
            if ((SystemSettings.GetSettingValue("CartOnHold", Session._LocationCode) == "1"))
            {
                frmCartOnHold frmCartOnHold = new frmCartOnHold();
                frmCartOnHold.ShowDialog();
                this.Hide();
            }
        }

        private void SetColor()
        {
            try
            {
                Form[] forms = Application.OpenForms.Cast<Form>().ToArray();
                foreach (Form form in forms)
                {
                    if (form.Name == "frmCustomer")
                    {
                        frmCustomer frm = (frmCustomer)form;
                        frm.CheckTrainningMode();

                    }
                    else if (form.Name == "frmOrder")
                    {
                        frmOrder frm = (frmOrder)form;
                        frm.CheckTrainningMode();
                    }
                }

            }
            catch (Exception ex)
            {
                Logger.Trace("ERROR", ex.Message, ex, true);
            }
        }
    }
}
