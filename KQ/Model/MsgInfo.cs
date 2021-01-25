using System;
using Mirai_CSharp.Models;

namespace KQ.Model
{
    public class MsgInfo : IBaseInfo
    {
        private IBaseInfo _sender;

        public enum MsgType
        {
            Image,
            Text,
            Voice
        }

        public MsgInfo(DateTime dateTime, IBaseInfo sender, string msgData, MsgType type = MsgType.Text)
        {
            MsgData = msgData;
            DateTime = dateTime;
            _sender = sender;
            Type = type;
        }

        public string MsgData { get; set; }
        public DateTime DateTime { get; set; }

        public MsgType Type { get; set; }

        public long Id
        {
            get => _sender.Id;
        }

        public string Name
        {
            get => _sender.Name;
        }
    }
}