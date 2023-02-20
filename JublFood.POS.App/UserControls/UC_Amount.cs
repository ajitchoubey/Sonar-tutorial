using System;
using System.Windows.Forms;

namespace JublFood.POS.App.UserControls
{
    public partial class UC_Amount : UserControl
    {
        public UC_Amount()
        {
            InitializeComponent();
        }

        private void btn_Exact_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            if(btn.Text=="Exact")
            {

            }
        }
    }
}
