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
        private readonly IDisposable onItemDeletedEventSubscription;
        private readonly IItemsNotificationService itemsNotificationService;

        public ObservableCollection<Item> Items { get; }
        public Command LoadItemsCommand { get; }
        public Command AddItemCommand { get; }
        public Command<Item> ItemTapped { get; }

        public ItemsViewModel(IItemsDataService<Item> dataStore,
            IMessageBus messageBus, IItemsNotificationService itemsNotificationService)
        {
            Title = "Browse";
            Items = new ObservableCollection<Item>();

            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            ItemTapped = new Command<Item>(OnItemSelected);

            AddItemCommand = new Command(OnAddItem);
            DataStore = dataStore;
            this.itemsNotificationService = itemsNotificationService;

            onItemDeletedEventSubscription = messageBus.WhenPublished<ItemDeletedEvent>().Subscribe(OnItemDeletedEvent);

            itemsNotificationService.ItemCreated += OnNotifiedItemCreated;
            itemsNotificationService.ItemDeleted += OnNotifiedItemDeleted;
        }

        private void OnNotifiedItemCreated(object sender, Item item)
        {
            Items.Add(item);
        }

        private void OnNotifiedItemDeleted(object sender, string id)
        {
            var item = Items.FirstOrDefault(item => item.Id == id);
            if (item != null)
            {
                Items.Remove(item);
            }
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

            if (itemsNotificationService.IsConnected)
                return;

            await this.itemsNotificationService.ConnectAsync();
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

        public IItemsDataService<Item> DataStore { get; }

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

            itemsNotificationService.ItemCreated -= OnNotifiedItemCreated;
            itemsNotificationService.ItemDeleted -= OnNotifiedItemDeleted;

            await this.itemsNotificationService.DisconnectAsync();
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
