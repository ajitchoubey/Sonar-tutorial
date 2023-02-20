
namespace JublFood.POS.App
{
    partial class frmUpsellPrompt
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmUpsellPrompt));
            this.tlpUpsellPrompt = new System.Windows.Forms.TableLayoutPanel();
            this.lblmsg = new System.Windows.Forms.Label();
            this.flowLayoutPanelAttributes = new System.Windows.Forms.FlowLayoutPanel();
            this.btnYes = new System.Windows.Forms.Button();
            this.btnNo = new System.Windows.Forms.Button();
            this.tlpUpsellPrompt.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlpUpsellPrompt
            // 
            this.tlpUpsellPrompt.BackColor = System.Drawing.Color.Teal;
            this.tlpUpsellPrompt.ColumnCount = 3;
            this.tlpUpsellPrompt.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.tlpUpsellPrompt.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tlpUpsellPrompt.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tlpUpsellPrompt.Controls.Add(this.lblmsg, 0, 0);
            this.tlpUpsellPrompt.Controls.Add(this.flowLayoutPanelAttributes, 0, 1);
            this.tlpUpsellPrompt.Controls.Add(this.btnYes, 1, 2);
            this.tlpUpsellPrompt.Controls.Add(this.btnNo, 2, 2);
            this.tlpUpsellPrompt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpUpsellPrompt.Location = new System.Drawing.Point(0, 0);
            this.tlpUpsellPrompt.Name = "tlpUpsellPrompt";
            this.tlpUpsellPrompt.RowCount = 3;
            this.tlpUpsellPrompt.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tlpUpsellPrompt.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 66.66666F));
            this.tlpUpsellPrompt.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 19.04762F));
            this.tlpUpsellPrompt.Size = new System.Drawing.Size(494, 336);
            this.tlpUpsellPrompt.TabIndex = 1;
            // 
            // lblmsg
            // 
            this.lblmsg.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblmsg.AutoSize = true;
            this.tlpUpsellPrompt.SetColumnSpan(this.lblmsg, 3);
            this.lblmsg.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblmsg.Location = new System.Drawing.Point(3, 3);
            this.lblmsg.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.lblmsg.Name = "lblmsg";
            this.lblmsg.Size = new System.Drawing.Size(488, 44);
            this.lblmsg.TabIndex = 4;
            this.lblmsg.Text = "Menu Item Search bjfg jjgfgn jnjrnjntjr jrnjtnrjt   jrnjtnrt  jrntjr\r\nhgfhg\r\n";
            this.lblmsg.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // flowLayoutPanelAttributes
            // 
            this.tlpUpsellPrompt.SetColumnSpan(this.flowLayoutPanelAttributes, 3);
            this.flowLayoutPanelAttributes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanelAttributes.Location = new System.Drawing.Point(5, 47);
            this.flowLayoutPanelAttributes.Margin = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.flowLayoutPanelAttributes.Name = "flowLayoutPanelAttributes";
            this.flowLayoutPanelAttributes.Size = new System.Drawing.Size(489, 223);
            this.flowLayoutPanelAttributes.TabIndex = 3;
            // 
            // btnYes
            // 
            this.btnYes.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnYes.Image = ((System.Drawing.Image)(resources.GetObject("btnYes.Image")));
            this.btnYes.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnYes.Location = new System.Drawing.Point(311, 275);
            this.btnYes.Name = "btnYes";
            this.btnYes.Size = new System.Drawing.Size(68, 55);
            this.btnYes.TabIndex = 5;
            this.btnYes.Text = "Yes";
            this.btnYes.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnYes.UseVisualStyleBackColor = true;
            this.btnYes.Click += new System.EventHandler(this.btnYes_Click);
            // 
            // btnNo
            // 
            this.btnNo.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnNo.Image = ((System.Drawing.Image)(resources.GetObject("btnNo.Image")));
            this.btnNo.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnNo.Location = new System.Drawing.Point(410, 275);
            this.btnNo.Name = "btnNo";
            this.btnNo.Size = new System.Drawing.Size(68, 55);
            this.btnNo.TabIndex = 6;
            this.btnNo.Text = "No";
            this.btnNo.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnNo.UseVisualStyleBackColor = true;
            this.btnNo.Click += new System.EventHandler(this.btnNo_Click);
            // 
            // frmUpsellPrompt
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(494, 336);
            this.Controls.Add(this.tlpUpsellPrompt);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmUpsellPrompt";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "frmUpsellPrompt";
            this.Load += new System.EventHandler(this.frmUpsellPrompt_Load);
            this.tlpUpsellPrompt.ResumeLayout(false);
            this.tlpUpsellPrompt.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpUpsellPrompt;
        private System.Windows.Forms.Label lblmsg;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelAttributes;
        private System.Windows.Forms.Button btnYes;
        private System.Windows.Forms.Button btnNo;
    }
}