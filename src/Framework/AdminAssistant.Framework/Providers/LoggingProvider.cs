using System;
using System.Runtime.CompilerServices;
using Microsoft.Extensions.Logging;

namespace AdminAssistant.Framework.Providers
{
    public interface ILoggingProvider
    {
        public void Start(
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0);

        public void Start([CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0,
            string message = "", params object[] args);

        public void Finish(
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0,
            string message = "",
            params object[] args);

        public void Finish(
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0);

        public TResult Finish<TResult>(
            TResult result,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0,
            string message = "",
            params object[] args);

        public TResult Finish<TResult>(
            TResult result,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0);
    }
    public class LoggingProvider : ILoggingProvider
    {
        public const string LogCategoryName = "AdminAssistant";

        private readonly ILogger logger;

        public LoggingProvider(ILoggerFactory loggerFactory)
        {
            this.logger = loggerFactory.CreateLogger(LogCategoryName);
        }

        public void Start([CallerMemberName] string memberName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
        {
            logger.LogDebug("Start {memberName}", memberName);
        }

        public void Start([CallerMemberName] string memberName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0, string message = "", params object[] args)
        {
            logger.LogDebug("Start {memberName}", memberName);
        }

        public void Finish([CallerMemberName] string memberName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0, string message = "", params object[] args)
        {
            logger.LogDebug("Finish {memberName}", memberName);
        }

        public void Finish([CallerMemberName] string memberName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
        {
            logger.LogDebug("Finish {memberName}", memberName);
        }

        public TResult Finish<TResult>(TResult result, [CallerMemberName] string memberName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0, string message = "", params object[] args)
        {
            logger.LogDebug("Finish {memberName}", memberName);
            return result;
        }

        public TResult Finish<TResult>(TResult result, [CallerMemberName] string memberName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
        {
            logger.LogDebug("Finish {memberName}", memberName);
            return result;
        }
    }
}
