
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
            this.BtnSend = new System.Windows.Forms.Button();
            this.RtbMessage = new System.Windows.Forms.RichTextBox();
            this.TxtSendMsg = new System.Windows.Forms.TextBox();
            this.LstSessions = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // BtnSend
            // 
            this.BtnSend.Location = new System.Drawing.Point(774, 409);
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
            this.RtbMessage.Location = new System.Drawing.Point(242, 12);
            this.RtbMessage.Name = "RtbMessage";
            this.RtbMessage.Size = new System.Drawing.Size(627, 312);
            this.RtbMessage.TabIndex = 1;
            this.RtbMessage.Text = "";
            // 
            // TxtSendMsg
            // 
            this.TxtSendMsg.Location = new System.Drawing.Point(242, 330);
            this.TxtSendMsg.Multiline = true;
            this.TxtSendMsg.Name = "TxtSendMsg";
            this.TxtSendMsg.Size = new System.Drawing.Size(626, 66);
            this.TxtSendMsg.TabIndex = 3;
            // 
            // LstSessions
            // 
            this.LstSessions.FormattingEnabled = true;
            this.LstSessions.ItemHeight = 20;
            this.LstSessions.Location = new System.Drawing.Point(5, 12);
            this.LstSessions.Name = "LstSessions";
            this.LstSessions.Size = new System.Drawing.Size(231, 384);
            this.LstSessions.TabIndex = 4;
            this.LstSessions.SelectedIndexChanged += new System.EventHandler(this.LstSessions_SelectedIndexChanged);
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(878, 450);
            this.Controls.Add(this.LstSessions);
            this.Controls.Add(this.TxtSendMsg);
            this.Controls.Add(this.RtbMessage);
            this.Controls.Add(this.BtnSend);
            this.Name = "FrmMain";
            this.Text = "Main";
            this.Load += new System.EventHandler(this.FrmMain_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button BtnSend;
        private System.Windows.Forms.RichTextBox RtbMessage;
        private System.Windows.Forms.TextBox TxtSendMsg;
        private System.Windows.Forms.ListBox LstSessions;
    }
}

