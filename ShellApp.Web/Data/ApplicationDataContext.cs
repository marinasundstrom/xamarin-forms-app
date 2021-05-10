using System;
using Microsoft.EntityFrameworkCore;
using ShellApp.Models;

namespace ShellApp.Web.Data
{
    public class ApplicationDataContext : DbContext
    {
        public ApplicationDataContext(DbContextOptions<ApplicationDataContext> options)
            : base(options)
        {
        }

#nullable disable

        public DbSet<Item> Items { get; set; }

#nullable restore
    }
}
