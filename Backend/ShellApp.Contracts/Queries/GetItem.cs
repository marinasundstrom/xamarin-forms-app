using MediatR;

namespace ShellApp.Queries
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
