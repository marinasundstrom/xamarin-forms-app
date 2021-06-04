using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ShellApp.Identity.Application.Common.Interfaces;
using ShellApp.Identity.Domain.Entities;

namespace ShellApp.Identity.Infrastructure
{
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IUserClaimsPrincipalFactory<ApplicationUser> _userClaimsPrincipalFactory;
        private readonly IAuthorizationService _authorizationService;

        public IdentityService(
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IUserClaimsPrincipalFactory<ApplicationUser> userClaimsPrincipalFactory,
            IAuthorizationService authorizationService)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _userClaimsPrincipalFactory = userClaimsPrincipalFactory;
            _authorizationService = authorizationService;
        }

        public async Task<string> GetUserNameAsync(string userId)
        {
            var user = await _userManager.Users.FirstAsync(u => u.Id == userId);

            return user.UserName;
        }

        public async Task<(IdentityResult IdentityResult, string UserId)> CreateUserAsync(string firstName, string lastName, string userName, string password)
        {
            var user = new ApplicationUser
            {
                FirstName = firstName,
                LastName = lastName,
                UserName = userName,
                Email = userName
            };

            var result = await _userManager.CreateAsync(user, password);

            var role = await _roleManager.FindByNameAsync("Admin");

            if(role == null)
            {
                role = new IdentityRole() { Name = "Admin" };
                await _roleManager.CreateAsync(role);
            }

            await _userManager.AddToRoleAsync(user, role.Name);

            return (result, user.Id);
        }

        public async Task<bool> IsInRoleAsync(string userId, string role)
        {
            var user = _userManager.Users.SingleOrDefault(u => u.Id == userId);

            return await _userManager.IsInRoleAsync(user!, role);
        }

        public async Task<bool> AuthorizeAsync(string userId, string policyName)
        {
            var user = _userManager.Users.SingleOrDefault(u => u.Id == userId);

            var principal = await _userClaimsPrincipalFactory.CreateAsync(user!);

            var result = await _authorizationService.AuthorizeAsync(principal, policyName);

            return result.Succeeded;
        }

        public async Task<IdentityResult> DeleteUserAsync(string userId)
        {
            var user = _userManager.Users.SingleOrDefault(u => u.Id == userId);

            if (user != null)
            {
                return await DeleteUserAsync(user);
            }

            throw new Exception();
        }

        public async Task<IdentityResult> DeleteUserAsync(ApplicationUser user)
        {
            return await _userManager.DeleteAsync(user);
        }
    }
}
