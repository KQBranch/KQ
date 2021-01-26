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

        private static void AddMsg(ref Dictionary<long, HistoryMsgUnit> target, IBaseInfo groupInfo,
            string msg, DateTime dateTime, IBaseInfo senderInfo,
            MsgInfo.MsgType msgType = MsgInfo.MsgType.Text)
        {
            if (!target.ContainsKey(groupInfo.Id))
            {
                target.Add(groupInfo.Id, new HistoryMsgUnit(groupInfo));
            }

            target[groupInfo.Id].Msg.Enqueue(new MsgInfo(dateTime, senderInfo, msg, msgType));
            target[groupInfo.Id].Msg.ToLength(Config.Instance.HistoryLines);
        }

        public static void AddGroupMsg(IBaseInfo groupInfo, string msg, DateTime dateTime, IBaseInfo senderInfo,
            MsgInfo.MsgType msgType = MsgInfo.MsgType.Text)
            => AddMsg(ref Group, groupInfo, msg, dateTime, senderInfo, msgType);

        public static void AddFriendMsg(IBaseInfo groupInfo, string msg, DateTime dateTime, IBaseInfo senderInfo,
            MsgInfo.MsgType msgType = MsgInfo.MsgType.Text)
            => AddMsg(ref Friend, groupInfo, msg, dateTime, senderInfo, msgType);

        private static string GetMsg(ref Dictionary<long, HistoryMsgUnit> target, long id)
        {
            if (!target.ContainsKey(id))
                return string.Empty;

            return target[id].Msg.ToRtbString();
        }

        public static string GetFriendHistoryMsg(long id)
            => GetMsg(ref Friend, id);

        public static string GetGroupHistoryMsg(long id)
            => GetMsg(ref Group, id);
    }
}