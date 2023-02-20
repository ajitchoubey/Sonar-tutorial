using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using JublFood.POS.App.Class;
namespace JublFood.POS.App
{
    public partial class frmOrder1 : Form
    {
        public frmOrder1()
        {
            
            InitializeComponent();
            Common.colorListViewHeader(ref lv_ItemDetails, Color.Teal, Color.White);
            uC_Customer_OrderMenu.SetButtonText("Customer");
            btn_Misc_Click(btn_Pizza, null);
        }

        private void frmOrder_Load(object sender, EventArgs e)
        {

        }

        private void btn_Misc_Click(object sender, EventArgs e)
        {
            lbl_MenuText.Text = "Menu Items";
            Button btn = (Button)sender;
            if (btn.Name == "btn_Misc")
            {
                ShowHidePanel(pnl_Misc);
            }
            else if (btn.Name == "btn_Pizza")
            {
                ShowHidePanel(pnl_Pizza);
            }
            else if (btn.Name == "btn_Beverages")
            {
                ShowHidePanel(pnl_Beverages);
            }
            else if (btn.Name == "btn_Bread")
            {
                ShowHidePanel(pnl_Bread);
            }
            else if (btn.Name == "btn_Sides")
            {
                ShowHidePanel(pnl_Sides);
            }
            else if (btn.Name == "btn_Desserts")
            {
                ShowHidePanel(pnl_Desserts);
            }
            else if (btn.Name == "btn_Combo")
            {
                ShowHidePanel(pnl_Combos);
            }
            else if (btn.Name == "btn_More")
            {
                ShowHidePanel(pnl_PizzaMore);
            }
            SetButtonColor(btn);
        }

        void ShowHidePanel(Panel pnl)
        {
            pnl_Pizza.Visible = false;
            pnl_PizzaMore.Visible = false;
            pnl_Misc.Visible = false;
            pnl_Beverages.Visible = false;
            pnl_Bread.Visible = false;
            pnl_Sides.Visible = false;
            pnl_Desserts.Visible = false;
            pnl_Combos.Visible = false;
            Flpnl_ExpandControl.Visible = false;
            flPanel_PizzaTopping.Visible = false;
            pnl_Misc.Visible = false;

            pnl.Visible = true;
            pnl.Location = new Point(272, 181);
            pnl.Size = new Size(507, 304);
        }
        void SetButtonColor(Button btn)
        {
            btn_Misc.BackColor = DefaultBackColor;
            btn_Pizza.BackColor = DefaultBackColor;
            btn_Beverages.BackColor = DefaultBackColor;
            btn_Bread.BackColor = DefaultBackColor;
            btn_Sides.BackColor = DefaultBackColor;
            btn_Desserts.BackColor = DefaultBackColor;
            btn_Combo.BackColor = DefaultBackColor;
            btn.BackColor = Color.FromArgb(255, 128, 128);
            if (btn.Name == "btn_More")
            {
                btn_Pizza.BackColor = Color.FromArgb(255, 128, 128);
            }
        }

