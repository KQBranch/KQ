using System.Collections.Generic;
using System.Text;
using KQ.Controller;

namespace KQ.Controller
{
    static class HistoryMsgExtensionMethods
    {
        public static void ToLength(this Queue<HistoryMsg.MsgInfo> q, int length)
        {
            while (q.Count > length)
            {
                q.Dequeue();
            }
        }

        public static string ToRtbString(this Queue<HistoryMsg.MsgInfo> q)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var t in q)
            {
                sb.Append(t.DateTime.ToString("dd/MM/yyyy HH:mm:ss"));
                sb.Append($" {t.Sender}:\r\n");
                sb.Append(t.MsgData);
                sb.Append("\r\n");
            }

            return sb.ToString().Trim();
        }

    }
}