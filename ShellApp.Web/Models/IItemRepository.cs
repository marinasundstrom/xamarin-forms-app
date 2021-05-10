using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShellApp.Models
{
    public interface IItemRepository
    {
        Task Add(Item item);
        Task Update(Item item);
        Task<Item> Remove(string key);
        Task<Item> Get(string id);
        Task<IEnumerable<Item>> GetAll();
    }
}
