namespace JublFood.POS.App
{
    partial class frmOrder1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmOrder1));
            this.pnl_Order = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panelAttributes = new System.Windows.Forms.Panel();
            this.flowLayoutPanelMenuItems = new System.Windows.Forms.FlowLayoutPanel();
            this.tlpToppings = new System.Windows.Forms.TableLayoutPanel();
            this.flowLayoutPanelToppings = new System.Windows.Forms.FlowLayoutPanel();
            this.flowLayoutPanelToppingSizes = new System.Windows.Forms.FlowLayoutPanel();
            this.panelItemParts = new System.Windows.Forms.Panel();
            this.flowLayoutPanelSpecialtyPizzas = new System.Windows.Forms.FlowLayoutPanel();
            this.panelMenuCatagories = new System.Windows.Forms.Panel();
            this.pnl_Quantity = new System.Windows.Forms.Panel();
            this.btn_OK = new System.Windows.Forms.Button();
            this.btn_Dot = new System.Windows.Forms.Button();
            this.txt_Quantity = new System.Windows.Forms.TextBox();
            this.uC_KeyBoardNumeric = new JublFood.POS.App.UC_KeyBoardNumeric();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btn_Instructions = new System.Windows.Forms.Button();
            this.btn_Details = new System.Windows.Forms.Button();
            this.btn_Quantity = new System.Windows.Forms.Button();
            this.button8 = new System.Windows.Forms.Button();
            this.btn_Minus = new System.Windows.Forms.Button();
            this.btn_Plus = new System.Windows.Forms.Button();
            this.btn_Down = new System.Windows.Forms.Button();
            this.btn_Up = new System.Windows.Forms.Button();
            this.lv_ItemDetails = new System.Windows.Forms.ListView();
            this.lv_SrNo = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lv_Item = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lv_Price = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lbl_MenuText = new System.Windows.Forms.Label();
            this.uC_CustomerOrderBottomMenu1 = new JublFood.POS.App.UC_CustomerOrderBottomMenu();
            this.label3 = new System.Windows.Forms.Label();
            this.uC_Customer_OrderMenu = new JublFood.POS.App.UC_Customer_OrderMenu();
            this.pnl_Order.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panelAttributes.SuspendLayout();
            this.tlpToppings.SuspendLayout();
            this.pnl_Quantity.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnl_Order
            // 
            this.pnl_Order.BackColor = System.Drawing.Color.Teal;
            this.pnl_Order.Controls.Add(this.tableLayoutPanel1);
            this.pnl_Order.Controls.Add(this.pnl_Quantity);
            this.pnl_Order.Controls.Add(this.panel1);
            this.pnl_Order.Controls.Add(this.lv_ItemDetails);
            this.pnl_Order.Controls.Add(this.lbl_MenuText);
            this.pnl_Order.Controls.Add(this.uC_CustomerOrderBottomMenu1);
            this.pnl_Order.Controls.Add(this.label3);
            this.pnl_Order.Controls.Add(this.uC_Customer_OrderMenu);
            this.pnl_Order.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl_Order.Location = new System.Drawing.Point(0, 0);
            this.pnl_Order.Name = "pnl_Order";
            this.pnl_Order.Size = new System.Drawing.Size(787, 561);
            this.pnl_Order.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.panelAttributes, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.panelMenuCatagories, 0, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(278, 90);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(412, 163);
            this.tableLayoutPanel1.TabIndex = 67;
            // 
            // panelAttributes
            // 
            this.panelAttributes.Controls.Add(this.flowLayoutPanelMenuItems);
            this.panelAttributes.Controls.Add(this.tlpToppings);
            this.panelAttributes.Controls.Add(this.flowLayoutPanelSpecialtyPizzas);
            this.panelAttributes.Location = new System.Drawing.Point(3, 84);
            this.panelAttributes.Name = "panelAttributes";
            this.panelAttributes.Size = new System.Drawing.Size(406, 76);
            this.panelAttributes.TabIndex = 6;
            // 
            // flowLayoutPanelMenuItems
            // 
            this.flowLayoutPanelMenuItems.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanelMenuItems.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanelMenuItems.Name = "flowLayoutPanelMenuItems";
            this.flowLayoutPanelMenuItems.Size = new System.Drawing.Size(406, 76);
            this.flowLayoutPanelMenuItems.TabIndex = 1;
            // 
            // tlpToppings
            // 
            this.tlpToppings.ColumnCount = 1;
            this.tlpToppings.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpToppings.Controls.Add(this.flowLayoutPanelToppings, 0, 1);
            this.tlpToppings.Controls.Add(this.flowLayoutPanelToppingSizes, 0, 0);
            this.tlpToppings.Controls.Add(this.panelItemParts, 0, 2);
            this.tlpToppings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpToppings.Location = new System.Drawing.Point(0, 0);
            this.tlpToppings.Name = "tlpToppings";
            this.tlpToppings.RowCount = 3;
            this.tlpToppings.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tlpToppings.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 67F));
            this.tlpToppings.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 18F));
            this.tlpToppings.Size = new System.Drawing.Size(406, 76);
            this.tlpToppings.TabIndex = 3;
            // 
            // flowLayoutPanelToppings
            // 
            this.flowLayoutPanelToppings.AutoScroll = true;
            this.flowLayoutPanelToppings.BackColor = System.Drawing.SystemColors.Control;
            this.flowLayoutPanelToppings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanelToppings.Location = new System.Drawing.Point(3, 14);
            this.flowLayoutPanelToppings.Name = "flowLayoutPanelToppings";
            this.flowLayoutPanelToppings.Size = new System.Drawing.Size(400, 44);
            this.flowLayoutPanelToppings.TabIndex = 3;
            // 
            // flowLayoutPanelToppingSizes
            // 
            this.flowLayoutPanelToppingSizes.BackColor = System.Drawing.SystemColors.Control;
            this.flowLayoutPanelToppingSizes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanelToppingSizes.Location = new System.Drawing.Point(3, 3);
            this.flowLayoutPanelToppingSizes.Name = "flowLayoutPanelToppingSizes";
            this.flowLayoutPanelToppingSizes.Size = new System.Drawing.Size(400, 5);
            this.flowLayoutPanelToppingSizes.TabIndex = 2;
            // 
            // panelItemParts
            // 
            this.panelItemParts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelItemParts.Location = new System.Drawing.Point(3, 64);
            this.panelItemParts.Name = "panelItemParts";
            this.panelItemParts.Size = new System.Drawing.Size(400, 9);
            this.panelItemParts.TabIndex = 4;
            // 
            // flowLayoutPanelSpecialtyPizzas
            // 
            this.flowLayoutPanelSpecialtyPizzas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanelSpecialtyPizzas.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanelSpecialtyPizzas.Name = "flowLayoutPanelSpecialtyPizzas";
            this.flowLayoutPanelSpecialtyPizzas.Size = new System.Drawing.Size(406, 76);
            this.flowLayoutPanelSpecialtyPizzas.TabIndex = 4;
            // 
            // panelMenuCatagories
            // 
            this.panelMenuCatagories.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMenuCatagories.Location = new System.Drawing.Point(3, 3);
            this.panelMenuCatagories.Name = "panelMenuCatagories";
            this.panelMenuCatagories.Size = new System.Drawing.Size(406, 75);
            this.panelMenuCatagories.TabIndex = 3;
            // 
            // pnl_Quantity
            // 
            this.pnl_Quantity.Controls.Add(this.btn_OK);
            this.pnl_Quantity.Controls.Add(this.btn_Dot);
            this.pnl_Quantity.Controls.Add(this.txt_Quantity);
            this.pnl_Quantity.Controls.Add(this.uC_KeyBoardNumeric);
            this.pnl_Quantity.Location = new System.Drawing.Point(312, 413);
            this.pnl_Quantity.Name = "pnl_Quantity";
            this.pnl_Quantity.Size = new System.Drawing.Size(80, 49);
            this.pnl_Quantity.TabIndex = 66;
            this.pnl_Quantity.Visible = false;
            // 
            // btn_OK
            // 
            this.btn_OK.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btn_OK.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_OK.Image = ((System.Drawing.Image)(resources.GetObject("btn_OK.Image")));
            this.btn_OK.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btn_OK.Location = new System.Drawing.Point(337, 211);
            this.btn_OK.Name = "btn_OK";
            this.btn_OK.Size = new System.Drawing.Size(77, 62);
            this.btn_OK.TabIndex = 27;
            this.btn_OK.Text = "OK";
            this.btn_OK.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btn_OK.UseVisualStyleBackColor = false;
            this.btn_OK.Click += new System.EventHandler(this.btn_OK_Click);
            // 
            // btn_Dot
            // 
            this.btn_Dot.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btn_Dot.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Dot.Location = new System.Drawing.Point(169, 210);
            this.btn_Dot.Name = "btn_Dot";
            this.btn_Dot.Size = new System.Drawing.Size(77, 62);
            this.btn_Dot.TabIndex = 22;
            this.btn_Dot.Text = ".";
            this.btn_Dot.UseVisualStyleBackColor = false;
            this.btn_Dot.Click += new System.EventHandler(this.btn_Dot_Click);
            // 
            // txt_Quantity
            // 
            this.txt_Quantity.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_Quantity.Location = new System.Drawing.Point(260, 130);
            this.txt_Quantity.Name = "txt_Quantity";
            this.txt_Quantity.Size = new System.Drawing.Size(154, 31);
            this.txt_Quantity.TabIndex = 1;
            this.txt_Quantity.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_Quantity_KeyPress);
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
            this.panel1.Controls.Add(this.btn_Instructions);
            this.panel1.Controls.Add(this.btn_Details);
            this.panel1.Controls.Add(this.btn_Quantity);
            this.panel1.Controls.Add(this.button8);
            this.panel1.Controls.Add(this.btn_Minus);
            this.panel1.Controls.Add(this.btn_Plus);
            this.panel1.Controls.Add(this.btn_Down);
            this.panel1.Controls.Add(this.btn_Up);
            this.panel1.Location = new System.Drawing.Point(6, 436);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(266, 113);
            this.panel1.TabIndex = 65;
            // 
            // btn_Instructions
            // 
            this.btn_Instructions.Image = global::JublFood.POS.App.Properties.Resources._103;
            this.btn_Instructions.Location = new System.Drawing.Point(202, 53);
            this.btn_Instructions.Name = "btn_Instructions";
            this.btn_Instructions.Size = new System.Drawing.Size(60, 50);
            this.btn_Instructions.TabIndex = 6;
            this.btn_Instructions.UseVisualStyleBackColor = true;
            this.btn_Instructions.Click += new System.EventHandler(this.btn_Instructions_Click);
            // 
            // btn_Details
            // 
            this.btn_Details.Image = global::JublFood.POS.App.Properties.Resources._112;
            this.btn_Details.Location = new System.Drawing.Point(202, 3);
            this.btn_Details.Name = "btn_Details";
            this.btn_Details.Size = new System.Drawing.Size(60, 50);
            this.btn_Details.TabIndex = 7;
            this.btn_Details.UseVisualStyleBackColor = true;
            // 
            // btn_Quantity
            // 
            this.btn_Quantity.Image = global::JublFood.POS.App.Properties.Resources._110;
            this.btn_Quantity.Location = new System.Drawing.Point(142, 53);
            this.btn_Quantity.Name = "btn_Quantity";
            this.btn_Quantity.Size = new System.Drawing.Size(60, 50);
            this.btn_Quantity.TabIndex = 4;
            this.btn_Quantity.UseVisualStyleBackColor = true;
            this.btn_Quantity.Click += new System.EventHandler(this.btn_Quantity_Click);
            // 
            // button8
            // 
            this.button8.Image = global::JublFood.POS.App.Properties.Resources._6;
            this.button8.Location = new System.Drawing.Point(142, 3);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(60, 50);
            this.button8.TabIndex = 5;
            this.button8.UseVisualStyleBackColor = true;
            // 
            // btn_Minus
            // 
            this.btn_Minus.Image = global::JublFood.POS.App.Properties.Resources._63;
            this.btn_Minus.Location = new System.Drawing.Point(65, 53);
            this.btn_Minus.Name = "btn_Minus";
            this.btn_Minus.Size = new System.Drawing.Size(60, 50);
            this.btn_Minus.TabIndex = 2;
            this.btn_Minus.UseVisualStyleBackColor = true;
            this.btn_Minus.Click += new System.EventHandler(this.btn_Minus_Click);
            // 
            // btn_Plus
            // 
            this.btn_Plus.Image = global::JublFood.POS.App.Properties.Resources._62;
            this.btn_Plus.Location = new System.Drawing.Point(65, 3);
            this.btn_Plus.Name = "btn_Plus";
            this.btn_Plus.Size = new System.Drawing.Size(60, 50);
            this.btn_Plus.TabIndex = 3;
            this.btn_Plus.UseVisualStyleBackColor = true;
            this.btn_Plus.Click += new System.EventHandler(this.btn_Plus_Click);
            // 
            // btn_Down
            // 
            this.btn_Down.Image = global::JublFood.POS.App.Properties.Resources._31;
            this.btn_Down.Location = new System.Drawing.Point(5, 53);
            this.btn_Down.Name = "btn_Down";
            this.btn_Down.Size = new System.Drawing.Size(60, 50);
            this.btn_Down.TabIndex = 1;
            this.btn_Down.UseVisualStyleBackColor = true;
            // 
            // btn_Up
            // 
            this.btn_Up.Image = global::JublFood.POS.App.Properties.Resources._34;
            this.btn_Up.Location = new System.Drawing.Point(5, 3);
            this.btn_Up.Name = "btn_Up";
            this.btn_Up.Size = new System.Drawing.Size(60, 50);
            this.btn_Up.TabIndex = 1;
            this.btn_Up.UseVisualStyleBackColor = true;
            // 
            // lv_ItemDetails
            // 
            this.lv_ItemDetails.Activation = System.Windows.Forms.ItemActivation.OneClick;
            this.lv_ItemDetails.BackColor = System.Drawing.SystemColors.Info;
            this.lv_ItemDetails.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.lv_SrNo,
            this.lv_Item,
            this.lv_Price});
            this.lv_ItemDetails.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lv_ItemDetails.FullRowSelect = true;
            this.lv_ItemDetails.GridLines = true;
            this.lv_ItemDetails.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lv_ItemDetails.HideSelection = false;
            this.lv_ItemDetails.HoverSelection = true;
            this.lv_ItemDetails.Location = new System.Drawing.Point(6, 101);
            this.lv_ItemDetails.Name = "lv_ItemDetails";
            this.lv_ItemDetails.OwnerDraw = true;
            this.lv_ItemDetails.Size = new System.Drawing.Size(266, 372);
            this.lv_ItemDetails.TabIndex = 64;
            this.lv_ItemDetails.UseCompatibleStateImageBehavior = false;
            this.lv_ItemDetails.View = System.Windows.Forms.View.Details;
            // 
            // lv_SrNo
            // 
            this.lv_SrNo.Text = "#";
            this.lv_SrNo.Width = 30;
            // 
            // lv_Item
            // 
            this.lv_Item.Text = "Item";
            this.lv_Item.Width = 172;
            // 
            // lv_Price
            // 
            this.lv_Price.Text = "Price";
            // 
            // lbl_MenuText
            // 
            this.lbl_MenuText.AutoSize = true;
            this.lbl_MenuText.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_MenuText.Location = new System.Drawing.Point(433, 67);
            this.lbl_MenuText.Name = "lbl_MenuText";
            this.lbl_MenuText.Size = new System.Drawing.Size(103, 20);
            this.lbl_MenuText.TabIndex = 53;
            this.lbl_MenuText.Text = "Menu Items";
            // 
            // uC_CustomerOrderBottomMenu1
            // 
            this.uC_CustomerOrderBottomMenu1.Location = new System.Drawing.Point(272, 493);
            this.uC_CustomerOrderBottomMenu1.Name = "uC_CustomerOrderBottomMenu1";
            this.uC_CustomerOrderBottomMenu1.Size = new System.Drawing.Size(514, 61);
            this.uC_CustomerOrderBottomMenu1.TabIndex = 50;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.WhiteSmoke;
            this.label3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label3.Location = new System.Drawing.Point(2, 59);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(780, 3);
            this.label3.TabIndex = 49;
            // 
            // uC_Customer_OrderMenu
            // 
            this.uC_Customer_OrderMenu.Location = new System.Drawing.Point(3, 1);
            this.uC_Customer_OrderMenu.Name = "uC_Customer_OrderMenu";
            this.uC_Customer_OrderMenu.Size = new System.Drawing.Size(712, 55);
            this.uC_Customer_OrderMenu.TabIndex = 0;
            // 
            // frmOrder1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(787, 561);
            this.ControlBox = false;
            this.Controls.Add(this.pnl_Order);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmOrder1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Order";
            this.Load += new System.EventHandler(this.frmOrder_Load);
            this.pnl_Order.ResumeLayout(false);
            this.pnl_Order.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panelAttributes.ResumeLayout(false);
            this.tlpToppings.ResumeLayout(false);
            this.pnl_Quantity.ResumeLayout(false);
            this.pnl_Quantity.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnl_Order;
        private UC_Customer_OrderMenu uC_Customer_OrderMenu;
        private UC_CustomerOrderBottomMenu uC_CustomerOrderBottomMenu1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lbl_MenuText;
        private System.Windows.Forms.ListView lv_ItemDetails;
        private System.Windows.Forms.ColumnHeader lv_SrNo;
        private System.Windows.Forms.ColumnHeader lv_Item;
        private System.Windows.Forms.ColumnHeader lv_Price;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btn_Instructions;
        private System.Windows.Forms.Button btn_Details;
        private System.Windows.Forms.Button btn_Quantity;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.Button btn_Minus;
        private System.Windows.Forms.Button btn_Plus;
        private System.Windows.Forms.Button btn_Down;
        private System.Windows.Forms.Button btn_Up;
        private System.Windows.Forms.Panel pnl_Quantity;
        private System.Windows.Forms.TextBox txt_Quantity;
        private UC_KeyBoardNumeric uC_KeyBoardNumeric;
        private System.Windows.Forms.Button btn_Dot;
        private System.Windows.Forms.Button btn_OK;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panelAttributes;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelMenuItems;
        private System.Windows.Forms.TableLayoutPanel tlpToppings;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelToppings;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelToppingSizes;
        private System.Windows.Forms.Panel panelItemParts;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelSpecialtyPizzas;
        private System.Windows.Forms.Panel panelMenuCatagories;
    }
}