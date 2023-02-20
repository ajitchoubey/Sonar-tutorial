namespace JublFood.POS.App
{
    partial class frmDetermineOrderDate
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
            this.pnl_DetermineOrderDate = new System.Windows.Forms.Panel();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.cmdDate2 = new System.Windows.Forms.Button();
            this.cmdDate1 = new System.Windows.Forms.Button();
            this.lblMSGPart2 = new System.Windows.Forms.Label();
            this.lblMSGPart1 = new System.Windows.Forms.Label();
            this.pnl_DetermineOrderDate.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnl_DetermineOrderDate
            // 
            this.pnl_DetermineOrderDate.Controls.Add(this.cmdCancel);
            this.pnl_DetermineOrderDate.Controls.Add(this.cmdDate2);
            this.pnl_DetermineOrderDate.Controls.Add(this.cmdDate1);
            this.pnl_DetermineOrderDate.Controls.Add(this.lblMSGPart2);
            this.pnl_DetermineOrderDate.Controls.Add(this.lblMSGPart1);
            this.pnl_DetermineOrderDate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl_DetermineOrderDate.Location = new System.Drawing.Point(0, 0);
            this.pnl_DetermineOrderDate.Name = "pnl_DetermineOrderDate";
            this.pnl_DetermineOrderDate.Size = new System.Drawing.Size(375, 200);
            this.pnl_DetermineOrderDate.TabIndex = 0;
            // 
            // cmdCancel
            // 
            this.cmdCancel.BackColor = System.Drawing.Color.PeachPuff;
            this.cmdCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.cmdCancel.Image = global::JublFood.POS.App.Properties.Resources._92;
            this.cmdCancel.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.cmdCancel.Location = new System.Drawing.Point(218, 124);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(68, 55);
            this.cmdCancel.TabIndex = 30;
            this.cmdCancel.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.cmdCancel.UseVisualStyleBackColor = false;
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // cmdDate2
            // 
            this.cmdDate2.BackColor = System.Drawing.Color.PeachPuff;
            this.cmdDate2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.cmdDate2.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.cmdDate2.Location = new System.Drawing.Point(151, 124);
            this.cmdDate2.Name = "cmdDate2";
            this.cmdDate2.Size = new System.Drawing.Size(68, 55);
            this.cmdDate2.TabIndex = 30;
            this.cmdDate2.UseVisualStyleBackColor = false;
            this.cmdDate2.Click += new System.EventHandler(this.cmdDate2_Click);
            // 
            // cmdDate1
            // 
            this.cmdDate1.BackColor = System.Drawing.Color.PeachPuff;
            this.cmdDate1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.cmdDate1.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.cmdDate1.Location = new System.Drawing.Point(84, 124);
            this.cmdDate1.Name = "cmdDate1";
            this.cmdDate1.Size = new System.Drawing.Size(68, 55);
            this.cmdDate1.TabIndex = 30;
            this.cmdDate1.UseVisualStyleBackColor = false;
            this.cmdDate1.Click += new System.EventHandler(this.cmdDate1_Click);
            // 
            // lblMSGPart2
            // 
            this.lblMSGPart2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMSGPart2.ForeColor = System.Drawing.Color.White;
            this.lblMSGPart2.Location = new System.Drawing.Point(13, 70);
            this.lblMSGPart2.Name = "lblMSGPart2";
            this.lblMSGPart2.Size = new System.Drawing.Size(315, 32);
            this.lblMSGPart2.TabIndex = 1;
            this.lblMSGPart2.Text = "Please choose the processing date that you \r\n  would like to have this order repo" +
    "rted on.";
            this.lblMSGPart2.UseCompatibleTextRendering = true;
            // 
            // lblMSGPart1
            // 
            this.lblMSGPart1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMSGPart1.ForeColor = System.Drawing.Color.White;
            this.lblMSGPart1.Location = new System.Drawing.Point(13, 21);
            this.lblMSGPart1.Name = "lblMSGPart1";
            this.lblMSGPart1.Size = new System.Drawing.Size(335, 32);
            this.lblMSGPart1.TabIndex = 0;
            this.lblMSGPart1.Text = "The delayed order date and time that you have \r\n         chosen occurs after the " +
    "store is closed.";
            this.lblMSGPart1.UseCompatibleTextRendering = true;
            // 
            // frmDetermineOrderDate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Teal;
            this.ClientSize = new System.Drawing.Size(375, 200);
            this.Controls.Add(this.pnl_DetermineOrderDate);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmDetermineOrderDate";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Determine Order Date";
            this.Load += new System.EventHandler(this.frmDetermineOrderDate_Load);
            this.pnl_DetermineOrderDate.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnl_DetermineOrderDate;
        private System.Windows.Forms.Label lblMSGPart2;
        private System.Windows.Forms.Label lblMSGPart1;
        private System.Windows.Forms.Button cmdCancel;
        private System.Windows.Forms.Button cmdDate2;
        private System.Windows.Forms.Button cmdDate1;
    }
}