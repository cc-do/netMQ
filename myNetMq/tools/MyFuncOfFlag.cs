using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace myNetMq
{
    public class MyFuncOfFlag
    {
        const string endOfFlag = "FINISH";

        public static string EndOfFlag
        {
            get
            {
                return endOfFlag;
            }
        }

        public static byte[] EncodeFlag()
        {
            return Encoding.UTF8.GetBytes(EndOfFlag);
        }

        public static string DecodeFlag(byte[] data)
        {
            return Encoding.UTF8.GetString(data);
        }

        public static bool IsEndOfFlag(string msg)
        {
            return msg == EndOfFlag;
        }

        public static bool IsEndOfFlag(byte[] data)
        {
            if (data.Length != EndOfFlag.Length)
            {
                return false;
            }
            return IsEndOfFlag(DecodeFlag(data));
        }
    }
}
