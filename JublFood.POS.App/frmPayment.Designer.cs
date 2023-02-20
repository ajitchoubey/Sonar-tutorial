namespace JublFood.POS.App
{
    partial class frmPayment
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
            this.pnl_payment = new System.Windows.Forms.Panel();
            this.panelNumricPad = new System.Windows.Forms.Panel();
            this.btnNum_Dot = new System.Windows.Forms.Button();
            this.btnNum_0 = new System.Windows.Forms.Button();
            this.btnBackSpace = new System.Windows.Forms.Button();
            this.btnNum_9 = new System.Windows.Forms.Button();
            this.btnNum_8 = new System.Windows.Forms.Button();
            this.btnNum_7 = new System.Windows.Forms.Button();
            this.btnNum_6 = new System.Windows.Forms.Button();
            this.btnNum_5 = new System.Windows.Forms.Button();
            this.btnNum_4 = new System.Windows.Forms.Button();
            this.btnNum_3 = new System.Windows.Forms.Button();
            this.btnNum_2 = new System.Windows.Forms.Button();
            this.btnNum_1 = new System.Windows.Forms.Button();
            this.panelPayType = new System.Windows.Forms.Panel();
            this.cmdCash = new System.Windows.Forms.Button();
            this.cmdDigital = new System.Windows.Forms.Button();
            this.cmdCheque = new System.Windows.Forms.Button();
            this.cmdSodexo = new System.Windows.Forms.Button();
            this.cmdAccor = new System.Windows.Forms.Button();
            this.btndot = new System.Windows.Forms.Button();
            this.uC_Customer_order_Header1 = new JublFood.POS.App.UserControls.UC_Customer_order_Header();
            this.gridPayment = new System.Windows.Forms.DataGridView();
            this.pnl_Cheque = new System.Windows.Forms.Panel();
            this.txtCheckInfo = new System.Windows.Forms.TextBox();
            this.ltxtCheckInfo = new System.Windows.Forms.Label();
            this.uc_KeyBoardNumeric = new JublFood.POS.App.UC_KeyBoardNumeric();
            this.tdbnTip = new System.Windows.Forms.TextBox();
            this.ltxtTip = new System.Windows.Forms.Label();
            this.btnDelete = new System.Windows.Forms.Button();
            this.cmdKeyboard = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.lblAmount = new System.Windows.Forms.Label();
            this.lblAmountDue = new System.Windows.Forms.Label();
            this.tdbnAmountTendered = new System.Windows.Forms.TextBox();
            this.tlblAmountDue = new System.Windows.Forms.Label();
            this.ltxtAmountTendered = new System.Windows.Forms.Label();
            this.ltxtAmountDue = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.btn_Order = new System.Windows.Forms.Button();
            this.lblPlaceHolder = new System.Windows.Forms.Label();
            this.lblmsg = new System.Windows.Forms.Label();
            this.flPanel_CardsWallet = new System.Windows.Forms.FlowLayoutPanel();
            this.pnl_amount = new System.Windows.Forms.FlowLayoutPanel();
            this.pnl_payment.SuspendLayout();
            this.panelNumricPad.SuspendLayout();
            this.panelPayType.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridPayment)).BeginInit();
            this.pnl_Cheque.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnl_payment
            // 
            this.pnl_payment.BackColor = System.Drawing.Color.Teal;
            this.pnl_payment.Controls.Add(this.panelNumricPad);
            this.pnl_payment.Controls.Add(this.panelPayType);
            this.pnl_payment.Controls.Add(this.btndot);
            this.pnl_payment.Controls.Add(this.uC_Customer_order_Header1);
            this.pnl_payment.Controls.Add(this.gridPayment);
            this.pnl_payment.Controls.Add(this.pnl_Cheque);
            this.pnl_payment.Controls.Add(this.uc_KeyBoardNumeric);
            this.pnl_payment.Controls.Add(this.tdbnTip);
            this.pnl_payment.Controls.Add(this.ltxtTip);
            this.pnl_payment.Controls.Add(this.btnDelete);
            this.pnl_payment.Controls.Add(this.cmdKeyboard);
            this.pnl_payment.Controls.Add(this.btnOK);
            this.pnl_payment.Controls.Add(this.label2);
            this.pnl_payment.Controls.Add(this.lblAmount);
            this.pnl_payment.Controls.Add(this.lblAmountDue);
            this.pnl_payment.Controls.Add(this.tdbnAmountTendered);
            this.pnl_payment.Controls.Add(this.tlblAmountDue);
            this.pnl_payment.Controls.Add(this.ltxtAmountTendered);
            this.pnl_payment.Controls.Add(this.ltxtAmountDue);
            this.pnl_payment.Controls.Add(this.btnSave);
            this.pnl_payment.Controls.Add(this.btn_Order);
            this.pnl_payment.Controls.Add(this.lblPlaceHolder);
            this.pnl_payment.Controls.Add(this.lblmsg);
            this.pnl_payment.Controls.Add(this.flPanel_CardsWallet);
            this.pnl_payment.Controls.Add(this.pnl_amount);
            this.pnl_payment.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl_payment.Location = new System.Drawing.Point(0, 0);
            this.pnl_payment.Name = "pnl_payment";
            this.pnl_payment.Size = new System.Drawing.Size(800, 600);
            this.pnl_payment.TabIndex = 0;
            this.pnl_payment.Paint += new System.Windows.Forms.PaintEventHandler(this.pnl_payment_Paint);
            // 
            // panelNumricPad
            // 
            this.panelNumricPad.Controls.Add(this.btnNum_Dot);
            this.panelNumricPad.Controls.Add(this.btnNum_0);
            this.panelNumricPad.Controls.Add(this.btnBackSpace);
            this.panelNumricPad.Controls.Add(this.btnNum_9);
            this.panelNumricPad.Controls.Add(this.btnNum_8);
            this.panelNumricPad.Controls.Add(this.btnNum_7);
            this.panelNumricPad.Controls.Add(this.btnNum_6);
            this.panelNumricPad.Controls.Add(this.btnNum_5);
            this.panelNumricPad.Controls.Add(this.btnNum_4);
            this.panelNumricPad.Controls.Add(this.btnNum_3);
            this.panelNumricPad.Controls.Add(this.btnNum_2);
            this.panelNumricPad.Controls.Add(this.btnNum_1);
            this.panelNumricPad.Location = new System.Drawing.Point(288, 257);
            this.panelNumricPad.Name = "panelNumricPad";
            this.panelNumricPad.Size = new System.Drawing.Size(237, 256);
            this.panelNumricPad.TabIndex = 96;
            // 
            // btnNum_Dot
            // 
            this.btnNum_Dot.BackColor = System.Drawing.SystemColors.Control;
            this.btnNum_Dot.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNum_Dot.Location = new System.Drawing.Point(155, 189);
            this.btnNum_Dot.Name = "btnNum_Dot";
            this.btnNum_Dot.Size = new System.Drawing.Size(76, 62);
            this.btnNum_Dot.TabIndex = 92;
            this.btnNum_Dot.Text = ".";
            this.btnNum_Dot.UseVisualStyleBackColor = false;
            this.btnNum_Dot.Click += new System.EventHandler(this.btnNum_Dot_Click);
            // 
            // btnNum_0
            // 
            this.btnNum_0.BackColor = System.Drawing.SystemColors.Control;
            this.btnNum_0.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNum_0.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnNum_0.Location = new System.Drawing.Point(79, 189);
            this.btnNum_0.Name = "btnNum_0";
            this.btnNum_0.Size = new System.Drawing.Size(76, 62);
            this.btnNum_0.TabIndex = 90;
            this.btnNum_0.Text = "0";
            this.btnNum_0.UseVisualStyleBackColor = false;
            this.btnNum_0.Click += new System.EventHandler(this.btnNum_0_Click);
            // 
            // btnBackSpace
            // 
            this.btnBackSpace.BackColor = System.Drawing.SystemColors.Control;
            this.btnBackSpace.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBackSpace.Image = global::JublFood.POS.App.Properties.Resources._175;
            this.btnBackSpace.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnBackSpace.Location = new System.Drawing.Point(3, 189);
            this.btnBackSpace.Name = "btnBackSpace";
            this.btnBackSpace.Size = new System.Drawing.Size(76, 62);
            this.btnBackSpace.TabIndex = 91;
            this.btnBackSpace.Text = "BackSpace";
            this.btnBackSpace.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnBackSpace.UseVisualStyleBackColor = false;
            this.btnBackSpace.Click += new System.EventHandler(this.btnBackSpace_Click);
            // 
            // btnNum_9
            // 
            this.btnNum_9.BackColor = System.Drawing.SystemColors.Control;
            this.btnNum_9.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNum_9.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnNum_9.Location = new System.Drawing.Point(155, 127);
            this.btnNum_9.Name = "btnNum_9";
            this.btnNum_9.Size = new System.Drawing.Size(76, 62);
            this.btnNum_9.TabIndex = 87;
            this.btnNum_9.Text = "9";
            this.btnNum_9.UseVisualStyleBackColor = false;
            this.btnNum_9.Click += new System.EventHandler(this.btnNum_9_Click);
            // 
            // btnNum_8
            // 
            this.btnNum_8.BackColor = System.Drawing.SystemColors.Control;
            this.btnNum_8.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNum_8.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnNum_8.Location = new System.Drawing.Point(79, 127);
            this.btnNum_8.Name = "btnNum_8";
            this.btnNum_8.Size = new System.Drawing.Size(76, 62);
            this.btnNum_8.TabIndex = 88;
            this.btnNum_8.Text = "8";
            this.btnNum_8.UseVisualStyleBackColor = false;
            this.btnNum_8.Click += new System.EventHandler(this.btnNum_8_Click);
            // 
            // btnNum_7
            // 
            this.btnNum_7.BackColor = System.Drawing.SystemColors.Control;
            this.btnNum_7.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNum_7.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnNum_7.Location = new System.Drawing.Point(3, 127);
            this.btnNum_7.Name = "btnNum_7";
            this.btnNum_7.Size = new System.Drawing.Size(76, 62);
            this.btnNum_7.TabIndex = 89;
            this.btnNum_7.Text = "7";
            this.btnNum_7.UseVisualStyleBackColor = false;
            this.btnNum_7.Click += new System.EventHandler(this.btnNum_7_Click);
            // 
            // btnNum_6
            // 
            this.btnNum_6.BackColor = System.Drawing.SystemColors.Control;
            this.btnNum_6.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNum_6.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnNum_6.Location = new System.Drawing.Point(155, 65);
            this.btnNum_6.Name = "btnNum_6";
            this.btnNum_6.Size = new System.Drawing.Size(76, 62);
            this.btnNum_6.TabIndex = 84;
            this.btnNum_6.Text = "6";
            this.btnNum_6.UseVisualStyleBackColor = false;
            this.btnNum_6.Click += new System.EventHandler(this.btnNum_6_Click);
            // 
            // btnNum_5
            // 
            this.btnNum_5.BackColor = System.Drawing.SystemColors.Control;
            this.btnNum_5.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNum_5.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnNum_5.Location = new System.Drawing.Point(79, 65);
            this.btnNum_5.Name = "btnNum_5";
            this.btnNum_5.Size = new System.Drawing.Size(76, 62);
            this.btnNum_5.TabIndex = 85;
            this.btnNum_5.Text = "5";
            this.btnNum_5.UseVisualStyleBackColor = false;
            this.btnNum_5.Click += new System.EventHandler(this.btnNum_5_Click);
            // 
            // btnNum_4
            // 
            this.btnNum_4.BackColor = System.Drawing.SystemColors.Control;
            this.btnNum_4.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNum_4.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnNum_4.Location = new System.Drawing.Point(3, 65);
            this.btnNum_4.Name = "btnNum_4";
            this.btnNum_4.Size = new System.Drawing.Size(76, 62);
            this.btnNum_4.TabIndex = 86;
            this.btnNum_4.Text = "4";
            this.btnNum_4.UseVisualStyleBackColor = false;
            this.btnNum_4.Click += new System.EventHandler(this.btnNum_4_Click);
            // 
            // btnNum_3
            // 
            this.btnNum_3.BackColor = System.Drawing.SystemColors.Control;
            this.btnNum_3.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNum_3.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnNum_3.Location = new System.Drawing.Point(155, 3);
            this.btnNum_3.Name = "btnNum_3";
            this.btnNum_3.Size = new System.Drawing.Size(76, 62);
            this.btnNum_3.TabIndex = 81;
            this.btnNum_3.Text = "3";
            this.btnNum_3.UseVisualStyleBackColor = false;
            this.btnNum_3.Click += new System.EventHandler(this.btnNum_3_Click);
            // 
            // btnNum_2
            // 
            this.btnNum_2.BackColor = System.Drawing.SystemColors.Control;
            this.btnNum_2.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNum_2.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnNum_2.Location = new System.Drawing.Point(79, 3);
            this.btnNum_2.Name = "btnNum_2";
            this.btnNum_2.Size = new System.Drawing.Size(76, 62);
            this.btnNum_2.TabIndex = 82;
            this.btnNum_2.Text = "2";
            this.btnNum_2.UseVisualStyleBackColor = false;
            this.btnNum_2.Click += new System.EventHandler(this.btnNum_2_Click);
            // 
            // btnNum_1
            // 
            this.btnNum_1.BackColor = System.Drawing.SystemColors.Control;
            this.btnNum_1.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNum_1.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnNum_1.Location = new System.Drawing.Point(3, 3);
            this.btnNum_1.Name = "btnNum_1";
            this.btnNum_1.Size = new System.Drawing.Size(76, 62);
            this.btnNum_1.TabIndex = 83;
            this.btnNum_1.Text = "1";
            this.btnNum_1.UseVisualStyleBackColor = false;
            this.btnNum_1.Click += new System.EventHandler(this.btnNum_1_Click);
            // 
            // panelPayType
            // 
            this.panelPayType.Controls.Add(this.cmdCash);
            this.panelPayType.Controls.Add(this.cmdDigital);
            this.panelPayType.Controls.Add(this.cmdCheque);
            this.panelPayType.Controls.Add(this.cmdSodexo);
            this.panelPayType.Controls.Add(this.cmdAccor);
            this.panelPayType.Location = new System.Drawing.Point(310, 25);
            this.panelPayType.Name = "panelPayType";
            this.panelPayType.Size = new System.Drawing.Size(404, 62);
            this.panelPayType.TabIndex = 94;
            this.panelPayType.Paint += new System.Windows.Forms.PaintEventHandler(this.panelPayType_Paint);
            // 
            // cmdCash
            // 
            this.cmdCash.BackColor = System.Drawing.SystemColors.Control;
            this.cmdCash.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdCash.Image = global::JublFood.POS.App.Properties.Resources._98;
            this.cmdCash.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.cmdCash.Location = new System.Drawing.Point(0, 2);
            this.cmdCash.Name = "cmdCash";
            this.cmdCash.Size = new System.Drawing.Size(80, 58);
            this.cmdCash.TabIndex = 100;
            this.cmdCash.Tag = "1";
            this.cmdCash.Text = "Cash";
            this.cmdCash.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.cmdCash.UseVisualStyleBackColor = false;
            this.cmdCash.Click += new System.EventHandler(this.cmdCash_Click);
            // 
            // cmdDigital
            // 
            this.cmdDigital.BackColor = System.Drawing.SystemColors.Control;
            this.cmdDigital.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdDigital.Image = global::JublFood.POS.App.Properties.Resources._106;
            this.cmdDigital.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.cmdDigital.Location = new System.Drawing.Point(160, 2);
            this.cmdDigital.Name = "cmdDigital";
            this.cmdDigital.Size = new System.Drawing.Size(79, 58);
            this.cmdDigital.TabIndex = 102;
            this.cmdDigital.Tag = "4";
            this.cmdDigital.Text = "Digital/Wallet";
            this.cmdDigital.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.cmdDigital.UseVisualStyleBackColor = false;
            this.cmdDigital.Click += new System.EventHandler(this.cmdDigital_Click);
            // 
            // cmdCheque
            // 
            this.cmdCheque.BackColor = System.Drawing.SystemColors.Control;
            this.cmdCheque.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdCheque.Image = global::JublFood.POS.App.Properties.Resources._107;
            this.cmdCheque.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.cmdCheque.Location = new System.Drawing.Point(80, 2);
            this.cmdCheque.Name = "cmdCheque";
            this.cmdCheque.Size = new System.Drawing.Size(80, 58);
            this.cmdCheque.TabIndex = 101;
            this.cmdCheque.Tag = "2";
            this.cmdCheque.Text = "Cheque";
            this.cmdCheque.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.cmdCheque.UseVisualStyleBackColor = false;
            this.cmdCheque.Click += new System.EventHandler(this.cmdCheque_Click);
            // 
            // cmdSodexo
            // 
            this.cmdSodexo.BackColor = System.Drawing.SystemColors.Control;
            this.cmdSodexo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdSodexo.Image = global::JublFood.POS.App.Properties.Resources.Sodexo;
            this.cmdSodexo.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.cmdSodexo.Location = new System.Drawing.Point(240, 2);
            this.cmdSodexo.Name = "cmdSodexo";
            this.cmdSodexo.Size = new System.Drawing.Size(80, 58);
            this.cmdSodexo.TabIndex = 98;
            this.cmdSodexo.Text = "Sodexo";
            this.cmdSodexo.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.cmdSodexo.UseVisualStyleBackColor = false;
            this.cmdSodexo.Visible = false;
            this.cmdSodexo.Click += new System.EventHandler(this.cmdSodexo_Click);
            // 
            // cmdAccor
            // 
            this.cmdAccor.BackColor = System.Drawing.SystemColors.Control;
            this.cmdAccor.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdAccor.Image = global::JublFood.POS.App.Properties.Resources._121;
            this.cmdAccor.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.cmdAccor.Location = new System.Drawing.Point(320, 2);
            this.cmdAccor.Name = "cmdAccor";
            this.cmdAccor.Size = new System.Drawing.Size(80, 58);
            this.cmdAccor.TabIndex = 99;
            this.cmdAccor.Text = "Accor";
            this.cmdAccor.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.cmdAccor.UseVisualStyleBackColor = false;
            this.cmdAccor.Visible = false;
            this.cmdAccor.Click += new System.EventHandler(this.cmdAccor_Click);
            // 
            // btndot
            // 
            this.btndot.BackColor = System.Drawing.SystemColors.Control;
            this.btndot.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btndot.Location = new System.Drawing.Point(449, 449);
            this.btndot.Name = "btndot";
            this.btndot.Size = new System.Drawing.Size(76, 62);
            this.btndot.TabIndex = 89;
            this.btndot.Text = ".";
            this.btndot.UseVisualStyleBackColor = false;
            this.btndot.Visible = false;
            this.btndot.Click += new System.EventHandler(this.btndot_Click);
            // 
            // uC_Customer_order_Header1
            // 
            this.uC_Customer_order_Header1.LabelOrderTaker = null;
            this.uC_Customer_order_Header1.Location = new System.Drawing.Point(3, 1);
            this.uC_Customer_order_Header1.Name = "uC_Customer_order_Header1";
            this.uC_Customer_order_Header1.Size = new System.Drawing.Size(787, 24);
            this.uC_Customer_order_Header1.TabIndex = 88;
            // 
            // gridPayment
            // 
            this.gridPayment.AllowUserToAddRows = false;
            this.gridPayment.AllowUserToDeleteRows = false;
            this.gridPayment.AllowUserToResizeColumns = false;
            this.gridPayment.AllowUserToResizeRows = false;
            this.gridPayment.BackgroundColor = System.Drawing.Color.LightGoldenrodYellow;
            this.gridPayment.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.gridPayment.Location = new System.Drawing.Point(0, 112);
            this.gridPayment.Name = "gridPayment";
            this.gridPayment.ReadOnly = true;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.LightGoldenrodYellow;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridPayment.RowsDefaultCellStyle = dataGridViewCellStyle1;
            this.gridPayment.Size = new System.Drawing.Size(287, 420);
            this.gridPayment.TabIndex = 87;
            this.gridPayment.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridPayment_CellClick);
            // 
            // pnl_Cheque
            // 
            this.pnl_Cheque.Controls.Add(this.txtCheckInfo);
            this.pnl_Cheque.Controls.Add(this.ltxtCheckInfo);
            this.pnl_Cheque.Location = new System.Drawing.Point(288, 204);
            this.pnl_Cheque.Name = "pnl_Cheque";
            this.pnl_Cheque.Size = new System.Drawing.Size(500, 47);
            this.pnl_Cheque.TabIndex = 84;
            this.pnl_Cheque.Visible = false;
            // 
            // txtCheckInfo
            // 
            this.txtCheckInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCheckInfo.Location = new System.Drawing.Point(3, 20);
            this.txtCheckInfo.MaxLength = 50;
            this.txtCheckInfo.Name = "txtCheckInfo";
            this.txtCheckInfo.Size = new System.Drawing.Size(494, 24);
            this.txtCheckInfo.TabIndex = 82;
            // 
            // ltxtCheckInfo
            // 
            this.ltxtCheckInfo.AutoSize = true;
            this.ltxtCheckInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ltxtCheckInfo.ForeColor = System.Drawing.Color.White;
            this.ltxtCheckInfo.Location = new System.Drawing.Point(160, 5);
            this.ltxtCheckInfo.Name = "ltxtCheckInfo";
            this.ltxtCheckInfo.Size = new System.Drawing.Size(141, 16);
            this.ltxtCheckInfo.TabIndex = 81;
            this.ltxtCheckInfo.Text = "Cheque Information";
            // 
            // uc_KeyBoardNumeric
            // 
            this.uc_KeyBoardNumeric.Location = new System.Drawing.Point(283, 257);
            this.uc_KeyBoardNumeric.Name = "uc_KeyBoardNumeric";
            this.uc_KeyBoardNumeric.Size = new System.Drawing.Size(239, 258);
            this.uc_KeyBoardNumeric.TabIndex = 80;
            this.uc_KeyBoardNumeric.txtUserID = null;
            this.uc_KeyBoardNumeric.Visible = false;
            // 
            // tdbnTip
            // 
            this.tdbnTip.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tdbnTip.Location = new System.Drawing.Point(661, 172);
            this.tdbnTip.Name = "tdbnTip";
            this.tdbnTip.Size = new System.Drawing.Size(124, 22);
            this.tdbnTip.TabIndex = 79;
            this.tdbnTip.Visible = false;
            // 
            // ltxtTip
            // 
            this.ltxtTip.AutoSize = true;
            this.ltxtTip.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ltxtTip.ForeColor = System.Drawing.Color.White;
            this.ltxtTip.Location = new System.Drawing.Point(517, 176);
            this.ltxtTip.Name = "ltxtTip";
            this.ltxtTip.Size = new System.Drawing.Size(31, 16);
            this.ltxtTip.TabIndex = 78;
            this.ltxtTip.Text = "Tip";
            this.ltxtTip.Visible = false;
            // 
            // btnDelete
            // 
            this.btnDelete.BackColor = System.Drawing.SystemColors.Control;
            this.btnDelete.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDelete.Image = global::JublFood.POS.App.Properties.Resources._97;
            this.btnDelete.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnDelete.Location = new System.Drawing.Point(710, 534);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(80, 58);
            this.btnDelete.TabIndex = 77;
            this.btnDelete.Text = "Delete";
            this.btnDelete.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // cmdKeyboard
            // 
            this.cmdKeyboard.BackColor = System.Drawing.SystemColors.Control;
            this.cmdKeyboard.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdKeyboard.Image = global::JublFood.POS.App.Properties.Resources._42;
            this.cmdKeyboard.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.cmdKeyboard.Location = new System.Drawing.Point(506, 534);
            this.cmdKeyboard.Name = "cmdKeyboard";
            this.cmdKeyboard.Size = new System.Drawing.Size(80, 58);
            this.cmdKeyboard.TabIndex = 77;
            this.cmdKeyboard.Text = "Keyboard";
            this.cmdKeyboard.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.cmdKeyboard.UseVisualStyleBackColor = false;
            this.cmdKeyboard.Visible = false;
            this.cmdKeyboard.Click += new System.EventHandler(this.cmdKeyboard_Click);
            // 
            // btnOK
            // 
            this.btnOK.BackColor = System.Drawing.SystemColors.Control;
            this.btnOK.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOK.Image = global::JublFood.POS.App.Properties.Resources._171;
            this.btnOK.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnOK.Location = new System.Drawing.Point(287, 534);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(80, 58);
            this.btnOK.TabIndex = 77;
            this.btnOK.Text = "OK";
            this.btnOK.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnOK.UseVisualStyleBackColor = false;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.SystemColors.Control;
            this.label2.Location = new System.Drawing.Point(287, 522);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(502, 10);
            this.label2.TabIndex = 76;
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblAmount
            // 
            this.lblAmount.BackColor = System.Drawing.SystemColors.Control;
            this.lblAmount.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAmount.Location = new System.Drawing.Point(137, 534);
            this.lblAmount.Name = "lblAmount";
            this.lblAmount.Size = new System.Drawing.Size(150, 57);
            this.lblAmount.TabIndex = 75;
            this.lblAmount.Text = "0";
            this.lblAmount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblAmountDue
            // 
            this.lblAmountDue.BackColor = System.Drawing.SystemColors.Control;
            this.lblAmountDue.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAmountDue.Location = new System.Drawing.Point(1, 534);
            this.lblAmountDue.Name = "lblAmountDue";
            this.lblAmountDue.Size = new System.Drawing.Size(140, 57);
            this.lblAmountDue.TabIndex = 75;
            this.lblAmountDue.Text = "Amount Due:";
            this.lblAmountDue.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tdbnAmountTendered
            // 
            this.tdbnAmountTendered.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tdbnAmountTendered.Location = new System.Drawing.Point(661, 147);
            this.tdbnAmountTendered.MaxLength = 10;
            this.tdbnAmountTendered.Name = "tdbnAmountTendered";
            this.tdbnAmountTendered.Size = new System.Drawing.Size(124, 22);
            this.tdbnAmountTendered.TabIndex = 73;
            this.tdbnAmountTendered.Text = "0.00";
            this.tdbnAmountTendered.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.tdbnAmountTendered.TextChanged += new System.EventHandler(this.tdbnAmountTendered_TextChanged);
            this.tdbnAmountTendered.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tdbnAmountTendered_KeyPress);
            // 
            // tlblAmountDue
            // 
            this.tlblAmountDue.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlblAmountDue.ForeColor = System.Drawing.Color.White;
            this.tlblAmountDue.Location = new System.Drawing.Point(661, 126);
            this.tlblAmountDue.Name = "tlblAmountDue";
            this.tlblAmountDue.Size = new System.Drawing.Size(126, 16);
            this.tlblAmountDue.TabIndex = 72;
            this.tlblAmountDue.Text = "0.00";
            this.tlblAmountDue.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // ltxtAmountTendered
            // 
            this.ltxtAmountTendered.AutoSize = true;
            this.ltxtAmountTendered.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ltxtAmountTendered.ForeColor = System.Drawing.Color.Yellow;
            this.ltxtAmountTendered.Location = new System.Drawing.Point(518, 151);
            this.ltxtAmountTendered.Name = "ltxtAmountTendered";
            this.ltxtAmountTendered.Size = new System.Drawing.Size(131, 16);
            this.ltxtAmountTendered.TabIndex = 72;
            this.ltxtAmountTendered.Text = "Amount Tendered";
            // 
            // ltxtAmountDue
            // 
            this.ltxtAmountDue.AutoSize = true;
            this.ltxtAmountDue.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ltxtAmountDue.ForeColor = System.Drawing.Color.White;
            this.ltxtAmountDue.Location = new System.Drawing.Point(518, 126);
            this.ltxtAmountDue.Name = "ltxtAmountDue";
            this.ltxtAmountDue.Size = new System.Drawing.Size(91, 16);
            this.ltxtAmountDue.TabIndex = 72;
            this.ltxtAmountDue.Text = "Amount Due";
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.SystemColors.Control;
            this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Image = global::JublFood.POS.App.Properties.Resources._9;
            this.btnSave.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnSave.Location = new System.Drawing.Point(717, 26);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(80, 58);
            this.btnSave.TabIndex = 71;
            this.btnSave.Text = "Save";
            this.btnSave.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btn_Order
            // 
            this.btn_Order.BackColor = System.Drawing.SystemColors.Control;
            this.btn_Order.Enabled = false;
            this.btn_Order.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Order.Image = global::JublFood.POS.App.Properties.Resources._175;
            this.btn_Order.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btn_Order.Location = new System.Drawing.Point(3, 25);
            this.btn_Order.Name = "btn_Order";
            this.btn_Order.Size = new System.Drawing.Size(80, 58);
            this.btn_Order.TabIndex = 70;
            this.btn_Order.Text = "Back";
            this.btn_Order.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btn_Order.UseVisualStyleBackColor = false;
            this.btn_Order.Click += new System.EventHandler(this.btn_Order_Click);
            // 
            // lblPlaceHolder
            // 
            this.lblPlaceHolder.BackColor = System.Drawing.SystemColors.Control;
            this.lblPlaceHolder.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblPlaceHolder.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPlaceHolder.Location = new System.Drawing.Point(-3, 87);
            this.lblPlaceHolder.Name = "lblPlaceHolder";
            this.lblPlaceHolder.Size = new System.Drawing.Size(803, 22);
            this.lblPlaceHolder.TabIndex = 0;
            this.lblPlaceHolder.Text = "Cash";
            this.lblPlaceHolder.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblmsg
            // 
            this.lblmsg.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblmsg.ForeColor = System.Drawing.Color.Yellow;
            this.lblmsg.Location = new System.Drawing.Point(373, 532);
            this.lblmsg.Name = "lblmsg";
            this.lblmsg.Size = new System.Drawing.Size(341, 43);
            this.lblmsg.TabIndex = 95;
            this.lblmsg.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // flPanel_CardsWallet
            // 
            this.flPanel_CardsWallet.Location = new System.Drawing.Point(566, 260);
            this.flPanel_CardsWallet.Name = "flPanel_CardsWallet";
            this.flPanel_CardsWallet.Size = new System.Drawing.Size(231, 251);
            this.flPanel_CardsWallet.TabIndex = 0;
            // 
            // pnl_amount
            // 
            this.pnl_amount.Location = new System.Drawing.Point(566, 260);
            this.pnl_amount.Name = "pnl_amount";
            this.pnl_amount.Size = new System.Drawing.Size(231, 251);
            this.pnl_amount.TabIndex = 92;
            // 
            // frmPayment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 600);
            this.ControlBox = false;
            this.Controls.Add(this.pnl_payment);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmPayment";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "PAYMENT";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmPayment_FormClosing);
            this.Load += new System.EventHandler(this.frmPayment_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmPayment_KeyDown);
            this.pnl_payment.ResumeLayout(false);
            this.pnl_payment.PerformLayout();
            this.panelNumricPad.ResumeLayout(false);
            this.panelPayType.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridPayment)).EndInit();
            this.pnl_Cheque.ResumeLayout(false);
            this.pnl_Cheque.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnl_payment;
        private System.Windows.Forms.Label lblPlaceHolder;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btn_Order;
        private System.Windows.Forms.TextBox tdbnAmountTendered;
        private System.Windows.Forms.Label tlblAmountDue;
        private System.Windows.Forms.Label ltxtAmountTendered;
        private System.Windows.Forms.Label ltxtAmountDue;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblAmount;
        private System.Windows.Forms.Label lblAmountDue;
        private System.Windows.Forms.TextBox tdbnTip;
        private System.Windows.Forms.Label ltxtTip;
        private System.Windows.Forms.TextBox txtCheckInfo;
        private System.Windows.Forms.Label ltxtCheckInfo;
        private UC_KeyBoardNumeric uc_KeyBoardNumeric;
        private System.Windows.Forms.Button cmdKeyboard;
        private System.Windows.Forms.Panel pnl_Cheque;
        private System.Windows.Forms.FlowLayoutPanel flPanel_CardsWallet;
        private System.Windows.Forms.DataGridView gridPayment;
        private UserControls.UC_Customer_order_Header uC_Customer_order_Header1;
        private System.Windows.Forms.Button btndot;
        private System.Windows.Forms.Panel panelPayType;
        private System.Windows.Forms.Button cmdDigital;
        private System.Windows.Forms.Button cmdCheque;
        private System.Windows.Forms.Button cmdCash;
        private System.Windows.Forms.Button cmdSodexo;
        private System.Windows.Forms.Button cmdAccor;
        private System.Windows.Forms.Label lblmsg;
        private System.Windows.Forms.Panel panelNumricPad;
        private System.Windows.Forms.Button btnNum_Dot;
        private System.Windows.Forms.Button btnNum_0;
        private System.Windows.Forms.Button btnBackSpace;
        private System.Windows.Forms.Button btnNum_9;
        private System.Windows.Forms.Button btnNum_8;
        private System.Windows.Forms.Button btnNum_7;
        private System.Windows.Forms.Button btnNum_6;
        private System.Windows.Forms.Button btnNum_5;
        private System.Windows.Forms.Button btnNum_4;
        private System.Windows.Forms.Button btnNum_3;
        private System.Windows.Forms.Button btnNum_2;
        private System.Windows.Forms.Button btnNum_1;
        private System.Windows.Forms.FlowLayoutPanel pnl_amount;
    }
}