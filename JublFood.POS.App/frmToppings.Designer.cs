namespace JublFood.POS.App
{
    partial class frmToppings
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.pnl_Topping = new System.Windows.Forms.Panel();
            this.cmdUp = new System.Windows.Forms.Button();
            this.cmdClose = new System.Windows.Forms.Button();
            this.cmdDown = new System.Windows.Forms.Button();
            this.dgvToppings = new System.Windows.Forms.DataGridView();
            this.pnl_Topping.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvToppings)).BeginInit();
            this.SuspendLayout();
            // 
            // pnl_Topping
            // 
            this.pnl_Topping.BackColor = System.Drawing.Color.Teal;
            this.pnl_Topping.Controls.Add(this.dgvToppings);
            this.pnl_Topping.Controls.Add(this.cmdUp);
            this.pnl_Topping.Controls.Add(this.cmdClose);
            this.pnl_Topping.Controls.Add(this.cmdDown);
            this.pnl_Topping.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl_Topping.Location = new System.Drawing.Point(0, 0);
            this.pnl_Topping.Name = "pnl_Topping";
            this.pnl_Topping.Size = new System.Drawing.Size(581, 491);
            this.pnl_Topping.TabIndex = 0;
            // 
            // cmdUp
            // 
            this.cmdUp.BackColor = System.Drawing.Color.PeachPuff;
            this.cmdUp.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdUp.Image = global::JublFood.POS.App.Properties.Resources._34;
            this.cmdUp.Location = new System.Drawing.Point(4, 431);
            this.cmdUp.Name = "cmdUp";
            this.cmdUp.Size = new System.Drawing.Size(68, 55);
            this.cmdUp.TabIndex = 42;
            this.cmdUp.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.cmdUp.UseVisualStyleBackColor = false;
            this.cmdUp.Click += new System.EventHandler(this.cmdUp_Click);
            // 
            // cmdClose
            // 
            this.cmdClose.BackColor = System.Drawing.Color.PeachPuff;
            this.cmdClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdClose.Image = global::JublFood.POS.App.Properties.Resources._35;
            this.cmdClose.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.cmdClose.Location = new System.Drawing.Point(510, 430);
            this.cmdClose.Name = "cmdClose";
            this.cmdClose.Size = new System.Drawing.Size(68, 55);
            this.cmdClose.TabIndex = 40;
            this.cmdClose.Text = "Close";
            this.cmdClose.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.cmdClose.UseVisualStyleBackColor = false;
            this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
            // 
            // cmdDown
            // 
            this.cmdDown.BackColor = System.Drawing.Color.PeachPuff;
            this.cmdDown.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdDown.Image = global::JublFood.POS.App.Properties.Resources._31;
            this.cmdDown.Location = new System.Drawing.Point(72, 431);
            this.cmdDown.Name = "cmdDown";
            this.cmdDown.Size = new System.Drawing.Size(68, 55);
            this.cmdDown.TabIndex = 41;
            this.cmdDown.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.cmdDown.UseVisualStyleBackColor = false;
            this.cmdDown.Click += new System.EventHandler(this.cmdDown_Click);
            // 
            // dgvToppings
            // 
            this.dgvToppings.AllowUserToAddRows = false;
            this.dgvToppings.AllowUserToDeleteRows = false;
            this.dgvToppings.AllowUserToResizeColumns = false;
            this.dgvToppings.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.dgvToppings.BackgroundColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Teal;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.Teal;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvToppings.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvToppings.ColumnHeadersHeight = 27;
            this.dgvToppings.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvToppings.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvToppings.Dock = System.Windows.Forms.DockStyle.Top;
            this.dgvToppings.EnableHeadersVisualStyles = false;
            this.dgvToppings.GridColor = System.Drawing.SystemColors.Window;
            this.dgvToppings.Location = new System.Drawing.Point(0, 0);
            this.dgvToppings.Name = "dgvToppings";
            this.dgvToppings.ReadOnly = true;
            this.dgvToppings.RowHeadersVisible = false;
            this.dgvToppings.RowTemplate.Height = 27;
            this.dgvToppings.RowTemplate.ReadOnly = true;
            this.dgvToppings.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgvToppings.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvToppings.Size = new System.Drawing.Size(581, 422);
            this.dgvToppings.TabIndex = 45;
            // 
            // frmToppings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(581, 491);
            this.Controls.Add(this.pnl_Topping);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmToppings";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.frmToppings_Load);
            this.pnl_Topping.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvToppings)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnl_Topping;
        private System.Windows.Forms.Button cmdUp;
        private System.Windows.Forms.Button cmdClose;
        private System.Windows.Forms.Button cmdDown;
        private System.Windows.Forms.DataGridView dgvToppings;
    }
}