using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace testApp.scriptCs
{
    public class RunStatCs
    {
        private string className;

        public RunStatCs(string className)
        {
            this.className = className;
        }
        /// <summary>
        /// 用类型的命名空间和名称获得类型
        /// </summary>
        /// <param name="className"></param>
        /// <returns></returns>
        Type GetClassType(string className) => Type.GetType(className);
        /// <summary>
        /// 利用指定的参数实例化类型
        /// </summary>
        /// <param name="type"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        object CreateInstance(Type type, params object[] args) => Activator.CreateInstance(type, args);
        /// <summary>
        /// 通过方法名称获得方法
        /// </summary>
        /// <param name="type"></param>
        /// <param name="methodName"></param>
        /// <returns></returns>
        MethodInfo GetMethod(Type type, string methodName) => type.GetMethod(methodName);

        MethodInfo GetMethod(Type type, string methodName, Type[] types) => type.GetMethod(methodName, types);

        object RunMethod(MethodInfo mi, object obj, params object[] parameters)
        {
            return mi.Invoke(obj, parameters);
        }

        public void Run(ConfigParam configParam, params object[] parameters)
        {
            List<string> param = new List<string>();
            if (parameters != null)
            {
                foreach (string p in parameters)
                {
                    param.Add(p);
                }
            }
            Type type = GetClassType(className);
            object instance = CreateInstance(type);
            //dynamic instance = new DataCount();
            MethodInfo mi = GetMethod(type, "StatFunc");


            configParam.Result.BeginTime = DateTime.Now;
            RunMethod(mi, instance, new object[] { param.ToArray()});
            configParam.Result.FinishTime = DateTime.Now;

            FieldInfo[] fi =
            {
                instance.GetType().GetField("dataTime", BindingFlags.Instance | BindingFlags.NonPublic),
                instance.GetType().GetField("totalBytes", BindingFlags.Instance | BindingFlags.NonPublic),
                instance.GetType().GetField("dataPath", BindingFlags.Instance | BindingFlags.NonPublic)
            };

            configParam.Result.CountTime = (DateTime)fi[0].GetValue(instance);
            configParam.Result.TotalBytes = (long)fi[1].GetValue(instance);
            configParam.Result.DataPath = (HashSet<string>)fi[2].GetValue(instance);
        }
    }
}
