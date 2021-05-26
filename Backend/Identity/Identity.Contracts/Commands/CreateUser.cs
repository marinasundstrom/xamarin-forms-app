using System.IO;
using MediatR;

namespace ShellApp.Identity.Commands
{
    public class CreateUserCommand : IRequest
    {
        public string Email { get; set; } = null!;

        public string Password { get; set; } = null!;
    }
}
