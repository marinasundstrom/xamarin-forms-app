using System.Threading.Tasks;
using ShellApp.Items.Queries;

namespace ShellApp.Items.Application.Common.Interfaces
{
    public interface IItemsNotificationClient
    {
        Task ItemCreated(ItemDto item);
        Task ItemDeleted(string id);
    }
}