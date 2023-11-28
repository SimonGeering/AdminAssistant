namespace AdminAssistant.DomainModel.Modules.MailModule;

public sealed record MailMessage : IDatabasePersistable
{
    public const int SubjectNameMaxLength = Constants.DescriptionMaxLength;

    public MailMessageId MailMessageID { get; set; } = MailMessageId.Default;
    public string Subject { get; set; } = string.Empty;

    public Id PrimaryKey => MailMessageID;
}
public sealed record MailMessageId(int Value) : Id(Value)
{
    public static MailMessageId Default => new(Constants.UnknownRecordID);
}
