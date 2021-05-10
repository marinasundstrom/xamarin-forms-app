using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using ShellApp.Web.Data;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ShellApp.Models
{
    public class ItemRepository : IItemRepository
    {
        private ApplicationDataContext applicationDataContext;

        public ItemRepository(ApplicationDataContext applicationDataContext)
        {
            //Add(new Item { Id = Guid.NewGuid().ToString(), Text = "Item 1", Description = "This is an item description." });
            //Add(new Item { Id = Guid.NewGuid().ToString(), Text = "Item 2", Description = "This is an item description." });
            //Add(new Item { Id = Guid.NewGuid().ToString(), Text = "Item 3", Description = "This is an item description." });

            this.applicationDataContext = applicationDataContext;
        }

        public async Task<IEnumerable<Item>> GetAll()
        {
            return await applicationDataContext.Items.ToListAsync();
        }

        public async Task Add(Item item)
        {
            item.Id = Guid.NewGuid().ToString();
            applicationDataContext.Items.Add(item);
            await applicationDataContext.SaveChangesAsync();
        }

        public async Task<Item> Get(string id)
        {
            return await applicationDataContext.Items.FindAsync(id);
        }

        public async Task<Item> Remove(string id)
        {
            var item = await applicationDataContext.Items.FindAsync(id);
            applicationDataContext.Items.Remove(item);
            await applicationDataContext.SaveChangesAsync();
            return item;
        }

        public async Task Update(Item item)
        {
            applicationDataContext.Items.Update(item);
            await applicationDataContext.SaveChangesAsync();
        }
    }
}
