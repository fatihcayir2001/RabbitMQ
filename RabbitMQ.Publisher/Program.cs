using RabbitMQ.Client;
using RabbitMQ.Core;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

var factory = new ConnectionFactory();
factory.Uri = new Uri(Constant.URI);

using (var connection = factory.CreateConnection())
{
    var channel = connection.CreateModel();

    channel.ExchangeDeclare(Constant.EXCHANGE_NAME_HEADER, durable: true, type: ExchangeType.Headers);

    Dictionary<string, object> headers = new Dictionary<string, object>();

    headers.Add("format", "pdf");
    headers.Add("shape2", "a4");

    var product = new Product()
    {
        Id = 1,
        Name = "KITAP",
        Price = 100,
        Stock = 100
    };

    var productStrings = JsonSerializer.Serialize(product);

    var propertires = channel.CreateBasicProperties();
    propertires.Headers = headers;
    propertires.Persistent = true; //Mesajlar kalıcı olsun

    channel.BasicPublish(Constant.EXCHANGE_NAME_HEADER, string.Empty, propertires, Encoding.UTF8.GetBytes(productStrings));

    Console.WriteLine("Mesaj gönderildi");

    Console.Read();
}