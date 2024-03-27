using RabbitMQ.Client;
using System.Text;

var factory = new ConnectionFactory();
factory.Uri = new Uri("amqps://xsueigoy:WJGlOt03UN9fCe0omGRVECSxeaROIkDe@stingray.rmq.cloudamqp.com/xsueigoy");

using (var connection = factory.CreateConnection())
{
    var channel = connection.CreateModel();

    var queueName = "hello-queue";
    channel.QueueDeclare(queueName, true, false, false);

    string message = "Hello World";
    var messageBody = Encoding.UTF8.GetBytes(message);

    channel.BasicPublish(string.Empty, queueName, null, messageBody);

    Console.WriteLine("Mesaj gönderilmiştir");
    Console.Read();
}