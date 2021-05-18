using MediatR;

namespace ShellApp.Items.Queries
{
    public class GetItemQuery : IRequest<ItemDto>
    {
        public string ItemId { get; set; }

        public GetItemQuery(string itemId)
        {
            ItemId = itemId;
        }
    }
}
