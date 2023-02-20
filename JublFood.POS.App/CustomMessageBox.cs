using Jublfood.AppLogger;
using JublFood.POS.App.BusinessLayer;
using JublFood.POS.App.Cache;
using JublFood.POS.App.Class;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;


namespace JublFood.POS.App
{
    public partial class CustomMessageBox : Form
    {        
        private const int CS_DROPSHADOW = 0x00020000;
        private static CustomMessageBox _msgBox;
        private Panel _plHeader = new Panel();
        private Panel _plFooter = new Panel();
        private Panel _plIcon = new Panel();
        private PictureBox _picIcon = new PictureBox();
        private FlowLayoutPanel _flpButtons = new FlowLayoutPanel();
        //private Label _lblTitle;
        private Label _lblMessage;
        private List<Button> _buttonCollection = new List<Button>();
        private static DialogResult _buttonResult = new DialogResult();
        private static Point lastMousePos;

        private CustomMessageBox()
        {
            #region InitializeComponent

            this.FormBorderStyle = FormBorderStyle.None;
            //this.BackColor = Color.White; //Color.FromArgb(45, 45, 48);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Padding = new Padding(3);
            this.Width = 200;
            this.Load += new System.EventHandler(this.CustomMessageBox_Load);

            //_lblTitle = new Label();
            //_lblTitle.ForeColor = Color.Black;//Color.White;
            //_lblTitle.Font = new Font("Segoe UI", 9);
            //_lblTitle.Dock = DockStyle.Top;
            //_lblTitle.TextAlign = ContentAlignment.TopLeft;
            //_lblTitle.Height = 30;

            _lblMessage = new Label();
            _lblMessage.ForeColor = Color.Black;//Color.White;
            _lblMessage.Font = new Font("Segoe UI", 10);
            _lblMessage.TextAlign = ContentAlignment.MiddleLeft;
            _lblMessage.Dock = DockStyle.Fill;

            _flpButtons.FlowDirection = FlowDirection.LeftToRight;
            _flpButtons.Dock = DockStyle.Fill;

            _plHeader.Dock = DockStyle.Fill;
            _plHeader.Padding = new Padding(2);
            //_plHeader.Controls.Add(_lblTitle);
            _plHeader.Controls.Add(_lblMessage);

            _plFooter.Dock = DockStyle.Bottom;
            _plFooter.Padding = new Padding(10);
            //_plFooter.BackColor = Color.White; //Color.FromArgb(37, 37, 38);
            _plFooter.Height = 65;
            _plFooter.Controls.Add(_flpButtons);

            _picIcon.Width = 32;
            _picIcon.Height = 32;
            _picIcon.Location = new Point(15, 40);

            _plIcon.Dock = DockStyle.Left;
            _plIcon.Padding = new Padding(10);
            _plIcon.Width = 60;
            _plIcon.Controls.Add(_picIcon);

            List<Control> controlCollection = new List<Control>();
            controlCollection.Add(this);
            //controlCollection.Add(_lblTitle);
            controlCollection.Add(_lblMessage);
            controlCollection.Add(_flpButtons);
            controlCollection.Add(_plHeader);
            controlCollection.Add(_plFooter);
            controlCollection.Add(_plIcon);
            controlCollection.Add(_picIcon);

            foreach (Control control in controlCollection)
            {
                control.MouseDown += CustomMessageBox_MouseDown;
                control.MouseMove += CustomMessageBox_MouseMove;
            }

            this.Controls.Add(_plHeader);
            this.Controls.Add(_plIcon);
            this.Controls.Add(_plFooter);            

            #endregion
        }

        internal static void Show(string v)
        {
            throw new NotImplementedException();
        }

