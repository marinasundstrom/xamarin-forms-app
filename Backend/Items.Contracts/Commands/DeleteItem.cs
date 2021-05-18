using MediatR;

namespace ShellApp.Items.Commands
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
