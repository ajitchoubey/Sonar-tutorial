using JublFood.POS.App.Class;
using System;
using System.Windows.Forms;

namespace JublFood.POS.App
{
    public partial class frmKeyBoard : Form
    {
        public frmKeyBoard()
        {
            InitializeComponent();
            SetButtonText();
        }

        TextBox txtBox;
        public frmKeyBoard(TextBox txt, string caption)
        {
            InitializeComponent();
            this.Text = caption;
            txtBox = txt;
            txtBox.MaxLength = txt.MaxLength;
        }
        
        private void btn_enter_Click(object sender, EventArgs e)
        {
            txtBox.Text = txt_Input.Text;
            this.Close();
        }

        private void txt_Input_Enter(object sender, EventArgs e)
        {
            TextBox txt_Input = (TextBox)sender;
            UC_KeyBoard.txt_Input = txt_Input;
        }

        private void UC_KeyBoard_Load(object sender, EventArgs e)
        {

        }
        private void SetButtonText()
        {
            string labelText = null;

            if (Common.DictAllButtonText.TryGetValue(LanguageConstant.cintEnter, out labelText))
            {
                btn_enter.Text = labelText;
            }
        }

        private void txt_Input_KeyPress(object sender, KeyPressEventArgs e)
        {
            //e.Handled = !Char.IsNumber(e.KeyChar) && e.KeyChar != 8;
        }

        private void txt_Input_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btn_enter_Click(this, new EventArgs());
            }
        }
    }
}
