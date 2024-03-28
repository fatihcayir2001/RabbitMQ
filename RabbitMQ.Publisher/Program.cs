using RabbitMQ.Client;
using RabbitMQ.Core;
using System.Collections.Generic;
using System.Text;

var factory = new ConnectionFactory();
factory.Uri = new Uri(Constant.URI);

using (var connection = factory.CreateConnection())
{
    var channel = connection.CreateModel();

    var queueName = Constant.QUEUE_NAME;
    channel.QueueDeclare(queueName, true, false, false);

    Enumerable.Range(1, 2).ToList().ForEach(x =>
    {
        string message = $"Mesaj {x}";
        var messageBody = Encoding.UTF8.GetBytes(message);

        channel.BasicPublish(string.Empty, queueName, null, messageBody);

        Console.WriteLine($"Mesaj gönderilmiştir: {message}");
    });

    

    
    Console.Read();
}