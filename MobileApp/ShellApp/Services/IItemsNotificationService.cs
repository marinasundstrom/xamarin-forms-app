using System;
using System.Threading.Tasks;
using ShellApp.Client;

namespace ShellApp.Services
{
    public interface IItemsNotificationService
    {
        bool IsConnected { get; }

        bool IsConnecting { get; }

        event EventHandler<Item> ItemCreated;
        event EventHandler<string> ItemDeleted;

        Task ConnectAsync();
        Task DisconnectAsync();
    }
}