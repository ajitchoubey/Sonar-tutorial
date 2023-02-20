using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using JublFood.POS.App.Class;
namespace JublFood.POS.App
{
    public partial class UC_KeyBoard : UserControl
    {
        public  TextBox txt_Input{get;set;}
        public UC_KeyBoard()
        {
            InitializeComponent();
            SetButtonText();
        }


        [DllImport("user32.dll")]
        static extern short VkKeyScan(char c);
        [DllImport("user32.dll")]
        static extern short VkKeyScanEx(char c);
      
        private char GetKeyCharWithShiftEffect(Char Ch, byte bytcode)
        {
            char result = ' ';
            var scankey = VkKeyScan(Ch);
            uint code = (uint)scankey & 0xff;
            byte[] byt = new byte[256];
            byt[0x10] = bytcode;//0x80;
            uint ShiftAsci;
            if (ToAscii(code, code, byt, out ShiftAsci, 0) == 1)
            {
                result = (char)ShiftAsci;
            }
            return result;
        }

        [DllImport("user32.dll", SetLastError = true)]
        static extern int ToAscii(
            uint uVirtKey,
            uint uScanCode,
            byte[] lpKeyState,
            out uint lpChar,
            uint flags
            );

        private void btn1_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            if (txt_Input.Text.Length >= txt_Input.MaxLength)
                return;
            else
            {
                txt_Input.Text = txt_Input.Text + btn.Text;
                txt_Input.Focus();

                txt_Input.Select(txt_Input.Text.Length, 0);
            }
        }

        private void btn_shift_Click(object sender, EventArgs e)
        {
            if (btn_a.Text == "a")
            {
                foreach (Control ctl in pnl_Password.Controls)
                {
                    if (ctl is Button && ctl.Text.Length == 1)
                    {
                        ctl.Text = GetKeyCharWithShiftEffect(ctl.Text[0], 0x80).ToString().Replace("&", "&&");
                    }
                }
            }
            else
            {
                btn_7.Text = "&";
                foreach (Control ctl in pnl_Password.Controls)
                {

                    if (ctl is Button && (ctl.Text.Length == 1))
                    {
                        ctl.Text = GetKeyCharWithShiftEffect(ctl.Text[0], 0).ToString();
                    }
                }
            }

        }
        private void btn_backSpace_Click(object sender, EventArgs e)
        {
            int currentSelection = txt_Input.SelectionStart;
            if (txt_Input.Text.Length > 0)
            {
                txt_Input.Focus();
                if (txt_Input.SelectionStart > 0)
                {
                    txt_Input.Text = txt_Input.Text.Remove(txt_Input.SelectionStart - 1, 1);
                    txt_Input.Select(currentSelection - 1, 0);
                }
            }
        }
        private void btn_Left_Click(object sender, EventArgs e)
        {
            txt_Input.Focus();
            if (txt_Input.SelectionStart > 0)
            {
                txt_Input.Select(txt_Input.SelectionStart - 1, 0);
            }
        }

        private void btn_Right_Click(object sender, EventArgs e)
        {
            txt_Input.Focus();
            if (txt_Input.SelectionStart < txt_Input.Text.Length)
            {
                txt_Input.Select(txt_Input.SelectionStart + 1, 0);
            }
        }
        private void btn_Spacebar_Click(object sender, EventArgs e)
        {
            txt_Input.Text = txt_Input.Text + " ";
            txt_Input.Focus();
            txt_Input.Select(txt_Input.Text.Length+1, 0);
        }

        private void pnl_Password_Paint(object sender, PaintEventArgs e)
        {

        }

        private void UC_KeyBoard_Load(object sender, EventArgs e)
        {

        }

        private void SetButtonText()
        {
            string labelText = null;

            if (Common.DictAllButtonText.TryGetValue(LanguageConstant.cintShift, out labelText))
            {
                btn_shift.Text = labelText;
            }
            if (Common.DictAllButtonText.TryGetValue(LanguageConstant.cintBackSpace, out labelText))
            {
                btn_backSpace.Text = labelText;
            }
            if (Common.DictAllButtonText.TryGetValue(LanguageConstant.cintLeft, out labelText))
            {
                btn_Left.Text = labelText;
            }
            if (Common.DictAllButtonText.TryGetValue(LanguageConstant.cintRight, out labelText))
            {
                btn_Right.Text = labelText;
            }
        }





    }
}
