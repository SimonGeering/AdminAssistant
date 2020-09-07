using System;

namespace AdminAssistant.DomainModel.Modules.DocumentsModule.Builders
{
    public interface IDocumentBuilder
    {
        Document Build();
        IDocumentBuilder WithTestData(int documentID = Constants.UnknownRecordID);
        IDocumentBuilder WithUri(Uri uri);
    }
    internal class DocumentBuilder : Document, IDocumentBuilder
    {
        public static Document Default(IDocumentBuilder builder) => builder.Build();
        public static Document Default(DocumentBuilder builder) => builder.Build();

        public Document Build() => this;

        public IDocumentBuilder WithTestData(int documentID = Constants.UnknownRecordID)
        {
            DocumentID = documentID;
            Uri = new Uri("SomRandomFileName.txt");
            return this;
        }
        public IDocumentBuilder WithUri(Uri uri)
        {
            Uri = uri;
            return this;
        }
    }
}
