using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace ShellApp.Services
{
    public interface IDataStore<T>
    {
        Task<bool> CreateItemAsync(string text, string description, Stream picture);
        Task<bool> UpdateItemAsync(string id, string text, string description);
        Task<bool> DeleteItemAsync(string id);
        Task<T> GetItemAsync(string id);
        Task<IEnumerable<T>> GetItemsAsync(int limit, int skip);
    }
}
