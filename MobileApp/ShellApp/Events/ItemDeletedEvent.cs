using System;
namespace ShellApp.Events
{
    public class ItemDeletedEvent
    {
        public string Id { get; set; } = null!;

        public string Text { get; set; } = null!;
    }
}
