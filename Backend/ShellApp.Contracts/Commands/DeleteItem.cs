using MediatR;

namespace ShellApp.Commands
{
    public class DeleteItemCommand : IRequest
    {
        public DeleteItemCommand(string itemId)
        {
            ItemId = itemId;
        }

        public string ItemId { get; }
    }
}
