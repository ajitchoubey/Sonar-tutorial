using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace JublFood.POS.App
{
    public partial class frmSplash : Form
    {
        public frmSplash()
        {
            try
            {
                InitializeComponent();
            }
            catch (Exception ex)
            {

            }
        }

        private void frmSplash_Load(object sender, EventArgs e)
        {
            try
            {
                lblVersion.Text = "Version " + Assembly.GetEntryAssembly().GetName().Version.ToString();
                lbllastUpdated.Text = "Last Updated -" + (new FileInfo(Assembly.GetEntryAssembly().Location).LastWriteTime).ToString("MM/dd/yyyy hh:mm:ss tt");
                lblCopyright.Text = lblCopyright.Text.Replace("Year", DateTime.Now.Year.ToString());
                progressBarSplash.Maximum = 11;
                progressBarSplash.Step = 1;
                timerSplash.Start();
            }
            catch (Exception ex)
            {

            }
        }

        private void timerSplash_Tick(object sender, EventArgs e)
        {
            try
            {
                if (progressBarSplash.Value + 1 < progressBarSplash.Maximum)
                    progressBarSplash.Value++;

                switch (progressBarSplash.Value)
                {
                    case 1:
                    case 2:
                        lblProgressStatus.Text = "Validating Version";
                        break;
                    case 3:
                    case 4:
                        lblProgressStatus.Text = "API Health Checkup in Progress";
                        break;
                    case 5:
                    case 6:
                        lblProgressStatus.Text = "Loading Settings";
                        break;
                    case 7:
                    case 8:
                    case 9:
                        lblProgressStatus.Text = "Fetching Session Data";
                        break;
                    case 10:
                        lblProgressStatus.Text = "Launching Now";
                        break;
                }

                Form frmObj = Application.OpenForms["frmLogin"];
                if (frmObj != null)
                {
                    progressBarSplash.Value = progressBarSplash.Maximum;
                    this.Close();
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}
