namespace AdminAssistant.Modules.MailModule.Builders;

public interface IMailMessageBuilder
{
    MailMessage Build();
    IMailMessageBuilder WithTestData(int assetID = Constants.UnknownRecordID);
    IMailMessageBuilder WithSubject(string subject);
}
internal sealed class MailMessageBuilder : IMailMessageBuilder
{
    private MailMessage _mailMessage = new();

    public static MailMessage Default(IMailMessageBuilder builder) => builder.Build();
    public static MailMessage Default(MailMessageBuilder builder) => builder.Build();

    public MailMessage Build() => _mailMessage;

    public IMailMessageBuilder WithTestData(int assetID = Constants.UnknownRecordID)
    {
        _mailMessage = _mailMessage with
        {
            MailMessageID = new(assetID),
            Subject = "A mail from the boss"
        };
        return this;
    }

    public IMailMessageBuilder WithSubject(string subject)
    {
        _mailMessage = _mailMessage with { Subject = subject };
        return this;
    }
}
