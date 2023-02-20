namespace JublFood.POS.App
{
    partial class frmTimeClock
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmTimeClock));
            this.pnl_TimeClock = new System.Windows.Forms.Panel();
            this.pnl_Statistics = new System.Windows.Forms.Panel();
            this.txtDL_Expiration = new System.Windows.Forms.TextBox();
            this.txtFood_Handler_Expiration = new System.Windows.Forms.TextBox();
            this.txtInspection_Date = new System.Windows.Forms.TextBox();
            this.txtMVR_Check_Date = new System.Windows.Forms.TextBox();
            this.txtIns_Exp_Date = new System.Windows.Forms.TextBox();
            this.txtReg_Expiration = new System.Windows.Forms.TextBox();
            this.ltxtExpirationLegend_Missing = new System.Windows.Forms.Label();
            this.ltxtExpirationLegend_1Week = new System.Windows.Forms.Label();
            this.ltxtExpirationLegend_Expired = new System.Windows.Forms.Label();
            this.ltxtExpirationLegend_Active = new System.Windows.Forms.Label();
            this.ltxtExpirationLegend_Today = new System.Windows.Forms.Label();
            this.ltxtExpirationLegend_NotRequired = new System.Windows.Forms.Label();
            this.ltxtFood_Handler_Expiration = new System.Windows.Forms.Label();
            this.ltxtInspection_Date = new System.Windows.Forms.Label();
            this.ltxtMVR_Check_Date = new System.Windows.Forms.Label();
            this.ltxtIns_Exp_Date = new System.Windows.Forms.Label();
            this.ltxtReg_Expiration = new System.Windows.Forms.Label();
            this.ltxtDL_Expiration = new System.Windows.Forms.Label();
            this.ltxtExpirationDatesTitle = new System.Windows.Forms.Label();
            this.btn_OK = new System.Windows.Forms.Button();
            this.lblName = new System.Windows.Forms.Label();
            this.lblAction = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.tdbnBank = new System.Windows.Forms.TextBox();
            this.ltxtBank = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tdbnEnd_Odometer = new System.Windows.Forms.TextBox();
            this.tdbnBegin_Odometer = new System.Windows.Forms.TextBox();
            this.ltxtEnd_Odometer = new System.Windows.Forms.Label();
            this.ltxtBegin_Odometer = new System.Windows.Forms.Label();
            this.Time = new System.Windows.Forms.GroupBox();
            this.lblTime_Out = new System.Windows.Forms.TextBox();
            this.lblTime_In = new System.Windows.Forms.TextBox();
            this.ltxtTime_Out = new System.Windows.Forms.Label();
            this.ltxtTime_In = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblSystem_Date = new System.Windows.Forms.TextBox();
            this.lblShiftNumber = new System.Windows.Forms.TextBox();
            this.ltxtSystem_Date = new System.Windows.Forms.Label();
            this.ltxtShift_Number = new System.Windows.Forms.Label();
            this.txtUserID = new System.Windows.Forms.TextBox();
            this.uC_KeyBoardNumeric = new JublFood.POS.App.UC_KeyBoardNumeric();
            this.cmdExit = new System.Windows.Forms.Button();
            this.cmdComplete = new System.Windows.Forms.Button();
            this.cmdChangePassword = new System.Windows.Forms.Button();
            this.cmdStatistics = new System.Windows.Forms.Button();
            this.pnl_TimeClock.SuspendLayout();
            this.pnl_Statistics.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.Time.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnl_TimeClock
            // 
            this.pnl_TimeClock.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.pnl_TimeClock.Controls.Add(this.pnl_Statistics);
            this.pnl_TimeClock.Controls.Add(this.btn_OK);
            this.pnl_TimeClock.Controls.Add(this.lblName);
            this.pnl_TimeClock.Controls.Add(this.lblAction);
            this.pnl_TimeClock.Controls.Add(this.groupBox3);
            this.pnl_TimeClock.Controls.Add(this.groupBox2);
            this.pnl_TimeClock.Controls.Add(this.Time);
            this.pnl_TimeClock.Controls.Add(this.groupBox1);
            this.pnl_TimeClock.Controls.Add(this.txtUserID);
            this.pnl_TimeClock.Controls.Add(this.uC_KeyBoardNumeric);
            this.pnl_TimeClock.Location = new System.Drawing.Point(0, 55);
            this.pnl_TimeClock.Name = "pnl_TimeClock";
            this.pnl_TimeClock.Size = new System.Drawing.Size(684, 377);
            this.pnl_TimeClock.TabIndex = 0;
            // 
            // pnl_Statistics
            // 
            this.pnl_Statistics.BackColor = System.Drawing.Color.Teal;
            this.pnl_Statistics.Controls.Add(this.txtDL_Expiration);
            this.pnl_Statistics.Controls.Add(this.txtFood_Handler_Expiration);
            this.pnl_Statistics.Controls.Add(this.txtInspection_Date);
            this.pnl_Statistics.Controls.Add(this.txtMVR_Check_Date);
            this.pnl_Statistics.Controls.Add(this.txtIns_Exp_Date);
            this.pnl_Statistics.Controls.Add(this.txtReg_Expiration);
            this.pnl_Statistics.Controls.Add(this.ltxtExpirationLegend_Missing);
            this.pnl_Statistics.Controls.Add(this.ltxtExpirationLegend_1Week);
            this.pnl_Statistics.Controls.Add(this.ltxtExpirationLegend_Expired);
            this.pnl_Statistics.Controls.Add(this.ltxtExpirationLegend_Active);
            this.pnl_Statistics.Controls.Add(this.ltxtExpirationLegend_Today);
            this.pnl_Statistics.Controls.Add(this.ltxtExpirationLegend_NotRequired);
            this.pnl_Statistics.Controls.Add(this.ltxtFood_Handler_Expiration);
            this.pnl_Statistics.Controls.Add(this.ltxtInspection_Date);
            this.pnl_Statistics.Controls.Add(this.ltxtMVR_Check_Date);
            this.pnl_Statistics.Controls.Add(this.ltxtIns_Exp_Date);
            this.pnl_Statistics.Controls.Add(this.ltxtReg_Expiration);
            this.pnl_Statistics.Controls.Add(this.ltxtDL_Expiration);
            this.pnl_Statistics.Controls.Add(this.ltxtExpirationDatesTitle);
            this.pnl_Statistics.Location = new System.Drawing.Point(655, 36);
            this.pnl_Statistics.Name = "pnl_Statistics";
            this.pnl_Statistics.Size = new System.Drawing.Size(358, 311);
            this.pnl_Statistics.TabIndex = 15;
            this.pnl_Statistics.Visible = false;
            // 
            // txtDL_Expiration
            // 
            this.txtDL_Expiration.Location = new System.Drawing.Point(202, 43);
            this.txtDL_Expiration.Name = "txtDL_Expiration";
            this.txtDL_Expiration.Size = new System.Drawing.Size(122, 20);
            this.txtDL_Expiration.TabIndex = 7;
            // 
            // txtFood_Handler_Expiration
            // 
            this.txtFood_Handler_Expiration.Location = new System.Drawing.Point(202, 208);
            this.txtFood_Handler_Expiration.Name = "txtFood_Handler_Expiration";
            this.txtFood_Handler_Expiration.Size = new System.Drawing.Size(122, 20);
            this.txtFood_Handler_Expiration.TabIndex = 6;
            // 
            // txtInspection_Date
            // 
            this.txtInspection_Date.Location = new System.Drawing.Point(202, 174);
            this.txtInspection_Date.Name = "txtInspection_Date";
            this.txtInspection_Date.Size = new System.Drawing.Size(122, 20);
            this.txtInspection_Date.TabIndex = 5;
            // 
            // txtMVR_Check_Date
            // 
            this.txtMVR_Check_Date.Location = new System.Drawing.Point(202, 141);
            this.txtMVR_Check_Date.Name = "txtMVR_Check_Date";
            this.txtMVR_Check_Date.Size = new System.Drawing.Size(122, 20);
            this.txtMVR_Check_Date.TabIndex = 4;
            // 
            // txtIns_Exp_Date
            // 
            this.txtIns_Exp_Date.Location = new System.Drawing.Point(202, 109);
            this.txtIns_Exp_Date.Name = "txtIns_Exp_Date";
            this.txtIns_Exp_Date.Size = new System.Drawing.Size(122, 20);
            this.txtIns_Exp_Date.TabIndex = 3;
            // 
            // txtReg_Expiration
            // 
            this.txtReg_Expiration.Location = new System.Drawing.Point(202, 77);
            this.txtReg_Expiration.Name = "txtReg_Expiration";
            this.txtReg_Expiration.Size = new System.Drawing.Size(122, 20);
            this.txtReg_Expiration.TabIndex = 2;
            // 
            // ltxtExpirationLegend_Missing
            // 
            this.ltxtExpirationLegend_Missing.AutoSize = true;
            this.ltxtExpirationLegend_Missing.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.ltxtExpirationLegend_Missing.ForeColor = System.Drawing.Color.Yellow;
            this.ltxtExpirationLegend_Missing.Location = new System.Drawing.Point(199, 265);
            this.ltxtExpirationLegend_Missing.Name = "ltxtExpirationLegend_Missing";
            this.ltxtExpirationLegend_Missing.Size = new System.Drawing.Size(42, 13);
            this.ltxtExpirationLegend_Missing.TabIndex = 1;
            this.ltxtExpirationLegend_Missing.Text = "Missing";
            // 
            // ltxtExpirationLegend_1Week
            // 
            this.ltxtExpirationLegend_1Week.AutoSize = true;
            this.ltxtExpirationLegend_1Week.BackColor = System.Drawing.Color.Lime;
            this.ltxtExpirationLegend_1Week.Location = new System.Drawing.Point(199, 244);
            this.ltxtExpirationLegend_1Week.Name = "ltxtExpirationLegend_1Week";
            this.ltxtExpirationLegend_1Week.Size = new System.Drawing.Size(42, 13);
            this.ltxtExpirationLegend_1Week.TabIndex = 1;
            this.ltxtExpirationLegend_1Week.Text = "1-week";
            // 
            // ltxtExpirationLegend_Expired
            // 
            this.ltxtExpirationLegend_Expired.AutoSize = true;
            this.ltxtExpirationLegend_Expired.BackColor = System.Drawing.Color.Red;
            this.ltxtExpirationLegend_Expired.ForeColor = System.Drawing.Color.Yellow;
            this.ltxtExpirationLegend_Expired.Location = new System.Drawing.Point(126, 265);
            this.ltxtExpirationLegend_Expired.Name = "ltxtExpirationLegend_Expired";
            this.ltxtExpirationLegend_Expired.Size = new System.Drawing.Size(42, 13);
            this.ltxtExpirationLegend_Expired.TabIndex = 1;
            this.ltxtExpirationLegend_Expired.Text = "Expired";
            // 
            // ltxtExpirationLegend_Active
            // 
            this.ltxtExpirationLegend_Active.AutoSize = true;
            this.ltxtExpirationLegend_Active.Location = new System.Drawing.Point(126, 244);
            this.ltxtExpirationLegend_Active.Name = "ltxtExpirationLegend_Active";
            this.ltxtExpirationLegend_Active.Size = new System.Drawing.Size(37, 13);
            this.ltxtExpirationLegend_Active.TabIndex = 1;
            this.ltxtExpirationLegend_Active.Text = "Active";
            // 
            // ltxtExpirationLegend_Today
            // 
            this.ltxtExpirationLegend_Today.AutoSize = true;
            this.ltxtExpirationLegend_Today.BackColor = System.Drawing.Color.Yellow;
            this.ltxtExpirationLegend_Today.Location = new System.Drawing.Point(63, 265);
            this.ltxtExpirationLegend_Today.Name = "ltxtExpirationLegend_Today";
            this.ltxtExpirationLegend_Today.Size = new System.Drawing.Size(37, 13);
            this.ltxtExpirationLegend_Today.TabIndex = 1;
            this.ltxtExpirationLegend_Today.Text = "Today";
            // 
            // ltxtExpirationLegend_NotRequired
            // 
            this.ltxtExpirationLegend_NotRequired.AutoSize = true;
            this.ltxtExpirationLegend_NotRequired.BackColor = System.Drawing.Color.LightGray;
            this.ltxtExpirationLegend_NotRequired.Location = new System.Drawing.Point(63, 244);
            this.ltxtExpirationLegend_NotRequired.Name = "ltxtExpirationLegend_NotRequired";
            this.ltxtExpirationLegend_NotRequired.Size = new System.Drawing.Size(47, 13);
            this.ltxtExpirationLegend_NotRequired.TabIndex = 1;
            this.ltxtExpirationLegend_NotRequired.Text = "Not Req";
            // 
            // ltxtFood_Handler_Expiration
            // 
            this.ltxtFood_Handler_Expiration.AutoSize = true;
            this.ltxtFood_Handler_Expiration.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ltxtFood_Handler_Expiration.ForeColor = System.Drawing.Color.White;
            this.ltxtFood_Handler_Expiration.Location = new System.Drawing.Point(61, 210);
            this.ltxtFood_Handler_Expiration.Name = "ltxtFood_Handler_Expiration";
            this.ltxtFood_Handler_Expiration.Size = new System.Drawing.Size(110, 16);
            this.ltxtFood_Handler_Expiration.TabIndex = 0;
            this.ltxtFood_Handler_Expiration.Text = "Food Handling";
            // 
            // ltxtInspection_Date
            // 
            this.ltxtInspection_Date.AutoSize = true;
            this.ltxtInspection_Date.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ltxtInspection_Date.ForeColor = System.Drawing.Color.White;
            this.ltxtInspection_Date.Location = new System.Drawing.Point(64, 176);
            this.ltxtInspection_Date.Name = "ltxtInspection_Date";
            this.ltxtInspection_Date.Size = new System.Drawing.Size(107, 16);
            this.ltxtInspection_Date.TabIndex = 0;
            this.ltxtInspection_Date.Text = "Car Inspection";
            // 
            // ltxtMVR_Check_Date
            // 
            this.ltxtMVR_Check_Date.AutoSize = true;
            this.ltxtMVR_Check_Date.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ltxtMVR_Check_Date.ForeColor = System.Drawing.Color.White;
            this.ltxtMVR_Check_Date.Location = new System.Drawing.Point(46, 143);
            this.ltxtMVR_Check_Date.Name = "ltxtMVR_Check_Date";
            this.ltxtMVR_Check_Date.Size = new System.Drawing.Size(125, 16);
            this.ltxtMVR_Check_Date.TabIndex = 0;
            this.ltxtMVR_Check_Date.Text = "MVR Check Date";
            // 
            // ltxtIns_Exp_Date
            // 
            this.ltxtIns_Exp_Date.AutoSize = true;
            this.ltxtIns_Exp_Date.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ltxtIns_Exp_Date.ForeColor = System.Drawing.Color.White;
            this.ltxtIns_Exp_Date.Location = new System.Drawing.Point(23, 111);
            this.ltxtIns_Exp_Date.Name = "ltxtIns_Exp_Date";
            this.ltxtIns_Exp_Date.Size = new System.Drawing.Size(148, 16);
            this.ltxtIns_Exp_Date.TabIndex = 0;
            this.ltxtIns_Exp_Date.Text = "Insurance Expiration";
            // 
            // ltxtReg_Expiration
            // 
            this.ltxtReg_Expiration.AutoSize = true;
            this.ltxtReg_Expiration.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ltxtReg_Expiration.ForeColor = System.Drawing.Color.White;
            this.ltxtReg_Expiration.Location = new System.Drawing.Point(51, 79);
            this.ltxtReg_Expiration.Name = "ltxtReg_Expiration";
            this.ltxtReg_Expiration.Size = new System.Drawing.Size(120, 16);
            this.ltxtReg_Expiration.TabIndex = 0;
            this.ltxtReg_Expiration.Text = "Car Registration";
            // 
            // ltxtDL_Expiration
            // 
            this.ltxtDL_Expiration.AutoSize = true;
            this.ltxtDL_Expiration.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ltxtDL_Expiration.ForeColor = System.Drawing.Color.White;
            this.ltxtDL_Expiration.Location = new System.Drawing.Point(55, 45);
            this.ltxtDL_Expiration.Name = "ltxtDL_Expiration";
            this.ltxtDL_Expiration.Size = new System.Drawing.Size(116, 16);
            this.ltxtDL_Expiration.TabIndex = 0;
            this.ltxtDL_Expiration.Text = "Drivers License";
            // 
            // ltxtExpirationDatesTitle
            // 
            this.ltxtExpirationDatesTitle.AutoSize = true;
            this.ltxtExpirationDatesTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ltxtExpirationDatesTitle.ForeColor = System.Drawing.Color.White;
            this.ltxtExpirationDatesTitle.Location = new System.Drawing.Point(29, 13);
            this.ltxtExpirationDatesTitle.Name = "ltxtExpirationDatesTitle";
            this.ltxtExpirationDatesTitle.Size = new System.Drawing.Size(142, 20);
            this.ltxtExpirationDatesTitle.TabIndex = 0;
            this.ltxtExpirationDatesTitle.Text = "Expiration Dates";
            // 
            // btn_OK
            // 
            this.btn_OK.BackColor = System.Drawing.Color.PeachPuff;
            this.btn_OK.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_OK.Location = new System.Drawing.Point(168, 262);
            this.btn_OK.Name = "btn_OK";
            this.btn_OK.Size = new System.Drawing.Size(77, 62);
            this.btn_OK.TabIndex = 27;
            this.btn_OK.Text = "OK";
            this.btn_OK.UseVisualStyleBackColor = false;
            this.btn_OK.Click += new System.EventHandler(this.btn_OK_Click);
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblName.Location = new System.Drawing.Point(319, 14);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(69, 18);
            this.lblName.TabIndex = 8;
            this.lblName.Text = "lblName";
            this.lblName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblAction
            // 
            this.lblAction.AutoSize = true;
            this.lblAction.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAction.Location = new System.Drawing.Point(96, 14);
            this.lblAction.Name = "lblAction";
            this.lblAction.Size = new System.Drawing.Size(72, 18);
            this.lblAction.TabIndex = 8;
            this.lblAction.Text = "lblAction";
            this.lblAction.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.tdbnBank);
            this.groupBox3.Controls.Add(this.ltxtBank);
            this.groupBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.Location = new System.Drawing.Point(308, 291);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(341, 43);
            this.groupBox3.TabIndex = 14;
            this.groupBox3.TabStop = false;
            // 
            // tdbnBank
            // 
            this.tdbnBank.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tdbnBank.Location = new System.Drawing.Point(141, 13);
            this.tdbnBank.Name = "tdbnBank";
            this.tdbnBank.ReadOnly = true;
            this.tdbnBank.Size = new System.Drawing.Size(176, 20);
            this.tdbnBank.TabIndex = 13;
            this.tdbnBank.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // ltxtBank
            // 
            this.ltxtBank.AutoSize = true;
            this.ltxtBank.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ltxtBank.Location = new System.Drawing.Point(6, 13);
            this.ltxtBank.Name = "ltxtBank";
            this.ltxtBank.Size = new System.Drawing.Size(45, 18);
            this.ltxtBank.TabIndex = 12;
            this.ltxtBank.Text = "Bank";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.tdbnEnd_Odometer);
            this.groupBox2.Controls.Add(this.tdbnBegin_Odometer);
            this.groupBox2.Controls.Add(this.ltxtEnd_Odometer);
            this.groupBox2.Controls.Add(this.ltxtBegin_Odometer);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(308, 200);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(341, 88);
            this.groupBox2.TabIndex = 14;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Mileage";
            // 
            // tdbnEnd_Odometer
            // 
            this.tdbnEnd_Odometer.Enabled = false;
            this.tdbnEnd_Odometer.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tdbnEnd_Odometer.Location = new System.Drawing.Point(141, 58);
            this.tdbnEnd_Odometer.MaxLength = 6;
            this.tdbnEnd_Odometer.Name = "tdbnEnd_Odometer";
            this.tdbnEnd_Odometer.Size = new System.Drawing.Size(176, 20);
            this.tdbnEnd_Odometer.TabIndex = 13;
            this.tdbnEnd_Odometer.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.tdbnEnd_Odometer.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tdbnEnd_Odometer_KeyPress);
            // 
            // tdbnBegin_Odometer
            // 
            this.tdbnBegin_Odometer.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tdbnBegin_Odometer.Location = new System.Drawing.Point(141, 29);
            this.tdbnBegin_Odometer.MaxLength = 6;
            this.tdbnBegin_Odometer.Name = "tdbnBegin_Odometer";
            this.tdbnBegin_Odometer.Size = new System.Drawing.Size(176, 20);
            this.tdbnBegin_Odometer.TabIndex = 13;
            this.tdbnBegin_Odometer.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.tdbnBegin_Odometer.Enter += new System.EventHandler(this.tdbnBegin_Odometer_Enter);
            this.tdbnBegin_Odometer.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tdbnBegin_Odometer_KeyPress);
            // 
            // ltxtEnd_Odometer
            // 
            this.ltxtEnd_Odometer.AutoSize = true;
            this.ltxtEnd_Odometer.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ltxtEnd_Odometer.Location = new System.Drawing.Point(6, 58);
            this.ltxtEnd_Odometer.Name = "ltxtEnd_Odometer";
            this.ltxtEnd_Odometer.Size = new System.Drawing.Size(116, 18);
            this.ltxtEnd_Odometer.TabIndex = 12;
            this.ltxtEnd_Odometer.Text = "End Odometer";
            // 
            // ltxtBegin_Odometer
            // 
            this.ltxtBegin_Odometer.AutoSize = true;
            this.ltxtBegin_Odometer.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ltxtBegin_Odometer.Location = new System.Drawing.Point(6, 29);
            this.ltxtBegin_Odometer.Name = "ltxtBegin_Odometer";
            this.ltxtBegin_Odometer.Size = new System.Drawing.Size(129, 18);
            this.ltxtBegin_Odometer.TabIndex = 12;
            this.ltxtBegin_Odometer.Text = "Begin Odometer";
            // 
            // Time
            // 
            this.Time.Controls.Add(this.lblTime_Out);
            this.Time.Controls.Add(this.lblTime_In);
            this.Time.Controls.Add(this.ltxtTime_Out);
            this.Time.Controls.Add(this.ltxtTime_In);
            this.Time.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Time.Location = new System.Drawing.Point(308, 117);
            this.Time.Name = "Time";
            this.Time.Size = new System.Drawing.Size(341, 80);
            this.Time.TabIndex = 12;
            this.Time.TabStop = false;
            this.Time.Text = "Time Clock";
            // 
            // lblTime_Out
            // 
            this.lblTime_Out.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTime_Out.Location = new System.Drawing.Point(141, 53);
            this.lblTime_Out.Name = "lblTime_Out";
            this.lblTime_Out.ReadOnly = true;
            this.lblTime_Out.Size = new System.Drawing.Size(176, 20);
            this.lblTime_Out.TabIndex = 13;
            // 
            // lblTime_In
            // 
            this.lblTime_In.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTime_In.Location = new System.Drawing.Point(141, 27);
            this.lblTime_In.Name = "lblTime_In";
            this.lblTime_In.ReadOnly = true;
            this.lblTime_In.Size = new System.Drawing.Size(176, 20);
            this.lblTime_In.TabIndex = 13;
            this.lblTime_In.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // ltxtTime_Out
            // 
            this.ltxtTime_Out.AutoSize = true;
            this.ltxtTime_Out.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ltxtTime_Out.Location = new System.Drawing.Point(6, 52);
            this.ltxtTime_Out.Name = "ltxtTime_Out";
            this.ltxtTime_Out.Size = new System.Drawing.Size(35, 18);
            this.ltxtTime_Out.TabIndex = 12;
            this.ltxtTime_Out.Text = "Out";
            // 
            // ltxtTime_In
            // 
            this.ltxtTime_In.AutoSize = true;
            this.ltxtTime_In.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ltxtTime_In.Location = new System.Drawing.Point(6, 27);
            this.ltxtTime_In.Name = "ltxtTime_In";
            this.ltxtTime_In.Size = new System.Drawing.Size(22, 18);
            this.ltxtTime_In.TabIndex = 12;
            this.ltxtTime_In.Text = "In";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblSystem_Date);
            this.groupBox1.Controls.Add(this.lblShiftNumber);
            this.groupBox1.Controls.Add(this.ltxtSystem_Date);
            this.groupBox1.Controls.Add(this.ltxtShift_Number);
            this.groupBox1.Location = new System.Drawing.Point(308, 36);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(341, 76);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            // 
            // lblSystem_Date
            // 
            this.lblSystem_Date.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSystem_Date.Location = new System.Drawing.Point(141, 46);
            this.lblSystem_Date.Multiline = true;
            this.lblSystem_Date.Name = "lblSystem_Date";
            this.lblSystem_Date.ReadOnly = true;
            this.lblSystem_Date.Size = new System.Drawing.Size(176, 20);
            this.lblSystem_Date.TabIndex = 13;
            this.lblSystem_Date.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblShiftNumber
            // 
            this.lblShiftNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblShiftNumber.Location = new System.Drawing.Point(141, 15);
            this.lblShiftNumber.Multiline = true;
            this.lblShiftNumber.Name = "lblShiftNumber";
            this.lblShiftNumber.ReadOnly = true;
            this.lblShiftNumber.Size = new System.Drawing.Size(176, 20);
            this.lblShiftNumber.TabIndex = 13;
            this.lblShiftNumber.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // ltxtSystem_Date
            // 
            this.ltxtSystem_Date.AutoSize = true;
            this.ltxtSystem_Date.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ltxtSystem_Date.Location = new System.Drawing.Point(6, 46);
            this.ltxtSystem_Date.Name = "ltxtSystem_Date";
            this.ltxtSystem_Date.Size = new System.Drawing.Size(129, 18);
            this.ltxtSystem_Date.TabIndex = 12;
            this.ltxtSystem_Date.Text = "Accounting Date";
            // 
            // ltxtShift_Number
            // 
            this.ltxtShift_Number.AutoSize = true;
            this.ltxtShift_Number.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ltxtShift_Number.Location = new System.Drawing.Point(6, 16);
            this.ltxtShift_Number.Name = "ltxtShift_Number";
            this.ltxtShift_Number.Size = new System.Drawing.Size(105, 18);
            this.ltxtShift_Number.TabIndex = 12;
            this.ltxtShift_Number.Text = "Shift Number";
            // 
            // txtUserID
            // 
            this.txtUserID.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUserID.Location = new System.Drawing.Point(16, 44);
            this.txtUserID.MaxLength = 8;
            this.txtUserID.Name = "txtUserID";
            this.txtUserID.PasswordChar = '*';
            this.txtUserID.Size = new System.Drawing.Size(229, 26);
            this.txtUserID.TabIndex = 0;
            this.txtUserID.TextChanged += new System.EventHandler(this.txtUserID_TextChanged);
            this.txtUserID.Enter += new System.EventHandler(this.txtUserID_Enter);
            this.txtUserID.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtUserID_KeyDown);
            this.txtUserID.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtUserID_KeyPress);
            // 
            // uC_KeyBoardNumeric
            // 
            this.uC_KeyBoardNumeric.Location = new System.Drawing.Point(11, 72);
            this.uC_KeyBoardNumeric.Name = "uC_KeyBoardNumeric";
            this.uC_KeyBoardNumeric.Size = new System.Drawing.Size(246, 279);
            this.uC_KeyBoardNumeric.TabIndex = 16;
            this.uC_KeyBoardNumeric.txtUserID = null;
            // 
            // cmdExit
            // 
            this.cmdExit.BackColor = System.Drawing.Color.PeachPuff;
            this.cmdExit.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.cmdExit.Image = ((System.Drawing.Image)(resources.GetObject("cmdExit.Image")));
            this.cmdExit.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.cmdExit.Location = new System.Drawing.Point(1, 0);
            this.cmdExit.Name = "cmdExit";
            this.cmdExit.Size = new System.Drawing.Size(68, 55);
            this.cmdExit.TabIndex = 1;
            this.cmdExit.Text = "Exit";
            this.cmdExit.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.cmdExit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.cmdExit.UseVisualStyleBackColor = false;
            this.cmdExit.Click += new System.EventHandler(this.cmdExit_Click);
            // 
            // cmdComplete
            // 
            this.cmdComplete.BackColor = System.Drawing.Color.PeachPuff;
            this.cmdComplete.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.cmdComplete.Image = ((System.Drawing.Image)(resources.GetObject("cmdComplete.Image")));
            this.cmdComplete.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.cmdComplete.Location = new System.Drawing.Point(68, 0);
            this.cmdComplete.Name = "cmdComplete";
            this.cmdComplete.Size = new System.Drawing.Size(68, 55);
            this.cmdComplete.TabIndex = 1;
            this.cmdComplete.Text = "Complete";
            this.cmdComplete.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.cmdComplete.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.cmdComplete.UseVisualStyleBackColor = false;
            this.cmdComplete.Click += new System.EventHandler(this.cmdComplete_Click);
            // 
            // cmdChangePassword
            // 
            this.cmdChangePassword.BackColor = System.Drawing.Color.PeachPuff;
            this.cmdChangePassword.Enabled = false;
            this.cmdChangePassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.cmdChangePassword.Image = ((System.Drawing.Image)(resources.GetObject("cmdChangePassword.Image")));
            this.cmdChangePassword.Location = new System.Drawing.Point(135, 0);
            this.cmdChangePassword.Name = "cmdChangePassword";
            this.cmdChangePassword.Size = new System.Drawing.Size(68, 55);
            this.cmdChangePassword.TabIndex = 1;
            this.cmdChangePassword.Text = "Change Password";
            this.cmdChangePassword.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.cmdChangePassword.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.cmdChangePassword.UseVisualStyleBackColor = false;
            this.cmdChangePassword.Click += new System.EventHandler(this.cmdChangePassword_Click);
            // 
            // cmdStatistics
            // 
            this.cmdStatistics.BackColor = System.Drawing.Color.PeachPuff;
            this.cmdStatistics.Enabled = false;
            this.cmdStatistics.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.cmdStatistics.Image = ((System.Drawing.Image)(resources.GetObject("cmdStatistics.Image")));
            this.cmdStatistics.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.cmdStatistics.Location = new System.Drawing.Point(202, 0);
            this.cmdStatistics.Name = "cmdStatistics";
            this.cmdStatistics.Size = new System.Drawing.Size(68, 55);
            this.cmdStatistics.TabIndex = 1;
            this.cmdStatistics.Text = "Statistics";
            this.cmdStatistics.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.cmdStatistics.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.cmdStatistics.UseVisualStyleBackColor = false;
            this.cmdStatistics.Click += new System.EventHandler(this.cmdStatistics_Click);
            // 
            // frmTimeClock
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Teal;
            this.ClientSize = new System.Drawing.Size(678, 434);
            this.ControlBox = false;
            this.Controls.Add(this.pnl_TimeClock);
            this.Controls.Add(this.cmdStatistics);
            this.Controls.Add(this.cmdChangePassword);
            this.Controls.Add(this.cmdComplete);
            this.Controls.Add(this.cmdExit);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmTimeClock";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Time Clock";
            this.Activated += new System.EventHandler(this.frmTimeClock_Activated);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmTimeClock_FormClosing);
            this.Load += new System.EventHandler(this.frmTimeClock_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmTimeClock_KeyDown);
            this.pnl_TimeClock.ResumeLayout(false);
            this.pnl_TimeClock.PerformLayout();
            this.pnl_Statistics.ResumeLayout(false);
            this.pnl_Statistics.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.Time.ResumeLayout(false);
            this.Time.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnl_TimeClock;
        private System.Windows.Forms.TextBox txtUserID;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox lblSystem_Date;
        private System.Windows.Forms.TextBox lblShiftNumber;
        private System.Windows.Forms.Label ltxtSystem_Date;
        private System.Windows.Forms.Label ltxtShift_Number;
        private System.Windows.Forms.GroupBox Time;
        private System.Windows.Forms.TextBox lblTime_In;
        private System.Windows.Forms.Label ltxtTime_Out;
        private System.Windows.Forms.Label ltxtTime_In;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox tdbnBank;
        private System.Windows.Forms.Label ltxtBank;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox tdbnEnd_Odometer;
        private System.Windows.Forms.TextBox tdbnBegin_Odometer;
        private System.Windows.Forms.Label ltxtEnd_Odometer;
        private System.Windows.Forms.Label ltxtBegin_Odometer;
        private System.Windows.Forms.Button cmdExit;
        private System.Windows.Forms.Button cmdComplete;
        private System.Windows.Forms.Button cmdChangePassword;
        private System.Windows.Forms.Panel pnl_Statistics;
        private System.Windows.Forms.Label ltxtFood_Handler_Expiration;
        private System.Windows.Forms.Label ltxtInspection_Date;
        private System.Windows.Forms.Label ltxtMVR_Check_Date;
        private System.Windows.Forms.Label ltxtIns_Exp_Date;
        private System.Windows.Forms.Label ltxtReg_Expiration;
        private System.Windows.Forms.Label ltxtDL_Expiration;
        private System.Windows.Forms.Label ltxtExpirationDatesTitle;
        private System.Windows.Forms.Label ltxtExpirationLegend_Missing;
        private System.Windows.Forms.Label ltxtExpirationLegend_1Week;
        private System.Windows.Forms.Label ltxtExpirationLegend_Expired;
        private System.Windows.Forms.Label ltxtExpirationLegend_Active;
        private System.Windows.Forms.Label ltxtExpirationLegend_Today;
        private System.Windows.Forms.Label ltxtExpirationLegend_NotRequired;
        private System.Windows.Forms.TextBox txtDL_Expiration;
        private System.Windows.Forms.TextBox txtFood_Handler_Expiration;
        private System.Windows.Forms.TextBox txtInspection_Date;
        private System.Windows.Forms.TextBox txtMVR_Check_Date;
        private System.Windows.Forms.TextBox txtIns_Exp_Date;
        private System.Windows.Forms.TextBox txtReg_Expiration;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblAction;
        private UC_KeyBoardNumeric uC_KeyBoardNumeric;
        private System.Windows.Forms.Button btn_OK;
        private System.Windows.Forms.TextBox lblTime_Out;
        private System.Windows.Forms.Button cmdStatistics;
    }
}