using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.CommunityToolkit.Extensions;

using ShellApp.Views;
using ShellApp.Services;
using ShellApp.Client;
using System.Linq;
using Xamarin.Essentials;
using ShellApp.Events;

using System.Reactive.Linq;
using Xamarin.CommunityToolkit.UI.Views.Options;
using Microsoft.AspNetCore.SignalR.Client;

namespace ShellApp.ViewModels
{
    public class ItemsViewModel : BaseViewModel, IDisposable
    {
        private Item _selectedItem;
        private int _itemThreshold;
        private Command _loadMoreCommand;
        private readonly HubConnection hubConnection;
        private readonly IDisposable onItemDeletedEventSubscription;

        public ObservableCollection<Item> Items { get; }
        public Command LoadItemsCommand { get; }
        public Command AddItemCommand { get; }
        public Command<Item> ItemTapped { get; }

        public ItemsViewModel(IDataStore<Item> dataStore, IMessageBus messageBus, HubConnection hubConnection)
        {
            Title = "Browse";
            Items = new ObservableCollection<Item>();

            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            ItemTapped = new Command<Item>(OnItemSelected);

            AddItemCommand = new Command(OnAddItem);
            DataStore = dataStore;

            this.hubConnection = hubConnection;

            onItemDeletedEventSubscription = messageBus.WhenPublished<ItemDeletedEvent>().Subscribe(OnItemDeletedEvent);

            hubConnection.On("ItemCreated", (Item item) =>
            {
                Items.Add(item);
            });

            hubConnection.On("ItemDeleted", (string id) =>
            {
                var item = Items.FirstOrDefault(item => item.Id == id);
                if(item != null)
                {
                    Items.Remove(item);
                }
            });
        }

        private void OnItemDeletedEvent(ItemDeletedEvent obj)
        {
            Shell.Current.CurrentPage.DisplayToastAsync(new ToastOptions
            {
                MessageOptions = new MessageOptions()
                {
                    Message = $"\"{obj.Text}\" was deleted"
                }
            });
        }

        async Task ExecuteLoadItemsCommand()
        {
            try
            {
                Items.Clear();
                var items = await DataStore.GetItemsAsync(10, 0);
                foreach (var item in items)
                {
                    Items.Add(item);
                }

                ItemThreshold = 2;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        public async void OnAppearing()
        {
            IsBusy = true;
            SelectedItem = null;

            if (hubConnection.State == HubConnectionState.Connected || hubConnection.State == HubConnectionState.Connecting)
                return;

            await this.hubConnection.StartAsync();
        }

        public Item SelectedItem
        {
            get => _selectedItem;
            set
            {
                SetProperty(ref _selectedItem, value);
                OnItemSelected(value);
            }
        }

        public int ItemThreshold
        {
            get => _itemThreshold;
            set
            {
                SetProperty(ref _itemThreshold, value);
            }
        }

        public IDataStore<Item> DataStore { get; }

        private async void OnAddItem(object obj)
        {
            await Shell.Current.GoToAsync(nameof(NewItemPage));
        }

        async void OnItemSelected(Item item)
        {
            if (item == null)
                return;

            // This will push the ItemDetailPage onto the navigation stack
            await Shell.Current.GoToAsync($"{nameof(ItemDetailPage)}?{nameof(ItemDetailViewModel.ItemId)}={item.Id}");
        }

        public async void Dispose()
        {
            onItemDeletedEventSubscription.Dispose();

            await this.hubConnection.StopAsync();
        }

        public Command LoadMoreCommand => _loadMoreCommand ??= new Command(async () =>
        {
            if (IsLoading)
                return;

            IsLoading = true;

            try
            {
                var items = await DataStore.GetItemsAsync(10, Items.Count);

                await MainThread.InvokeOnMainThreadAsync(() =>
                {
                    foreach (var item in items)
                    {
                        Items.Add(item);
                    }
                });

                var count = items.Count();

                if (count == 0)
                {
                    ItemThreshold = -1;
                    return;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsLoading = false;
            }
        });

        public bool IsLoading { get; private set; }
    }
}
