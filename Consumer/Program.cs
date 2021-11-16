using System;
using System.Text;
using System.Threading;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Consumer
{
    class Program
    {
        private static readonly ManualResetEvent _resetEvent = new ManualResetEvent(false);
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory() { HostName = "192.168.0.111", UserName = "admin", Password = "admin" };
            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.BasicQos(0, 1, false);
                    channel.ExchangeDeclare("myexchange", type: ExchangeType.Direct, durable: true);
                    channel.QueueDeclare("requests", durable: true, exclusive: false, autoDelete: false);
                    channel.QueueBind("requests", "myexchange", "key");
                    Console.WriteLine("wait for messages...");
                    var consumer = new EventingBasicConsumer(channel);
                    consumer.Received += (sender, e) =>
                    {
                        var sleep = new Random().Next(100, 10000);
                        Console.WriteLine($"{Environment.MachineName} will begin process message... {DateTime.Now}, after {sleep} seconds");
                        Thread.Sleep(sleep);
                        var message = Encoding.UTF8.GetString(e.Body.ToArray());

                        Console.WriteLine($"{Environment.MachineName} Received message: {message}");
                        Console.WriteLine($"{Environment.MachineName} end process message!!! {DateTime.Now}");
                        channel.BasicAck(deliveryTag: e.DeliveryTag, multiple: false);
                    };
                    channel.BasicConsume(queue: "requests", autoAck: false, consumer: consumer);

                    Console.CancelKeyPress += Console_CancelKeyPress;
                    _resetEvent.WaitOne();
                }
            }

            
            Console.WriteLine("exited");
        }

        private static void Console_CancelKeyPress(object sender, ConsoleCancelEventArgs e)
        {
            e.Cancel = true;
            _resetEvent.Set();
        }
    }
}
