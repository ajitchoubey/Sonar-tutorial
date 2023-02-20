using Jublfood.AppLogger;
using JublFood.POS.App.API;
using JublFood.POS.App.Cache;
using JublFood.POS.App.Class;
using JublFood.POS.App.Models.Catalog;
using System;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace JublFood.POS.App
{
    public partial class frmSpecials : Form
    {
        public frmSpecials()
        {
            InitializeComponent();
            SetButtonText();
            CheckTrainningMode();
        }
        private void SetButtonText()
        {
            string labelText = null;
            if (Common.DictAllButtonText.TryGetValue(LanguageConstant.cintClose, out labelText))
            {
                cmdClose.Text = labelText;
            }
        }
        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmSpecials_Load(object sender, EventArgs e)
        {
            LoadSpecialInformation();
        }

        private void LoadSpecialInformation()
        {
            try
            {
                DateTime systemdate = Session.SystemDate;
                SpecialInformation specialInfo = APILayer.GetSepcialInfo(systemdate.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture), SystemSettings.LocationCodes.LocationCode);
                txt_Specials.Rtf= specialInfo.Notes;
                txt_Specials.SelectAll();
                txt_Specials.SelectionAlignment = HorizontalAlignment.Left;
               
            }
            catch(Exception ex)
            {
                Logger.Trace("ERROR", "frmSpecials-loadspecialinfo(): " + ex.Message, ex, true);
            }
        }

        public void CheckTrainningMode()
        {
            try
            {
                Color color = DefaultBackColor;
                if (SystemSettings.settings.pblnTrainingMode)
                {
                    color = Session.TrainningModeColor;
                }
                else
                {
                    color = Session.NormalModeColor;
                }

                pnl_frmSpecials.BackColor = color;
            }
            catch (Exception ex)
            {
                Logger.Trace("ERROR", ex.Message, ex, true);
            }
        }

        private void cmdDown_Click(object sender, EventArgs e)
        {
            try
            {

                if (txt_Specials.SelectedText.Length < (txt_Specials.Text.Length))
                {
                    txt_Specials.Focus();
                    SendKeys.Send("{PGDN}");
                }
            }
            catch
            {

            }
        }

        private void cmdUp_Click(object sender, EventArgs e)
        {
            try
            {

                //if (txt_Specials.SelectedText.Length > 0)
                {
                    txt_Specials.Focus();
                    SendKeys.Send("{PGUP}");
                }
            }
            catch
            {

            }
        }

        private void txt_Specials_SelectionChanged(object sender, EventArgs e)
        {
            txt_Specials.SelectionAlignment = HorizontalAlignment.Center;
        }
    }
}
