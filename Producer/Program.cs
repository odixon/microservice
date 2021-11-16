using System;
using System.Text;
using System.Threading;
using RabbitMQ.Client;

namespace Producer
{
    class Program
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory() { HostName = "192.168.0.111", UserName="admin", Password="admin" };
            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.BasicQos(0, 1, false);
                    channel.ExchangeDeclare("myexchange", type: ExchangeType.Direct, durable: true);
                    channel.QueueDeclare("requests", durable: true, exclusive: false, autoDelete: false);
                    channel.QueueBind("requests", "myexchange", "key");
                    var properties = channel.CreateBasicProperties();
                    properties.Persistent = true;

                    for (int i = 0; i < 500; i++)
                    {
                        channel.BasicPublish(exchange: "myexchange",
                        routingKey: "key",
                        basicProperties: properties,
                        body: Encoding.UTF8.GetBytes($"aiden: {i}"));

                        Console.WriteLine("Message sent:" + i);
                        Thread.Sleep(new Random().Next(10, 30));
                    }
                }
            }
        }
    }
}
