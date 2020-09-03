namespace AdminAssistant.DomainModel.Modules.MailModule.Builders
{
    public interface IMailMessageBuilder
    {
        MailMessage Build();
        IMailMessageBuilder WithTestData(int assetID = Constants.UnknownRecordID);
        IMailMessageBuilder WithSubject(string subject);
    }
    internal class MailMessageBuilder : MailMessage, IMailMessageBuilder
    {
        public static MailMessage Default(IMailMessageBuilder builder) => builder.Build();
        public static MailMessage Default(MailMessageBuilder builder) => builder.Build();

        public MailMessage Build() => this;

        public IMailMessageBuilder WithTestData(int assetID = Constants.UnknownRecordID)
        {
            MailMessageID = assetID;
            Subject = "A mail from the boss";
            return this;
        }
        public IMailMessageBuilder WithSubject(string subject)
        {
            Subject = subject;
            return this;
        }
    }
}