        private void btn_EcoBag_Click(object sender, EventArgs e)
        {

            Button btn = (Button)sender;
            lbl_MenuText.Text = btn.Text + " - Sizes";
            ShowHidePanel(Flpnl_ExpandControl);
            Flpnl_ExpandControl.Controls.Clear();
            if (btn.Name == "btn_BPDecoChg")
            {
                Flpnl_ExpandControl.Controls.Add(new Button { Name = "btn_DecoChrg1.5", Text = "Deco Chrg 1.5", Size = new Size(71, 60), BackColor = DefaultBackColor });
                Flpnl_ExpandControl.Controls.Add(new Button { Name = "btn_DecoChrg2.0", Text = "Deco Chrg 2.0", Size = new Size(71, 60), BackColor = DefaultBackColor });
            }
            else if (btn.Name == "btn_OtherCharges")
            {
                Flpnl_ExpandControl.Controls.Add(new Button { Name = "btn_DecoChrg1.5", Text = ".", Size = new Size(71, 60), BackColor = DefaultBackColor });
                Flpnl_ExpandControl.Controls.Add(new Button { Name = "btn_DecoChrg2.0", Text = ".", Size = new Size(71, 60), BackColor = DefaultBackColor });
            }
            else if (btn.Name == "btn_PlasitcBag")
            {
                Flpnl_ExpandControl.Controls.Add(new Button { Name = "btn_30.4CM", Text = "30.4cm * 45.7cm", Size = new Size(71, 60), BackColor = DefaultBackColor });
                Flpnl_ExpandControl.Controls.Add(new Button { Name = "btn_30.9CM", Text = "30.9cm * 55.9cm", Size = new Size(71, 60), BackColor = DefaultBackColor });
                Flpnl_ExpandControl.Controls.Add(new Button { Name = "btn_46.7CM", Text = "46.7cm * 57.1cm", Size = new Size(71, 60), BackColor = DefaultBackColor });
            }
            else if (btn.Name == "btn_Paperbag")
            {
                Flpnl_ExpandControl.Controls.Add(new Button { Name = "btn_33CM", Text = "33cm * 30.5cm", Size = new Size(71, 60), BackColor = DefaultBackColor });
                Flpnl_ExpandControl.Controls.Add(new Button { Name = "btn_36.8CM", Text = "36.8cm * 30.5cm", Size = new Size(71, 60), BackColor = DefaultBackColor });
            }
            else if (btn.Name == "btn_EcoBag")
            {
                Flpnl_ExpandControl.Controls.Add(new Button { Name = "btn_EcoBag7", Text = "ECO BAGS 7''", Size = new Size(71, 60), BackColor = DefaultBackColor });
                Flpnl_ExpandControl.Controls.Add(new Button { Name = "btn_EcoBag10", Text = "ECO BAGS 10''", Size = new Size(71, 60), BackColor = DefaultBackColor });
                Flpnl_ExpandControl.Controls.Add(new Button { Name = "btn_EcoBag13", Text = "ECO BAGS 13''", Size = new Size(71, 60), BackColor = DefaultBackColor });
            }
        }

        private void btn_PMOnion_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            ShowHidePanel(Flpnl_ExpandControl);
            Flpnl_ExpandControl.Controls.Clear();
            lbl_MenuText.Text = btn.Text + " - Sizes";
            if (btn.Name == "btn_PMChessy")
            {

                Flpnl_ExpandControl.Controls.Add(new Button { Name = "btn_RegHT", Text = "Reg HT", Size = new Size(71, 60), BackColor = DefaultBackColor });
                Flpnl_ExpandControl.Controls.Add(new Button { Name = "btn_RegPAN", Text = "Reg PAN", Size = new Size(71, 60), BackColor = DefaultBackColor });
            }
            else
            {
                Flpnl_ExpandControl.Controls.Add(new Button { Name = "btn_RegDD", Text = "Reg DD", Size = new Size(71, 60), BackColor = DefaultBackColor });
                Flpnl_ExpandControl.Controls.Add(new Button { Name = "btn_RegPAN", Text = "Reg PAN", Size = new Size(71, 60), BackColor = DefaultBackColor });
                Flpnl_ExpandControl.Controls.Add(new Button { Name = "btn_RegHT", Text = "Reg HT", Size = new Size(71, 60), BackColor = DefaultBackColor });
            }
        }

