namespace AdminAssistant.DomainModel.Modules.DocumentsModule.Builders
{
    public interface IDocumentBuilder
    {
        Document Build();
        IDocumentBuilder WithTestData(int documentID = Constants.UnknownRecordID);
        IDocumentBuilder WithFileName(string fileName);
    }
    internal class DocumentBuilder : Document, IDocumentBuilder
    {
        public static Document Default(IDocumentBuilder builder) => builder.Build();
        public static Document Default(DocumentBuilder builder) => builder.Build();

        public Document Build() => this;

        public IDocumentBuilder WithTestData(int documentID = Constants.UnknownRecordID)
        {
            DocumentID = documentID;
            FileName = "SomRandomFileName.txt";
            return this;
        }
        public IDocumentBuilder WithFileName(string fileName)
        {
            FileName = fileName;
            return this;
        }
    }
}
