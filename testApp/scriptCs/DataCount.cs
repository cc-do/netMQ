using System;

namespace testApp.scriptCs
{
    public class DataCount : StatInter
    {
        public override void StatFunc(params string[] path)
        {
            foreach (string p in path)
            {
                try
                {
                    DateTime preDate = DateTime.Today.AddDays(-1);
                    dateTime = preDate;
                    string ymd = preDate.ToString("yyyy.MM.dd");
                    string px = Alphaleonis.Win32.Filesystem.Path.Combine(p, string.Format("{0}数据", ymd));
                    if (Alphaleonis.Win32.Filesystem.File.Exists(px))
                    {
                        AddPath(px);
                    }
                }
                catch { }
            }
        }
    }
}

