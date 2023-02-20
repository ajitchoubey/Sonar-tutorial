using Jublfood.AppLogger;
using JublFood.POS.App.API;
using JublFood.POS.App.BusinessLayer;
using JublFood.POS.App.Cache;
using JublFood.POS.App.Class;
using JublFood.POS.App.Models.Cart;
using JublFood.POS.App.Models.Catalog;
using JublFood.POS.App.Models.Order;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace JublFood.POS.App
{
    public enum enumReasonGroupID
    {
        BadOrder = 1,
        VoidOrder = 2,
        AbandonOrder = 3,
        Coupon = 4,
        CookingInstruction = 5

    }
    public partial class frmReason : Form
    {
        public frmReason(int? reasonGroupId = null)
        {
            InitializeComponent();
            ReasonGroup_ID = reasonGroupId;
            //  LoadReason();
            SetControlText();
            SetButtonText();
            SetControl();
            CheckTrainningMode();
        }
        public int? ReasonGroup_ID;
        public int SelectedLineNumber;
        public bool isExit;
        private void SetControl()
        {
            List<CatalogReasons> lstCatalogReasons = new List<CatalogReasons>();
            string labelText = null;
            lstCatalogReasons = APILayer.GetCatalogReasons(SystemSettings.settings.pstrDefault_Location_Code, Convert.ToInt32(Session.CurrentEmployee.LoginDetail.LanguageCode), ReasonGroup_ID);

            if (ReasonGroup_ID == Convert.ToInt32(enumReasonGroupID.CookingInstruction))
            {
                if (Common.DictAllButtonText.TryGetValue(LanguageConstant.cintInstructions, out labelText))
                {
                    ltxtReason.Text = labelText + ":";
                }
                GenrateReasonUI(lstCatalogReasons);
            }
            else if (ReasonGroup_ID == Convert.ToInt32(enumReasonGroupID.AbandonOrder))
            {
                if (Common.DictAllButtonText.TryGetValue(LanguageConstant.cintAbandonedOrder, out labelText))
                {
                    ltxtReason.Text = labelText + ":";
                }
                GenrateReasonUI(lstCatalogReasons);
            }
            else if (ReasonGroup_ID == Convert.ToInt32(enumReasonGroupID.Coupon))
            {
                //if (Common.DictAllButtonText.TryGetValue(LanguageConstant.cintCouponType, out labelText))
                //{
                ltxtReason.Text = "";//labelText + ":";
                //}
                GenrateReasonUI(lstCatalogReasons);
            }
           
        }
        private void SetButtonText()
        {
            string labelText = null;
            if (Common.DictAllButtonText.TryGetValue(LanguageConstant.cintClear, out labelText))
            {
                cmdClear.Text = labelText;

            }
            if (Common.DictAllButtonText.TryGetValue(LanguageConstant.cintKeyBoard, out labelText))
            {
                cmdKeyBoard.Text = labelText;
            }
            if (Common.DictAllButtonText.TryGetValue(LanguageConstant.cintOK, out labelText))
            {
                cmdOK.Text = labelText;
            }
            if (Common.DictAllButtonText.TryGetValue(LanguageConstant.cintCancel, out labelText))
            {
                cmdCancel.Text = labelText;
            }
        }
        private void SetControlText()
        {
            //BAL obj = new BAL();
            //List<FormField> listFormField = obj.GetControlText("frmReason");
            Session.catalogControlText = APILayer.GetControlText("frmReason");
            foreach (Control ctl in this.pnl_Reason.Controls)
            {
                if (ctl is Label)
                {
                    foreach (CatalogControlText formField in Session.catalogControlText)
                    {
                        if (ctl.Name.Substring(4, ctl.Name.Length - 4) == formField.Field_Name)
                        {
                            ctl.Text = formField.text;
                        }
                    }
                }
            }
        }
        private void GenrateReasonUI(List<CatalogReasons> lstCatalogReasons)
        {
            try
            {
                if (lstCatalogReasons != null && lstCatalogReasons.Count > 0)
                {
                    foreach (CatalogReasons reason in lstCatalogReasons)
                    {
                        var chekbox = new CheckBox();
                        chekbox.Font = new Font("Microsoft Sans Serif", 12, FontStyle.Regular);
                        chekbox.Name = Convert.ToString(reason.Reason_ID);
                        chekbox.Tag = Convert.ToString(reason.Reason_ID);
                        chekbox.Text = reason.System_Text;
                        chekbox.AutoSize = false;
                        chekbox.Size = new Size(160, 62);
                        chekbox.Appearance = Appearance.Button;
                        chekbox.BackColor = DefaultBackColor;
                        chekbox.UseVisualStyleBackColor = true;
                        chekbox.CheckedChanged += new EventHandler(chk_CheckedChanged);
                        flowLayout_reason.Controls.Add(chekbox);
                    }
                }
                else
                {
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                Logger.Trace("ERROR", "frmReason-genrateReasonUI(): " + ex.Message, ex, true);
            }
        }
        protected void chk_CheckedChanged(Object sender, EventArgs e)
        {

            CheckBox chk = (CheckBox)sender;
            if (chk.Checked)
            {
                chk.BackColor = Color.PeachPuff;

                if (ReasonGroup_ID == Convert.ToInt32(enumReasonGroupID.AbandonOrder))
                {
                    foreach (Control cBox in flowLayout_reason.Controls)
                    {
                        if (cBox is CheckBox && cBox.Name != chk.Name)
                        {
                            ((CheckBox)cBox).Checked = false;
                            cBox.BackColor = Color.WhiteSmoke;
                        }
                    }
                }
            }
            else
            {
                chk.BackColor = Color.WhiteSmoke;
            }
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmdKeyBoard_Click(object sender, EventArgs e)
        {
            frmKeyBoard objfrmKeyBoard = new frmKeyBoard(txtOtherInfo, ltxtOtherInfo.Text);
            objfrmKeyBoard.ShowDialog();
        }

        private void cmdOK_Click(object sender, EventArgs e)
        {
            if (!IsReasonSelected())
            {
                CustomMessageBox.Show(MessageConstant.SelectReason, CustomMessageBox.Buttons.OK, CustomMessageBox.Icon.Warning);
                return;
            }

            try
            {
                if (ReasonGroup_ID == Convert.ToInt32(enumReasonGroupID.CookingInstruction))
                {
                    AdditemReason();
                }
                else if (ReasonGroup_ID == Convert.ToInt32(enumReasonGroupID.AbandonOrder))
                {
                    AddAbandonReason();
                }
                else if(ReasonGroup_ID == Convert.ToInt32(enumReasonGroupID.Coupon))
                {
                    AddOrderLineCouponReason();
                    AddOrderCouponReason();  
                }
            }
            catch (Exception ex)
            {
                Logger.Trace("ERROR", "frmReason-cmdOk_Click(): " + ex.Message, ex, true);
            }
            this.Close();
        }

        private void AdditemReason()
        {
            try
            {
                CartItem currentCartItem = CartFunctions.GetCurrentCartItem(SelectedLineNumber);
                List<ItemReason> lstitemReasons = new List<ItemReason>();
                ItemReason itemReason = null;
                int tempReasonSequence = 0;
                foreach (CheckBox chk in flowLayout_reason.Controls)
                {
                    if (chk.Checked)
                    {
                        tempReasonSequence = tempReasonSequence + 1;
                        itemReason = new ItemReason();
                        itemReason.CartId = currentCartItem.CartId;
                        itemReason.Location_Code = currentCartItem.Location_Code;
                        itemReason.Order_Number = currentCartItem.Order_Number;
                        itemReason.Order_Date = currentCartItem.Order_Date;
                        itemReason.Line_Number = currentCartItem.Line_Number;
                        itemReason.Sequence = currentCartItem.Sequence;
                        itemReason.Reason_Sequence = tempReasonSequence;
                        itemReason.Reason_Group_Code = Convert.ToInt64(ReasonGroup_ID);
                        itemReason.Reason_ID = Convert.ToInt32(chk.Tag);
                        itemReason.Other_Information = "";
                        itemReason.Deleted = false;
                        itemReason.Added_By = currentCartItem.Added_By;
                        itemReason.Reason_Description = chk.Text;
                        lstitemReasons.Add(itemReason);
                    }
                }
                if (!string.IsNullOrEmpty(txtOtherInfo.Text))
                {
                    tempReasonSequence = tempReasonSequence + 1;
                    itemReason = new ItemReason();
                    itemReason.CartId = currentCartItem.CartId;
                    itemReason.Location_Code = currentCartItem.Location_Code;
                    itemReason.Order_Number = currentCartItem.Order_Number;
                    itemReason.Order_Date = currentCartItem.Order_Date;
                    itemReason.Line_Number = currentCartItem.Line_Number;
                    itemReason.Sequence = currentCartItem.Sequence;
                    itemReason.Reason_Sequence = tempReasonSequence;
                    itemReason.Reason_Group_Code = Convert.ToInt64(ReasonGroup_ID);
                    itemReason.Reason_ID = 0;
                    itemReason.Other_Information = txtOtherInfo.Text.ToUpper();
                    itemReason.Deleted = false;
                    itemReason.Added_By = currentCartItem.Added_By;
                    itemReason.Reason_Description = txtOtherInfo.Text.ToUpper();
                    lstitemReasons.Add(itemReason);
                }
                currentCartItem.itemReasons = lstitemReasons;
                currentCartItem.Action = "M";
                CartFunctions.AddReasonTocart(currentCartItem);
                Form lastOpenedForm = Application.OpenForms["frmOrder"];
                if (lastOpenedForm != null)
                {
                    frmOrder frm = (frmOrder)lastOpenedForm;
                    frm.RefreshCartUI();
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                Logger.Trace("ERROR", "frmReason-additemreason(): " + ex.Message, ex, true);
            }
            this.Close();
        }
        private void AddAbandonReason()
        {
            ////////Cart cartLocal = (new Cart().GetCart());
            //////////  cartLocal.orderReason
            ////////List<OrderReason> lstorderReasons = new List<OrderReason>();
            ////////OrderReason orderReason = new OrderReason();
            ////////foreach (CheckBox chk in flowLayout_reason.Controls)
            ////////{
            ////////    if (chk.Checked)
            ////////    {
            ////////        //tempReasonSequence = tempReasonSequence + 1;
            ////////        //orderReason = new OrderReason();
            ////////        //orderReason.CartId = String.IsNullOrEmpty(Session.cart.cartHeader.CartId) ? "" : Session.cart.cartHeader.CartId;
            ////////        //orderReason.Location_Code = curretCartItem.Location_Code;
            ////////        //orderReason.Order_Number = curretCartItem.Order_Number;
            ////////        //orderReason.Order_Date = curretCartItem.Order_Date;
            ////////        //orderReason.Reason_Sequence = tempReasonSequence;
            ////////        //orderReason.Reason_Group_Code = Convert.ToInt64(ReasonGroup_ID);
            ////////        //orderReason.Reason_ID = Convert.ToInt32(chk.Tag);
            ////////        //orderReason.Other_Information = "";
            ////////        //orderReason.Deleted = false;
            ////////        //orderReason.Added_By = curretCartItem.Added_By;
            ////////        //orderReason.Reason_Description = "";
            ////////        //lstitemReasons.Add(itemReason);
            ////////    }
            ////////}

            ////////isExit = true;

            //////////Form[] forms = Application.OpenForms.Cast<Form>().ToArray();
            //////////foreach (Form thisForm in forms)
            //////////{
            //////////    thisForm.Close();
            //////////}
            //////////UserFunctions.ClearSession();
            //////////frmCustomer frmCust = new frmCustomer();
            //////////frmCust.Show();
            //////////frmLogin frm = new frmLogin();
            //////////frm.ShowDialog();





            try
            {
                CartFunctions.CheckCart();
                Cart cartLocal = (new Cart().GetCart());

                cartLocal.orderReasons = Session.cart.orderReasons;

                if (cartLocal.orderReasons == null)
                    cartLocal.orderReasons = new List<OrderReason>();

                foreach (OrderReason orderReason1 in cartLocal.orderReasons)
                {
                    if (orderReason1.Reason_Group_Code == Convert.ToInt64(ReasonGroup_ID))
                        orderReason1.Action = "D";
                }

                //Session.cart.orderReason.RemoveAll(x => x.Reason_Group_Code == Convert.ToInt64(ReasonGroup_ID));

                OrderReason orderReason = null;                
                int tempReasonSequence = 0;
                foreach (CheckBox chk in flowLayout_reason.Controls)
                {
                    if (chk.Checked)
                    {
                        tempReasonSequence = tempReasonSequence + 1;
                        orderReason = new OrderReason();
                        orderReason.CartId = Session.cart.cartHeader.CartId;
                        orderReason.Location_Code = Session.cart.cartHeader.LocationCode;
                        orderReason.Order_Number = Session.cart.cartHeader.Order_Number;
                        orderReason.Order_Date = Session.cart.cartHeader.Order_Date;
                        orderReason.Reason_Sequence = tempReasonSequence;
                        orderReason.Reason_Group_Code = Convert.ToInt64(ReasonGroup_ID);
                        orderReason.Reason_ID = Convert.ToInt32(chk.Tag);
                        orderReason.Other_Information = "";
                        orderReason.Deleted = false;
                        orderReason.Added_By = Session.cart.cartHeader.Added_By;
                        orderReason.Reason_Description = chk.Text;
                        orderReason.Action = "A";
                        cartLocal.orderReasons.Add(orderReason);                        
                    }
                }
                if (!string.IsNullOrEmpty(txtOtherInfo.Text))
                {
                    tempReasonSequence = tempReasonSequence + 1;
                    orderReason = new OrderReason();
                    orderReason.CartId = Session.cart.cartHeader.CartId;
                    orderReason.Location_Code = Session.cart.cartHeader.LocationCode;
                    orderReason.Order_Number = Session.cart.cartHeader.Order_Number;
                    orderReason.Order_Date = Session.cart.cartHeader.Order_Date;
                    orderReason.Reason_Sequence = tempReasonSequence;
                    orderReason.Reason_Group_Code = Convert.ToInt64(ReasonGroup_ID);
                    orderReason.Reason_ID = 0;
                    orderReason.Other_Information = txtOtherInfo.Text.ToUpper();
                    orderReason.Deleted = false;
                    orderReason.Added_By = Session.cart.cartHeader.Added_By;
                    orderReason.Reason_Description = txtOtherInfo.Text.ToUpper();
                    orderReason.Action = "A";
                    cartLocal.orderReasons.Add(orderReason);
                }

                cartLocal.cartHeader = Session.cart.cartHeader;
                CartFunctions.UpdateCustomer(cartLocal);
                Session.cart = APILayer.Add2Cart(cartLocal);

                isExit = true;

                this.Close();

            }
            catch (Exception ex)
            {
                Logger.Trace("ERROR", "frmReason-addabandonreason(): " + ex.Message, ex, true);
            }
            this.Close();
        }
        private bool IsReasonSelected()
        {
            bool isValid = true;
            bool isReasonChecked = false;
            try
            {
                foreach (CheckBox chk in flowLayout_reason.Controls)
                {
                    if (chk.Checked)
                    {
                        isReasonChecked = true;
                        break;
                    }
                }
                if (ReasonGroup_ID != Convert.ToInt32(enumReasonGroupID.CookingInstruction) && ReasonGroup_ID != Convert.ToInt32(enumReasonGroupID.BadOrder))
                {
                    if (isReasonChecked == false && string.IsNullOrEmpty(txtOtherInfo.Text))
                    {
                        isValid = false;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Trace("ERROR", "frmReason-isreasonselected(): " + ex.Message, ex, true);
            }
            return isValid;
        }
        public void HighlightItemReason(int SelectedLineNumber)
        {
            var curretCartItem = CartFunctions.GetCurrentCartItem(SelectedLineNumber);
            try
            {
                if (curretCartItem != null)
                {
                    List<ItemReason> lstitemReasons = curretCartItem.itemReasons;
                    if (lstitemReasons != null && lstitemReasons.Count > 0)
                    {
                        foreach (ItemReason itemReason in lstitemReasons)
                        {
                            if (itemReason.Reason_ID > 0)
                            {
                                CheckBox chk = (CheckBox)flowLayout_reason.Controls[Convert.ToString(itemReason.Reason_ID)];
                                chk.Checked = true;
                                chk_CheckedChanged(chk, null);
                            }
                            else
                            {
                                txtOtherInfo.Text = itemReason.Other_Information;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Trace("ERROR", "frmReason-highlightitemreason(): " + ex.Message, ex, true);
            }
        }

        public void CheckTrainningMode()
        {
            try
            {
                Color color = DefaultBackColor;
                if (SystemSettings.settings.pblnTrainingMode)
                {
                    color = Session.TrainningModeColor;
                }
                else
                {
                    color = Session.NormalModeColor;
                }
                pnl_Reason.BackColor = color;
                flowLayout_reason.BackColor = color;
            }
            catch (Exception ex)
            {
                Logger.Trace("ERROR", ex.Message, ex, true);
            }
        }

        private void AddOrderLineCouponReason()
        {
            try
            {
                CartItem curretCartItem = CartFunctions.GetCurrentCartItem(SelectedLineNumber);
                if (curretCartItem != null)
                {
                    if (curretCartItem.itemReasons == null)
                    {
                        curretCartItem.itemReasons = new List<ItemReason>();
                    }
                    else
                    {
                        curretCartItem.itemReasons.RemoveAll(x => x.Reason_Group_Code == Convert.ToInt64(ReasonGroup_ID));
                    }
                    
                    ItemReason itemReason = null;
                    int tempReasonSequence = 0;
                    foreach (CheckBox chk in flowLayout_reason.Controls)
                    {
                        if (chk.Checked)
                        {
                            tempReasonSequence = tempReasonSequence + 1;
                            itemReason = new ItemReason();
                            itemReason.CartId = curretCartItem.CartId;
                            itemReason.Location_Code = curretCartItem.Location_Code;
                            itemReason.Order_Number = curretCartItem.Order_Number;
                            itemReason.Order_Date = curretCartItem.Order_Date;
                            itemReason.Line_Number = curretCartItem.Line_Number;
                            itemReason.Sequence = curretCartItem.Sequence;
                            itemReason.Reason_Sequence = tempReasonSequence;
                            itemReason.Reason_Group_Code = Convert.ToInt64(ReasonGroup_ID);
                            itemReason.Reason_ID = Convert.ToInt32(chk.Tag);
                            itemReason.Other_Information = "";
                            itemReason.Deleted = false;
                            itemReason.Added_By = curretCartItem.Added_By;
                            itemReason.Reason_Description = chk.Text;
                            curretCartItem.itemReasons.Add(itemReason);
                        }
                    }
                    if (!string.IsNullOrEmpty(txtOtherInfo.Text))
                    {
                        tempReasonSequence = tempReasonSequence + 1;
                        itemReason = new ItemReason();
                        itemReason.CartId = curretCartItem.CartId;
                        itemReason.Location_Code = curretCartItem.Location_Code;
                        itemReason.Order_Number = curretCartItem.Order_Number;
                        itemReason.Order_Date = curretCartItem.Order_Date;
                        itemReason.Line_Number = curretCartItem.Line_Number;
                        itemReason.Sequence = curretCartItem.Sequence;
                        itemReason.Reason_Sequence = tempReasonSequence;
                        itemReason.Reason_Group_Code = Convert.ToInt64(ReasonGroup_ID);
                        itemReason.Reason_ID = 0;
                        itemReason.Other_Information = txtOtherInfo.Text.ToUpper();
                        itemReason.Deleted = false;
                        itemReason.Added_By = curretCartItem.Added_By;
                        itemReason.Reason_Description = txtOtherInfo.Text.ToUpper();
                        curretCartItem.itemReasons.Add(itemReason);
                    }

                    this.Close();
                }
            }
            catch (Exception ex)
            {
                Logger.Trace("ERROR", "frmReason-addorderlinecouponreason(): " + ex.Message, ex, true);
            }
            this.Close();
        }

        private void cmdClear_Click(object sender, EventArgs e)
        {
            txtOtherInfo.Text = string.Empty;

            foreach (CheckBox chk in flowLayout_reason.Controls)
            {
                if (chk.Checked)
                {
                    chk.Checked = false;
                }
            }
        }
        private void AddOrderCouponReason()
        {
            try
            {
                Cart cartLocal = (new Cart().GetCart());
        
                if (cartLocal != null)
                {
                    int tempReasonSequence = 0;
                    foreach (CheckBox chk in flowLayout_reason.Controls)
                    {
                        if (chk.Checked)
                        {
                            tempReasonSequence = tempReasonSequence + 1;
                            OrderReason Reason = new OrderReason();
                            Reason.CartId = Session.cart.cartHeader.CartId;
                            Reason.Location_Code = Session.cart.cartHeader.LocationCode;
                            Reason.Order_Number = Session.cart.cartHeader.Order_Number;
                            Reason.Order_Date = Session.cart.cartHeader.Order_Date;
                            Reason.Reason_Sequence = 0;
                            Reason.Reason_Sequence = tempReasonSequence;
                            Reason.Reason_Group_Code = Convert.ToInt64(ReasonGroup_ID);
                            Reason.Reason_ID = Convert.ToInt32(chk.Tag);
                            Reason.Other_Information = txtOtherInfo.Text.ToUpper();
                            Reason.Deleted = false;
                            Reason.Added_By = Session.cart.cartHeader.Added_By;
                            Reason.Reason_Description = chk.Text;
                            cartLocal.orderReasons.Add(Reason);
                            CartFunctions.UpdateCustomer(cartLocal);
                            Session.cart = APILayer.Add2Cart(cartLocal);
                        }
                    }


                    this.Close();
                }
            }
            catch (Exception ex)
            {
                Logger.Trace("ERROR", "frmReason-addordercouponreason(): " + ex.Message, ex, true);
            }
            this.Close();
        }

    }

}
