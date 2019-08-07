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
    public class MySubscriber : IMySubscriber
    {
        public event Action<string, object> Notify = delegate { };
        Dictionary<SubscriberSocket, DateTime> timeout = new Dictionary<SubscriberSocket, DateTime>();

        private string addrPoint = null;
        
        private int MillisecondsTimeout
        {
            get;
            set;
        }

        public MySubscriber(string endpoint)
        {
            addrPoint = endpoint;
            MillisecondsTimeout = -1;
        }

        private SubscriberSocket Create(string connectString)
        {
            SubscriberSocket subscriber = new SubscriberSocket(connectString);
            subscriber.Options.ReceiveHighWatermark = 1000;
            subscriber.Options.ReceiveBuffer = 1024;
            return subscriber;
        }

        private void Register(SubscriberSocket subscriber, List<string> topics = null)
        {
            if (topics == null)
            {
                subscriber.SubscribeToAnyTopic();
            }
            else
            {
                topics.ForEach(item => subscriber.Subscribe(item));
                timeout.Add(subscriber, DateTime.Now);
            }

        }

        private void Unregister(SubscriberSocket subscriber, List<string> topics = null)
        {
            if (topics == null)
            {
                subscriber.Close();
                subscriber.Dispose();
            }
            else
            {
                topics.ForEach(item => subscriber.Unsubscribe(item));
            }

            if (timeout.ContainsKey(subscriber))
            {
                timeout.Remove(subscriber);
            }
        }

        public void SubscriberData(List<string> topics = null)
        {
            SubscriberSocket subscriber = Create(addrPoint);
            Register(subscriber, topics);
            Task.Factory.StartNew(() =>
            {
                try
                {
                    while (true)
                    {
                        string messageTopicReceived = subscriber.ReceiveFrameString();
                        byte[] dataReceived = subscriber.ReceiveFrameBytes();
                        if (MyFuncOfFlag.IsEndOfFlag(dataReceived))
                        {
                            break;
                        }
                        Notify(messageTopicReceived, dataReceived);
                    }
                }
                catch { }
                finally
                {
                    Unregister(subscriber, topics);
                }

            });
        }

        public void SubscriberString(List<string> topics = null)
        {
            SubscriberSocket subscriber = Create(addrPoint);
            Register(subscriber, topics);
            Task.Factory.StartNew(() =>
            {
                try
                {
                    while (true)
                    {
                        string messageTopicReceived = subscriber.ReceiveFrameString();
                        string strReceived = subscriber.ReceiveFrameString();
                        if (MyFuncOfFlag.IsEndOfFlag(strReceived))
                        {
                            break;
                        }
                        Notify(messageTopicReceived, strReceived);
                    }
                }
                catch { }
                finally
                {
                    Unregister(subscriber, topics);
                }

            });
        }

        public void Subscriber<T>(List<string> topics = null)
        {
            if (typeof(T) == typeof(byte[]))
            {
                SubscriberData(topics);
            }
            else if (typeof(T) == typeof(string))
            {
                SubscriberString(topics);
            }
            else
            {
                SubscriberSocket subscriber = Create(addrPoint);
                Register(subscriber, topics);
                Task.Factory.StartNew(() =>
                {
                    try
                    {
                        while (true)
                        {
                            string messageTopicReceived = subscriber.ReceiveFrameString();
                            byte[] dataReceived = subscriber.ReceiveFrameBytes();
                            if (MyFuncOfFlag.IsEndOfFlag(dataReceived))
                            {
                                break;
                            }
                            using (MemoryStream stream = new MemoryStream())
                            {
                                stream.Write(dataReceived, 0, dataReceived.Length);
                                stream.Seek(0, SeekOrigin.Begin);
                                Notify(messageTopicReceived, (T)new BinaryFormatter().Deserialize(stream));
                            }

                        }
                    }
                    catch { }
                    finally
                    {
                        Unregister(subscriber, topics);
                    }

                });
            }
        }

        public void Dispose()
        {
            try
            {
                Join(MillisecondsTimeout);
            }
            catch
            {

            }
        }

        private void Join(int secondsTimeout = -1)
        {
            if (secondsTimeout < 0)
            {
                while (timeout.Count > 0)
                {
                    Thread.Sleep(500);
                }
            }
            else
            {
                while (timeout.Count > 0)
                {
                    SubscriberSocket[] ss = timeout.Keys.ToArray();
                    for (int i = ss.Length - 1; i >= 0; i--)
                    {
                        TimeSpan ts = DateTime.Now - timeout[ss[i]];
                        if ((int)ts.TotalMilliseconds - secondsTimeout >= 0)
                        {
                            ss[i].Close();
                            ss[i].Dispose();
                            timeout.Remove(ss[i]);
                        }
                    }
                    Thread.Sleep(500);
                }
            }
        }
    }
}
