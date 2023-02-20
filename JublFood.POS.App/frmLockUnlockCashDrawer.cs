using Jublfood.AppLogger;
using JublFood.POS.App.API;
using JublFood.POS.App.BusinessLayer;
using JublFood.POS.App.Cache;
using JublFood.POS.App.Class;
using JublFood.POS.App.Models.Catalog;
using JublFood.POS.App.Models.Order;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JublFood.POS.App
{
    public partial class frmLockUnlockCashDrawer : Form
    {
        bool CDStatus = false;
        public frmLockUnlockCashDrawer()
        {
            InitializeComponent();
        }

        List<CatalogReasons> lstCatalogReasons = new List<CatalogReasons>();
        void Combox_Item_Display()
        {
            try
            {

                lstCatalogReasons = APILayer.GetCatalogReasons(SystemSettings.settings.pstrDefault_Location_Code, Convert.ToInt32(Session.CurrentEmployee.LoginDetail.LanguageCode), Convert.ToInt16(UserTypes.ReasonGroupID.CashDrawerLockUnlock));
                ComCashDrawerOpenReason.Items.Clear();
                ComCashDrawerOpenReason.Items.Add("Please select reason");
                foreach (CatalogReasons item in lstCatalogReasons)
                {
                    ComCashDrawerOpenReason.Items.Add(item.System_Text);
                }
                ComCashDrawerOpenReason.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                Logger.Trace("ERROR", "frmCashDrawer-combox_item_display(): " + ex.Message, ex, true);
                CustomMessageBox.Show(PrintFunctions.System_Error_Message, CustomMessageBox.Buttons.OK, CustomMessageBox.Icon.Warning);

            }

        }
       


        private void cmdOK_Click(object sender, EventArgs e)
        {
            string WorkstationName = string.Empty;

            try
            {
                string ReasonText = Convert.ToString(ComCashDrawerOpenReason.SelectedItem);
                if (ComCashDrawerOpenReason.SelectedIndex == 0)
                {
                    CustomMessageBox.Show("Please select reason. ", CustomMessageBox.Buttons.OK, CustomMessageBox.Icon.Warning);
                    return;
                }

                var value = lstCatalogReasons.Find(item => item.System_Text == ReasonText).Reason_ID;

                CashDrawerLockUnlockRequest  cashDrawerLockUnlockRequest = new CashDrawerLockUnlockRequest();
                cashDrawerLockUnlockRequest.workStationName = Session.ComputerName;
                cashDrawerLockUnlockRequest.workstationID = Session._WorkStationID;
                cashDrawerLockUnlockRequest.employeeID = Session.CurrentEmployee.LoginDetail.EmployeeCode;
                cashDrawerLockUnlockRequest.lockStatus = CDStatus ==true ? false : true;
                cashDrawerLockUnlockRequest.reasonID = value;
                cashDrawerLockUnlockRequest.reasonGrp = Convert.ToInt32(UserTypes.ReasonGroupID.CashDrawerLockUnlock);
                
                bool CashDropResponse = APILayer.CashDrawerLockUpdate(cashDrawerLockUnlockRequest);

               
   
                this.Close();
            }
            catch (Exception ex)
            {
                Logger.Trace("ERROR", "FrmCashRegisterInsert" + ex.Message, ex, true);
            }
        }

        private void cmdExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmLockUnlockCashDrawer_Load(object sender, EventArgs e)
        {
            
            string cashDrawerLockedBy = string.Empty;
            
            CashDrawerInfoDto cashDrawer = APILayer.GetCashDrawerInfo(Session.ComputerName, Session.CurrentEmployee.LoginDetail.EmployeeCode,0);
            if(cashDrawer !=null)
            {
                CDStatus = cashDrawer.cash_register_lock;
                if (CDStatus)
                {
                    this.Text= "Unlock Cash Drawer";
                    lblHeading.Text = "Unlock Cash Drawer";
                    lblInfo.Text = "Cash Drawer is locked by " + cashDrawer.Cash_Register_Locked_By;
                }
                else
                {
                    this.Text = "Lock Cash Drawer";
                    lblHeading.Text = "Lock Cash Drawer";
                    lblInfo.Text = "";
                }
            }
            Combox_Item_Display();
        }
    }
}
