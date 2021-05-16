using System;

namespace ShellApp.Queries
{
    public class ItemDto
    {
        public string Id { get; set; } = null!;

        public string Text { get; set; } = null!;

        public string Description { get; set; } = null!;

        public string PictureUri { get; set; } = null!;

        public DateTime Created { get; set; }

        public string CreatedBy { get; set; } = null!;

        public DateTime? LastModified { get; set; }

        public string? LastModifiedBy { get; set; }

    }
}