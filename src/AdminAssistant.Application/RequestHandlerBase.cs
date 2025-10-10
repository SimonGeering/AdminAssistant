namespace AdminAssistant;

internal abstract class RequestHandlerBase<TRequest, TResponse>(ILoggingProvider loggingProvider)
    : IRequestHandler<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    protected ILoggingProvider Log { get; } = loggingProvider;

    public abstract Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken);
}
