namespace AdminAssistant.DomainModel.Modules.DocumentsModule.Builders;

internal sealed class DocumentBuilder : IDocumentBuilder
{
    private Document _document = new();

    public static Document Default(IDocumentBuilder builder) => builder.Build();
    public static Document Default(DocumentBuilder builder) => builder.Build();

    public Document Build() => _document;

    public IDocumentBuilder WithTestData(int documentID = Constants.UnknownRecordID)
    {
        _document = _document with
        {
            DocumentID = new DocumentId(documentID),
            FileName = "SomRandomFileName.txt"
        };
        return this;
    }

    public IDocumentBuilder WithFileName(string fileName)
    {
        _document = _document with { FileName = fileName };
        return this;
    }
}