        private void btn_Margherita_Click(object sender, EventArgs e)
        {
            Button btnn = (Button)sender;
            lbl_MenuText.Text = btnn.Text + " - Sizes";
            ShowHidePanel(Flpnl_ExpandControl);
            Flpnl_ExpandControl.Controls.Clear();

            Flpnl_ExpandControl.Controls.Add(new Button { Name = "btn_LarNHT", Text = "Lar NHT", Size = new Size(71, 60), BackColor = DefaultBackColor });
            Flpnl_ExpandControl.Controls.Add(new Button { Name = "btn_MedNHT", Text = "Med NHT", Size = new Size(71, 60), BackColor = DefaultBackColor });
            Flpnl_ExpandControl.Controls.Add(new Button { Name = "btn_MedBU", Text = "Med BU", Size = new Size(71, 60), BackColor = DefaultBackColor });
            Flpnl_ExpandControl.Controls.Add(new Button { Name = "btn_RegBU", Text = "Reg BU", Size = new Size(71, 60), BackColor = DefaultBackColor });
            Flpnl_ExpandControl.Controls.Add(new Button { Name = "btn_RegPAN", Text = "Reg PAN", Size = new Size(71, 60), BackColor = DefaultBackColor });
            Flpnl_ExpandControl.Controls.Add(new Button { Name = "btn_MedPAN", Text = "Med PAN", Size = new Size(71, 60), BackColor = DefaultBackColor });
            Flpnl_ExpandControl.Controls.Add(new Button { Name = "btn_MedTC", Text = "Med TC", Size = new Size(71, 60), BackColor = DefaultBackColor });
            Flpnl_ExpandControl.Controls.Add(new Button { Name = "btn_RegDD", Text = "Reg DD", Size = new Size(71, 60), BackColor = DefaultBackColor });
            Flpnl_ExpandControl.Controls.Add(new Button { Name = "btn_RegNHT", Text = "Reg NHT", Size = new Size(71, 60), BackColor = DefaultBackColor });
            Flpnl_ExpandControl.Controls.Add(new Button { Name = "btn_RegHT", Text = "Reg HT", Size = new Size(71, 60), BackColor = DefaultBackColor });
            Flpnl_ExpandControl.Controls.Add(new Button { Name = "btn_MedDD", Text = "Med DD", Size = new Size(71, 60), BackColor = DefaultBackColor });

            foreach (Button btn in Flpnl_ExpandControl.Controls)
            {
                btn.Click += new EventHandler(ClickforTopping);
            }
        }

        private void pnl_PizzaMore_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btn_BurgerPizza_Click(object sender, EventArgs e)
        {
          
            Button btn = (Button)sender;
            lbl_MenuText.Text = btn.Text + " - Sizes";
            ShowHidePanel(Flpnl_ExpandControl);
            Flpnl_ExpandControl.Controls.Clear();

            Flpnl_ExpandControl.Controls.Add(new Button { Name = "btn_NVCLsc", Text = "NV Clsc", Size = new Size(71, 60), BackColor = DefaultBackColor });
            Flpnl_ExpandControl.Controls.Add(new Button { Name = "btn_VGClsc", Text = "VG Clsc", Size = new Size(71, 60), BackColor = DefaultBackColor });
            Flpnl_ExpandControl.Controls.Add(new Button { Name = "btn_VGPrem", Text = "VG Prem", Size = new Size(71, 60), BackColor = DefaultBackColor });
        }

