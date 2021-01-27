using System;
using System.Collections.Generic;
using KQ.Controller.ExtensionMethods;
using KQ.Model;
using Mirai_CSharp.Models;

namespace KQ.Controller
{
    public class HistoryMsgBase
    {
        public Dictionary<long, HistoryMsgUnit> Dic = new Dictionary<long, HistoryMsgUnit>();

        public Dictionary<long, HistoryMsgUnit> GetDic() => Dic;
        public void AddMsg(IBaseInfo groupInfo, string msg, DateTime dateTime, IBaseInfo senderInfo,
            MsgInfo.MsgType msgType = MsgInfo.MsgType.Text)
        {
            if (!Dic.ContainsKey(groupInfo.Id))
            {
                Dic.Add(groupInfo.Id, new HistoryMsgUnit(groupInfo));
            }

            Dic[groupInfo.Id].Msg.Enqueue(new MsgInfo(dateTime, senderInfo, msg, msgType));
            Dic[groupInfo.Id].Msg.ToLength(Config.Instance.HistoryLines);
        }

        public string GetMsg(long id)
        {
            if (!Dic.ContainsKey(id))
                return string.Empty;

            return Dic[id].Msg.ToRtbString();
        }
    }
}