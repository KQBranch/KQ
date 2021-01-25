using System.Collections.Generic;
using Mirai_CSharp.Models;

namespace KQ.Model
{
    public class HistoryMsgUnit : IBaseInfo
    {
        public HistoryMsgUnit(IBaseInfo info)
        {
            _info = info;
            Msg = new Queue<Model.MsgInfo>();
        }
        
        private readonly IBaseInfo _info;
        public Queue<Model.MsgInfo> Msg;

        public long Id
        {
            get => _info.Id;
        }

        public string Name
        {
            get => _info.Name;
        }
    }
}