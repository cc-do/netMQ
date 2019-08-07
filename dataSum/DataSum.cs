using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dataSum
{
    public class DataSum : SumInter
    {
        public override void StatFunc(params string[] path)
        {
            foreach (string p in path)
            {
                try
                {
                    DateTime preDate = DateTime.Today.AddDays(-1);
                    addDate = DateTime.Now.ToString("yyyy-MM-dd");
                    updateDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    string ymd = preDate.ToString("yyyy.MM.dd");
                    string px = Alphaleonis.Win32.Filesystem.Path.Combine(p, string.Format("{0}数据.zip", ymd));
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
