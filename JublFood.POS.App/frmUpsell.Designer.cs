namespace JublFood.POS.App
{
    partial class frmUpsell
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmUpsell));
            this.pnl_Upsell = new System.Windows.Forms.Panel();
            this.flPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.Cmdaddcart = new System.Windows.Forms.Button();
            this.cmdSkip = new System.Windows.Forms.Button();
            this.lbltxtsubtotal1 = new System.Windows.Forms.Label();
            this.lbltxtitemcost = new System.Windows.Forms.Label();
            this.lblsubtotal1 = new System.Windows.Forms.Label();
            this.lblitemcost = new System.Windows.Forms.Label();
            this.lbltxtsubtotal = new System.Windows.Forms.Label();
            this.lbltxtquantity = new System.Windows.Forms.Label();
            this.lblsubtotal = new System.Windows.Forms.Label();
            this.lblquntity = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.pnl_Upsell.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnl_Upsell
            // 
            this.pnl_Upsell.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.pnl_Upsell.Controls.Add(this.flPanel);
            this.pnl_Upsell.Controls.Add(this.Cmdaddcart);
            this.pnl_Upsell.Controls.Add(this.cmdSkip);
            this.pnl_Upsell.Controls.Add(this.lbltxtsubtotal1);
            this.pnl_Upsell.Controls.Add(this.lbltxtitemcost);
            this.pnl_Upsell.Controls.Add(this.lblsubtotal1);
            this.pnl_Upsell.Controls.Add(this.lblitemcost);
            this.pnl_Upsell.Controls.Add(this.lbltxtsubtotal);
            this.pnl_Upsell.Controls.Add(this.lbltxtquantity);
            this.pnl_Upsell.Controls.Add(this.lblsubtotal);
            this.pnl_Upsell.Controls.Add(this.lblquntity);
            this.pnl_Upsell.Controls.Add(this.label2);
            this.pnl_Upsell.Controls.Add(this.label1);
            this.pnl_Upsell.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl_Upsell.Location = new System.Drawing.Point(0, 0);
            this.pnl_Upsell.Name = "pnl_Upsell";
            this.pnl_Upsell.Size = new System.Drawing.Size(697, 432);
            this.pnl_Upsell.TabIndex = 0;
            // 
            // flPanel
            // 
            this.flPanel.AutoScroll = true;
            this.flPanel.Location = new System.Drawing.Point(8, 40);
            this.flPanel.Name = "flPanel";
            this.flPanel.Size = new System.Drawing.Size(683, 293);
            this.flPanel.TabIndex = 39;
            // 
            // Cmdaddcart
            // 
            this.Cmdaddcart.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Cmdaddcart.Image = global::JublFood.POS.App.Properties.Resources.Cart;
            this.Cmdaddcart.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.Cmdaddcart.Location = new System.Drawing.Point(586, 345);
            this.Cmdaddcart.Name = "Cmdaddcart";
            this.Cmdaddcart.Size = new System.Drawing.Size(87, 67);
            this.Cmdaddcart.TabIndex = 3;
            this.Cmdaddcart.Text = "Add To Cart";
            this.Cmdaddcart.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.Cmdaddcart.UseVisualStyleBackColor = true;
            this.Cmdaddcart.Click += new System.EventHandler(this.Cmdaddcart_Click);
            // 
            // cmdSkip
            // 
            this.cmdSkip.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdSkip.Image = ((System.Drawing.Image)(resources.GetObject("cmdSkip.Image")));
            this.cmdSkip.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.cmdSkip.Location = new System.Drawing.Point(493, 345);
            this.cmdSkip.Name = "cmdSkip";
            this.cmdSkip.Size = new System.Drawing.Size(87, 67);
            this.cmdSkip.TabIndex = 3;
            this.cmdSkip.Text = "Skip";
            this.cmdSkip.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.cmdSkip.UseVisualStyleBackColor = true;
            this.cmdSkip.Click += new System.EventHandler(this.cmdSkip_Click);
            // 
            // lbltxtsubtotal1
            // 
            this.lbltxtsubtotal1.AutoSize = true;
            this.lbltxtsubtotal1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbltxtsubtotal1.ForeColor = System.Drawing.Color.White;
            this.lbltxtsubtotal1.Location = new System.Drawing.Point(401, 383);
            this.lbltxtsubtotal1.Name = "lbltxtsubtotal1";
            this.lbltxtsubtotal1.Size = new System.Drawing.Size(15, 15);
            this.lbltxtsubtotal1.TabIndex = 2;
            this.lbltxtsubtotal1.Text = "0";
            // 
            // lbltxtitemcost
            // 
            this.lbltxtitemcost.AutoSize = true;
            this.lbltxtitemcost.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbltxtitemcost.ForeColor = System.Drawing.Color.White;
            this.lbltxtitemcost.Location = new System.Drawing.Point(168, 383);
            this.lbltxtitemcost.Name = "lbltxtitemcost";
            this.lbltxtitemcost.Size = new System.Drawing.Size(15, 15);
            this.lbltxtitemcost.TabIndex = 2;
            this.lbltxtitemcost.Text = "0";
            // 
            // lblsubtotal1
            // 
            this.lblsubtotal1.AutoSize = true;
            this.lblsubtotal1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblsubtotal1.ForeColor = System.Drawing.Color.White;
            this.lblsubtotal1.Location = new System.Drawing.Point(244, 383);
            this.lblsubtotal1.Name = "lblsubtotal1";
            this.lblsubtotal1.Size = new System.Drawing.Size(132, 15);
            this.lblsubtotal1.TabIndex = 2;
            this.lblsubtotal1.Text = "Sub Total(w/o Disc)";
            // 
            // lblitemcost
            // 
            this.lblitemcost.AutoSize = true;
            this.lblitemcost.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblitemcost.ForeColor = System.Drawing.Color.White;
            this.lblitemcost.Location = new System.Drawing.Point(21, 383);
            this.lblitemcost.Name = "lblitemcost";
            this.lblitemcost.Size = new System.Drawing.Size(88, 15);
            this.lblitemcost.TabIndex = 2;
            this.lblitemcost.Text = "Item(s) Cost:";
            // 
            // lbltxtsubtotal
            // 
            this.lbltxtsubtotal.AutoSize = true;
            this.lbltxtsubtotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbltxtsubtotal.ForeColor = System.Drawing.Color.White;
            this.lbltxtsubtotal.Location = new System.Drawing.Point(401, 349);
            this.lbltxtsubtotal.Name = "lbltxtsubtotal";
            this.lbltxtsubtotal.Size = new System.Drawing.Size(15, 15);
            this.lbltxtsubtotal.TabIndex = 2;
            this.lbltxtsubtotal.Text = "0";
            // 
            // lbltxtquantity
            // 
            this.lbltxtquantity.AutoSize = true;
            this.lbltxtquantity.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbltxtquantity.ForeColor = System.Drawing.Color.White;
            this.lbltxtquantity.Location = new System.Drawing.Point(168, 351);
            this.lbltxtquantity.Name = "lbltxtquantity";
            this.lbltxtquantity.Size = new System.Drawing.Size(15, 15);
            this.lbltxtquantity.TabIndex = 2;
            this.lbltxtquantity.Text = "0";
            // 
            // lblsubtotal
            // 
            this.lblsubtotal.AutoSize = true;
            this.lblsubtotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblsubtotal.ForeColor = System.Drawing.Color.White;
            this.lblsubtotal.Location = new System.Drawing.Point(244, 349);
            this.lblsubtotal.Name = "lblsubtotal";
            this.lblsubtotal.Size = new System.Drawing.Size(73, 15);
            this.lblsubtotal.TabIndex = 2;
            this.lblsubtotal.Text = "Cart Total:";
            // 
            // lblquntity
            // 
            this.lblquntity.AutoSize = true;
            this.lblquntity.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblquntity.ForeColor = System.Drawing.Color.White;
            this.lblquntity.Location = new System.Drawing.Point(21, 349);
            this.lblquntity.Name = "lblquntity";
            this.lblquntity.Size = new System.Drawing.Size(116, 15);
            this.lblquntity.TabIndex = 2;
            this.lblquntity.Text = "Item(s) Selected:";
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(2, 337);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(689, 2);
            this.label2.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(207, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(239, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Would you like to add this...?";
            // 
            // frmUpsell
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(697, 432);
            this.ControlBox = false;
            this.Controls.Add(this.pnl_Upsell);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmUpsell";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmUpsell_FormClosing);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmUpsell_KeyDown);
            this.pnl_Upsell.ResumeLayout(false);
            this.pnl_Upsell.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnl_Upsell;
        private System.Windows.Forms.Label lblquntity;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbltxtitemcost;
        private System.Windows.Forms.Label lblitemcost;
        private System.Windows.Forms.Label lbltxtquantity;
        private System.Windows.Forms.Label lbltxtsubtotal1;
        private System.Windows.Forms.Label lblsubtotal1;
        private System.Windows.Forms.Label lbltxtsubtotal;
        private System.Windows.Forms.Label lblsubtotal;
        private System.Windows.Forms.Button Cmdaddcart;
        private System.Windows.Forms.Button cmdSkip;
        private System.Windows.Forms.FlowLayoutPanel flPanel;
    }
}