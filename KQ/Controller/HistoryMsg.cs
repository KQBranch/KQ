using System;
using System.Collections.Generic;

namespace KQ.Controller
{
    class HistoryMsg
    {
        public static Dictionary<long, Queue<MsgInfo>> Group = new Dictionary<long, Queue<MsgInfo>>();
        public static Dictionary<long, Queue<MsgInfo>> Friend = new Dictionary<long, Queue<MsgInfo>>();

        public class MsgInfo
        {
            public enum MsgType
            {
                Image,
                Text,
                Voice
            }

            private readonly string msgData;
            private readonly long sender;
            private readonly DateTime dateTime;
            private readonly MsgType type;

            public MsgInfo(DateTime dateTime, long sender, string msgData, MsgType type = MsgType.Text)
            {
                this.msgData = msgData;
                this.dateTime = dateTime;
                this.sender = sender;
                this.type = type;
            }

            public string MsgData
            {
                get => msgData;
            }

            public long Sender
            {
                get => sender;
            }

            public DateTime DateTime
            {
                get => dateTime;
            }

            public MsgType Type
            {
                get => type;
            }
        }

        public static void AddGroupMsg(long id, string msg, long sender, DateTime dateTime,
            MsgInfo.MsgType msgType = MsgInfo.MsgType.Text)
        {
            if (!Group.ContainsKey(id))
            {
                Group.Add(id, new Queue<MsgInfo>());
            }

            Group[id].Enqueue(new MsgInfo(dateTime, id, msg, msgType));
            Group[id].ToLength(Config.Instance.HistoryLines);
        }

        public static void AddFriendMsg(long id, string msg, DateTime dateTime,
            MsgInfo.MsgType msgType = MsgInfo.MsgType.Text)
        {
            if (!Friend.ContainsKey(id))
            {
                Friend.Add(id, new Queue<MsgInfo>());
            }

            Friend[id].Enqueue(new MsgInfo(dateTime, id, msg, msgType));
            Group[id].ToLength(Config.Instance.HistoryLines);
        }

        public static string GetFriendHistoryMsg(long id)
        {
            if (!Friend.ContainsKey(id))
                return string.Empty;

            return Friend[id].ToRtbString();
        }

        public static string GetGroupHistoryMsg(long id)
        {
            if (!Group.ContainsKey(id))
                return string.Empty;

            return Group[id].ToRtbString();
        }
    }
}