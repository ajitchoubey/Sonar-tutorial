namespace JublFood.POS.App
{
    partial class frmDebit
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
            this.pnl_Debit = new System.Windows.Forms.Panel();
            this.cmdOK = new System.Windows.Forms.Button();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.txtOrderAmount = new System.Windows.Forms.TextBox();
            this.lblOrderAmount = new System.Windows.Forms.Label();
            this.txtAdvanceAmount = new System.Windows.Forms.TextBox();
            this.lblAdvanceAmount = new System.Windows.Forms.Label();
            this.lblHeading = new System.Windows.Forms.Label();
            this.pnl_Debit.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnl_Debit
            // 
            this.pnl_Debit.BackColor = System.Drawing.Color.Teal;
            this.pnl_Debit.Controls.Add(this.cmdOK);
            this.pnl_Debit.Controls.Add(this.cmdCancel);
            this.pnl_Debit.Controls.Add(this.txtOrderAmount);
            this.pnl_Debit.Controls.Add(this.lblOrderAmount);
            this.pnl_Debit.Controls.Add(this.txtAdvanceAmount);
            this.pnl_Debit.Controls.Add(this.lblAdvanceAmount);
            this.pnl_Debit.Location = new System.Drawing.Point(2, 26);
            this.pnl_Debit.Name = "pnl_Debit";
            this.pnl_Debit.Size = new System.Drawing.Size(391, 258);
            this.pnl_Debit.TabIndex = 0;
            // 
            // cmdOK
            // 
            this.cmdOK.BackColor = System.Drawing.Color.PeachPuff;
            this.cmdOK.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.cmdOK.Image = global::JublFood.POS.App.Properties.Resources._171;
            this.cmdOK.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.cmdOK.Location = new System.Drawing.Point(287, 152);
            this.cmdOK.Name = "cmdOK";
            this.cmdOK.Size = new System.Drawing.Size(68, 55);
            this.cmdOK.TabIndex = 29;
            this.cmdOK.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.cmdOK.UseVisualStyleBackColor = false;
            // 
            // cmdCancel
            // 
            this.cmdCancel.BackColor = System.Drawing.Color.PeachPuff;
            this.cmdCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.cmdCancel.Image = global::JublFood.POS.App.Properties.Resources._92;
            this.cmdCancel.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.cmdCancel.Location = new System.Drawing.Point(220, 152);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(68, 55);
            this.cmdCancel.TabIndex = 30;
            this.cmdCancel.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.cmdCancel.UseVisualStyleBackColor = false;
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // txtOrderAmount
            // 
            this.txtOrderAmount.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtOrderAmount.Location = new System.Drawing.Point(179, 105);
            this.txtOrderAmount.Name = "txtOrderAmount";
            this.txtOrderAmount.Size = new System.Drawing.Size(183, 26);
            this.txtOrderAmount.TabIndex = 4;
            // 
            // lblOrderAmount
            // 
            this.lblOrderAmount.AutoSize = true;
            this.lblOrderAmount.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOrderAmount.ForeColor = System.Drawing.Color.White;
            this.lblOrderAmount.Location = new System.Drawing.Point(30, 105);
            this.lblOrderAmount.Name = "lblOrderAmount";
            this.lblOrderAmount.Size = new System.Drawing.Size(121, 20);
            this.lblOrderAmount.TabIndex = 3;
            this.lblOrderAmount.Text = "Order Amount";
            // 
            // txtAdvanceAmount
            // 
            this.txtAdvanceAmount.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAdvanceAmount.Location = new System.Drawing.Point(179, 50);
            this.txtAdvanceAmount.Name = "txtAdvanceAmount";
            this.txtAdvanceAmount.Size = new System.Drawing.Size(183, 26);
            this.txtAdvanceAmount.TabIndex = 4;
            // 
            // lblAdvanceAmount
            // 
            this.lblAdvanceAmount.AutoSize = true;
            this.lblAdvanceAmount.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAdvanceAmount.ForeColor = System.Drawing.Color.White;
            this.lblAdvanceAmount.Location = new System.Drawing.Point(30, 50);
            this.lblAdvanceAmount.Name = "lblAdvanceAmount";
            this.lblAdvanceAmount.Size = new System.Drawing.Size(145, 20);
            this.lblAdvanceAmount.TabIndex = 3;
            this.lblAdvanceAmount.Text = "Advance Amount";
            // 
            // lblHeading
            // 
            this.lblHeading.BackColor = System.Drawing.Color.PeachPuff;
            this.lblHeading.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeading.Location = new System.Drawing.Point(3, 1);
            this.lblHeading.Name = "lblHeading";
            this.lblHeading.Size = new System.Drawing.Size(388, 23);
            this.lblHeading.TabIndex = 5;
            this.lblHeading.Text = "Advance Sale";
            this.lblHeading.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // frmDebit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(394, 290);
            this.Controls.Add(this.pnl_Debit);
            this.Controls.Add(this.lblHeading);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmDebit";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmDebit";
            this.pnl_Debit.ResumeLayout(false);
            this.pnl_Debit.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnl_Debit;
        private System.Windows.Forms.TextBox txtAdvanceAmount;
        private System.Windows.Forms.Label lblAdvanceAmount;
        private System.Windows.Forms.Label lblHeading;
        private System.Windows.Forms.TextBox txtOrderAmount;
        private System.Windows.Forms.Label lblOrderAmount;
        private System.Windows.Forms.Button cmdOK;
        private System.Windows.Forms.Button cmdCancel;
    }
}