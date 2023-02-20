namespace JublFood.POS.App
{
    partial class frmModify
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.pnl_modifyOrder = new System.Windows.Forms.Panel();
            this.tdbnNumber = new System.Windows.Forms.TextBox();
            this.lblTitle = new System.Windows.Forms.Label();
            this.cmdDown = new System.Windows.Forms.Button();
            this.cmdUp = new System.Windows.Forms.Button();
            this.cmdRight = new System.Windows.Forms.Button();
            this.cmdLeft = new System.Windows.Forms.Button();
            this.pnl_Order = new System.Windows.Forms.Panel();
            this.gridmodify = new System.Windows.Forms.DataGridView();
            this.pnlFilter = new System.Windows.Forms.Panel();
            this.txtFilter = new System.Windows.Forms.TextBox();
            this.ltxtFilterCriteria = new System.Windows.Forms.Label();
            this.cmdLike = new System.Windows.Forms.Button();
            this.cmdExact = new System.Windows.Forms.Button();
            this.cmdKeyboard = new System.Windows.Forms.Button();
            this.cmdClear = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.cmdPrintOnDemand = new System.Windows.Forms.Button();
            this.cmdStatusFilter = new System.Windows.Forms.Button();
            this.cmdDayFilter = new System.Windows.Forms.Button();
            this.cmdPay = new System.Windows.Forms.Button();
            this.cmdCloseOrder = new System.Windows.Forms.Button();
            this.uC_CustomerOrderBottomMenu = new JublFood.POS.App.UC_CustomerOrderBottomMenu();
            this.cmdTillStatus = new System.Windows.Forms.Button();
            this.cmdTillChange = new System.Windows.Forms.Button();
            this.cmdNoSale = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.tmrRefresh = new System.Windows.Forms.Timer(this.components);
            this.uC_InformationList1 = new JublFood.POS.App.UC_InformationList();
            this.uC_FunctionList1 = new JublFood.POS.App.UC_FunctionList();
            this.uC_Customer_order_Header1 = new JublFood.POS.App.UserControls.UC_Customer_order_Header();
            this.uC_Customer_OrderMenu1 = new JublFood.POS.App.UC_Customer_OrderMenu();
            this.pnl_modifyOrder.SuspendLayout();
            this.pnl_Order.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridmodify)).BeginInit();
            this.pnlFilter.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnl_modifyOrder
            // 
            this.pnl_modifyOrder.BackColor = System.Drawing.Color.WhiteSmoke;
            this.pnl_modifyOrder.Controls.Add(this.tdbnNumber);
            this.pnl_modifyOrder.Controls.Add(this.lblTitle);
            this.pnl_modifyOrder.Controls.Add(this.cmdDown);
            this.pnl_modifyOrder.Controls.Add(this.cmdUp);
            this.pnl_modifyOrder.Controls.Add(this.cmdRight);
            this.pnl_modifyOrder.Controls.Add(this.cmdLeft);
            this.pnl_modifyOrder.Controls.Add(this.pnl_Order);
            this.pnl_modifyOrder.Controls.Add(this.pnlFilter);
            this.pnl_modifyOrder.Controls.Add(this.panel2);
            this.pnl_modifyOrder.Location = new System.Drawing.Point(1, 117);
            this.pnl_modifyOrder.Name = "pnl_modifyOrder";
            this.pnl_modifyOrder.Size = new System.Drawing.Size(793, 483);
            this.pnl_modifyOrder.TabIndex = 0;
            // 
            // tdbnNumber
            // 
            this.tdbnNumber.Location = new System.Drawing.Point(6, 9);
            this.tdbnNumber.Name = "tdbnNumber";
            this.tdbnNumber.Size = new System.Drawing.Size(100, 20);
            this.tdbnNumber.TabIndex = 1;
            this.tdbnNumber.Visible = false;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.Location = new System.Drawing.Point(292, 6);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(175, 20);
            this.lblTitle.TabIndex = 44;
            this.lblTitle.Text = "Today - Open Orders";
            // 
            // cmdDown
            // 
            this.cmdDown.Image = global::JublFood.POS.App.Properties.Resources._34;
            this.cmdDown.Location = new System.Drawing.Point(745, 263);
            this.cmdDown.Name = "cmdDown";
            this.cmdDown.Size = new System.Drawing.Size(43, 42);
            this.cmdDown.TabIndex = 43;
            this.cmdDown.UseVisualStyleBackColor = true;
            this.cmdDown.Click += new System.EventHandler(this.cmdDown_Click);
            // 
            // cmdUp
            // 
            this.cmdUp.Image = global::JublFood.POS.App.Properties.Resources._31;
            this.cmdUp.Location = new System.Drawing.Point(745, 35);
            this.cmdUp.Name = "cmdUp";
            this.cmdUp.Size = new System.Drawing.Size(43, 42);
            this.cmdUp.TabIndex = 42;
            this.cmdUp.UseVisualStyleBackColor = true;
            this.cmdUp.Click += new System.EventHandler(this.cmdUp_Click);
            // 
            // cmdRight
            // 
            this.cmdRight.Image = global::JublFood.POS.App.Properties.Resources._33;
            this.cmdRight.Location = new System.Drawing.Point(702, 321);
            this.cmdRight.Name = "cmdRight";
            this.cmdRight.Size = new System.Drawing.Size(43, 31);
            this.cmdRight.TabIndex = 41;
            this.cmdRight.UseVisualStyleBackColor = true;
            this.cmdRight.Click += new System.EventHandler(this.cmdRight_Click);
            // 
            // cmdLeft
            // 
            this.cmdLeft.Image = global::JublFood.POS.App.Properties.Resources._32;
            this.cmdLeft.Location = new System.Drawing.Point(6, 321);
            this.cmdLeft.Name = "cmdLeft";
            this.cmdLeft.Size = new System.Drawing.Size(43, 31);
            this.cmdLeft.TabIndex = 40;
            this.cmdLeft.UseVisualStyleBackColor = true;
            this.cmdLeft.Click += new System.EventHandler(this.cmdLeft_Click);
            // 
            // pnl_Order
            // 
            this.pnl_Order.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnl_Order.Controls.Add(this.gridmodify);
            this.pnl_Order.Location = new System.Drawing.Point(7, 35);
            this.pnl_Order.Name = "pnl_Order";
            this.pnl_Order.Size = new System.Drawing.Size(739, 283);
            this.pnl_Order.TabIndex = 39;
            // 
            // gridmodify
            // 
            this.gridmodify.AllowUserToAddRows = false;
            this.gridmodify.AllowUserToResizeColumns = false;
            this.gridmodify.AllowUserToResizeRows = false;
            this.gridmodify.BackgroundColor = System.Drawing.SystemColors.Info;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridmodify.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.gridmodify.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridmodify.DefaultCellStyle = dataGridViewCellStyle2;
            this.gridmodify.Location = new System.Drawing.Point(3, 1);
            this.gridmodify.Name = "gridmodify";
            this.gridmodify.ReadOnly = true;
            this.gridmodify.RowHeadersVisible = false;
            this.gridmodify.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.gridmodify.Size = new System.Drawing.Size(730, 276);
            this.gridmodify.TabIndex = 1;
            this.gridmodify.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridmodify_CellClick);
            this.gridmodify.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.gridmodify_DataBindingComplete);
            // 
            // pnlFilter
            // 
            this.pnlFilter.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlFilter.Controls.Add(this.txtFilter);
            this.pnlFilter.Controls.Add(this.ltxtFilterCriteria);
            this.pnlFilter.Controls.Add(this.cmdLike);
            this.pnlFilter.Controls.Add(this.cmdExact);
            this.pnlFilter.Controls.Add(this.cmdKeyboard);
            this.pnlFilter.Controls.Add(this.cmdClear);
            this.pnlFilter.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlFilter.Location = new System.Drawing.Point(496, 353);
            this.pnlFilter.Name = "pnlFilter";
            this.pnlFilter.Size = new System.Drawing.Size(293, 119);
            this.pnlFilter.TabIndex = 38;
            // 
            // txtFilter
            // 
            this.txtFilter.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFilter.Location = new System.Drawing.Point(133, 12);
            this.txtFilter.Name = "txtFilter";
            this.txtFilter.Size = new System.Drawing.Size(153, 26);
            this.txtFilter.TabIndex = 46;
            // 
            // ltxtFilterCriteria
            // 
            this.ltxtFilterCriteria.AutoSize = true;
            this.ltxtFilterCriteria.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ltxtFilterCriteria.Location = new System.Drawing.Point(67, 20);
            this.ltxtFilterCriteria.Name = "ltxtFilterCriteria";
            this.ltxtFilterCriteria.Size = new System.Drawing.Size(47, 13);
            this.ltxtFilterCriteria.TabIndex = 45;
            this.ltxtFilterCriteria.Text = "Search";
            // 
            // cmdLike
            // 
            this.cmdLike.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdLike.Image = global::JublFood.POS.App.Properties.Resources._55;
            this.cmdLike.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.cmdLike.Location = new System.Drawing.Point(216, 58);
            this.cmdLike.Name = "cmdLike";
            this.cmdLike.Size = new System.Drawing.Size(68, 56);
            this.cmdLike.TabIndex = 44;
            this.cmdLike.Text = "Like";
            this.cmdLike.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.cmdLike.UseVisualStyleBackColor = true;
            this.cmdLike.Click += new System.EventHandler(this.cmdLike_Click);
            // 
            // cmdExact
            // 
            this.cmdExact.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdExact.Image = global::JublFood.POS.App.Properties.Resources._52;
            this.cmdExact.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.cmdExact.Location = new System.Drawing.Point(144, 58);
            this.cmdExact.Name = "cmdExact";
            this.cmdExact.Size = new System.Drawing.Size(68, 56);
            this.cmdExact.TabIndex = 43;
            this.cmdExact.Text = "Exact";
            this.cmdExact.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.cmdExact.UseVisualStyleBackColor = true;
            this.cmdExact.Click += new System.EventHandler(this.cmdExact_Click);
            // 
            // cmdKeyboard
            // 
            this.cmdKeyboard.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdKeyboard.Image = global::JublFood.POS.App.Properties.Resources._42;
            this.cmdKeyboard.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.cmdKeyboard.Location = new System.Drawing.Point(73, 58);
            this.cmdKeyboard.Name = "cmdKeyboard";
            this.cmdKeyboard.Size = new System.Drawing.Size(68, 56);
            this.cmdKeyboard.TabIndex = 42;
            this.cmdKeyboard.Text = "Keyboard";
            this.cmdKeyboard.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.cmdKeyboard.UseVisualStyleBackColor = true;
            this.cmdKeyboard.Click += new System.EventHandler(this.cmdKeyboard_Click);
            // 
            // cmdClear
            // 
            this.cmdClear.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdClear.Image = global::JublFood.POS.App.Properties.Resources._75;
            this.cmdClear.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.cmdClear.Location = new System.Drawing.Point(2, 59);
            this.cmdClear.Name = "cmdClear";
            this.cmdClear.Size = new System.Drawing.Size(68, 56);
            this.cmdClear.TabIndex = 41;
            this.cmdClear.Text = "Clear";
            this.cmdClear.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.cmdClear.UseVisualStyleBackColor = true;
            this.cmdClear.Click += new System.EventHandler(this.cmdClear_Click);
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel2.Controls.Add(this.cmdPrintOnDemand);
            this.panel2.Controls.Add(this.cmdStatusFilter);
            this.panel2.Controls.Add(this.cmdDayFilter);
            this.panel2.Controls.Add(this.cmdPay);
            this.panel2.Controls.Add(this.cmdCloseOrder);
            this.panel2.Controls.Add(this.uC_CustomerOrderBottomMenu);
            this.panel2.Controls.Add(this.cmdTillStatus);
            this.panel2.Controls.Add(this.cmdTillChange);
            this.panel2.Controls.Add(this.cmdNoSale);
            this.panel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel2.Location = new System.Drawing.Point(5, 353);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(487, 125);
            this.panel2.TabIndex = 37;
            // 
            // cmdPrintOnDemand
            // 
            this.cmdPrintOnDemand.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdPrintOnDemand.Image = global::JublFood.POS.App.Properties.Resources._25;
            this.cmdPrintOnDemand.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.cmdPrintOnDemand.Location = new System.Drawing.Point(268, 4);
            this.cmdPrintOnDemand.Name = "cmdPrintOnDemand";
            this.cmdPrintOnDemand.Size = new System.Drawing.Size(68, 56);
            this.cmdPrintOnDemand.TabIndex = 41;
            this.cmdPrintOnDemand.Text = "Print Extra";
            this.cmdPrintOnDemand.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.cmdPrintOnDemand.UseVisualStyleBackColor = true;
            this.cmdPrintOnDemand.Click += new System.EventHandler(this.cmdPrintOnDemand_Click);
            // 
            // cmdStatusFilter
            // 
            this.cmdStatusFilter.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdStatusFilter.Image = global::JublFood.POS.App.Properties.Resources._43;
            this.cmdStatusFilter.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.cmdStatusFilter.Location = new System.Drawing.Point(409, 60);
            this.cmdStatusFilter.Name = "cmdStatusFilter";
            this.cmdStatusFilter.Size = new System.Drawing.Size(68, 56);
            this.cmdStatusFilter.TabIndex = 40;
            this.cmdStatusFilter.Text = "All";
            this.cmdStatusFilter.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.cmdStatusFilter.UseVisualStyleBackColor = true;
            this.cmdStatusFilter.Click += new System.EventHandler(this.cmdStatusFilter_Click);
            // 
            // cmdDayFilter
            // 
            this.cmdDayFilter.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdDayFilter.Image = global::JublFood.POS.App.Properties.Resources._201;
            this.cmdDayFilter.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.cmdDayFilter.Location = new System.Drawing.Point(337, 60);
            this.cmdDayFilter.Name = "cmdDayFilter";
            this.cmdDayFilter.Size = new System.Drawing.Size(68, 56);
            this.cmdDayFilter.TabIndex = 39;
            this.cmdDayFilter.Text = "Future";
            this.cmdDayFilter.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.cmdDayFilter.UseVisualStyleBackColor = true;
            this.cmdDayFilter.Click += new System.EventHandler(this.cmdDayFilter_Click);
            // 
            // cmdPay
            // 
            this.cmdPay.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdPay.Image = global::JublFood.POS.App.Properties.Resources._98;
            this.cmdPay.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.cmdPay.Location = new System.Drawing.Point(409, 3);
            this.cmdPay.Name = "cmdPay";
            this.cmdPay.Size = new System.Drawing.Size(68, 56);
            this.cmdPay.TabIndex = 38;
            this.cmdPay.Text = "Pay";
            this.cmdPay.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.cmdPay.UseVisualStyleBackColor = true;
            this.cmdPay.Click += new System.EventHandler(this.cmdPay_Click);
            // 
            // cmdCloseOrder
            // 
            this.cmdCloseOrder.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdCloseOrder.Image = global::JublFood.POS.App.Properties.Resources._220;
            this.cmdCloseOrder.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.cmdCloseOrder.Location = new System.Drawing.Point(337, 3);
            this.cmdCloseOrder.Name = "cmdCloseOrder";
            this.cmdCloseOrder.Size = new System.Drawing.Size(68, 56);
            this.cmdCloseOrder.TabIndex = 37;
            this.cmdCloseOrder.Text = "Close";
            this.cmdCloseOrder.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.cmdCloseOrder.UseVisualStyleBackColor = true;
            this.cmdCloseOrder.Click += new System.EventHandler(this.cmdCloseOrder_Click);
            // 
            // uC_CustomerOrderBottomMenu
            // 
            this.uC_CustomerOrderBottomMenu.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uC_CustomerOrderBottomMenu.Location = new System.Drawing.Point(0, 59);
            this.uC_CustomerOrderBottomMenu.Name = "uC_CustomerOrderBottomMenu";
            this.uC_CustomerOrderBottomMenu.Size = new System.Drawing.Size(275, 62);
            this.uC_CustomerOrderBottomMenu.TabIndex = 1;
            // 
            // cmdTillStatus
            // 
            this.cmdTillStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdTillStatus.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.cmdTillStatus.Location = new System.Drawing.Point(159, 1);
            this.cmdTillStatus.Name = "cmdTillStatus";
            this.cmdTillStatus.Size = new System.Drawing.Size(68, 56);
            this.cmdTillStatus.TabIndex = 36;
            this.cmdTillStatus.Text = "Till Change";
            this.cmdTillStatus.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.cmdTillStatus.UseVisualStyleBackColor = true;
            this.cmdTillStatus.Visible = false;
            // 
            // cmdTillChange
            // 
            this.cmdTillChange.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdTillChange.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.cmdTillChange.Location = new System.Drawing.Point(81, 1);
            this.cmdTillChange.Name = "cmdTillChange";
            this.cmdTillChange.Size = new System.Drawing.Size(68, 56);
            this.cmdTillChange.TabIndex = 36;
            this.cmdTillChange.Text = "Till Status";
            this.cmdTillChange.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.cmdTillChange.UseVisualStyleBackColor = true;
            this.cmdTillChange.Visible = false;
            // 
            // cmdNoSale
            // 
            this.cmdNoSale.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdNoSale.Image = global::JublFood.POS.App.Properties.Resources._190;
            this.cmdNoSale.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.cmdNoSale.Location = new System.Drawing.Point(5, 1);
            this.cmdNoSale.Name = "cmdNoSale";
            this.cmdNoSale.Size = new System.Drawing.Size(68, 56);
            this.cmdNoSale.TabIndex = 36;
            this.cmdNoSale.Text = "No Sale";
            this.cmdNoSale.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.cmdNoSale.UseVisualStyleBackColor = true;
            this.cmdNoSale.Click += new System.EventHandler(this.cmdNoSale_Click);
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.White;
            this.label3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label3.Location = new System.Drawing.Point(2, 83);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(790, 3);
            this.label3.TabIndex = 49;
            // 
            // uC_InformationList1
            // 
            this.uC_InformationList1.Location = new System.Drawing.Point(454, 85);
            this.uC_InformationList1.Name = "uC_InformationList1";
            this.uC_InformationList1.Size = new System.Drawing.Size(69, 218);
            this.uC_InformationList1.TabIndex = 52;
            this.uC_InformationList1.Visible = false;
            // 
            // uC_FunctionList1
            // 
            this.uC_FunctionList1.AutoScroll = true;
            this.uC_FunctionList1.Location = new System.Drawing.Point(386, 85);
            this.uC_FunctionList1.Name = "uC_FunctionList1";
            this.uC_FunctionList1.Size = new System.Drawing.Size(70, 379);
            this.uC_FunctionList1.TabIndex = 51;
            this.uC_FunctionList1.Visible = false;
            // 
            // uC_Customer_order_Header1
            // 
            this.uC_Customer_order_Header1.LabelOrderTaker = null;
            this.uC_Customer_order_Header1.Location = new System.Drawing.Point(1, 0);
            this.uC_Customer_order_Header1.Name = "uC_Customer_order_Header1";
            this.uC_Customer_order_Header1.Size = new System.Drawing.Size(798, 24);
            this.uC_Customer_order_Header1.TabIndex = 50;
            // 
            // uC_Customer_OrderMenu1
            // 
            this.uC_Customer_OrderMenu1.Location = new System.Drawing.Point(1, 25);
            this.uC_Customer_OrderMenu1.Name = "uC_Customer_OrderMenu1";
            this.uC_Customer_OrderMenu1.Size = new System.Drawing.Size(827, 56);
            this.uC_Customer_OrderMenu1.TabIndex = 1;
            // 
            // frmModify
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Teal;
            this.ClientSize = new System.Drawing.Size(800, 600);
            this.Controls.Add(this.uC_InformationList1);
            this.Controls.Add(this.uC_FunctionList1);
            this.Controls.Add(this.uC_Customer_order_Header1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.uC_Customer_OrderMenu1);
            this.Controls.Add(this.pnl_modifyOrder);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmModify";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Modify";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmModify_FormClosing);
            this.Load += new System.EventHandler(this.frmModify_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmModify_KeyDown);
            this.pnl_modifyOrder.ResumeLayout(false);
            this.pnl_modifyOrder.PerformLayout();
            this.pnl_Order.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridmodify)).EndInit();
            this.pnlFilter.ResumeLayout(false);
            this.pnlFilter.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnl_modifyOrder;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button cmdNoSale;
        private System.Windows.Forms.Panel pnlFilter;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button cmdPay;
        private UC_CustomerOrderBottomMenu uC_CustomerOrderBottomMenu;
        private System.Windows.Forms.Button cmdLike;
        private System.Windows.Forms.Button cmdExact;
        private System.Windows.Forms.Button cmdKeyboard;
        private System.Windows.Forms.Button cmdClear;
        private System.Windows.Forms.Button cmdStatusFilter;
        private System.Windows.Forms.Button cmdDayFilter;
        private System.Windows.Forms.Panel pnl_Order;
        private System.Windows.Forms.Button cmdLeft;
        private System.Windows.Forms.Button cmdDown;
        private System.Windows.Forms.Button cmdUp;
        private System.Windows.Forms.Button cmdRight;
        private System.Windows.Forms.TextBox txtFilter;
        private System.Windows.Forms.Label ltxtFilterCriteria;
        private System.Windows.Forms.Timer tmrRefresh;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Button cmdTillStatus;
        private System.Windows.Forms.Button cmdTillChange;
        private System.Windows.Forms.Button cmdPrintOnDemand;
        private System.Windows.Forms.TextBox tdbnNumber;
        private System.Windows.Forms.Button cmdCloseOrder;
        private UserControls.UC_Customer_order_Header uC_Customer_order_Header1;
        private System.Windows.Forms.DataGridView gridmodify;
        public UC_Customer_OrderMenu uC_Customer_OrderMenu1;
        private UC_FunctionList uC_FunctionList1;
        private UC_InformationList uC_InformationList1;
    }
}