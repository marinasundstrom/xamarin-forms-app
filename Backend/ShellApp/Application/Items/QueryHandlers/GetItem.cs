using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ShellApp.Application.Common.Interfaces;
using ShellApp.Domain.Exceptions;
using ShellApp.Queries;

using static ShellApp.Application.Items.Mappings;

namespace ShellApp.Application.Items.QueryHandlers
{
    public class GetItemQueryHandler : IRequestHandler<GetItemQuery, ItemDto>
    {
        private readonly IApplicationContext context;

        public GetItemQueryHandler(IApplicationContext context)
        {
            this.context = context;
        }

        public async Task<ItemDto> Handle(GetItemQuery request, CancellationToken cancellationToken)
        {
            var item = await context.Items
                .AsNoTracking()
                .AsSplitQuery()
                .FirstOrDefaultAsync(item => item.Id == request.ItemId);

            if (item == null)
            {
                throw new NotFoundException();
            }

            return Mappings.Map(item);
        }
    }
}
