using System.Collections.Generic;
using ShellApp.Identity;

namespace ShellApp.Identity.Domain.Common
{
    public interface IHasDomainEvent
    {
        public List<DomainEvent> DomainEvents { get; set; }
    }
}
