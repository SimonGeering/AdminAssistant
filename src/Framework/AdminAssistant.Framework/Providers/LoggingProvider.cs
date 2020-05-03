using System;
using System.Runtime.CompilerServices;
using Microsoft.Extensions.Logging;

namespace AdminAssistant.Framework.Providers
{
    public interface ILoggingProvider
    {
        /// <summary>Formats and writes a debug log message.</summary>
        /// <param name="eventId">The event id associated with the log.</param>
        /// <param name="exception">The exception to log.</param>
        /// <param name="message">
        /// Format string of the log message in message template format. Example:
        ///     "User {User} logged in from {Address}"
        /// </param>
        /// <param name="args">An object array that contains zero or more objects to format.</param>
        void LogDebug(EventId eventId, Exception exception, string message, params object[] args);

        /// <summary>Formats and writes a debug log message.</summary>
        /// <param name="eventId">The event id associated with the log.</param>
        /// <param name="message">
        /// Format string of the log message in message template format. Example:
        ///     "User {User} logged in from {Address}"
        /// </param>
        /// <param name="args">An object array that contains zero or more objects to format.</param>
        void LogDebug(EventId eventId, string message, params object[] args);

        /// <summary>Formats and writes a debug log message.</summary>
        /// <param name="exception">The exception to log.</param>
        /// <param name="message">
        /// Format string of the log message in message template format. Example:
        ///     "User {User} logged in from {Address}"
        /// </param>
        /// <param name="args">An object array that contains zero or more objects to format.</param>
        void LogDebug(Exception exception, string message, params object[] args);

        /// <summary>Formats and writes a debug log message.</summary>
        /// <param name="message">
        /// Format string of the log message in message template format. Example:
        ///     "User {User} logged in from {Address}"
        /// </param>
        /// <param name="args">An object array that contains zero or more objects to format.</param>
        void LogDebug(string message, params object[] args);

        /// <summary>Formats and writes a trace log message.</summary>
        /// <param name="eventId">The event id associated with the log.</param>
        /// <param name="exception">The exception to log.</param>
        /// <param name="message">
        /// Format string of the log message in message template format. Example:
        ///     "User {User} logged in from {Address}"
        /// </param>
        /// <param name="args">An object array that contains zero or more objects to format.</param>
        void LogTrace(EventId eventId, Exception exception, string message, params object[] args);

        //   eventId:
        //     The event id associated with the log.
        //
        //   message:
        //     Format string of the log message in message template format. Example:
        //     "User {User} logged in from {Address}"
        //
        //   args:
        //     An object array that contains zero or more objects to format.
        /// <summary>Formats and writes a trace log message.</summary>
        /// <param name="logger"></param>
        /// <param name="eventId"></param>
        /// <param name="message"></param>
        /// <param name="args"></param>
        void LogTrace(this ILogger logger, EventId eventId, string message, params object[] args);

        //   exception:
        //     The exception to log.
        //
        //   message:
        //     Format string of the log message in message template format. Example:
        //     "User {User} logged in from {Address}"
        //
        //   args:
        //     An object array that contains zero or more objects to format.
        /// <summary>Formats and writes a trace log message.</summary>
        /// <param name="exception"></param>
        /// <param name="message"></param>
        /// <param name="args"></param>
        void LogTrace(Exception exception, string message, params object[] args);

        //   message:
        //     Format string of the log message in message template format. Example:
        //     "User {User} logged in from {Address}"
        //
        //   args:
        //     An object array that contains zero or more objects to format.
        /// <summary>Formats and writes a trace log message.</summary>
        /// <param name="message"></param>
        /// <param name="args"></param>
        void LogTrace(string message, params object[] args);

        //   eventId:
        //     The event id associated with the log.
        //
        //   exception:
        //     The exception to log.
        //
        //   message:
        //     Format string of the log message in message template format. Example:
        //     "User {User} logged in from {Address}"
        //
        //   args:
        //     An object array that contains zero or more objects to format.
        /// <summary>Formats and writes an informational log message.</summary>
        /// <param name="eventId"></param>
        /// <param name="exception"></param>
        /// <param name="message"></param>
        /// <param name="args"></param>
        void LogInformation(EventId eventId, Exception exception, string message, params object[] args);

