using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using JublFood.POS.App.Class;
using Jublfood.AppLogger;
using JublFood.POS.App.Cache;
using JublFood.POS.App.Models.Cart;
using JublFood.POS.App.BusinessLayer;


using JublFood.POS.App.API;
//using JublFood.POS.App.Models.Catalog;
//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Drawing;
//using System.IO;




namespace JublFood.POS.App
{
    public partial class frmTaxPrompt : Form
    {
        public frmTaxPrompt()
        {
            InitializeComponent();
        }

        public void frmTaxPrompt_Load(object sender, EventArgs e)
        {
            try
            {
            string labelText = null;
            if (Common.DictAllButtonText.TryGetValue(LanguageConstant.cintMSGODCTax, out labelText))
            {
                lblODCTax.Text = labelText;
                btnDefaulttax.Text= SystemSettings.GetSettingValue("UserDefinedTax4Description", Session._LocationCode);
                btnChangeTax.Text = SystemSettings.GetSettingValue("ODC_Change_Tax_Description", Session._LocationCode);
                    if (Session.ODC_Tax)
                    {
                        btnDefaulttax.BackColor =SystemColors.Control;
                        btnChangeTax.BackColor = Session.DefaultEntityColor;
                    }
                    else
                    {
                        btnDefaulttax.BackColor = Session.DefaultEntityColor;
                        btnChangeTax.BackColor = SystemColors.Control;
                    }
             }

            }
            catch (Exception ex)
            {
                Logger.Trace("ERROR", "frmTaxPrompt-frmTaxprompt_load(): " + ex.Message, ex, true);
            }
        }

        private void btnChangeTax_Click(object sender, EventArgs e)
        {
            try
            {
                Session.ODC_Tax = true;
                CartFunctions.ODCTaxChange();
                btnDefaulttax.BackColor = SystemColors.Control;
                btnChangeTax.BackColor = Session.DefaultEntityColor;

                frmOrder frmord = null;
                foreach (Form form in Application.OpenForms)
                {
                    if (form.Name == "frmOrder")
                        frmord = (frmOrder)form;
                }
                if (frmord != null)
                {
                    frmord.InitializeCart();
                    frmord.RefreshCartUI();
                }

                this.Close();
            }
            catch (Exception ex)
            {
                Logger.Trace("ERROR", "frmTaxPrompt-btnchangetax_click(): " + ex.Message, ex, true);
            }
        }

        private void btnDefaulttax_Click(object sender, EventArgs e)
        {
            try
            {
                Session.ODC_Tax = false;
                CartFunctions.ODCTaxChange();
                btnChangeTax.BackColor = SystemColors.Control;
                btnDefaulttax.BackColor = Session.DefaultEntityColor;
                
                frmOrder frmord = null;
                foreach (Form form in Application.OpenForms)
                {
                    if (form.Name == "frmOrder")
                        frmord = (frmOrder)form;
                }
                if (frmord != null)
                {
                    frmord.InitializeCart();
                    frmord.RefreshCartUI();
                }

                this.Close();
            }
            catch (Exception ex)
            {
                Logger.Trace("ERROR", "frmTaxPrompt-btnDefaulttax_click(): " + ex.Message, ex, true);
            }
        }
  
    }
}
