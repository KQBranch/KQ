using System.Collections.Generic;
using System.Text;

namespace KQ.Controller
{
    static class HistoryMsgExtensionMethods
    {
        public static void ToLength(this Queue<Model.MsgInfo> q, int length)
        {
            while (q.Count > length)
            {
                q.Dequeue();
            }
        }

        public static string ToRtbString(this Queue<Model.MsgInfo> q)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var t in q)
            {
                sb.Append(t.DateTime.ToString("dd/MM/yyyy HH:mm:ss"));
                sb.Append($" {t.Name} ({t.Id}):\r\n");
                sb.Append(t.MsgData);
                sb.Append("\r\n");
            }

            return sb.ToString().Trim();
        }

    }
}