namespace JublFood.POS.App
{
    partial class frmPasswordChange
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
            this.pnl_passwordChange = new System.Windows.Forms.Panel();
            this.btnClose = new System.Windows.Forms.Button();
            this.btn_enter = new System.Windows.Forms.Button();
            this.grpBox_Password = new System.Windows.Forms.GroupBox();
            this.lbltextmessgae = new System.Windows.Forms.Label();
            this.lalblEmployeenamebel1 = new System.Windows.Forms.Label();
            this.lblUserId = new System.Windows.Forms.Label();
            this.txt_ConfirmPassword = new System.Windows.Forms.TextBox();
            this.txt_NewPassword = new System.Windows.Forms.TextBox();
            this.txt_CurrentPassword = new System.Windows.Forms.TextBox();
            this.lbl_ConfirmPassword = new System.Windows.Forms.Label();
            this.lbl_NewPassword = new System.Windows.Forms.Label();
            this.lbl_CurrentPassword = new System.Windows.Forms.Label();
            this.UC_KeyBoard = new JublFood.POS.App.UC_KeyBoard();
            this.pnl_passwordChange.SuspendLayout();
            this.grpBox_Password.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnl_passwordChange
            // 
            this.pnl_passwordChange.BackColor = System.Drawing.Color.Teal;
            this.pnl_passwordChange.Controls.Add(this.btnClose);
            this.pnl_passwordChange.Controls.Add(this.btn_enter);
            this.pnl_passwordChange.Controls.Add(this.UC_KeyBoard);
            this.pnl_passwordChange.Controls.Add(this.grpBox_Password);
            this.pnl_passwordChange.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl_passwordChange.Location = new System.Drawing.Point(0, 0);
            this.pnl_passwordChange.Name = "pnl_passwordChange";
            this.pnl_passwordChange.Size = new System.Drawing.Size(785, 519);
            this.pnl_passwordChange.TabIndex = 0;
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.PeachPuff;
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Image = global::JublFood.POS.App.Properties.Resources._97;
            this.btnClose.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnClose.Location = new System.Drawing.Point(554, 443);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 60);
            this.btnClose.TabIndex = 29;
            this.btnClose.Text = "Close";
            this.btnClose.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Visible = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btn_enter
            // 
            this.btn_enter.BackColor = System.Drawing.Color.PeachPuff;
            this.btn_enter.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_enter.Image = global::JublFood.POS.App.Properties.Resources._35;
            this.btn_enter.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btn_enter.Location = new System.Drawing.Point(257, 443);
            this.btn_enter.Name = "btn_enter";
            this.btn_enter.Size = new System.Drawing.Size(75, 60);
            this.btn_enter.TabIndex = 28;
            this.btn_enter.Text = "Enter";
            this.btn_enter.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btn_enter.UseVisualStyleBackColor = false;
            this.btn_enter.Click += new System.EventHandler(this.btn_enter_Click);
            // 
            // grpBox_Password
            // 
            this.grpBox_Password.Controls.Add(this.lbltextmessgae);
            this.grpBox_Password.Controls.Add(this.lalblEmployeenamebel1);
            this.grpBox_Password.Controls.Add(this.lblUserId);
            this.grpBox_Password.Controls.Add(this.txt_ConfirmPassword);
            this.grpBox_Password.Controls.Add(this.txt_NewPassword);
            this.grpBox_Password.Controls.Add(this.txt_CurrentPassword);
            this.grpBox_Password.Controls.Add(this.lbl_ConfirmPassword);
            this.grpBox_Password.Controls.Add(this.lbl_NewPassword);
            this.grpBox_Password.Controls.Add(this.lbl_CurrentPassword);
            this.grpBox_Password.Location = new System.Drawing.Point(6, 5);
            this.grpBox_Password.Name = "grpBox_Password";
            this.grpBox_Password.Size = new System.Drawing.Size(773, 121);
            this.grpBox_Password.TabIndex = 0;
            this.grpBox_Password.TabStop = false;
            // 
            // lbltextmessgae
            // 
            this.lbltextmessgae.AutoSize = true;
            this.lbltextmessgae.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbltextmessgae.ForeColor = System.Drawing.Color.Yellow;
            this.lbltextmessgae.Location = new System.Drawing.Point(395, 82);
            this.lbltextmessgae.Name = "lbltextmessgae";
            this.lbltextmessgae.Size = new System.Drawing.Size(372, 30);
            this.lbltextmessgae.TabIndex = 3;
            this.lbltextmessgae.Text = "*Password length should be atleast 4 characters, Password should \r\nnot be repeate" +
    "d characters, Password should not be sequential";
            this.lbltextmessgae.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lalblEmployeenamebel1
            // 
            this.lalblEmployeenamebel1.AutoSize = true;
            this.lalblEmployeenamebel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lalblEmployeenamebel1.ForeColor = System.Drawing.Color.Yellow;
            this.lalblEmployeenamebel1.Location = new System.Drawing.Point(400, 42);
            this.lalblEmployeenamebel1.Name = "lalblEmployeenamebel1";
            this.lalblEmployeenamebel1.Size = new System.Drawing.Size(163, 24);
            this.lalblEmployeenamebel1.TabIndex = 2;
            this.lalblEmployeenamebel1.Text = "lblEmployeename";
            this.lalblEmployeenamebel1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblUserId
            // 
            this.lblUserId.AutoSize = true;
            this.lblUserId.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUserId.ForeColor = System.Drawing.Color.Yellow;
            this.lblUserId.Location = new System.Drawing.Point(400, 18);
            this.lblUserId.Name = "lblUserId";
            this.lblUserId.Size = new System.Drawing.Size(83, 24);
            this.lblUserId.TabIndex = 2;
            this.lblUserId.Text = "lblUserId";
            this.lblUserId.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txt_ConfirmPassword
            // 
            this.txt_ConfirmPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_ConfirmPassword.Location = new System.Drawing.Point(170, 86);
            this.txt_ConfirmPassword.MaxLength = 100;
            this.txt_ConfirmPassword.Name = "txt_ConfirmPassword";
            this.txt_ConfirmPassword.PasswordChar = '*';
            this.txt_ConfirmPassword.Size = new System.Drawing.Size(181, 29);
            this.txt_ConfirmPassword.TabIndex = 3;
            this.txt_ConfirmPassword.Enter += new System.EventHandler(this.txt_CurrentPassword_Enter);
            this.txt_ConfirmPassword.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_ConfirmPassword_KeyPress);
            this.txt_ConfirmPassword.Leave += new System.EventHandler(this.txt_ConfirmPassword_Leave);
            // 
            // txt_NewPassword
            // 
            this.txt_NewPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_NewPassword.Location = new System.Drawing.Point(170, 50);
            this.txt_NewPassword.MaxLength = 100;
            this.txt_NewPassword.Name = "txt_NewPassword";
            this.txt_NewPassword.PasswordChar = '*';
            this.txt_NewPassword.Size = new System.Drawing.Size(181, 29);
            this.txt_NewPassword.TabIndex = 2;
            this.txt_NewPassword.Enter += new System.EventHandler(this.txt_CurrentPassword_Enter);
            this.txt_NewPassword.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_NewPassword_KeyPress);
            this.txt_NewPassword.Leave += new System.EventHandler(this.txt_NewPassword_Leave);
            // 
            // txt_CurrentPassword
            // 
            this.txt_CurrentPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_CurrentPassword.Location = new System.Drawing.Point(170, 15);
            this.txt_CurrentPassword.MaxLength = 100;
            this.txt_CurrentPassword.Name = "txt_CurrentPassword";
            this.txt_CurrentPassword.PasswordChar = '*';
            this.txt_CurrentPassword.Size = new System.Drawing.Size(181, 29);
            this.txt_CurrentPassword.TabIndex = 1;
            this.txt_CurrentPassword.Enter += new System.EventHandler(this.txt_CurrentPassword_Enter);
            this.txt_CurrentPassword.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_CurrentPassword_KeyPress);
            this.txt_CurrentPassword.Leave += new System.EventHandler(this.txt_CurrentPassword_Leave);
            // 
            // lbl_ConfirmPassword
            // 
            this.lbl_ConfirmPassword.AutoSize = true;
            this.lbl_ConfirmPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_ConfirmPassword.ForeColor = System.Drawing.Color.Yellow;
            this.lbl_ConfirmPassword.Location = new System.Drawing.Point(5, 88);
            this.lbl_ConfirmPassword.Name = "lbl_ConfirmPassword";
            this.lbl_ConfirmPassword.Size = new System.Drawing.Size(162, 24);
            this.lbl_ConfirmPassword.TabIndex = 0;
            this.lbl_ConfirmPassword.Text = "Confirm Password";
            // 
            // lbl_NewPassword
            // 
            this.lbl_NewPassword.AutoSize = true;
            this.lbl_NewPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_NewPassword.ForeColor = System.Drawing.Color.Yellow;
            this.lbl_NewPassword.Location = new System.Drawing.Point(5, 52);
            this.lbl_NewPassword.Name = "lbl_NewPassword";
            this.lbl_NewPassword.Size = new System.Drawing.Size(136, 24);
            this.lbl_NewPassword.TabIndex = 0;
            this.lbl_NewPassword.Text = "New Password";
            // 
            // lbl_CurrentPassword
            // 
            this.lbl_CurrentPassword.AutoSize = true;
            this.lbl_CurrentPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_CurrentPassword.ForeColor = System.Drawing.Color.Yellow;
            this.lbl_CurrentPassword.Location = new System.Drawing.Point(5, 17);
            this.lbl_CurrentPassword.Name = "lbl_CurrentPassword";
            this.lbl_CurrentPassword.Size = new System.Drawing.Size(159, 24);
            this.lbl_CurrentPassword.TabIndex = 0;
            this.lbl_CurrentPassword.Text = "Current Password";
            // 
            // UC_KeyBoard
            // 
            this.UC_KeyBoard.Location = new System.Drawing.Point(1, 132);
            this.UC_KeyBoard.Name = "UC_KeyBoard";
            this.UC_KeyBoard.Size = new System.Drawing.Size(783, 382);
            this.UC_KeyBoard.TabIndex = 1;
            this.UC_KeyBoard.txt_Input = null;
            // 
            // frmPasswordChange
            // 
            this.AcceptButton = this.btn_enter;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(785, 519);
            this.ControlBox = false;
            this.Controls.Add(this.pnl_passwordChange);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "frmPasswordChange";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Change Password";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmPasswordChange_FormClosing);
            this.Load += new System.EventHandler(this.frmPasswordChange_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmPasswordChange_KeyDown);
            this.pnl_passwordChange.ResumeLayout(false);
            this.grpBox_Password.ResumeLayout(false);
            this.grpBox_Password.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnl_passwordChange;
        private System.Windows.Forms.GroupBox grpBox_Password;
        private System.Windows.Forms.Label lbl_NewPassword;
        private System.Windows.Forms.Label lbl_CurrentPassword;
        private System.Windows.Forms.Label lbl_ConfirmPassword;
        private System.Windows.Forms.TextBox txt_ConfirmPassword;
        private System.Windows.Forms.TextBox txt_NewPassword;
        private System.Windows.Forms.TextBox txt_CurrentPassword;
        private System.Windows.Forms.Label lblUserId;
        private System.Windows.Forms.Label lalblEmployeenamebel1;
        private UC_KeyBoard UC_KeyBoard;
        private System.Windows.Forms.Label lbltextmessgae;
        private System.Windows.Forms.Button btn_enter;
        private System.Windows.Forms.Button btnClose;
    }
}