        private static void CustomMessageBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                lastMousePos = new Point(e.X, e.Y);
            }
        }


        private static void CustomMessageBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                _msgBox.Left += e.X - lastMousePos.X;
                _msgBox.Top += e.Y - lastMousePos.Y;
            }
        }

        public static DialogResult Show(object p, string message)
        {
            if (String.IsNullOrWhiteSpace(message))
                message = string.Empty;
            _msgBox = new CustomMessageBox();
            //_msgBox._lblTitle.Text = MessageConstant.AppTitle;
            _msgBox._lblMessage.Text = message;
            _msgBox._plIcon.Hide();

            CustomMessageBox.InitButtons(Buttons.OK);
            
            _msgBox.Size = CustomMessageBox.MessageSize(message);
            SplashThread.StopSplashThread();
            _msgBox.ShowDialog();
            return _buttonResult;
        }

        public static DialogResult Show(string message, Buttons buttons)
        {
            if (String.IsNullOrWhiteSpace(message))
                message = string.Empty;
            _msgBox = new CustomMessageBox();
            //_msgBox._lblTitle.Text = MessageConstant.AppTitle;
            _msgBox._lblMessage.Text = message;
            _msgBox._plIcon.Hide();

            CustomMessageBox.InitButtons(buttons);

            _msgBox.Size = CustomMessageBox.MessageSize(message);
            SplashThread.StopSplashThread();
            _msgBox.ShowDialog();
            return _buttonResult;
        }

        public static DialogResult Show(string message, Buttons buttons, Icon icon)
        {
            if (String.IsNullOrWhiteSpace(message))
                message = string.Empty;
            _msgBox = new CustomMessageBox();
            //_msgBox._lblTitle.Text = MessageConstant.AppTitle;
            _msgBox._lblMessage.Text = message;

            CustomMessageBox.InitButtons(buttons);
            CustomMessageBox.InitIcon(icon);

            _msgBox.Size = CustomMessageBox.MessageSize(message);
            SplashThread.StopSplashThread();
            _msgBox.ShowDialog();
            return _buttonResult;
        }

        private static void InitButtons(Buttons buttons)
        {
            switch (buttons)
            {
                case CustomMessageBox.Buttons.AbortRetryIgnore:
                    _msgBox.InitAbortRetryIgnoreButtons();
                    break;

                case CustomMessageBox.Buttons.OK:
                    _msgBox.InitOKButton();
                    break;

                case CustomMessageBox.Buttons.OKCancel:
                    _msgBox.InitOKCancelButtons();
                    break;

                case CustomMessageBox.Buttons.RetryCancel:
                    _msgBox.InitRetryCancelButtons();
                    break;

                case CustomMessageBox.Buttons.YesNo:
                    _msgBox.InitYesNoButtons();
                    break;

                case CustomMessageBox.Buttons.YesNoCancel:
                    _msgBox.InitYesNoCancelButtons();
                    break;
            }

            foreach (Button btn in _msgBox._buttonCollection)
            {
                btn.ForeColor = Color.Black; //Color.FromArgb(170, 170, 170);
                btn.Font = new Font("Segoe UI", 8);
                btn.Padding = new Padding(3);
                btn.FlatStyle = FlatStyle.Flat;
                btn.Height = 30;
                btn.FlatAppearance.BorderColor = Color.FromArgb(99, 99, 98);
                btn.Cursor = Cursors.Hand;
                _msgBox._flpButtons.Controls.Add(btn);
            }

            if (_msgBox._flpButtons.Controls.Count == 3)
                _msgBox._flpButtons.Padding = new Padding(70, 0, 0, 0);
            else if (_msgBox._flpButtons.Controls.Count == 2)
                _msgBox._flpButtons.Padding = new Padding(150, 0, 0, 0);
            else if (_msgBox._flpButtons.Controls.Count == 1)
                _msgBox._flpButtons.Padding = new Padding(230, 0, 0, 0);
        }

        private static void InitIcon(Icon icon)
        {
            switch (icon)
            {
                case CustomMessageBox.Icon.Application:
                    _msgBox._picIcon.Image = SystemIcons.Application.ToBitmap();
                    break;

                case CustomMessageBox.Icon.Exclamation:
                    _msgBox._picIcon.Image = SystemIcons.Exclamation.ToBitmap();
                    break;

                case CustomMessageBox.Icon.Error:
                    _msgBox._picIcon.Image = SystemIcons.Error.ToBitmap();
                    break;

                case CustomMessageBox.Icon.Info:
                    _msgBox._picIcon.Image = SystemIcons.Information.ToBitmap();
                    break;

                case CustomMessageBox.Icon.Question:
                    _msgBox._picIcon.Image = SystemIcons.Question.ToBitmap();
                    break;

                case CustomMessageBox.Icon.Shield:
                    _msgBox._picIcon.Image = SystemIcons.Shield.ToBitmap();
                    break;

                case CustomMessageBox.Icon.Warning:
                    _msgBox._picIcon.Image = SystemIcons.Warning.ToBitmap();
                    break;
            }
        }

        private void InitAbortRetryIgnoreButtons()
        {
            Button btnAbort = new Button();
            btnAbort.Text = "Abort";
            btnAbort.Click += ButtonClick;

            Button btnRetry = new Button();
            btnRetry.Text = "Retry";
            btnRetry.Click += ButtonClick;

            Button btnIgnore = new Button();
            btnIgnore.Text = "Ignore";
            btnIgnore.Click += ButtonClick;

            this._buttonCollection.Add(btnIgnore);
            this._buttonCollection.Add(btnRetry);
            this._buttonCollection.Add(btnAbort);
            
            
        }

        private void InitOKButton()
        {
            Button btnOK = new Button();
            btnOK.Text = "OK";
            btnOK.Click += ButtonClick;

            this._buttonCollection.Add(btnOK);
        }

        private void InitOKCancelButtons()
        {
            Button btnOK = new Button();
            btnOK.Text = "OK";
            btnOK.Click += ButtonClick;

            Button btnCancel = new Button();
            btnCancel.Text = "Cancel";
            btnCancel.Click += ButtonClick;

            this._buttonCollection.Add(btnOK);
            this._buttonCollection.Add(btnCancel);
        }

        private void InitRetryCancelButtons()
        {
            Button btnRetry = new Button();
            btnRetry.Text = "Retry";
            btnRetry.Click += ButtonClick;

            Button btnCancel = new Button();
            btnCancel.Text = "Cancel";
            btnCancel.Click += ButtonClick;

            this._buttonCollection.Add(btnRetry);
            this._buttonCollection.Add(btnCancel);
        }

        private void InitYesNoButtons()
        {
            Button btnYes = new Button();
            btnYes.Text = "Yes";
            btnYes.Click += ButtonClick;

            Button btnNo = new Button();
            btnNo.Text = "No";
            btnNo.Click += ButtonClick;

            this._buttonCollection.Add(btnYes);
            this._buttonCollection.Add(btnNo);
        }

        private void InitYesNoCancelButtons()
        {
            Button btnYes = new Button();
            btnYes.Text = "Yes";
            btnYes.Click += ButtonClick;

            Button btnNo = new Button();
            btnNo.Text = "No";
            btnNo.Click += ButtonClick;

            Button btnCancel = new Button();
            btnCancel.Text = "Cancel";
            btnCancel.Click += ButtonClick;

            this._buttonCollection.Add(btnYes);
            this._buttonCollection.Add(btnNo);
            this._buttonCollection.Add(btnCancel);
        }

        private static void ButtonClick(object sender, EventArgs e)
        {
            Button btn = (Button)sender;

            switch (btn.Text)
            {
                case "Abort":
                    _buttonResult = DialogResult.Abort;
                    break;

                case "Retry":
                    _buttonResult = DialogResult.Retry;
                    break;

                case "Ignore":
                    _buttonResult = DialogResult.Ignore;
                    break;

                case "OK":
                    _buttonResult = DialogResult.OK;
                    break;

                case "Cancel":
                    _buttonResult = DialogResult.Cancel;
                    break;

                case "Yes":
                    _buttonResult = DialogResult.Yes;
                    break;

                case "No":
                    _buttonResult = DialogResult.No;
                    break;
            }

            _msgBox.Dispose();
        }

        private static Size MessageSize(string message)
        {
            Graphics g = _msgBox.CreateGraphics();
            int width = 350;
            int height = 170;//230  

            SizeF size = g.MeasureString(message, new Font("Segoe UI", 10));

            if (message.Length < 150)
            {
                if ((int)size.Width > 350)
                {
                    width = (int)size.Width;
                }
            }
            else
            {
                string[] groups = (from Match m in Regex.Matches(message, ".{1,180}") select m.Value).ToArray();
                int lines = groups.Length;
                width = 490;
                height += (int)(size.Height) * lines;
            }
            return new Size(width, height);
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ClassStyle |= CS_DROPSHADOW;
                return cp;
            }
        }

        public enum Buttons
        {
            AbortRetryIgnore = 1,
            OK = 2,
            OKCancel = 3,
            RetryCancel = 4,
            YesNo = 5,
            YesNoCancel = 6
        }

        public enum Icon
        {
            Application = 1,
            Exclamation = 2,
            Error = 3,
            Warning = 4,
            Info = 5,
            Question = 6,
            Shield = 7,
            Search = 8
        }

        private void CustomMessageBox_Load(object sender, EventArgs e)
        {
            this.Activate();
        }
    }
}
