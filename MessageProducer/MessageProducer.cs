using System;
using System.Security.Cryptography;
using System.Text;
using System.Timers;
using RabbitMQ.Client;

namespace MessageProducer
{
    class Sender
    {
        private static readonly RNGCryptoServiceProvider Rand = new RNGCryptoServiceProvider();

        public static void Main()
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            var connection = factory.CreateConnection();
            var channel = connection.CreateModel();

            channel.QueueDeclare(queue: "MessageQueue",
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null);


            var timer = new Timer { Interval = 1000 };
            timer.Elapsed += (sender, elapsedEventArgs) =>
            {
                var randomNumber = RandomInteger(-2, 16).ToString();
                var body = Encoding.UTF8.GetBytes(randomNumber);

                channel.BasicPublish(exchange: "",
                    routingKey: "MessageQueue",
                    basicProperties: null,
                    body: body);

                Console.WriteLine("MessageProducer pushed [{0}] to queue.", randomNumber);
            };

            timer.Start();

            Console.WriteLine(" Press [enter] to exit.");
            Console.ReadLine();
        }

        private static int RandomInteger(int min, int max)
        {
            var scale = uint.MaxValue;
            while (scale == uint.MaxValue)
            {
                var fourBytes = new byte[4];
                Rand.GetBytes(fourBytes);
                scale = BitConverter.ToUInt32(fourBytes, 0);
            }

            return (int)(min + (max - min) * (scale / (double)uint.MaxValue));
        }
    }
}
