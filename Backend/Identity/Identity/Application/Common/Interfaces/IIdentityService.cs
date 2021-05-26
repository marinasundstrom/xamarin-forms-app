using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using ShellApp.Identity.Domain.Entities;

namespace ShellApp.Identity.Application.Common.Interfaces
{
    public interface IIdentityService
    {
        Task<bool> AuthorizeAsync(string userId, string policyName);
        Task<(IdentityResult IdentityResult, string UserId)> CreateUserAsync(string userName, string password);
        Task<IdentityResult> DeleteUserAsync(string userId);
        Task<IdentityResult> DeleteUserAsync(ApplicationUser user);
        Task<string> GetUserNameAsync(string userId);
        Task<bool> IsInRoleAsync(string userId, string role);
    }
}
