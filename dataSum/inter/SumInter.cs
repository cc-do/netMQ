using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dataSum
{
    public class SumInter
    {
        /// <summary>
        /// 数据统计时间
        /// </summary>
        public string addDate;
        /// <summary>
        /// 数据更新时间
        /// </summary>
        public string updateDate;
        /// <summary>
        /// 文件总大小
        /// </summary>
        public long totalBytes = 0;
        /// <summary>
        /// 文件列表
        /// </summary>
        public HashSet<string> dataPath = new HashSet<string>();

        public string SearchPattern { get; set; }

        public SumInter()
        {
            SearchPattern = "*";
            
        }

        private string GetFullPath(string path) => Alphaleonis.Win32.Filesystem.Path.GetFullPath(path);

        protected string Convert(string path) => GetFullPath(path).ToLower();

        protected bool Exists(string path)
        {
            foreach (string dp in dataPath)
            {
                if (Convert(dp) == Convert(path))
                {
                    return true;
                }
            }
            return false;
        }

        protected void AddPath(string path)
        {
            if (!Exists(path))
            {
                dataPath.Add(path);
                totalBytes += Alphaleonis.Win32.Filesystem.File.GetSize(path);
            }
        }

        public virtual void StatFunc(params string[] path)
        {
            foreach (string p in path)
            {
                try
                {
                    if (Alphaleonis.Win32.Filesystem.Directory.Exists(p))
                    {
                        foreach (string f in Alphaleonis.Win32.Filesystem.Directory.GetFiles(p, SearchPattern, System.IO.SearchOption.AllDirectories))
                        {
                            AddPath(f);
                        }
                    }
                    else if (Alphaleonis.Win32.Filesystem.File.Exists(p))
                    {
                        AddPath(p);
                    }
                }
                catch
                {

                }
            }
            
        }
    }
}
