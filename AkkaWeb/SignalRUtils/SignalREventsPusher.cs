using System;
using ActorModel;
using ActorModel.Messages;
using Microsoft.AspNetCore.SignalR;

namespace AkkaWeb.SignalRUtils
{
    public class SignalREventsPusher : ISignalREventsPusher
    {
        private readonly Func<IHubContext> _getHubContext;

        public SignalREventsPusher(Func<IHubContext> getHubContext)
        {
            _getHubContext = getHubContext;
        }

        public void NotifyFibonacciSeriesMessage(FibonacciSeriesMessage queueMessage)
        {
            _getHubContext().Clients.All.setFibonacciSeriesMessage(queueMessage.Series);
        }
    }
}
