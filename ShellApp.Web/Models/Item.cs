using System;
using System.ComponentModel.DataAnnotations;

namespace ShellApp.Models
{
    public class Item
    {
        public string Id { get; set; } = null!;

        [Required]
        public string Text { get; set; } = null!;

        [Required]
        public string Description { get; set; } = null!;
    }
}
