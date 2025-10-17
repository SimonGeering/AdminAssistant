#pragma warning disable CA1707 // Identifiers should not contain underscores
using AdminAssistant.Domain;
using AdminAssistant.Modules.AccountsModule;
using AdminAssistant.Modules.AccountsModule.Queries;
using AdminAssistant.Shared;
using AdminAssistant.WebAPI.v1.AccountsModule;
using Microsoft.AspNetCore.Mvc;
using MappingProfile = AdminAssistant.WebAPI.v1.MappingProfile;

namespace AdminAssistant.Test.WebAPI.v1.AccountsModule;

public sealed class BankAccountInfoController_BankAccountInfoGet_Should
{
    [Fact]
    [Trait("Category", "Unit")]
    public async Task Return_Status200OK_With_AListOfBankAccountInfo_Given_NoArguments()
    {
        // Arrange
        var bankAccountInfoList = new List<BankAccountInfo>()
            {
                Factory.BankAccountInfo.WithTestData(10).Build(),
                Factory.BankAccountInfo.WithTestData(20).Build()
            };

        var mockMediator = new Mock<IMediator>();
        mockMediator.Setup(x => x.Send(It.IsAny<BankAccountInfoQuery>(), It.IsAny<CancellationToken>()))
                    .Returns(ValueTask.FromResult(Result<IEnumerable<BankAccountInfo>>.Success(bankAccountInfoList)));

        var mockUserContextProvider = new Mock<IUserContextProvider>();
        mockUserContextProvider.Setup(x => x.GetCurrentUser())
                               .Returns(new User() { UserID = 10, SignOn = "simongeering" });

        var services = new ServiceCollection();
        services.AddMockServerSideLogging();
        services.AddAutoMapper(typeof(MappingProfile));
        services.AddTransient((sp) => mockMediator.Object);
        services.AddTransient((sp) => mockUserContextProvider.Object);
        services.AddTransient<BankAccountInfoController>();

        // Act
        var response = await services.BuildServiceProvider().GetRequiredService<BankAccountInfoController>().BankAccountInfoGet(default);

        // Assert
        response.Value.Should().BeNull();

        response.Result.Should().NotBeNull();
        var result = (OkObjectResult)response.Result!;
        result.Value.Should().BeAssignableTo<IEnumerable<BankAccountInfoResponseDto>>();

        result.Value.Should().NotBeNull();
        var value = ((IEnumerable<BankAccountInfoResponseDto>)result.Value!).ToArray();
        value.Should().HaveCount(bankAccountInfoList.Count);

        var expected = bankAccountInfoList.ToArray();
        for (var index = 0; index < expected.Length; index++)
        {
            value[index].BankAccountID.Should().Be(expected[index].BankAccountID.Value);
            value[index].AccountName.Should().Be(expected[index].AccountName);
        }
    }
}
#pragma warning restore CA1707 // Identifiers should not contain underscores
