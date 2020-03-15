using System;

namespace AdminAssistant.Framework.Providers
{
    public interface IDateTimeProvider
    {
        DateTime UtcNow { get; }
    }
    public class DateTimeProvider : IDateTimeProvider
    {
        public DateTime UtcNow => System.DateTime.UtcNow;
    }
}
