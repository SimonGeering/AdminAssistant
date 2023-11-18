namespace AdminAssistant;

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
    /// The default value for a RecordID.
    /// </summary>
    public const int UnknownRecordID = default;

    /// <summary>
    /// The value for a RecordID on an un-saved record.
    /// </summary>
    public const int NewRecordID = default;

    /// <summary>
    /// The maximum length of a key field.
    /// </summary>
    public const int KeyMaxLength = 20;

    /// <summary>
    /// The maximum length of a name filed
    /// </summary>
    /// <remarks>
    /// For example the name of a person or company.
    /// </remarks>
    public const int NameMaxLength = 50;

    /// <summary>
    /// The max length of a user sign-on.
    /// </summary>
    /// <remarks>
    /// For example a windows login or online account user name
    /// </remarks>
    public const int UserSignOnMaxLength = 50;

    /// <summary>
    /// The maximum length of a description field.
    /// </summary>
    public const int DescriptionMaxLength = 255;

    /// <summary>
    /// The maximum length of a notes field.
    /// </summary>
    public const int NotesMaxLength = 4000;

    // TODO: Lookup what the max length of an ID is this is in the MS Graph API docs.
    /// <summary>
    /// The maximum length of an ID field used for objects in MS Graph APIs
    /// </summary>
    public const int MSGraphIDMaxLength = 50;

    /// <summary>
    /// The date format used throughout the application.
    /// </summary>
    /// <remarks>
    /// The application does not currently support internationalization so this is sufficient for now.
    /// </remarks>
    public const string DateFormat = "dd-MMM-yyyy";

    /// <summary>
    /// The name used for adding httpClient to the services collection.
    /// </summary>
    public const string AdminAssistantWebAPIClient = "AdminAssistantWebAPIClient";
}
