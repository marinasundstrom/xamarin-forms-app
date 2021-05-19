using System.Collections.Generic;
using ShellApp.Items;
using ShellApp.Items.Events;

namespace ShellApp.Items.Domain.Common
{
    public interface IHasDomainEvent
    {
        public List<DomainEvent> DomainEvents { get; set; }
    }
}
