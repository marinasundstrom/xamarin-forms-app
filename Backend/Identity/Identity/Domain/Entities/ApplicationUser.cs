using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using ShellApp.Identity.Domain.Common;

namespace ShellApp.Identity.Domain.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;
    }
}
