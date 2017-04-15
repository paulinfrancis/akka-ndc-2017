using System;
using AkkaWeb.Hubs;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.SignalR.Infrastructure;

namespace AkkaWeb.SignalRUtils
{
    public class HubContextAccessor : IHubContextAccessor
    {
        public HubContextAccessor(IServiceProvider serviceProvider)
        {
            GetHubContext = () => new ConnectionManager(serviceProvider).GetHubContext<Example>();
        }

        public Func<IHubContext> GetHubContext { get; }
    }
}
