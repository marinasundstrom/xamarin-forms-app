using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ShellApp.Items.Application.Common.Interfaces;
using ShellApp.Items.Queries;

using static ShellApp.Items.Application.Items.Mappings;

namespace ShellApp.Items.Application.Items.QueryHandlers
{
    public class GetItemsQueryHandler : IRequestHandler<GetItemsQuery, IEnumerable<ItemDto>>
    {
        private readonly IApplicationDataContext context;

        public GetItemsQueryHandler(IApplicationDataContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<ItemDto>> Handle(GetItemsQuery request, CancellationToken cancellationToken)
        {
            var query = context.Items
                .AsNoTracking()
                .AsSplitQuery()
                .OrderBy(item => item.Created)
                .AsQueryable();

            query = query.Skip(request.Skip);

            query = query.Take(request.Limit);

            var items = await query.ToArrayAsync();

            return items.Select(Map);
        }
    }
}
