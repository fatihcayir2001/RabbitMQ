using RabbitMQ.Client;
using RabbitMQ.Core;
using System.Collections.Generic;
using System.Text;

var factory = new ConnectionFactory();
factory.Uri = new Uri(Constant.URI);

using (var connection = factory.CreateConnection())
{
    var channel = connection.CreateModel();

    channel.ExchangeDeclare(Constant.EXCHANGE_NAME, durable: true, type: ExchangeType.Fanout);

    Enumerable.Range(1, 50).ToList().ForEach(x =>
    {
        string message = $"log {x}";
        var messageBody = Encoding.UTF8.GetBytes(message);

        channel.BasicPublish(Constant.EXCHANGE_NAME, "", null, messageBody);

        Console.WriteLine($"Mesaj gönderilmiştir: {message}");
    });

    

    
    Console.Read();
}