using System;

namespace AdminAssistant.Infra.Providers
{

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Build", "CA1812", Justification = "Compiler dosen't understand dependency injection")]
    internal class DateTimeProvider : IDateTimeProvider
    {
        public DateTime UtcNow => DateTime.UtcNow;
    }
}
