using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Core;
using System.Text;
using System.Text.Json;

var factory = new ConnectionFactory();
factory.Uri = new Uri(Constant.URI);

using (var connection = factory.CreateConnection())
{
    var channel = connection.CreateModel();

    var queueName = channel.QueueDeclare().QueueName;//channel.QueueDeclare().QueueName;

    Dictionary<string, object> headers = new Dictionary<string, object>();

    headers.Add("format", "pdf");
    headers.Add("shape", "a4");
    headers.Add("x-match", "any");

    channel.QueueBind(queueName, Constant.EXCHANGE_NAME_HEADER, string.Empty, headers);

    channel.BasicQos(0, 1, false);//Herhangi bir void mesaji, mesajlar birer gelsin, global true ise tek bir seferde tüm subsicribirlera toplam x adet

    var consumer = new EventingBasicConsumer(channel);

    channel.BasicConsume(queueName, false,  consumer); //true hemen sil false silme haberdar et, bu true kalinca birden fazla ayağa kalkdıysa gitmiyor ayrı ayrı

    Console.WriteLine("Loglar bekleniyor");

    consumer.Received += (object? sender, BasicDeliverEventArgs e) => {
        var message = Encoding.UTF8.GetString(e.Body.ToArray());

        var product = JsonSerializer.Deserialize<Product>(message);

        Console.WriteLine($"Stok sayısı {product.Stock}, Ürün Adı: {product.Name}, Ürün Fiyatı: {product.Price}");
        Thread.Sleep(1000);

        channel.BasicAck(e.DeliveryTag, false); //Kuyruktan sil

    };

    Console.Read();
}