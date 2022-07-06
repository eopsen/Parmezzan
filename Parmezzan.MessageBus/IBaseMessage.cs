using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parmezzan.MessageBus
{
    internal interface IBaseMessage
    {
        Task PublishMessage(BaseMessage message, string topicName);
    }
}
