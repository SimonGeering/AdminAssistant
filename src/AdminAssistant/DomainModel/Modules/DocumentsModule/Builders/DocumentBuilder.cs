namespace AdminAssistant.DomainModel.Modules.DocumentsModule.Builders
{
    public interface IDocumentBuilder
    {
        Document Build();
        IDocumentBuilder WithTestData(int documentID = Constants.UnknownRecordID);
        IDocumentBuilder WithFileName(string fileName);
    }
    internal class DocumentBuilder : IDocumentBuilder
    {
        private Document _document = new Document();

        public static Document Default(IDocumentBuilder builder) => builder.Build();
        public static Document Default(DocumentBuilder builder) => builder.Build();

        public Document Build() => _document;

        public IDocumentBuilder WithTestData(int documentID = Constants.UnknownRecordID)
        {
            _document = _document with
            {
                DocumentID = documentID,
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
}
