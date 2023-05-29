using System.Runtime.CompilerServices;
using Microsoft.Extensions.Logging;

namespace AdminAssistant.Infra.Providers;
public abstract class LoggingProvider : ILoggingProvider
{
    private readonly ILogger _logger;

    protected LoggingProvider(ILoggerFactory loggerFactory, string logCategoryName)
        => _logger = loggerFactory.CreateLogger(logCategoryName);

#pragma warning disable CA2254 // Template should be a static expression

    public void LogDebug(EventId eventId, Exception exception, string message, params object[] args) => _logger.Log(LogLevel.Debug, eventId, exception, message, args);
    public void LogDebug(EventId eventId, string message, params object[] args) => _logger.Log(LogLevel.Debug, eventId, message, args);
    public void LogDebug(Exception exception, string message, params object[] args) => _logger.Log(LogLevel.Debug, exception, message, args);
    public void LogDebug(string message, params object[] args) => _logger.Log(LogLevel.Debug, message, args);

    public void LogTrace(EventId eventId, Exception exception, string message, params object[] args) => _logger.Log(LogLevel.Trace, eventId, exception, message, args);
    public void LogTrace(EventId eventId, string message, params object[] args) => _logger.Log(LogLevel.Trace, eventId, message, args);
    public void LogTrace(Exception exception, string message, params object[] args) => _logger.Log(LogLevel.Trace, exception, message, args);
    public void LogTrace(string message, params object[] args) => _logger.Log(LogLevel.Trace, message, args);

    public void LogInformation(EventId eventId, Exception exception, string message, params object[] args) => _logger.Log(LogLevel.Information, eventId, exception, message, args);
    public void LogInformation(EventId eventId, string message, params object[] args) => _logger.Log(LogLevel.Information, eventId, message, args);
    public void LogInformation(Exception exception, string message, params object[] args) => _logger.Log(LogLevel.Information, exception, message, args);
    public void LogInformation(string message, params object[] args) => _logger.Log(LogLevel.Information, message, args);

    public void LogWarning(EventId eventId, Exception exception, string message, params object[] args) => _logger.Log(LogLevel.Warning, eventId, exception, message, args);
    public void LogWarning(EventId eventId, string message, params object[] args) => _logger.Log(LogLevel.Warning, eventId, message, args);
    public void LogWarning(Exception exception, string message, params object[] args) => _logger.Log(LogLevel.Warning, exception, message, args);
    public void LogWarning(string message, params object[] args) => _logger.Log(LogLevel.Warning, message, args);

    public void LogError(EventId eventId, Exception exception, string message, params object[] args) => _logger.Log(LogLevel.Error, eventId, exception, message, args);
    public void LogError(EventId eventId, string message, params object[] args) => _logger.Log(LogLevel.Error, eventId, message, args);
    public void LogError(Exception exception, string message, params object[] args) => _logger.Log(LogLevel.Error, exception, message, args);
    public void LogError(string message, params object[] args) => _logger.Log(LogLevel.Error, message, args);

    public void LogCritical(EventId eventId, Exception exception, string message, params object[] args) => _logger.Log(LogLevel.Critical, eventId, exception, message, args);
    public void LogCritical(EventId eventId, string message, params object[] args) => _logger.Log(LogLevel.Critical, eventId, message, args);
    public void LogCritical(Exception exception, string message, params object[] args) => _logger.Log(LogLevel.Critical, exception, message, args);
    public void LogCritical(string message, params object[] args) => _logger.Log(LogLevel.Critical, message, args);

    public void Log(LogLevel logLevel, string message, params object[] args) => _logger.Log(logLevel, 0, null, message, args);
    public void Log(LogLevel logLevel, EventId eventId, string message, params object[] args) => _logger.Log(logLevel, eventId, null, message, args);
    public void Log(LogLevel logLevel, Exception exception, string message, params object[] args) => _logger.Log(logLevel, 0, exception, message, args);
    public void Log(LogLevel logLevel, EventId eventId, Exception exception, string message, params object[] args) => _logger.Log(logLevel, eventId, exception, message, args);
    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter) => _logger.Log(logLevel, eventId, state, exception, formatter);

    public IDisposable? BeginScope<TState>(TState state) where TState : notnull => _logger.BeginScope(state);
    public IDisposable? BeginScope(string messageFormat, params object[] args) => _logger.BeginScope(messageFormat, args);

    public void Start([CallerMemberName] string memberName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0) => _logger.LogDebug("Start {memberName}", memberName);
    public void Start(string message = "", [CallerMemberName] string memberName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0, params object[] args) => _logger.LogDebug("Start {memberName}", memberName);

    public void Finish([CallerMemberName] string memberName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0) => _logger.LogDebug("Finish {memberName}", memberName);
    public void Finish(string message = "", [CallerMemberName] string memberName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0, params object[] args) => _logger.LogDebug("Finish {memberName}", memberName);

    public TResult Finish<TResult>(TResult result, string message = "", [CallerMemberName] string memberName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0, params object[] args)
    {
        _logger.LogDebug("Finish {memberName}", memberName);
        return result;
    }
    public TResult Finish<TResult>(TResult result, [CallerMemberName] string memberName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
    {
        _logger.LogDebug("Finish {memberName}", memberName);
        return result;
    }

#pragma warning restore CA2254 // Template should be a static expression

    public bool IsEnabled(LogLevel logLevel) => _logger.IsEnabled(logLevel);
}
