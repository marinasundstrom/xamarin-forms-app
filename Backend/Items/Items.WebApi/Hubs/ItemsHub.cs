using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using ShellApp.Items.Application.Common.Interfaces;

namespace ShellApp.Items.WebApi.Hubs
{
    [Authorize]
    [Authorize(AuthenticationSchemes.DefaultAuthenticationScheme)]
    public class ItemsHub : Hub<IItemsNotificationClient>
    {

    }
}
