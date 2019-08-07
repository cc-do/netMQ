using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace testApp
{
    [Serializable]
    public class ConfigParam
    {
        [JsonProperty(PropertyName = "identify")]
        public string Identify { get; private set; }

        [JsonProperty(PropertyName = "path")]
        public string[] Path { get; private set; }

        [JsonProperty(PropertyName = "code_path")]
        public string CodePath { get; private set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; private set; }

        [JsonProperty(PropertyName = "interval")]
        public int Interval { get; private set; }

        [JsonProperty(PropertyName = "address")]
        public string Address { get; private set; }

        Result result = new Result();
        [JsonProperty(PropertyName = "result")]
        public Result Result { get { return result; } set { result = value; } }

        Result preResult = new Result();
        [JsonProperty(PropertyName = "pre_result")]
        public Result PreResult { get { return preResult; } set { preResult = value; } }
        /// <summary>
        /// 如果当前数据大小有变化，则将当前数据保存到前一次结果中
        /// </summary>
        public void SaveResult()
        {
            if (Result.ExistData && Result.TotalBytes != PreResult.TotalBytes)
            {
                PreResult = Result.Clone();
            }
        }

    }
    [Serializable]
    public class Result
    {
        [JsonProperty(PropertyName = "total_bytes")]
        public long TotalBytes { get; set; }

        [JsonProperty(PropertyName = "count_time")]
        public DateTime? CountTime { get; set; }

        [JsonProperty(PropertyName = "path")]
        public HashSet<string> DataPath { get; set; }

        [JsonProperty(PropertyName = "begin_time")]
        public DateTime BeginTime { get; set; }

        [JsonProperty(PropertyName = "finish_time")]
        public DateTime FinishTime { get; set; }

        /// <summary>
        /// 复制数据
        /// </summary>
        /// <returns></returns>
        public Result Clone()
        {
            Result result = new Result();
            result.CountTime = CountTime;
            result.TotalBytes = TotalBytes;
            if (DataPath != null)
            {
                result.DataPath = new HashSet<string>();
                foreach (string path in DataPath)
                {
                    result.DataPath.Add(path);
                }
            }
            result.BeginTime = BeginTime;
            result.FinishTime = FinishTime;
            return result;
        }
        /// <summary>
        /// 文件数
        /// </summary>
        /// <returns></returns>
        public int GetFileCount()
        {
            return DataPath != null ? DataPath.Count : 0;
        }
        /// <summary>
        /// 根据有无文件来判断是否存在数据
        /// </summary>
        public bool ExistData
        {
            get
            {
                return GetFileCount() > 0;
            }
        }
        /// <summary>
        /// 将文件大小（字节）转成（KB、MB、GB）
        /// </summary>
        /// <param name="fileLength"></param>
        /// <returns></returns>
        public string GetSizeStr(long fileLength)
        {
            return fileLength >= 1024 * 1024 * 1024 ?
                string.Format("{0:N2}GB", fileLength / (1024.0 * 1024 * 1024)) :
                (
                    fileLength >= 1024 * 1024 ?
                    string.Format("{0:N2}MB", fileLength / (1024.0 * 1024)) :
                    (
                        fileLength > 1024 ?
                        string.Format("{0:N2}KB", fileLength / 1024.0) :
                        (
                            string.Format("{0}", fileLength)
                        )
                    )
                );
        }

        public override string ToString()
        {
            string str = "";
            str += string.Format("数据时间 : {0}",CountTime.HasValue?CountTime.Value.ToString("yyyy-MM-dd") : "<NULL>") + "\n";
            str += string.Format("数据大小 : {0} <{1}>",TotalBytes,GetSizeStr(TotalBytes)) + "\n";
            str += string.Format("文件总数 : {0}",GetFileCount()) + "\n";
            str += "文件列表 : [\n";
            int i = 1;
            foreach (string f in DataPath)
            {
                str += string.Format("\t\t{0,4}、 {1}",i++,f) + "\n";
            }
            str += "\t   ]\n";
            str += string.Format("开始时间 : {0}\n", BeginTime.ToString("yyyy-MM-dd HH:mm:ss"));
            str += string.Format("结束时间 : {0}\n", FinishTime.ToString("yyyy-MM-dd HH:mm:ss"));
            return str;
        }
    }

    [Serializable]
    public class ConfigList
    {
        /// <summary>
        /// 心跳包标志
        /// </summary>
        [JsonProperty(PropertyName = "heart_flag")]
        public string HeartFlag { get; private set; }

        /// <summary>
        /// 每种数据的配置
        /// </summary>
        [JsonProperty(PropertyName = "config")]
        public ConfigParam[] configs { get; private set; }
    }
}
