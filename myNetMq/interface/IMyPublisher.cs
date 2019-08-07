using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myNetMq
{
    public interface IMyPublisher : IDisposable
    {
        //void Publish(string topicName, string data);
        void Publish<T>(string topic, T obj, int length = 0);
    }
}
