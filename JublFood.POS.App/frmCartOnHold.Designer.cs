namespace JublFood.POS.App
{
    partial class frmCartOnHold
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
            this.lblList = new System.Windows.Forms.Label();
            this.dgvOnHold = new System.Windows.Forms.DataGridView();
            this.Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Time = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CustomerName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CustomerNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OrderAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OrderTaker = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Terminal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CartId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IsActive = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblNoRecord = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnDelete = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOnHold)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblList
            // 
            this.lblList.AutoSize = true;
            this.lblList.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblList.Location = new System.Drawing.Point(258, 10);
            this.lblList.Name = "lblList";
            this.lblList.Size = new System.Drawing.Size(235, 18);
            this.lblList.TabIndex = 0;
            this.lblList.Text = "List of all orders kept On Hold";
            this.lblList.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.lblList.Click += new System.EventHandler(this.label1_Click);
            // 
            // dgvOnHold
            // 
            this.dgvOnHold.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvOnHold.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Id,
            this.Time,
            this.CustomerName,
            this.CustomerNumber,
            this.OrderAmount,
            this.OrderTaker,
            this.Terminal,
            this.CartId,
            this.IsActive});
            this.dgvOnHold.Location = new System.Drawing.Point(6, 35);
            this.dgvOnHold.Name = "dgvOnHold";
            this.dgvOnHold.RowHeadersVisible = false;
            this.dgvOnHold.RowTemplate.Height = 30;
            this.dgvOnHold.Size = new System.Drawing.Size(713, 329);
            this.dgvOnHold.TabIndex = 0;
            this.dgvOnHold.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvOnHold_CellClick);
            // 
            // Id
            // 
            this.Id.HeaderText = "#";
            this.Id.Name = "Id";
            this.Id.ReadOnly = true;
            this.Id.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Id.Width = 40;
            // 
            // Time
            // 
            this.Time.HeaderText = "Time";
            this.Time.Name = "Time";
            this.Time.ReadOnly = true;
            // 
            // CustomerName
            // 
            this.CustomerName.HeaderText = "Customer Name";
            this.CustomerName.Name = "CustomerName";
            this.CustomerName.ReadOnly = true;
            this.CustomerName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.CustomerName.Width = 160;
            // 
            // CustomerNumber
            // 
            this.CustomerNumber.HeaderText = "Customer Number";
            this.CustomerNumber.Name = "CustomerNumber";
            this.CustomerNumber.ReadOnly = true;
            this.CustomerNumber.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.CustomerNumber.Width = 150;
            // 
            // OrderAmount
            // 
            this.OrderAmount.HeaderText = "Order Amount";
            this.OrderAmount.Name = "OrderAmount";
            this.OrderAmount.ReadOnly = true;
            this.OrderAmount.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // OrderTaker
            // 
            this.OrderTaker.HeaderText = "Order Taker";
            this.OrderTaker.Name = "OrderTaker";
            this.OrderTaker.ReadOnly = true;
            this.OrderTaker.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.OrderTaker.Width = 160;
            // 
            // Terminal
            // 
            this.Terminal.HeaderText = "Terminal";
            this.Terminal.Name = "Terminal";
            this.Terminal.ReadOnly = true;
            this.Terminal.Width = 160;
            // 
            // CartId
            // 
            this.CartId.HeaderText = "CartId";
            this.CartId.Name = "CartId";
            this.CartId.ReadOnly = true;
            this.CartId.Visible = false;
            // 
            // IsActive
            // 
            this.IsActive.HeaderText = "IsActive";
            this.IsActive.Name = "IsActive";
            this.IsActive.ReadOnly = true;
            this.IsActive.Visible = false;
            // 
            // lblNoRecord
            // 
            this.lblNoRecord.AutoSize = true;
            this.lblNoRecord.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNoRecord.Location = new System.Drawing.Point(396, 140);
            this.lblNoRecord.Name = "lblNoRecord";
            this.lblNoRecord.Size = new System.Drawing.Size(97, 20);
            this.lblNoRecord.TabIndex = 1;
            this.lblNoRecord.Text = "No Records!";
            this.lblNoRecord.Visible = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnDelete);
            this.panel1.Location = new System.Drawing.Point(4, 369);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(714, 54);
            this.panel1.TabIndex = 2;
            // 
            // btnDelete
            // 
            this.btnDelete.BackColor = System.Drawing.SystemColors.Control;
            this.btnDelete.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDelete.Image = global::JublFood.POS.App.Properties.Resources._97;
            this.btnDelete.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnDelete.Location = new System.Drawing.Point(639, 3);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(68, 49);
            this.btnDelete.TabIndex = 78;
            this.btnDelete.Text = "Close";
            this.btnDelete.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // frmCartOnHold
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(724, 428);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.lblNoRecord);
            this.Controls.Add(this.dgvOnHold);
            this.Controls.Add(this.lblList);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmCartOnHold";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "On Hold Orders";
            this.Load += new System.EventHandler(this.frmCartOnHold_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvOnHold)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblList;
        private System.Windows.Forms.DataGridView dgvOnHold;
        private System.Windows.Forms.DataGridViewTextBoxColumn Id;
        private System.Windows.Forms.DataGridViewTextBoxColumn Time;
        private System.Windows.Forms.DataGridViewTextBoxColumn CustomerName;
        private System.Windows.Forms.DataGridViewTextBoxColumn CustomerNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn OrderAmount;
        private System.Windows.Forms.DataGridViewTextBoxColumn OrderTaker;
        private System.Windows.Forms.DataGridViewTextBoxColumn Terminal;
        private System.Windows.Forms.DataGridViewTextBoxColumn CartId;
        private System.Windows.Forms.Label lblNoRecord;
        private System.Windows.Forms.DataGridViewTextBoxColumn IsActive;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnDelete;
    }
}