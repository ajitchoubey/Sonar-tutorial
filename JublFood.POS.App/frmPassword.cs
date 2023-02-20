using System;
using System.Windows.Forms;

namespace JublFood.POS.App
{
    public partial class frmPassword : Form
    {
        private const int CP_NOCLOSE_BUTTON = 0x200;
        private bool ALT_F4 = false;
        public string TypedPassword = string.Empty;
        public Form CallerForm;
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams myCp = base.CreateParams;
                myCp.ClassStyle = myCp.ClassStyle | CP_NOCLOSE_BUTTON;
                return myCp;
            }
        }

        public frmPassword()
        {
            InitializeComponent();
        }
        public frmPassword(Form callerForm)
        {
            InitializeComponent();
            CallerForm = callerForm;
        }
    
        private void txt_Input_Enter(object sender, EventArgs e)
        {
            TextBox txt_Input = (TextBox)sender;
            UC_KeyBoard.txt_Input = txt_Input;
        }

        private void btn_enter_Click(object sender, EventArgs e)
        {
            //frmCustomer objfrmCustomer = new frmCustomer();
            //objfrmCustomer.Show();
            //this.Close();
            TypedPassword = Convert.ToString(txt_Input.Text);

             this.Close();

            if (CallerForm != null)
            {
                CallerForm.Closed += (s, args) => this.Close();
                CallerForm.ShowDialog();
            }
        }

        private void txt_Input_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (e.KeyChar != (char)Keys.Back)
            //    e.Handled = !Char.IsNumber(e.KeyChar);
        }

        private void frmPassword_FormClosing(object sender, FormClosingEventArgs e)
        {
            //e.Cancel = true;
            if (ALT_F4)
            {
                ALT_F4 = false;
                e.Cancel = true;
                return;
            }
        }

        private void frmPassword_KeyDown(object sender, KeyEventArgs e)
        {
            ALT_F4 = (e.KeyCode.Equals(Keys.F4) && e.Alt == true);
        }
    }
}
