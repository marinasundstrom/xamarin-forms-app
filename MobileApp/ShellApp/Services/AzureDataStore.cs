using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xamarin.Essentials;
using ShellApp.Client;
using System.IO;

namespace ShellApp.Services
{
    public class AzureDataStore : IDataStore<Item>
    {
        IEnumerable<Item> items;
        private readonly IItemsClient client;

        public AzureDataStore(IItemsClient client)
        {
            this.client = client;
            items = new List<Item>();
        }

        bool IsConnected => Connectivity.NetworkAccess == NetworkAccess.Internet;
        public async Task<IEnumerable<Item>> GetItemsAsync(int limit, int skip)
        {
            if (IsConnected)
            {
                items = await client.GetItemsAsync(limit, skip);
            }

            return items;
        }

        public async Task<Item> GetItemAsync(string id)
        {
            if (id != null && IsConnected)
            {
                return await client.GetItemAsync(id);
            }

            return null;
        }

        public async Task<bool> CreateItemAsync(string text, string description, Stream picture)
        {
            if (!IsConnected)
                return false;

            var response = await client.CreateItemAsync(text, description, new FileParameter(picture));

            return true;
        }

        public async Task<bool> UpdateItemAsync(string id, string text, string description)
        {
            if (id == null || !IsConnected)
                return false;

            await client.UpdateItemAsync(id, text, description);

            return true;
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            if (string.IsNullOrEmpty(id) && !IsConnected)
                return false;

            await client.DeleteItemAsync(id);

            return true;
        }
    }
}
