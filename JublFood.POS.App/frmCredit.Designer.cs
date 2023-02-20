namespace JublFood.POS.App
{
    partial class frmCredit
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
            this.pnl_Credit = new System.Windows.Forms.Panel();
            this.cmdOK = new System.Windows.Forms.Button();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.ChkAccept = new System.Windows.Forms.CheckBox();
            this.Text1 = new System.Windows.Forms.TextBox();
            this.ComComCode = new System.Windows.Forms.ComboBox();
            this.TDBNAddlCredit = new System.Windows.Forms.TextBox();
            this.TDBNCreditToApply = new System.Windows.Forms.TextBox();
            this.TxtComName = new System.Windows.Forms.Label();
            this.ltxtCreditToApply = new System.Windows.Forms.Label();
            this.lblHeading = new System.Windows.Forms.Label();
            this.pnl_Credit.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnl_Credit
            // 
            this.pnl_Credit.BackColor = System.Drawing.Color.Teal;
            this.pnl_Credit.Controls.Add(this.cmdOK);
            this.pnl_Credit.Controls.Add(this.cmdCancel);
            this.pnl_Credit.Controls.Add(this.ChkAccept);
            this.pnl_Credit.Controls.Add(this.Text1);
            this.pnl_Credit.Controls.Add(this.ComComCode);
            this.pnl_Credit.Controls.Add(this.TDBNAddlCredit);
            this.pnl_Credit.Controls.Add(this.TDBNCreditToApply);
            this.pnl_Credit.Controls.Add(this.TxtComName);
            this.pnl_Credit.Controls.Add(this.ltxtCreditToApply);
            this.pnl_Credit.Location = new System.Drawing.Point(0, 27);
            this.pnl_Credit.Name = "pnl_Credit";
            this.pnl_Credit.Size = new System.Drawing.Size(423, 335);
            this.pnl_Credit.TabIndex = 0;
            // 
            // cmdOK
            // 
            this.cmdOK.BackColor = System.Drawing.Color.PeachPuff;
            this.cmdOK.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.cmdOK.Image = global::JublFood.POS.App.Properties.Resources._171;
            this.cmdOK.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.cmdOK.Location = new System.Drawing.Point(300, 271);
            this.cmdOK.Name = "cmdOK";
            this.cmdOK.Size = new System.Drawing.Size(68, 55);
            this.cmdOK.TabIndex = 28;
            this.cmdOK.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.cmdOK.UseVisualStyleBackColor = false;
            // 
            // cmdCancel
            // 
            this.cmdCancel.BackColor = System.Drawing.Color.PeachPuff;
            this.cmdCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.cmdCancel.Image = global::JublFood.POS.App.Properties.Resources._92;
            this.cmdCancel.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.cmdCancel.Location = new System.Drawing.Point(233, 271);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(68, 55);
            this.cmdCancel.TabIndex = 28;
            this.cmdCancel.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.cmdCancel.UseVisualStyleBackColor = false;
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // ChkAccept
            // 
            this.ChkAccept.AutoSize = true;
            this.ChkAccept.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkAccept.ForeColor = System.Drawing.Color.White;
            this.ChkAccept.Location = new System.Drawing.Point(13, 262);
            this.ChkAccept.Name = "ChkAccept";
            this.ChkAccept.Size = new System.Drawing.Size(87, 24);
            this.ChkAccept.TabIndex = 5;
            this.ChkAccept.Text = "I Agree";
            this.ChkAccept.UseVisualStyleBackColor = true;
            // 
            // Text1
            // 
            this.Text1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Text1.Location = new System.Drawing.Point(13, 137);
            this.Text1.Multiline = true;
            this.Text1.Name = "Text1";
            this.Text1.Size = new System.Drawing.Size(390, 117);
            this.Text1.TabIndex = 4;
            this.Text1.Text = "I hereby confirm that I have taken all the \r\nnecessary approval from corporate sa" +
    "le for \r\nprocessing this credit order.";
            // 
            // ComComCode
            // 
            this.ComComCode.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ComComCode.FormattingEnabled = true;
            this.ComComCode.Location = new System.Drawing.Point(158, 84);
            this.ComComCode.Name = "ComComCode";
            this.ComComCode.Size = new System.Drawing.Size(236, 28);
            this.ComComCode.TabIndex = 3;
            // 
            // TDBNAddlCredit
            // 
            this.TDBNAddlCredit.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TDBNAddlCredit.Location = new System.Drawing.Point(296, 45);
            this.TDBNAddlCredit.Name = "TDBNAddlCredit";
            this.TDBNAddlCredit.Size = new System.Drawing.Size(124, 26);
            this.TDBNAddlCredit.TabIndex = 2;
            // 
            // TDBNCreditToApply
            // 
            this.TDBNCreditToApply.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TDBNCreditToApply.Location = new System.Drawing.Point(158, 45);
            this.TDBNCreditToApply.Name = "TDBNCreditToApply";
            this.TDBNCreditToApply.Size = new System.Drawing.Size(136, 26);
            this.TDBNCreditToApply.TabIndex = 2;
            // 
            // TxtComName
            // 
            this.TxtComName.AutoSize = true;
            this.TxtComName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtComName.ForeColor = System.Drawing.Color.White;
            this.TxtComName.Location = new System.Drawing.Point(9, 82);
            this.TxtComName.Name = "TxtComName";
            this.TxtComName.Size = new System.Drawing.Size(134, 20);
            this.TxtComName.TabIndex = 1;
            this.TxtComName.Text = "Company Name";
            // 
            // ltxtCreditToApply
            // 
            this.ltxtCreditToApply.AutoSize = true;
            this.ltxtCreditToApply.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ltxtCreditToApply.ForeColor = System.Drawing.Color.White;
            this.ltxtCreditToApply.Location = new System.Drawing.Point(9, 45);
            this.ltxtCreditToApply.Name = "ltxtCreditToApply";
            this.ltxtCreditToApply.Size = new System.Drawing.Size(127, 20);
            this.ltxtCreditToApply.TabIndex = 1;
            this.ltxtCreditToApply.Text = "Credit to Apply";
            // 
            // lblHeading
            // 
            this.lblHeading.BackColor = System.Drawing.Color.PeachPuff;
            this.lblHeading.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeading.Location = new System.Drawing.Point(0, 2);
            this.lblHeading.Name = "lblHeading";
            this.lblHeading.Size = new System.Drawing.Size(423, 23);
            this.lblHeading.TabIndex = 0;
            this.lblHeading.Text = "Credit Sale";
            this.lblHeading.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // frmCredit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(423, 364);
            this.Controls.Add(this.pnl_Credit);
            this.Controls.Add(this.lblHeading);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmCredit";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmCredit";
            this.pnl_Credit.ResumeLayout(false);
            this.pnl_Credit.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnl_Credit;
        private System.Windows.Forms.Label lblHeading;
        private System.Windows.Forms.TextBox TDBNCreditToApply;
        private System.Windows.Forms.Label ltxtCreditToApply;
        private System.Windows.Forms.CheckBox ChkAccept;
        private System.Windows.Forms.TextBox Text1;
        private System.Windows.Forms.ComboBox ComComCode;
        private System.Windows.Forms.Label TxtComName;
        private System.Windows.Forms.Button cmdOK;
        private System.Windows.Forms.Button cmdCancel;
        private System.Windows.Forms.TextBox TDBNAddlCredit;
    }
}