using System;
using MediatR;
using ShellApp.Items;

namespace ShellApp.Items.Events
{
    public class ItemCreatedEvent : DomainEvent
    {
        public ItemCreatedEvent(string itemId)
        {
            ItemId = itemId;
        }

        public string ItemId { get; }
    }
}
