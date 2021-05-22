using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ShellApp.Items.Application.Common.Interfaces;
using ShellApp.Domain.Exceptions;
using ShellApp.Items.Queries;

using static ShellApp.Items.Application.Mappings;

namespace ShellApp.Items.Application.Queries
{
    public class GetItemQueryHandler : IRequestHandler<GetItemQuery, ItemDto>
    {
        private readonly IApplicationDbContext context;

        public GetItemQueryHandler(IApplicationDbContext context)
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
