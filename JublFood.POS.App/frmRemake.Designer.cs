namespace JublFood.POS.App
{
    partial class frmRemake
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmRemake));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.pnl_History = new System.Windows.Forms.Panel();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.lblItemCount = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.comReason = new System.Windows.Forms.ComboBox();
            this.btnQty_OK = new System.Windows.Forms.Button();
            this.btn_Down = new System.Windows.Forms.Button();
            this.btn_Up = new System.Windows.Forms.Button();
            this.pnl_Quantity = new System.Windows.Forms.Panel();
            this.btn_Dot = new System.Windows.Forms.Button();
            this.txt_Quantity = new System.Windows.Forms.TextBox();
            this.uC_KeyBoardNumeric = new JublFood.POS.App.UC_KeyBoardNumeric();
            this.btn_Quantity = new System.Windows.Forms.Button();
            this.lblOrderInfo = new System.Windows.Forms.Label();
            this.dgvCart = new System.Windows.Forms.DataGridView();
            this.Qty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Item = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VegNVegColor = new System.Windows.Forms.DataGridViewImageColumn();
            this.Price = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lbltotalAmount = new System.Windows.Forms.Label();
            this.lblTotal = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cmdDown = new System.Windows.Forms.Button();
            this.cmdUp = new System.Windows.Forms.Button();
            this.cmdAdd = new System.Windows.Forms.Button();
            this.cmdReplace = new System.Windows.Forms.Button();
            this.cmdLast = new System.Windows.Forms.Button();
            this.cmdNext = new System.Windows.Forms.Button();
            this.cmdPrevious = new System.Windows.Forms.Button();
            this.cmdFirst = new System.Windows.Forms.Button();
            this.btn_Plus = new System.Windows.Forms.Button();
            this.btn_Minus = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.pnl_History.SuspendLayout();
            this.pnl_Quantity.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCart)).BeginInit();
            this.SuspendLayout();
            // 
            // pnl_History
            // 
            this.pnl_History.BackColor = System.Drawing.Color.Teal;
            this.pnl_History.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnl_History.Controls.Add(this.cmdCancel);
            this.pnl_History.Controls.Add(this.lblItemCount);
            this.pnl_History.Controls.Add(this.label3);
            this.pnl_History.Controls.Add(this.comReason);
            this.pnl_History.Controls.Add(this.btnQty_OK);
            this.pnl_History.Controls.Add(this.btn_Down);
            this.pnl_History.Controls.Add(this.btn_Up);
            this.pnl_History.Controls.Add(this.pnl_Quantity);
            this.pnl_History.Controls.Add(this.btn_Quantity);
            this.pnl_History.Controls.Add(this.lblOrderInfo);
            this.pnl_History.Controls.Add(this.dgvCart);
            this.pnl_History.Controls.Add(this.lbltotalAmount);
            this.pnl_History.Controls.Add(this.lblTotal);
            this.pnl_History.Controls.Add(this.label1);
            this.pnl_History.Controls.Add(this.cmdDown);
            this.pnl_History.Controls.Add(this.cmdUp);
            this.pnl_History.Controls.Add(this.cmdAdd);
            this.pnl_History.Controls.Add(this.cmdReplace);
            this.pnl_History.Controls.Add(this.cmdLast);
            this.pnl_History.Controls.Add(this.cmdNext);
            this.pnl_History.Controls.Add(this.cmdPrevious);
            this.pnl_History.Controls.Add(this.cmdFirst);
            this.pnl_History.Controls.Add(this.btn_Plus);
            this.pnl_History.Controls.Add(this.btn_Minus);
            this.pnl_History.Controls.Add(this.label2);
            this.pnl_History.Location = new System.Drawing.Point(10, 12);
            this.pnl_History.Name = "pnl_History";
            this.pnl_History.Size = new System.Drawing.Size(756, 495);
            this.pnl_History.TabIndex = 0;
            this.pnl_History.Paint += new System.Windows.Forms.PaintEventHandler(this.pnl_History_Paint);
            // 
            // cmdCancel
            // 
            this.cmdCancel.BackColor = System.Drawing.Color.PeachPuff;
            this.cmdCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.cmdCancel.Image = global::JublFood.POS.App.Properties.Resources._92;
            this.cmdCancel.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.cmdCancel.Location = new System.Drawing.Point(683, 354);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(68, 55);
            this.cmdCancel.TabIndex = 3;
            this.cmdCancel.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.cmdCancel.UseVisualStyleBackColor = false;
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // lblItemCount
            // 
            this.lblItemCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblItemCount.ForeColor = System.Drawing.Color.Yellow;
            this.lblItemCount.Location = new System.Drawing.Point(320, 105);
            this.lblItemCount.Name = "lblItemCount";
            this.lblItemCount.Size = new System.Drawing.Size(94, 28);
            this.lblItemCount.TabIndex = 75;
            this.lblItemCount.Text = "Item Count";
            this.lblItemCount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Yellow;
            this.label3.Location = new System.Drawing.Point(287, 47);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(127, 28);
            this.label3.TabIndex = 74;
            this.label3.Text = "Remake Reason";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // comReason
            // 
            this.comReason.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comReason.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comReason.FormattingEnabled = true;
            this.comReason.Location = new System.Drawing.Point(420, 46);
            this.comReason.Name = "comReason";
            this.comReason.Size = new System.Drawing.Size(227, 23);
            this.comReason.TabIndex = 73;
            this.comReason.SelectedIndexChanged += new System.EventHandler(this.comReason_SelectedIndexChanged_1);
            // 
            // btnQty_OK
            // 
            this.btnQty_OK.BackColor = System.Drawing.Color.PeachPuff;
            this.btnQty_OK.Enabled = false;
            this.btnQty_OK.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnQty_OK.Image = ((System.Drawing.Image)(resources.GetObject("btnQty_OK.Image")));
            this.btnQty_OK.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnQty_OK.Location = new System.Drawing.Point(683, 299);
            this.btnQty_OK.Name = "btnQty_OK";
            this.btnQty_OK.Size = new System.Drawing.Size(68, 55);
            this.btnQty_OK.TabIndex = 27;
            this.btnQty_OK.Text = "Add";
            this.btnQty_OK.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnQty_OK.UseVisualStyleBackColor = false;
            this.btnQty_OK.Click += new System.EventHandler(this.btnQty_OK_Click);
            // 
            // btn_Down
            // 
            this.btn_Down.Enabled = false;
            this.btn_Down.Image = global::JublFood.POS.App.Properties.Resources._31;
            this.btn_Down.Location = new System.Drawing.Point(287, 408);
            this.btn_Down.Name = "btn_Down";
            this.btn_Down.Size = new System.Drawing.Size(50, 40);
            this.btn_Down.TabIndex = 69;
            this.btn_Down.UseVisualStyleBackColor = true;
            // 
            // btn_Up
            // 
            this.btn_Up.Enabled = false;
            this.btn_Up.Image = global::JublFood.POS.App.Properties.Resources._34;
            this.btn_Up.Location = new System.Drawing.Point(287, 367);
            this.btn_Up.Name = "btn_Up";
            this.btn_Up.Size = new System.Drawing.Size(50, 40);
            this.btn_Up.TabIndex = 70;
            this.btn_Up.UseVisualStyleBackColor = true;
            // 
            // pnl_Quantity
            // 
            this.pnl_Quantity.Controls.Add(this.btn_Dot);
            this.pnl_Quantity.Controls.Add(this.txt_Quantity);
            this.pnl_Quantity.Controls.Add(this.uC_KeyBoardNumeric);
            this.pnl_Quantity.Location = new System.Drawing.Point(413, 75);
            this.pnl_Quantity.Name = "pnl_Quantity";
            this.pnl_Quantity.Size = new System.Drawing.Size(250, 316);
            this.pnl_Quantity.TabIndex = 68;
            // 
            // btn_Dot
            // 
            this.btn_Dot.BackColor = System.Drawing.Color.PeachPuff;
            this.btn_Dot.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Dot.Location = new System.Drawing.Point(162, 251);
            this.btn_Dot.Name = "btn_Dot";
            this.btn_Dot.Size = new System.Drawing.Size(77, 62);
            this.btn_Dot.TabIndex = 22;
            this.btn_Dot.Text = "Reset";
            this.btn_Dot.UseVisualStyleBackColor = false;
            this.btn_Dot.Visible = false;
            this.btn_Dot.Click += new System.EventHandler(this.btn_Dot_Click);
            // 
            // txt_Quantity
            // 
            this.txt_Quantity.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_Quantity.Location = new System.Drawing.Point(7, 27);
            this.txt_Quantity.MaxLength = 3;
            this.txt_Quantity.Name = "txt_Quantity";
            this.txt_Quantity.Size = new System.Drawing.Size(231, 31);
            this.txt_Quantity.TabIndex = 1;
            this.txt_Quantity.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt_Quantity.Visible = false;
            this.txt_Quantity.TextChanged += new System.EventHandler(this.txt_Quantity_TextChanged);
            this.txt_Quantity.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_Quantity_KeyPress);
            // 
            // uC_KeyBoardNumeric
            // 
            this.uC_KeyBoardNumeric.Location = new System.Drawing.Point(4, 61);
            this.uC_KeyBoardNumeric.Name = "uC_KeyBoardNumeric";
            this.uC_KeyBoardNumeric.Size = new System.Drawing.Size(242, 260);
            this.uC_KeyBoardNumeric.TabIndex = 0;
            this.uC_KeyBoardNumeric.txtUserID = null;
            this.uC_KeyBoardNumeric.Visible = false;
            // 
            // btn_Quantity
            // 
            this.btn_Quantity.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Quantity.BackColor = System.Drawing.Color.PeachPuff;
            this.btn_Quantity.Image = global::JublFood.POS.App.Properties.Resources._110;
            this.btn_Quantity.Location = new System.Drawing.Point(691, 330);
            this.btn_Quantity.Name = "btn_Quantity";
            this.btn_Quantity.Size = new System.Drawing.Size(50, 41);
            this.btn_Quantity.TabIndex = 17;
            this.btn_Quantity.UseVisualStyleBackColor = false;
            this.btn_Quantity.Click += new System.EventHandler(this.btn_Quantity_Click);
            // 
            // lblOrderInfo
            // 
            this.lblOrderInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOrderInfo.ForeColor = System.Drawing.Color.Yellow;
            this.lblOrderInfo.Location = new System.Drawing.Point(154, 0);
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
            this.dgvCart.Location = new System.Drawing.Point(8, 26);
            this.dgvCart.MultiSelect = false;
            this.dgvCart.Name = "dgvCart";
            this.dgvCart.ReadOnly = true;
            this.dgvCart.RowHeadersVisible = false;
            this.dgvCart.RowHeadersWidth = 10;
            this.dgvCart.RowTemplate.Height = 25;
            this.dgvCart.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgvCart.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvCart.Size = new System.Drawing.Size(273, 394);
            this.dgvCart.TabIndex = 7;
            this.dgvCart.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvCart_CellClick);
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
            this.lbltotalAmount.Location = new System.Drawing.Point(202, 420);
            this.lbltotalAmount.Name = "lbltotalAmount";
            this.lbltotalAmount.Size = new System.Drawing.Size(79, 25);
            this.lbltotalAmount.TabIndex = 6;
            this.lbltotalAmount.Text = "0.00";
            this.lbltotalAmount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lbltotalAmount.TextAlignChanged += new System.EventHandler(this.lbltotalAmount_TextAlignChanged);
            this.lbltotalAmount.Click += new System.EventHandler(this.lbltotalAmount_Click);
            // 
            // lblTotal
            // 
            this.lblTotal.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotal.ForeColor = System.Drawing.Color.White;
            this.lblTotal.Location = new System.Drawing.Point(8, 420);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(197, 25);
            this.lblTotal.TabIndex = 6;
            this.lblTotal.Text = "Total";
            this.lblTotal.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label1
            // 
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label1.Location = new System.Drawing.Point(17, 380);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 23);
            this.label1.TabIndex = 6;
            // 
            // cmdDown
            // 
            this.cmdDown.Location = new System.Drawing.Point(69, 380);
            this.cmdDown.Name = "cmdDown";
            this.cmdDown.Size = new System.Drawing.Size(60, 55);
            this.cmdDown.TabIndex = 5;
            this.cmdDown.UseVisualStyleBackColor = true;
            this.cmdDown.Visible = false;
            // 
            // cmdUp
            // 
            this.cmdUp.Location = new System.Drawing.Point(17, 386);
            this.cmdUp.Name = "cmdUp";
            this.cmdUp.Size = new System.Drawing.Size(60, 55);
            this.cmdUp.TabIndex = 4;
            this.cmdUp.UseVisualStyleBackColor = true;
            this.cmdUp.Visible = false;
            // 
            // cmdAdd
            // 
            this.cmdAdd.BackColor = System.Drawing.Color.PeachPuff;
            this.cmdAdd.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.cmdAdd.Image = global::JublFood.POS.App.Properties.Resources._81;
            this.cmdAdd.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.cmdAdd.Location = new System.Drawing.Point(314, 435);
            this.cmdAdd.Name = "cmdAdd";
            this.cmdAdd.Size = new System.Drawing.Size(10, 10);
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
            this.cmdReplace.Location = new System.Drawing.Point(683, 244);
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
            this.cmdLast.Location = new System.Drawing.Point(683, 189);
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
            this.cmdNext.Location = new System.Drawing.Point(683, 134);
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
            this.cmdPrevious.Location = new System.Drawing.Point(683, 79);
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
            this.cmdFirst.Location = new System.Drawing.Point(683, 24);
            this.cmdFirst.Name = "cmdFirst";
            this.cmdFirst.Size = new System.Drawing.Size(68, 55);
            this.cmdFirst.TabIndex = 3;
            this.cmdFirst.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.cmdFirst.UseVisualStyleBackColor = false;
            this.cmdFirst.Click += new System.EventHandler(this.cmdFirst_Click);
            // 
            // btn_Plus
            // 
            this.btn_Plus.BackColor = System.Drawing.Color.PeachPuff;
            this.btn_Plus.Image = global::JublFood.POS.App.Properties.Resources._62;
            this.btn_Plus.Location = new System.Drawing.Point(694, 346);
            this.btn_Plus.Name = "btn_Plus";
            this.btn_Plus.Size = new System.Drawing.Size(20, 25);
            this.btn_Plus.TabIndex = 72;
            this.btn_Plus.UseVisualStyleBackColor = false;
            this.btn_Plus.Click += new System.EventHandler(this.btn_Plus_Click);
            // 
            // btn_Minus
            // 
            this.btn_Minus.BackColor = System.Drawing.Color.PeachPuff;
            this.btn_Minus.Image = global::JublFood.POS.App.Properties.Resources._63;
            this.btn_Minus.Location = new System.Drawing.Point(691, 356);
            this.btn_Minus.Name = "btn_Minus";
            this.btn_Minus.Size = new System.Drawing.Size(23, 25);
            this.btn_Minus.TabIndex = 71;
            this.btn_Minus.UseVisualStyleBackColor = false;
            this.btn_Minus.Click += new System.EventHandler(this.btn_Minus_Click);
            // 
            // label2
            // 
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(257, 371);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(10, 25);
            this.label2.TabIndex = 9;
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // frmRemake
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(778, 498);
            this.ControlBox = false;
            this.Controls.Add(this.pnl_History);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Name = "frmRemake";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmHistory";
            this.Activated += new System.EventHandler(this.frmHistory_Activated);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmRemake_FormClosing);
            this.Load += new System.EventHandler(this.frmHistory_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmRemake_KeyDown);
            this.pnl_History.ResumeLayout(false);
            this.pnl_Quantity.ResumeLayout(false);
            this.pnl_Quantity.PerformLayout();
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
        private System.Windows.Forms.Timer timer1;
        public System.Windows.Forms.Button btn_Quantity;
        private System.Windows.Forms.Panel pnl_Quantity;
        private System.Windows.Forms.Button btnQty_OK;
        private System.Windows.Forms.Button btn_Dot;
        private System.Windows.Forms.TextBox txt_Quantity;
        private UC_KeyBoardNumeric uC_KeyBoardNumeric;
        public System.Windows.Forms.Button btn_Minus;
        public System.Windows.Forms.Button btn_Plus;
        public System.Windows.Forms.Button btn_Down;
        public System.Windows.Forms.Button btn_Up;
        private System.Windows.Forms.ComboBox comReason;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblItemCount;
    }
}