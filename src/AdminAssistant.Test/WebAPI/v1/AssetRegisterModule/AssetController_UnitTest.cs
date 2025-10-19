// ReSharper disable InconsistentNaming
#pragma warning disable CA1707 // Identifiers should not contain underscores

using AdminAssistant.Domain;
using AdminAssistant.Modules.AssetRegisterModule;
using AdminAssistant.Modules.AssetRegisterModule.Queries;
using AdminAssistant.WebAPI.v1.AssetRegisterModule;
using Microsoft.AspNetCore.Mvc;

namespace AdminAssistant.Test.WebAPI.v1.AssetRegisterModule;

public sealed class AssetController_GetAssets
{
    [Fact]
    [Trait("Category", "Unit")]
    [SuppressMessage("Major Code Smell", "S125:Sections of code should not be commented out", Justification = "WIP")]
    public async Task Returns_Status200OK_With_AListOfAssets_Given_NoArguments()
    {
        // Arrange
        var documents = new List<Asset>()
            {
                Factory.Asset.WithTestData(10).Build(),
                Factory.Asset.WithTestData(20).Build()
            };

        var services = new ServiceCollection();
        services.AddMockServerSideLogging();

        var mockMediator = new Mock<IMediator>();
        mockMediator.Setup(x => x.Send(It.IsAny<AssetQuery>(), It.IsAny<CancellationToken>()))
                    .Returns(ValueTask.FromResult(Result<IEnumerable<Asset>>.Success(documents)));

        services.AddTransient(_ => mockMediator.Object);
        services.AddTransient<AssetController>();

        // Act
        var response = await services.BuildServiceProvider().GetRequiredService<AssetController>().GetAssets(CancellationToken.None);

        // Assert
        response.Value.ShouldBeNull();
        response.Result.ShouldNotBeNull();
        response.Result.ShouldBeOfType<OkObjectResult>();

        var result = (OkObjectResult)response.Result!;
        result.Value.ShouldBeAssignableTo<IEnumerable<AssetResponseDto>>();

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
#pragma warning restore S125 // Sections of code should not be commented out
}
#pragma warning restore CA1707 // Identifiers should not contain underscores
