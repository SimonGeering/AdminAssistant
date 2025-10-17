#pragma warning disable CA1707 // Identifiers should not contain underscores
using AdminAssistant.Modules.DocumentsModule;
using AdminAssistant.Modules.DocumentsModule.UI;
using AdminAssistant.UI.Shared.WebAPIClient.v1;

namespace AdminAssistant.Test.UI.Modules.DocumentsModule;

public sealed class DocumentsService_GetDocumentListAsync
{
    [Fact]
    [Trait("Category", "Unit")]
    public async Task ReturnAListOfDocuments_GivenNoArguments()
    {
        // Arrange
        ICollection<DocumentResponseDto> documentsList = new List<DocumentResponseDto>()
            {
                new DocumentResponseDto { DocumentID = 1, FileName = "test.pdf" },
                new DocumentResponseDto { DocumentID = 2, FileName = "test2.docx" },
            };

        var mockWebAPIClient = new Mock<IAdminAssistantWebAPIClient>();
        mockWebAPIClient.Setup(x => x.GetDocumentAsync())
            .Returns(Task.FromResult(documentsList));

        var services = new ServiceCollection();
        services.AddAdminAssistantUI();
        services.AddMockClientSideLogging();
        services.AddAutoMapper(typeof(MappingProfile));
        services.AddTransient((sp) => mockWebAPIClient.Object);

        // Act
        var result = await services.BuildServiceProvider().GetRequiredService<IDocumentsService>().GetDocumentListAsync();

        // Assert
        result.ShouldBeEquivalentTo(new List<Document>()
            {
                new Document { DocumentID = new(1), FileName = "test.pdf" },
                new Document { DocumentID = new(2), FileName = "test2.docx" },
            });
    }
}
#pragma warning restore CA1707 // Identifiers should not contain underscores
