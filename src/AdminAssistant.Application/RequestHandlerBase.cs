namespace AdminAssistant;

internal abstract class RequestHandlerBase<TRequest, TResponse> : IRequestHandler<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    protected ILoggingProvider Log { get; }

    protected RequestHandlerBase(ILoggingProvider loggingProvider) => Log = loggingProvider;

    public abstract Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken);
}
