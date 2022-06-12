#pragma warning disable CA1707 // Identifiers should not contain underscores
using AdminAssistant.DomainModel;
using AdminAssistant.DomainModel.Modules.DocumentsModule;
using AdminAssistant.DomainModel.Modules.DocumentsModule.CQRS;
using AdminAssistant.WebAPI.v1;
using AdminAssistant.WebAPI.v1.DocumentsModule;
using Microsoft.AspNetCore.Mvc;

namespace AdminAssistant.Test.WebAPI.v1.DocumentsModule;

public class DocumentController_UnitTest_Should
{
    [Fact]
    [Trait("Category", "Unit")]
    public async Task Return_Status200OK_With_AListOfDocuments_Given_NoArguments()
    {
        // Arrange
        var documents = new List<Document>()
            {
                Factory.Document.WithTestData(10).Build(),
                Factory.Document.WithTestData(20).Build()
            };

        var services = new ServiceCollection();
        services.AddMockServerSideLogging();
        services.AddAutoMapper(typeof(MappingProfile));

        var mockMediator = new Mock<IMediator>();
        mockMediator.Setup(x => x.Send(It.IsAny<DocumentQuery>(), It.IsAny<CancellationToken>()))
                    .Returns(Task.FromResult(Result<IEnumerable<Document>>.Success(documents)));

        services.AddTransient((sp) => mockMediator.Object);
        services.AddTransient<AdminAssistant.WebAPI.v1.DocumentsModule.DocumentController>();

        // Act
        var response = await services.BuildServiceProvider().GetRequiredService<DocumentController>().GetDocuments().ConfigureAwait(false);

        // Assert
        response.Value.Should().BeNull();
        response.Result.Should().NotBeNull();
        response.Result.Should().BeOfType<OkObjectResult>();

        var result = (OkObjectResult)response.Result!;
        result.Value.Should().BeAssignableTo<IEnumerable<DocumentResponseDto>>();

        //var value = ((IEnumerable<CurrencyResponseDto>)result.Value).ToArray();
        //value.Should().HaveCount(currencies.Count);

        //var expected = currencies.ToArray();
        //for (int i = 0; i < expected.Length; i++)
        //{
        //    value[i].CurrencyID.Should().Be(expected[i].CurrencyID);
        //    value[i].Symbol.Should().Be(expected[i].Symbol);
        //    value[i].DecimalFormat.Should().Be(expected[i].DecimalFormat);
        //}
    }
}
#pragma warning restore CA1707 // Identifiers should not contain underscores
