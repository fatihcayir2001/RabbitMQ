using RabbitMQ.Client;
using RabbitMQ.Core;
using System.Text;

var factory = new ConnectionFactory();
factory.Uri = new Uri(Constant.URI);

using (var connection = factory.CreateConnection())
{
    var channel = connection.CreateModel();

    var queueName = Constant.QUEUE_NAME;
    channel.QueueDeclare(queueName, true, false, false);

    string message = "Hello World";
    var messageBody = Encoding.UTF8.GetBytes(message);

    channel.BasicPublish(string.Empty, queueName, null, messageBody);

    Console.WriteLine("Mesaj gönderilmiştir");
    Console.Read();
}