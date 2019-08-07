using System;
using System.Net;
using System.Text;
using testApp.scriptCs;
using testApp.tools;

namespace testApp
{
    class Program
    {
        static void Main(string[] args)
        {
            GetDataCount();

        }

        static void DisplayLocalIP()
        {
            IPAddress[] addresses = IpInfoCheck.LocalAddr;
            Console.WriteLine("本机IP为：");
            foreach (IPAddress ip in addresses)
            {
                Console.WriteLine("\t{0}", ip);
            }
            Console.ReadLine();
        }

        static ConfigList GetJsonConfig()
        {
            //ConfigParam[] configParam = null;
            //string heartFlag = null;
            string config_path = @"D:\data\code\vspro\Dll\netMQ\testApp\config.json";

            JsonMethods jsonMethods = new JsonMethods();
            jsonMethods.Encoding = Encoding.UTF8;
            ConfigList configList = jsonMethods.DeserializeObject<ConfigList>(config_path, jsonMethods.Encoding);
            return configList;
            //configParam = configList.configs;
            //heartFlag = configList.HeartFlag;
            //Console.WriteLine(heartFlag);
            //foreach (ConfigParam con in configParam)
            //{
            //    string identify = con.Identify;
            //    Console.WriteLine(identify);
            //}

            //Console.ReadLine();
        }

        static void GetDataCount()
        {
            //string code_path = @"D:\data\code\vspro\Dll\netMQ\testApp\scriptCs\DataCount.cs";
            ConfigParam[] configParam = GetJsonConfig().configs;
            //dynamic script = CSScript.Evaluator.LoadFile(code_path);
            new RunStatCs(configParam[0].Name).Run(configParam[0], configParam[0].Path);

            Console.WriteLine(configParam[0]);
            Console.ReadLine();
        }
    }
}
