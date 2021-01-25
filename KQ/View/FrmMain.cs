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
            catch
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

            await Task.Run(() => UpdateList(1000)).ConfigureAwait(false);
        }

        private void UpdateList(int ms = 1000)
        {
            int counter = 0;
            while (true)
            {
                Invoke(new Action(() =>
                {
                    TssCurrentQInfo.Text = $"ID: {session.QQNumber} | Connection: {session.Connected}";
                    LstSessions.Items.Clear();
                    foreach (var i in HistoryMsg.Friend)
                    {
                        this.LstSessions.Items.Add(new Model.BaseInfo(i.Value));
                    }
                }));

                if (counter == 0)
                {
                    LstContacts.Items.Clear();
                    var contact = session.GetFriendListAsync().Result;
                    foreach (var i in contact)
                    {
                        LstContacts.Items.Add(new Model.UnitInfo(i));
                    }

                    LstGroups.Items.Clear();
                    var group = session.GetGroupListAsync().Result;
                    foreach (var i in group)
                    {
                        LstGroups.Items.Add(new Model.UnitInfo(i));
                    }
                }

                if (counter == 5 * 60)
                {
                    counter = 0;
                }

                Thread.Sleep(ms);
                ++counter;
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
                                       $"{DateTime.Now:dd/MM/yyyy HH:mm:ss} {e.Sender.Name}({e.Sender.Id}):\r\n{msg}";
                }));
            }

            HistoryMsg.AddFriendMsg(
                e.Sender,
                msg,
                DateTime.Now);
            return false;
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
        }

        private void LstSessions_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (LstSessions.SelectedItem == null) return;
            currentSession = ((Model.BaseInfo) LstSessions.SelectedItem).Id;
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