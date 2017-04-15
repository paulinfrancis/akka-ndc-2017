using System.Text;
using ActorModel.Messages;
using Akka.Actor;
using Akka.Event;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace ActorModel.Actors
{
    public class QueueReaderActor : ReceiveActor
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private readonly ILoggingAdapter _log = Context.GetLogger();

        public QueueReaderActor(IActorRef fibonacciActor)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();

            _channel.QueueDeclare(queue: "MessageQueue",
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null);

            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (model, basicDeliverEventArgs) =>
            {
                var body = basicDeliverEventArgs.Body;
                var message = Encoding.UTF8.GetString(body);

                _log.Info("Received [{0}] from queue", message);

                int.TryParse(message, out var i);
                fibonacciActor.Tell(new FibonacciSourceMessage(i));
            };

            _channel.BasicConsume(queue: "MessageQueue", noAck: true, consumer: consumer);
        }

        public override void AroundPostStop()
        {
            _channel.Dispose();
            _connection.Dispose();
            base.AroundPostStop();
        }
    }
}
