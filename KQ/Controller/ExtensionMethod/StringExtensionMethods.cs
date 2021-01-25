namespace KQ.Controller
{
    public static class StringExtensionMethods
    {
        public static string RemoveMirai(this string msg)
        {
            var index = msg.IndexOf("]");
            if (index < 0)
                return msg;
            return msg.Substring(index + 1);
        }
    }
}