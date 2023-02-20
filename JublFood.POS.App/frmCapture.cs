using Jublfood.AppLogger;
using JublFood.POS.App.API;
using JublFood.POS.App.Cache;
using JublFood.POS.App.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace JublFood.POS.App
{
    public partial class frmCapture : Form
    {        
        int pbytMinPhoneDigits = 0;
        public bool mblnNameVisible = false;
        public bool mblnPhoneVisible = false;
        public bool mblnTentNumberVisible = false;

        public bool pblnCancel = false;
        public TextBox strActiveControl = null;
        public bool blnDeliveryCaptureInfo = false;
        public bool returnValue = false;

        Dictionary<string, CustomerKeyBoardInfo> DictKeyBoardInfo;
        CustomerKeyBoardInfo objCustomerKeyBoardInfo;
        TextBox txtFocusedtextBox;

        public frmCapture()
        {
            InitializeComponent();           
        }

        public frmCapture(bool blnDeliveryCapture)
        {
            InitializeComponent();
            if (blnDeliveryCapture)
                returnValue = DeliveryPOSCapture();
            else
                returnValue = QuickServiceCapture();
        }

        private void frmCapture_Load(object sender, EventArgs e)
        {
            
            pbytMinPhoneDigits = Session.MaxPhoneDigits;
            txtName.Text = Session.cart.Customer.Name;
            txtTentNumber.Text = Session.cart.cartHeader.Tent_Number;
            tdbmPhone_Number.Text = Session.cart.Customer.Phone_Number;
            tdbtxtPhone_Ext.Text = Session.cart.Customer.Phone_Ext;
            if (blnDeliveryCaptureInfo)
                returnValue = DeliveryPOSCaptureInfo();
            else
                returnValue = QuickServiceCaptureInfo();

            if (!returnValue )
            {
                this.Close();
            }
            else
            {
                txtName.GotFocus += TxtName_GotFocus;
                txtName.LostFocus += TxtName_LostFocus;
                txtTentNumber.GotFocus += TxtTentNumber_GotFocus;
                txtTentNumber.LostFocus += TxtTentNumber_LostFocus;
                tdbmPhone_Number.GotFocus += TdbmPhone_Number_GotFocus;
                tdbtxtPhone_Ext.GotFocus += TdbtxtPhone_Ext_GotFocus;
            }

            LoadKeyBoardInfo();
            tdbmPhone_Number.MaxLength = Session.MaxPhoneDigits; 
        }

        private void TxtTentNumber_LostFocus(object sender, EventArgs e)
        {
            txtTentNumber.Text = txtTentNumber.Text.ToUpper();
        }

        private void TxtName_LostFocus(object sender, EventArgs e)
        {
            txtName.Text = txtName.Text.ToUpper();
        }

        private void TdbtxtPhone_Ext_GotFocus(object sender, EventArgs e)
        {
            SetActiveControlName();
        }

        private void TdbmPhone_Number_GotFocus(object sender, EventArgs e)
        {
            SetActiveControlName();
        }

        private void TxtTentNumber_GotFocus(object sender, EventArgs e)
        {
            SetActiveControlName();
        }

        private void TxtName_GotFocus(object sender, EventArgs e)
        {
            SetActiveControlName();
        }

        private void frmCapture_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == 13)
                    btnOK_Click(new object(), new EventArgs());
            }
            catch(Exception ex)
            {
                Logger.Trace("ERROR", "frmCapture-frmCapture_KeyPress(): " + ex.Message, ex, true);
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                if ((txtName.Text.Trim().Length == 0 && txtName.Visible) || (lblPhoneNumber.Visible && (tdbmPhone_Number.Text.Trim().Length < Session.MinPhoneDigits)))
                {
                    CustomMessageBox.Show(APILayer.GetCatalogText(LanguageConstant.cintMSGAllRequiredFieldsYellow), CustomMessageBox.Buttons.OK, CustomMessageBox.Icon.Exclamation);
                }
                else
                {
                    Session.cart.cartHeader.Customer_Name = txtName.Text;
                    Session.cart.Customer.Name = txtName.Text;
                    Session.cart.Customer.Location_Code = Session._LocationCode;
                    Session.cart.Customer.Name = txtName.Text;
                    Session.cart.Customer.Location_Code = Session._LocationCode;
                    if (lblPhoneNumber.Visible)
                    {
                        Session.cart.Customer.Phone_Number = tdbmPhone_Number.Text;
                        Session.cart.Customer.Phone_Ext = tdbtxtPhone_Ext.Text;
                        Session.cart.Customer.Phone_Number = tdbmPhone_Number.Text;
                        Session.cart.Customer.Phone_Ext= tdbtxtPhone_Ext.Text;
                    }
                    Session.cart.Customer.Added_By = Session.CurrentEmployee.LoginDetail.EmployeeCode;
                    Session.cart.Customer.Added_By = Session.CurrentEmployee.LoginDetail.EmployeeCode;
                    Session.cart.cartHeader.Tent_Number = txtTentNumber.Text;

                    pblnCancel = false;

                    this.Close();
                }
            }
            catch (Exception ex)
            {
                Logger.Trace("ERROR", "frmCapture-btnOK_Click(): " + ex.Message, ex, true);
            }
        }

        public void RePositionControls()
        {
            if (mblnPhoneVisible)
            {
                if (!mblnTentNumberVisible)
                {
                    btnCancel.Top = txtTentNumber.Top + 10;
                    btnKeyboard.Top = txtTentNumber.Top + 10;
                    btnOK.Top = txtTentNumber.Top + 10;
                }
            }
            else if (mblnNameVisible)
            {
                if (mblnTentNumberVisible)
                {
                    btnCancel.Top = txtTentNumber.Top +10;
                    btnKeyboard.Top = txtTentNumber.Top + 10;
                    btnOK.Top = txtTentNumber.Top + 10;

                    lblTentNumber.Top = lblName.Top;
                    txtTentNumber.Top = lblName.Top;

                    lblName.Top = lblPhoneNumber.Top;
                    txtName.Top = lblPhoneNumber.Top;

                    this.Height = btnOK.Top + btnOK.Height + 50;

                }
                else
                {
                    btnCancel.Top = txtCustomerLookup.Top + 10;
                    btnKeyboard.Top = txtCustomerLookup.Top + 10;
                    btnOK.Top = txtCustomerLookup.Top + 10;

                    lblName.Top = lblPhoneNumber.Top;
                    txtName.Top = lblPhoneNumber.Top;

                    this.Height = btnOK.Top + btnOK.Height + 50;
                }
            }
            else
            {
                lblTentNumber.Top = lblPhoneNumber.Top;
                txtTentNumber.Top = lblTentNumber.Top;

                btnCancel.Top = txtCustomerLookup.Top + 10;
                btnKeyboard.Top = txtCustomerLookup.Top + 10;
                btnOK.Top = txtCustomerLookup.Top + 10;

                this.Height = btnOK.Top + btnOK.Height + 50;

            }

        }

        public bool QuickServiceCaptureInfo()
        {
            bool returnValue = false;
            if (Session.pblnChargeOrder && Session.cart.Customer.Customer_Code == 0)
            {
                tdbmPhone_Number.Visible = true;
                lblPhoneNumber.Visible = true;
                txtCustomerLookup.Visible = true;
                lblCustomerLookup.Visible = true;
                lblName.Visible = true;
                txtName.Visible = true;
                lblExt.Visible = true;
                tdbtxtPhone_Ext.Visible = true;

                mblnNameVisible = true;
                mblnPhoneVisible = true;

                if((SystemSettings.settings.pblnPromptTentCarryOut && Session.cart.cartHeader.Order_Type_Code =="C") ||
                    (SystemSettings.settings.pblnPromptTentDelivery && Session.cart.cartHeader.Order_Type_Code == "D") ||
                    (SystemSettings.settings.pblnPromptTentDineIn && Session.cart.cartHeader.Order_Type_Code == "I") ||
                    (SystemSettings.settings.pblnPromptTentPickUp && Session.cart.cartHeader.Order_Type_Code == "P"))
                {
                    lblTentNumber.Visible = true;
                    txtTentNumber.Visible = true;
                    mblnTentNumberVisible = true;
                }
                else
                {
                    lblTentNumber.Visible = false;
                    txtTentNumber.Visible = false;
                    mblnTentNumberVisible = false;
                }


                RePositionControls();
                Session.pblnChargeOrder = false;
                txtName.Text = "";
                tdbtxtPhone_Ext.Text = "";
                tdbmPhone_Number.Text = "";

                returnValue = true;

                //If pudtWorkstation_Settings.pblnAttached_Scanner = True Then //TO DO

            }


            if ((SystemSettings.settings.pblnCustNameReqCarryOut && Session.cart.cartHeader.Order_Type_Code == "C") ||
                    (SystemSettings.settings.pblnCustNameReqPickUp && Session.cart.cartHeader.Order_Type_Code == "P") ||
                    (SystemSettings.settings.pblnCustNameReqDineIn && Session.cart.cartHeader.Order_Type_Code == "I") ||
                    Session.cart.Customer.Tax_Exempt1)
            {
                if(Session.cart.cartHeader.Customer_Name.Trim().Length ==0 || (Session.cart.Customer.Phone_Number.Length < Session.MinPhoneDigits && 
                            SystemSettings.WorkStationSettings.pblnRequire_Phone_Pick_Up || Session.cart.cartHeader.Delayed_Date !=DateTime.MinValue || 
                            Session.cart.Customer.Tax_Exempt1 ) )
                {
                    if(Session.cart.cartHeader.Delayed_Date != DateTime.MinValue || Session.cart.Customer.Tax_Exempt1)
                    {
                        tdbmPhone_Number.Visible = true;
                        lblPhoneNumber.Visible = true;
                        txtCustomerLookup.Visible = true;
                        lblCustomerLookup.Visible = true;
                        lblName.Visible = true;
                        txtName.Visible = true;
                        lblExt.Visible = true;
                        tdbtxtPhone_Ext.Visible = true;

                        mblnNameVisible = true;
                        mblnPhoneVisible = true;

                        if ((SystemSettings.settings.pblnPromptTentCarryOut && Session.cart.cartHeader.Order_Type_Code == "C") ||
                            (SystemSettings.settings.pblnPromptTentDelivery && Session.cart.cartHeader.Order_Type_Code == "D") ||
                            (SystemSettings.settings.pblnPromptTentDineIn && Session.cart.cartHeader.Order_Type_Code == "I") ||
                            (SystemSettings.settings.pblnPromptTentPickUp && Session.cart.cartHeader.Order_Type_Code == "P"))
                        {
                            lblTentNumber.Visible = true;
                            txtTentNumber.Visible = true;
                            mblnTentNumberVisible = true;
                        }
                        else
                        {
                            lblTentNumber.Visible = false;
                            txtTentNumber.Visible = false;
                            mblnTentNumberVisible = false;
                        }

                    }
                    else
                    {
                        if(Session.cart.cartHeader.Order_Type_Code =="P" && SystemSettings.WorkStationSettings.pblnRequire_Phone_Pick_Up)
                        {
                            lblPhoneNumber.Visible = true;
                            lblExt.Visible = true;
                            tdbmPhone_Number.Visible = true;
                            tdbtxtPhone_Ext.Visible = true;

                            mblnPhoneVisible = true;
                        }
                        else
                        {
                            lblPhoneNumber.Visible = false;
                            lblExt.Visible = false;
                            tdbmPhone_Number.Visible = false;
                            tdbtxtPhone_Ext.Visible = false;

                            mblnPhoneVisible = false;
                        }

                        txtCustomerLookup.Visible = false;
                        lblCustomerLookup.Visible = false;

                        if ((SystemSettings.settings.pblnPromptTentCarryOut && Session.cart.cartHeader.Order_Type_Code == "C") ||
                            (SystemSettings.settings.pblnPromptTentDelivery && Session.cart.cartHeader.Order_Type_Code == "D") ||
                            (SystemSettings.settings.pblnPromptTentDineIn && Session.cart.cartHeader.Order_Type_Code == "I") ||
                            (SystemSettings.settings.pblnPromptTentPickUp && Session.cart.cartHeader.Order_Type_Code == "P"))
                        {
                            lblTentNumber.Visible = true;
                            txtTentNumber.Visible = true;
                            mblnTentNumberVisible = true;
                        }
                        else
                        {
                            lblTentNumber.Visible = false;
                            txtTentNumber.Visible = false;
                            mblnTentNumberVisible = false;
                        }

                    }

                    lblName.Visible = true;
                    txtName.Visible = true;
                    mblnNameVisible = true;

                    if ((SystemSettings.settings.pblnPromptTentCarryOut && Session.cart.cartHeader.Order_Type_Code == "C") ||
                            (SystemSettings.settings.pblnPromptTentDelivery && Session.cart.cartHeader.Order_Type_Code == "D") ||
                            (SystemSettings.settings.pblnPromptTentDineIn && Session.cart.cartHeader.Order_Type_Code == "I") ||
                            (SystemSettings.settings.pblnPromptTentPickUp && Session.cart.cartHeader.Order_Type_Code == "P"))
                    {
                        lblTentNumber.Visible = true;
                        txtTentNumber.Visible = true;
                        mblnTentNumberVisible = true;
                    }
                    else
                    {
                        lblTentNumber.Visible = false;
                        txtTentNumber.Visible = false;
                        mblnTentNumberVisible = false;
                    }

                    RePositionControls();
                    returnValue = true;
                    //If pudtWorkstation_Settings.pblnAttached_Scanner = True Then //TO DO

                }
                else
                {
                    if ((SystemSettings.settings.pblnPromptTentCarryOut && Session.cart.cartHeader.Order_Type_Code == "C") ||
                            (SystemSettings.settings.pblnPromptTentDelivery && Session.cart.cartHeader.Order_Type_Code == "D") ||
                            (SystemSettings.settings.pblnPromptTentDineIn && Session.cart.cartHeader.Order_Type_Code == "I") ||
                            (SystemSettings.settings.pblnPromptTentPickUp && Session.cart.cartHeader.Order_Type_Code == "P"))
                    {

                        if (Session.cart.cartHeader.Delayed_Date != DateTime.MinValue || Session.cart.Customer.Tax_Exempt1)
                        {
                            tdbmPhone_Number.Visible = true;
                            lblPhoneNumber.Visible = true;
                            txtCustomerLookup.Visible = true;
                            lblCustomerLookup.Visible = true;
                            lblName.Visible = true;
                            txtName.Visible = true;
                            lblExt.Visible = true;
                            tdbtxtPhone_Ext.Visible = true;

                            mblnNameVisible = true;
                            mblnPhoneVisible = true;
                        }
                        else 
                        { 
                            if((Session.cart.cartHeader.Order_Type_Code == "P" && SystemSettings.WorkStationSettings.pblnRequire_Phone_Pick_Up) || Session.cart.cartHeader.Order_Type_Code=="D")
                            {
                                lblPhoneNumber.Visible = true;
                                lblExt.Visible = true;
                                tdbmPhone_Number.Visible = true;
                                tdbtxtPhone_Ext.Visible = true;

                                mblnPhoneVisible = true;
                            }
                            else
                            {
                                lblPhoneNumber.Visible = false;
                                lblExt.Visible = false;
                                tdbmPhone_Number.Visible = false;
                                tdbtxtPhone_Ext.Visible = false;

                                mblnPhoneVisible = false;
                            }

                            txtCustomerLookup.Visible = false;
                            lblCustomerLookup.Visible = false;

                            lblName.Visible = true;
                            txtName.Visible = true;
                            mblnNameVisible = true; 

                        }
                        lblTentNumber.Visible = true;
                        txtTentNumber.Visible = true;
                        mblnTentNumberVisible = true;
                        RePositionControls();
                        returnValue = true;
                    }


                }

            }
            else if ((Session.cart.cartHeader.Delayed_Date != DateTime.MinValue || Session.cart.Customer.Tax_Exempt1) || 
                (Session.cart.cartHeader.Order_Type_Code == "P" && SystemSettings.WorkStationSettings.pblnRequire_Phone_Pick_Up))
            {
                if (Session.cart.cartHeader.Customer_Name.Trim().Length == 0 || (Session.cart.Customer.Phone_Number.Length < Session.MinPhoneDigits))
                {
                    lblPhoneNumber.Visible = true;
                    tdbmPhone_Number.Visible = true;

                    if (Session.cart.cartHeader.Delayed_Date != DateTime.MinValue && !SystemSettings.settings.pblnCustNameReqPickUp)
                    {
                        lblName.Visible = false;
                        txtName.Visible = false;
                        mblnNameVisible = false;
                    }
                    else
                    {
                        lblName.Visible = true;
                        txtName.Visible = true;
                        mblnNameVisible = true;
                    }

                    lblExt.Visible = true;
                    tdbtxtPhone_Ext.Visible = true;
                    txtCustomerLookup.Visible = true;
                    lblCustomerLookup.Visible = true;

                    mblnPhoneVisible = true;

                    if ((SystemSettings.settings.pblnPromptTentCarryOut && Session.cart.cartHeader.Order_Type_Code == "C") ||
                           (SystemSettings.settings.pblnPromptTentDelivery && Session.cart.cartHeader.Order_Type_Code == "D") ||
                           (SystemSettings.settings.pblnPromptTentDineIn && Session.cart.cartHeader.Order_Type_Code == "I") ||
                           (SystemSettings.settings.pblnPromptTentPickUp && Session.cart.cartHeader.Order_Type_Code == "P"))
                    {
                        lblTentNumber.Visible = true;
                        txtTentNumber.Visible = true;
                        mblnTentNumberVisible = true;
                    }
                    else
                    {
                        lblTentNumber.Visible = false;
                        txtTentNumber.Visible = false;
                        mblnTentNumberVisible = false;
                    }

                    RePositionControls();
                    returnValue = true;
                    //If pudtWorkstation_Settings.pblnAttached_Scanner = True Then //TO DO

                }
            }
            else
            {
                if ((SystemSettings.settings.pblnPromptTentCarryOut && Session.cart.cartHeader.Order_Type_Code == "C") ||
                            (SystemSettings.settings.pblnPromptTentDelivery && Session.cart.cartHeader.Order_Type_Code == "D") ||
                            (SystemSettings.settings.pblnPromptTentDineIn && Session.cart.cartHeader.Order_Type_Code == "I") ||
                            (SystemSettings.settings.pblnPromptTentPickUp && Session.cart.cartHeader.Order_Type_Code == "P"))
                {
                    tdbmPhone_Number.Visible = false;
                    lblPhoneNumber.Visible = false;
                    txtCustomerLookup.Visible = false;
                    lblCustomerLookup.Visible = false;
                    lblName.Visible = false;
                    txtName.Visible = false;
                    lblExt.Visible = false;
                    tdbtxtPhone_Ext.Visible = false;
                    lblTentNumber.Visible = true;
                    txtTentNumber.Visible = true;

                    mblnNameVisible = false;
                    mblnPhoneVisible = false;
                    mblnTentNumberVisible = true;

                    RePositionControls();
                    returnValue = true;
                    //If pudtWorkstation_Settings.pblnAttached_Scanner = True Then //TO DO

                }
            }
            return returnValue;


        }


        public bool QuickServiceCapture()
        {
            bool returnValue = false;
            if (Session.pblnChargeOrder && Session.cart.Customer.Customer_Code == 0)
            {                
                returnValue = true;

            }


            if ((SystemSettings.settings.pblnCustNameReqCarryOut && Session.cart.cartHeader.Order_Type_Code == "C") ||
                    (SystemSettings.settings.pblnCustNameReqPickUp && Session.cart.cartHeader.Order_Type_Code == "P") ||
                    (SystemSettings.settings.pblnCustNameReqDineIn && Session.cart.cartHeader.Order_Type_Code == "I") ||
                    Session.cart.Customer.Tax_Exempt1)
            {
                if (Session.cart.cartHeader.Customer_Name.Trim().Length == 0 || (Session.cart.Customer.Phone_Number.Length < Session.MinPhoneDigits &&
                            SystemSettings.WorkStationSettings.pblnRequire_Phone_Pick_Up || Session.cart.cartHeader.Delayed_Date != DateTime.MinValue ||
                            Session.cart.Customer.Tax_Exempt1))
                {
                    if (Session.cart.cartHeader.Delayed_Date != DateTime.MinValue || Session.cart.Customer.Tax_Exempt1)
                    {
                        

                    }
                    else
                    {
                        if (Session.cart.cartHeader.Order_Type_Code == "P" && SystemSettings.WorkStationSettings.pblnRequire_Phone_Pick_Up)
                        {
                            
                        }
                        else
                        {
                           
                        }

                       

                        if ((SystemSettings.settings.pblnPromptTentCarryOut && Session.cart.cartHeader.Order_Type_Code == "C") ||
                            (SystemSettings.settings.pblnPromptTentDelivery && Session.cart.cartHeader.Order_Type_Code == "D") ||
                            (SystemSettings.settings.pblnPromptTentDineIn && Session.cart.cartHeader.Order_Type_Code == "I") ||
                            (SystemSettings.settings.pblnPromptTentPickUp && Session.cart.cartHeader.Order_Type_Code == "P"))
                        {

                        }
                        else
                        {
                            
                        }

                    }

                    

                    if ((SystemSettings.settings.pblnPromptTentCarryOut && Session.cart.cartHeader.Order_Type_Code == "C") ||
                            (SystemSettings.settings.pblnPromptTentDelivery && Session.cart.cartHeader.Order_Type_Code == "D") ||
                            (SystemSettings.settings.pblnPromptTentDineIn && Session.cart.cartHeader.Order_Type_Code == "I") ||
                            (SystemSettings.settings.pblnPromptTentPickUp && Session.cart.cartHeader.Order_Type_Code == "P"))
                    {
                        
                    }
                    else
                    {
                        
                    }

                    
                    returnValue = true;
                    
                }
                else
                {
                    if ((SystemSettings.settings.pblnPromptTentCarryOut && Session.cart.cartHeader.Order_Type_Code == "C") ||
                            (SystemSettings.settings.pblnPromptTentDelivery && Session.cart.cartHeader.Order_Type_Code == "D") ||
                            (SystemSettings.settings.pblnPromptTentDineIn && Session.cart.cartHeader.Order_Type_Code == "I") ||
                            (SystemSettings.settings.pblnPromptTentPickUp && Session.cart.cartHeader.Order_Type_Code == "P"))
                    {

                        if (Session.cart.cartHeader.Delayed_Date != DateTime.MinValue || Session.cart.Customer.Tax_Exempt1)
                        {
                            
                        }
                        else
                        {
                            if ((Session.cart.cartHeader.Order_Type_Code == "P" && SystemSettings.WorkStationSettings.pblnRequire_Phone_Pick_Up) || Session.cart.cartHeader.Order_Type_Code == "D")
                            {
                                
                            }
                            else
                            {
                                
                            }

                           

                        }
                        
                        returnValue = true;
                    }


                }

            }
            else if ((Session.cart.cartHeader.Delayed_Date != DateTime.MinValue || Session.cart.Customer.Tax_Exempt1) ||
                (Session.cart.cartHeader.Order_Type_Code == "P" && SystemSettings.WorkStationSettings.pblnRequire_Phone_Pick_Up))
            {
                if (Session.cart.cartHeader.Customer_Name.Trim().Length == 0 || (Session.cart.Customer.Phone_Number.Length < Session.MinPhoneDigits))
                {
                    

                    if (Session.cart.cartHeader.Delayed_Date != DateTime.MinValue && !SystemSettings.settings.pblnCustNameReqPickUp)
                    {
                        
                    }
                    else
                    {
                        
                    }

                    

                    if ((SystemSettings.settings.pblnPromptTentCarryOut && Session.cart.cartHeader.Order_Type_Code == "C") ||
                           (SystemSettings.settings.pblnPromptTentDelivery && Session.cart.cartHeader.Order_Type_Code == "D") ||
                           (SystemSettings.settings.pblnPromptTentDineIn && Session.cart.cartHeader.Order_Type_Code == "I") ||
                           (SystemSettings.settings.pblnPromptTentPickUp && Session.cart.cartHeader.Order_Type_Code == "P"))
                    {
                       
                    }
                    else
                    {
                        
                    }

                    
                    returnValue = true;
                    

                }
            }
            else
            {
                if ((SystemSettings.settings.pblnPromptTentCarryOut && Session.cart.cartHeader.Order_Type_Code == "C") ||
                            (SystemSettings.settings.pblnPromptTentDelivery && Session.cart.cartHeader.Order_Type_Code == "D") ||
                            (SystemSettings.settings.pblnPromptTentDineIn && Session.cart.cartHeader.Order_Type_Code == "I") ||
                            (SystemSettings.settings.pblnPromptTentPickUp && Session.cart.cartHeader.Order_Type_Code == "P"))
                {
                    
                    returnValue = true;
                    

                }
            }
            return returnValue;


        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (lblPhoneNumber.ForeColor == System.Drawing.Color.Yellow || lblName.ForeColor == System.Drawing.Color.Yellow || lblTentNumber.ForeColor == System.Drawing.Color.Yellow)
                pblnCancel = true;
            else
                pblnCancel = false;

            this.Close();
        }

        private void frmCapture_Activated(object sender, EventArgs e)
        {
            if (tdbmPhone_Number.Visible)
                tdbmPhone_Number.Focus();
            else if (txtName.Visible)
                txtName.Focus();
            else
                txtTentNumber.Focus();

            //if(tdbmPhone_Number.Text)

        }

        private void btnKeyboard_Click(object sender, EventArgs e)
        {
            try
            {
                objCustomerKeyBoardInfo = null;
                int frmtxtboxmaxlength = 0;
                if (txtFocusedtextBox != null)
                {
                    objCustomerKeyBoardInfo = DictKeyBoardInfo.FirstOrDefault(x => x.Key == txtFocusedtextBox.Name).Value;
                    TextBox textBox = (TextBox)(txtFocusedtextBox);
                    frmtxtboxmaxlength = textBox.MaxLength;
                }

                if (objCustomerKeyBoardInfo != null)
                {
                    if (objCustomerKeyBoardInfo.KeyBoardType == 1)
                    {
                        frmKeyBoard objfrmKeyBoard = new frmKeyBoard(txtFocusedtextBox, objCustomerKeyBoardInfo.CaptionName);
                        objfrmKeyBoard.txt_Input.MaxLength = frmtxtboxmaxlength;
                        objfrmKeyBoard.ShowDialog();
                    }
                    else if (objCustomerKeyBoardInfo.KeyBoardType == 2)
                    {
                        frmKeyBoardNumeric objfrmKeyBoardNumeric = new frmKeyBoardNumeric(txtFocusedtextBox, objCustomerKeyBoardInfo.CaptionName);
                        objfrmKeyBoardNumeric.txt_Input.MaxLength = frmtxtboxmaxlength;
                        objfrmKeyBoardNumeric.ShowDialog();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Trace("ERROR", "frmCapture-btnKeyboard_click(): " + ex.Message, ex, true);
            }
        }

        private void SetActiveControlName()
        {
             strActiveControl = (TextBox)this.ActiveControl;

        }

        public bool DeliveryPOSCaptureInfo()
        {
            bool blnReturnValue = false;
            if (SystemSettings.settings.pblnPromptTentCarryOut && Session.cart.cartHeader.Order_Type_Code == "C"
                || SystemSettings.settings.pblnPromptTentDelivery && Session.cart.cartHeader.Order_Type_Code == "D"
                || SystemSettings.settings.pblnPromptTentDineIn && Session.cart.cartHeader.Order_Type_Code == "I"
                || SystemSettings.settings.pblnPromptTentPickUp && Session.cart.cartHeader.Order_Type_Code == "P")
            {
                if (Session.pblnModifyingOrder || (!Session.pblnModifyingOrder && string.IsNullOrEmpty(Session.cart.cartHeader.Tent_Number)))
                {
                    tdbmPhone_Number.Visible = false;
                    lblPhoneNumber.Visible = false;
                    txtCustomerLookup.Visible = false;
                    lblCustomerLookup.Visible = false;
                    lblName.Visible = false;
                    txtName.Visible = false;
                    lblExt.Visible = false;
                    tdbtxtPhone_Ext.Visible = false;

                    mblnNameVisible = false;
                    mblnPhoneVisible = false;

                    lblTentNumber.Visible = true;
                    txtTentNumber.Visible = true;
                    mblnTentNumberVisible = true;

                    RePositionControls();
                    blnReturnValue = true;
                }
                

            }
            return blnReturnValue;

        }

        public bool DeliveryPOSCapture()
        {
            bool blnReturnValue = false;
            if (SystemSettings.settings.pblnPromptTentCarryOut && Session.cart.cartHeader.Order_Type_Code == "C"
                || SystemSettings.settings.pblnPromptTentDelivery && Session.cart.cartHeader.Order_Type_Code == "D"
                || SystemSettings.settings.pblnPromptTentDineIn && Session.cart.cartHeader.Order_Type_Code == "I"
                || SystemSettings.settings.pblnPromptTentPickUp && Session.cart.cartHeader.Order_Type_Code == "P")
            {
                if (Session.pblnModifyingOrder || (!Session.pblnModifyingOrder && string.IsNullOrEmpty(Session.cart.cartHeader.Tent_Number)))
                {
                    
                    blnReturnValue = true;
                }


            }
            return blnReturnValue;

        }

        private void LoadKeyBoardInfo()
        {
            DictKeyBoardInfo = new Dictionary<string, CustomerKeyBoardInfo>();

            objCustomerKeyBoardInfo = new CustomerKeyBoardInfo();
            objCustomerKeyBoardInfo.CaptionName = "Phone Number";
            objCustomerKeyBoardInfo.KeyBoardType = 2;
            DictKeyBoardInfo.Add("tdbmPhone_Number", objCustomerKeyBoardInfo);

            objCustomerKeyBoardInfo = new CustomerKeyBoardInfo();
            objCustomerKeyBoardInfo.CaptionName = "Ext";
            objCustomerKeyBoardInfo.KeyBoardType = 2;
            DictKeyBoardInfo.Add("tdbtxtPhone_Ext", objCustomerKeyBoardInfo);

            objCustomerKeyBoardInfo = new CustomerKeyBoardInfo();
            objCustomerKeyBoardInfo.CaptionName = "Customer Lookup";
            objCustomerKeyBoardInfo.KeyBoardType = 1;
            DictKeyBoardInfo.Add("txtCustomerLookup", objCustomerKeyBoardInfo);

            objCustomerKeyBoardInfo = new CustomerKeyBoardInfo();
            objCustomerKeyBoardInfo.CaptionName = "Name";
            objCustomerKeyBoardInfo.KeyBoardType = 1;
            DictKeyBoardInfo.Add("txtName", objCustomerKeyBoardInfo);

            objCustomerKeyBoardInfo = new CustomerKeyBoardInfo();
            objCustomerKeyBoardInfo.CaptionName = "Tent Number";
            objCustomerKeyBoardInfo.KeyBoardType = 1;
            DictKeyBoardInfo.Add("txtTentNumber", objCustomerKeyBoardInfo);
        }

        private void tdbmPhone_Number_Enter(object sender, EventArgs e)
        {
            try
            {
                txtFocusedtextBox = (TextBox)sender;
            }
            catch (Exception ex)
            {
                Logger.Trace("ERROR", ex.Message, ex, true);
            }
        }

        private void tdbmPhone_Number_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                e.Handled = !Char.IsNumber(e.KeyChar) && e.KeyChar != 8;
            }
            catch (Exception ex)
            {
                Logger.Trace("ERROR", ex.Message, ex, true);
            }
        }

        private void tdbtxtPhone_Ext_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                e.Handled = !Char.IsNumber(e.KeyChar) && e.KeyChar != 8;
            }
            catch (Exception ex)
            {
                Logger.Trace("ERROR", ex.Message, ex, true);
            }
        }
    }
}
