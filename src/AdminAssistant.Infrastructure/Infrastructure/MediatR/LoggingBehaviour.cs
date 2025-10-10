using AdminAssistant.Infrastructure.Providers;
using Ardalis.GuardClauses;
using Ardalis.Result;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AdminAssistant.Infrastructure.MediatR;

/// <summary>Logger for MediatR</summary>
/// <typeparam name="TRequest"></typeparam>
/// <typeparam name="TResponse"></typeparam>
/// <remarks>See "https://www.codewithmukesh.com/blog/mediatr-pipeline-behaviour/"</remarks>
internal sealed class LoggingBehaviour<TRequest, TResponse>(ILoggingProvider loggingProvider)
    : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        Guard.Against.Null(request);

        //Request
        var requestName = typeof(TRequest).Name;

        loggingProvider.LogInformation("{requestName} Handling Started", requestName);

        foreach (var prop in request.GetType().GetProperties())
        {
            loggingProvider.LogDebug("{Property} : {@Value}", prop.Name, prop.GetValue(request, null));
        }

        var response = await next().ConfigureAwait(false);

        //Response
        if (response is not IResult result)
        {
            loggingProvider.LogInformation("{requestName} Handling Completed", requestName);
            return response;
        }

        var status = LogResultDetails(requestName, result);

        loggingProvider.LogInformation("{requestName} Handling Completed - {status}", requestName, status);
        return response;
    }

    private string LogResultDetails(string requestName, IResult result)
    {

        if (result.Errors.Any())
        {
            loggingProvider.LogDebug("{requestName} Handling - {result.Errors.Count()} Errors:", requestName, result.Errors.Count());
        }

        if (result.ValidationErrors.Any())
        {
            loggingProvider.LogDebug("{requestName} Handling - {result.ValidationErrors.Count} Validation Errors:", requestName, result.ValidationErrors.Count());
        }

        return result.Status.ToString();
    }
}
