using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

var factory = new ConnectionFactory();
factory.Uri = new Uri("amqps://xsueigoy:WJGlOt03UN9fCe0omGRVECSxeaROIkDe@stingray.rmq.cloudamqp.com/xsueigoy");

using (var connection = factory.CreateConnection())
{
    var channel = connection.CreateModel();

    var queueName = "hello-queue";
    channel.QueueDeclare(queueName, true, false, false);

    var consumer = new EventingBasicConsumer(channel);

    channel.BasicConsume(queueName, true,  consumer);

    consumer.Received += Consumer_Received;

    Console.Read();
}

void Consumer_Received(object? sender, BasicDeliverEventArgs e)
{
    var message = Encoding.UTF8.GetString(e.Body.ToArray());

    Console.WriteLine("Mesaj : " + message);
}