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

#pragma warning disable 1998

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

            await Task.Run(() => UpdateList(500)).ConfigureAwait(false);
        }

        private async Task<bool> Session_FriendMessageEvt(MiraiHttpSession sender, IFriendMessageEventArgs e)
        {
            var msg = MsgParser.GetMsgString(e.Chain);
            var time = DateTime.Now;
            if (e.Sender.Id == currentSession && currentType == Model.Enums.SessionType.PrivateMsg)
            {
                this.Invoke(new Action(() =>
                {
                    RtbMessage.Text += "\r\n" +
                                       $"{time:dd/MM/yyyy HH:mm:ss} {e.Sender.Name} ({e.Sender.Id}):\r\n{msg}";
                }));
            }

            Invoke(new Action(() =>
            {
                NtfIcon.ShowBalloonTip(500, "New Message", $"From {e.Sender.Name} ({e.Sender.Id}\n{msg}",
                    ToolTipIcon.Info);

            }));

            HistoryMsg.Friend.AddMsg(e.Sender, msg, time, e.Sender);
            return false;
        }

        private async Task<bool> Session_GroupMessageEvt(MiraiHttpSession sender, IGroupMessageEventArgs e)
        {
            var msg = MsgParser.GetMsgString(e.Chain);
            var time = DateTime.Now;
            if (e.Sender.Group.Id == currentSession && currentType == Model.Enums.SessionType.GroupMsg)
            {
                this.Invoke(new Action(() =>
                {
                    RtbMessage.Text += "\r\n" +
                                       $"{time:dd/MM/yyyy HH:mm:ss} {e.Sender.Name} ({e.Sender.Id}):\r\n{msg}";
                }));
            }

            this.Invoke(new Action(() =>
            {
                NtfIcon.ShowBalloonTip(500, "New Message", $"From {e.Sender.Group.Name} ({e.Sender.Id}\n{msg}",
                    ToolTipIcon.Info);
            }));
            
            HistoryMsg.Group.AddMsg(e.Sender.Group, msg, time, e.Sender);
            return false;
        }

        private async Task<bool> Session_GroupNameChangedEvt(MiraiHttpSession sender, IGroupNameChangedEventArgs e)
        {
            if (HistoryMsg.Group.Dic.ContainsKey(e.Group.Id))
            {
                HistoryMsg.Group.Dic[e.Group.Id].Name = e.Group.Name;
            }

            return false;
        }

        private void UpdateListBoxItems(ref ListBox listBox, ref HistoryMsgBase msgBase)
        {
            listBox.Items.Clear();
            foreach (var i in msgBase.Dic)
            {
                listBox.Items.Add(new Model.BaseInfo(i.Value));
            }
        }

        private void UpdateListBoxItems(ref ListBox listBox, IEnumerable<IBaseInfo> infos)
        {
            listBox.Items.Clear();
            foreach (var i in infos)
            {
                listBox.Items.Add(new Model.BaseInfo(i));
            }
        }

        private void UpdateList(int ms = 1000)
        {
            int counter = 0;
            while (true)
            {
                TssCurrentQInfo.Text = $"ID: {session.QQNumber} | Connection: {session.Connected}";

                this.Invoke(new Action(() =>
                {
                    UpdateListBoxItems(ref LstSessions, ref HistoryMsg.Friend);
                    UpdateListBoxItems(ref LstGroupMsg, ref HistoryMsg.Group);
                }));

                if (counter == 0)
                {
                    var contact = session.GetFriendListAsync().Result;
                    this.Invoke(new Action(() =>
                    {
                        UpdateListBoxItems(ref LstContacts, contact);
                    }));

                    var group = session.GetGroupListAsync().Result;
                                        
                    this.Invoke(new Action(() =>
                    {
                        UpdateListBoxItems(ref LstGroups, group);
                    }));
                }

                if (counter == 5 * 60)
                {
                    counter = 0;
                }

                Thread.Sleep(ms);
                ++counter;
            }
        }


        private void FrmMain_Load(object sender, EventArgs e)
        {
        }

        private void LstSessions_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (LstSessions.SelectedItem == null) return;
            currentSession = ((Model.BaseInfo) LstSessions.SelectedItem).Id;
            currentType = Model.Enums.SessionType.PrivateMsg;
            RtbMessage.Text = "Change session to " + currentSession + "\r\n";
            RtbMessage.Text += HistoryMsg.Friend.GetMsg(currentSession);
        }

        private void LstGroupMsg_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (LstGroupMsg.SelectedItem == null) return;
            currentSession = ((Model.BaseInfo) LstGroupMsg.SelectedItem).Id;
            currentType = Model.Enums.SessionType.GroupMsg;
            RtbMessage.Text = "Change session to " + currentSession + "\r\n";
            RtbMessage.Text += HistoryMsg.Group.GetMsg(currentSession);
        }

        private void BtnSend_Click(object sender, EventArgs e)
        {
            if (currentSession == 0 || currentType == Model.Enums.SessionType.None) return;
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
                HistoryMsg.Friend.AddMsg(
                    new Model.BaseInfo(currentSession, null),
                    msg, time,
                    new Model.BaseInfo(Config.Instance.QQNumber, "Me")
                );
            }
            else
            {
                session.SendGroupMessageAsync(currentSession, new IMessageBase[]
                {
                    new PlainMessage($"{msg}")
                });
                HistoryMsg.Group.AddMsg(
                    new Model.BaseInfo(currentSession, null),
                    msg, time,
                    new Model.BaseInfo(Config.Instance.QQNumber, "Me")
                );
            }
        }

        private void RtbMessage_ContentsResized(object sender, ContentsResizedEventArgs e)
        {
            RtbMessage.SelectionStart = RtbMessage.Text.Length;
            RtbMessage.ScrollToCaret();
        }
    }
}