        //   eventId:
        //     The event id associated with the log.
        //
        //   message:
        //     Format string of the log message in message template format. Example:
        //     "User {User} logged in from {Address}"
        //
        //   args:
        //     An object array that contains zero or more objects to format.
        /// <summary>Formats and writes an informational log message.</summary>
        /// <param name="logger"></param>
        /// <param name="eventId"></param>
        /// <param name="message"></param>
        /// <param name="args"></param>
        void LogInformation(EventId eventId, string message, params object[] args);

        //   exception:
        //     The exception to log.
        //
        //   message:
        //     Format string of the log message in message template format. Example:
        //     "User {User} logged in from {Address}"
        //
        //   args:
        //     An object array that contains zero or more objects to format.
        /// <summary>Formats and writes an informational log message.</summary>
        /// <param name="exception"></param>
        /// <param name="message"></param>
        /// <param name="args"></param>
        void LogInformation(Exception exception, string message, params object[] args);

        //   message:
        //     Format string of the log message in message template format. Example:
        //     "User {User} logged in from {Address}"
        //
        //   args:
        //     An object array that contains zero or more objects to format.
        /// <summary>Formats and writes an informational log message.</summary>
        /// <param name="message"></param>
        /// <param name="args"></param>
        void LogInformation(string message, params object[] args);

        //   eventId:
        //     The event id associated with the log.
        //
        //   exception:
        //     The exception to log.
        //
        //   message:
        //     Format string of the log message in message template format. Example:
        //     "User {User} logged in from {Address}"
        //
        //   args:
        //     An object array that contains zero or more objects to format.
        /// <summary>Formats and writes a warning log message.</summary>
        /// <param name="eventId"></param>
        /// <param name="exception"></param>
        /// <param name="message"></param>
        /// <param name="args"></param>
        void LogWarning(EventId eventId, Exception exception, string message, params object[] args);

        //   eventId:
        //     The event id associated with the log.
        //
        //   message:
        //     Format string of the log message in message template format. Example:
        //     "User {User} logged in from {Address}"
        //
        //   args:
        //     An object array that contains zero or more objects to format.
        /// <summary>Formats and writes a warning log message.</summary>
        /// <param name="eventId"></param>
        /// <param name="message"></param>
        /// <param name="args"></param>
        void LogWarning(EventId eventId, string message, params object[] args);

        //   exception:
        //     The exception to log.
        //
        //   message:
        //     Format string of the log message in message template format. Example:
        //     "User {User} logged in from {Address}"
        //
        //   args:
        //     An object array that contains zero or more objects to format.
        /// <summary>Formats and writes a warning log message.</summary>
        /// <param name="exception"></param>
        /// <param name="message"></param>
        /// <param name="args"></param>
        void LogWarning(Exception exception, string message, params object[] args);

        //   message:
        //     Format string of the log message in message template format. Example:
        //     "User {User} logged in from {Address}"
        //
        //   args:
        //     An object array that contains zero or more objects to format.
        /// <summary>Formats and writes a warning log message.</summary>
        /// <param name="message"></param>
        /// <param name="args"></param>
        void LogWarning(string message, params object[] args);

        //   eventId:
        //     The event id associated with the log.
        //
        //   exception:
        //     The exception to log.
        //
        //   message:
        //     Format string of the log message in message template format. Example:
        //     "User {User} logged in from {Address}"
        //
        //   args:
        //     An object array that contains zero or more objects to format.
        /// <summary>Formats and writes an error log message.</summary>
        /// <param name="eventId"></param>
        /// <param name="exception"></param>
        /// <param name="message"></param>
        /// <param name="args"></param>
        void LogError(EventId eventId, Exception exception, string message, params object[] args);

        //   eventId:
        //     The event id associated with the log.
        //
        //   message:
        //     Format string of the log message in message template format. Example:
        //     "User {User} logged in from {Address}"
        //
        //   args:
        //     An object array that contains zero or more objects to format.
        /// <summary>Formats and writes an error log message.</summary>
        /// <param name="eventId"></param>
        /// <param name="message"></param>
        /// <param name="args"></param>
        void LogError(EventId eventId, string message, params object[] args);

