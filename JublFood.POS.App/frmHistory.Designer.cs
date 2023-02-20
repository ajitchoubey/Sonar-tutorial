namespace JublFood.POS.App
{
    partial class frmHistory
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.pnl_History = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.lblOrderInfo = new System.Windows.Forms.Label();
            this.dgvCart = new System.Windows.Forms.DataGridView();
            this.Qty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Item = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VegNVegColor = new System.Windows.Forms.DataGridViewImageColumn();
            this.Price = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lbltotalAmount = new System.Windows.Forms.Label();
            this.lblTotal = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cmdDetails = new System.Windows.Forms.Button();
            this.cmdDown = new System.Windows.Forms.Button();
            this.cmdUp = new System.Windows.Forms.Button();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.cmdAdd = new System.Windows.Forms.Button();
            this.cmdReplace = new System.Windows.Forms.Button();
            this.cmdLast = new System.Windows.Forms.Button();
            this.cmdNext = new System.Windows.Forms.Button();
            this.cmdPrevious = new System.Windows.Forms.Button();
            this.cmdFirst = new System.Windows.Forms.Button();
            this.pnl_History.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCart)).BeginInit();
            this.SuspendLayout();
            // 
            // pnl_History
            // 
            this.pnl_History.BackColor = System.Drawing.Color.Teal;
            this.pnl_History.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnl_History.Controls.Add(this.label2);
            this.pnl_History.Controls.Add(this.lblOrderInfo);
            this.pnl_History.Controls.Add(this.dgvCart);
            this.pnl_History.Controls.Add(this.lbltotalAmount);
            this.pnl_History.Controls.Add(this.lblTotal);
            this.pnl_History.Controls.Add(this.label1);
            this.pnl_History.Controls.Add(this.cmdDetails);
            this.pnl_History.Controls.Add(this.cmdDown);
            this.pnl_History.Controls.Add(this.cmdUp);
            this.pnl_History.Controls.Add(this.cmdCancel);
            this.pnl_History.Controls.Add(this.cmdAdd);
            this.pnl_History.Controls.Add(this.cmdReplace);
            this.pnl_History.Controls.Add(this.cmdLast);
            this.pnl_History.Controls.Add(this.cmdNext);
            this.pnl_History.Controls.Add(this.cmdPrevious);
            this.pnl_History.Controls.Add(this.cmdFirst);
            this.pnl_History.Location = new System.Drawing.Point(10, 10);
            this.pnl_History.Name = "pnl_History";
            this.pnl_History.Size = new System.Drawing.Size(445, 474);
            this.pnl_History.TabIndex = 0;
            this.pnl_History.Paint += new System.Windows.Forms.PaintEventHandler(this.pnl_History_Paint);
            // 
            // label2
            // 
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(279, 442);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(25, 25);
            this.label2.TabIndex = 9;
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblOrderInfo
            // 
            this.lblOrderInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOrderInfo.ForeColor = System.Drawing.Color.Yellow;
            this.lblOrderInfo.Location = new System.Drawing.Point(-10, 0);
            this.lblOrderInfo.Name = "lblOrderInfo";
            this.lblOrderInfo.Size = new System.Drawing.Size(464, 28);
            this.lblOrderInfo.TabIndex = 8;
            this.lblOrderInfo.Text = "Order Info";
            this.lblOrderInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dgvCart
            // 
            this.dgvCart.AllowUserToAddRows = false;
            this.dgvCart.AllowUserToDeleteRows = false;
            this.dgvCart.AllowUserToResizeRows = false;
            this.dgvCart.BackgroundColor = System.Drawing.Color.LightGoldenrodYellow;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Teal;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.Teal;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvCart.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvCart.ColumnHeadersHeight = 30;
            this.dgvCart.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Qty,
            this.Item,
            this.VegNVegColor,
            this.Price});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.LightGoldenrodYellow;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvCart.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvCart.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgvCart.EnableHeadersVisualStyles = false;
            this.dgvCart.GridColor = System.Drawing.SystemColors.ActiveBorder;
            this.dgvCart.Location = new System.Drawing.Point(8, 48);
            this.dgvCart.MultiSelect = false;
            this.dgvCart.Name = "dgvCart";
            this.dgvCart.ReadOnly = true;
            this.dgvCart.RowHeadersVisible = false;
            this.dgvCart.RowHeadersWidth = 10;
            this.dgvCart.RowTemplate.Height = 25;
            this.dgvCart.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgvCart.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvCart.Size = new System.Drawing.Size(296, 394);
            this.dgvCart.TabIndex = 7;
            // 
            // Qty
            // 
            this.Qty.HeaderText = "Qty";
            this.Qty.Name = "Qty";
            this.Qty.ReadOnly = true;
            this.Qty.Width = 40;
            // 
            // Item
            // 
            this.Item.HeaderText = "Item";
            this.Item.Name = "Item";
            this.Item.ReadOnly = true;
            this.Item.Width = 150;
            // 
            // VegNVegColor
            // 
            this.VegNVegColor.HeaderText = "";
            this.VegNVegColor.Name = "VegNVegColor";
            this.VegNVegColor.ReadOnly = true;
            this.VegNVegColor.Width = 20;
            // 
            // Price
            // 
            this.Price.HeaderText = "Price";
            this.Price.Name = "Price";
            this.Price.ReadOnly = true;
            this.Price.Width = 60;
            // 
            // lbltotalAmount
            // 
            this.lbltotalAmount.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lbltotalAmount.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbltotalAmount.ForeColor = System.Drawing.Color.White;
            this.lbltotalAmount.Location = new System.Drawing.Point(202, 442);
            this.lbltotalAmount.Name = "lbltotalAmount";
            this.lbltotalAmount.Size = new System.Drawing.Size(79, 25);
            this.lbltotalAmount.TabIndex = 6;
            this.lbltotalAmount.Text = "0.00";
            this.lbltotalAmount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lbltotalAmount.TextAlignChanged += new System.EventHandler(this.lbltotalAmount_TextAlignChanged);
            // 
            // lblTotal
            // 
            this.lblTotal.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotal.ForeColor = System.Drawing.Color.White;
            this.lblTotal.Location = new System.Drawing.Point(8, 442);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(197, 25);
            this.lblTotal.TabIndex = 6;
            this.lblTotal.Text = "Total";
            this.lblTotal.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label1
            // 
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label1.Location = new System.Drawing.Point(17, 467);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 23);
            this.label1.TabIndex = 6;
            // 
            // cmdDetails
            // 
            this.cmdDetails.Location = new System.Drawing.Point(161, 416);
            this.cmdDetails.Name = "cmdDetails";
            this.cmdDetails.Size = new System.Drawing.Size(60, 55);
            this.cmdDetails.TabIndex = 5;
            this.cmdDetails.UseVisualStyleBackColor = true;
            this.cmdDetails.Visible = false;
            // 
            // cmdDown
            // 
            this.cmdDown.Location = new System.Drawing.Point(3, 435);
            this.cmdDown.Name = "cmdDown";
            this.cmdDown.Size = new System.Drawing.Size(60, 55);
            this.cmdDown.TabIndex = 5;
            this.cmdDown.UseVisualStyleBackColor = true;
            this.cmdDown.Visible = false;
            // 
            // cmdUp
            // 
            this.cmdUp.Location = new System.Drawing.Point(3, 386);
            this.cmdUp.Name = "cmdUp";
            this.cmdUp.Size = new System.Drawing.Size(60, 55);
            this.cmdUp.TabIndex = 4;
            this.cmdUp.UseVisualStyleBackColor = true;
            this.cmdUp.Visible = false;
            // 
            // cmdCancel
            // 
            this.cmdCancel.BackColor = System.Drawing.Color.PeachPuff;
            this.cmdCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.cmdCancel.Image = global::JublFood.POS.App.Properties.Resources._92;
            this.cmdCancel.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.cmdCancel.Location = new System.Drawing.Point(361, 371);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(68, 55);
            this.cmdCancel.TabIndex = 3;
            this.cmdCancel.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.cmdCancel.UseVisualStyleBackColor = false;
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // cmdAdd
            // 
            this.cmdAdd.BackColor = System.Drawing.Color.PeachPuff;
            this.cmdAdd.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.cmdAdd.Image = global::JublFood.POS.App.Properties.Resources._81;
            this.cmdAdd.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.cmdAdd.Location = new System.Drawing.Point(361, 317);
            this.cmdAdd.Name = "cmdAdd";
            this.cmdAdd.Size = new System.Drawing.Size(68, 55);
            this.cmdAdd.TabIndex = 3;
            this.cmdAdd.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.cmdAdd.UseVisualStyleBackColor = false;
            this.cmdAdd.Click += new System.EventHandler(this.cmdAdd_Click);
            // 
            // cmdReplace
            // 
            this.cmdReplace.BackColor = System.Drawing.Color.PeachPuff;
            this.cmdReplace.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.cmdReplace.Image = global::JublFood.POS.App.Properties.Resources._80;
            this.cmdReplace.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.cmdReplace.Location = new System.Drawing.Point(361, 263);
            this.cmdReplace.Name = "cmdReplace";
            this.cmdReplace.Size = new System.Drawing.Size(68, 55);
            this.cmdReplace.TabIndex = 3;
            this.cmdReplace.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.cmdReplace.UseVisualStyleBackColor = false;
            this.cmdReplace.Click += new System.EventHandler(this.cmdReplace_Click);
            // 
            // cmdLast
            // 
            this.cmdLast.BackColor = System.Drawing.Color.PeachPuff;
            this.cmdLast.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.cmdLast.Image = global::JublFood.POS.App.Properties.Resources._41;
            this.cmdLast.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.cmdLast.Location = new System.Drawing.Point(361, 210);
            this.cmdLast.Name = "cmdLast";
            this.cmdLast.Size = new System.Drawing.Size(68, 55);
            this.cmdLast.TabIndex = 3;
            this.cmdLast.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.cmdLast.UseVisualStyleBackColor = false;
            this.cmdLast.Click += new System.EventHandler(this.cmdLast_Click);
            // 
            // cmdNext
            // 
            this.cmdNext.BackColor = System.Drawing.Color.PeachPuff;
            this.cmdNext.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.cmdNext.Image = global::JublFood.POS.App.Properties.Resources._31;
            this.cmdNext.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.cmdNext.Location = new System.Drawing.Point(361, 156);
            this.cmdNext.Name = "cmdNext";
            this.cmdNext.Size = new System.Drawing.Size(68, 55);
            this.cmdNext.TabIndex = 3;
            this.cmdNext.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.cmdNext.UseVisualStyleBackColor = false;
            this.cmdNext.Click += new System.EventHandler(this.cmdNext_Click);
            // 
            // cmdPrevious
            // 
            this.cmdPrevious.BackColor = System.Drawing.Color.PeachPuff;
            this.cmdPrevious.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.cmdPrevious.Image = global::JublFood.POS.App.Properties.Resources._34;
            this.cmdPrevious.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.cmdPrevious.Location = new System.Drawing.Point(361, 102);
            this.cmdPrevious.Name = "cmdPrevious";
            this.cmdPrevious.Size = new System.Drawing.Size(68, 55);
            this.cmdPrevious.TabIndex = 3;
            this.cmdPrevious.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.cmdPrevious.UseVisualStyleBackColor = false;
            this.cmdPrevious.Click += new System.EventHandler(this.cmdPrevious_Click);
            // 
            // cmdFirst
            // 
            this.cmdFirst.BackColor = System.Drawing.Color.PeachPuff;
            this.cmdFirst.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.cmdFirst.Image = global::JublFood.POS.App.Properties.Resources._40;
            this.cmdFirst.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.cmdFirst.Location = new System.Drawing.Point(361, 48);
            this.cmdFirst.Name = "cmdFirst";
            this.cmdFirst.Size = new System.Drawing.Size(68, 55);
            this.cmdFirst.TabIndex = 3;
            this.cmdFirst.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.cmdFirst.UseVisualStyleBackColor = false;
            this.cmdFirst.Click += new System.EventHandler(this.cmdFirst_Click);
            // 
            // frmHistory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(467, 494);
            this.ControlBox = false;
            this.Controls.Add(this.pnl_History);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmHistory";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmHistory";
            this.Activated += new System.EventHandler(this.frmHistory_Activated);
            this.Load += new System.EventHandler(this.frmHistory_Load);
            this.pnl_History.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCart)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnl_History;
        private System.Windows.Forms.Button cmdCancel;
        private System.Windows.Forms.Button cmdAdd;
        private System.Windows.Forms.Button cmdReplace;
        private System.Windows.Forms.Button cmdLast;
        private System.Windows.Forms.Button cmdNext;
        private System.Windows.Forms.Button cmdPrevious;
        private System.Windows.Forms.Button cmdFirst;
        private System.Windows.Forms.Button cmdDetails;
        private System.Windows.Forms.Button cmdDown;
        private System.Windows.Forms.Button cmdUp;
        private System.Windows.Forms.Label lbltotalAmount;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.DataGridView dgvCart;
        private System.Windows.Forms.DataGridViewTextBoxColumn Qty;
        private System.Windows.Forms.DataGridViewTextBoxColumn Item;
        private System.Windows.Forms.DataGridViewImageColumn VegNVegColor;
        private System.Windows.Forms.DataGridViewTextBoxColumn Price;
        private System.Windows.Forms.Label lblOrderInfo;
        private System.Windows.Forms.Label label2;
    }
}