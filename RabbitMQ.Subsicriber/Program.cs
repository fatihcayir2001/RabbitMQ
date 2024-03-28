using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Core;
using System.Text;

var factory = new ConnectionFactory();
factory.Uri = new Uri(Constant.URI);

using (var connection = factory.CreateConnection())
{
    var channel = connection.CreateModel();

    var queueName = Constant.QUEUE_NAME;
    var queue = channel.QueueDeclare(queueName, true, false, false);
    channel.BasicQos(0, 1, false);//Herhangi bir void mesaji, mesajlar birer gelsin, global true ise tek bir seferde tüm subsicribirlera toplam x adet

    var consumer = new EventingBasicConsumer(channel);

    channel.BasicConsume(queueName, false,  consumer); //true hemen sil false silme haberdar et, bu true kalinca birden fazla ayağa kalkdıysa gitmiyor ayrı ayrı

    consumer.Received += (object? sender, BasicDeliverEventArgs e) => {
        var message = Encoding.UTF8.GetString(e.Body.ToArray());

        Console.WriteLine("Mesaj : " + message);
        channel.BasicAck(e.DeliveryTag, false); //Kuyruktan sil

    };

    Console.Read();
}