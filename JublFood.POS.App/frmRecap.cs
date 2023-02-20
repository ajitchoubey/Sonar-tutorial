using Jublfood.AppLogger;
using JublFood.POS.App.API;
using JublFood.POS.App.BusinessLayer;
using JublFood.POS.App.Cache;
using JublFood.POS.App.Class;
using JublFood.POS.App.Models.Catalog;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace JublFood.POS.App
{
    public partial class frmRecap : Form
    {
        public bool blnEstimatedTime;
        private bool ALT_F4 = false;

        public frmRecap()
        {
            InitializeComponent();
            SetControlTextAsync();
        }
        private void frm_Recap_Load(object sender, EventArgs e)
        {
            Logger.Trace("INFO", "S-12.3 PaymentComplete : ProcessPayments is success", null, false);
            if (!blnEstimatedTime)
            {
                lblBeginET.Text = Convert.ToString(Session.cart.cartHeader.Order_Type_Code == "D" ? Session.EstimatedDeliveryTime : Session.EstimatedCarryOutTime);
                lblEndET.Text = Convert.ToString((Session.cart.cartHeader.Order_Type_Code == "D" ? Session.EstimatedDeliveryTime : Session.EstimatedCarryOutTime) + 10);

                tdbtOrderTime.Text = DateTime.Now.ToString("h:mm tt");

                if (Session.cart.cartHeader.Delayed_Date == DateTime.MinValue)
                {
                    ltxtDelayedTimeDate.Visible = false;
                    tdbdDelayedOrderDate.Visible = false;
                    tdbtDelayedOrderTime.Visible = false;
                    tlpRecap.RowStyles[2].Height = 0;
                }
                else
                {
                    ltxtDelayedTimeDate.Visible = true;
                    tdbdDelayedOrderDate.Visible = true;
                    tdbtDelayedOrderTime.Visible = true;
                    tdbdDelayedOrderDate.Text = Session.cart.cartHeader.Delayed_Date.ToString("M/d/yyyy");
                    tdbtDelayedOrderTime.Text = Session.cart.cartHeader.Delayed_Date.ToString("h:mm tt");
                }

                lblOrderTotal.Text = String.Format(Session.DisplayFormat, Session.cart.cartHeader.Total);

                switch (Session.cart.cartHeader.Order_Type_Code)
                {
                    case "D":
                        lblEstimatedTime.Text = APILayer.GetCatalogText(LanguageConstant.cintEstimatedDeliveryTime); 
                        break;
                    case "P":
                        lblEstimatedTime.Text = APILayer.GetCatalogText(LanguageConstant.cintEstimatedPickUpTime); 
                        break;
                    case "C":
                        lblEstimatedTime.Text = APILayer.GetCatalogText(LanguageConstant.cintEstimatedCarryOutTime); 
                        break;
                    case "I":
                        lblEstimatedTime.Text = APILayer.GetCatalogText(LanguageConstant.cintEstimatedDineInTime); 
                        break;
                }
                Logger.Trace("INFO", "S-12.4 PaymentComplete : ProcessPayments is success", null, false);
                tlpRecap.RowStyles[5].Height = 0;

                if (tlpRecap.RowStyles[2].Height == 0 && tlpRecap.RowStyles[5].Height == 0)
                    this.Size = new Size(this.Size.Width, this.Size.Height - 70);
                else
                    this.Size = new Size(this.Size.Width, this.Size.Height - 35);
            }
            else
            {
                lblOrderTotal.Visible = false;
                ltxtCurrentTime.Visible = false;
                ltxtDelayedTimeDate.Visible = false;
                ltxtOrderTotal.Visible = false;
                tdbdDelayedOrderDate.Visible = false;
                tdbtDelayedOrderTime.Visible = false;
                tdbtOrderTime.Visible = false;
                lblBeginET2.Visible = true;
                lblDash2.Visible = true;
                lblEndET2.Visible = true;
                lblEstimatedTime2.Visible = true;
                ltxtMinutes2.Visible = true;
                //UserFunctions.GetEstimatedTimes();
                lblBeginET.Text = Convert.ToString(Session.EstimatedDeliveryTime);
                lblEndET.Text = Convert.ToString((Session.EstimatedDeliveryTime) + 10);

                lblBeginET2.Text = Convert.ToString(Session.EstimatedCarryOutTime);
                lblEndET2.Text = Convert.ToString((Session.EstimatedCarryOutTime) + 10);

                lblEstimatedTime.Text = APILayer.GetCatalogText(LanguageConstant.cintEstimatedDeliveryTime); 
                lblEstimatedTime2.Text = APILayer.GetCatalogText(LanguageConstant.cintEstimatedCarryOutTime); 

                tlpRecap.RowStyles[1].Height = 0;
                tlpRecap.RowStyles[2].Height = 0;
                tlpRecap.RowStyles[3].Height = 0;

                this.Size = new Size(this.Size.Width, this.Size.Height - 105);
            }

            cmdClose.Select();
            Logger.Trace("INFO", "S-12.5 PaymentComplete : ProcessPayments is success", null, false);
        }
        private void cmdClose_Click(object sender, EventArgs e)
        {
            Session.Upsellcnt = 0;
            this.Close();
        }
        private void SetControlTextAsync()
        {
            // BAL obj = new BAL();
            //List<FormField> listFormField = obj.GetControlText("frmRecap");
            Logger.Trace("INFO", "S-12.1 PaymentComplete : ProcessPayments is success", null, false);
            Session.catalogControlText = APILayer.GetControlText("frmRecap");
            Logger.Trace("INFO", "S-12.2 PaymentComplete : ProcessPayments is success", null, false);
            foreach (Control ctl in this.tlpRecap.Controls)
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
            string labelText = null;
            if (Common.DictAllButtonText.TryGetValue(LanguageConstant.cintClose, out labelText))
            {
                cmdClose.Text = labelText;

            }

        }

        private void frmRecap_KeyDown(object sender, KeyEventArgs e)
        {
            ALT_F4 = (e.KeyCode.Equals(Keys.F4) && e.Alt == true);
        }

        private void frmRecap_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (ALT_F4)
            {
                ALT_F4 = false;
                e.Cancel = true;
                return;
            }
        }

        private void tlpRecap_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
