using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myNetMq
{
    public interface IMySubscriber : IDisposable
    {
        event Action<string, object> Notify;

        //void RegisterSubscriber(List<string> topics);

        //void RegisterSubscriberAll();

        //void RemoveSubscriberAll();
        void SubscriberData(List<string> topics);
        void SubscriberString(List<string> topics);
        void Subscriber<T>(List<string> topics);
    }
}
