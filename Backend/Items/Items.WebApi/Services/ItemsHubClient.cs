using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using ShellApp.Items.Application.Common.Interfaces;
using ShellApp.Items.Queries;
using ShellApp.Items.WebApi.Hubs;

namespace ShellApp.Items.WebApi.Services
{
    public class ItemsHubClient : IItemsNotificationClient
    {
        private readonly IHubContext<ItemsHub, IItemsNotificationClient> hubContext;

        public ItemsHubClient(IHubContext<ItemsHub, IItemsNotificationClient> hubContext)
        {
            this.hubContext = hubContext;
        }

        public async Task ItemCreated(ItemDto item)
        {
            await hubContext.Clients.All.ItemCreated(item);
        }

        public async Task ItemDeleted(string id)
        {
            await hubContext.Clients.All.ItemDeleted(id);
        }
    }
}
