namespace JublFood.POS.App
{
    partial class UC_CustomerOrderBottomMenu
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.flPanel_orderType = new System.Windows.Forms.FlowLayoutPanel();
            this.cmdPay = new System.Windows.Forms.Button();
            this.cmdComplete = new System.Windows.Forms.Button();
            this.cmdPrintOnDemand = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // flPanel_orderType
            // 
            this.flPanel_orderType.Location = new System.Drawing.Point(3, 3);
            this.flPanel_orderType.Name = "flPanel_orderType";
            this.flPanel_orderType.Size = new System.Drawing.Size(274, 53);
            this.flPanel_orderType.TabIndex = 55;
            
            // 
            // cmdPay
            // 
            this.cmdPay.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.cmdPay.Enabled = false;
            this.cmdPay.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.cmdPay.FlatAppearance.BorderSize = 5;
            this.cmdPay.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdPay.Image = global::JublFood.POS.App.Properties.Resources._98;
            this.cmdPay.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.cmdPay.Location = new System.Drawing.Point(411, 3);
            this.cmdPay.Name = "cmdPay";
            this.cmdPay.Size = new System.Drawing.Size(68, 55);
            this.cmdPay.TabIndex = 52;
            this.cmdPay.Text = "Pay";
            this.cmdPay.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.cmdPay.UseVisualStyleBackColor = false;
            this.cmdPay.Click += new System.EventHandler(this.cmdPay_Click);
            // 
            // cmdComplete
            // 
            this.cmdComplete.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.cmdComplete.Enabled = false;
            this.cmdComplete.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.cmdComplete.FlatAppearance.BorderSize = 0;
            this.cmdComplete.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdComplete.Image = global::JublFood.POS.App.Properties.Resources._9;
            this.cmdComplete.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.cmdComplete.Location = new System.Drawing.Point(343, 3);
            this.cmdComplete.Name = "cmdComplete";
            this.cmdComplete.Size = new System.Drawing.Size(68, 55);
            this.cmdComplete.TabIndex = 53;
            this.cmdComplete.Text = "Save";
            this.cmdComplete.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.cmdComplete.UseVisualStyleBackColor = false;
            this.cmdComplete.Click += new System.EventHandler(this.cmdComplete_Click);
            // 
            // cmdPrintOnDemand
            // 
            this.cmdPrintOnDemand.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.cmdPrintOnDemand.Enabled = false;
            this.cmdPrintOnDemand.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.cmdPrintOnDemand.FlatAppearance.BorderSize = 0;
            this.cmdPrintOnDemand.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdPrintOnDemand.Image = global::JublFood.POS.App.Properties.Resources._157;
            this.cmdPrintOnDemand.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.cmdPrintOnDemand.Location = new System.Drawing.Point(275, 3);
            this.cmdPrintOnDemand.Name = "cmdPrintOnDemand";
            this.cmdPrintOnDemand.Size = new System.Drawing.Size(68, 55);
            this.cmdPrintOnDemand.TabIndex = 54;
            this.cmdPrintOnDemand.Text = "Print Extra";
            this.cmdPrintOnDemand.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.cmdPrintOnDemand.UseVisualStyleBackColor = false;
            // 
            // UC_CustomerOrderBottomMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.flPanel_orderType);
            this.Controls.Add(this.cmdPay);
            this.Controls.Add(this.cmdComplete);
            this.Controls.Add(this.cmdPrintOnDemand);
            this.Name = "UC_CustomerOrderBottomMenu";
            this.Size = new System.Drawing.Size(485, 59);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.FlowLayoutPanel flPanel_orderType;
        public System.Windows.Forms.Button cmdPay;
        public System.Windows.Forms.Button cmdComplete;
        public System.Windows.Forms.Button cmdPrintOnDemand;
    }
}
