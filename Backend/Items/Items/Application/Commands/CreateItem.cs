using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ShellApp.Application.Common.Interfaces;
using ShellApp.Items.Commands;
using ShellApp.Items.Events;
using ShellApp.Items.Domain.Entities;
using System;
using ShellApp.Items.Queries;
using ShellApp.Items.Application.Common.Interfaces;

namespace ShellApp.Items.Application.Commands
{
    public class CreateItemCommandHandler : IRequestHandler<CreateItemCommand, ItemDto>
    {
        private readonly IDomainEventService domainEventService;
        private readonly IApplicationDbContext context;
        private readonly IImageUploader imageUploader;

        public CreateItemCommandHandler(
            IDomainEventService domainEventService, IApplicationDbContext context,
            IImageUploader imageUploader)
        {
            this.domainEventService = domainEventService;
            this.context = context;
            this.imageUploader = imageUploader;
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

            item.PictureUri = await imageUploader.UploadImageAsync(item.Id, request.Picture, cancellationToken);

            await context.SaveChangesAsync();

            await domainEventService.Publish(new ItemCreatedEvent(item.Id));

            var itemDto = Mappings.Map(item);

            return itemDto;
        }
    }
}
