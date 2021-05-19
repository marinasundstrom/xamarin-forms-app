using System;
using Microsoft.AspNetCore.SignalR;
using ShellApp.Items.Application.Common.Interfaces;

namespace ShellApp.Items.WebApi.Hubs
{
    public class ItemsHub : Hub<IItemsNotificationClient>
    {

    }
}
