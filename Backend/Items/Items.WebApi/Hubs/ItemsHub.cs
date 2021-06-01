using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using ShellApp.Items.Application.Common.Interfaces;

namespace ShellApp.Items.WebApi.Hubs
{
    [Authorize(LocalApi.AuthenticationScheme)]
    public class ItemsHub : Hub<IItemsNotificationClient>
    {
        public override Task OnConnectedAsync()
        {
            var user = Context.User;

            return Task.CompletedTask;
        }

        public override Task OnDisconnectedAsync(Exception? exception)
        {
            return Task.CompletedTask;
        }
    }
}