        //
        // Summary:
        //     Formats and writes an error log message.
        //
        // Parameters:
        //   logger:
        //     The Microsoft.Extensions.Logging.ILogger to write to.
        //
        //   exception:
        //     The exception to log.
        //
        //   message:
        //     Format string of the log message in message template format. Example:
        //     "User {User} logged in from {Address}"
        //
        //   args:
        //     An object array that contains zero or more objects to format.
        /// <summary>Formats and writes an error log message.</summary>
        /// <param name="exception"></param>
        /// <param name="message"></param>
        /// <param name="args"></param>
        void LogError(Exception exception, string message, params object[] args);

        //
        // Summary:
        //     Formats and writes an error log message.
        //
        // Parameters:
        //   logger:
        //     The Microsoft.Extensions.Logging.ILogger to write to.
        //
        //   message:
        //     Format string of the log message in message template format. Example:
        //     "User {User} logged in from {Address}"
        //
        //   args:
        //     An object array that contains zero or more objects to format.
        /// <summary>Formats and writes an error log message.</summary>
        /// <param name="message"></param>
        /// <param name="args"></param>
        void LogError(string message, params object[] args);

        // Summary:
        //     Formats and writes a critical log message.
        //
        // Parameters:
        //   logger:
        //     The Microsoft.Extensions.Logging.ILogger to write to.
        //
        //   eventId:
        //     The event id associated with the log.
        //
        //   exception:
        //     The exception to log.
        //
        //   message:
        //     Format string of the log message in message template format. Example:
        //     "User {User} logged in from {Address}"
        //
        //   args:
        //     An object array that contains zero or more objects to format.
        /// <summary>Formats and writes a critical log message.</summary>
        /// <param name="eventId"></param>
        /// <param name="exception"></param>
        /// <param name="message"></param>
        /// <param name="args"></param>
        void LogCritical(EventId eventId, Exception exception, string message, params object[] args);

        //
        // Summary:
        //     Formats and writes a critical log message.
        //
        // Parameters:
        //   logger:
        //     The Microsoft.Extensions.Logging.ILogger to write to.
        //
        //   eventId:
        //     The event id associated with the log.
        //
        //   message:
        //     Format string of the log message in message template format. Example:
        //     "User {User} logged in from {Address}"
        //
        //   args:
        //     An object array that contains zero or more objects to format.
        /// <summary>Formats and writes a critical log message.</summary>
        /// <param name="eventId"></param>
        /// <param name="message"></param>
        /// <param name="args"></param>
        void LogCritical(EventId eventId, string message, params object[] args);

        //
        // Summary:
        //     Formats and writes a critical log message.
        //
        // Parameters:
        //   logger:
        //     The Microsoft.Extensions.Logging.ILogger to write to.
        //
        //   exception:
        //     The exception to log.
        //
        //   message:
        //     Format string of the log message in message template format. Example:
        //     "User {User} logged in from {Address}"
        //
        //   args:
        //     An object array that contains zero or more objects to format.
        /// <summary>Formats and writes a critical log message.</summary>
        /// <param name="exception"></param>
        /// <param name="message"></param>
        /// <param name="args"></param>
        void LogCritical(Exception exception, string message, params object[] args);

        //
        // Summary:
        //     Formats and writes a critical log message.
        //
        // Parameters:
        //   logger:
        //     The Microsoft.Extensions.Logging.ILogger to write to.
        //
        //   message:
        //     Format string of the log message in message template format. Example:
        //     "User {User} logged in from {Address}"
        //
        //   args:
        //     An object array that contains zero or more objects to format.
        /// <summary>Formats and writes a critical log message.</summary>
        /// <param name="message"></param>
        /// <param name="args"></param>
        void LogCritical(string message, params object[] args);

        //
        // Summary:
        //     
        //
        // Parameters:
        //   logger:
        //     The Microsoft.Extensions.Logging.ILogger to write to.
        //
        //   logLevel:
        //     Entry will be written on this level.
        //
        //   message:
        //     Format string of the log message.
        //
        //   args:
        //     An object array that contains zero or more objects to format.
        /// <summary>Formats and writes a log message at the specified log level.</summary>
        /// <param name="logLevel"></param>
        /// <param name="message"></param>
        /// <param name="args"></param>
        void Log(LogLevel logLevel, string message, params object[] args);

