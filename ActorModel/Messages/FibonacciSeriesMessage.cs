using System.Collections.Generic;

namespace ActorModel.Messages
{
    public class FibonacciSeriesMessage
    {
        public FibonacciSeriesMessage(IList<int> series)
        {
            Series = series;
        }

        public IList<int> Series { get; }
    }
}
