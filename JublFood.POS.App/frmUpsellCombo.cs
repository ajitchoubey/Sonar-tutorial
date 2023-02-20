using JublFood.POS.App.Class;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using JublFood.POS.App.Models.Catalog;
using JublFood.POS.App.Cache;

namespace JublFood.POS.App
{
    public partial class frmUpsellCombo : Form
    {
        string MenuCode1, SizeCode1, MenuCode2, SizeCode2;

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmUpsellCombo_Load(object sender, EventArgs e)
        {
            if (Session.CombosAvailableForUpsell != null && Session.CombosAvailableForUpsell.Count > 0)
                PopulateCombos();
        }

        public frmUpsellCombo()
        {
            InitializeComponent();
        }

        public frmUpsellCombo(string MenuCode1, string SizeCode1, string MenuCode2 = "", string SizeCode2 = "")
        {
            InitializeComponent();
            this.MenuCode1 = MenuCode1;
            this.SizeCode1 = SizeCode1;
            this.MenuCode2 = MenuCode2;
            this.SizeCode2 = SizeCode2;
        }

        private void PopulateCombos()
        {
            int x = Constants.HorizontalSpace, y = Constants.VerticalSpace;

            foreach (CombosForUpsell combo in Session.CombosAvailableForUpsell)
            {
                Label lblComboName = new Label();
                lblComboName.Location = new System.Drawing.Point(x, y);
                lblComboName.AutoSize = true;
                lblComboName.Font = new Font(new FontFamily("Microsoft Sans Serif"), 8);
                lblComboName.Text = combo.Combo_Size_Description == "." ? combo.Combo_Description : combo.Combo_Size_Description + " " + combo.Combo_Description;

                Panel panelCombo = new Panel();
                panelCombo.Location = new System.Drawing.Point(x, y);
                panelCombo.Size = new System.Drawing.Size(200, 100);
                panelCombo.Controls.Add(lblComboName);

                flowLayoutPanelmain.Controls.Add(panelCombo);
                
            }
        }
        }
}
