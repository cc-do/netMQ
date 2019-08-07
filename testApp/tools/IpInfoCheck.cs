using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace testApp.tools
{
    public class IpInfoCheck
    {
        /// <summary>
        /// 获取本机IP
        /// </summary>
        /// <returns></returns>
        public static IPAddress[] LocalAddr => Dns.GetHostEntry(Dns.GetHostName()).AddressList;


    }
}
