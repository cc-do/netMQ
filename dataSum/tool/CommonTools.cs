using dataSum.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dataSum.tool
{
    public class CommonTools
    {
        /// <summary>
        /// 将文件大小（字节）转成（KB、MB、GB）
        /// </summary>
        /// <param name="fileLength"></param>
        /// <returns></returns>
        public static string GetSizeStr(long fileLength)
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
        /// <summary>
        /// 返回json文件中的配置数据
        /// </summary>
        /// <returns></returns>
        public static ConfigModel GetJsonConfig()
        {
            string config_path = Environment.CurrentDirectory + @"\config.json";

            JsonMethods jsonMethods = new JsonMethods();
            jsonMethods.Encoding = Encoding.UTF8;
            ConfigModel configModel = jsonMethods.DeserializeObject<ConfigModel>(config_path, jsonMethods.Encoding);
            return configModel;
        }
        /// <summary>
        /// 判断时间是否在工作时段
        /// </summary>
        /// <returns></returns>
        public static bool WorkingDate()
        {
            string _strWorkingDayAM = "08:00";
            string _strWorkingDayPM = "08:20";

            TimeSpan dspWorkingDayAM;
            TimeSpan dspWorkingDayPM;

            dspWorkingDayAM = DateTime.Parse(_strWorkingDayAM).TimeOfDay;
            dspWorkingDayPM = DateTime.Parse(_strWorkingDayPM).TimeOfDay;

            TimeSpan dspNow = DateTime.Now.TimeOfDay;
            if (dspNow > dspWorkingDayAM && dspNow < dspWorkingDayPM)
            {
                return true;
            }
            return false;
        }
    }
}
