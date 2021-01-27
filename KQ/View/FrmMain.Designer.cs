
namespace KQ.View
{
    partial class FrmMain
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
            this.BtnSend = new System.Windows.Forms.Button();
            this.RtbMessage = new System.Windows.Forms.RichTextBox();
            this.TxtSendMsg = new System.Windows.Forms.TextBox();
            this.StsStatus = new System.Windows.Forms.StatusStrip();
            this.TssCurrentQInfo = new System.Windows.Forms.ToolStripStatusLabel();
            this.TacMainTabControl = new System.Windows.Forms.TabControl();
            this.TapMessages = new System.Windows.Forms.TabPage();
            this.LstSessions = new System.Windows.Forms.ListBox();
            this.TabGroupMsg = new System.Windows.Forms.TabPage();
            this.LstGroupMsg = new System.Windows.Forms.ListBox();
            this.TapContacts = new System.Windows.Forms.TabPage();
            this.LstContacts = new System.Windows.Forms.ListBox();
            this.TapGroups = new System.Windows.Forms.TabPage();
            this.LstGroups = new System.Windows.Forms.ListBox();
            this.NtfIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.StsStatus.SuspendLayout();
            this.TacMainTabControl.SuspendLayout();
            this.TapMessages.SuspendLayout();
            this.TabGroupMsg.SuspendLayout();
            this.TapContacts.SuspendLayout();
            this.TapGroups.SuspendLayout();
            this.SuspendLayout();
            // 
            // BtnSend
            // 
            this.BtnSend.Location = new System.Drawing.Point(783, 405);
            this.BtnSend.Name = "BtnSend";
            this.BtnSend.Size = new System.Drawing.Size(94, 29);
            this.BtnSend.TabIndex = 0;
            this.BtnSend.Text = "Send";
            this.BtnSend.UseVisualStyleBackColor = true;
            this.BtnSend.Click += new System.EventHandler(this.BtnSend_Click);
            // 
            // RtbMessage
            // 
            this.RtbMessage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.RtbMessage.Location = new System.Drawing.Point(250, 12);
            this.RtbMessage.Name = "RtbMessage";
            this.RtbMessage.Size = new System.Drawing.Size(627, 312);
            this.RtbMessage.TabIndex = 1;
            this.RtbMessage.Text = "";
            this.RtbMessage.ContentsResized += new System.Windows.Forms.ContentsResizedEventHandler(this.RtbMessage_ContentsResized);
            // 
            // TxtSendMsg
            // 
            this.TxtSendMsg.Location = new System.Drawing.Point(250, 330);
            this.TxtSendMsg.Multiline = true;
            this.TxtSendMsg.Name = "TxtSendMsg";
            this.TxtSendMsg.Size = new System.Drawing.Size(527, 104);
            this.TxtSendMsg.TabIndex = 3;
            // 
            // StsStatus
            // 
            this.StsStatus.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.StsStatus.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TssCurrentQInfo});
            this.StsStatus.Location = new System.Drawing.Point(0, 452);
            this.StsStatus.Name = "StsStatus";
            this.StsStatus.Size = new System.Drawing.Size(890, 26);
            this.StsStatus.TabIndex = 5;
            this.StsStatus.Text = "statusStrip1";
            // 
            // TssCurrentQInfo
            // 
            this.TssCurrentQInfo.Name = "TssCurrentQInfo";
            this.TssCurrentQInfo.Size = new System.Drawing.Size(94, 20);
            this.TssCurrentQInfo.Text = "CurrentQInfo";
            // 
            // TacMainTabControl
            // 
            this.TacMainTabControl.Alignment = System.Windows.Forms.TabAlignment.Left;
            this.TacMainTabControl.Controls.Add(this.TapMessages);
            this.TacMainTabControl.Controls.Add(this.TabGroupMsg);
            this.TacMainTabControl.Controls.Add(this.TapContacts);
            this.TacMainTabControl.Controls.Add(this.TapGroups);
            this.TacMainTabControl.Location = new System.Drawing.Point(12, 12);
            this.TacMainTabControl.Multiline = true;
            this.TacMainTabControl.Name = "TacMainTabControl";
            this.TacMainTabControl.SelectedIndex = 0;
            this.TacMainTabControl.Size = new System.Drawing.Size(236, 426);
            this.TacMainTabControl.TabIndex = 6;
            // 
            // TapMessages
            // 
            this.TapMessages.Controls.Add(this.LstSessions);
            this.TapMessages.Location = new System.Drawing.Point(30, 4);
            this.TapMessages.Name = "TapMessages";
            this.TapMessages.Size = new System.Drawing.Size(202, 418);
            this.TapMessages.TabIndex = 0;
            this.TapMessages.Text = "Messages";
            this.TapMessages.UseVisualStyleBackColor = true;
            // 
            // LstSessions
            // 
            this.LstSessions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LstSessions.ItemHeight = 20;
            this.LstSessions.Location = new System.Drawing.Point(0, 0);
            this.LstSessions.Name = "LstSessions";
            this.LstSessions.Size = new System.Drawing.Size(202, 418);
            this.LstSessions.TabIndex = 4;
            this.LstSessions.SelectedIndexChanged += new System.EventHandler(this.LstSessions_SelectedIndexChanged);
            // 
            // TabGroupMsg
            // 
            this.TabGroupMsg.Controls.Add(this.LstGroupMsg);
            this.TabGroupMsg.Location = new System.Drawing.Point(30, 4);
            this.TabGroupMsg.Name = "TabGroupMsg";
            this.TabGroupMsg.Size = new System.Drawing.Size(202, 418);
            this.TabGroupMsg.TabIndex = 3;
            this.TabGroupMsg.Text = "Group Messages";
            this.TabGroupMsg.UseVisualStyleBackColor = true;
            // 
            // LstGroupMsg
            // 
            this.LstGroupMsg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LstGroupMsg.ItemHeight = 20;
            this.LstGroupMsg.Location = new System.Drawing.Point(0, 0);
            this.LstGroupMsg.Name = "LstGroupMsg";
            this.LstGroupMsg.Size = new System.Drawing.Size(202, 418);
            this.LstGroupMsg.TabIndex = 0;
            this.LstGroupMsg.SelectedIndexChanged += new System.EventHandler(this.LstGroupMsg_SelectedIndexChanged);
            // 
            // TapContacts
            // 
            this.TapContacts.Controls.Add(this.LstContacts);
            this.TapContacts.Location = new System.Drawing.Point(30, 4);
            this.TapContacts.Name = "TapContacts";
            this.TapContacts.Size = new System.Drawing.Size(202, 418);
            this.TapContacts.TabIndex = 1;
            this.TapContacts.Text = "Contacts";
            this.TapContacts.UseVisualStyleBackColor = true;
            // 
            // LstContacts
            // 
            this.LstContacts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LstContacts.ItemHeight = 20;
            this.LstContacts.Location = new System.Drawing.Point(0, 0);
            this.LstContacts.Name = "LstContacts";
            this.LstContacts.Size = new System.Drawing.Size(202, 418);
            this.LstContacts.TabIndex = 0;
            this.LstContacts.SelectedIndexChanged += new System.EventHandler(this.LstContacts_SelectedIndexChanged);
            // 
            // TapGroups
            // 
            this.TapGroups.Controls.Add(this.LstGroups);
            this.TapGroups.Location = new System.Drawing.Point(30, 4);
            this.TapGroups.Name = "TapGroups";
            this.TapGroups.Size = new System.Drawing.Size(202, 418);
            this.TapGroups.TabIndex = 2;
            this.TapGroups.Text = "Groups";
            this.TapGroups.UseVisualStyleBackColor = true;
            // 
            // LstGroups
            // 
            this.LstGroups.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LstGroups.FormattingEnabled = true;
            this.LstGroups.ItemHeight = 20;
            this.LstGroups.Location = new System.Drawing.Point(0, 0);
            this.LstGroups.Name = "LstGroups";
            this.LstGroups.Size = new System.Drawing.Size(202, 418);
            this.LstGroups.TabIndex = 0;
            this.LstGroups.SelectedIndexChanged += new System.EventHandler(this.LstGroups_SelectedIndexChanged);
            // 
            // NtfIcon
            // 
            this.NtfIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("NtfIcon.Icon")));
            this.NtfIcon.Text = "KQ";
            this.NtfIcon.Visible = true;
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(890, 478);
            this.Controls.Add(this.TacMainTabControl);
            this.Controls.Add(this.StsStatus);
            this.Controls.Add(this.TxtSendMsg);
            this.Controls.Add(this.RtbMessage);
            this.Controls.Add(this.BtnSend);
            this.Name = "FrmMain";
            this.Text = "Main";
            this.Load += new System.EventHandler(this.FrmMain_Load);
            this.StsStatus.ResumeLayout(false);
            this.StsStatus.PerformLayout();
            this.TacMainTabControl.ResumeLayout(false);
            this.TapMessages.ResumeLayout(false);
            this.TabGroupMsg.ResumeLayout(false);
            this.TapContacts.ResumeLayout(false);
            this.TapGroups.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button BtnSend;
        private System.Windows.Forms.RichTextBox RtbMessage;
        private System.Windows.Forms.TextBox TxtSendMsg;
        private System.Windows.Forms.StatusStrip StsStatus;
        private System.Windows.Forms.ToolStripStatusLabel TssCurrentQInfo;
        private System.Windows.Forms.TabControl TacMainTabControl;
        private System.Windows.Forms.TabPage TapMessages;
        private System.Windows.Forms.ListBox LstSessions;
        private System.Windows.Forms.TabPage TapContacts;
        private System.Windows.Forms.ListBox LstContacts;
        private System.Windows.Forms.TabPage TapGroups;
        private System.Windows.Forms.ListBox LstGroups;
        private System.Windows.Forms.TabPage TabGroupMsg;
        private System.Windows.Forms.ListBox LstGroupMsg;
        private System.Windows.Forms.NotifyIcon NtfIcon;
    }
}

