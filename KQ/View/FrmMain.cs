using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

using Mirai_CSharp;
using Mirai_CSharp.Models;

using KQ.Controller;

// ReSharper disable InconsistentNaming
// ReSharper disable LocalizableElement

namespace KQ.View
{
    public partial class FrmMain : Form
    {
        long currentSession = 0;
        Model.Enums.SessionType currentType = Model.Enums.SessionType.None;
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
            TssCurrentQInfo.Text = $"ID: {Config.Instance.QQNumber} | Connection: False";
            MiraiHttpSessionOptions options = new MiraiHttpSessionOptions(
                Config.Instance.Address,
                Config.Instance.Port,
                Config.Instance.Token);

            session = new MiraiHttpSession();
            await session.ConnectAsync(options, Config.Instance.QQNumber);

            session.FriendMessageEvt += Session_FriendMessageEvt;
            session.GroupNameChangedEvt += Session_GroupNameChangedEvt;
            session.GroupMessageEvt += Session_GroupMessageEvt;

            TssCurrentQInfo.Text = $"ID: {session.QQNumber} | Connection: {session.Connected}";

            await Task.Run(() => UpdateList(1000)).ConfigureAwait(false);
        }

        private async Task<bool> Session_GroupMessageEvt(MiraiHttpSession sender, IGroupMessageEventArgs e)
        {
            var msg = (string.Join(null, (IEnumerable<IMessageBase>)e.Chain)).RemoveMirai();

            if (e.Sender.Group.Id == currentSession && currentType == Model.Enums.SessionType.GroupMsg)
            {
                this.Invoke(new Action(() =>
                {
                    RtbMessage.Text += "\r\n" +
                                       $"{DateTime.Now:dd/MM/yyyy HH:mm:ss} {e.Sender.Name} ({e.Sender.Id}):\r\n{msg}";
                }));
            }

            HistoryMsg.AddGroupMsg(
                e.Sender.Group,
                msg,
                DateTime.Now,
                e.Sender);
            return false;
        }

        private async Task<bool> Session_GroupNameChangedEvt(MiraiHttpSession sender, IGroupNameChangedEventArgs e)
        {
            if (HistoryMsg.Group.ContainsKey(e.Group.Id))
            {
                HistoryMsg.Group[e.Group.Id].Name = e.Group.Name;
            }
            return false;
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
                    LstGroupMsg.Items.Clear();
                    foreach (var i in HistoryMsg.Group)
                    {
                        this.LstGroupMsg.Items.Add(new Model.BaseInfo(i.Value));
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

#pragma warning disable 1998
        private async Task<bool> Session_FriendMessageEvt(MiraiHttpSession sender, IFriendMessageEventArgs e)
#pragma warning restore 1998
        {
            var msg = (string.Join(null, (IEnumerable<IMessageBase>)e.Chain)).RemoveMirai();

            if (e.Sender.Id == currentSession && currentType == Model.Enums.SessionType.PrivateMsg)
            {
                this.Invoke(new Action(() =>
                {
                    RtbMessage.Text += "\r\n" +
                                       $"{DateTime.Now:dd/MM/yyyy HH:mm:ss} {e.Sender.Name} ({e.Sender.Id}):\r\n{msg}";
                }));
            }

            HistoryMsg.AddFriendMsg(
                e.Sender,
                msg,
                DateTime.Now,
                e.Sender);
            return false;
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
        }

        private void LstSessions_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (LstSessions.SelectedItem == null) return;
            currentSession = ((Model.BaseInfo)LstSessions.SelectedItem).Id;
            currentType = Model.Enums.SessionType.PrivateMsg;
            RtbMessage.Text = "Change session to " + currentSession + "\r\n";
            RtbMessage.Text += HistoryMsg.GetFriendHistoryMsg(currentSession);
        }

        private void LstGroupMsg_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (LstGroupMsg.SelectedItem == null) return;
            currentSession = ((Model.BaseInfo)LstGroupMsg.SelectedItem).Id;
            currentType = Model.Enums.SessionType.GroupMsg;
            RtbMessage.Text = "Change session to " + currentSession + "\r\n";
            RtbMessage.Text += HistoryMsg.GetGroupHistoryMsg(currentSession);
        }

        private void BtnSend_Click(object sender, EventArgs e)
        {
            if (currentSession != 0 && currentType != Model.Enums.SessionType.None)
            {
                var time = DateTime.Now;
                var msg = TxtSendMsg.Text;

                RtbMessage.Text += $"\r\n{time:dd/MM/yyyy HH:mm:ss} Me ({Config.Instance.QQNumber}):\r\n{msg}";
                TxtSendMsg.Text = "";
                if (currentType == Model.Enums.SessionType.PrivateMsg)
                {
                    session.SendFriendMessageAsync(currentSession, new IMessageBase[]
                    {
                    new PlainMessage($"{msg}")
                    });
                    HistoryMsg.AddFriendMsg(
                        new Model.BaseInfo(currentSession, null),
                        msg,
                        time,
                        new Model.BaseInfo(Config.Instance.QQNumber, "Me")
                        );
                }
                else
                {
                    session.SendGroupMessageAsync(currentSession, new IMessageBase[]
                    {
                    new PlainMessage($"{msg}")
                    });
                    HistoryMsg.AddGroupMsg(
                        new Model.BaseInfo(currentSession, null),
                        msg,
                        time,
                        new Model.BaseInfo(Config.Instance.QQNumber, "Me")
                        );
                }
            }
        }

    }
}