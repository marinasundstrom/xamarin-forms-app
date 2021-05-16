using System.Collections.Generic;
using ShellApp;
using ShellApp.Events;

namespace ShellApp.Domain.Common
{
    public interface IHasDomainEvent
    {
        public List<DomainEvent> DomainEvents { get; set; }
    }
}
