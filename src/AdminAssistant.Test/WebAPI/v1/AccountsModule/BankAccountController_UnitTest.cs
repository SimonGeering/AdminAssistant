#pragma warning disable CA1707 // Identifiers should not contain underscores
using AdminAssistant.DomainModel;
using AdminAssistant.DomainModel.Modules.AccountsModule;
using AdminAssistant.DomainModel.Modules.AccountsModule.CQRS;
using AdminAssistant.WebAPI.v1;
using AdminAssistant.WebAPI.v1.AccountsModule;
using Microsoft.AspNetCore.Mvc;

namespace AdminAssistant.Test.WebAPI.v1.AccountsModule;

public sealed class BankAccountController_Put_Should
{
    [Fact]
    [Trait("Category", "Unit")]
    public async Task Return_Status200OK_Given_AValidBankAccount()
    {
        // Arrange
        var bankAccount = Factory.BankAccount.WithTestData(10).Build();

        var services = new ServiceCollection();
        services.AddMockServerSideLogging();
        services.AddAutoMapper(typeof(MappingProfile));

        var mockMediator = new Mock<IMediator>();
        mockMediator.Setup(x => x.Send(It.IsAny<BankAccountUpdateCommand>(), It.IsAny<CancellationToken>()))
                    .Returns(Task.FromResult(Result<BankAccount>.Success(bankAccount)));

        services.AddTransient((sp) => mockMediator.Object);
        services.AddTransient<BankAccountController>();

        var container = services.BuildServiceProvider();
        var bankAccountRequest = new BankAccountUpdateRequestDto()
        {
            BankAccountTypeID = bankAccount.BankAccountTypeID,
            CurrencyID = bankAccount.CurrencyID,
            AccountName = bankAccount.AccountName,
            IsBudgeted = bankAccount.IsBudgeted,
            OpeningBalance = bankAccount.OpeningBalance,
            CurrentBalance = bankAccount.CurrentBalance,
            OpenedOn = bankAccount.OpenedOn
        };

        // Act
        var response = await container.GetRequiredService<BankAccountController>().BankAccountPut(bankAccountRequest).ConfigureAwait(false);

        // Assert
        response.Value.Should().BeNull();
        response.Result.Should().NotBeNull();
        response.Result.Should().BeOfType<OkObjectResult>();

        var result = (OkObjectResult)response.Result!;
        result.Value.Should().NotBeNull();
        result.Value.Should().BeAssignableTo<BankAccountResponseDto>();

        var value = (BankAccountResponseDto)result.Value!;
        value.BankAccountID.Should().Be(bankAccount.BankAccountID);
        value.BankAccountTypeID.Should().Be(bankAccount.BankAccountTypeID);
        value.CurrencyID.Should().Be(bankAccount.CurrencyID);
        value.AccountName.Should().Be(bankAccount.AccountName);
        value.IsBudgeted.Should().Be(bankAccount.IsBudgeted);
        value.OpeningBalance.Should().Be(bankAccount.OpeningBalance);
        value.CurrentBalance.Should().Be(bankAccount.CurrentBalance);
        value.OpenedOn.Should().Be(bankAccount.OpenedOn);
    }

    [Fact]
    [Trait("Category", "Unit")]
    public async Task Return_Status404NotFound_Given_ANonExistentBankAccountID()
    {
        // Arrange
        var bankAccount = Factory.BankAccount.WithTestData(10).Build();

        var services = new ServiceCollection();
        services.AddMocksOfExternalServerSideDependencies();

        var mockMediator = new Mock<IMediator>();
        mockMediator.Setup(x => x.Send(It.IsAny<BankAccountUpdateCommand>(), It.IsAny<CancellationToken>()))
                    .Returns(Task.FromResult(Result<BankAccount>.NotFound()));

        services.AddTransient((sp) => mockMediator.Object);
        services.AddTransient<BankAccountController>();

        var container = services.BuildServiceProvider();

        var mapper = container.GetRequiredService<IMapper>();
        var bankAccountRequest = mapper.Map<BankAccountUpdateRequestDto>(bankAccount);

        // Act
        var response = await services.BuildServiceProvider().GetRequiredService<BankAccountController>().BankAccountPut(bankAccountRequest).ConfigureAwait(false);

        // Assert
        response.Result.Should().BeOfType<NotFoundObjectResult>();
        response.Value.Should().BeNull();
    }

