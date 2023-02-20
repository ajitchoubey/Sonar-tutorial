namespace JublFood.POS.App
{
    partial class frmPassword
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPassword));
            this.pnl_Password = new System.Windows.Forms.Panel();
            this.btn_enter = new System.Windows.Forms.Button();
            this.UC_KeyBoard = new JublFood.POS.App.UC_KeyBoard();
            this.txt_Input = new System.Windows.Forms.TextBox();
            this.pnl_Password.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnl_Password
            // 
            this.pnl_Password.BackColor = System.Drawing.Color.Teal;
            this.pnl_Password.Controls.Add(this.btn_enter);
            this.pnl_Password.Controls.Add(this.UC_KeyBoard);
            this.pnl_Password.Controls.Add(this.txt_Input);
            this.pnl_Password.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl_Password.Location = new System.Drawing.Point(0, 0);
            this.pnl_Password.Name = "pnl_Password";
            this.pnl_Password.Size = new System.Drawing.Size(789, 451);
            this.pnl_Password.TabIndex = 0;
            // 
            // btn_enter
            // 
            this.btn_enter.BackColor = System.Drawing.Color.PeachPuff;
            this.btn_enter.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_enter.Image = ((System.Drawing.Image)(resources.GetObject("btn_enter.Image")));
            this.btn_enter.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btn_enter.Location = new System.Drawing.Point(262, 360);
            this.btn_enter.Name = "btn_enter";
            this.btn_enter.Size = new System.Drawing.Size(75, 60);
            this.btn_enter.TabIndex = 28;
            this.btn_enter.Text = "Enter";
            this.btn_enter.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btn_enter.UseVisualStyleBackColor = false;
            this.btn_enter.Click += new System.EventHandler(this.btn_enter_Click);
            // 
            // UC_KeyBoard
            // 
            this.UC_KeyBoard.Location = new System.Drawing.Point(6, 49);
            this.UC_KeyBoard.Name = "UC_KeyBoard";
            this.UC_KeyBoard.Size = new System.Drawing.Size(778, 385);
            this.UC_KeyBoard.TabIndex = 1;
            this.UC_KeyBoard.txt_Input = null;
            // 
            // txt_Input
            // 
            this.txt_Input.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_Input.Location = new System.Drawing.Point(12, 19);
            this.txt_Input.MaxLength = 100;
            this.txt_Input.Name = "txt_Input";
            this.txt_Input.PasswordChar = '*';
            this.txt_Input.Size = new System.Drawing.Size(772, 29);
            this.txt_Input.TabIndex = 0;
            this.txt_Input.Enter += new System.EventHandler(this.txt_Input_Enter);
            this.txt_Input.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_Input_KeyPress);
            // 
            // frmPassword
            // 
            this.AcceptButton = this.btn_enter;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(789, 451);
            this.ControlBox = false;
            this.Controls.Add(this.pnl_Password);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "frmPassword";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Password";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmPassword_FormClosing);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmPassword_KeyDown);
            this.pnl_Password.ResumeLayout(false);
            this.pnl_Password.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnl_Password;
        private System.Windows.Forms.TextBox txt_Input;
        private UC_KeyBoard UC_KeyBoard;
        private System.Windows.Forms.Button btn_enter;
    }
}