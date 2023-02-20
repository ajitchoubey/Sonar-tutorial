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
    public partial class frmCashRegister : Form
    {
        public frmCashRegister()
        {
            InitializeComponent();
            Combox_Item_Display();
        }
        List<CatalogReasons> lstCatalogReasons = new List<CatalogReasons>();
        void Combox_Item_Display()
        {
            try
            {

                lstCatalogReasons = APILayer.GetCatalogReasons(SystemSettings.settings.pstrDefault_Location_Code, Convert.ToInt32(Session.CurrentEmployee.LoginDetail.LanguageCode), Convert.ToInt16(UserTypes.ReasonGroupID.CashDrawerOpen));
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
            try
            {
                string ReasonText = Convert.ToString(ComCashDrawerOpenReason.SelectedItem);
                if (ComCashDrawerOpenReason.SelectedIndex == 0)
                {
                    CustomMessageBox.Show("Please select reason. ", CustomMessageBox.Buttons.OK, CustomMessageBox.Icon.Warning);
                    return;
                }

                //Cash Drawer open
                CashDrawer cashDrawer = new CashDrawer();
                if (SystemSettings.WorkStationSettings.pstrPOSOrderTypePreference == "1")
                    cashDrawer.OpenDrawer();
                else
                    cashDrawer.OpenDrawerCommon();

                var value = lstCatalogReasons.Find(item => item.System_Text == ReasonText).Reason_ID;

                CashDrawerReason drawerReason = new CashDrawerReason();
                drawerReason.Reason_Group_Code = Convert.ToInt32(UserTypes.ReasonGroupID.CashDrawerOpen);
                drawerReason.Reason_ID = value;
                drawerReason.iStatus = 1;
                drawerReason.Added_By = Session.CurrentEmployee.LoginDetail.EmployeeCode;
                drawerReason.Workstation_id = Session._WorkStationID;
                drawerReason.Order_Number = 0;

                APILayer.InsertCashRegisterReason(drawerReason);
                
                this.Close();
            }
            catch (Exception ex)
            {
                Logger.Trace("ERROR", "FrmCashRegister - cmdOK_Click" + ex.Message, ex, true);
            }
        }

        private void cmdExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        

        
    }
}
