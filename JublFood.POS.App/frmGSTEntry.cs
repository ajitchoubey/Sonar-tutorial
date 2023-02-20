using Jublfood.AppLogger;
using JublFood.POS.App.Cache;
using JublFood.POS.App.Class;
using System;
using System.Drawing;
using System.Windows.Forms;
using JublFood.POS.App.API;
using System.Collections.Generic;
using JublFood.POS.App.BusinessLayer;
using JublFood.POS.App.Models.Order;
using JublFood.POS.App.Models.Catalog;
using System.Data;
using JublFood.POS.App.Models.Cart;
using JublFood.POS.App.Models.Customer;


namespace JublFood.POS.App
{
    public partial class frmGSTEntry : Form
    {
        public frmGSTEntry()
        {
            InitializeComponent();
        }

        private void frmGSTEntry_Load(object sender, EventArgs e)
        {

            txtGSTNumber.LostFocus += TxtGSTNumber_LostFocus;
            lblGSTNumber.Text = APILayer.GetCatalogText(LanguageConstant.cintMSGGSTINLabel);
            if(SystemSettings.settings.pbytGSTIN_Mandatory==0)
            {
                btnCancel.Text = APILayer.GetCatalogText(LanguageConstant.cintCancel); 
            }
            else
            {
                btnCancel.Visible = false;
                lblGSTNumber.ForeColor = System.Drawing.Color.Yellow; 
            }

            if(Session.cart.Customer.Phone_Number.Trim().Length >= Session.MinPhoneDigits)
            {
                txtGSTNumber.Text= Session.cart.Customer.gstin_number; 
            }
        }

        private void TxtGSTNumber_LostFocus(object sender, EventArgs e)
        {
            txtGSTNumber.Text = txtGSTNumber.Text.ToUpper();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                if (SystemSettings.settings.pbytGSTIN_Mandatory == 1)
                {
                    if (txtGSTNumber.Text == "")
                    {
                        CustomMessageBox.Show(APILayer.GetCatalogText(LanguageConstant.cintMSGAllRequiredFieldsYellow));
                        return;
                    }
                    else
                    {
                        Session.cart.Customer.gstin_number = txtGSTNumber.Text;
                    }
                }
                else
                {
                    Session.cart.Customer.gstin_number = txtGSTNumber.Text;
                }
                this.Close();
            }
            catch(Exception ex)
            {
                Logger.Trace("ERROR", "frmGSTEntry-btnOK_Click(): " + ex.Message, ex, true);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnKeyboard_Click(object sender, EventArgs e)
        {
            try
            {
                using (frmKeyBoard objfrmKeyBoard = new frmKeyBoard(txtGSTNumber, "GSTIN Number"))
                {
                    objfrmKeyBoard.ShowDialog();
                }
            }
            catch(Exception ex)
            {
                Logger.Trace("ERROR", ex.Message, ex, true);
            }
        }
    }
}
