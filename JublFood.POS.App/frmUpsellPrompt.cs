using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Jublfood.AppLogger;
using JublFood.POS.App.BusinessLayer;
using JublFood.POS.App.Cache;
using JublFood.POS.App.Class;
using JublFood.POS.App.Models.Cart;

namespace JublFood.POS.App
{
    public partial class frmUpsellPrompt : Form
    {
        private ItemwiseUpsellDatawithType itemwiseUpsellDatawithType;
        private int TotalPages;
        private int currentMenuItemPageNo;
        public string AttribteCode;
        public frmUpsellPrompt()
        {
            InitializeComponent();
        }

        public frmUpsellPrompt(ItemwiseUpsellDatawithType itemwiseUpsellDatawithType)
        {
            InitializeComponent();
            this.itemwiseUpsellDatawithType = itemwiseUpsellDatawithType;

        }

        private void frmUpsellPrompt_Load(object sender, EventArgs e)
        {
            try
            {
                string msg = CartFunctions.GetMessageTextForUpsell(itemwiseUpsellDatawithType.UpsellType);
                lblmsg.Text = msg.Replace("<<" + itemwiseUpsellDatawithType.UpsellType + ">>", "");

                PopulateAttributes(1);
            }
            catch (Exception ex)
            {
                Logger.Trace("ERROR", "frmUpsellPrompt-frmUpsellPrompt_Load(): " + ex.Message, ex, true);
            }
        }

        private void PopulateAttributes(int PageNo)
        {
            int x = Constants.HorizontalSpace, y = Constants.VerticalSpace;
            int startIndex = 0;
            int LastIndex = 0;
            bool CreateMoreButton = false;
            int MaxMenuItemsPerPage = 0;
            int MenuCardButtonWidthG = 90;
            int ButtonHeightG = 69;

            MaxMenuItemsPerPage = ((flowLayoutPanelAttributes.Width - Constants.HorizontalSpace) / MenuCardButtonWidthG) * ((flowLayoutPanelAttributes.Height - Constants.VerticalSpace) / ButtonHeightG);

            TotalPages = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(itemwiseUpsellDatawithType.AttributeList.Count) / MaxMenuItemsPerPage));

            if (MaxMenuItemsPerPage < itemwiseUpsellDatawithType.AttributeList.Count)
                CreateMoreButton = true;

            if (PageNo == 1)
            {
                startIndex = 0;
                if (CreateMoreButton)
                    LastIndex = MaxMenuItemsPerPage - 1;
                else
                    LastIndex = itemwiseUpsellDatawithType.AttributeList.Count;
            }
            else
            {
                startIndex = (MaxMenuItemsPerPage - 1) * (PageNo - 1);
                LastIndex = (itemwiseUpsellDatawithType.AttributeList.Count - startIndex) > (MaxMenuItemsPerPage - 1) ? ((MaxMenuItemsPerPage - 1) * (PageNo - 1)) + (MaxMenuItemsPerPage - 1) : itemwiseUpsellDatawithType.AttributeList.Count;
            }


            for (int i = startIndex; i < LastIndex; i++)
            {
                Button btnDynamic = new Button();

                btnDynamic.Location = new System.Drawing.Point(x, y);
                btnDynamic.Size = new System.Drawing.Size(MenuCardButtonWidthG, ButtonHeightG);

                btnDynamic.Name = itemwiseUpsellDatawithType.AttributeList[i].Code.Trim();
                btnDynamic.Tag = "MenuItems";
                btnDynamic.Text = itemwiseUpsellDatawithType.AttributeList[i].Description.Trim();
                btnDynamic.Font = new Font(new FontFamily("Microsoft Sans Serif"), 8);

                btnDynamic.BackColor = SystemColors.Control;
                
                btnDynamic.Margin = new Padding(0);

                btnDynamic.Enabled = itemwiseUpsellDatawithType.AttributeList[i].IsEnabled;

                flowLayoutPanelAttributes.Controls.Add(btnDynamic);
                x += MenuCardButtonWidthG;
                btnDynamic.Click += new EventHandler(this.DynamicButtonClick);
                
            }

            if (CreateMoreButton)
            {
                Button btn = new Button();
                btn.Location = new System.Drawing.Point(x, y);
                btn.Size = new System.Drawing.Size(MenuCardButtonWidthG, ButtonHeightG);
                btn.Name = "btnMore";
                btn.Tag = PageNo;
                btn.Font = new Font(new FontFamily("Microsoft Sans Serif"), 8);
                btn.BackColor = SystemColors.Control;
                btn.Margin = new Padding(0);
                btn.TextAlign = ContentAlignment.BottomCenter;
                btn.TextImageRelation = TextImageRelation.ImageAboveText;               
                btn.Text = "More";
                btn.BackgroundImage = Properties.Resources._33;
                //}

                btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
                flowLayoutPanelAttributes.Controls.Add(btn);
                btn.Click += new EventHandler(this.MenuItemMoreButtonClick);
            }

            currentMenuItemPageNo = PageNo;
        }

        private void MenuItemMoreButtonClick(object sender, EventArgs e)
        {
            try
            {
                flowLayoutPanelAttributes.Visible = true;
                flowLayoutPanelAttributes.BringToFront();
                flowLayoutPanelAttributes.Controls.Clear();

                int CurrentPageNo = Convert.ToInt32(((Button)sender).Tag);
                int NextPageNo = 0;

                if (CurrentPageNo + 1 > TotalPages)
                    NextPageNo = 1;
                else
                    NextPageNo = CurrentPageNo + 1;

                PopulateAttributes(NextPageNo);
            }
            catch (Exception ex)
            {
                Logger.Trace("ERROR", "frmUpsellPrompt-MenuItemMoreButtonClick(): " + ex.Message, ex, true);
            }
        }

        private void DynamicButtonClick(object sender, EventArgs e)
        {
            try
            {
                Button btnDynamic = (Button)sender;
                foreach (Control control in flowLayoutPanelAttributes.Controls)
                    control.BackColor = SystemColors.Control;
                btnDynamic.BackColor = Color.FromArgb(0, 185, 241);
                AttribteCode = btnDynamic.Name;

            }
            catch (Exception ex)
            {
                Logger.Trace("ERROR", "frmUpsellPrompt-dynamicbuttonclick(): " + ex.Message, ex, true);
            }

        }

        private void btnNo_Click(object sender, EventArgs e)
        {
            AttribteCode = string.Empty;
            this.Close();
        }

        private void btnYes_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(AttribteCode))
            {
                CustomMessageBox.Show(MessageConstant.SelectAtleastoneOption, CustomMessageBox.Buttons.OK, CustomMessageBox.Icon.Warning);
                return;
            }
            this.Close();
        }
    }
}
