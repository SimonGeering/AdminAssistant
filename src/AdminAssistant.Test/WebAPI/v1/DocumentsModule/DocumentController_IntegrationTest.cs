#if DEBUG // quick and dirty fix for #85 category filtering breaking CI Unit Test run.
#pragma warning disable CA1707 // Identifiers should not contain underscores
using AdminAssistant.DomainModel.Modules.DocumentsModule;
using AdminAssistant.Infra.DAL.Modules.DocumentsModule;
using AdminAssistant.UI.Shared.WebAPIClient.v1;

namespace AdminAssistant.Test.WebAPI.v1.DocumentsModule;

[Collection("SequentialDBBackedTests")]
public sealed class Document_Get_Should : IntegrationTestBase
{
    [Fact]
    [Trait("Category", "Integration")]
    public async Task Return_AllDocuments_Given_NoParameters()
    {
        // Arrange
        await ResetDatabaseAsync();

        var dal = Container.GetRequiredService<IDocumentRepository>();
        await dal.SaveAsync(new Document() { FileName = "TestFileName.txt" });
        await dal.SaveAsync(new Document() { FileName = "TestFileTwo.pdf" });

        // Act
        var response = await Container.GetRequiredService<IAdminAssistantWebAPIClient>().GetDocumentAsync();

        // Assert
        response.Should().HaveCount(2);
        response.Should().ContainSingle(x => x.FileName == "TestFileName.txt");
        response.Should().ContainSingle(x => x.FileName == "TestFileTwo.pdf");
    }
}
#pragma warning restore CA1707 // Identifiers should not contain underscores
#endif // quick and dirty fix for #85 category filtering breaking CI Unit Test run.
