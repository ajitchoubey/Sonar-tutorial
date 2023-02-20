namespace JublFood.POS.App
{
    partial class frmSearch
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
            this.tlpSearch = new System.Windows.Forms.TableLayoutPanel();
            this.flowLayoutPanelMenuItems = new System.Windows.Forms.FlowLayoutPanel();
            this.btnClose = new System.Windows.Forms.Button();
            this.cmdKeyboard = new System.Windows.Forms.Button();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.lblHeader = new System.Windows.Forms.Label();
            this.tlpSearch.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlpSearch
            // 
            this.tlpSearch.BackColor = System.Drawing.Color.Teal;
            this.tlpSearch.ColumnCount = 3;
            this.tlpSearch.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.tlpSearch.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tlpSearch.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tlpSearch.Controls.Add(this.btnClose, 2, 1);
            this.tlpSearch.Controls.Add(this.cmdKeyboard, 1, 1);
            this.tlpSearch.Controls.Add(this.txtSearch, 0, 1);
            this.tlpSearch.Controls.Add(this.lblHeader, 0, 0);
            this.tlpSearch.Controls.Add(this.flowLayoutPanelMenuItems, 0, 2);
            this.tlpSearch.Location = new System.Drawing.Point(5, 5);
            this.tlpSearch.Name = "tlpSearch";
            this.tlpSearch.RowCount = 3;
            this.tlpSearch.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpSearch.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tlpSearch.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 83.33333F));
            this.tlpSearch.Size = new System.Drawing.Size(605, 425);
            this.tlpSearch.TabIndex = 0;
            // 
            // flowLayoutPanelMenuItems
            // 
            this.tlpSearch.SetColumnSpan(this.flowLayoutPanelMenuItems, 3);
            this.flowLayoutPanelMenuItems.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanelMenuItems.Location = new System.Drawing.Point(8, 86);
            this.flowLayoutPanelMenuItems.Margin = new System.Windows.Forms.Padding(12, 3, 0, 3);
            this.flowLayoutPanelMenuItems.Name = "flowLayoutPanelMenuItems";
            this.flowLayoutPanelMenuItems.Size = new System.Drawing.Size(591, 311);
            this.flowLayoutPanelMenuItems.TabIndex = 3;
            // 
            // btnClose
            // 
            this.btnClose.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnClose.BackColor = System.Drawing.Color.PeachPuff;
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Image = global::JublFood.POS.App.Properties.Resources._35;
            this.btnClose.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnClose.Location = new System.Drawing.Point(519, 24);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(80, 58);
            this.btnClose.TabIndex = 2;
            this.btnClose.Text = "Close";
            this.btnClose.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // cmdKeyboard
            // 
            this.cmdKeyboard.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.cmdKeyboard.BackColor = System.Drawing.SystemColors.Control;
            this.cmdKeyboard.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdKeyboard.Image = global::JublFood.POS.App.Properties.Resources._42;
            this.cmdKeyboard.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.cmdKeyboard.Location = new System.Drawing.Point(428, 24);
            this.cmdKeyboard.Name = "cmdKeyboard";
            this.cmdKeyboard.Size = new System.Drawing.Size(80, 58);
            this.cmdKeyboard.TabIndex = 1;
            this.cmdKeyboard.Text = "Keyboard";
            this.cmdKeyboard.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.cmdKeyboard.UseVisualStyleBackColor = false;
            this.cmdKeyboard.Click += new System.EventHandler(this.cmdKeyboard_Click);
            // 
            // txtSearch
            // 
            this.txtSearch.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSearch.Location = new System.Drawing.Point(13, 40);
            this.txtSearch.MaxLength = 50;
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(397, 27);
            this.txtSearch.TabIndex = 0;
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            // 
            // lblHeader
            // 
            this.lblHeader.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblHeader.AutoSize = true;
            this.tlpSearch.SetColumnSpan(this.lblHeader, 3);
            this.lblHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeader.Location = new System.Drawing.Point(3, 3);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(599, 17);
            this.lblHeader.TabIndex = 4;
            this.lblHeader.Text = "Menu Item Search";
            this.lblHeader.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // frmSearch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(615, 435);
            this.Controls.Add(this.tlpSearch);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmSearch";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmSearch";
            this.Activated += new System.EventHandler(this.frmSearch_Activated);
            this.Load += new System.EventHandler(this.frmSearch_Load);
            this.tlpSearch.ResumeLayout(false);
            this.tlpSearch.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpSearch;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Button cmdKeyboard;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelMenuItems;
        private System.Windows.Forms.Label lblHeader;
    }
}