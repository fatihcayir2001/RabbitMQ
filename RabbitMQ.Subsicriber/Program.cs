using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Core;
using System.Text;

var factory = new ConnectionFactory();
factory.Uri = new Uri(Constant.URI);

using (var connection = factory.CreateConnection())
{
    var channel = connection.CreateModel();

    var randomQueueName = "hello-queue";//channel.QueueDeclare().QueueName;

    var queue = channel.QueueDeclare(randomQueueName, true, false, false);

    channel.QueueBind(randomQueueName, Constant.EXCHANGE_NAME, "", null);

    channel.BasicQos(0, 1, false);//Herhangi bir void mesaji, mesajlar birer gelsin, global true ise tek bir seferde tüm subsicribirlera toplam x adet

    var consumer = new EventingBasicConsumer(channel);

    channel.BasicConsume(randomQueueName, false,  consumer); //true hemen sil false silme haberdar et, bu true kalinca birden fazla ayağa kalkdıysa gitmiyor ayrı ayrı

    Console.WriteLine("Loglar bekleniyor");

    consumer.Received += (object? sender, BasicDeliverEventArgs e) => {
        var message = Encoding.UTF8.GetString(e.Body.ToArray());

        Console.WriteLine("Mesaj : " + message);
        channel.BasicAck(e.DeliveryTag, false); //Kuyruktan sil

    };

    Console.Read();
}