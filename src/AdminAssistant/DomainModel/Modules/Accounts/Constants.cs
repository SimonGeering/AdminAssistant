namespace AdminAssistant.DomainModel.Modules.Accounts
{
    public static class Constants
    {
        public const int UnknownRecordID = default;
        public const int NewRecordID = default;

        public const int KeyMaxLength = 20;
        public const int NameMaxLength = 50;
        public const int UserSignOnMaxLength = 50;
        public const int DescriptionMaxLength = 255;
        public const int NotesMaxLength = 4000;

        // TODO: Lookup what the max length of an ID is this is in the MS Graph API docs.
        public const int MSGraphIDMaxLength = 50;

        public const string DateFormat = "dd-MMM-yyyy";
    }
}
