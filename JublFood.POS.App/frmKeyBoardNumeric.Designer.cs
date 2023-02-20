namespace JublFood.POS.App
{
    partial class frmKeyBoardNumeric
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
            this.pnl_Login = new System.Windows.Forms.Panel();
            this.btn_Enter = new System.Windows.Forms.Button();
            this.uC_KeyBoardNumeric = new JublFood.POS.App.UC_KeyBoardNumeric();
            this.txt_Input = new System.Windows.Forms.TextBox();
            this.pnl_Login.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnl_Login
            // 
            this.pnl_Login.BackColor = System.Drawing.Color.Teal;
            this.pnl_Login.Controls.Add(this.btn_Enter);
            this.pnl_Login.Controls.Add(this.uC_KeyBoardNumeric);
            this.pnl_Login.Controls.Add(this.txt_Input);
            this.pnl_Login.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl_Login.Location = new System.Drawing.Point(0, 0);
            this.pnl_Login.Name = "pnl_Login";
            this.pnl_Login.Size = new System.Drawing.Size(262, 360);
            this.pnl_Login.TabIndex = 1;
            // 
            // btn_Enter
            // 
            this.btn_Enter.BackColor = System.Drawing.Color.PeachPuff;
            this.btn_Enter.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Enter.Location = new System.Drawing.Point(167, 264);
            this.btn_Enter.Name = "btn_Enter";
            this.btn_Enter.Size = new System.Drawing.Size(77, 62);
            this.btn_Enter.TabIndex = 26;
            this.btn_Enter.Text = "Enter";
            this.btn_Enter.UseVisualStyleBackColor = false;
            this.btn_Enter.Click += new System.EventHandler(this.btn_Enter_Click);
            // 
            // uC_KeyBoardNumeric
            // 
            this.uC_KeyBoardNumeric.Location = new System.Drawing.Point(10, 74);
            this.uC_KeyBoardNumeric.Name = "uC_KeyBoardNumeric";
            this.uC_KeyBoardNumeric.Size = new System.Drawing.Size(233, 279);
            this.uC_KeyBoardNumeric.TabIndex = 25;
            this.uC_KeyBoardNumeric.txtUserID = null;
            // 
            // txt_Input
            // 
            this.txt_Input.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_Input.Location = new System.Drawing.Point(12, 34);
            this.txt_Input.MaxLength = 10;
            this.txt_Input.Name = "txt_Input";
            this.txt_Input.Size = new System.Drawing.Size(231, 29);
            this.txt_Input.TabIndex = 0;
            this.txt_Input.Enter += new System.EventHandler(this.txt_Input_Enter);
            this.txt_Input.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_Input_KeyDown);
            this.txt_Input.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_Input_KeyPress);
            // 
            // frmKeyBoardNumeric
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(262, 360);
            this.ControlBox = false;
            this.Controls.Add(this.pnl_Login);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "frmKeyBoardNumeric";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmKeyBoardNumeric";
            this.pnl_Login.ResumeLayout(false);
            this.pnl_Login.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnl_Login;
        private System.Windows.Forms.Button btn_Enter;
        private UC_KeyBoardNumeric uC_KeyBoardNumeric;
        public System.Windows.Forms.TextBox txt_Input;
    }
}