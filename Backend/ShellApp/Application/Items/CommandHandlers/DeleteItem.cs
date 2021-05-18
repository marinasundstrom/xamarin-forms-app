using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ShellApp.Application.Common.Interfaces;
using ShellApp.Commands;
using ShellApp.Events;
using ShellApp.Domain.Exceptions;

namespace ShellApp.Application.Items.CommandHandlers
{

    public class DeleteItemCommandHandler : IRequestHandler<DeleteItemCommand>
    {
        private readonly IApplicationDataContext context;

        public DeleteItemCommandHandler(IApplicationDataContext context)
        {
            this.context = context;
        }

        public async Task<Unit> Handle(DeleteItemCommand request, CancellationToken cancellationToken)
        {
            var item = await context.Items.FindAsync(request.ItemId);

            if(item == null)
            {
                throw new NotFoundException();
            }

            item.DomainEvents.Add(new ItemDeletedEvent(item.Id));

            context.Items.Remove(item);

            await context.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
