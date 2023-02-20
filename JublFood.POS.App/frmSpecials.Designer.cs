namespace JublFood.POS.App
{
    partial class frmSpecials
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
            this.pnl_frmSpecials = new System.Windows.Forms.Panel();
            this.cmdUp = new System.Windows.Forms.Button();
            this.cmdClose = new System.Windows.Forms.Button();
            this.cmdDown = new System.Windows.Forms.Button();
            this.txt_Specials = new System.Windows.Forms.RichTextBox();
            this.pnl_frmSpecials.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnl_frmSpecials
            // 
            this.pnl_frmSpecials.BackColor = System.Drawing.Color.Teal;
            this.pnl_frmSpecials.Controls.Add(this.txt_Specials);
            this.pnl_frmSpecials.Controls.Add(this.cmdUp);
            this.pnl_frmSpecials.Controls.Add(this.cmdClose);
            this.pnl_frmSpecials.Controls.Add(this.cmdDown);
            this.pnl_frmSpecials.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl_frmSpecials.Location = new System.Drawing.Point(0, 0);
            this.pnl_frmSpecials.Name = "pnl_frmSpecials";
            this.pnl_frmSpecials.Size = new System.Drawing.Size(529, 367);
            this.pnl_frmSpecials.TabIndex = 0;
            // 
            // cmdUp
            // 
            this.cmdUp.BackColor = System.Drawing.Color.PeachPuff;
            this.cmdUp.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdUp.Image = global::JublFood.POS.App.Properties.Resources._34;
            this.cmdUp.Location = new System.Drawing.Point(5, 303);
            this.cmdUp.Name = "cmdUp";
            this.cmdUp.Size = new System.Drawing.Size(68, 55);
            this.cmdUp.TabIndex = 39;
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
            this.cmdClose.Location = new System.Drawing.Point(455, 303);
            this.cmdClose.Name = "cmdClose";
            this.cmdClose.Size = new System.Drawing.Size(68, 55);
            this.cmdClose.TabIndex = 38;
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
            this.cmdDown.Location = new System.Drawing.Point(73, 303);
            this.cmdDown.Name = "cmdDown";
            this.cmdDown.Size = new System.Drawing.Size(68, 55);
            this.cmdDown.TabIndex = 38;
            this.cmdDown.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.cmdDown.UseVisualStyleBackColor = false;
            this.cmdDown.Click += new System.EventHandler(this.cmdDown_Click);
            // 
            // txt_Specials
            // 
            this.txt_Specials.BackColor = System.Drawing.SystemColors.Menu;
            this.txt_Specials.Location = new System.Drawing.Point(3, 3);
            this.txt_Specials.Name = "txt_Specials";
            this.txt_Specials.ReadOnly = true;
            this.txt_Specials.Size = new System.Drawing.Size(520, 296);
            this.txt_Specials.TabIndex = 40;
            this.txt_Specials.Text = "";
            // 
            // frmSpecials
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(529, 367);
            this.Controls.Add(this.pnl_frmSpecials);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSpecials";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.frmSpecials_Load);
            this.pnl_frmSpecials.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnl_frmSpecials;
        private System.Windows.Forms.Button cmdUp;
        private System.Windows.Forms.Button cmdClose;
        private System.Windows.Forms.Button cmdDown;
        private System.Windows.Forms.RichTextBox txt_Specials;
    }
}