using System;
using MediatR;
using ShellApp;

namespace ShellApp.Events
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
