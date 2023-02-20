using JublFood.POS.App.Cache;
using JublFood.POS.App.Class;
using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace JublFood.POS.App.UserControls
{
    public partial class UC_Customer_order_Header : UserControl
    {
        //public Timer tm = new Timer();
        Timer tmrCurrent = null;
        Timer tmrElapsed = null;
        Timer tmrContinuous = null;
        System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
        private DateTime endTime;
        //Timer timer;
        Stopwatch sw;
        
        public UC_Customer_order_Header()
        {
            InitializeComponent();
            LoadlabelText();
        }
        public Label LabelOrderTaker { get; set; }

        public void LoadlabelText()
        {
            string labelText = null;
            if (Common.DictAllButtonText.TryGetValue(LanguageConstant.cintPOS, out labelText))
            {
                lblApplication.Text = labelText;
            }
            if (Common.DictAllButtonText.TryGetValue(LanguageConstant.cintStatus, out labelText))
            {
                lblOrdStatus.Text = labelText;
            }
            if (Common.DictAllButtonText.TryGetValue(LanguageConstant.cintNewOrder, out labelText))
            {
                lblOrderStatus.Text = labelText;
            }
            if (Common.DictAllButtonText.TryGetValue(LanguageConstant.cintElapsedTime, out labelText))
            {
                lblElapsed.Text = labelText;
            }
            if (Common.DictAllButtonText.TryGetValue(LanguageConstant.cintMin, out labelText))
            {
                lblMinText.Text = labelText;
            }
            if (Common.DictAllButtonText.TryGetValue(LanguageConstant.cintMax, out labelText))
            {
                lblMaxText.Text = labelText;
            }

            lblCurrentDate.Text = Session.SystemDate.Date.ToString("dd/MM/yyyy");
            //lblCurrentTime.Text = DateTime.Now.ToString("HH:mm");

            StartCurrentTimer();
            //btnStart_Click();
            //button1_Click();
            //StartElapseTimer();
            //btn_Import_Click();
            lblApplication.Text = Constants.Source;

            if (Session.IsTimerStarted)
            {
                btnConitnuousTimer();
                //btnStart_Click();
                //Session.CurrentElapsedTime = lblElapsedTime.Text;
            }

            if (Session.CurrentEmployee != null && Session.CurrentEmployee.LoginDetail != null)
            {
                //StartCurrentTimer();
                //btnStart_Click();
                if (Session.CurrentEmployee.LoginDetail.LastName == ".")
                {
                    Session.CurrentEmployee.LoginDetail.LastName = string.Empty;
                    lblOrderTaker.Text = Session.CurrentEmployee.LoginDetail.LastName + " " + Session.CurrentEmployee.LoginDetail.FirstName;
                }
                else
                {
                    lblOrderTaker.Text = Session.CurrentEmployee.LoginDetail.LastName + "," + " " + Session.CurrentEmployee.LoginDetail.FirstName;
                }

                this.lblOrderTaker.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.lblOrderTaker.ForeColor = System.Drawing.Color.Yellow;
                this.lblOrderTaker.Location = new System.Drawing.Point(65, 5);
            }
        }

        public void UpdateStatusBarTimes()
        {
            DateTime dtmServerDateTime;
            //string strMinutes;
            //string strSeconds;

            dtmServerDateTime = Convert.ToDateTime(SystemSettings.settings.pdtmServerDateTime);

            lblCurrentDate.Text = dtmServerDateTime.ToString("dd/MM/yyyy");
            lblCurrentTime.Text = dtmServerDateTime.ToString("HH:mm");

            lblElapsedTime.Text = CalculateElapseTime(dtmServerDateTime);
        }

        private string CalculateElapseTime(DateTime dtmStartingTime)
        {
            string result = string.Empty;
            string strMinutes;
            string strSeconds;

            //strMinutes = Int(Abs(DateDiff("s", dtmStartingTime, OrderCollection.Start_DateTime)) / 60)
            //strSeconds = Abs(DateDiff("s", dtmStartingTime, OrderCollection.Start_DateTime)) - (Val(strMinutes) * 60)

            //        If Len(Trim$(strMinutes)) = 1 Then
            //   strMinutes = "0" & strMinutes
            //End If


            //If Len(Trim$(strSeconds)) = 1 Then
            //    strSeconds = "0" & strSeconds
            //End If

            //CalculateElapseTime = strMinutes & ":" & Left$(strSeconds, 2)



            return result;
        }

        private void btn_Import_Click()
        {
            timer = new Timer();
            //timer.Interval = (1000);
            timer.Tick += new EventHandler(timer_Tick);
            sw = new Stopwatch();
            timer.Start();
            sw.Start();

            // start processing emails

            // when finished 
            timer.Stop();
            sw.Stop();
            //lblTime.text = "Completed in " + sw.Elapsed.Seconds.ToString() + "seconds";
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            lblElapsedTime.Text = sw.Elapsed.Seconds.ToString();
            Application.DoEvents();
        }

        void tm_Tick(object sender, EventArgs e)
        {
            lblElapsedTime.Text = DateTime.Now.ToLongTimeString();
        }

        private void StartCurrentTimer()
        {
            tmrCurrent = new System.Windows.Forms.Timer();
            //tmrCurrent.Interval = 1000;
            tmrCurrent.Tick += new EventHandler(tmr_Tick);
            tmrCurrent.Enabled = true;
        }

        private void StartElapseTimer()
        {
            tmrElapsed = new System.Windows.Forms.Timer();
            tmrElapsed.Interval = 1000;
            tmrElapsed.Tick += new EventHandler(tmr_TickElapsed);
            tmrElapsed.Enabled = true;
        }

        void tmr_Tick(object sender, EventArgs e)
        {
            lblCurrentTime.Text = DateTime.Now.ToString("HH:mm");
        }

        void tmr_TickElapsed(object sender, EventArgs e)
        {
            lblElapsed.Text = DateTime.Now.ToString("HH:mm");
        }

        private void button1_Click()
        {
            var minutes = 0;
            //if (int.TryParse(lblElapsedTime.Text, out minutes) && timer.Enabled == false)
            if (timer.Enabled == false)
            {
                endTime = DateTime.Now.AddMinutes(minutes);
                timer.Interval = 1000;
                timer.Tick -= new EventHandler(timer_TickElapsed);
                timer.Tick += new EventHandler(timer_TickElapsed);
                timer.Start();
                UpdateText();
            }
        }
        void timer_TickElapsed(object sender, EventArgs e)
        {
            UpdateText();
        }
        void UpdateText()
        {
            var diff = endTime.Subtract(DateTime.Now);
            if (diff.TotalSeconds > 0)
                lblElapsedTime.Text = string.Format("{0:D2}:{1:D2}:{2:D2}",
                                           diff.Hours, diff.Minutes, diff.Seconds);
            else
            {
                this.Text = "00:00:00";
                timer.Enabled = false;
            }
        }

        public void btnStart_Click()
        {   if (lblElapsedTime.Text != "00:00") return;
            //tmrClock.Enabled = !tmrClock.Enabled;
            //btnStart.Text = tmrClock.Enabled ? "Stop" : "Start";
            tmrElapsed = new System.Windows.Forms.Timer();
            //tmrElapsed.Interval = 1000;
            tmrElapsed.Tick += new EventHandler(tmrClock_Tick);
            tmrElapsed.Enabled = true;
            if (!Session.IsTimerStarted) Session.StartTime = DateTime.Now;
            Session.IsTimerStarted = true;
        }

        public void btnStop_Click()
        {
            Session.IsTimerStarted = false;
            lblElapsedTime.Text = "00:00";
            tmrElapsed.Enabled = false;
            //tmrContinuous.Enabled = false;
            //tmrClock.Enabled = !tmrClock.Enabled;
            //btnStart.Text = tmrClock.Enabled ? "Stop" : "Start";
            //tmrElapsed = new System.Windows.Forms.Timer();
            ////tmrElapsed.Interval = 1000;
            //tmrElapsed.Tick += new EventHandler(tmrClock_Tick);
            //tmrElapsed.Enabled = true;
            //StartTime = DateTime.Now;
            //Session.IsTimerStarted = false;
        }

        public void btnConitnuousTimer()
        {
            //tmrClock.Enabled = !tmrClock.Enabled;
            //btnStart.Text = tmrClock.Enabled ? "Stop" : "Start";
            tmrContinuous = new System.Windows.Forms.Timer();
            //tmrElapsed.Interval = 1000;
            tmrContinuous.Tick += new EventHandler(tmrCont_Tick);
            tmrContinuous.Enabled = true;
            //StartTime = DateTime.Now;
        }

        public void tmrClock_Tick(object sender, EventArgs e)
        {
            TimeSpan elapsed = DateTime.Now - Session.StartTime;

            // Start with the days if greater than 0.
            string text = "";
            //if (elapsed.Days > 0)
            //    text += elapsed.Days.ToString() + ".";

            // Convert milliseconds into tenths of seconds.
            int tenths = elapsed.Milliseconds / 100;

            //if(!string.IsNullOrEmpty(Session.CurrentElapsedTime))
            //{
            //    text = Session.CurrentElapsedTime;
            //}

            text +=
            //elapsed.Hours.ToString("00") + ":" +
            elapsed.Minutes.ToString("00") + ":" +
            elapsed.Seconds.ToString("00");
            //elapsed.Seconds.ToString("00") + "." +
            //tenths.ToString("0");

            // Compose the rest of the elapsed time.
            //if(!Session.FormOrderLoad)
            //{
            //    text +=
            //    //elapsed.Hours.ToString("00") + ":" +
            //    elapsed.Minutes.ToString("00") + ":" +
            //    elapsed.Seconds.ToString("00");
            //    //elapsed.Seconds.ToString("00") + "." +
            //    //tenths.ToString("0");
            //}
            //else
            //{
            //    text +=
            //    //elapsed.Hours.ToString("00") + ":" +
            //    elapsed.Minutes.ToString("00") + ":" +
            //    elapsed.Seconds.ToString("00");
            //    //elapsed.Seconds.ToString("00") + "." +
            //    //tenths.ToString("0");
            //}


            this.lblElapsedTime.Text = text;
            Session.CurrentElapsedTime = lblElapsedTime.Text;
        }

        public void tmrCont_Tick(object sender, EventArgs e)
        {
            //TimeSpan elapsed = DateTime.Now - StartTime;

            // Start with the days if greater than 0.
            //string text = "";
            //if (elapsed.Days > 0)
            //    text += elapsed.Days.ToString() + ".";

            // Convert milliseconds into tenths of seconds.
            //int tenths = elapsed.Milliseconds / 100;

            //if(!string.IsNullOrEmpty(Session.CurrentElapsedTime))
            //{
            //    text = Session.CurrentElapsedTime;
            //}

            //text +=
            ////elapsed.Hours.ToString("00") + ":" +
            //elapsed.Minutes.ToString("00") + ":" +
            //elapsed.Seconds.ToString("00");
            ////elapsed.Seconds.ToString("00") + "." +
            //tenths.ToString("0");

            // Compose the rest of the elapsed time.
            //if(!Session.FormOrderLoad)
            //{
            //    text +=
            //    //elapsed.Hours.ToString("00") + ":" +
            //    elapsed.Minutes.ToString("00") + ":" +
            //    elapsed.Seconds.ToString("00");
            //    //elapsed.Seconds.ToString("00") + "." +
            //    //tenths.ToString("0");
            //}
            //else
            //{
            //    text +=
            //    //elapsed.Hours.ToString("00") + ":" +
            //    elapsed.Minutes.ToString("00") + ":" +
            //    elapsed.Seconds.ToString("00");
            //    //elapsed.Seconds.ToString("00") + "." +
            //    //tenths.ToString("0");
            //}


            this.lblElapsedTime.Text = Session.CurrentElapsedTime;
            //Session.CurrentElapsedTime = lblElapsedTime.Text;
        }
    }
}