        //
        // Summary:
        //     Formats and writes a log message at the specified log level.
        //
        // Parameters:
        //   logger:
        //     The Microsoft.Extensions.Logging.ILogger to write to.
        //
        //   logLevel:
        //     Entry will be written on this level.
        //
        //   eventId:
        //     The event id associated with the log.
        //
        //   message:
        //     Format string of the log message.
        //
        //   args:
        //     An object array that contains zero or more objects to format.
        void Log(LogLevel logLevel, EventId eventId, string message, params object[] args);

        //
        // Summary:
        //     Formats and writes a log message at the specified log level.
        //
        // Parameters:
        //   logger:
        //     The Microsoft.Extensions.Logging.ILogger to write to.
        //
        //   logLevel:
        //     Entry will be written on this level.
        //
        //   exception:
        //     The exception to log.
        //
        //   message:
        //     Format string of the log message.
        //
        //   args:
        //     An object array that contains zero or more objects to format.
        void Log(LogLevel logLevel, Exception exception, string message, params object[] args);

        //
        // Summary:
        //     Formats and writes a log message at the specified log level.
        //
        // Parameters:
        //   logger:
        //     The Microsoft.Extensions.Logging.ILogger to write to.
        //
        //   logLevel:
        //     Entry will be written on this level.
        //
        //   eventId:
        //     The event id associated with the log.
        //
        //   exception:
        //     The exception to log.
        //
        //   message:
        //     Format string of the log message.
        //
        //   args:
        //     An object array that contains zero or more objects to format.
        void Log(LogLevel logLevel, EventId eventId, Exception exception, string message, params object[] args);

