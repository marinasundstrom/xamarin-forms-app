using System;
using System.Diagnostics;
using System.Threading.Tasks;
using ShellApp.Client;
using ShellApp.Events;
using ShellApp.Services;
using Xamarin.Forms;

namespace ShellApp.ViewModels
{
    [QueryProperty(nameof(ItemId), nameof(ItemId))]
    public class ItemDetailViewModel : BaseViewModel
    {
        private string itemId;
        private string text;
        private string description;
        private string pictureUri;
        private Command deleteItemCommand;
        private readonly IMessageBus messageBus;

        public ItemDetailViewModel(IDataStore<Item> dataStore, IMessageBus messageBus)
        {
            DataStore = dataStore;
            this.messageBus = messageBus;
        }

        public string Id { get; set; }

        public string Text
        {
            get => text;
            set => SetProperty(ref text, value);
        }

        public string Description
        {
            get => description;
            set => SetProperty(ref description, value);
        }

        public string PictureUri
        {
            get => pictureUri;
            set => SetProperty(ref pictureUri, value);
        }

        public string ItemId
        {
            get
            {
                return itemId;
            }
            set
            {
                itemId = value;
                LoadItemId(value);
            }
        }

        public IDataStore<Item> DataStore { get; }

        public async void LoadItemId(string itemId)
        {
            try
            {
                var item = await DataStore.GetItemAsync(itemId);
                Id = item.Id;
                Text = item.Text;
                Description = item.Description;
                PictureUri = item.PictureUri;
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Item");
            }
        }

        public Command DeleteItemCommand => deleteItemCommand ??= new Command(async () =>
        {
            var result = await Shell.Current.DisplayActionSheet("Are you sure that you want to delete this item?", "Cancel", "Delete");

            switch (result)
            {
                case "Cancel":

                    // Do Something when 'Cancel' Button is pressed

                    break;

                case "Delete":
                    var items = await DataStore.DeleteItemAsync(ItemId);

                    await Shell.Current.Navigation.PopAsync();

                    messageBus.Publish(new ItemDeletedEvent
                    {
                        Id = ItemId,
                        Text = text
                    });

                    break;
            }
        });
    }
}
