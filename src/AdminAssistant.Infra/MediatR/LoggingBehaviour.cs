using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using AdminAssistant.Framework.Providers;
using Ardalis.GuardClauses;
using Ardalis.Result;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AdminAssistant.Framework.MediatR
{
    /// <summary>Logger for MediatR</summary>
    /// <typeparam name="TRequest"></typeparam>
    /// <typeparam name="TResponse"></typeparam>
    /// <remarks>See "https://www.codewithmukesh.com/blog/mediatr-pipeline-behaviour/"</remarks>
    public class LoggingBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
    {
        private readonly ILoggingProvider loggingProvider;

        public LoggingBehaviour(ILoggingProvider loggingProvider)
        {
            this.loggingProvider = loggingProvider;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            Guard.Against.Null(request, nameof(request));

            //Request
            string requestName = typeof(TRequest).Name;

            this.loggingProvider.LogInformation($"{requestName} Handling Started");

            foreach (PropertyInfo prop in request.GetType().GetProperties())
            {
                this.loggingProvider.LogDebug("{Property} : {@Value}", prop.Name, prop.GetValue(request, null));
            }

            var response = await next().ConfigureAwait(false);

            //Response
            var result = (response as IResult);

            if (result == null)
            {
                this.loggingProvider.LogInformation($"{requestName} Handling Completed");
                return response;
            }

            string status = this.LogResultDetails(requestName, result);

            this.loggingProvider.LogInformation($"{requestName} Handling Completed - {status}");
            return response;
        }

        private string LogResultDetails(string requestName, IResult result)
        {
            if (result.Errors.Any())
            {
                this.loggingProvider.LogDebug($"{requestName} Handling - {result.Errors.Count()} Errors:");
            }

            if (result.ValidationErrors.Any())
            {
                this.loggingProvider.LogDebug($"{requestName} Handling - {result.ValidationErrors.Count} Validation Errors:");
            }

            return result.Status.ToString();
        }
    }
}
