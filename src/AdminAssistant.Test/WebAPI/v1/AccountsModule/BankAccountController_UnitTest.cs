// ReSharper disable InconsistentNaming
#pragma warning disable CA1707 // Identifiers should not contain underscores

using AdminAssistant.Domain;
using AdminAssistant.Modules.AccountsModule;
using AdminAssistant.Modules.AccountsModule.Commands;
using AdminAssistant.Modules.AccountsModule.Queries;
using AdminAssistant.WebAPI.v1.AccountsModule;
using Microsoft.AspNetCore.Mvc;
using MappingProfile = AdminAssistant.WebAPI.v1.MappingProfile;

namespace AdminAssistant.Test.WebAPI.v1.AccountsModule;

public sealed class BankAccountController_Put_Should
{
    [Fact]
    [Trait("Category", "Unit")]
    public async Task Return_Status200OK_Given_AValidBankAccount()
    {
        // Arrange
        var bankAccount = Factory.BankAccount.WithTestData(10).Build();

        var mockMediator = new Mock<IMediator>();
        mockMediator.Setup(x => x.Send(It.IsAny<BankAccountUpdateCommand>(), It.IsAny<CancellationToken>()))
                    .Returns(ValueTask.FromResult(Result<BankAccount>.Success(bankAccount)));

        var services = new ServiceCollection();
        services.AddMockServerSideLogging();
        services.AddAutoMapper(typeof(MappingProfile));
        services.AddTransient(_ => mockMediator.Object);
        services.AddTransient<BankAccountController>();

        var container = services.BuildServiceProvider();
        var bankAccountRequest = new BankAccountUpdateRequestDto()
        {
            BankAccountTypeID = bankAccount.BankAccountTypeID.Value,
            CurrencyID = bankAccount.CurrencyID.Value,
            AccountName = bankAccount.AccountName,
            IsBudgeted = bankAccount.IsBudgeted,
            OpeningBalance = bankAccount.OpeningBalance,
            CurrentBalance = bankAccount.CurrentBalance,
            OpenedOn = bankAccount.OpenedOn
        };

        // Act
        var response = await container.GetRequiredService<BankAccountController>().BankAccountPut(bankAccountRequest, CancellationToken.None);

        // Assert
        response.Value.ShouldBeNull();
        response.Result.ShouldNotBeNull();
        response.Result.ShouldBeOfType<OkObjectResult>();

        var result = (OkObjectResult)response.Result!;
        result.Value.ShouldNotBeNull();
        result.Value.ShouldBeAssignableTo<BankAccountResponseDto>();

        var value = (BankAccountResponseDto)result.Value!;
        value.BankAccountID.ShouldBe(bankAccount.BankAccountID.Value);
        value.BankAccountTypeID.ShouldBe(bankAccount.BankAccountTypeID.Value);
        value.CurrencyID.ShouldBe(bankAccount.CurrencyID.Value);
        value.AccountName.ShouldBe(bankAccount.AccountName);
        value.IsBudgeted.ShouldBe(bankAccount.IsBudgeted);
        value.OpeningBalance.ShouldBe(bankAccount.OpeningBalance);
        value.CurrentBalance.ShouldBe(bankAccount.CurrentBalance);
        value.OpenedOn.ShouldBe(bankAccount.OpenedOn);
    }

    [Fact]
    [Trait("Category", "Unit")]
    public async Task Return_Status404NotFound_Given_ANonExistentBankAccountID()
    {
        // Arrange
        var bankAccount = Factory.BankAccount.WithTestData(10).Build();

        var mockMediator = new Mock<IMediator>();
        mockMediator.Setup(x => x.Send(It.IsAny<BankAccountUpdateCommand>(), It.IsAny<CancellationToken>()))
                    .Returns(ValueTask.FromResult(Result<BankAccount>.NotFound()));

        var services = new ServiceCollection();
        services.AddMocksOfExternalServerSideDependencies();
        services.AddTransient(_ => mockMediator.Object);
        services.AddTransient<BankAccountController>();

        var bankAccountRequest = bankAccount.ToBankAccountUpdateRequestDto();

        // Act
        var response = await services.BuildServiceProvider().GetRequiredService<BankAccountController>().BankAccountPut(bankAccountRequest, CancellationToken.None);

        // Assert
        response.Result.ShouldBeOfType<NotFoundObjectResult>();
        response.Value.ShouldBeNull();
    }

