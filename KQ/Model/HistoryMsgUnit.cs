using System.Collections.Generic;
using Mirai_CSharp.Models;

namespace KQ.Model
{
    public class HistoryMsgUnit : IBaseInfo
    {
        public Queue<MsgInfo> Msg;

        public long Id { get; set; }
        public string Name { get; set; }

        public HistoryMsgUnit(IBaseInfo info)
        {
            Id = info.Id;
            Name = info.Name;
            Msg = new Queue<MsgInfo>();
        }

        public HistoryMsgUnit(long id, string name)
        {
            Id = id;
            Name = name;
            Msg = new Queue<MsgInfo>();
        }
    }
}