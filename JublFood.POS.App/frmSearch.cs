using Jublfood.AppLogger;
using JublFood.POS.App.API;
using JublFood.POS.App.Cache;
using JublFood.POS.App.Class;
using JublFood.POS.App.Models.Catalog;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JublFood.POS.App
{
    public partial class frmSearch : Form
    {
        private List<CatalogMenuItems> catalogMenuItems;
        private bool checkBoxVegOnly;
        private int MaxMenuItemsPerPage;
        public frmSearch(bool VegOnly)
        {
            InitializeComponent();
            checkBoxVegOnly = VegOnly;
        }

        private void PopulateMenuItems(int PageNo)
        {
            int x = Constants.HorizontalSpace, y = Constants.VerticalSpace;
            int startIndex = 0;
            int LastIndex = 0;
            bool CreateMoreButton = false;

            if (checkBoxVegOnly)
                catalogMenuItems = catalogMenuItems.FindAll(z => z.MenuItemType == false);

            Session.TotalPageMenuItems = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(catalogMenuItems.Count) / MaxMenuItemsPerPage));

            if (MaxMenuItemsPerPage < catalogMenuItems.Count)
                CreateMoreButton = true;

            if (PageNo == 1)
            {
                startIndex = 0;
                if (CreateMoreButton)
                    LastIndex = MaxMenuItemsPerPage - 1;
                else
                    LastIndex = catalogMenuItems.Count;
            }
            else
            {
                startIndex = (MaxMenuItemsPerPage - 1) * (PageNo - 1);
                LastIndex = (catalogMenuItems.Count - startIndex) > (MaxMenuItemsPerPage - 1) ? ((MaxMenuItemsPerPage - 1) * (PageNo - 1)) + (MaxMenuItemsPerPage - 1) : catalogMenuItems.Count;
            }


            for (int i = startIndex; i < LastIndex; i++)
            {
                Button btnDynamic = new Button();

                btnDynamic.Location = new System.Drawing.Point(x, y);
                btnDynamic.Size = new System.Drawing.Size(Constants.MenuCardButtonWidthG, Constants.ButtonHeightG);

                btnDynamic.Name = catalogMenuItems[i].Menu_Code.Trim();
                btnDynamic.Tag = "MenuItems";
                btnDynamic.Text = catalogMenuItems[i].Order_Description.Trim();
                btnDynamic.Font = new Font(new FontFamily("Microsoft Sans Serif"), 8);

                btnDynamic.BackColor = SystemColors.Control;
                if (catalogMenuItems[i].MenuItemType == false)
                {
                    btnDynamic.FlatStyle = FlatStyle.Flat;
                    btnDynamic.FlatAppearance.BorderColor = Session.vegColor;
                    btnDynamic.FlatAppearance.BorderSize = 3;
                }
                else if (catalogMenuItems[i].MenuItemType == true)
                {
                    btnDynamic.FlatStyle = FlatStyle.Flat;
                    btnDynamic.FlatAppearance.BorderColor = Session.nonVegColor;
                    btnDynamic.FlatAppearance.BorderSize = 3;
                }
                else if (catalogMenuItems[i].MenuItemType == null)
                {
                    btnDynamic.BackColor = SystemColors.Control;
                }

                btnDynamic.Margin = new Padding(0);
                btnDynamic.Enabled = catalogMenuItems[i].Enabled ? catalogMenuItems[i].EnabledInv : catalogMenuItems[i].Enabled;
                if (catalogMenuItems[i].Menu_Item_Image != null)
                {
                    byte[] binaryData = Convert.FromBase64String(catalogMenuItems[i].Menu_Item_Image);
                    btnDynamic.Image = Image.FromStream(new MemoryStream(binaryData));
                    btnDynamic.TextAlign = ContentAlignment.BottomCenter;
                    btnDynamic.ImageAlign = ContentAlignment.TopCenter;
                }
                flowLayoutPanelMenuItems.Controls.Add(btnDynamic);
                x += Constants.MenuCardButtonWidthG;
                btnDynamic.Click += new EventHandler(this.DynamicButtonClick);
                if (Session.VegOnlySelected)
                {
                    if (btnDynamic.FlatAppearance.BorderColor == Session.nonVegColor)
                    {
                        btnDynamic.Visible = false;
                    }
                }
                else
                {
                    if (btnDynamic.FlatAppearance.BorderColor == Session.nonVegColor)
                    {
                        btnDynamic.Visible = true;
                    }
                }
            }

            if (CreateMoreButton)
            {
                Button btn = new Button();
                btn.Location = new System.Drawing.Point(x, y);
                btn.Size = new System.Drawing.Size(Constants.MenuCardButtonWidthG, Constants.ButtonHeightG);
                btn.Name = "btnMore";
                btn.Tag = PageNo;
                btn.Font = new Font(new FontFamily("Microsoft Sans Serif"), 8);
                btn.BackColor = SystemColors.Control;
                btn.Margin = new Padding(0);
                btn.TextAlign = ContentAlignment.BottomCenter;
                btn.TextImageRelation = TextImageRelation.ImageAboveText;
                //if (showBackButton)
                //{
                //    btn.Text = "Back";
                //    btn.BackgroundImage = Properties.Resources._32;
                //}
                //else
                //{
                btn.Text = "More";
                btn.BackgroundImage = Properties.Resources._33;
                //}

                btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
                flowLayoutPanelMenuItems.Controls.Add(btn);
                btn.Click += new EventHandler(this.MenuItemMoreButtonClick);
            }

            Session.currentMenuItemPageNo = PageNo;
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                flowLayoutPanelMenuItems.Controls.Clear();

                if (!string.IsNullOrEmpty(txtSearch.Text.Trim()))
                {
                    catalogMenuItems = Session.AllCatalogMenuItems.FindAll(x => x.Order_Type_Code == Session.selectedOrderType && x.Order_Description.ToLower().Contains(txtSearch.Text.Trim().ToLower()));

                    PopulateMenuItems(1);
                }
            }
            catch (Exception ex)
            {
                Logger.Trace("ERROR", "frmSearch-txtSearch_TextChanged(): " + ex.Message, ex, true);
            }
        }

        private void MenuItemMoreButtonClick(object sender, EventArgs e)
        {
            try
            {
                flowLayoutPanelMenuItems.Visible = true;
                flowLayoutPanelMenuItems.BringToFront();
                flowLayoutPanelMenuItems.Controls.Clear();

                int CurrentPageNo = Convert.ToInt32(((Button)sender).Tag);
                int NextPageNo = 0;

                if (CurrentPageNo + 1 > Session.TotalPageMenuItems)
                    NextPageNo = 1;
                else
                    NextPageNo = CurrentPageNo + 1;

                PopulateMenuItems(NextPageNo);
            }
            catch (Exception ex)
            {
                Logger.Trace("ERROR", "frmSearch-MenuItemMoreButtonClick(): " + ex.Message, ex, true);
            }
        }

        private void DynamicButtonClick(object sender, EventArgs e)
        {
            try
            {
                Button btnDynamic = (Button)sender;
                frmOrder frmord = null;

                foreach (Form form in Application.OpenForms)
                {
                    if (form.Name == "frmOrder")
                        frmord = (frmOrder)form;
                }

                if (frmord != null)
                {
                    this.Close();
                    frmord.AddItem(Convert.ToString(btnDynamic.Tag), btnDynamic.Name);
                }
                
            }
            catch (Exception ex)
            {
                Logger.Trace("ERROR", "frmSearch-dynamicbuttonclick(): " + ex.Message, ex, true);
            }

        }

        private void frmSearch_Load(object sender, EventArgs e)
        {
            try
            {
                txtSearch.Clear();
                //MaxMenuItemsPerPage = ((flowLayoutPanelMenuItems.Width - Constants.HorizontalSpace) / Constants.MenuCardButtonWidthG) * (((flowLayoutPanelMenuItems.Location.Y == 114 ? flowLayoutPanelMenuItems.Height - 57 : flowLayoutPanelMenuItems.Height) - Constants.VerticalSpace) / Constants.ButtonHeightG);
                MaxMenuItemsPerPage = ((flowLayoutPanelMenuItems.Width - Constants.HorizontalSpace) / Constants.MenuCardButtonWidthG) * ((flowLayoutPanelMenuItems.Height - Constants.VerticalSpace) / Constants.ButtonHeightG);

            }
            catch (Exception ex)
            {
                Logger.Trace("ERROR", "frmSearch-frmSearch_Load(): " + ex.Message, ex, true);
            }
        }

        private void frmSearch_Activated(object sender, EventArgs e)
        {
            txtSearch.Focus();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmdKeyboard_Click(object sender, EventArgs e)
        {
            using (frmKeyBoard objfrmKeyBoard = new frmKeyBoard(txtSearch, "Menu Item Search"))
            {
                objfrmKeyBoard.ShowDialog();
            }
        }
    }
}
