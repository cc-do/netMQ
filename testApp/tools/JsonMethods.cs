using Alphaleonis.Win32.Filesystem;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace testApp.tools
{
    public class JsonMethods
    {
        private string json;
        /// <summary>
        /// 全局文件编码，默认为UTF8
        /// </summary>
        public Encoding Encoding { get; set; }
        /// <summary>
        /// 注释字符串
        /// </summary>
        public string[] CommentStr { get; set; }

        public JsonMethods() => Init();

        public JsonMethods(string json) : this() => this.json = json;

        public JsonMethods(string path, Encoding encoding) : this() => this.json = GetJsonStr(path, encoding);

        private void Init()
        {
            CommentStr = new string[] { "#", "//" };
            Encoding = Encoding.UTF8;
        }
        /// <summary>
        /// 是否注解
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private bool IsComment(string str)
        {
            foreach (string cs in CommentStr)
            {
                if (str.TrimStart(new char[] { ' ', '\t'}).StartsWith(cs))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 移除json包中的注解
        /// </summary>
        /// <param name="jsonStr"></param>
        /// <returns></returns>
        private string RemoveComment(string jsonStr)
        {
            string r = jsonStr;
            if (CommentStr != null)
            {
                r = "";
                string[] sa = jsonStr.Split(new string[] { "\r\n", "\n"}, StringSplitOptions.None);
                foreach(string s in sa)
                {
                    if (!IsComment(s))
                    {
                        r += s + Environment.NewLine;
                    }
                }
            }
            return r;
        }
        /// <summary>
        /// 从文件中读取json数据
        /// </summary>
        /// <param name="path">文件路径</param>
        /// <param name="encoding">文件编码</param>
        /// <returns></returns>
        public string GetJsonStr(string path, Encoding encoding) => RemoveComment(File.ReadAllText(path, encoding));
        public string GetJsonStr(string path) => GetJsonStr(path, Encoding);
        /// <summary>
        /// 从字符串反序列化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="json">json字符串，可带注释</param>
        /// <returns></returns>
        public T DeserializeObject<T>(string json) => JsonConvert.DeserializeObject<T>(RemoveComment(json));
        /// <summary>
        /// 从文件反序列化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="path"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public T DeserializeObject<T>(string path, Encoding encoding) => DeserializeObject<T>(GetJsonStr(path, encoding));

        public T DeserializeObject<T>() => DeserializeObject<T>(this.json);
        /// <summary>
        /// 序列化
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string SerializeObject(object obj) => JsonConvert.SerializeObject(obj);

        public static T LoadConfig<T>(string path, Encoding encoding) => new JsonMethods() { Encoding = encoding }.DeserializeObject<T>(path, encoding);

        public static T LoadConfig<T>(string json) => new JsonMethods().DeserializeObject<T>(json);
        /// <summary>
        /// 用于单项的值设置
        /// </summary>
        /// <param name="json"></param>
        /// <param name="propertyName"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        public string Set(string json, string propertyName, JToken item)
        {
            JObject jObject = JObject.Parse(RemoveComment(json));
            if (!jObject.Properties().Any(p => p.Name == propertyName))
            {
                jObject.Add(propertyName, item);
            }
            else
            {
                jObject[propertyName] = item;
            }
            return jObject.ToString();
        }
        /// <summary>
        /// 用于数组项的值设置
        /// </summary>
        /// <param name="json"></param>
        /// <param name="propertyName"></param>
        /// <param name="index"></param>
        /// <param name="items"></param>
        /// <returns></returns>
        public string Insert(string json, string propertyName, int index = 0, params JToken[] items)
        {
            JObject jObject = JObject.Parse(RemoveComment(json));
            if (items != null && items.Length > 0)
            {
                if (!jObject.Properties().Any(p => p.Name == propertyName))
                {
                    jObject.Add(propertyName, new JArray());
                }
                foreach (JToken item in items)
                {
                    ((JArray)jObject[propertyName]).Insert(index, item);
                }
            }
            return jObject.ToString();
        }
    }
}
