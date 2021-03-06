using System.Threading;
using System.Threading.Tasks;
using AdminAssistant.Infra.Providers;
using MediatR;

namespace AdminAssistant.DomainModel
{
    public abstract class RequestHandlerBase<TRequest, TResponse> : IRequestHandler<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        protected ILoggingProvider Log { get; }

        public RequestHandlerBase(ILoggingProvider loggingProvider) => Log = loggingProvider;

        public abstract Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken);
    }
}
