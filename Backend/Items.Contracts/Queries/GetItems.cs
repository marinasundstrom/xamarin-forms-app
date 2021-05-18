using System;
using System.Collections.Generic;
using MediatR;

namespace ShellApp.Items.Queries
{
    public class GetItemsQuery : IRequest<IEnumerable<ItemDto>>
    {
        public int Skip { get; set; } = 0;

        public int Limit { get; set; } = 10;
    }
}
