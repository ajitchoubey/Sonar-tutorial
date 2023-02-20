using JublFood.POS.App.Cache;
using System;
using System.Windows.Forms;

namespace JublFood.POS.App
{
    public partial class frmKeyBoardNumeric : Form
    {
        
        public frmKeyBoardNumeric()
        {
            InitializeComponent();
        }
        TextBox txtBox = new TextBox();
        public frmKeyBoardNumeric(TextBox txt, string caption)
        {
            InitializeComponent();
            this.Text = caption;
            txtBox = txt;
        }
        
        private void btn_Enter_Click(object sender, EventArgs e)
        {
            if(txt_Input.TextLength > Session.MaxPhoneDigits)
            {
                txtBox.Text = txt_Input.Text.Substring(0, Session.MaxPhoneDigits);
            }
            else
            {
                txtBox.Text = txt_Input.Text;
            }
            
            this.Close();
        }

        private void txt_Input_Enter(object sender, EventArgs e)
        {
            TextBox txt_Input = (TextBox)sender;
            uC_KeyBoardNumeric.txtUserID = txt_Input;
        }

        private void txt_Input_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btn_Enter_Click(this, new EventArgs());
            }
        }

        private void txt_Input_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !Char.IsNumber(e.KeyChar) && e.KeyChar != 8 && e.KeyChar != 46;
        }
    }
}
