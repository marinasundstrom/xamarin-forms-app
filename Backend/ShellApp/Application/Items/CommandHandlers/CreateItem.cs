using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ShellApp.Application.Common.Interfaces;
using ShellApp.Commands;
using ShellApp.Events;
using ShellApp.Domain.Entities;
using System;
using ShellApp.Queries;

namespace ShellApp.Application.Items.CommandHandlers
{
    public class CreateItemCommandHandler : IRequestHandler<CreateItemCommand, ItemDto>
    {
        private readonly IDomainEventService domainEventService;
        private readonly IApplicationDataContext context;

        public CreateItemCommandHandler(IDomainEventService domainEventService, IApplicationDataContext context)
        {
            this.domainEventService = domainEventService;
            this.context = context;
        }

        public async Task<ItemDto> Handle(CreateItemCommand request, CancellationToken cancellationToken)
        {
            var item = new Item()
            {
                Id = Guid.NewGuid().ToString(),
                Text = request.Text,
                Description = request.Description,
                PictureUri = ""
            };

            context.Items.Add(item);

            await context.SaveChangesAsync();

            await domainEventService.Publish(new ItemCreatedEvent(item.Id));

            return Mappings.Map(item);
        }
    }
}