    [Fact]
    [Trait("Category", "Unit")]
    public async Task Return_Status422UnprocessableEntity_Given_AnInvalidBankAccount()
    {
        // Arrange
        var validationErrors = new List<ValidationError>()
            {
                new() { Identifier="ExampleErrorCode", ErrorMessage="ExampleErrorMessage", Severity=ValidationSeverity.Error },
                new() { Identifier="ExampleErrorCode2", ErrorMessage="ExampleErrorMessage2", Severity=ValidationSeverity.Error }
            };
        var bankAccount = Factory.BankAccount.WithTestData(10).Build();

        var mockMediator = new Mock<IMediator>();
        mockMediator.Setup(x => x.Send(It.IsAny<BankAccountUpdateCommand>(), It.IsAny<CancellationToken>()))
                    .Returns(ValueTask.FromResult(Result<BankAccount>.Invalid(validationErrors)));

        var services = new ServiceCollection();
        services.AddMocksOfExternalServerSideDependencies();
        services.AddTransient(_ => mockMediator.Object);
        services.AddTransient<BankAccountController>();

        var bankAccountRequest = bankAccount.ToBankAccountUpdateRequestDto();

        // Act
        var response = await services.BuildServiceProvider().GetRequiredService<BankAccountController>().BankAccountPut(bankAccountRequest, CancellationToken.None);

        // Assert
        response.Value.ShouldBeNull();
        response.Result.ShouldNotBeNull();
        response.Result.ShouldBeOfType<UnprocessableEntityObjectResult>();

        var result = (UnprocessableEntityObjectResult)response.Result!;
        result.Value.ShouldNotBeNull();
        var errors = (SerializableError)result.Value!;

        foreach (var expectedErrorDetails in validationErrors)
        {
            var messages = (string[])errors[expectedErrorDetails.Identifier];
            messages.ShouldContain(expectedErrorDetails.ErrorMessage);
        }
    }
}

