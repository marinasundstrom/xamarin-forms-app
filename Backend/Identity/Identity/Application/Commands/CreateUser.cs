using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ShellApp.Identity.Application.Common.Interfaces;
using ShellApp.Identity.Commands;
using ShellApp.Identity.Domain.Entities;

namespace ShellApp.Identity.Application.Commands
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand>
    {
        private readonly IIdentityService identityService;

        public CreateUserCommandHandler(IIdentityService identityService)
        {
            this.identityService = identityService;
        }

        public async Task<Unit> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            await identityService.CreateUserAsync(request.Email, request.Password);

            return Unit.Value;
        }
    }
}
