using System;
using ActorModel.Messages;
using Akka.Actor;

namespace ActorModel.Actors
{
    public class FibonacciActor : ReceiveActor
    {
        public FibonacciActor(IActorRef clientBridgeActor)
        {
            var fibonacciActorProps = Props.Create(() => new FibonacciSeriesActor());
            var fibonacciSeriesActorRef = Context.ActorOf(fibonacciActorProps, "FibonacciSeriesActor");

            Receive<FibonacciSeriesMessage>(fsm => clientBridgeActor.Tell(fsm));
            Receive<FibonacciSourceMessage>(qm => fibonacciSeriesActorRef.Tell(qm));
        }

        protected override SupervisorStrategy SupervisorStrategy()
        {
            // AllForOneStrategy is the other alternative
            return new OneForOneStrategy(maxNrOfRetries: 3, withinTimeMilliseconds: 1000, localOnlyDecider: ex =>
            {
                // Throw away the message and just continue processing incoming messages
                if (ex is ArgumentOutOfRangeException)
                {
                    return Directive.Restart;
                }

                return Directive.Stop;
            });
        }
    }
}
