using MediatR;
using ShellApp.Queries;

namespace ShellApp.Commands
{
    public class CreateItemCommand : IRequest<ItemDto>
    {
        public string Text { get; set; } = null!;
        public string Description { get; set; } = null!;
    }
}
