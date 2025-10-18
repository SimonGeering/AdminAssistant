using Ardalis.GuardClauses;
using Microsoft.Extensions.Logging;

namespace AdminAssistant.Application;

/// <summary>Logger for Mediator</summary>
/// <typeparam name="TRequest"></typeparam>
/// <typeparam name="TResponse"></typeparam>
/// <remarks>See "https://github.com/martinothamar/Mediator?tab=readme-ov-file#44-use-pipeline-behaviors"</remarks>
internal sealed class LoggingPipelineBehaviour<TRequest, TResponse>(ILoggingProvider loggingProvider)
    : IPipelineBehavior<TRequest, TResponse> where TRequest : Mediator.IMessage
{
    public async ValueTask<TResponse> Handle(TRequest message, MessageHandlerDelegate<TRequest, TResponse> next, CancellationToken cancellationToken)
    {
        Guard.Against.Null(message);

        //Request
        var requestName = typeof(TRequest).Name;

        loggingProvider.LogInformation("{RequestName} Handling Started", requestName);

        foreach (var prop in message.GetType().GetProperties())
        {
            loggingProvider.LogDebug("{Property} : {@Value}", prop.Name, prop.GetValue(message, null));
        }

        var response = await next(message, cancellationToken).ConfigureAwait(false);

        //Response
        if (response is not IResult result)
        {
            loggingProvider.LogInformation("{RequestName} Handling Completed", requestName);
            return response;
        }

        var status = LogResultDetails(requestName, result);

        loggingProvider.LogInformation("{RequestName} Handling Completed - {Status}", requestName, status);
        return response;
    }

    private string LogResultDetails(string requestName, IResult result)
    {
        if (result.Errors.Any())
            loggingProvider.LogDebug("{RequestName} Handling - {ErrorCount} Errors:", requestName, result.Errors.Count());

        if (result.ValidationErrors.Any())
            loggingProvider.LogDebug("{RequestName} Handling - {ValidationErrorCount} Validation Errors:", requestName, result.ValidationErrors.Count());

        return result.Status.ToString();
    }
}
