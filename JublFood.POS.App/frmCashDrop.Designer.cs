namespace JublFood.POS.App
{
    partial class frmCashDrop
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
            this.pnl_CashDrop = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.flpanel_Employee = new System.Windows.Forms.FlowLayoutPanel();
            this.cmdPrevious = new System.Windows.Forms.Button();
            this.cmdNext = new System.Windows.Forms.Button();
            this.btn_Dot = new System.Windows.Forms.Button();
            this.cmdOk = new System.Windows.Forms.Button();
            this.cmdCurrencySelect = new System.Windows.Forms.Button();
            this.cmdClose = new System.Windows.Forms.Button();
            this.cmdPlusMinus = new System.Windows.Forms.Button();
            this.tdbnAmount = new System.Windows.Forms.TextBox();
            this.uc_KeyBoardNumeric = new JublFood.POS.App.UC_KeyBoardNumeric();
            this.ltxtAmount = new System.Windows.Forms.Label();
            this.ltxtCurrencyCode = new System.Windows.Forms.Label();
            this.ltxtCurrency = new System.Windows.Forms.Label();
            this.pnl_CashDrop.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnl_CashDrop
            // 
            this.pnl_CashDrop.Controls.Add(this.panel1);
            this.pnl_CashDrop.Controls.Add(this.btn_Dot);
            this.pnl_CashDrop.Controls.Add(this.cmdOk);
            this.pnl_CashDrop.Controls.Add(this.cmdCurrencySelect);
            this.pnl_CashDrop.Controls.Add(this.cmdClose);
            this.pnl_CashDrop.Controls.Add(this.cmdPlusMinus);
            this.pnl_CashDrop.Controls.Add(this.tdbnAmount);
            this.pnl_CashDrop.Controls.Add(this.uc_KeyBoardNumeric);
            this.pnl_CashDrop.Controls.Add(this.ltxtAmount);
            this.pnl_CashDrop.Controls.Add(this.ltxtCurrencyCode);
            this.pnl_CashDrop.Controls.Add(this.ltxtCurrency);
            this.pnl_CashDrop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl_CashDrop.Location = new System.Drawing.Point(0, 0);
            this.pnl_CashDrop.Name = "pnl_CashDrop";
            this.pnl_CashDrop.Size = new System.Drawing.Size(330, 414);
            this.pnl_CashDrop.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.flpanel_Employee);
            this.panel1.Controls.Add(this.cmdPrevious);
            this.panel1.Controls.Add(this.cmdNext);
            this.panel1.Location = new System.Drawing.Point(1, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(392, 62);
            this.panel1.TabIndex = 23;
            // 
            // flpanel_Employee
            // 
            this.flpanel_Employee.Location = new System.Drawing.Point(73, 1);
            this.flpanel_Employee.Margin = new System.Windows.Forms.Padding(5);
            this.flpanel_Employee.Name = "flpanel_Employee";
            this.flpanel_Employee.Size = new System.Drawing.Size(178, 59);
            this.flpanel_Employee.TabIndex = 1;
            // 
            // cmdPrevious
            // 
            this.cmdPrevious.BackColor = System.Drawing.Color.PeachPuff;
            this.cmdPrevious.Enabled = false;
            this.cmdPrevious.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.cmdPrevious.Image = global::JublFood.POS.App.Properties.Resources._32;
            this.cmdPrevious.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.cmdPrevious.Location = new System.Drawing.Point(2, 1);
            this.cmdPrevious.Name = "cmdPrevious";
            this.cmdPrevious.Size = new System.Drawing.Size(68, 55);
            this.cmdPrevious.TabIndex = 0;
            this.cmdPrevious.Text = "Previous";
            this.cmdPrevious.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.cmdPrevious.UseVisualStyleBackColor = false;
            this.cmdPrevious.Click += new System.EventHandler(this.cmdPrevious_Click);
            // 
            // cmdNext
            // 
            this.cmdNext.BackColor = System.Drawing.Color.PeachPuff;
            this.cmdNext.Enabled = false;
            this.cmdNext.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.cmdNext.Image = global::JublFood.POS.App.Properties.Resources._33;
            this.cmdNext.Location = new System.Drawing.Point(255, 3);
            this.cmdNext.Name = "cmdNext";
            this.cmdNext.Size = new System.Drawing.Size(68, 55);
            this.cmdNext.TabIndex = 0;
            this.cmdNext.Text = "Next";
            this.cmdNext.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.cmdNext.UseVisualStyleBackColor = false;
            this.cmdNext.Click += new System.EventHandler(this.cmdNext_Click);
            // 
            // btn_Dot
            // 
            this.btn_Dot.BackColor = System.Drawing.SystemColors.Control;
            this.btn_Dot.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Dot.Location = new System.Drawing.Point(160, 330);
            this.btn_Dot.Name = "btn_Dot";
            this.btn_Dot.Size = new System.Drawing.Size(77, 62);
            this.btn_Dot.TabIndex = 22;
            this.btn_Dot.Text = ".";
            this.btn_Dot.UseVisualStyleBackColor = false;
            this.btn_Dot.Click += new System.EventHandler(this.btn_Dot_Click);
            // 
            // cmdOk
            // 
            this.cmdOk.BackColor = System.Drawing.Color.PeachPuff;
            this.cmdOk.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdOk.Image = global::JublFood.POS.App.Properties.Resources._171;
            this.cmdOk.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.cmdOk.Location = new System.Drawing.Point(257, 352);
            this.cmdOk.Name = "cmdOk";
            this.cmdOk.Size = new System.Drawing.Size(68, 55);
            this.cmdOk.TabIndex = 8;
            this.cmdOk.Text = "Ok";
            this.cmdOk.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.cmdOk.UseVisualStyleBackColor = false;
            this.cmdOk.Click += new System.EventHandler(this.cmdOk_Click);
            // 
            // cmdCurrencySelect
            // 
            this.cmdCurrencySelect.BackColor = System.Drawing.Color.PeachPuff;
            this.cmdCurrencySelect.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdCurrencySelect.Image = global::JublFood.POS.App.Properties.Resources._31;
            this.cmdCurrencySelect.Location = new System.Drawing.Point(257, 126);
            this.cmdCurrencySelect.Name = "cmdCurrencySelect";
            this.cmdCurrencySelect.Size = new System.Drawing.Size(68, 55);
            this.cmdCurrencySelect.TabIndex = 6;
            this.cmdCurrencySelect.UseVisualStyleBackColor = false;
            // 
            // cmdClose
            // 
            this.cmdClose.BackColor = System.Drawing.Color.PeachPuff;
            this.cmdClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdClose.Image = global::JublFood.POS.App.Properties.Resources._35;
            this.cmdClose.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.cmdClose.Location = new System.Drawing.Point(257, 295);
            this.cmdClose.Name = "cmdClose";
            this.cmdClose.Size = new System.Drawing.Size(68, 55);
            this.cmdClose.TabIndex = 7;
            this.cmdClose.Text = "Close";
            this.cmdClose.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.cmdClose.UseVisualStyleBackColor = false;
            this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
            // 
            // cmdPlusMinus
            // 
            this.cmdPlusMinus.BackColor = System.Drawing.Color.PeachPuff;
            this.cmdPlusMinus.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdPlusMinus.Image = global::JublFood.POS.App.Properties.Resources._63;
            this.cmdPlusMinus.Location = new System.Drawing.Point(257, 69);
            this.cmdPlusMinus.Name = "cmdPlusMinus";
            this.cmdPlusMinus.Size = new System.Drawing.Size(68, 55);
            this.cmdPlusMinus.TabIndex = 5;
            this.cmdPlusMinus.Tag = "-";
            this.cmdPlusMinus.UseVisualStyleBackColor = false;
            this.cmdPlusMinus.Click += new System.EventHandler(this.cmdPlusMinus_Click);
            // 
            // tdbnAmount
            // 
            this.tdbnAmount.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tdbnAmount.Location = new System.Drawing.Point(144, 75);
            this.tdbnAmount.MaxLength = 8;
            this.tdbnAmount.Name = "tdbnAmount";
            this.tdbnAmount.Size = new System.Drawing.Size(102, 29);
            this.tdbnAmount.TabIndex = 2;
            this.tdbnAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.tdbnAmount.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tdbnAmount_KeyPress);
            // 
            // uc_KeyBoardNumeric
            // 
            this.uc_KeyBoardNumeric.BackColor = System.Drawing.Color.Teal;
            this.uc_KeyBoardNumeric.Location = new System.Drawing.Point(4, 140);
            this.uc_KeyBoardNumeric.Name = "uc_KeyBoardNumeric";
            this.uc_KeyBoardNumeric.Size = new System.Drawing.Size(242, 269);
            this.uc_KeyBoardNumeric.TabIndex = 4;
            this.uc_KeyBoardNumeric.txtUserID = null;
            // 
            // ltxtAmount
            // 
            this.ltxtAmount.AutoSize = true;
            this.ltxtAmount.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ltxtAmount.Location = new System.Drawing.Point(43, 75);
            this.ltxtAmount.Name = "ltxtAmount";
            this.ltxtAmount.Size = new System.Drawing.Size(59, 16);
            this.ltxtAmount.TabIndex = 1;
            this.ltxtAmount.Text = "Amount";
            // 
            // ltxtCurrencyCode
            // 
            this.ltxtCurrencyCode.AutoSize = true;
            this.ltxtCurrencyCode.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ltxtCurrencyCode.Location = new System.Drawing.Point(141, 114);
            this.ltxtCurrencyCode.Name = "ltxtCurrencyCode";
            this.ltxtCurrencyCode.Size = new System.Drawing.Size(19, 16);
            this.ltxtCurrencyCode.TabIndex = 3;
            this.ltxtCurrencyCode.Text = "R";
            // 
            // ltxtCurrency
            // 
            this.ltxtCurrency.AutoSize = true;
            this.ltxtCurrency.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ltxtCurrency.Location = new System.Drawing.Point(43, 114);
            this.ltxtCurrency.Name = "ltxtCurrency";
            this.ltxtCurrency.Size = new System.Drawing.Size(69, 16);
            this.ltxtCurrency.TabIndex = 3;
            this.ltxtCurrency.Text = "Currency";
            // 
            // frmCashDrop
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(330, 414);
            this.ControlBox = false;
            this.Controls.Add(this.pnl_CashDrop);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmCashDrop";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.pnl_CashDrop.ResumeLayout(false);
            this.pnl_CashDrop.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnl_CashDrop;
        private System.Windows.Forms.Label ltxtCurrency;
        private System.Windows.Forms.Button cmdPrevious;
        private System.Windows.Forms.Button cmdNext;
        private System.Windows.Forms.Button cmdOk;
        private System.Windows.Forms.Button cmdCurrencySelect;
        private System.Windows.Forms.Button cmdClose;
        private System.Windows.Forms.Button cmdPlusMinus;
        private System.Windows.Forms.TextBox tdbnAmount;
        private UC_KeyBoardNumeric uc_KeyBoardNumeric;
        private System.Windows.Forms.Label ltxtAmount;
        private System.Windows.Forms.Label ltxtCurrencyCode;
        private System.Windows.Forms.Button btn_Dot;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.FlowLayoutPanel flpanel_Employee;
    }
}