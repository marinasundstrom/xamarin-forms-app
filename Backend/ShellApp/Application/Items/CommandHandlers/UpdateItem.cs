﻿using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ShellApp.Application.Common.Interfaces;
using ShellApp.Commands;
using ShellApp.Events;
using ShellApp.Domain.Exceptions;

namespace ShellApp.Application.Items.CommandHandlers
{
    public class UpdateItemCommandHandler : IRequestHandler<UpdateItemCommand, string>
    {
        private readonly IApplicationContext context;

        public UpdateItemCommandHandler(IApplicationContext context)
        {
            this.context = context;
        }

        public async Task<string> Handle(UpdateItemCommand request, CancellationToken cancellationToken)
        {
            var item = await context.Items.FindAsync(request.ItemId);

            if (item == null)
            {
                throw new NotFoundException();
            }

            Mappings.Map(request, item);

            item.DomainEvents.Add(new ItemUpdatedEvent(item.Id));

            context.Items.Remove(item);

            await context.SaveChangesAsync();

            return item.Id;
        }
    }
}