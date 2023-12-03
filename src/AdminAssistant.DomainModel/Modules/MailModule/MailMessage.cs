using AdminAssistant.Shared;

namespace AdminAssistant.Modules.MailModule;

public sealed record MailMessage : IPersistable
{
    public const int SubjectNameMaxLength = EntityDescription.MaxLength;

    public MailMessageId MailMessageID { get; set; } = MailMessageId.Default;
    public string Subject { get; set; } = string.Empty;

    public Id PrimaryKey => MailMessageID;
}
public sealed record MailMessageId(int Value) : Id(Value)
{
    public static MailMessageId Default => new(Constants.UnknownRecordID);
}
