using AdminAssistant.DAL.EntityFramework.Model.Documents;
using Microsoft.EntityFrameworkCore;

namespace AdminAssistant.DAL.EntityFramework.Model
{
    internal class DocumentsSchema
    {
        private const string Name = "Documents";

        internal static void OnModelCreating(ModelBuilder modelBuilder)
        {
            DocumentsSchema.Document_OnModelCreating(modelBuilder);
            // TODO: DocumentsSchema.OnModelCreating
        }

        private static void Document_OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DocumentEntity>().ToTable("Document").Metadata.SetSchema(DocumentsSchema.Name);
            modelBuilder.Entity<DocumentEntity>().HasKey(x => x.DocumentID);
            // TODO: Document_OnModelCreating
        }
    }
}
