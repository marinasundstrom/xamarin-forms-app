using System.IO;
using MediatR;
using ShellApp.Items.Queries;

namespace ShellApp.Items.Commands
{
    public class CreateItemCommand : IRequest<ItemDto>
    {
        public string Text { get; set; } = null!;
        public string Description { get; set; } = null!;
        public Stream Picture { get; set; } = null!;
    }
}
