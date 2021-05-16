using System.Threading.Tasks;
using MediatR;

namespace ShellApp.Application.Common.Interfaces
{
    public interface ICommandDispatcher
    {
        Task Send(IRequest request);

        Task<TResponse> Send<TResponse>(IRequest<TResponse> request);
    }
}