using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQ.Core
{
    public class Constant
    {
        public const string QUEUE_NAME = "hello-queue";
        public const string URI = "amqps://xsueigoy:WJGlOt03UN9fCe0omGRVECSxeaROIkDe@stingray.rmq.cloudamqp.com/xsueigoy";
        public const string EXCHANGE_NAME = "logs-fanout";
        public const string EXCHANGE_NAME_DIRECT = "logs-direct";
        public const string EXCHANGE_NAME_TOPIC = "logs-topic";
        public const string EXCHANGE_NAME_HEADER = "header-exhange";
    }
}
