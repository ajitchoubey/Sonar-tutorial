using JublFood.POS.App.Cache;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace JublFood.POS.App
{
    public partial class UC_KeyBoardNumeric : UserControl
    {
        public UC_KeyBoardNumeric()
        {
            InitializeComponent();
        }
        public TextBox txtUserID { get; set; }
        private void btn1_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;

            if (txtUserID != null)
            {
                if (txtUserID.Text.Length >= txtUserID.MaxLength)
                    return;
                txtUserID.Text += btn.Text;
            }
        }

        private void btn_Back_Click(object sender, EventArgs e)
        {
            if (txtUserID != null)
            {
                if (txtUserID.Text.Length > 0)
                {
                    txtUserID.Text = txtUserID.Text.Remove(txtUserID.Text.Length - 1);
                }
            }
        }

        private void UC_KeyBoardNumeric_Load(object sender, EventArgs e)
        {
           
        }
        public void ChangeButtonColor(Color color)
        {
            foreach (Control ctl in this.Controls)
            {
                if (ctl is Button)
                {
                    ctl.BackColor = color;
                }
            }
        }
    }
}
