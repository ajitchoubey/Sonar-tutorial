namespace JublFood.POS.App
{
    partial class frmRecap
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmRecap));
            this.ltxtMinutes = new System.Windows.Forms.Label();
            this.lblEndET = new System.Windows.Forms.TextBox();
            this.lblBeginET = new System.Windows.Forms.TextBox();
            this.lblOrderTotal = new System.Windows.Forms.TextBox();
            this.tdbdDelayedOrderDate = new System.Windows.Forms.TextBox();
            this.tdbtDelayedOrderTime = new System.Windows.Forms.TextBox();
            this.tdbtOrderTime = new System.Windows.Forms.TextBox();
            this.lblEstimatedTime = new System.Windows.Forms.Label();
            this.ltxtOrderTotal = new System.Windows.Forms.Label();
            this.ltxtDelayedTimeDate = new System.Windows.Forms.Label();
            this.ltxtCurrentTime = new System.Windows.Forms.Label();
            this.lblEstimatedTime2 = new System.Windows.Forms.Label();
            this.lblBeginET2 = new System.Windows.Forms.TextBox();
            this.lblEndET2 = new System.Windows.Forms.TextBox();
            this.ltxtMinutes2 = new System.Windows.Forms.Label();
            this.tlpRecap = new System.Windows.Forms.TableLayoutPanel();
            this.lblDash2 = new System.Windows.Forms.Label();
            this.cmdClose = new System.Windows.Forms.Button();
            this.lblDash = new System.Windows.Forms.Label();
            this.tlpRecap.SuspendLayout();
            this.SuspendLayout();
            // 
            // ltxtMinutes
            // 
            this.ltxtMinutes.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.ltxtMinutes.AutoSize = true;
            this.ltxtMinutes.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ltxtMinutes.Location = new System.Drawing.Point(418, 122);
            this.ltxtMinutes.Name = "ltxtMinutes";
            this.ltxtMinutes.Size = new System.Drawing.Size(65, 20);
            this.ltxtMinutes.TabIndex = 5;
            this.ltxtMinutes.Text = "Minutes";
            // 
            // lblEndET
            // 
            this.lblEndET.BackColor = System.Drawing.Color.White;
            this.lblEndET.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEndET.ForeColor = System.Drawing.Color.Red;
            this.lblEndET.Location = new System.Drawing.Point(352, 118);
            this.lblEndET.Name = "lblEndET";
            this.lblEndET.ReadOnly = true;
            this.lblEndET.Size = new System.Drawing.Size(60, 29);
            this.lblEndET.TabIndex = 1;
            this.lblEndET.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lblBeginET
            // 
            this.lblBeginET.BackColor = System.Drawing.Color.White;
            this.lblBeginET.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBeginET.ForeColor = System.Drawing.Color.Red;
            this.lblBeginET.Location = new System.Drawing.Point(242, 118);
            this.lblBeginET.Name = "lblBeginET";
            this.lblBeginET.ReadOnly = true;
            this.lblBeginET.Size = new System.Drawing.Size(73, 29);
            this.lblBeginET.TabIndex = 1;
            this.lblBeginET.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lblOrderTotal
            // 
            this.lblOrderTotal.BackColor = System.Drawing.Color.White;
            this.tlpRecap.SetColumnSpan(this.lblOrderTotal, 4);
            this.lblOrderTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOrderTotal.Location = new System.Drawing.Point(242, 83);
            this.lblOrderTotal.Name = "lblOrderTotal";
            this.lblOrderTotal.ReadOnly = true;
            this.lblOrderTotal.Size = new System.Drawing.Size(225, 29);
            this.lblOrderTotal.TabIndex = 1;
            this.lblOrderTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tdbdDelayedOrderDate
            // 
            this.tdbdDelayedOrderDate.BackColor = System.Drawing.Color.White;
            this.tlpRecap.SetColumnSpan(this.tdbdDelayedOrderDate, 2);
            this.tdbdDelayedOrderDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tdbdDelayedOrderDate.ForeColor = System.Drawing.Color.Red;
            this.tdbdDelayedOrderDate.Location = new System.Drawing.Point(352, 48);
            this.tdbdDelayedOrderDate.Name = "tdbdDelayedOrderDate";
            this.tdbdDelayedOrderDate.ReadOnly = true;
            this.tdbdDelayedOrderDate.Size = new System.Drawing.Size(115, 29);
            this.tdbdDelayedOrderDate.TabIndex = 1;
            this.tdbdDelayedOrderDate.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tdbtDelayedOrderTime
            // 
            this.tdbtDelayedOrderTime.BackColor = System.Drawing.Color.White;
            this.tlpRecap.SetColumnSpan(this.tdbtDelayedOrderTime, 2);
            this.tdbtDelayedOrderTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tdbtDelayedOrderTime.ForeColor = System.Drawing.Color.Red;
            this.tdbtDelayedOrderTime.Location = new System.Drawing.Point(242, 48);
            this.tdbtDelayedOrderTime.Name = "tdbtDelayedOrderTime";
            this.tdbtDelayedOrderTime.ReadOnly = true;
            this.tdbtDelayedOrderTime.Size = new System.Drawing.Size(104, 29);
            this.tdbtDelayedOrderTime.TabIndex = 1;
            this.tdbtDelayedOrderTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tdbtOrderTime
            // 
            this.tdbtOrderTime.BackColor = System.Drawing.Color.White;
            this.tlpRecap.SetColumnSpan(this.tdbtOrderTime, 4);
            this.tdbtOrderTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tdbtOrderTime.ForeColor = System.Drawing.Color.Red;
            this.tdbtOrderTime.Location = new System.Drawing.Point(242, 13);
            this.tdbtOrderTime.Name = "tdbtOrderTime";
            this.tdbtOrderTime.ReadOnly = true;
            this.tdbtOrderTime.Size = new System.Drawing.Size(225, 29);
            this.tdbtOrderTime.TabIndex = 1;
            this.tdbtOrderTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lblEstimatedTime
            // 
            this.lblEstimatedTime.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblEstimatedTime.AutoSize = true;
            this.lblEstimatedTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEstimatedTime.Location = new System.Drawing.Point(58, 122);
            this.lblEstimatedTime.Name = "lblEstimatedTime";
            this.lblEstimatedTime.Size = new System.Drawing.Size(178, 20);
            this.lblEstimatedTime.TabIndex = 0;
            this.lblEstimatedTime.Text = "Estimated Delivery Time";
            // 
            // ltxtOrderTotal
            // 
            this.ltxtOrderTotal.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.ltxtOrderTotal.AutoSize = true;
            this.ltxtOrderTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ltxtOrderTotal.Location = new System.Drawing.Point(144, 87);
            this.ltxtOrderTotal.Name = "ltxtOrderTotal";
            this.ltxtOrderTotal.Size = new System.Drawing.Size(92, 20);
            this.ltxtOrderTotal.TabIndex = 0;
            this.ltxtOrderTotal.Text = "Order Total:";
            // 
            // ltxtDelayedTimeDate
            // 
            this.ltxtDelayedTimeDate.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.ltxtDelayedTimeDate.AutoSize = true;
            this.ltxtDelayedTimeDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ltxtDelayedTimeDate.Location = new System.Drawing.Point(57, 52);
            this.ltxtDelayedTimeDate.Name = "ltxtDelayedTimeDate";
            this.ltxtDelayedTimeDate.Size = new System.Drawing.Size(179, 20);
            this.ltxtDelayedTimeDate.TabIndex = 0;
            this.ltxtDelayedTimeDate.Text = "Delayed Time and Date:";
            // 
            // ltxtCurrentTime
            // 
            this.ltxtCurrentTime.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.ltxtCurrentTime.AutoSize = true;
            this.ltxtCurrentTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ltxtCurrentTime.Location = new System.Drawing.Point(132, 17);
            this.ltxtCurrentTime.Name = "ltxtCurrentTime";
            this.ltxtCurrentTime.Size = new System.Drawing.Size(104, 20);
            this.ltxtCurrentTime.TabIndex = 0;
            this.ltxtCurrentTime.Text = "Current Time:";
            // 
            // lblEstimatedTime2
            // 
            this.lblEstimatedTime2.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblEstimatedTime2.AutoSize = true;
            this.lblEstimatedTime2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEstimatedTime2.Location = new System.Drawing.Point(58, 157);
            this.lblEstimatedTime2.Name = "lblEstimatedTime2";
            this.lblEstimatedTime2.Size = new System.Drawing.Size(178, 20);
            this.lblEstimatedTime2.TabIndex = 2;
            this.lblEstimatedTime2.Text = "Estimated Delivery Time";
            this.lblEstimatedTime2.Visible = false;
            // 
            // lblBeginET2
            // 
            this.lblBeginET2.BackColor = System.Drawing.Color.White;
            this.lblBeginET2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBeginET2.ForeColor = System.Drawing.Color.Red;
            this.lblBeginET2.Location = new System.Drawing.Point(242, 153);
            this.lblBeginET2.Name = "lblBeginET2";
            this.lblBeginET2.ReadOnly = true;
            this.lblBeginET2.Size = new System.Drawing.Size(73, 29);
            this.lblBeginET2.TabIndex = 3;
            this.lblBeginET2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.lblBeginET2.Visible = false;
            // 
            // lblEndET2
            // 
            this.lblEndET2.BackColor = System.Drawing.Color.White;
            this.lblEndET2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEndET2.ForeColor = System.Drawing.Color.Red;
            this.lblEndET2.Location = new System.Drawing.Point(352, 153);
            this.lblEndET2.Name = "lblEndET2";
            this.lblEndET2.ReadOnly = true;
            this.lblEndET2.Size = new System.Drawing.Size(60, 29);
            this.lblEndET2.TabIndex = 3;
            this.lblEndET2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.lblEndET2.Visible = false;
            // 
            // ltxtMinutes2
            // 
            this.ltxtMinutes2.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.ltxtMinutes2.AutoSize = true;
            this.ltxtMinutes2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ltxtMinutes2.Location = new System.Drawing.Point(418, 157);
            this.ltxtMinutes2.Name = "ltxtMinutes2";
            this.ltxtMinutes2.Size = new System.Drawing.Size(65, 20);
            this.ltxtMinutes2.TabIndex = 5;
            this.ltxtMinutes2.Text = "Minutes";
            this.ltxtMinutes2.Visible = false;
            // 
            // tlpRecap
            // 
            this.tlpRecap.ColumnCount = 6;
            this.tlpRecap.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tlpRecap.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 229F));
            this.tlpRecap.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tlpRecap.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tlpRecap.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 66F));
            this.tlpRecap.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 91F));
            this.tlpRecap.Controls.Add(this.ltxtMinutes2, 5, 5);
            this.tlpRecap.Controls.Add(this.ltxtCurrentTime, 1, 1);
            this.tlpRecap.Controls.Add(this.ltxtMinutes, 5, 4);
            this.tlpRecap.Controls.Add(this.lblEndET2, 4, 5);
            this.tlpRecap.Controls.Add(this.tdbtOrderTime, 2, 1);
            this.tlpRecap.Controls.Add(this.lblBeginET2, 2, 5);
            this.tlpRecap.Controls.Add(this.ltxtDelayedTimeDate, 1, 2);
            this.tlpRecap.Controls.Add(this.ltxtOrderTotal, 1, 3);
            this.tlpRecap.Controls.Add(this.lblEstimatedTime, 1, 4);
            this.tlpRecap.Controls.Add(this.lblEndET, 4, 4);
            this.tlpRecap.Controls.Add(this.lblEstimatedTime2, 1, 5);
            this.tlpRecap.Controls.Add(this.lblBeginET, 2, 4);
            this.tlpRecap.Controls.Add(this.tdbtDelayedOrderTime, 2, 2);
            this.tlpRecap.Controls.Add(this.lblOrderTotal, 2, 3);
            this.tlpRecap.Controls.Add(this.tdbdDelayedOrderDate, 4, 2);
            this.tlpRecap.Controls.Add(this.lblDash2, 3, 5);
            this.tlpRecap.Controls.Add(this.cmdClose, 2, 6);
            this.tlpRecap.Controls.Add(this.lblDash, 3, 4);
            this.tlpRecap.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpRecap.Location = new System.Drawing.Point(0, 0);
            this.tlpRecap.Name = "tlpRecap";
            this.tlpRecap.RowCount = 7;
            this.tlpRecap.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tlpRecap.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tlpRecap.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tlpRecap.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tlpRecap.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tlpRecap.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tlpRecap.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpRecap.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpRecap.Size = new System.Drawing.Size(506, 259);
            this.tlpRecap.TabIndex = 1;
            this.tlpRecap.Paint += new System.Windows.Forms.PaintEventHandler(this.tlpRecap_Paint);
            // 
            // lblDash2
            // 
            this.lblDash2.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblDash2.AutoSize = true;
            this.lblDash2.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDash2.Location = new System.Drawing.Point(327, 158);
            this.lblDash2.Margin = new System.Windows.Forms.Padding(8, 0, 3, 0);
            this.lblDash2.Name = "lblDash2";
            this.lblDash2.Size = new System.Drawing.Size(16, 18);
            this.lblDash2.TabIndex = 5;
            this.lblDash2.Text = "-";
            this.lblDash2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblDash2.Visible = false;
            // 
            // cmdClose
            // 
            this.cmdClose.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.cmdClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.cmdClose.Image = ((System.Drawing.Image)(resources.GetObject("cmdClose.Image")));
            this.cmdClose.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.cmdClose.Location = new System.Drawing.Point(245, 195);
            this.cmdClose.Margin = new System.Windows.Forms.Padding(3, 10, 3, 3);
            this.cmdClose.Name = "cmdClose";
            this.cmdClose.Size = new System.Drawing.Size(68, 55);
            this.cmdClose.TabIndex = 4;
            this.cmdClose.Text = "Close";
            this.cmdClose.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.cmdClose.UseVisualStyleBackColor = true;
            this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
            // 
            // lblDash
            // 
            this.lblDash.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblDash.AutoSize = true;
            this.lblDash.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDash.Location = new System.Drawing.Point(327, 123);
            this.lblDash.Margin = new System.Windows.Forms.Padding(8, 0, 3, 0);
            this.lblDash.Name = "lblDash";
            this.lblDash.Size = new System.Drawing.Size(16, 18);
            this.lblDash.TabIndex = 5;
            this.lblDash.Text = "-";
            this.lblDash.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // frmRecap
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(506, 259);
            this.ControlBox = false;
            this.Controls.Add(this.tlpRecap);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "frmRecap";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmRecap_FormClosing);
            this.Load += new System.EventHandler(this.frm_Recap_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmRecap_KeyDown);
            this.tlpRecap.ResumeLayout(false);
            this.tlpRecap.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TextBox tdbtOrderTime;
        private System.Windows.Forms.Label lblEstimatedTime;
        private System.Windows.Forms.Label ltxtOrderTotal;
        private System.Windows.Forms.Label ltxtDelayedTimeDate;
        private System.Windows.Forms.Label ltxtCurrentTime;
        private System.Windows.Forms.TextBox lblBeginET;
        private System.Windows.Forms.TextBox lblOrderTotal;
        private System.Windows.Forms.TextBox tdbtDelayedOrderTime;
        private System.Windows.Forms.Button cmdClose;
        private System.Windows.Forms.TextBox lblEndET;
        private System.Windows.Forms.TextBox tdbdDelayedOrderDate;
        private System.Windows.Forms.Label ltxtMinutes;
        private System.Windows.Forms.Label ltxtMinutes2;
        private System.Windows.Forms.TextBox lblEndET2;
        private System.Windows.Forms.TextBox lblBeginET2;
        private System.Windows.Forms.Label lblEstimatedTime2;
        private System.Windows.Forms.TableLayoutPanel tlpRecap;
        private System.Windows.Forms.Label lblDash;
        private System.Windows.Forms.Label lblDash2;
    }
}