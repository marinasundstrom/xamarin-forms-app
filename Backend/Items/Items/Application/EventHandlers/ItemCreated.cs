using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ShellApp.Application.Common.Interfaces;
using ShellApp.Application.Common.Models;
using ShellApp.Items.Application.Common.Interfaces;
using ShellApp.Items.Events;
using ShellApp.Items.Queries;

namespace ShellApp.Items.Application.EventHandlers
{
    public class ItemCreatedEventHandler : INotificationHandler<DomainEventNotification<ItemCreatedEvent>>
    {
        private readonly ICommandDispatcher commandDispatcher;
        private readonly IItemsNotificationClient itemsNotificationClient;

        public ItemCreatedEventHandler(ICommandDispatcher commandDispatcher, IItemsNotificationClient itemsNotificationClient)
        {
            this.commandDispatcher = commandDispatcher;
            this.itemsNotificationClient = itemsNotificationClient;
        }

        public async Task Handle(DomainEventNotification<ItemCreatedEvent> notification, CancellationToken cancellationToken)
        {
            var itemCreatedEvent = notification.DomainEvent;
            var item = await commandDispatcher.Send(new GetItemQuery(itemCreatedEvent.ItemId));
            await itemsNotificationClient.ItemCreated(item);
        }
    }
}
