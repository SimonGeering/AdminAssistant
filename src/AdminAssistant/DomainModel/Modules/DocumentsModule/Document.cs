using System;
using AdminAssistant.Infra.DAL;

namespace AdminAssistant.DomainModel.Modules.DocumentsModule
{
    public class Document : IDatabasePersistable
    {
        public const int URIMaxLength = 255;

        public int DocumentID { get; set; }

        public Uri? Uri { get; set; }

        public int PrimaryKey => DocumentID;
    }
}
