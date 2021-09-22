#pragma warning disable CA1707 // Identifiers should not contain underscores
using AdminAssistant.DomainModel;
using AdminAssistant.DomainModel.Modules.AccountsModule;
using AdminAssistant.DomainModel.Modules.AccountsModule.CQRS;
using Ardalis.Result;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Xunit;

namespace AdminAssistant.WebAPI.v1.AccountsModule;

public class BankAccountTypeController_BankAccountTypeGet_Should
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
        services.AddAutoMapper(typeof(MappingProfile));

        var mockMediator = new Mock<IMediator>();
        mockMediator.Setup(x => x.Send(It.IsAny<BankAccountTypesQuery>(), It.IsAny<CancellationToken>()))
                    .Returns(Task.FromResult(Result<IEnumerable<BankAccountType>>.Success(bankAccountTypes)));

        services.AddTransient((sp) => mockMediator.Object);
        services.AddTransient<BankAccountTypeController>();

        // Act
        var response = await services.BuildServiceProvider().GetRequiredService<BankAccountTypeController>().BankAccountTypeGet().ConfigureAwait(false);

        // Assert
        response.Value.Should().BeNull();
        response.Result.Should().NotBeNull();
        response.Result.Should().BeOfType<OkObjectResult>();

        var result = (OkObjectResult)response.Result!;
        result.Value.Should().BeAssignableTo<IEnumerable<BankAccountTypeResponseDto>>();

        result.Value.Should().NotBeNull();
        var value = ((IEnumerable<BankAccountTypeResponseDto>)result.Value!).ToArray();
        value.Should().HaveCount(bankAccountTypes.Count);

        var expected = bankAccountTypes.ToArray();
        for (var index = 0; index < expected.Length; index++)
        {
            value[index].BankAccountTypeID.Should().Be(expected[index].BankAccountTypeID);
            value[index].Description.Should().Be(expected[index].Description);
        }
    }
}
#pragma warning restore CA1707 // Identifiers should not contain underscores
