namespace JublFood.POS.App
{
    partial class frmKeyBoard
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmKeyBoard));
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
            this.pnl_Password.Size = new System.Drawing.Size(780, 442);
            this.pnl_Password.TabIndex = 1;
            // 
            // btn_enter
            // 
            this.btn_enter.BackColor = System.Drawing.Color.PeachPuff;
            this.btn_enter.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_enter.Image = ((System.Drawing.Image)(resources.GetObject("btn_enter.Image")));
            this.btn_enter.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btn_enter.Location = new System.Drawing.Point(252, 363);
            this.btn_enter.Name = "btn_enter";
            this.btn_enter.Size = new System.Drawing.Size(75, 60);
            this.btn_enter.TabIndex = 29;
            this.btn_enter.Text = "Enter";
            this.btn_enter.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btn_enter.UseVisualStyleBackColor = false;
            this.btn_enter.Click += new System.EventHandler(this.btn_enter_Click);
            // 
            // UC_KeyBoard
            // 
            this.UC_KeyBoard.Location = new System.Drawing.Point(-4, 52);
            this.UC_KeyBoard.Name = "UC_KeyBoard";
            this.UC_KeyBoard.Size = new System.Drawing.Size(780, 385);
            this.UC_KeyBoard.TabIndex = 1;
            this.UC_KeyBoard.txt_Input = null;
            this.UC_KeyBoard.Load += new System.EventHandler(this.UC_KeyBoard_Load);
            // 
            // txt_Input
            // 
            this.txt_Input.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_Input.Location = new System.Drawing.Point(4, 19);
            this.txt_Input.Name = "txt_Input";
            this.txt_Input.Size = new System.Drawing.Size(771, 29);
            this.txt_Input.TabIndex = 0;
            this.txt_Input.Enter += new System.EventHandler(this.txt_Input_Enter);
            this.txt_Input.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_Input_KeyDown);
            this.txt_Input.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_Input_KeyPress);
            // 
            // frmKeyBoard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(780, 442);
            this.ControlBox = false;
            this.Controls.Add(this.pnl_Password);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "frmKeyBoard";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmKeyBoard";
            this.pnl_Password.ResumeLayout(false);
            this.pnl_Password.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnl_Password;
        private UC_KeyBoard UC_KeyBoard;
        private System.Windows.Forms.Button btn_enter;
        public System.Windows.Forms.TextBox txt_Input;
    }
}