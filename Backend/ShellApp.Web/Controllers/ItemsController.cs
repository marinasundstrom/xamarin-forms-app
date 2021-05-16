using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShellApp.Commands;
using ShellApp.Queries;

namespace ShellApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private readonly IMediator mediator;

        public ItemsController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<ItemDto>>> GetItems(int limit = 10, int skip = 0)
        {
            return Ok(await mediator.Send(new GetItemsQuery() {
                Limit = limit,
                Skip = skip
            }));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ItemDto>> GetItem(string id)
        {
            ItemDto item = await mediator.Send(new GetItemQuery(id));

            if (item == null)
                return NotFound();

            return item;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ItemDto>> CreateItem(string text, string description)
        {
            var item = await mediator.Send(new CreateItemCommand()
            {
                Text = text,
                Description = description
            });

            return CreatedAtAction(nameof(GetItem), new { id = item.Id }, item);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> UpdateItem(string id, string text, string description)
        {
            try
            {
                await mediator.Send(new UpdateItemCommand()
                {
                    ItemId = id,
                    Text = text,
                    Description = description
                });
            }
            catch (Exception)
            {
                return BadRequest("Error while editing item");
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DeleteItem(string id)
        {
            await mediator.Send(new DeleteItemCommand(id));

            return Ok();
        }
    }
}
