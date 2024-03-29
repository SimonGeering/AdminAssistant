using AdminAssistant.Modules.DocumentsModule;
using AdminAssistant.Infrastructure.EntityFramework.Model.Documents;
using Microsoft.EntityFrameworkCore;

namespace AdminAssistant.Infrastructure.EntityFramework.Model;

internal static class DocumentsSchema
{
    private const string Name = "Documents";

    // TODO: DocumentsSchema.OnModelCreating
    internal static void OnModelCreating(ModelBuilder modelBuilder) => Document_OnModelCreating(modelBuilder);

    private static void Document_OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DocumentEntity>().ToTable("Document").Metadata.SetSchema(Name);
        modelBuilder.Entity<DocumentEntity>().HasKey(x => x.DocumentID);
        modelBuilder.Entity<DocumentEntity>().Property(x => x.FileName).IsRequired().IsUnicode().HasMaxLength(Document.FileNameMaxLength);
        // TODO: Document_OnModelCreating
    }
}
