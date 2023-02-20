namespace JublFood.POS.App
{
    partial class frmOrder
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.flowLayoutPanelMenuItems = new System.Windows.Forms.FlowLayoutPanel();
            this.dgvCart = new System.Windows.Forms.DataGridView();
            this.Qty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Item = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VegNVegColor = new System.Windows.Forms.DataGridViewImageColumn();
            this.Price = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tlpMain = new System.Windows.Forms.TableLayoutPanel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.rtfUpsellReminder = new System.Windows.Forms.RichTextBox();
            this.checkBoxVegOnly = new System.Windows.Forms.CheckBox();
            this.lblWhere = new System.Windows.Forms.Label();
            this.uC_Customer_OrderMenu = new JublFood.POS.App.UC_Customer_OrderMenu();
            this.panelAttributes = new System.Windows.Forms.Panel();
            this.flowLayoutPanelBUnit = new System.Windows.Forms.FlowLayoutPanel();
            this.tlpToppings = new System.Windows.Forms.TableLayoutPanel();
            this.flowLayoutPanelToppings = new System.Windows.Forms.FlowLayoutPanel();
            this.flowLayoutPanelToppingSizes = new System.Windows.Forms.FlowLayoutPanel();
            this.panelItemParts = new System.Windows.Forms.Panel();
            this.flowLayoutPanelMenuCatagories = new System.Windows.Forms.FlowLayoutPanel();
            this.flowLayoutPanelSpecialtyPizzas = new System.Windows.Forms.FlowLayoutPanel();
            this.pnl_Quantity = new System.Windows.Forms.Panel();
            this.btnQty_OK = new System.Windows.Forms.Button();
            this.btn_Dot = new System.Windows.Forms.Button();
            this.txt_Quantity = new System.Windows.Forms.TextBox();
            this.uC_KeyBoardNumeric = new JublFood.POS.App.UC_KeyBoardNumeric();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnOnHold = new System.Windows.Forms.Button();
            this.btn_Instructions = new System.Windows.Forms.Button();
            this.btnDiscardCart = new System.Windows.Forms.Button();
            this.btn_Details = new System.Windows.Forms.Button();
            this.btn_Quantity = new System.Windows.Forms.Button();
            this.btn_Coupons = new System.Windows.Forms.Button();
            this.btn_Minus = new System.Windows.Forms.Button();
            this.btn_Plus = new System.Windows.Forms.Button();
            this.btn_Down = new System.Windows.Forms.Button();
            this.btn_Up = new System.Windows.Forms.Button();
            this.uC_CustomerOrderBottomMenu1 = new JublFood.POS.App.UC_CustomerOrderBottomMenu();
            this.panelCart = new System.Windows.Forms.Panel();
            this.lbltimedorder = new System.Windows.Forms.Label();
            this.dgvCartTotals = new System.Windows.Forms.DataGridView();
            this.lbltimed = new System.Windows.Forms.Label();
            this.uC_Customer_order_Header1 = new JublFood.POS.App.UserControls.UC_Customer_order_Header();
            this.ucInformationList = new JublFood.POS.App.UC_InformationList();
            this.ucFunctionList = new JublFood.POS.App.UC_FunctionList();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCart)).BeginInit();
            this.tlpMain.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panelAttributes.SuspendLayout();
            this.tlpToppings.SuspendLayout();
            this.pnl_Quantity.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panelCart.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCartTotals)).BeginInit();
            this.SuspendLayout();
            // 
            // flowLayoutPanelMenuItems
            // 
            this.flowLayoutPanelMenuItems.Location = new System.Drawing.Point(0, 118);
            this.flowLayoutPanelMenuItems.Margin = new System.Windows.Forms.Padding(3, 3, 0, 3);
            this.flowLayoutPanelMenuItems.Name = "flowLayoutPanelMenuItems";
            this.flowLayoutPanelMenuItems.Size = new System.Drawing.Size(515, 333);
            this.flowLayoutPanelMenuItems.TabIndex = 1;
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
            this.dgvCart.Location = new System.Drawing.Point(0, 22);
            this.dgvCart.MultiSelect = false;
            this.dgvCart.Name = "dgvCart";
            this.dgvCart.ReadOnly = true;
            this.dgvCart.RowHeadersVisible = false;
            this.dgvCart.RowHeadersWidth = 10;
            this.dgvCart.RowTemplate.Height = 25;
            this.dgvCart.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.dgvCart.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvCart.Size = new System.Drawing.Size(273, 364);
            this.dgvCart.TabIndex = 3;
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
            // tlpMain
            // 
            this.tlpMain.ColumnCount = 2;
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 34.875F));
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 65.125F));
            this.tlpMain.Controls.Add(this.panel2, 0, 1);
            this.tlpMain.Controls.Add(this.panelAttributes, 1, 2);
            this.tlpMain.Controls.Add(this.panel1, 0, 3);
            this.tlpMain.Controls.Add(this.uC_CustomerOrderBottomMenu1, 1, 3);
            this.tlpMain.Controls.Add(this.panelCart, 0, 2);
            this.tlpMain.Controls.Add(this.uC_Customer_order_Header1, 0, 0);
            this.tlpMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpMain.Location = new System.Drawing.Point(0, 0);
            this.tlpMain.Name = "tlpMain";
            this.tlpMain.RowCount = 4;
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 84F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 88F));
            this.tlpMain.Size = new System.Drawing.Size(800, 600);
            this.tlpMain.TabIndex = 4;
            // 
            // panel2
            // 
            this.tlpMain.SetColumnSpan(this.panel2, 2);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.rtfUpsellReminder);
            this.panel2.Controls.Add(this.checkBoxVegOnly);
            this.panel2.Controls.Add(this.lblWhere);
            this.panel2.Controls.Add(this.uC_Customer_OrderMenu);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(3, 33);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(794, 78);
            this.panel2.TabIndex = 0;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.White;
            this.label3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label3.Location = new System.Drawing.Point(-2, 58);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(790, 3);
            this.label3.TabIndex = 50;
            // 
            // rtfUpsellReminder
            // 
            this.rtfUpsellReminder.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.rtfUpsellReminder.Location = new System.Drawing.Point(3, 63);
            this.rtfUpsellReminder.Margin = new System.Windows.Forms.Padding(0);
            this.rtfUpsellReminder.Name = "rtfUpsellReminder";
            this.rtfUpsellReminder.ReadOnly = true;
            this.rtfUpsellReminder.Size = new System.Drawing.Size(267, 28);
            this.rtfUpsellReminder.TabIndex = 10;
            this.rtfUpsellReminder.Text = "terer";
            // 
            // checkBoxVegOnly
            // 
            this.checkBoxVegOnly.AutoSize = true;
            this.checkBoxVegOnly.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxVegOnly.Location = new System.Drawing.Point(706, 61);
            this.checkBoxVegOnly.Name = "checkBoxVegOnly";
            this.checkBoxVegOnly.Size = new System.Drawing.Size(82, 19);
            this.checkBoxVegOnly.TabIndex = 9;
            this.checkBoxVegOnly.Text = "Veg Only";
            this.checkBoxVegOnly.UseVisualStyleBackColor = true;
            this.checkBoxVegOnly.CheckedChanged += new System.EventHandler(this.checkBoxVegOnly_CheckedChanged);
            // 
            // lblWhere
            // 
            this.lblWhere.AutoSize = true;
            this.lblWhere.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWhere.Location = new System.Drawing.Point(456, 60);
            this.lblWhere.Name = "lblWhere";
            this.lblWhere.Size = new System.Drawing.Size(95, 18);
            this.lblWhere.TabIndex = 8;
            this.lblWhere.Text = "Menu Items";
            // 
            // uC_Customer_OrderMenu
            // 
            this.uC_Customer_OrderMenu.Location = new System.Drawing.Point(0, 0);
            this.uC_Customer_OrderMenu.Name = "uC_Customer_OrderMenu";
            this.uC_Customer_OrderMenu.Size = new System.Drawing.Size(797, 56);
            this.uC_Customer_OrderMenu.TabIndex = 7;
            // 
            // panelAttributes
            // 
            this.panelAttributes.Controls.Add(this.flowLayoutPanelBUnit);
            this.panelAttributes.Controls.Add(this.tlpToppings);
            this.panelAttributes.Controls.Add(this.flowLayoutPanelMenuCatagories);
            this.panelAttributes.Controls.Add(this.flowLayoutPanelMenuItems);
            this.panelAttributes.Controls.Add(this.flowLayoutPanelSpecialtyPizzas);
            this.panelAttributes.Controls.Add(this.pnl_Quantity);
            this.panelAttributes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelAttributes.Location = new System.Drawing.Point(282, 117);
            this.panelAttributes.Name = "panelAttributes";
            this.panelAttributes.Size = new System.Drawing.Size(515, 392);
            this.panelAttributes.TabIndex = 5;
            // 
            // flowLayoutPanelBUnit
            // 
            this.flowLayoutPanelBUnit.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanelBUnit.Name = "flowLayoutPanelBUnit";
            this.flowLayoutPanelBUnit.Size = new System.Drawing.Size(515, 59);
            this.flowLayoutPanelBUnit.TabIndex = 0;
            this.flowLayoutPanelBUnit.Visible = false;
            // 
            // tlpToppings
            // 
            this.tlpToppings.ColumnCount = 1;
            this.tlpToppings.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpToppings.Controls.Add(this.flowLayoutPanelToppings, 0, 1);
            this.tlpToppings.Controls.Add(this.flowLayoutPanelToppingSizes, 0, 0);
            this.tlpToppings.Controls.Add(this.panelItemParts, 0, 2);
            this.tlpToppings.Location = new System.Drawing.Point(0, 0);
            this.tlpToppings.Name = "tlpToppings";
            this.tlpToppings.RowCount = 3;
            this.tlpToppings.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 65F));
            this.tlpToppings.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpToppings.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 65F));
            this.tlpToppings.Size = new System.Drawing.Size(514, 404);
            this.tlpToppings.TabIndex = 3;
            // 
            // flowLayoutPanelToppings
            // 
            this.flowLayoutPanelToppings.BackColor = System.Drawing.Color.Teal;
            this.flowLayoutPanelToppings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanelToppings.Location = new System.Drawing.Point(3, 68);
            this.flowLayoutPanelToppings.Name = "flowLayoutPanelToppings";
            this.flowLayoutPanelToppings.Size = new System.Drawing.Size(508, 268);
            this.flowLayoutPanelToppings.TabIndex = 3;
            // 
            // flowLayoutPanelToppingSizes
            // 
            this.flowLayoutPanelToppingSizes.BackColor = System.Drawing.Color.Teal;
            this.flowLayoutPanelToppingSizes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanelToppingSizes.Location = new System.Drawing.Point(3, 3);
            this.flowLayoutPanelToppingSizes.Name = "flowLayoutPanelToppingSizes";
            this.flowLayoutPanelToppingSizes.Size = new System.Drawing.Size(508, 59);
            this.flowLayoutPanelToppingSizes.TabIndex = 2;
            // 
            // panelItemParts
            // 
            this.panelItemParts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelItemParts.Location = new System.Drawing.Point(3, 342);
            this.panelItemParts.Name = "panelItemParts";
            this.panelItemParts.Size = new System.Drawing.Size(508, 59);
            this.panelItemParts.TabIndex = 4;
            // 
            // flowLayoutPanelMenuCatagories
            // 
            this.flowLayoutPanelMenuCatagories.Location = new System.Drawing.Point(0, 59);
            this.flowLayoutPanelMenuCatagories.Name = "flowLayoutPanelMenuCatagories";
            this.flowLayoutPanelMenuCatagories.Size = new System.Drawing.Size(515, 59);
            this.flowLayoutPanelMenuCatagories.TabIndex = 47;
            // 
            // flowLayoutPanelSpecialtyPizzas
            // 
            this.flowLayoutPanelSpecialtyPizzas.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanelSpecialtyPizzas.Name = "flowLayoutPanelSpecialtyPizzas";
            this.flowLayoutPanelSpecialtyPizzas.Size = new System.Drawing.Size(514, 404);
            this.flowLayoutPanelSpecialtyPizzas.TabIndex = 4;
            // 
            // pnl_Quantity
            // 
            this.pnl_Quantity.Controls.Add(this.btnQty_OK);
            this.pnl_Quantity.Controls.Add(this.btn_Dot);
            this.pnl_Quantity.Controls.Add(this.txt_Quantity);
            this.pnl_Quantity.Controls.Add(this.uC_KeyBoardNumeric);
            this.pnl_Quantity.Location = new System.Drawing.Point(0, 0);
            this.pnl_Quantity.Name = "pnl_Quantity";
            this.pnl_Quantity.Size = new System.Drawing.Size(514, 404);
            this.pnl_Quantity.TabIndex = 67;
            this.pnl_Quantity.Visible = false;
            // 
            // btnQty_OK
            // 
            this.btnQty_OK.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnQty_OK.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnQty_OK.Image = global::JublFood.POS.App.Properties.Resources._171;
            this.btnQty_OK.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnQty_OK.Location = new System.Drawing.Point(337, 195);
            this.btnQty_OK.Name = "btnQty_OK";
            this.btnQty_OK.Size = new System.Drawing.Size(77, 62);
            this.btnQty_OK.TabIndex = 27;
            this.btnQty_OK.Text = "OK";
            this.btnQty_OK.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnQty_OK.UseVisualStyleBackColor = false;
            this.btnQty_OK.Click += new System.EventHandler(this.btnQty_OK_Click);
            // 
            // btn_Dot
            // 
            this.btn_Dot.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btn_Dot.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Dot.Location = new System.Drawing.Point(161, 195);
            this.btn_Dot.Name = "btn_Dot";
            this.btn_Dot.Size = new System.Drawing.Size(77, 62);
            this.btn_Dot.TabIndex = 22;
            this.btn_Dot.Text = ".";
            this.btn_Dot.UseVisualStyleBackColor = false;
            // 
            // txt_Quantity
            // 
            this.txt_Quantity.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_Quantity.Location = new System.Drawing.Point(260, 147);
            this.txt_Quantity.MaxLength = 3;
            this.txt_Quantity.Name = "txt_Quantity";
            this.txt_Quantity.Size = new System.Drawing.Size(154, 31);
            this.txt_Quantity.TabIndex = 1;
            this.txt_Quantity.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt_Quantity.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_Quantity_KeyDown);
            // 
            // uC_KeyBoardNumeric
            // 
            this.uC_KeyBoardNumeric.Location = new System.Drawing.Point(4, 5);
            this.uC_KeyBoardNumeric.Name = "uC_KeyBoardNumeric";
            this.uC_KeyBoardNumeric.Size = new System.Drawing.Size(242, 268);
            this.uC_KeyBoardNumeric.TabIndex = 0;
            this.uC_KeyBoardNumeric.txtUserID = null;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.btnOnHold);
            this.panel1.Controls.Add(this.btn_Instructions);
            this.panel1.Controls.Add(this.btnDiscardCart);
            this.panel1.Controls.Add(this.btn_Details);
            this.panel1.Controls.Add(this.btn_Quantity);
            this.panel1.Controls.Add(this.btn_Coupons);
            this.panel1.Controls.Add(this.btn_Minus);
            this.panel1.Controls.Add(this.btn_Plus);
            this.panel1.Controls.Add(this.btn_Down);
            this.panel1.Controls.Add(this.btn_Up);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 515);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(273, 82);
            this.panel1.TabIndex = 66;
            // 
            // btnOnHold
            // 
            this.btnOnHold.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOnHold.BackColor = System.Drawing.SystemColors.Control;
            this.btnOnHold.Enabled = false;
            this.btnOnHold.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOnHold.Location = new System.Drawing.Point(108, 0);
            this.btnOnHold.Name = "btnOnHold";
            this.btnOnHold.Size = new System.Drawing.Size(52, 40);
            this.btnOnHold.TabIndex = 8;
            this.btnOnHold.Text = "Put On Hold";
            this.btnOnHold.UseVisualStyleBackColor = false;
            this.btnOnHold.Visible = false;
            this.btnOnHold.Click += new System.EventHandler(this.btnOnHold_Click);
            // 
            // btn_Instructions
            // 
            this.btn_Instructions.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Instructions.Enabled = false;
            this.btn_Instructions.Image = global::JublFood.POS.App.Properties.Resources._103;
            this.btn_Instructions.Location = new System.Drawing.Point(219, 40);
            this.btn_Instructions.Name = "btn_Instructions";
            this.btn_Instructions.Size = new System.Drawing.Size(50, 40);
            this.btn_Instructions.TabIndex = 6;
            this.btn_Instructions.UseVisualStyleBackColor = true;
            this.btn_Instructions.Click += new System.EventHandler(this.btn_Instructions_Click);
            // 
            // btnDiscardCart
            // 
            this.btnDiscardCart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDiscardCart.BackColor = System.Drawing.SystemColors.Control;
            this.btnDiscardCart.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDiscardCart.Location = new System.Drawing.Point(107, 40);
            this.btnDiscardCart.Name = "btnDiscardCart";
            this.btnDiscardCart.Size = new System.Drawing.Size(52, 40);
            this.btnDiscardCart.TabIndex = 0;
            this.btnDiscardCart.Text = "Discard";
            this.btnDiscardCart.UseVisualStyleBackColor = false;
            this.btnDiscardCart.Visible = false;
            this.btnDiscardCart.Click += new System.EventHandler(this.btnDiscardCart_Click);
            // 
            // btn_Details
            // 
            this.btn_Details.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Details.Image = global::JublFood.POS.App.Properties.Resources._112;
            this.btn_Details.Location = new System.Drawing.Point(219, 0);
            this.btn_Details.Name = "btn_Details";
            this.btn_Details.Size = new System.Drawing.Size(50, 40);
            this.btn_Details.TabIndex = 7;
            this.btn_Details.UseVisualStyleBackColor = true;
            this.btn_Details.Click += new System.EventHandler(this.btn_Details_Click);
            // 
            // btn_Quantity
            // 
            this.btn_Quantity.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Quantity.Enabled = false;
            this.btn_Quantity.Image = global::JublFood.POS.App.Properties.Resources._110;
            this.btn_Quantity.Location = new System.Drawing.Point(166, 40);
            this.btn_Quantity.Name = "btn_Quantity";
            this.btn_Quantity.Size = new System.Drawing.Size(50, 40);
            this.btn_Quantity.TabIndex = 4;
            this.btn_Quantity.UseVisualStyleBackColor = true;
            this.btn_Quantity.Click += new System.EventHandler(this.btn_Quantity_Click);
            // 
            // btn_Coupons
            // 
            this.btn_Coupons.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Coupons.Enabled = false;
            this.btn_Coupons.Image = global::JublFood.POS.App.Properties.Resources._6;
            this.btn_Coupons.Location = new System.Drawing.Point(166, 0);
            this.btn_Coupons.Name = "btn_Coupons";
            this.btn_Coupons.Size = new System.Drawing.Size(50, 40);
            this.btn_Coupons.TabIndex = 5;
            this.btn_Coupons.UseVisualStyleBackColor = true;
            this.btn_Coupons.Click += new System.EventHandler(this.btn_Coupons_Click);
            // 
            // btn_Minus
            // 
            this.btn_Minus.Image = global::JublFood.POS.App.Properties.Resources._63;
            this.btn_Minus.Location = new System.Drawing.Point(51, 40);
            this.btn_Minus.Name = "btn_Minus";
            this.btn_Minus.Size = new System.Drawing.Size(50, 40);
            this.btn_Minus.TabIndex = 2;
            this.btn_Minus.UseVisualStyleBackColor = true;
            this.btn_Minus.Click += new System.EventHandler(this.btn_Minus_Click);
            // 
            // btn_Plus
            // 
            this.btn_Plus.Image = global::JublFood.POS.App.Properties.Resources._62;
            this.btn_Plus.Location = new System.Drawing.Point(51, 0);
            this.btn_Plus.Name = "btn_Plus";
            this.btn_Plus.Size = new System.Drawing.Size(50, 40);
            this.btn_Plus.TabIndex = 3;
            this.btn_Plus.UseVisualStyleBackColor = true;
            this.btn_Plus.Click += new System.EventHandler(this.btn_Plus_Click);
            // 
            // btn_Down
            // 
            this.btn_Down.Enabled = false;
            this.btn_Down.Image = global::JublFood.POS.App.Properties.Resources._31;
            this.btn_Down.Location = new System.Drawing.Point(0, 40);
            this.btn_Down.Name = "btn_Down";
            this.btn_Down.Size = new System.Drawing.Size(50, 40);
            this.btn_Down.TabIndex = 1;
            this.btn_Down.UseVisualStyleBackColor = true;
            this.btn_Down.Click += new System.EventHandler(this.btn_Down_Click);
            // 
            // btn_Up
            // 
            this.btn_Up.Enabled = false;
            this.btn_Up.Image = global::JublFood.POS.App.Properties.Resources._34;
            this.btn_Up.Location = new System.Drawing.Point(0, 0);
            this.btn_Up.Name = "btn_Up";
            this.btn_Up.Size = new System.Drawing.Size(50, 40);
            this.btn_Up.TabIndex = 1;
            this.btn_Up.UseVisualStyleBackColor = true;
            this.btn_Up.Click += new System.EventHandler(this.btn_Up_Click);
            // 
            // uC_CustomerOrderBottomMenu1
            // 
            this.uC_CustomerOrderBottomMenu1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.uC_CustomerOrderBottomMenu1.Location = new System.Drawing.Point(282, 536);
            this.uC_CustomerOrderBottomMenu1.Name = "uC_CustomerOrderBottomMenu1";
            this.uC_CustomerOrderBottomMenu1.Size = new System.Drawing.Size(515, 61);
            this.uC_CustomerOrderBottomMenu1.TabIndex = 51;
            // 
            // panelCart
            // 
            this.panelCart.Controls.Add(this.lbltimedorder);
            this.panelCart.Controls.Add(this.dgvCartTotals);
            this.panelCart.Controls.Add(this.dgvCart);
            this.panelCart.Controls.Add(this.lbltimed);
            this.panelCart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelCart.Location = new System.Drawing.Point(3, 117);
            this.panelCart.Name = "panelCart";
            this.panelCart.Size = new System.Drawing.Size(273, 392);
            this.panelCart.TabIndex = 68;
            // 
            // lbltimedorder
            // 
            this.lbltimedorder.AutoSize = true;
            this.lbltimedorder.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbltimedorder.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.lbltimedorder.Location = new System.Drawing.Point(80, 0);
            this.lbltimedorder.Name = "lbltimedorder";
            this.lbltimedorder.Size = new System.Drawing.Size(54, 18);
            this.lbltimedorder.TabIndex = 4;
            this.lbltimedorder.Text = "Timed";
            this.lbltimedorder.Visible = false;
            // 
            // dgvCartTotals
            // 
            this.dgvCartTotals.AllowUserToAddRows = false;
            this.dgvCartTotals.AllowUserToDeleteRows = false;
            this.dgvCartTotals.AllowUserToOrderColumns = true;
            this.dgvCartTotals.AllowUserToResizeColumns = false;
            this.dgvCartTotals.AllowUserToResizeRows = false;
            this.dgvCartTotals.BackgroundColor = System.Drawing.Color.LightGoldenrodYellow;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.Teal;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.Teal;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvCartTotals.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvCartTotals.ColumnHeadersHeight = 30;
            this.dgvCartTotals.ColumnHeadersVisible = false;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.LightGoldenrodYellow;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvCartTotals.DefaultCellStyle = dataGridViewCellStyle4;
            this.dgvCartTotals.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dgvCartTotals.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgvCartTotals.Enabled = false;
            this.dgvCartTotals.EnableHeadersVisualStyles = false;
            this.dgvCartTotals.GridColor = System.Drawing.SystemColors.ActiveBorder;
            this.dgvCartTotals.Location = new System.Drawing.Point(0, 204);
            this.dgvCartTotals.Name = "dgvCartTotals";
            this.dgvCartTotals.ReadOnly = true;
            this.dgvCartTotals.RowHeadersVisible = false;
            this.dgvCartTotals.RowHeadersWidth = 10;
            this.dgvCartTotals.RowTemplate.Height = 20;
            this.dgvCartTotals.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.dgvCartTotals.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvCartTotals.Size = new System.Drawing.Size(273, 188);
            this.dgvCartTotals.TabIndex = 3;
            this.dgvCartTotals.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvCart_CellClick);
            // 
            // lbltimed
            // 
            this.lbltimed.AutoSize = true;
            this.lbltimed.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbltimed.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.lbltimed.Location = new System.Drawing.Point(25, 0);
            this.lbltimed.Name = "lbltimed";
            this.lbltimed.Size = new System.Drawing.Size(59, 18);
            this.lbltimed.TabIndex = 5;
            this.lbltimed.Text = "Timed:";
            // 
            // uC_Customer_order_Header1
            // 
            this.tlpMain.SetColumnSpan(this.uC_Customer_order_Header1, 2);
            this.uC_Customer_order_Header1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uC_Customer_order_Header1.LabelOrderTaker = null;
            this.uC_Customer_order_Header1.Location = new System.Drawing.Point(3, 3);
            this.uC_Customer_order_Header1.Name = "uC_Customer_order_Header1";
            this.uC_Customer_order_Header1.Size = new System.Drawing.Size(794, 24);
            this.uC_Customer_order_Header1.TabIndex = 69;
            // 
            // ucInformationList
            // 
            this.ucInformationList.Location = new System.Drawing.Point(456, 87);
            this.ucInformationList.Name = "ucInformationList";
            this.ucInformationList.Size = new System.Drawing.Size(72, 274);
            this.ucInformationList.TabIndex = 46;
            this.ucInformationList.Visible = false;
            // 
            // ucFunctionList
            // 
            this.ucFunctionList.AutoScroll = true;
            this.ucFunctionList.Location = new System.Drawing.Point(388, 87);
            this.ucFunctionList.Name = "ucFunctionList";
            this.ucFunctionList.Size = new System.Drawing.Size(70, 165);
            this.ucFunctionList.TabIndex = 45;
            this.ucFunctionList.Visible = false;
            // 
            // frmOrder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Teal;
            this.ClientSize = new System.Drawing.Size(800, 600);
            this.Controls.Add(this.ucInformationList);
            this.Controls.Add(this.ucFunctionList);
            this.Controls.Add(this.tlpMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.MinimumSize = new System.Drawing.Size(800, 600);
            this.Name = "frmOrder";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Order";
            this.Activated += new System.EventHandler(this.frmOrder_Activated);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmOrder_FormClosing);
            this.Load += new System.EventHandler(this.frmOrder_Load);
            this.Click += new System.EventHandler(this.frmOrder_Click);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmOrder_KeyDown);
            this.Resize += new System.EventHandler(this.Catalog_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCart)).EndInit();
            this.tlpMain.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panelAttributes.ResumeLayout(false);
            this.tlpToppings.ResumeLayout(false);
            this.pnl_Quantity.ResumeLayout(false);
            this.pnl_Quantity.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panelCart.ResumeLayout(false);
            this.panelCart.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCartTotals)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelMenuItems;
        private System.Windows.Forms.TableLayoutPanel tlpMain;
        private System.Windows.Forms.Button btnDiscardCart;
        private System.Windows.Forms.Panel panelAttributes;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelToppingSizes;
        private System.Windows.Forms.TableLayoutPanel tlpToppings;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelToppings;
        private System.Windows.Forms.Panel panelItemParts;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelSpecialtyPizzas;
        private UC_CustomerOrderBottomMenu uC_CustomerOrderBottomMenu1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel pnl_Quantity;
        private System.Windows.Forms.Button btnQty_OK;
        private System.Windows.Forms.Button btn_Dot;
        private System.Windows.Forms.TextBox txt_Quantity;
        private System.Windows.Forms.Panel panelCart;
        private System.Windows.Forms.DataGridView dgvCartTotals;
        private UserControls.UC_Customer_order_Header uC_Customer_order_Header1;
        private System.Windows.Forms.Panel panel2;
        private UC_FunctionList ucFunctionList;
        private UC_InformationList ucInformationList;
        public System.Windows.Forms.Button btnOnHold;
        private System.Windows.Forms.Label lblWhere;
        private System.Windows.Forms.CheckBox checkBoxVegOnly;
        private System.Windows.Forms.DataGridViewTextBoxColumn Qty;
        private System.Windows.Forms.DataGridViewTextBoxColumn Item;
        private System.Windows.Forms.DataGridViewImageColumn VegNVegColor;
        private System.Windows.Forms.DataGridViewTextBoxColumn Price;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelMenuCatagories;
        public System.Windows.Forms.DataGridView dgvCart;
        public System.Windows.Forms.Button btn_Instructions;
        public System.Windows.Forms.Button btn_Details;
        public System.Windows.Forms.Button btn_Quantity;
        public System.Windows.Forms.Button btn_Coupons;
        public System.Windows.Forms.Button btn_Minus;
        public System.Windows.Forms.Button btn_Plus;
        public System.Windows.Forms.Button btn_Down;
        public System.Windows.Forms.Button btn_Up;

        public UC_Customer_OrderMenu uC_Customer_OrderMenu;

        private System.Windows.Forms.RichTextBox rtfUpsellReminder;
        private System.Windows.Forms.Label label3;
        private UC_KeyBoardNumeric uC_KeyBoardNumeric;
        private System.Windows.Forms.Label lbltimedorder;
        private System.Windows.Forms.Label lbltimed;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelBUnit;
    }
}

