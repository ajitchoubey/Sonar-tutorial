using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using JublFood.POS.App.Class;
using JublFood.POS.App.BusinessLayer;

namespace JublFood.POS.App
{
    public partial class UC_InformationList : UserControl
    {
        public UC_InformationList()
        {
            InitializeComponent();
        }
        private void SetAllbuttonText()
        {
    
            //  If ctlAddress.Visible = True Then
            //        cmdDelivery_Info.Caption = pobjFieldNames.Get_Text(cintOrder)
            //    Else
            //        cmdDelivery_Info.Caption = pobjFieldNames.Get_Text(cintCustomer)
            //    End If

            string labelText = null;
            if (Common.DictAllButtonText.TryGetValue(LanguageConstant.cintSpecials, out labelText))
            {
                btn_Specials.Text = labelText;
            }
            if (Common.DictAllButtonText.TryGetValue(LanguageConstant.cintToppings, out labelText))
            {
                btn_Toppings.Text = labelText;
            }
            if (Common.DictAllButtonText.TryGetValue(LanguageConstant.cintHelp, out labelText))
            {
                btn_Help.Text = labelText;
            }
            if (Common.DictAllButtonText.TryGetValue(LanguageConstant.cintTimes, out labelText))
            {
                btn_Times.Text = labelText;
            }
        }
            private void btn_Times_Click(object sender, EventArgs e)
        {
            frmRecap frmRecap = new frmRecap();
            frmRecap.blnEstimatedTime = true;
            frmRecap.ShowDialog();
        }

        private void btn_Specials_Click(object sender, EventArgs e)
        {
            frmSpecials frmSpecials = new frmSpecials();
            frmSpecials.ShowDialog();
        }

        private void btn_Toppings_Click(object sender, EventArgs e)
        {
            frmToppings frmToppings = new frmToppings();
            frmToppings.ShowDialog();
        }

        public void HandleCashDrawerClick(bool blnEnable)
        {
            btn_CashDrawer.Enabled = blnEnable;
            btn_LockDrawer.Enabled = blnEnable;
        }
        private void btn_CashDrawer_Click(object sender, EventArgs e)
        {
            if (EmployeeFunctions.MatchEmployeePassword())
            {
                frmCashRegister frmCashRegister = new frmCashRegister();

                frmCashRegister.StartPosition = FormStartPosition.CenterScreen;
                frmCashRegister.StartPosition = FormStartPosition.CenterScreen;
                frmCashRegister.ShowDialog();
            }
        }

        private void btn_LockDrawer_Click(object sender, EventArgs e)
        {
            if (EmployeeFunctions.MatchEmployeePassword())
            {
                frmLockUnlockCashDrawer frmCashRegister = new frmLockUnlockCashDrawer();


                frmCashRegister.StartPosition = FormStartPosition.CenterScreen;
                frmCashRegister.StartPosition = FormStartPosition.CenterScreen;
                frmCashRegister.ShowDialog();
            }
        }


    }
}
