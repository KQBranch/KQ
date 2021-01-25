using System;
using System.Collections.Generic;
using KQ.Model;
using Mirai_CSharp.Models;

namespace KQ.Controller
{
    class HistoryMsg
    {
        public static Dictionary<long, HistoryMsgUnit> Group = new Dictionary<long, HistoryMsgUnit>();
        public static Dictionary<long, HistoryMsgUnit> Friend = new Dictionary<long, HistoryMsgUnit>();


        public static void AddGroupMsg(IBaseInfo groupInfo, string msg, IBaseInfo senderInfo, DateTime dateTime,
            MsgInfo.MsgType msgType = MsgInfo.MsgType.Text)
        {
            if (!Group.ContainsKey(groupInfo.Id))
            {
                Group.Add(groupInfo.Id, new HistoryMsgUnit(groupInfo));
            }

            Group[groupInfo.Id].Msg.Enqueue(new MsgInfo(dateTime, senderInfo, msg, msgType));
            Group[groupInfo.Id].Msg.ToLength(Config.Instance.HistoryLines);
        }

        public static void AddFriendMsg(IBaseInfo senderInfo, string msg, DateTime dateTime,
            MsgInfo.MsgType msgType = MsgInfo.MsgType.Text)
        {
            if (!Friend.ContainsKey(senderInfo.Id))
            {
                Friend.Add(senderInfo.Id, new HistoryMsgUnit(senderInfo));
            }

            Friend[senderInfo.Id].Msg.Enqueue(new MsgInfo(dateTime, senderInfo, msg, msgType));
            Friend[senderInfo.Id].Msg.ToLength(Config.Instance.HistoryLines);
        }

        public static string GetFriendHistoryMsg(long id)
        {
            if (!Friend.ContainsKey(id))
                return string.Empty;

            return Friend[id].Msg.ToRtbString();
        }

        public static string GetGroupHistoryMsg(long id)
        {
            if (!Group.ContainsKey(id))
                return string.Empty;

            return Group[id].Msg.ToRtbString();
        }
    }
}