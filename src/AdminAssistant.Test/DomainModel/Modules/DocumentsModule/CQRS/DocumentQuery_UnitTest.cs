#pragma warning disable CA1707 // Identifiers should not contain underscores

using AdminAssistant.Domain;
using AdminAssistant.Infra.DAL.Modules.DocumentsModule;
using AdminAssistant.Modules.DocumentsModule;
using AdminAssistant.Modules.DocumentsModule.Queries;

namespace AdminAssistant.Test.DomainModel.Modules.DocumentsModule.CQRS;

public sealed class DocumentQuery_Should
{
    [Fact]
    [Trait("Category", "Unit")]
    public async Task Return_BankList()
    {
        // Arrange
        var documentList = new List<Document>()
            {
                Factory.Document.WithTestData(10).Build(),
                Factory.Document.WithTestData(20).Build()
            };

        var services = new ServiceCollection();
        services.AddMockServerSideLogging();
        services.AddAdminAssistantServerSideDomainModel();
        services.AddAdminAssistantApplication();

        var mockRepository = new Mock<IDocumentRepository>();
        mockRepository.Setup(x => x.GetListAsync(It.IsAny<CancellationToken>()))
            .Returns(Task.FromResult(documentList));

        services.AddTransient((sp) => mockRepository.Object);

        // Act
        var result = await services.BuildServiceProvider().GetRequiredService<IMediator>().Send(new DocumentQuery());

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().BeEquivalentTo(documentList);
    }
}
#pragma warning restore CA1707 // Identifiers should not contain underscores
