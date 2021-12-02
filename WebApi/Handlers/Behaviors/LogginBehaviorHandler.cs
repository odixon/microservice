using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;

namespace WebApi.Handlers.Behaviors
{
    public class LogginBehaviorHandler<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly ILogger<LogginBehaviorHandler<TRequest, TResponse>> _logger;

        public LogginBehaviorHandler(ILogger<LogginBehaviorHandler<TRequest, TResponse>> logger)
        {
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            _logger.LogInformation("调用 next 之前");
            var response = await next();
            _logger.LogInformation("调用 next 之后");

            return response;
        }
    }
}
