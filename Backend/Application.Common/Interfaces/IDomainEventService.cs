using System.Threading.Tasks;
using ShellApp;

namespace ShellApp.Application.Common.Interfaces
{
    public interface IDomainEventService
    {
        Task Publish(DomainEvent domainEvent);
    }
}