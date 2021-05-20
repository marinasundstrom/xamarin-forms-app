using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;
using ShellApp.Client;

namespace ShellApp.Services
{
    public class ItemsNotificationService : IAsyncDisposable, IItemsNotificationService
    {
        private readonly HubConnection hubConnection;

        public bool IsConnected => hubConnection.State == HubConnectionState.Connected;

        public bool IsConnecting => hubConnection.State == HubConnectionState.Connecting;

        public ItemsNotificationService(HubConnection hubConnection)
        {
            this.hubConnection = hubConnection;

            hubConnection.On("ItemCreated", (Item item) =>
            {
                ItemCreated?.Invoke(this, item);
            });

            hubConnection.On("ItemDeleted", (string id) =>
            {
                ItemDeleted?.Invoke(this, id);
            });
        }

        public async Task ConnectAsync()
        {
            await hubConnection.StartAsync();
        }

        public async Task DisconnectAsync()
        {
            await hubConnection.StopAsync();
        }

        public async ValueTask DisposeAsync()
        {
            await hubConnection.DisposeAsync();
        }

        public event EventHandler<Item> ItemCreated;

        public event EventHandler<string> ItemDeleted;
    }
}