        //
        // Summary:
        //     Formats the message and creates a scope.
        //
        // Parameters:
        //   logger:
        //     The Microsoft.Extensions.Logging.ILogger to create the scope in.
        //
        //   messageFormat:
        //     Format string of the log message in message template format. Example:
        //     "User {User} logged in from {Address}"
        //
        //   args:
        //     An object array that contains zero or more objects to format.
        //
        // Returns:
        //     A disposable scope object. Can be null.
        IDisposable BeginScope(string messageFormat, params object[] args);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="memberName"></param>
        /// <param name="sourceFilePath"></param>
        /// <param name="sourceLineNumber"></param>
        public void Start(
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="memberName"></param>
        /// <param name="sourceFilePath"></param>
        /// <param name="sourceLineNumber"></param>
        /// <param name="message"></param>
        /// <param name="args"></param>
        public void Start([CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0,
            string message = "", params object[] args);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="memberName"></param>
        /// <param name="sourceFilePath"></param>
        /// <param name="sourceLineNumber"></param>
        /// <param name="message"></param>
        /// <param name="args"></param>
        public void Finish(
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0,
            string message = "",
            params object[] args);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="memberName"></param>
        /// <param name="sourceFilePath"></param>
        /// <param name="sourceLineNumber"></param>
        public void Finish(
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0);

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="result"></param>
        /// <param name="memberName"></param>
        /// <param name="sourceFilePath"></param>
        /// <param name="sourceLineNumber"></param>
        /// <param name="message"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public TResult Finish<TResult>(
            TResult result,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0,
            string message = "",
            params object[] args);

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="result"></param>
        /// <param name="memberName"></param>
        /// <param name="sourceFilePath"></param>
        /// <param name="sourceLineNumber"></param>
        /// <returns></returns>
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


        //
        // Summary:
        //     Formats and writes a debug log message.
        //
        // Parameters:
        //   logger:
        //     The Microsoft.Extensions.Logging.ILogger to write to.
        //
        //   eventId:
        //     The event id associated with the log.
        //
        //   exception:
        //     The exception to log.
        //
        //   message:
        //     Format string of the log message in message template format. Example:
        //     "User {User} logged in from {Address}"
        //
        //   args:
        //     An object array that contains zero or more objects to format.
        public static void LogDebug(this ILogger logger, EventId eventId, Exception exception, string message, params object[] args)
        {
            logger.Log(LogLevel.Debug, eventId, exception, message, args);
        }

        //
        // Summary:
        //     Formats and writes a debug log message.
        //
        // Parameters:
        //   logger:
        //     The Microsoft.Extensions.Logging.ILogger to write to.
        //
        //   eventId:
        //     The event id associated with the log.
        //
        //   message:
        //     Format string of the log message in message template format. Example:
        //     "User {User} logged in from {Address}"
        //
        //   args:
        //     An object array that contains zero or more objects to format.
        public static void LogDebug(this ILogger logger, EventId eventId, string message, params object[] args)
        {
            logger.Log(LogLevel.Debug, eventId, message, args);
        }

        //
        // Summary:
        //     Formats and writes a debug log message.
        //
        // Parameters:
        //   logger:
        //     The Microsoft.Extensions.Logging.ILogger to write to.
        //
        //   exception:
        //     The exception to log.
        //
        //   message:
        //     Format string of the log message in message template format. Example:
        //     "User {User} logged in from {Address}"
        //
        //   args:
        //     An object array that contains zero or more objects to format.
        public static void LogDebug(this ILogger logger, Exception exception, string message, params object[] args)
        {
            logger.Log(LogLevel.Debug, exception, message, args);
        }

        //
        // Summary:
        //     Formats and writes a debug log message.
        //
        // Parameters:
        //   logger:
        //     The Microsoft.Extensions.Logging.ILogger to write to.
        //
        //   message:
        //     Format string of the log message in message template format. Example:
        //     "User {User} logged in from {Address}"
        //
        //   args:
        //     An object array that contains zero or more objects to format.
        public static void LogDebug(this ILogger logger, string message, params object[] args)
        {
            logger.Log(LogLevel.Debug, message, args);
        }

        //
        // Summary:
        //     Formats and writes a trace log message.
        //
        // Parameters:
        //   logger:
        //     The Microsoft.Extensions.Logging.ILogger to write to.
        //
        //   eventId:
        //     The event id associated with the log.
        //
        //   exception:
        //     The exception to log.
        //
        //   message:
        //     Format string of the log message in message template format. Example:
        //     "User {User} logged in from {Address}"
        //
        //   args:
        //     An object array that contains zero or more objects to format.
        public static void LogTrace(this ILogger logger, EventId eventId, Exception exception, string message, params object[] args)
        {
            logger.Log(LogLevel.Trace, eventId, exception, message, args);
        }

        //
        // Summary:
        //     Formats and writes a trace log message.
        //
        // Parameters:
        //   logger:
        //     The Microsoft.Extensions.Logging.ILogger to write to.
        //
        //   eventId:
        //     The event id associated with the log.
        //
        //   message:
        //     Format string of the log message in message template format. Example:
        //     "User {User} logged in from {Address}"
        //
        //   args:
        //     An object array that contains zero or more objects to format.
        public static void LogTrace(this ILogger logger, EventId eventId, string message, params object[] args)
        {
            logger.Log(LogLevel.Trace, eventId, message, args);
        }

        //
        // Summary:
        //     Formats and writes a trace log message.
        //
        // Parameters:
        //   logger:
        //     The Microsoft.Extensions.Logging.ILogger to write to.
        //
        //   exception:
        //     The exception to log.
        //
        //   message:
        //     Format string of the log message in message template format. Example:
        //     "User {User} logged in from {Address}"
        //
        //   args:
        //     An object array that contains zero or more objects to format.
        public static void LogTrace(this ILogger logger, Exception exception, string message, params object[] args)
        {
            logger.Log(LogLevel.Trace, exception, message, args);
        }

        //
        // Summary:
        //     Formats and writes a trace log message.
        //
        // Parameters:
        //   logger:
        //     The Microsoft.Extensions.Logging.ILogger to write to.
        //
        //   message:
        //     Format string of the log message in message template format. Example:
        //     "User {User} logged in from {Address}"
        //
        //   args:
        //     An object array that contains zero or more objects to format.
        public static void LogTrace(this ILogger logger, string message, params object[] args)
        {
            logger.Log(LogLevel.Trace, message, args);
        }

        //
        // Summary:
        //     Formats and writes an informational log message.
        //
        // Parameters:
        //   logger:
        //     The Microsoft.Extensions.Logging.ILogger to write to.
        //
        //   eventId:
        //     The event id associated with the log.
        //
        //   exception:
        //     The exception to log.
        //
        //   message:
        //     Format string of the log message in message template format. Example:
        //     "User {User} logged in from {Address}"
        //
        //   args:
        //     An object array that contains zero or more objects to format.
        public static void LogInformation(this ILogger logger, EventId eventId, Exception exception, string message, params object[] args)
        {
            logger.Log(LogLevel.Information, eventId, exception, message, args);
        }

        //
        // Summary:
        //     Formats and writes an informational log message.
        //
        // Parameters:
        //   logger:
        //     The Microsoft.Extensions.Logging.ILogger to write to.
        //
        //   eventId:
        //     The event id associated with the log.
        //
        //   message:
        //     Format string of the log message in message template format. Example:
        //     "User {User} logged in from {Address}"
        //
        //   args:
        //     An object array that contains zero or more objects to format.
        public static void LogInformation(this ILogger logger, EventId eventId, string message, params object[] args)
        {
            logger.Log(LogLevel.Information, eventId, message, args);
        }

        //
        // Summary:
        //     Formats and writes an informational log message.
        //
        // Parameters:
        //   logger:
        //     The Microsoft.Extensions.Logging.ILogger to write to.
        //
        //   exception:
        //     The exception to log.
        //
        //   message:
        //     Format string of the log message in message template format. Example:
        //     "User {User} logged in from {Address}"
        //
        //   args:
        //     An object array that contains zero or more objects to format.
        public static void LogInformation(this ILogger logger, Exception exception, string message, params object[] args)
        {
            logger.Log(LogLevel.Information, exception, message, args);
        }

        //
        // Summary:
        //     Formats and writes an informational log message.
        //
        // Parameters:
        //   logger:
        //     The Microsoft.Extensions.Logging.ILogger to write to.
        //
        //   message:
        //     Format string of the log message in message template format. Example:
        //     "User {User} logged in from {Address}"
        //
        //   args:
        //     An object array that contains zero or more objects to format.
        public static void LogInformation(this ILogger logger, string message, params object[] args)
        {
            logger.Log(LogLevel.Information, message, args);
        }

        //
        // Summary:
        //     Formats and writes a warning log message.
        //
        // Parameters:
        //   logger:
        //     The Microsoft.Extensions.Logging.ILogger to write to.
        //
        //   eventId:
        //     The event id associated with the log.
        //
        //   exception:
        //     The exception to log.
        //
        //   message:
        //     Format string of the log message in message template format. Example:
        //     "User {User} logged in from {Address}"
        //
        //   args:
        //     An object array that contains zero or more objects to format.
        public static void LogWarning(this ILogger logger, EventId eventId, Exception exception, string message, params object[] args)
        {
            logger.Log(LogLevel.Warning, eventId, exception, message, args);
        }

        //
        // Summary:
        //     Formats and writes a warning log message.
        //
        // Parameters:
        //   logger:
        //     The Microsoft.Extensions.Logging.ILogger to write to.
        //
        //   eventId:
        //     The event id associated with the log.
        //
        //   message:
        //     Format string of the log message in message template format. Example:
        //     "User {User} logged in from {Address}"
        //
        //   args:
        //     An object array that contains zero or more objects to format.
        public static void LogWarning(this ILogger logger, EventId eventId, string message, params object[] args)
        {
            logger.Log(LogLevel.Warning, eventId, message, args);
        }

        //
        // Summary:
        //     Formats and writes a warning log message.
        //
        // Parameters:
        //   logger:
        //     The Microsoft.Extensions.Logging.ILogger to write to.
        //
        //   exception:
        //     The exception to log.
        //
        //   message:
        //     Format string of the log message in message template format. Example:
        //     "User {User} logged in from {Address}"
        //
        //   args:
        //     An object array that contains zero or more objects to format.
        public static void LogWarning(this ILogger logger, Exception exception, string message, params object[] args)
        {
            logger.Log(LogLevel.Warning, exception, message, args);
        }

        //
        // Summary:
        //     Formats and writes a warning log message.
        //
        // Parameters:
        //   logger:
        //     The Microsoft.Extensions.Logging.ILogger to write to.
        //
        //   message:
        //     Format string of the log message in message template format. Example:
        //     "User {User} logged in from {Address}"
        //
        //   args:
        //     An object array that contains zero or more objects to format.
        public static void LogWarning(this ILogger logger, string message, params object[] args)
        {
            logger.Log(LogLevel.Warning, message, args);
        }

        //
        // Summary:
        //     Formats and writes an error log message.
        //
        // Parameters:
        //   logger:
        //     The Microsoft.Extensions.Logging.ILogger to write to.
        //
        //   eventId:
        //     The event id associated with the log.
        //
        //   exception:
        //     The exception to log.
        //
        //   message:
        //     Format string of the log message in message template format. Example:
        //     "User {User} logged in from {Address}"
        //
        //   args:
        //     An object array that contains zero or more objects to format.
        public static void LogError(this ILogger logger, EventId eventId, Exception exception, string message, params object[] args)
        {
            logger.Log(LogLevel.Error, eventId, exception, message, args);
        }

        //
        // Summary:
        //     Formats and writes an error log message.
        //
        // Parameters:
        //   logger:
        //     The Microsoft.Extensions.Logging.ILogger to write to.
        //
        //   eventId:
        //     The event id associated with the log.
        //
        //   message:
        //     Format string of the log message in message template format. Example:
        //     "User {User} logged in from {Address}"
        //
        //   args:
        //     An object array that contains zero or more objects to format.
        public static void LogError(this ILogger logger, EventId eventId, string message, params object[] args)
        {
            logger.Log(LogLevel.Error, eventId, message, args);
        }

        //
        // Summary:
        //     Formats and writes an error log message.
        //
        // Parameters:
        //   logger:
        //     The Microsoft.Extensions.Logging.ILogger to write to.
        //
        //   exception:
        //     The exception to log.
        //
        //   message:
        //     Format string of the log message in message template format. Example:
        //     "User {User} logged in from {Address}"
        //
        //   args:
        //     An object array that contains zero or more objects to format.
        public static void LogError(this ILogger logger, Exception exception, string message, params object[] args)
        {
            logger.Log(LogLevel.Error, exception, message, args);
        }

        //
        // Summary:
        //     Formats and writes an error log message.
        //
        // Parameters:
        //   logger:
        //     The Microsoft.Extensions.Logging.ILogger to write to.
        //
        //   message:
        //     Format string of the log message in message template format. Example:
        //     "User {User} logged in from {Address}"
        //
        //   args:
        //     An object array that contains zero or more objects to format.
        public static void LogError(this ILogger logger, string message, params object[] args)
        {
            logger.Log(LogLevel.Error, message, args);
        }

        //
        // Summary:
        //     Formats and writes a critical log message.
        //
        // Parameters:
        //   logger:
        //     The Microsoft.Extensions.Logging.ILogger to write to.
        //
        //   eventId:
        //     The event id associated with the log.
        //
        //   exception:
        //     The exception to log.
        //
        //   message:
        //     Format string of the log message in message template format. Example:
        //     "User {User} logged in from {Address}"
        //
        //   args:
        //     An object array that contains zero or more objects to format.
        public static void LogCritical(this ILogger logger, EventId eventId, Exception exception, string message, params object[] args)
        {
            logger.Log(LogLevel.Critical, eventId, exception, message, args);
        }

        //
        // Summary:
        //     Formats and writes a critical log message.
        //
        // Parameters:
        //   logger:
        //     The Microsoft.Extensions.Logging.ILogger to write to.
        //
        //   eventId:
        //     The event id associated with the log.
        //
        //   message:
        //     Format string of the log message in message template format. Example:
        //     "User {User} logged in from {Address}"
        //
        //   args:
        //     An object array that contains zero or more objects to format.
        public static void LogCritical(this ILogger logger, EventId eventId, string message, params object[] args)
        {
            logger.Log(LogLevel.Critical, eventId, message, args);
        }

        //
        // Summary:
        //     Formats and writes a critical log message.
        //
        // Parameters:
        //   logger:
        //     The Microsoft.Extensions.Logging.ILogger to write to.
        //
        //   exception:
        //     The exception to log.
        //
        //   message:
        //     Format string of the log message in message template format. Example:
        //     "User {User} logged in from {Address}"
        //
        //   args:
        //     An object array that contains zero or more objects to format.
        public static void LogCritical(this ILogger logger, Exception exception, string message, params object[] args)
        {
            logger.Log(LogLevel.Critical, exception, message, args);
        }

        //
        // Summary:
        //     Formats and writes a critical log message.
        //
        // Parameters:
        //   logger:
        //     The Microsoft.Extensions.Logging.ILogger to write to.
        //
        //   message:
        //     Format string of the log message in message template format. Example:
        //     "User {User} logged in from {Address}"
        //
        //   args:
        //     An object array that contains zero or more objects to format.
        public static void LogCritical(this ILogger logger, string message, params object[] args)
        {
            logger.Log(LogLevel.Critical, message, args);
        }

        //
        // Summary:
        //     Formats and writes a log message at the specified log level.
        //
        // Parameters:
        //   logger:
        //     The Microsoft.Extensions.Logging.ILogger to write to.
        //
        //   logLevel:
        //     Entry will be written on this level.
        //
        //   message:
        //     Format string of the log message.
        //
        //   args:
        //     An object array that contains zero or more objects to format.
        public static void Log(this ILogger logger, LogLevel logLevel, string message, params object[] args)
        {
            logger.Log(logLevel, 0, null, message, args);
        }

        //
        // Summary:
        //     Formats and writes a log message at the specified log level.
        //
        // Parameters:
        //   logger:
        //     The Microsoft.Extensions.Logging.ILogger to write to.
        //
        //   logLevel:
        //     Entry will be written on this level.
        //
        //   eventId:
        //     The event id associated with the log.
        //
        //   message:
        //     Format string of the log message.
        //
        //   args:
        //     An object array that contains zero or more objects to format.
        public static void Log(this ILogger logger, LogLevel logLevel, EventId eventId, string message, params object[] args)
        {
            logger.Log(logLevel, eventId, null, message, args);
        }

        //
        // Summary:
        //     Formats and writes a log message at the specified log level.
        //
        // Parameters:
        //   logger:
        //     The Microsoft.Extensions.Logging.ILogger to write to.
        //
        //   logLevel:
        //     Entry will be written on this level.
        //
        //   exception:
        //     The exception to log.
        //
        //   message:
        //     Format string of the log message.
        //
        //   args:
        //     An object array that contains zero or more objects to format.
        public static void Log(this ILogger logger, LogLevel logLevel, Exception exception, string message, params object[] args)
        {
            logger.Log(logLevel, 0, exception, message, args);
        }

        //
        // Summary:
        //     Formats and writes a log message at the specified log level.
        //
        // Parameters:
        //   logger:
        //     The Microsoft.Extensions.Logging.ILogger to write to.
        //
        //   logLevel:
        //     Entry will be written on this level.
        //
        //   eventId:
        //     The event id associated with the log.
        //
        //   exception:
        //     The exception to log.
        //
        //   message:
        //     Format string of the log message.
        //
        //   args:
        //     An object array that contains zero or more objects to format.
        public static void Log(this ILogger logger, LogLevel logLevel, EventId eventId, Exception exception, string message, params object[] args)
        {
            if (logger == null)
            {
                throw new ArgumentNullException("logger");
            }
            logger.Log(logLevel, eventId, new FormattedLogValues(message, args), exception, _messageFormatter);
        }

        //
        // Summary:
        //     Formats the message and creates a scope.
        //
        // Parameters:
        //   logger:
        //     The Microsoft.Extensions.Logging.ILogger to create the scope in.
        //
        //   messageFormat:
        //     Format string of the log message in message template format. Example:
        //     "User {User} logged in from {Address}"
        //
        //   args:
        //     An object array that contains zero or more objects to format.
        //
        // Returns:
        //     A disposable scope object. Can be null.
        public static IDisposable BeginScope(this ILogger logger, string messageFormat, params object[] args)
        {
            if (logger == null)
            {
                throw new ArgumentNullException("logger");
            }
            return logger.BeginScope(new FormattedLogValues(messageFormat, args));
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