public sealed class BankAccountController_BankAccountPost_Should
{
    [Fact]
    [Trait("Category", "Unit")]
    public async Task Return_Status201CreatedAtRoute_Given_AValidBank()
    {
        // Arrange
        var bankAccount = Factory.BankAccount.WithTestData(10).Build();

        var mockMediator = new Mock<IMediator>();
        mockMediator.Setup(x => x.Send(It.IsAny<BankAccountCreateCommand>(), It.IsAny<CancellationToken>()))
                    .Returns(ValueTask.FromResult(Result<BankAccount>.Success(bankAccount)));

        var services = new ServiceCollection();
        services.AddMockServerSideLogging();
        services.AddAutoMapper(typeof(MappingProfile));
        services.AddTransient(_ => mockMediator.Object);
        services.AddTransient<BankAccountController>();

        var container = services.BuildServiceProvider();

        var bankAccountRequest = new BankAccountCreateRequestDto()
        {
            BankAccountTypeID = bankAccount.BankAccountTypeID.Value,
            CurrencyID = bankAccount.CurrencyID.Value,
            AccountName = bankAccount.AccountName,
            IsBudgeted = bankAccount.IsBudgeted,
            OpeningBalance = bankAccount.OpeningBalance,
            CurrentBalance = bankAccount.CurrentBalance,
            OpenedOn = bankAccount.OpenedOn
        };

        // Act
        var response = await container.GetRequiredService<BankAccountController>().BankAccountPost(bankAccountRequest, CancellationToken.None);

        // Assert
        response.Value.ShouldBeNull();
        response.Result.ShouldNotBeNull();
        response.Result.ShouldBeOfType<CreatedAtRouteResult>();

        var result = (CreatedAtRouteResult)response.Result!;
        result.Value.ShouldNotBeNull();
        result.Value.ShouldBeAssignableTo<BankAccountResponseDto>();

        var value = (BankAccountResponseDto)result.Value!;
        value.BankAccountID.ShouldBe(bankAccount.BankAccountID.Value);
        value.BankAccountTypeID.ShouldBe(bankAccount.BankAccountTypeID.Value);
        value.CurrencyID.ShouldBe(bankAccount.CurrencyID.Value);
        value.AccountName.ShouldBe(bankAccount.AccountName);
        value.IsBudgeted.ShouldBe(bankAccount.IsBudgeted);
        value.OpeningBalance.ShouldBe(bankAccount.OpeningBalance);
        value.CurrentBalance.ShouldBe(bankAccount.CurrentBalance);
        value.OpenedOn.ShouldBe(bankAccount.OpenedOn);
    }

[Fact]
    [Trait("Category", "Unit")]
    public async Task Return_Status422UnprocessableEntity_Given_AnInvalidBankAccount()
    {
        // Arrange
        var validationErrors = new List<ValidationError>()
            {
                new() { Identifier="ExampleErrorCode", ErrorMessage="ExampleErrorMessage", Severity=ValidationSeverity.Error },
                new() { Identifier="ExampleErrorCode2", ErrorMessage="ExampleErrorMessage2", Severity=ValidationSeverity.Error }
            };
        var bankAccount = Factory.BankAccount.WithTestData(10).Build();

        var mockMediator = new Mock<IMediator>();
        mockMediator.Setup(x => x.Send(It.IsAny<BankAccountCreateCommand>(), It.IsAny<CancellationToken>()))
                    .Returns(ValueTask.FromResult(Result<BankAccount>.Invalid(validationErrors)));

        var services = new ServiceCollection();
        services.AddMocksOfExternalServerSideDependencies();
        services.AddTransient(_ => mockMediator.Object);
        services.AddTransient<BankAccountController>();

        var bankAccountRequest = bankAccount.ToBankAccountCreateRequestDto();

        // Act
        var response = await services.BuildServiceProvider().GetRequiredService<BankAccountController>().BankAccountPost(bankAccountRequest, CancellationToken.None);

        // Assert
        response.Result.ShouldNotBeNull();
        response.Result.ShouldBeOfType<UnprocessableEntityObjectResult>();
        response.Value.ShouldBeNull();

        var result = (UnprocessableEntityObjectResult)response.Result!;
        var errors = (SerializableError)result.Value!;

        foreach (var expectedErrorDetails in validationErrors)
        {
            var messages = (string[])errors[expectedErrorDetails.Identifier];
            messages.ShouldContain(expectedErrorDetails.ErrorMessage);
        }
    }
}

public sealed class BankAccountController_BankAccountGetById_Should
{
    [Fact]
    [Trait("Category", "Unit")]
    public async Task Return_Status200Ok_With_ABankAccount_Given_AnExistingBankAccountID()
    {
        // Arrange
        var bankAccount = Factory.BankAccount.WithTestData(10).Build();

        var mockMediator = new Mock<IMediator>();
        mockMediator.Setup(x => x.Send(It.IsAny<BankAccountByIDQuery>(), It.IsAny<CancellationToken>()))
                    .Returns(ValueTask.FromResult(Result<BankAccount>.Success(bankAccount)));

        var services = new ServiceCollection();
        services.AddMockServerSideLogging();
        services.AddAutoMapper(typeof(MappingProfile));
        services.AddTransient(_ => mockMediator.Object);
        services.AddTransient<BankAccountController>();

        // Act
        var response = await services.BuildServiceProvider().GetRequiredService<BankAccountController>().BankAccountGetById(bankAccount.BankAccountID.Value, CancellationToken.None);

        // Assert
        response.Value.ShouldBeNull();

        response.Result.ShouldNotBeNull();
        var result = (OkObjectResult)response.Result!;
        result.Value.ShouldBeAssignableTo<BankAccountResponseDto>();

        result.Value.ShouldNotBeNull();
        var value = (BankAccountResponseDto)result.Value!;
        value.BankAccountID.ShouldBe(bankAccount.BankAccountID.Value);
        value.AccountName.ShouldBe(bankAccount.AccountName);
    }

