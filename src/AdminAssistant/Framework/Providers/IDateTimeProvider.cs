using System;

namespace AdminAssistant.Framework.Providers
{
    public interface IDateTimeProvider
    {
        ///<summary>
        ///Gets a System.DateTime object that is set to the current date and time on this
        ///computer, expressed as the Coordinated Universal Time (UTC).
        ///</summary>
        ///<returns>An object whose value is the current UTC date and time.</returns>
        DateTime UtcNow { get; }
    }
}
