using System.Collections.Generic;
using Mirai_CSharp.Models;

namespace KQ.Controller
{
    public class MsgParser
    {
        public static string GetMsgString(IMessageBase[] imsg)
        {
            // Index 0 is mirai info
            return string.Join(null, (IEnumerable<IMessageBase>) imsg[1..]);
        }
    }
}