        private void btn_BuddyPepsi_Click(object sender, EventArgs e)
        {
           
            lbl_MenuText.Text =  "Buddy Pepsi - Sizes";
            ShowHidePanel(Flpnl_ExpandControl);
            Flpnl_ExpandControl.Controls.Clear();

            Flpnl_ExpandControl.Controls.Add(new Button { Name = "btn_PmOnionCombo", Text = "PM ONION", Size = new Size(71, 60), BackColor = DefaultBackColor });
            Flpnl_ExpandControl.Controls.Add(new Button { Name = "btn_PmCapsicumCOmbo", Text = "PM CAPSICUM", Size = new Size(71, 60), BackColor = DefaultBackColor });
            Flpnl_ExpandControl.Controls.Add(new Button { Name = "btn_PmGoldCornCombo", Text = "PM GOLD CORN", Size = new Size(71, 60), BackColor = DefaultBackColor });
            Flpnl_ExpandControl.Controls.Add(new Button { Name = "btn_PmPaneerOnCombo", Text = "PM PANEER_ON", Size = new Size(71, 60), BackColor = DefaultBackColor });
            Flpnl_ExpandControl.Controls.Add(new Button { Name = "btn_PmCheesyCombo", Text = "PM CHEESY", Size = new Size(71, 60), BackColor = DefaultBackColor });
            Flpnl_ExpandControl.Controls.Add(new Button { Name = "btn_PmChkSausgCombo", Text = "PM CHK SAUSG", Size = new Size(71, 60), BackColor = DefaultBackColor });
            Flpnl_ExpandControl.Controls.Add(new Button { Name = "btn_PmBBQCKNCombo", Text = "PM BBQ CKN", Size = new Size(71, 60), BackColor = DefaultBackColor });
            Flpnl_ExpandControl.Controls.Add(new Button { Name = "btn_PMVegLddCombo", Text = "PM VEG LDD", Size = new Size(71, 60), BackColor = DefaultBackColor });
            Flpnl_ExpandControl.Controls.Add(new Button { Name = "btn_NvgLddCombo", Text = "PM NVG LDD", Size = new Size(71, 60), BackColor = DefaultBackColor });
            Flpnl_ExpandControl.Controls.Add(new Button { Name = "btn_VGTomatoCombo", Text = "PM TOMATO", Size = new Size(71, 60), BackColor = DefaultBackColor });

            foreach (Button btn in Flpnl_ExpandControl.Controls)
            {
                btn.Click += new EventHandler(btn_Click_Combo_BuddyPepsiItem);
                btn.Click += new EventHandler(btn_Click_Combo_BuddyPepsiItem);
            }
        }
        protected void ClickforTopping(object sender, EventArgs e)
        {
            CreatePizzaTopping();
        }
        protected void ClickforToppingQuantity(object sender, EventArgs e)
        {


            Button btn = ((Button)flPanel_PizzaTopping.Controls["btn_Light"]);
            btn.Image = null;
            btn.ForeColor = Color.Black;
            btn.TextAlign = ContentAlignment.MiddleCenter;

            btn = ((Button)flPanel_PizzaTopping.Controls["btn_Single"]);
            btn.Image = null;
            btn.ForeColor = Color.Black;
            btn.TextAlign = ContentAlignment.MiddleCenter;

            btn = ((Button)flPanel_PizzaTopping.Controls["btn_Extra"]);
            btn.Image = null;
            btn.ForeColor = Color.Black;
            btn.TextAlign = ContentAlignment.MiddleCenter;

            btn = ((Button)flPanel_PizzaTopping.Controls["btn_Double"]);
            btn.Image = null;
            btn.ForeColor = Color.Black;
            btn.TextAlign = ContentAlignment.MiddleCenter;

            btn = ((Button)flPanel_PizzaTopping.Controls["btn_Triple"]);
            btn.Image = null;
            btn.ForeColor = Color.Black;
            btn.TextAlign = ContentAlignment.MiddleCenter;

            btn = (Button)sender;
            btn.Image = JublFood.POS.App.Properties.Resources._49;
            btn.ImageAlign = ContentAlignment.TopCenter;
            btn.ForeColor = Color.White;
            btn.TextAlign = ContentAlignment.BottomCenter;
        }
        void CreatePizzaTopping()
        {
           lbl_MenuText.Text= lbl_MenuText.Text.Replace("Sizes", "Pizza Topping");
            flPanel_PizzaTopping.Controls.Clear();
            Button btnOk =new Button { Name = "btn_OK", Text = "OK", Size = new Size(71, 60), BackColor = DefaultBackColor, Image = Properties.Resources._171, ImageAlign = ContentAlignment.TopCenter, TextAlign = ContentAlignment.BottomCenter };
            btnOk.Click += new EventHandler(btn_OKclick);
            flPanel_PizzaTopping.Controls.Add(btnOk);
            flPanel_PizzaTopping.Controls.Add(new Button { Name = "btn_Light", Text = "Light", Size = new Size(71, 60), Font = new Font(this.Font, FontStyle.Bold), BackColor = Color.FromArgb(255, 255, 192) });
            flPanel_PizzaTopping.Controls.Add(new Button { Name = "btn_Single", Text = "Single", Size = new Size(71, 60), Font = new Font(this.Font, FontStyle.Bold), BackColor = Color.FromArgb(255,128,128) });
            flPanel_PizzaTopping.Controls.Add(new Button { Name = "btn_Extra", Text = "Extra", Size = new Size(71, 60), Font = new Font(this.Font, FontStyle.Bold), BackColor = Color.FromArgb(128, 255, 128) });
            flPanel_PizzaTopping.Controls.Add(new Button { Name = "btn_Double", Text = "Double", Size = new Size(71, 60), Font = new Font(this.Font, FontStyle.Bold), BackColor = Color.FromArgb(128, 255, 255) });
            flPanel_PizzaTopping.Controls.Add(new Button { Name = "btn_Triple", Text = "Triple", Size = new Size(71, 60), Font = new Font(this.Font, FontStyle.Bold), BackColor = Color.FromArgb(128, 128, 255) });
            foreach (Button btn in flPanel_PizzaTopping.Controls)
            {
                if (btn.Name != "btn_OK")
                {
                    btn.Click += new EventHandler(ClickforToppingQuantity);
                }
            }
            flPanel_PizzaTopping.Controls.Add(new Button { Name = "btn_SepcaltyPizza", Text = "Speciality Pizza", Size = new Size(71, 60), BackColor = DefaultBackColor });

            flPanel_PizzaTopping.Controls.Add(new Button { Name = "btn_ChkRashers", Text = "Chk Rashers", Size = new Size(71, 60), BackColor = DefaultBackColor,Tag=1 });
            flPanel_PizzaTopping.Controls.Add(new Button { Name = "btn_Onion", Text = "Onion", Size = new Size(71, 60), BackColor = DefaultBackColor,Tag = 1 });
            flPanel_PizzaTopping.Controls.Add(new Button { Name = "btn_GPeper", Text = "G.Pepper", Size = new Size(71, 60), BackColor = DefaultBackColor, Tag = 1 });
            flPanel_PizzaTopping.Controls.Add(new Button { Name = "btn_Mushroom", Text = "Mushroom", Size = new Size(71, 60), BackColor = DefaultBackColor, Tag = 1 });
            flPanel_PizzaTopping.Controls.Add(new Button { Name = "btn_Tomato", Text = "Tomato", Size = new Size(71, 60), BackColor = DefaultBackColor, Tag = 1 });
            flPanel_PizzaTopping.Controls.Add(new Button { Name = "btn_Corn", Text = "Corn", Size = new Size(71, 60), BackColor = DefaultBackColor, Tag = 1 });
            flPanel_PizzaTopping.Controls.Add(new Button { Name = "btn_JalaPeno", Text = "JalaPeno", Size = new Size(71, 60), BackColor = DefaultBackColor, Tag = 1 });
            flPanel_PizzaTopping.Controls.Add(new Button { Name = "btn_RedPeppers", Text = "Red Peppers", Size = new Size(71, 60), BackColor = DefaultBackColor, Tag = 1 });
            flPanel_PizzaTopping.Controls.Add(new Button { Name = "btn_paneer", Text = "Paneer", Size = new Size(71, 60), BackColor = DefaultBackColor, Tag = 1 });
            flPanel_PizzaTopping.Controls.Add(new Button { Name = "btn_Olives", Text = "Olives", Size = new Size(71, 60), BackColor = DefaultBackColor, Tag = 1 });
            flPanel_PizzaTopping.Controls.Add(new Button { Name = "btn_DCheese", Text = "D Cheese", Size = new Size(71, 60), BackColor = DefaultBackColor, Tag = 1 });
            flPanel_PizzaTopping.Controls.Add(new Button { Name = "btn_ChkTikkams", Text = "Chk Tikka Ms", Size = new Size(71, 60), BackColor = DefaultBackColor, Tag = 1 });
            flPanel_PizzaTopping.Controls.Add(new Button { Name = "btn_HerbSausCh", Text = "Herb Saus Ch", Size = new Size(71, 60), BackColor = DefaultBackColor, Tag = 1 });
            flPanel_PizzaTopping.Controls.Add(new Button { Name = "btn_CkPepperBQ", Text = "Ck Pepper BQ", Size = new Size(71, 60), BackColor = DefaultBackColor, Tag = 1 });
            flPanel_PizzaTopping.Controls.Add(new Button { Name = "btn_CkPerriPerri", Text = "Ck Perri Perri", Size = new Size(71, 60), BackColor = DefaultBackColor, Tag = 1 });
            flPanel_PizzaTopping.Controls.Add(new Button { Name = "btn_CKPeperoni", Text = "Ck Pepperoni", Size = new Size(71, 60), BackColor = DefaultBackColor, Tag = 1 });
            flPanel_PizzaTopping.Controls.Add(new Button { Name = "btn_CkPaneerTikka", Text = "Paneer Tikka", Size = new Size(71, 60), BackColor = DefaultBackColor, Tag = 1 });
            flPanel_PizzaTopping.Controls.Add(new Button { Name = "btn_ChkMexSaus", Text = "Chk Mex Saus", Size = new Size(71, 60), BackColor = DefaultBackColor, Tag = 1 });
            flPanel_PizzaTopping.Controls.Add(new Button { Name = "btn_TomatoBlend", Text = "Tomato Blend", Size = new Size(71, 60), BackColor = DefaultBackColor, Tag = 1 });
            flPanel_PizzaTopping.Controls.Add(new Label { Text = "", Size = new Size(600, 70) });
            foreach (Control ctl in flPanel_PizzaTopping.Controls)
            {
                if (ctl is Button &&  Convert.ToInt16(ctl.Tag) ==1)
                {
                    ctl.Click += new EventHandler(btn_ClickToppingItem);
                }
            }
            Button btnPizzaPart=new Button{ Name = "btn_Whole", Text = "Whole", Size = new Size(71, 60), Image = Properties.Resources._198, ImageAlign = ContentAlignment.TopCenter, TextAlign = ContentAlignment.BottomCenter, BackColor = Color.FromArgb(255,128,128)};
            btnPizzaPart.Click += new EventHandler(ClickforPizzaPart);

            flPanel_PizzaTopping.Controls.Add(btnPizzaPart);

            btnPizzaPart = new Button { Name = "btn_1stHalf", Text = "1st Half", Size = new Size(71, 60), BackColor = DefaultBackColor, Image = Properties.Resources._199, ImageAlign = ContentAlignment.TopCenter, TextAlign = ContentAlignment.BottomCenter };
            btnPizzaPart.Click += new EventHandler(ClickforPizzaPart);
            flPanel_PizzaTopping.Controls.Add(btnPizzaPart);

            btnPizzaPart = new Button { Name = "btn_2ndHalf", Text = "2nd Half", Size = new Size(71, 60), BackColor = DefaultBackColor, Image = Properties.Resources._200, ImageAlign = ContentAlignment.TopCenter, TextAlign = ContentAlignment.BottomCenter  };
            btnPizzaPart.Click += new EventHandler(ClickforPizzaPart);
            flPanel_PizzaTopping.Controls.Add(btnPizzaPart);

            ShowHidePanel(flPanel_PizzaTopping);
            flPanel_PizzaTopping.Location = new Point(250, 90);
            flPanel_PizzaTopping.Size = new Size(600, 400);


        }

