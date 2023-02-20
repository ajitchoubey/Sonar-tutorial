using JublFood.POS.App.Class;
using System;
using System.Windows.Forms;

namespace JublFood.POS.App
{

    public partial class frmDetermineOrderDate : Form
    {
        public string date1;
        public string date2;
        public bool blnCancel;
        public frmDetermineOrderDate()
        {
            InitializeComponent();
        }

        private void cmdDate1_Click(object sender, EventArgs e)
        {
            SetSelectedDate(cmdDate1.Text);
        }

        private void cmdDate2_Click(object sender, EventArgs e)
        {
            SetSelectedDate(cmdDate2.Text);
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            SetSelectedDate(string.Empty);


        }
        private void SetSelectedDate(string date)
        {
            Form frmTimedOrders = Application.OpenForms["frmTimedOrders"];
            if (frmTimedOrders != null)
            {
                frmTimedOrders frm = (frmTimedOrders)frmTimedOrders;
                frm.choosenDate = date;
                this.Close();
            }
        }
        private void frmDetermineOrderDate_Load(object sender, EventArgs e)
        {
            //blnCancel = False
            LoadForm();
            cmdDate1.Text = date1;
            cmdDate2.Text = date2;
        }
        private void LoadForm()
        {
            string labelText = null;
            if (Common.DictAllButtonText.TryGetValue(LanguageConstant.cintCancel, out labelText))
            {
                cmdCancel.Text = labelText;
            }
            if (Common.DictAllButtonText.TryGetValue(LanguageConstant.cintMSGDelayedOrderAfterClose1, out labelText))
            {
                lblMSGPart1.Text = labelText;
            }
            if (Common.DictAllButtonText.TryGetValue(LanguageConstant.cintMSGDelayedOrderAfterClose2, out labelText))
            {
                lblMSGPart2.Text = labelText;
            }
        }
    }
}
