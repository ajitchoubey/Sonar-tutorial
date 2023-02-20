namespace JublFood.POS.App
{
    partial class UC_FunctionList
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UC_FunctionList));
            this.cmdDeliveryFee = new System.Windows.Forms.Button();
            this.cmdUseCredit = new System.Windows.Forms.Button();
            this.cmdUseDebit = new System.Windows.Forms.Button();
            this.cmdVoid = new System.Windows.Forms.Button();
            this.cmdTaxExempt = new System.Windows.Forms.Button();
            this.cmdPrintCheckout = new System.Windows.Forms.Button();
            this.cmdCashDrop = new System.Windows.Forms.Button();
            this.cmdTraining = new System.Windows.Forms.Button();
            this.cmdGiveCredit = new System.Windows.Forms.Button();
            this.btnPutOnHold = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // cmdDeliveryFee
            // 
            this.cmdDeliveryFee.Location = new System.Drawing.Point(0, 0);
            this.cmdDeliveryFee.Name = "cmdDeliveryFee";
            this.cmdDeliveryFee.Size = new System.Drawing.Size(75, 23);
            this.cmdDeliveryFee.TabIndex = 0;
            // 
            // cmdUseCredit
            // 
            this.cmdUseCredit.Location = new System.Drawing.Point(0, 0);
            this.cmdUseCredit.Name = "cmdUseCredit";
            this.cmdUseCredit.Size = new System.Drawing.Size(75, 23);
            this.cmdUseCredit.TabIndex = 0;
            // 
            // cmdUseDebit
            // 
            this.cmdUseDebit.Location = new System.Drawing.Point(0, 0);
            this.cmdUseDebit.Name = "cmdUseDebit";
            this.cmdUseDebit.Size = new System.Drawing.Size(75, 23);
            this.cmdUseDebit.TabIndex = 0;
            // 
            // cmdVoid
            // 
            this.cmdVoid.Enabled = false;
            this.cmdVoid.Location = new System.Drawing.Point(0, 0);
            this.cmdVoid.Name = "cmdVoid";
            this.cmdVoid.Size = new System.Drawing.Size(75, 23);
            this.cmdVoid.TabIndex = 0;
            // 
            // cmdTaxExempt
            // 
            this.cmdTaxExempt.Location = new System.Drawing.Point(0, 0);
            this.cmdTaxExempt.Name = "cmdTaxExempt";
            this.cmdTaxExempt.Size = new System.Drawing.Size(75, 23);
            this.cmdTaxExempt.TabIndex = 0;
            // 
            // cmdPrintCheckout
            // 
            this.cmdPrintCheckout.Location = new System.Drawing.Point(0, 0);
            this.cmdPrintCheckout.Name = "cmdPrintCheckout";
            this.cmdPrintCheckout.Size = new System.Drawing.Size(75, 23);
            this.cmdPrintCheckout.TabIndex = 0;
            // 
            // cmdCashDrop
            // 
            this.cmdCashDrop.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdCashDrop.Image = ((System.Drawing.Image)(resources.GetObject("cmdCashDrop.Image")));
            this.cmdCashDrop.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.cmdCashDrop.Location = new System.Drawing.Point(1, 55);
            this.cmdCashDrop.Name = "cmdCashDrop";
            this.cmdCashDrop.Size = new System.Drawing.Size(68, 55);
            this.cmdCashDrop.TabIndex = 81;
            this.cmdCashDrop.Text = "Cash Drop";
            this.cmdCashDrop.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.cmdCashDrop.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.cmdCashDrop.UseVisualStyleBackColor = true;
            this.cmdCashDrop.Click += new System.EventHandler(this.cmdCashdrop_Click);
            // 
            // cmdTraining
            // 
            this.cmdTraining.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdTraining.Image = ((System.Drawing.Image)(resources.GetObject("cmdTraining.Image")));
            this.cmdTraining.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.cmdTraining.Location = new System.Drawing.Point(1, 1);
            this.cmdTraining.Name = "cmdTraining";
            this.cmdTraining.Size = new System.Drawing.Size(68, 55);
            this.cmdTraining.TabIndex = 82;
            this.cmdTraining.Text = "Training";
            this.cmdTraining.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.cmdTraining.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.cmdTraining.UseVisualStyleBackColor = true;
            this.cmdTraining.Click += new System.EventHandler(this.cmdTraining_Click);
            // 
            // cmdGiveCredit
            // 
            this.cmdGiveCredit.Location = new System.Drawing.Point(0, 0);
            this.cmdGiveCredit.Name = "cmdGiveCredit";
            this.cmdGiveCredit.Size = new System.Drawing.Size(75, 23);
            this.cmdGiveCredit.TabIndex = 0;
            // 
            // btnPutOnHold
            // 
            this.btnPutOnHold.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPutOnHold.Image = global::JublFood.POS.App.Properties.Resources.Cart;
            this.btnPutOnHold.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnPutOnHold.Location = new System.Drawing.Point(1, 109);
            this.btnPutOnHold.Name = "btnPutOnHold";
            this.btnPutOnHold.Size = new System.Drawing.Size(68, 55);
            this.btnPutOnHold.TabIndex = 83;
            this.btnPutOnHold.Text = "On Hold";
            this.btnPutOnHold.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnPutOnHold.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnPutOnHold.UseVisualStyleBackColor = true;
            this.btnPutOnHold.Click += new System.EventHandler(this.btnPutOnHold_Click);
            // 
            // UC_FunctionList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.Controls.Add(this.btnPutOnHold);
            this.Controls.Add(this.cmdCashDrop);
            this.Controls.Add(this.cmdTraining);
            this.Name = "UC_FunctionList";
            this.Size = new System.Drawing.Size(70, 165);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button cmdUseDebit;
        public System.Windows.Forms.Button btnPutOnHold;
        public System.Windows.Forms.Button cmdDeliveryFee;
        public System.Windows.Forms.Button cmdUseCredit;
        public System.Windows.Forms.Button cmdPrintCheckout;
        public System.Windows.Forms.Button cmdCashDrop;
        public System.Windows.Forms.Button cmdTraining;
        public System.Windows.Forms.Button cmdGiveCredit;
        public System.Windows.Forms.Button cmdTaxExempt;
        public System.Windows.Forms.Button cmdVoid;
    }
}
