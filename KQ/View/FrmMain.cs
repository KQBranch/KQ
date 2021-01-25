using Mirai_CSharp;
using Mirai_CSharp.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using KQ.Controller;

// ReSharper disable InconsistentNaming
// ReSharper disable LocalizableElement

namespace KQ.View
{
    public partial class FrmMain : Form
    {
        long currentSession = 0;
        MiraiHttpSession session;

        public FrmMain()
        {
            if (!File.Exists("config.json"))
            {
                MessageBox.Show("Config file miss!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(1);
            }

            try
            {
                var configStr = File.ReadAllText("config.json");
                Config.Instance = JsonSerializer.Deserialize<Model.Config>(configStr);
                configStr = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Config is not valid!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(1);
            }

            InitializeComponent();
#pragma warning disable 4014
            InitializeMirai();
#pragma warning restore 4014
        }

        private async Task InitializeMirai()
        {
            MiraiHttpSessionOptions options = new MiraiHttpSessionOptions(
                Config.Instance.Address,
                Config.Instance.Port,
                Config.Instance.Token);

            session = new MiraiHttpSession();
            await session.ConnectAsync(options, Config.Instance.QQNumber);
            session.FriendMessageEvt += Session_FriendMessageEvt;

            await Task.Run(() => UpdateMsgList(1000)).ConfigureAwait(false);
        }

        private void UpdateMsgList(int ms = 1000)
        {
            while (true)
            {
                this.Invoke(new Action(() =>
                {
                    LstSessions.Items.Clear();
                    foreach (var i in HistoryMsg.Friend)
                    {
                        this.LstSessions.Items.Add(new QQContact(i.Key));
                    }
                }));
                Thread.Sleep(ms);
            }
        }


        private async Task<bool> Session_FriendMessageEvt(MiraiHttpSession sender, IFriendMessageEventArgs e)
        {
            var msg = (string.Join(null, (IEnumerable<IMessageBase>) e.Chain)).RemoveMirai();

            if (e.Sender.Id == currentSession)
            {
                this.Invoke(new Action(() =>
                {
                    RtbMessage.Text += "\r\n" +
                                       $"{DateTime.Now:dd/MM/yyyy HH:mm:ss} {e.Sender.Name}:\r\n{msg}";
                }));
            }

            HistoryMsg.AddFriendMsg(e.Sender.Id,
                msg,
                DateTime.Now);
            return false;
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
        }

        class QQContact
        {
            private IFriendInfo friendInfo;
            private long qId = 0;

            public string GetName()
            {
                if (friendInfo == null) return "";
                return friendInfo.Name;
            }

            public long GetQID()
            {
                if (qId != 0)
                    return qId;
                return friendInfo.Id;
            }

            public override string ToString()
            {
                return $"{GetName()}({GetQID()})";
            }

            public QQContact(IFriendInfo ifi)
            {
                friendInfo = ifi;
            }

            public QQContact(long qid)
            {
                qId = qid;
            }
        }

        private void LstSessions_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (LstSessions.SelectedItem == null) return;
            currentSession = ((QQContact) LstSessions.SelectedItem).GetQID();
            RtbMessage.Text = "Change session to " + currentSession + "\r\n";
            RtbMessage.Text += HistoryMsg.GetFriendHistoryMsg(currentSession);
        }

        private void BtnSend_Click(object sender, EventArgs e)
        {
            if (currentSession != 0)
            {
                session.SendFriendMessageAsync(currentSession, new IMessageBase[]
                {
                    new PlainMessage($"{TxtSendMsg.Text}")
                });
                RtbMessage.Text += $"\r\n{DateTime.Now:dd/MM/yyyy HH:mm:ss} You:\r\n{TxtSendMsg.Text}";
                TxtSendMsg.Text = "";
            }
        }
    }
}