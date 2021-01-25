using System;
using System.Collections.Generic;
using System.Text;
// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace KQ.Model
{
    public class Config
    {
        public long QQNumber { get; set; }
        public string Token { get; set; }
        public int Port { get; set; }
        public string Address { get; set; }
        public int HistoryLines { get; set; }
    }
}