    [Fact]
    [Trait("Category", "Unit")]
    public async Task Return_Status404NotFound_Given_ANonExistentBankAccountID()
    {
        // Arrange
        var mockMediator = new Mock<IMediator>();
        mockMediator.Setup(x => x.Send(It.IsAny<BankAccountByIDQuery>(), It.IsAny<CancellationToken>()))
                    .Returns(ValueTask.FromResult(Result<BankAccount>.NotFound()));

        var services = new ServiceCollection();
        services.AddMocksOfExternalServerSideDependencies();
        services.AddTransient(_ => mockMediator.Object);
        services.AddTransient<BankAccountController>();

        // Act
        var response = await services.BuildServiceProvider().GetRequiredService<BankAccountController>().BankAccountGetById(10, CancellationToken.None);

        // Assert
        response.Result.ShouldBeOfType<NotFoundResult>();
        response.Value.ShouldBeNull();
    }
}

public class BankAccountController_BankAccountTransactionsGetByBankAccountID_Should
{
    [Fact]
    [Trait("Category", "Unit")]
    public async Task Return_Status200Ok_With_AListOfBankAccountTransaction_Given_AnExistingBankAccountID()
    {
        // Arrange
        var bankAccountTransactionList = new List<BankAccountTransaction>()
        {
            Factory.BankAccountTransaction.WithTestData(10).Build(),
            Factory.BankAccountTransaction.WithTestData(20).Build(),
            Factory.BankAccountTransaction.WithTestData(30).Build()
        };

        var mockMediator = new Mock<IMediator>();
        mockMediator.Setup(x => x.Send(It.IsAny<BankAccountTransactionsByBankAccountIDQuery>(), It.IsAny<CancellationToken>()))
                    .Returns(ValueTask.FromResult(Result<IEnumerable<BankAccountTransaction>>.Success(bankAccountTransactionList)));

        var services = new ServiceCollection();
        services.AddMockServerSideLogging();
        services.AddAutoMapper(typeof(MappingProfile));
        services.AddTransient(_ => mockMediator.Object);
        services.AddTransient<BankAccountController>();

        var container = services.BuildServiceProvider();

        // Act
        var response = await container.GetRequiredService<BankAccountController>().BankAccountTransactionsGetByBankAccountID(bankAccountTransactionList[Constants.FirstItem].BankAccountID.Value, CancellationToken.None);

        // Assert
        response.Value.ShouldBeNull();

        response.Result.ShouldNotBeNull();
        var result = (OkObjectResult)response.Result!;
        result.Value.ShouldBeAssignableTo<IEnumerable<BankAccountTransactionResponseDto>>();

        result.Value.ShouldNotBeNull();
        var value = ((IEnumerable<BankAccountTransactionResponseDto>)result.Value!).ToArray();
        value.Length.ShouldBe(bankAccountTransactionList.Count);

        var expected = bankAccountTransactionList.ToArray();
        for (var index = 0; index < expected.Length; index++)
        {
            value[index].BankAccountTransactionID.ShouldBe(expected[index].BankAccountTransactionID.Value);
            value[index].BankAccountID.ShouldBe(expected[index].BankAccountID.Value);
        }
    }

    [Fact]
    [Trait("Category", "Unit")]
    public async Task Return_Status404NotFound_Given_ANonExistentBankAccountID()
    {
        // Arrange
        var mockMediator = new Mock<IMediator>();
        mockMediator.Setup(x => x.Send(It.IsAny<BankAccountTransactionsByBankAccountIDQuery>(), It.IsAny<CancellationToken>()))
                    .Returns(ValueTask.FromResult(Result<IEnumerable<BankAccountTransaction>>.NotFound()));

        var services = new ServiceCollection();
        services.AddMocksOfExternalServerSideDependencies();
        services.AddTransient(_ => mockMediator.Object);
        services.AddTransient<BankAccountController>();

        // Act
        var response = await services.BuildServiceProvider().GetRequiredService<BankAccountController>().BankAccountTransactionsGetByBankAccountID(Constants.UnknownRecordID, CancellationToken.None);

        // Assert
        response.Result.ShouldBeOfType<NotFoundResult>();
        response.Value.ShouldBeNull();
    }
}
#pragma warning restore CA1707 // Identifiers should not contain underscores
