using System;
using Microsoft.AspNetCore.SignalR;

namespace AkkaWeb.SignalRUtils
{
    public interface IHubContextAccessor
    {
        Func<IHubContext> GetHubContext { get; }
    }
}
