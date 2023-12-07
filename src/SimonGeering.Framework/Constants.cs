namespace SimonGeering.Framework;

/// <summary>
/// Global Constants.
/// </summary>
public static class Constants
{
    /// <summary>
    /// Represents the index of the first item in an array.
    /// </summary>
    /// <remarks>Used to implement https://rules.sonarsource.com/csharp/RSPEC-6608/</remarks>
    public const int FirstItem = 0;

    /// <summary>
    /// The default value for a RecordId.
    /// </summary>
    public const int UnknownRecordId = default;

    /// <summary>
    /// The value for a RecordId on an un-saved record.
    /// </summary>
    public const int NewRecordId = default;

    /// <summary>
    /// The date format used throughout the application.
    /// </summary>
    /// <remarks>
    /// The application does not currently support internationalization so this is sufficient for now.
    /// </remarks>
    public const string DefaultDateFormat = "dd-MMM-yyyy";
}
