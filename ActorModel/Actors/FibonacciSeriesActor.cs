using System;
using System.Collections.Generic;
using System.Linq;
using ActorModel.Messages;
using Akka.Actor;
using Akka.Event;

namespace ActorModel.Actors
{
    public class FibonacciSeriesActor : ReceiveActor
    {
        private readonly ILoggingAdapter _log = Context.GetLogger();

        public FibonacciSeriesActor()
        {
            _log.Info("FibonacciSeriesActor ctor.");

            Receive<FibonacciSourceMessage>(qm =>
            {
                var fibSeries = FibonacciSeries(qm.Value).ToList();
                Sender.Tell(new FibonacciSeriesMessage(fibSeries));
            });
        }

        public override void AroundPreRestart(Exception cause, object message)
        {
            _log.Warning("Restarting!" );
            base.AroundPreRestart(cause, message);
        }

        public IEnumerable<int> FibonacciSeries(int n)
        {
            _log.Info($"Computing fibonacci series for [{n}]");

            if (n < 0)
            {
                throw new ArgumentOutOfRangeException();
            }

            for (var i = 0; i < n; i++)
            {
                var a = 0;
                var b = 1;
                for (var j = 0; j < i; j++)
                {
                    var temp = a;
                    a = b;
                    b = temp + b;   
                }

                yield return a;
            }
        }
    }
}
