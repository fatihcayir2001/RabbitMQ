using RabbitMQ.Client;
using RabbitMQ.Core;
using System.Collections.Generic;
using System.Text;

var factory = new ConnectionFactory();
factory.Uri = new Uri(Constant.URI);

using (var connection = factory.CreateConnection())
{
    var channel = connection.CreateModel();

    channel.ExchangeDeclare(Constant.EXCHANGE_NAME_DIRECT, durable: true, type: ExchangeType.Direct);


    Enum.GetNames(typeof(LogNames)).ToList().ForEach(x =>
    {
        var queueName = $"direct-queue-{x}";
        var routeKey = $"route-{x}";

        channel.QueueDeclare(queueName, true, false, false);
        channel.QueueBind(queueName, Constant.EXCHANGE_NAME_DIRECT, routeKey, null);
    });

    Enumerable.Range(1, 50).ToList().ForEach(x =>
    {
        LogNames log = (LogNames)new Random().Next(1, 4);

        var routeKey = $"route-{log}";

        string message = $"log-type: {log} log {x}";
        var messageBody = Encoding.UTF8.GetBytes(message);


        channel.BasicPublish(Constant.EXCHANGE_NAME_DIRECT, routeKey, null, messageBody);

        Console.WriteLine($"Log gönderilmiştir: {message}");
    });

    

    
    Console.Read();
}