    [Fact]
    [Trait("Category", "Unit")]
    public async Task Return_Status422UnprocessableEntity_Given_AnInvalidBankAccount()
    {
        // Arrange
        var validationErrors = new List<ValidationError>()
            {
                new ValidationError() { Identifier="ExampleErrorCode", ErrorMessage="ExampleErrorMessage", Severity=ValidationSeverity.Error },
                new ValidationError() { Identifier="ExampleErrorCode2", ErrorMessage="ExampleErrorMessage2", Severity=ValidationSeverity.Error }
            };
        var bankAccount = Factory.BankAccount.WithTestData(10).Build();

        var services = new ServiceCollection();
        services.AddMocksOfExternalServerSideDependencies();

        var mockMediator = new Mock<IMediator>();
        mockMediator.Setup(x => x.Send(It.IsAny<BankAccountUpdateCommand>(), It.IsAny<CancellationToken>()))
                    .Returns(Task.FromResult(Result<BankAccount>.Invalid(validationErrors)));

        services.AddTransient((sp) => mockMediator.Object);
        services.AddTransient<BankAccountController>();

        var container = services.BuildServiceProvider();

        var mapper = container.GetRequiredService<IMapper>();
        var bankAccountRequest = mapper.Map<BankAccountUpdateRequestDto>(bankAccount);

        // Act
        var response = await container.GetRequiredService<BankAccountController>().BankAccountPut(bankAccountRequest).ConfigureAwait(false);

        // Assert
        response.Value.Should().BeNull();
        response.Result.Should().NotBeNull();
        response.Result.Should().BeOfType<UnprocessableEntityObjectResult>();

        var result = (UnprocessableEntityObjectResult)response.Result!;
        result.Value.Should().NotBeNull();
        var errors = (SerializableError)result.Value!;

        foreach (var expectedErrorDetails in validationErrors)
        {
            var messages = (string[])errors[expectedErrorDetails.Identifier];
            messages.Should().Contain(expectedErrorDetails.ErrorMessage);
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

        var services = new ServiceCollection();
        services.AddMockServerSideLogging();
        services.AddAutoMapper(typeof(MappingProfile));

        var mockMediator = new Mock<IMediator>();
        mockMediator.Setup(x => x.Send(It.IsAny<BankAccountCreateCommand>(), It.IsAny<CancellationToken>()))
                    .Returns(Task.FromResult(Result<BankAccount>.Success(bankAccount)));

        services.AddTransient((sp) => mockMediator.Object);
        services.AddTransient<BankAccountController>();

        var container = services.BuildServiceProvider();

        var bankAccountRequest = new BankAccountCreateRequestDto()
        {
            BankAccountTypeID = bankAccount.BankAccountTypeID,
            CurrencyID = bankAccount.CurrencyID,
            AccountName = bankAccount.AccountName,
            IsBudgeted = bankAccount.IsBudgeted,
            OpeningBalance = bankAccount.OpeningBalance,
            CurrentBalance = bankAccount.CurrentBalance,
            OpenedOn = bankAccount.OpenedOn
        };

        // Act
        var response = await container.GetRequiredService<BankAccountController>().BankAccountPost(bankAccountRequest).ConfigureAwait(false);

        // Assert
        response.Value.Should().BeNull();
        response.Result.Should().NotBeNull();
        response.Result.Should().BeOfType<CreatedAtRouteResult>();

        var result = (CreatedAtRouteResult)response.Result!;
        result.Value.Should().NotBeNull();
        result.Value.Should().BeAssignableTo<BankAccountResponseDto>();

        var value = (BankAccountResponseDto)result.Value!;
        value.BankAccountID.Should().Be(bankAccount.BankAccountID);
        value.BankAccountTypeID.Should().Be(bankAccount.BankAccountTypeID);
        value.CurrencyID.Should().Be(bankAccount.CurrencyID);
        value.AccountName.Should().Be(bankAccount.AccountName);
        value.IsBudgeted.Should().Be(bankAccount.IsBudgeted);
        value.OpeningBalance.Should().Be(bankAccount.OpeningBalance);
        value.CurrentBalance.Should().Be(bankAccount.CurrentBalance);
        value.OpenedOn.Should().Be(bankAccount.OpenedOn);
    }

[Fact]
    [Trait("Category", "Unit")]
    public async Task Return_Status422UnprocessableEntity_Given_AnInvalidBankAccount()
    {
        // Arrange
        var validationErrors = new List<ValidationError>()
            {
                new ValidationError() { Identifier="ExampleErrorCode", ErrorMessage="ExampleErrorMessage", Severity=ValidationSeverity.Error },
                new ValidationError() { Identifier="ExampleErrorCode2", ErrorMessage="ExampleErrorMessage2", Severity=ValidationSeverity.Error }
            };
        var bankAccount = Factory.BankAccount.WithTestData(10).Build();

        var services = new ServiceCollection();
        services.AddMocksOfExternalServerSideDependencies();

        var mockMediator = new Mock<IMediator>();
        mockMediator.Setup(x => x.Send(It.IsAny<BankAccountCreateCommand>(), It.IsAny<CancellationToken>()))
                    .Returns(Task.FromResult(Result<BankAccount>.Invalid(validationErrors)));

        services.AddTransient((sp) => mockMediator.Object);
        services.AddTransient<BankAccountController>();

        var container = services.BuildServiceProvider();

        var mapper = container.GetRequiredService<IMapper>();
        var bankAccountRequest = mapper.Map<BankAccountCreateRequestDto>(bankAccount);

        // Act
        var response = await container.GetRequiredService<BankAccountController>().BankAccountPost(bankAccountRequest).ConfigureAwait(false);

        // Assert
        response.Result.Should().NotBeNull();
        response.Result.Should().BeOfType<UnprocessableEntityObjectResult>();
        response.Value.Should().BeNull();

        var result = (UnprocessableEntityObjectResult)response.Result!;
        var errors = (SerializableError)result.Value!;

        foreach (var expectedErrorDetails in validationErrors)
        {
            var messages = (string[])errors[expectedErrorDetails.Identifier];
            messages.Should().Contain(expectedErrorDetails.ErrorMessage);
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

        var services = new ServiceCollection();
        services.AddMockServerSideLogging();
        services.AddAutoMapper(typeof(MappingProfile));

        var mockMediator = new Mock<IMediator>();
        mockMediator.Setup(x => x.Send(It.IsAny<BankAccountByIDQuery>(), It.IsAny<CancellationToken>()))
                    .Returns(Task.FromResult(Result<BankAccount>.Success(bankAccount)));

        services.AddTransient((sp) => mockMediator.Object);
        services.AddTransient<BankAccountController>();

        // Act
        var response = await services.BuildServiceProvider().GetRequiredService<BankAccountController>().BankAccountGetById(bankAccount.BankAccountID).ConfigureAwait(false);

        // Assert
        response.Value.Should().BeNull();

        response.Result.Should().NotBeNull();
        var result = (OkObjectResult)response.Result!;
        result.Value.Should().BeAssignableTo<BankAccountResponseDto>();

        result.Value.Should().NotBeNull();
        var value = (BankAccountResponseDto)result.Value!;
        value.BankAccountID.Should().Be(bankAccount.BankAccountID);
        value.AccountName.Should().Be(bankAccount.AccountName);
    }

    [Fact]
    [Trait("Category", "Unit")]
    public async Task Return_Status404NotFound_Given_ANonExistentBankAccountID()
    {
        // Arrange
        var services = new ServiceCollection();
        services.AddMocksOfExternalServerSideDependencies();

        var mockMediator = new Mock<IMediator>();
        mockMediator.Setup(x => x.Send(It.IsAny<BankAccountByIDQuery>(), It.IsAny<CancellationToken>()))
                    .Returns(Task.FromResult(Result<BankAccount>.NotFound()));

        services.AddTransient((sp) => mockMediator.Object);
        services.AddTransient<BankAccountController>();

        // Act
        var response = await services.BuildServiceProvider().GetRequiredService<BankAccountController>().BankAccountGetById(10).ConfigureAwait(false);

        // Assert
        response.Result.Should().BeOfType<NotFoundResult>();
        response.Value.Should().BeNull();
    }
}

public class BankAccountController_BankAccountTransactionsGetByBankAccountID_Should
{
    //[Fact(Skip="WIP")]
    //[Trait("Category", "Unit")]
    //public async Task Return_Status200Ok_With_AListOfBankAccountTransaction_Given_AnExistingBankAccountID()
    //{
    //    // Arrange
    //    var bankAccountTransactionList = new List<BankAccountTransaction>()
    //    {
    //        Factory.BankAccountTransaction.WithTestData(10).Build(),
    //        Factory.BankAccountTransaction.WithTestData(20).Build(),
    //        Factory.BankAccountTransaction.WithTestData(30).Build()
    //    };

    //    var services = new ServiceCollection();
    //    services.AddMockServerSideLogging();
    //    services.AddAutoMapper(typeof(MappingProfile));

    //    var mockMediator = new Mock<IMediator>();
    //    mockMediator.Setup(x => x.Send(It.IsAny<BankAccountTransactionsByBankAccountIDQuery>(), It.IsAny<CancellationToken>()))
    //                .Returns(Task.FromResult(Result<IEnumerable<BankAccountTransaction>>.Success(bankAccountTransactionList)));

    //    services.AddTransient((sp) => mockMediator.Object);
    //    services.AddTransient<BankAccountController>();

    //    var container = services.BuildServiceProvider();

    //    // Act
    //    var response = await container.GetRequiredService<BankAccountController>().BankAccountTransactionsGetByBankAccountID(bankAccountTransactionList.First().BankAccountID).ConfigureAwait(false);

    //    // Assert
    //    var result = (OkObjectResult)response.Result;
    //    result.Value.Should().BeAssignableTo<IEnumerable<BankAccountTransactionResponseDto>>();

    //    var value = ((IEnumerable<BankAccountTransactionResponseDto>)result.Value).ToArray();
    //    value.Should().HaveCount(bankAccountTransactionList.Count);

    //    var expected = bankAccountTransactionList.ToArray();
    //    for (int i = 0; i < expected.Length; i++)
    //    {
    //        // TODO: Switch this to use AutoMapper.
    //        value[i].BankAccountTransactionID.Should().Be(expected[i].BankAccountTransactionID);
    //        value[i].BankAccountID.Should().Be(expected[i].BankAccountID);
    //    }
    //}

    [Fact]
    [Trait("Category", "Unit")]
    public async Task Return_Status404NotFound_Given_ANonExistentBankAccountID()
    {
        // Arrange
        var services = new ServiceCollection();
        services.AddMocksOfExternalServerSideDependencies();

        var mockMediator = new Mock<IMediator>();
        mockMediator.Setup(x => x.Send(It.IsAny<BankAccountTransactionsByBankAccountIDQuery>(), It.IsAny<CancellationToken>()))
                    .Returns(Task.FromResult(Result<IEnumerable<BankAccountTransaction>>.NotFound()));

        services.AddTransient((sp) => mockMediator.Object);
        services.AddTransient<BankAccountController>();

        // Act
        var response = await services.BuildServiceProvider().GetRequiredService<BankAccountController>().BankAccountTransactionsGetByBankAccountID(Constants.UnknownRecordID).ConfigureAwait(false);

        // Assert
        response.Result.Should().BeOfType<NotFoundResult>();
        response.Value.Should().BeNull();
    }
}
#pragma warning restore CA1707 // Identifiers should not contain underscores
