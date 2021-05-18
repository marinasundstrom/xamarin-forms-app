using System;
using System.Collections.Generic;
using ShellApp.Items.Domain.Common;

namespace ShellApp.Items.Domain.Entities
{
    public class Item : AuditableEntity, ISoftDelete, IHasDomainEvent
    {
        public string Id { get; set; } = null!;

        public string Text { get; set; } = null!;

        public string Description { get; set; } = null!;

        public string PictureUri { get; set; } = null!;

        public DateTime? Deleted { get; set; }

        public string? DeletedBy { get; set; }

        public List<DomainEvent> DomainEvents { get; set; } = new List<DomainEvent>();
    }
}
