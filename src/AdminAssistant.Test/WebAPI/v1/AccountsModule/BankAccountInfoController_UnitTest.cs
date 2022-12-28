#pragma warning disable CA1707 // Identifiers should not contain underscores
using AdminAssistant.DomainModel;
using AdminAssistant.DomainModel.Modules.AccountsModule;
using AdminAssistant.DomainModel.Modules.AccountsModule.CQRS;
using AdminAssistant.DomainModel.Shared;
using AdminAssistant.WebAPI.v1;
using AdminAssistant.WebAPI.v1.AccountsModule;
using Microsoft.AspNetCore.Mvc;

namespace AdminAssistant.Test.WebAPI.v1.AccountsModule;

public class BankAccountInfoController_BankAccountInfoGet_Should
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
                    .Returns(Task.FromResult(Result<IEnumerable<BankAccountInfo>>.Success(bankAccountInfoList)));

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
        var response = await services.BuildServiceProvider().GetRequiredService<BankAccountInfoController>().BankAccountInfoGet().ConfigureAwait(false);

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
            value[index].BankAccountID.Should().Be(expected[index].BankAccountID);
            value[index].AccountName.Should().Be(expected[index].AccountName);
        }
    }
}
#pragma warning restore CA1707 // Identifiers should not contain underscores
