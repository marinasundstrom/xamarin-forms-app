using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xamarin.Essentials;
using ShellApp.Client;

namespace ShellApp.Services
{
    public class AzureDataStore : IDataStore<Item>
    {
        IEnumerable<Item> items;
        private readonly IShellAppClient client;

        public AzureDataStore(IShellAppClient client)
        {
            this.client = client;
            items = new List<Item>();
        }

        bool IsConnected => Connectivity.NetworkAccess == NetworkAccess.Internet;
        public async Task<IEnumerable<Item>> GetItemsAsync(bool forceRefresh = false)
        {
            if (forceRefresh && IsConnected)
            {
                items = await client.ListAsync();
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

        public async Task<bool> AddItemAsync(Item item)
        {
            if (item == null || !IsConnected)
                return false;

            var response = await client.CreateAsync(item);

            return true;
        }

        public async Task<bool> UpdateItemAsync(Item item)
        {
            if (item == null || item.Id == null || !IsConnected)
                return false;

            await client.EditAsync(item);

            return true;
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            if (string.IsNullOrEmpty(id) && !IsConnected)
                return false;

            await client.DeleteAsync(id);

            return true;
        }
    }
}
