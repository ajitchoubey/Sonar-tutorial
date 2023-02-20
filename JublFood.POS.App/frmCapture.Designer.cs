namespace JublFood.POS.App
{
    partial class frmCapture
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
            this.lblPhoneNumber = new System.Windows.Forms.Label();
            this.tdbmPhone_Number = new System.Windows.Forms.TextBox();
            this.tdbtxtPhone_Ext = new System.Windows.Forms.TextBox();
            this.lblExt = new System.Windows.Forms.Label();
            this.txtCustomerLookup = new System.Windows.Forms.TextBox();
            this.lblCustomerLookup = new System.Windows.Forms.Label();
            this.txtTentNumber = new System.Windows.Forms.TextBox();
            this.lblTentNumber = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.lblName = new System.Windows.Forms.Label();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnKeyboard = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblPhoneNumber
            // 
            this.lblPhoneNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPhoneNumber.ForeColor = System.Drawing.Color.Yellow;
            this.lblPhoneNumber.Location = new System.Drawing.Point(7, 19);
            this.lblPhoneNumber.Name = "lblPhoneNumber";
            this.lblPhoneNumber.Size = new System.Drawing.Size(130, 20);
            this.lblPhoneNumber.TabIndex = 0;
            this.lblPhoneNumber.Text = "Phone Number";
            this.lblPhoneNumber.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // tdbmPhone_Number
            // 
            this.tdbmPhone_Number.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tdbmPhone_Number.Location = new System.Drawing.Point(143, 18);
            this.tdbmPhone_Number.MaxLength = 10;
            this.tdbmPhone_Number.Name = "tdbmPhone_Number";
            this.tdbmPhone_Number.Size = new System.Drawing.Size(128, 22);
            this.tdbmPhone_Number.TabIndex = 0;
            this.tdbmPhone_Number.Enter += new System.EventHandler(this.tdbmPhone_Number_Enter);
            this.tdbmPhone_Number.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tdbmPhone_Number_KeyPress);
            // 
            // tdbtxtPhone_Ext
            // 
            this.tdbtxtPhone_Ext.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tdbtxtPhone_Ext.Location = new System.Drawing.Point(366, 17);
            this.tdbtxtPhone_Ext.MaxLength = 7;
            this.tdbtxtPhone_Ext.Name = "tdbtxtPhone_Ext";
            this.tdbtxtPhone_Ext.Size = new System.Drawing.Size(88, 22);
            this.tdbtxtPhone_Ext.TabIndex = 1;
            this.tdbtxtPhone_Ext.Enter += new System.EventHandler(this.tdbmPhone_Number_Enter);
            this.tdbtxtPhone_Ext.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tdbtxtPhone_Ext_KeyPress);
            // 
            // lblExt
            // 
            this.lblExt.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblExt.ForeColor = System.Drawing.Color.Yellow;
            this.lblExt.Location = new System.Drawing.Point(321, 20);
            this.lblExt.Name = "lblExt";
            this.lblExt.Size = new System.Drawing.Size(39, 20);
            this.lblExt.TabIndex = 2;
            this.lblExt.Text = "Ext.";
            this.lblExt.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // txtCustomerLookup
            // 
            this.txtCustomerLookup.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCustomerLookup.Location = new System.Drawing.Point(143, 46);
            this.txtCustomerLookup.MaxLength = 100;
            this.txtCustomerLookup.Name = "txtCustomerLookup";
            this.txtCustomerLookup.Size = new System.Drawing.Size(311, 22);
            this.txtCustomerLookup.TabIndex = 2;
            this.txtCustomerLookup.Enter += new System.EventHandler(this.tdbmPhone_Number_Enter);
            // 
            // lblCustomerLookup
            // 
            this.lblCustomerLookup.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCustomerLookup.ForeColor = System.Drawing.Color.Yellow;
            this.lblCustomerLookup.Location = new System.Drawing.Point(7, 47);
            this.lblCustomerLookup.Name = "lblCustomerLookup";
            this.lblCustomerLookup.Size = new System.Drawing.Size(130, 20);
            this.lblCustomerLookup.TabIndex = 4;
            this.lblCustomerLookup.Text = "Customer Lookup";
            this.lblCustomerLookup.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // txtTentNumber
            // 
            this.txtTentNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTentNumber.Location = new System.Drawing.Point(141, 102);
            this.txtTentNumber.MaxLength = 50;
            this.txtTentNumber.MinimumSize = new System.Drawing.Size(280, 25);
            this.txtTentNumber.Name = "txtTentNumber";
            this.txtTentNumber.Size = new System.Drawing.Size(313, 22);
            this.txtTentNumber.TabIndex = 4;
            this.txtTentNumber.Enter += new System.EventHandler(this.tdbmPhone_Number_Enter);
            // 
            // lblTentNumber
            // 
            this.lblTentNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTentNumber.ForeColor = System.Drawing.Color.Yellow;
            this.lblTentNumber.Location = new System.Drawing.Point(7, 107);
            this.lblTentNumber.Name = "lblTentNumber";
            this.lblTentNumber.Size = new System.Drawing.Size(130, 20);
            this.lblTentNumber.TabIndex = 6;
            this.lblTentNumber.Text = "Tent Number";
            this.lblTentNumber.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // txtName
            // 
            this.txtName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtName.Location = new System.Drawing.Point(142, 74);
            this.txtName.MaxLength = 50;
            this.txtName.MinimumSize = new System.Drawing.Size(280, 25);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(312, 22);
            this.txtName.TabIndex = 3;
            this.txtName.Enter += new System.EventHandler(this.tdbmPhone_Number_Enter);
            // 
            // lblName
            // 
            this.lblName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblName.ForeColor = System.Drawing.Color.Yellow;
            this.lblName.Location = new System.Drawing.Point(7, 79);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(130, 20);
            this.lblName.TabIndex = 8;
            this.lblName.Text = "Name";
            this.lblName.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // btnOK
            // 
            this.btnOK.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOK.Image = global::JublFood.POS.App.Properties.Resources._171;
            this.btnOK.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnOK.Location = new System.Drawing.Point(374, 144);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(79, 63);
            this.btnOK.TabIndex = 5;
            this.btnOK.Text = "OK";
            this.btnOK.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnKeyboard
            // 
            this.btnKeyboard.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnKeyboard.Image = global::JublFood.POS.App.Properties.Resources._42;
            this.btnKeyboard.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnKeyboard.Location = new System.Drawing.Point(289, 144);
            this.btnKeyboard.Name = "btnKeyboard";
            this.btnKeyboard.Size = new System.Drawing.Size(79, 63);
            this.btnKeyboard.TabIndex = 6;
            this.btnKeyboard.Text = "Keyboard";
            this.btnKeyboard.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnKeyboard.UseVisualStyleBackColor = true;
            this.btnKeyboard.Click += new System.EventHandler(this.btnKeyboard_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Image = global::JublFood.POS.App.Properties.Resources._92;
            this.btnCancel.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnCancel.Location = new System.Drawing.Point(205, 144);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(78, 63);
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // frmCapture
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Teal;
            this.ClientSize = new System.Drawing.Size(466, 219);
            this.ControlBox = false;
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnKeyboard);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.txtTentNumber);
            this.Controls.Add(this.lblTentNumber);
            this.Controls.Add(this.txtCustomerLookup);
            this.Controls.Add(this.lblCustomerLookup);
            this.Controls.Add(this.tdbtxtPhone_Ext);
            this.Controls.Add(this.lblExt);
            this.Controls.Add(this.tdbmPhone_Number);
            this.Controls.Add(this.lblPhoneNumber);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.Name = "frmCapture";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Capture Information";
            this.Activated += new System.EventHandler(this.frmCapture_Activated);
            this.Load += new System.EventHandler(this.frmCapture_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.frmCapture_KeyPress);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        public System.Windows.Forms.Label lblPhoneNumber;
        public System.Windows.Forms.TextBox tdbmPhone_Number;
        public System.Windows.Forms.TextBox tdbtxtPhone_Ext;
        public System.Windows.Forms.Label lblExt;
        public System.Windows.Forms.TextBox txtCustomerLookup;
        public System.Windows.Forms.Label lblCustomerLookup;
        public System.Windows.Forms.TextBox txtTentNumber;
        public System.Windows.Forms.Label lblTentNumber;
        public System.Windows.Forms.TextBox txtName;
        public System.Windows.Forms.Label lblName;
        public System.Windows.Forms.Button btnCancel;
        public System.Windows.Forms.Button btnKeyboard;
        public System.Windows.Forms.Button btnOK;
    }
}