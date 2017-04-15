using System;
using ActorModel;
using ActorModel.Actors;
using Akka.Actor;
using Akka.Configuration;
using AkkaWeb.SignalRUtils;

namespace AkkaWeb.ActorSys
{
    public static class DemoActorSystem
    {
        private static ActorSystem _actorSystem;

        public static void Create(Func<IHubContextAccessor> getHubContextAccessor, Config akkaConfig = null)
        {
            if (akkaConfig != null)
            {
                _actorSystem = ActorSystem.Create("DemoActorSystem", akkaConfig);
            }
            else
            {
                _actorSystem = ActorSystem.Create("DemoActorSystem");
            }

            Func<ISignalREventsPusher> getSignalRSignalREventsPusher = () => new SignalREventsPusher(getHubContextAccessor().GetHubContext);

            var clientBridgeProps = Props.Create(() => new ClientBridgeActor(getSignalRSignalREventsPusher));
            ActorReferences.ClientBridge = _actorSystem.ActorOf(clientBridgeProps, "ClientBridge");

            var fibonacciActorProps = Props.Create(() => new FibonacciActor(ActorReferences.ClientBridge));
            var fibonacciActor = _actorSystem.ActorOf(fibonacciActorProps, "FibonacciActor");

            var queueActorPops = Props.Create(() => new QueueReaderActor(fibonacciActor));
            var queueActor = _actorSystem.ActorOf(queueActorPops, "QueueReaderActor");
        }

        public static async void Shutdown()
        {
            await _actorSystem.Terminate();
            await _actorSystem.WhenTerminated;
        }

        public static class ActorReferences
        {
            public static IActorRef ClientBridge { get; set; }
        }
    }
}