        protected void ClickforPizzaPart(object sender, EventArgs e)
        {
            Button btn_Whole = (Button)flPanel_PizzaTopping.Controls["btn_Whole"];
            Button btn_1stHalf = (Button)flPanel_PizzaTopping.Controls["btn_1stHalf"];
            Button btn_2ndHalf = (Button)flPanel_PizzaTopping.Controls["btn_2ndHalf"];
            btn_Whole.BackColor = DefaultBackColor;
            btn_1stHalf.BackColor = DefaultBackColor;
            btn_2ndHalf.BackColor = DefaultBackColor;
            Button btn = (Button) sender;
            btn.BackColor = Color.FromArgb(255,128,128);
        }
        protected void btn_OKclick(object sender, EventArgs e)
        {
            btn_Misc_Click(btn_Pizza, null);
        }
        protected void btn_ClickToppingItem(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            btn.BackColor = Color.FromArgb(255,128,128);
        }

        protected void btn_Click_Combo_BuddyPepsiItem(object sender,EventArgs e)
        {
            Button btn=(Button)sender;
            lbl_MenuText.Text = btn.Text + " - Sizes";
            if (btn.Name == "btn_PMVegLddCombo" || btn.Name == "btn_NvgLddCombo")
            {
                ClickforTopping(null, null);
            }
            else
            {
                Flpnl_ExpandControl.Controls.Clear();
                Button btnControl=new Button { Name = "btn_Combo_BuddyPepsi_RegHT", Text = "Reg HT", Size = new Size(71, 60), BackColor = Color.FromArgb(255,128,128) };
                btnControl.Click += new EventHandler(ClickforTopping);
                Flpnl_ExpandControl.Controls.Add(btnControl);

                 btnControl = new Button { Name = "btn_Combo_BuddyPepsi_RegPAN", Text = "REG PAN", Size = new Size(71, 60), BackColor = DefaultBackColor };
                btnControl.Click += new EventHandler(ClickforTopping);
                Flpnl_ExpandControl.Controls.Add(btnControl);
            }
                
        }

