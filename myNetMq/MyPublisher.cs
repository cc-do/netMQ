using NetMQ;
using NetMQ.Sockets;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace myNetMq
{
    public class MyPublisher : IMyPublisher
    {
        private object _lockObj = new object();
        private PublisherSocket _publisher = null;

        HashSet<string> dataTopics = new HashSet<string>();
        HashSet<string> strTopics = new HashSet<string>();
        
        private bool SendEndOfFlag { get; set; }

        public MyPublisher(string addrPoint)
        {
            SendEndOfFlag = true;
            _publisher = Create(addrPoint);
        }

        private PublisherSocket Create(string connectString)
        {
            PublisherSocket publisher = new PublisherSocket(connectString);
            publisher.Options.SendHighWatermark = 1000;
            publisher.Options.SendBuffer = 1024;
            //publisher.Bind(connectString);
            Thread.Sleep(500);
            return publisher;
        }

        public void Publish(string topicName, string data)
        {
            lock (_lockObj)
            {
                strTopics.Add(topicName);
                _publisher.SendMoreFrame(topicName).SendFrame(data);
            }
        }

        public void Publish(string topicName, byte[] data, int length = 0)
        {
            lock (_lockObj)
            {
                dataTopics.Add(topicName);
                if (length > 0)
                {
                    _publisher.SendMoreFrame(topicName).SendFrame(data, length);
                }
                else
                {
                    _publisher.SendMoreFrame(topicName).SendFrame(data);
                }

            }
        }

        public void Publish<T>(string topicName, T obj, int length = 0)
        {
            if (typeof(T) == typeof(byte[]))
            {
                Publish(topicName, obj as byte[], length);
            }
            else if(typeof(T) == typeof(string))
            {
                Publish(topicName, obj as string);
            }
            else
            {
                using (MemoryStream stream = new MemoryStream())
                {
                    new BinaryFormatter().Serialize(stream, obj);
                    Publish(topicName, stream.ToArray());
                }
            }
        }

        public void Dispose()
        {
            lock (_lockObj)
            {
                try
                {
                    if (SendEndOfFlag)
                    {
                        // 发送结束标志
                        foreach (string topic in strTopics)
                        {
                            _publisher.SendMoreFrame(topic).SendFrame(MyFuncOfFlag.EndOfFlag);
                        }
                        foreach (string topic in dataTopics)
                        {
                            _publisher.SendMoreFrame(topic).SendFrame(MyFuncOfFlag.EncodeFlag());
                        }
                    }
                    _publisher.Close();
                    _publisher.Dispose();
                    dataTopics.Clear();
                    strTopics.Clear();
                }
                catch { }
                
            }
        }
        
    }
}
