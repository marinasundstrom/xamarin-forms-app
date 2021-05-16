using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShellApp.Services
{
    public interface IDataStore<T>
    {
        Task<bool> CreateItemAsync(string text, string description);
        Task<bool> UpdateItemAsync(string id, string text, string description);
        Task<bool> DeleteItemAsync(string id);
        Task<T> GetItemAsync(string id);
        Task<IEnumerable<T>> GetItemsAsync(bool forceRefresh = false);
    }
}
