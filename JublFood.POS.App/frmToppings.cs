using Jublfood.AppLogger;
using JublFood.POS.App.API;
using JublFood.POS.App.Cache;
using JublFood.POS.App.Class;
using JublFood.POS.App.Models.Catalog;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace JublFood.POS.App
{
    public partial class frmToppings : Form
    {
        public frmToppings()
        {
            InitializeComponent();            
            SetButtonText();
            CheckTrainningMode();

        }
        int Counter = 14;
        private void SetButtonText()
        {
            string labelText = null;
            if (Common.DictAllButtonText.TryGetValue(LanguageConstant.cintClose, out labelText))
            {
                cmdClose.Text = labelText;
            }
        }
        private void cmdDown_Click(object sender, EventArgs e)
        {            
            try
            {
                if (dgvToppings.FirstDisplayedScrollingRowIndex <= dgvToppings.RowCount - 1) dgvToppings.FirstDisplayedScrollingRowIndex = dgvToppings.FirstDisplayedScrollingRowIndex + 1;
            }
            catch (Exception ex)
            {
                Logger.Trace("ERROR", "frmToppings-cmdDown_click(): " + ex.Message, ex, true);
            }

        }

        private void cmdUp_Click(object sender, EventArgs e)
        {            
            try
            {                
                if (dgvToppings.FirstDisplayedScrollingRowIndex > 0) dgvToppings.FirstDisplayedScrollingRowIndex = dgvToppings.FirstDisplayedScrollingRowIndex - 1;
            }
            catch (Exception ex)
            {
                Logger.Trace("ERROR", "frmToppings-cmdup_click(): " + ex.Message, ex, true);
            }
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void LoadToppingDescrption()
        {
            try
            {
                List<CatlogToppingDescriptonCode> lstcatlogToppingDescriptonCode = APILayer.GetCatalogToppingDescrptionCode(SystemSettings.LocationCodes.LocationCode);
             
                dgvToppings.DataSource = lstcatlogToppingDescriptonCode;

                dgvToppings.Columns["Toppings"].Visible = false;
                dgvToppings.Columns["SpecialtyPizzas"].Width = 190;
                dgvToppings.Columns["ToppingsDescription"].Width = 370;

                dgvToppings.Columns["SpecialtyPizzas"].HeaderText = "Specialty Pizzas";
                dgvToppings.Columns["ToppingsDescription"].HeaderText = "Toppings";

                //System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();                
                //dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
                //dataGridViewCellStyle1.BackColor = System.Drawing.Color.Teal;
                //dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                //dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
                //dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.Teal;
                //dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
                //dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
                //this.dgvToppings.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;

                System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
                dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
                dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
                dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
                dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
                dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
                dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
                this.dgvToppings.DefaultCellStyle = dataGridViewCellStyle2;

                System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
                dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
                dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Menu;
                dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
                dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
                dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
                dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;                
                this.dgvToppings.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle3;

            }
            catch (Exception ex)
            {
                Logger.Trace("ERROR", "frmToppings-loadtoppingdescription(): " + ex.Message, ex, true);
            }
        }

        private void frmToppings_Load(object sender, EventArgs e)
        {
            LoadToppingDescrption();
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

                pnl_Topping.BackColor = color;
            }
            catch (Exception ex)
            {
                Logger.Trace("ERROR", ex.Message, ex, true);
            }
        }
    }

}
