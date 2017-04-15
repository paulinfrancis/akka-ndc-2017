using System;
using ActorModel.Messages;
using Akka.Actor;

namespace ActorModel.Actors
{
    public class ClientBridgeActor : ReceiveActor
    {
        public ClientBridgeActor(Func<ISignalREventsPusher> getSignalREventsPusher)
        {
            var signalREventsPusher = getSignalREventsPusher();
            Receive<FibonacciSeriesMessage>(fibonacciSeriesMessage => signalREventsPusher.NotifyFibonacciSeriesMessage(fibonacciSeriesMessage));
        }
    }
}
