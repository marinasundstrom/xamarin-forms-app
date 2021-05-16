using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using ShellApp.Application.Common.Interfaces;

namespace ShellApp.Infrastructure.Services
{
    class CommandDispatcher : ICommandDispatcher
    {
        private readonly ILogger<CommandDispatcher> logger;
        private ISender mediator;

        public CommandDispatcher(ILogger<CommandDispatcher> logger, ISender mediator)
        {
            this.logger = logger;
            this.mediator = mediator;
        }

        public async Task Send(IRequest request)
        {
            logger.LogInformation("Sent request. Request - {request}", request.GetType().Name);

            await mediator.Send(request);

            logger.LogInformation("Request returned. Request - {request}", request.GetType().Name);
        }

        public async Task<TResponse> Send<TResponse>(IRequest<TResponse> request)
        {
            logger.LogInformation("Sent request. Request - {request}", request.GetType().Name);

            var result = await mediator.Send(request);

            logger.LogInformation("Request returned. Request - {request}", request.GetType().Name);

            return result;
        }
    }
}
