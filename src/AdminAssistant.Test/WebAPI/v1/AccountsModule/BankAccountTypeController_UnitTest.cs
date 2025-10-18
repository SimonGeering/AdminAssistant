// ReSharper disable InconsistentNaming
#pragma warning disable CA1707 // Identifiers should not contain underscores

using AdminAssistant.Domain;
using AdminAssistant.Modules.AccountsModule;
using AdminAssistant.Modules.AccountsModule.Queries;
using AdminAssistant.WebAPI.v1.AccountsModule;
using Microsoft.AspNetCore.Mvc;

namespace AdminAssistant.Test.WebAPI.v1.AccountsModule;

public sealed class BankAccountTypeController_BankAccountTypeGet_Should
{
    [Fact]
    [Trait("Category", "Unit")]
    public async Task Return_Status200OK_With_AListOfBankAccountType_Given_NoArguments()
    {
        // Arrange
        var bankAccountTypes = new List<BankAccountType>()
            {
                Factory.BankAccountType.WithTestData(10).WithDescription("Current Account").Build(),
                Factory.BankAccountType.WithTestData(20).WithDescription("Savings Account").Build(),
            };

        var services = new ServiceCollection();
        services.AddMockServerSideLogging();

        var mockMediator = new Mock<IMediator>();
        mockMediator.Setup(x => x.Send(It.IsAny<BankAccountTypesQuery>(), It.IsAny<CancellationToken>()))
                    .Returns(ValueTask.FromResult(Result<IEnumerable<BankAccountType>>.Success(bankAccountTypes)));

        services.AddTransient(_ => mockMediator.Object);
        services.AddTransient<BankAccountTypeController>();

        // Act
        var response = await services.BuildServiceProvider().GetRequiredService<BankAccountTypeController>().BankAccountTypeGet(CancellationToken.None);

        // Assert
        response.Value.ShouldBeNull();
        response.Result.ShouldNotBeNull();
        response.Result.ShouldBeOfType<OkObjectResult>();

        var result = (OkObjectResult)response.Result!;
        result.Value.ShouldBeAssignableTo<IEnumerable<BankAccountTypeResponseDto>>();

        result.Value.ShouldNotBeNull();
        var value = ((IEnumerable<BankAccountTypeResponseDto>)result.Value!).ToArray();
        value.Length.ShouldBe(bankAccountTypes.Count);

        var expected = bankAccountTypes.ToArray();
        for (var index = 0; index < expected.Length; index++)
        {
            value[index].BankAccountTypeID.ShouldBe(expected[index].BankAccountTypeID.Value);
            value[index].Description.ShouldBe(expected[index].Description);
        }
    }
}
#pragma warning restore CA1707 // Identifiers should not contain underscores
