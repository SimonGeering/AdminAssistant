using AdminAssistant.Infra.Providers;
using Ardalis.GuardClauses;
using Ardalis.Result;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AdminAssistant.Framework.MediatR;

/// <summary>Logger for MediatR</summary>
/// <typeparam name="TRequest"></typeparam>
/// <typeparam name="TResponse"></typeparam>
/// <remarks>See "https://www.codewithmukesh.com/blog/mediatr-pipeline-behaviour/"</remarks>
internal sealed class LoggingBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
{
    private readonly ILoggingProvider _loggingProvider;

    public LoggingBehaviour(ILoggingProvider loggingProvider) => _loggingProvider = loggingProvider;

    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
    {
        Guard.Against.Null(request, nameof(request));

        //Request
        var requestName = typeof(TRequest).Name;

        _loggingProvider.LogInformation($"{requestName} Handling Started");

        foreach (var prop in request.GetType().GetProperties())
        {
            _loggingProvider.LogDebug("{Property} : {@Value}", prop.Name, prop.GetValue(request, null));
        }

        var response = await next().ConfigureAwait(false);

        //Response
        var result = response as IResult;

        if (result == null)
        {
            _loggingProvider.LogInformation($"{requestName} Handling Completed");
            return response;
        }

        var status = LogResultDetails(requestName, result);

        _loggingProvider.LogInformation($"{requestName} Handling Completed - {status}");
        return response;
    }

    private string LogResultDetails(string requestName, IResult result)
    {
        if (result.Errors.Any())
        {
            _loggingProvider.LogDebug($"{requestName} Handling - {result.Errors.Count()} Errors:");
        }

        if (result.ValidationErrors.Any())
        {
            _loggingProvider.LogDebug($"{requestName} Handling - {result.ValidationErrors.Count} Validation Errors:");
        }

        return result.Status.ToString();
    }
}
