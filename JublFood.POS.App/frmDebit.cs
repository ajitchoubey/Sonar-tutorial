using Jublfood.AppLogger;
using JublFood.POS.App.Cache;
using JublFood.POS.App.Class;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace JublFood.POS.App
{
    public partial class frmDebit : Form
    {
        public frmDebit()
        {
            InitializeComponent();
            SetButtonText();
            CheckTrainningMode();
        }

        void SetButtonText()
        {
            string labelText = null;
            if (Common.DictAllButtonText.TryGetValue(LanguageConstant.cintOK, out labelText))
            {
                cmdOK.Text = labelText;
            }
            if (Common.DictAllButtonText.TryGetValue(LanguageConstant.cintCancel, out labelText))
            {
                cmdCancel.Text = labelText;
            }


        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
         
            this.Close();
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

                pnl_Debit.BackColor = color;
            }
            catch (Exception ex)
            {
                Logger.Trace("ERROR", ex.Message, ex, true);
            }
        }
    }
}
