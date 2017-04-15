using ActorModel.Messages;

namespace ActorModel
{
    public interface ISignalREventsPusher
    {
        void NotifyFibonacciSeriesMessage(FibonacciSeriesMessage queueMessage);
    }
}