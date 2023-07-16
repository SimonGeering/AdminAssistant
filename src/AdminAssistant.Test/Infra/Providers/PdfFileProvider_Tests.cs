#if DEBUG // quick and dirty fix for #85 category filtering breaking CI Unit Test run.
#pragma warning disable CA1707 // Identifiers should not contain underscores
using AdminAssistant.Infra.Providers;

namespace AdminAssistant.Test.Infra.Providers;

public sealed class PdfFileProvider_ReadAllLinesAsync_Should
{
    //[Fact(Skip="WIP")]
    [Fact]
    [Trait("Category", "Manual")]
    public async Task ReturnMultipleLines_WhenReadingFromAValidLocalTestFile()
    {
        // Arrange
        var services = new ServiceCollection();
        services.AddAdminAssistantClientSideProviders();

        // Read test data file content ..
        var pdfFilePath = Path.Combine(Directory.GetCurrentDirectory(), "_TestData", "ConfidentialTestData", "BankAccountStatement.pdf");
        var fileContent = await File.ReadAllBytesAsync(pdfFilePath);

        // Act
        var result = await services.BuildServiceProvider().GetRequiredService<IPdfFileProvider>().ReadAllLinesAsync(fileContent).ConfigureAwait(false);

        // Assert
        result.Should().NotBeEmpty();
    }
}
#pragma warning restore CA1707 // Identifiers should not contain underscores
#endif // quick and dirty fix for #85 category filtering breaking CI Unit Test run.
