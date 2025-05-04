// ReSharper disable once CheckNamespace
namespace AdminAssistant;

/// <summary>
/// Global Constants.
/// </summary>
public static class Constants
{
    public const string ServerAppName = "AdminAssistantServer";

    public const string DatabaseServerName = "AdminAssistantDatabaseServer";
    public const string ApplicationDatabaseName = "AdminAssistant";
    public const string ScheduledJobDatabase = "AdminAssistantScheduledJobs";

    public const string AvaloniaAppName = "AdminAssistant-Avalonia";
    public const string RetroConsole = "AdminAssistant-RetroConsole";
    public const string DatabaseMigrationWorkerService = "AdminAssistant-DatabaseMigrationWorkerService";

    public static class ApiGateway
    {
        private const string ApiRoutePrefix = "api";
        public const string CoreApiPrefix = $"/{ApiRoutePrefix}/core";
    }

    public static class Services
    {
        public const string AccountsApi = "AdminAssistant-AccountsApi";
        public const string AdminApi = "AdminAssistant-AdminApi";
        public const string AssetRegisterApi = "AdminAssistant-AssetRegisterApi";
        public const string BudgetApi = "AdminAssistant-BudgetApi";
        public const string CalendarApi = "AdminAssistant-CalendarApi";
        public const string ContactsApi = "AdminAssistant-ContactsApi";
        public const string CoreApi = "AdminAssistant-CoreApi";
        public const string DocumentsApi = "AdminAssistant-DocumentsApi";
        public const string MailApi = "AdminAssistant-MailApi";
        public const string NotesApi = "AdminAssistant-NotesApi";
        public const string ScheduledPaymentsApi = "AdminAssistant-ScheduledPaymentsApi";
        public const string TasksApi = "AdminAssistant-TasksApi";

        public const string ScheduledJobHost = "AdminAssistant-ScheduledJobHost";

        public const string Gateway = "AdminAssistant-Gateway";
    }
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
    /// The max length of a user sign-on.
    /// </summary>
    /// <remarks>
    /// For example a windows login or online account user name
    /// </remarks>
    public const int UserSignOnMaxLength = 50;

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
