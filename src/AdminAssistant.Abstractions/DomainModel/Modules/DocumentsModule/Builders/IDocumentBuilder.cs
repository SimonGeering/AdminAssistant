namespace AdminAssistant.DomainModel.Modules.DocumentsModule.Builders;

public interface IDocumentBuilder
{
    Document Build();
    IDocumentBuilder WithTestData(int documentID = Constants.UnknownRecordID);
    IDocumentBuilder WithFileName(string fileName);
}
