namespace JublFood.POS.App
{
    partial class frmReason
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmReason));
            this.pnl_Reason = new System.Windows.Forms.Panel();
            this.pnl_badOrder = new System.Windows.Forms.Panel();
            this.lblWhere = new System.Windows.Forms.Label();
            this.ltxtOrderNumber = new System.Windows.Forms.Label();
            this.ltxtCurrentEmployeeID = new System.Windows.Forms.Label();
            this.lblCurrentEmployeeID = new System.Windows.Forms.Label();
            this.tdbnOrderNumber = new System.Windows.Forms.TextBox();
            this.lblCurrentName = new System.Windows.Forms.Label();
            this.flowLayout_reason = new System.Windows.Forms.FlowLayoutPanel();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.cmdOK = new System.Windows.Forms.Button();
            this.cmdKeyBoard = new System.Windows.Forms.Button();
            this.cmdClear = new System.Windows.Forms.Button();
            this.txtOtherInfo = new System.Windows.Forms.TextBox();
            this.ltxtOtherInfo = new System.Windows.Forms.Label();
            this.ltxtReason = new System.Windows.Forms.Label();
            this.pnl_Reason.SuspendLayout();
            this.pnl_badOrder.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnl_Reason
            // 
            this.pnl_Reason.BackColor = System.Drawing.Color.Teal;
            this.pnl_Reason.Controls.Add(this.pnl_badOrder);
            this.pnl_Reason.Controls.Add(this.flowLayout_reason);
            this.pnl_Reason.Controls.Add(this.cmdCancel);
            this.pnl_Reason.Controls.Add(this.cmdOK);
            this.pnl_Reason.Controls.Add(this.cmdKeyBoard);
            this.pnl_Reason.Controls.Add(this.cmdClear);
            this.pnl_Reason.Controls.Add(this.txtOtherInfo);
            this.pnl_Reason.Controls.Add(this.ltxtOtherInfo);
            this.pnl_Reason.Controls.Add(this.ltxtReason);
            this.pnl_Reason.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl_Reason.Location = new System.Drawing.Point(0, 0);
            this.pnl_Reason.Name = "pnl_Reason";
            this.pnl_Reason.Size = new System.Drawing.Size(524, 408);
            this.pnl_Reason.TabIndex = 0;
            // 
            // pnl_badOrder
            // 
            this.pnl_badOrder.Controls.Add(this.lblWhere);
            this.pnl_badOrder.Controls.Add(this.ltxtOrderNumber);
            this.pnl_badOrder.Controls.Add(this.ltxtCurrentEmployeeID);
            this.pnl_badOrder.Controls.Add(this.lblCurrentEmployeeID);
            this.pnl_badOrder.Controls.Add(this.tdbnOrderNumber);
            this.pnl_badOrder.Controls.Add(this.lblCurrentName);
            this.pnl_badOrder.Location = new System.Drawing.Point(11, 26);
            this.pnl_badOrder.Name = "pnl_badOrder";
            this.pnl_badOrder.Size = new System.Drawing.Size(507, 37);
            this.pnl_badOrder.TabIndex = 0;
            this.pnl_badOrder.Visible = false;
            // 
            // lblWhere
            // 
            this.lblWhere.AutoSize = true;
            this.lblWhere.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWhere.ForeColor = System.Drawing.Color.White;
            this.lblWhere.Location = new System.Drawing.Point(16, 0);
            this.lblWhere.Name = "lblWhere";
            this.lblWhere.Size = new System.Drawing.Size(57, 20);
            this.lblWhere.TabIndex = 1;
            this.lblWhere.Text = "where";
            // 
            // ltxtOrderNumber
            // 
            this.ltxtOrderNumber.AutoSize = true;
            this.ltxtOrderNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ltxtOrderNumber.ForeColor = System.Drawing.Color.White;
            this.ltxtOrderNumber.Location = new System.Drawing.Point(16, 20);
            this.ltxtOrderNumber.Name = "ltxtOrderNumber";
            this.ltxtOrderNumber.Size = new System.Drawing.Size(121, 20);
            this.ltxtOrderNumber.TabIndex = 1;
            this.ltxtOrderNumber.Text = "Order Number";
            // 
            // ltxtCurrentEmployeeID
            // 
            this.ltxtCurrentEmployeeID.AutoSize = true;
            this.ltxtCurrentEmployeeID.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ltxtCurrentEmployeeID.ForeColor = System.Drawing.Color.White;
            this.ltxtCurrentEmployeeID.Location = new System.Drawing.Point(16, 43);
            this.ltxtCurrentEmployeeID.Name = "ltxtCurrentEmployeeID";
            this.ltxtCurrentEmployeeID.Size = new System.Drawing.Size(136, 20);
            this.ltxtCurrentEmployeeID.TabIndex = 1;
            this.ltxtCurrentEmployeeID.Text = "Current Employee";
            // 
            // lblCurrentEmployeeID
            // 
            this.lblCurrentEmployeeID.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCurrentEmployeeID.ForeColor = System.Drawing.Color.White;
            this.lblCurrentEmployeeID.Location = new System.Drawing.Point(197, 47);
            this.lblCurrentEmployeeID.Name = "lblCurrentEmployeeID";
            this.lblCurrentEmployeeID.Size = new System.Drawing.Size(136, 19);
            this.lblCurrentEmployeeID.TabIndex = 1;
            // 
            // tdbnOrderNumber
            // 
            this.tdbnOrderNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tdbnOrderNumber.Location = new System.Drawing.Point(194, 20);
            this.tdbnOrderNumber.Name = "tdbnOrderNumber";
            this.tdbnOrderNumber.Size = new System.Drawing.Size(153, 26);
            this.tdbnOrderNumber.TabIndex = 2;
            // 
            // lblCurrentName
            // 
            this.lblCurrentName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCurrentName.ForeColor = System.Drawing.Color.White;
            this.lblCurrentName.Location = new System.Drawing.Point(356, 43);
            this.lblCurrentName.Name = "lblCurrentName";
            this.lblCurrentName.Size = new System.Drawing.Size(134, 20);
            this.lblCurrentName.TabIndex = 1;
            // 
            // flowLayout_reason
            // 
            this.flowLayout_reason.AutoScroll = true;
            this.flowLayout_reason.Location = new System.Drawing.Point(5, 68);
            this.flowLayout_reason.Name = "flowLayout_reason";
            this.flowLayout_reason.Size = new System.Drawing.Size(515, 252);
            this.flowLayout_reason.TabIndex = 4;
            // 
            // cmdCancel
            // 
            this.cmdCancel.Image = ((System.Drawing.Image)(resources.GetObject("cmdCancel.Image")));
            this.cmdCancel.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.cmdCancel.Location = new System.Drawing.Point(453, 347);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(68, 55);
            this.cmdCancel.TabIndex = 3;
            this.cmdCancel.Text = "Cancel";
            this.cmdCancel.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.cmdCancel.UseVisualStyleBackColor = true;
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // cmdOK
            // 
            this.cmdOK.Image = ((System.Drawing.Image)(resources.GetObject("cmdOK.Image")));
            this.cmdOK.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.cmdOK.Location = new System.Drawing.Point(385, 347);
            this.cmdOK.Name = "cmdOK";
            this.cmdOK.Size = new System.Drawing.Size(68, 55);
            this.cmdOK.TabIndex = 3;
            this.cmdOK.Text = "OK";
            this.cmdOK.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.cmdOK.UseVisualStyleBackColor = true;
            this.cmdOK.Click += new System.EventHandler(this.cmdOK_Click);
            // 
            // cmdKeyBoard
            // 
            this.cmdKeyBoard.Image = ((System.Drawing.Image)(resources.GetObject("cmdKeyBoard.Image")));
            this.cmdKeyBoard.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.cmdKeyBoard.Location = new System.Drawing.Point(317, 347);
            this.cmdKeyBoard.Name = "cmdKeyBoard";
            this.cmdKeyBoard.Size = new System.Drawing.Size(68, 55);
            this.cmdKeyBoard.TabIndex = 3;
            this.cmdKeyBoard.Text = "Keyboard";
            this.cmdKeyBoard.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.cmdKeyBoard.UseVisualStyleBackColor = true;
            this.cmdKeyBoard.Click += new System.EventHandler(this.cmdKeyBoard_Click);
            // 
            // cmdClear
            // 
            this.cmdClear.Image = ((System.Drawing.Image)(resources.GetObject("cmdClear.Image")));
            this.cmdClear.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.cmdClear.Location = new System.Drawing.Point(249, 347);
            this.cmdClear.Name = "cmdClear";
            this.cmdClear.Size = new System.Drawing.Size(68, 55);
            this.cmdClear.TabIndex = 3;
            this.cmdClear.Text = "Clear";
            this.cmdClear.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.cmdClear.UseVisualStyleBackColor = true;
            this.cmdClear.Click += new System.EventHandler(this.cmdClear_Click);
            // 
            // txtOtherInfo
            // 
            this.txtOtherInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtOtherInfo.Location = new System.Drawing.Point(5, 347);
            this.txtOtherInfo.Multiline = true;
            this.txtOtherInfo.Name = "txtOtherInfo";
            this.txtOtherInfo.Size = new System.Drawing.Size(238, 53);
            this.txtOtherInfo.TabIndex = 2;
            // 
            // ltxtOtherInfo
            // 
            this.ltxtOtherInfo.AutoSize = true;
            this.ltxtOtherInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ltxtOtherInfo.ForeColor = System.Drawing.Color.White;
            this.ltxtOtherInfo.Location = new System.Drawing.Point(3, 323);
            this.ltxtOtherInfo.Name = "ltxtOtherInfo";
            this.ltxtOtherInfo.Size = new System.Drawing.Size(156, 20);
            this.ltxtOtherInfo.TabIndex = 1;
            this.ltxtOtherInfo.Text = "Other Information:";
            // 
            // ltxtReason
            // 
            this.ltxtReason.AutoSize = true;
            this.ltxtReason.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ltxtReason.ForeColor = System.Drawing.Color.White;
            this.ltxtReason.Location = new System.Drawing.Point(14, 5);
            this.ltxtReason.Name = "ltxtReason";
            this.ltxtReason.Size = new System.Drawing.Size(57, 20);
            this.ltxtReason.TabIndex = 1;
            this.ltxtReason.Text = "where";
            // 
            // frmReason
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(524, 408);
            this.ControlBox = false;
            this.Controls.Add(this.pnl_Reason);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "frmReason";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.pnl_Reason.ResumeLayout(false);
            this.pnl_Reason.PerformLayout();
            this.pnl_badOrder.ResumeLayout(false);
            this.pnl_badOrder.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnl_Reason;
        private System.Windows.Forms.FlowLayoutPanel flowLayout_reason;
        private System.Windows.Forms.Button cmdCancel;
        private System.Windows.Forms.Button cmdOK;
        private System.Windows.Forms.Button cmdKeyBoard;
        private System.Windows.Forms.Button cmdClear;
        private System.Windows.Forms.TextBox txtOtherInfo;
        private System.Windows.Forms.TextBox tdbnOrderNumber;
        private System.Windows.Forms.Label ltxtCurrentEmployeeID;
        private System.Windows.Forms.Label ltxtOtherInfo;
        private System.Windows.Forms.Label lblWhere;
        private System.Windows.Forms.Label ltxtOrderNumber;
        private System.Windows.Forms.Label lblCurrentName;
        private System.Windows.Forms.Label lblCurrentEmployeeID;
        private System.Windows.Forms.Label ltxtReason;
        private System.Windows.Forms.Panel pnl_badOrder;
    }
}