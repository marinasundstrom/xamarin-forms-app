using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ShellApp.Application.Common.Interfaces;
using ShellApp.Application.Common.Models;
using ShellApp.Items.Application.Common.Interfaces;
using ShellApp.Items.Events;

namespace ShellApp.Items.Application.EventHandlers
{
    public class ItemDeletedEventHandler : INotificationHandler<DomainEventNotification<ItemDeletedEvent>>
    {
        private readonly IItemsNotificationClient itemsNotificationClient;

        public ItemDeletedEventHandler(IItemsNotificationClient itemsNotificationClient)
        {
            this.itemsNotificationClient = itemsNotificationClient;
        }

        public async Task Handle(DomainEventNotification<ItemDeletedEvent> notification, CancellationToken cancellationToken)
        {
            var itemDeletedEvent = notification.DomainEvent;
            await itemsNotificationClient.ItemDeleted(itemDeletedEvent.ItemId);
        }
    }
}
