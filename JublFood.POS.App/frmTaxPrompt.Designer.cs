namespace JublFood.POS.App
{
    partial class frmTaxPrompt
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblODCTax = new System.Windows.Forms.Label();
            this.btnChangeTax = new System.Windows.Forms.Button();
            this.btnDefaulttax = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.lblODCTax);
            this.panel1.Controls.Add(this.btnChangeTax);
            this.panel1.Controls.Add(this.btnDefaulttax);
            this.panel1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(315, 142);
            this.panel1.TabIndex = 0;
            // 
            // lblODCTax
            // 
            this.lblODCTax.AutoSize = true;
            this.lblODCTax.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblODCTax.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.lblODCTax.Location = new System.Drawing.Point(3, 12);
            this.lblODCTax.Name = "lblODCTax";
            this.lblODCTax.Size = new System.Drawing.Size(64, 20);
            this.lblODCTax.TabIndex = 2;
            this.lblODCTax.Text = "ODC Tax";
            this.lblODCTax.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblODCTax.UseCompatibleTextRendering = true;
            // 
            // btnChangeTax
            // 
            this.btnChangeTax.Location = new System.Drawing.Point(160, 60);
            this.btnChangeTax.Name = "btnChangeTax";
            this.btnChangeTax.Size = new System.Drawing.Size(130, 60);
            this.btnChangeTax.TabIndex = 1;
            this.btnChangeTax.Text = "Change Tax";
            this.btnChangeTax.UseVisualStyleBackColor = true;
            this.btnChangeTax.Click += new System.EventHandler(this.btnChangeTax_Click);
            // 
            // btnDefaulttax
            // 
            this.btnDefaulttax.Location = new System.Drawing.Point(20, 60);
            this.btnDefaulttax.Name = "btnDefaulttax";
            this.btnDefaulttax.Size = new System.Drawing.Size(130, 60);
            this.btnDefaulttax.TabIndex = 0;
            this.btnDefaulttax.Text = "Default Tax";
            this.btnDefaulttax.UseVisualStyleBackColor = true;
            this.btnDefaulttax.Click += new System.EventHandler(this.btnDefaulttax_Click);
            // 
            // frmTaxPrompt
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Teal;
            this.ClientSize = new System.Drawing.Size(341, 173);
            this.ControlBox = false;
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MinimizeBox = false;
            this.Name = "frmTaxPrompt";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ODC Tax";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.frmTaxPrompt_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnChangeTax;
        private System.Windows.Forms.Button btnDefaulttax;
        private System.Windows.Forms.Label lblODCTax;
    }
}