using System.Runtime.CompilerServices;
using Microsoft.Extensions.Logging;

namespace AdminAssistant.Framework.Providers
{
    public interface ILoggingProvider : ILogger
    {
        public const string ClientSideLogCategory = "Admin Assistant - Client";
        public const string ServerSideLogCategory = "Admin Assistant - Server";

        /// <summary></summary>
        /// <param name="memberName"></param>
        /// <param name="sourceFilePath"></param>
        /// <param name="sourceLineNumber"></param>
        public void Start([CallerMemberName] string memberName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0);

        /// <summary></summary>
        /// <param name="memberName"></param>
        /// <param name="sourceFilePath"></param>
        /// <param name="sourceLineNumber"></param>
        /// <param name="message"></param>
        /// <param name="args"></param>
        public void Start([CallerMemberName] string memberName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0, string message = "", params object[] args);

        /// <summary></summary>
        /// <param name="memberName"></param>
        /// <param name="sourceFilePath"></param>
        /// <param name="sourceLineNumber"></param>
        /// <param name="message"></param>
        /// <param name="args"></param>
        public void Finish([CallerMemberName] string memberName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0, string message = "", params object[] args);

        /// <summary></summary>
        /// <param name="memberName"></param>
        /// <param name="sourceFilePath"></param>
        /// <param name="sourceLineNumber"></param>
        public void Finish([CallerMemberName] string memberName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0);

        /// <summary></summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="result"></param>
        /// <param name="memberName"></param>
        /// <param name="sourceFilePath"></param>
        /// <param name="sourceLineNumber"></param>
        /// <param name="message"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public TResult Finish<TResult>(TResult result, [CallerMemberName] string memberName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0, string message = "", params object[] args);

        /// <summary></summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="result"></param>
        /// <param name="memberName"></param>
        /// <param name="sourceFilePath"></param>
        /// <param name="sourceLineNumber"></param>
        /// <returns></returns>
        public TResult Finish<TResult>(TResult result, [CallerMemberName] string memberName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0);
    }
}
