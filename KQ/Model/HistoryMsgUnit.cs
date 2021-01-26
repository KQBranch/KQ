using System.Collections.Generic;
using Mirai_CSharp.Models;

namespace KQ.Model
{
    public class HistoryMsgUnit : IBaseInfo
    {
        public Queue<Model.MsgInfo> Msg;

        public long Id { get; set; }
        public string Name { get; set; }

        public HistoryMsgUnit(IBaseInfo info)
        {
            Id = info.Id;
            Name = info.Name;
            Msg = new Queue<Model.MsgInfo>();
        }

        public HistoryMsgUnit(long id, string name)
        {
            Id = id;
            Name = name;
            Msg = new Queue<Model.MsgInfo>();
        }
    }
}