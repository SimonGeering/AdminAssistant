namespace AdminAssistant.DomainModel.Modules.MailModule.Builders;

public interface IMailMessageBuilder
{
    MailMessage Build();
    IMailMessageBuilder WithTestData(int assetID = Constants.UnknownRecordID);
    IMailMessageBuilder WithSubject(string subject);
}