        private void btn_PizAoeReg_Click(object sender, EventArgs e)
        {
            Button btnText = (Button)sender;
            lbl_MenuText.Text = btnText.Text + " - Sizes";
            Flpnl_ExpandControl.Controls.Clear();
            Flpnl_ExpandControl.Controls.Add(new Button { Name = "btn_ComboItemRegNHT", Text = "Reg NHT", Size = new Size(71, 60), BackColor = Color.FromArgb(255,128,128)});
            Flpnl_ExpandControl.Controls.Add(new Button { Name = "btn_ComboItemRegBU", Text = "Reg BU", Size = new Size(71, 60), BackColor = DefaultBackColor });
            Flpnl_ExpandControl.Controls.Add(new Button { Name = "btn_ComboItemRegPAN", Text = "Reg PAN", Size = new Size(71, 60), BackColor = DefaultBackColor });
            Flpnl_ExpandControl.Controls.Add(new Button { Name = "btn_ComboItemRegHT", Text = "Reg HT", Size = new Size(71, 60), BackColor = DefaultBackColor });

            foreach (Button btn in Flpnl_ExpandControl.Controls)
            {
                btn.Click += new EventHandler(ClickforTopping);
            }
            ShowHidePanel(Flpnl_ExpandControl);
        }

        private void btn_PizAoeMed_Click(object sender, EventArgs e)
        {
            Button btnText = (Button)sender;
            lbl_MenuText.Text = btnText.Text + " - Sizes";

            Flpnl_ExpandControl.Controls.Clear();
            Flpnl_ExpandControl.Controls.Add(new Button { Name = "btn_ComboItemMedNHT", Text = "Med NHT", Size = new Size(71, 60), BackColor = Color.FromArgb(255,128,128) });
            Flpnl_ExpandControl.Controls.Add(new Button { Name = "btn_ComboItemMedBU", Text = "Med BU", Size = new Size(71, 60), BackColor = DefaultBackColor });
            Flpnl_ExpandControl.Controls.Add(new Button { Name = "btn_ComboItemMedPAN", Text = "Med PAN", Size = new Size(71, 60), BackColor = DefaultBackColor });
            Flpnl_ExpandControl.Controls.Add(new Button { Name = "btn_ComboItemMedHT", Text = "Med TC", Size = new Size(71, 60), BackColor = DefaultBackColor });

            foreach (Button btn in Flpnl_ExpandControl.Controls)
            {
                btn.Click += new EventHandler(ClickforTopping);
            }
           
        }

        private void btn_PizAOELar_Click(object sender, EventArgs e)
        {
            ClickforTopping(null, null);
        }

     

        private void btn_OK_Click(object sender, EventArgs e)
        {
            pnl_Quantity.Hide();
        }

        private void btn_Quantity_Click(object sender, EventArgs e)
        {
            uC_KeyBoardNumeric.txtUserID = txt_Quantity;
            uC_KeyBoardNumeric.ChangeButtonColor(DefaultBackColor);
            pnl_Quantity.Size = new Size(510,403);
            pnl_Quantity.Location = new Point(276, 87);
            pnl_Quantity.Show();
        }

        private void btn_Instructions_Click(object sender, EventArgs e)
        {
            using(frmReason objfrmReason=new frmReason())
            {
                objfrmReason.ShowDialog();
            }
        }

        private void btn_Dot_Click(object sender, EventArgs e)
        {
            txt_Quantity.Text = txt_Quantity.Text + btn_Dot.Text;
        }

        private void txt_Quantity_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !Char.IsNumber(e.KeyChar) && e.KeyChar != 8;
        }

        private void btn_Minus_Click(object sender, EventArgs e)
        {

        }

        private void btn_Plus_Click(object sender, EventArgs e)
        {

        }
    }
}
