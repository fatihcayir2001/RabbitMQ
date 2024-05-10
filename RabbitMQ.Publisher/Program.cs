using RabbitMQ.Client;
using RabbitMQ.Core;
using System.Collections.Generic;
using System.Text;

var factory = new ConnectionFactory();
factory.Uri = new Uri(Constant.URI);

using (var connection = factory.CreateConnection())
{
    var channel = connection.CreateModel();

    channel.ExchangeDeclare(Constant.EXCHANGE_NAME_TOPIC, durable: true, type: ExchangeType.Topic);


    //Enum.GetNames(typeof(LogNames)).ToList().ForEach(x =>
    //{
    //    var queueName = $"direct-queue-{x}";
    //    var routeKey = $"route-{x}";

    //    channel.QueueDeclare(queueName, true, false, false);
    //    channel.QueueBind(queueName, Constant.EXCHANGE_NAME_TOPIC, routeKey, null);
    //});

    Enumerable.Range(1, 50).ToList().ForEach(x =>
    {
        LogNames log1 = (LogNames)new Random().Next(1, 5);
        LogNames log2 = (LogNames)new Random().Next(1, 5);
        LogNames log3 = (LogNames)new Random().Next(1, 5);

        var routeKey = $"{log1}.{log2}.{log3}";

        string message = $"log-type: {log1}-{log2}-{log3} log {x}";
        var messageBody = Encoding.UTF8.GetBytes(message);


        channel.BasicPublish(Constant.EXCHANGE_NAME_TOPIC, routeKey, null, messageBody);

        Console.WriteLine($"Log gönderilmiştir: {message}");
    });

    

    
    Console.Read();
}