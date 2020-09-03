using AdminAssistant.Infra.DAL.EntityFramework.Model.Documents;
using Microsoft.EntityFrameworkCore;

namespace AdminAssistant.Infra.DAL.EntityFramework.Model
{
    internal class DocumentsSchema
    {
        private const string Name = "Documents";

        // TODO: DocumentsSchema.OnModelCreating
        internal static void OnModelCreating(ModelBuilder modelBuilder) => Document_OnModelCreating(modelBuilder);

        private static void Document_OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DocumentEntity>().ToTable("Document").Metadata.SetSchema(Name);
            modelBuilder.Entity<DocumentEntity>().HasKey(x => x.DocumentID);
            // TODO: Document_OnModelCreating
        }
    }
}
