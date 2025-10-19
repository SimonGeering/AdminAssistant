#pragma warning disable CA1707 // Identifiers should not contain underscores

using AdminAssistant.Domain;
using AdminAssistant.Modules.AccountsModule;
using AdminAssistant.Modules.AccountsModule.Validation;

namespace AdminAssistant.Test.DomainModel.Modules.AccountsModule.Validation;

public sealed class BankAccountValidator_Should
{
    [Fact]
    [Trait("Category", "Unit")]
    public async Task Return_IsValid_GivenAValidBankAccount()
    {
        // Arrange
        var services = new ServiceCollection();
        services.AddAdminAssistantClientSideDomainModel();

        var bankAccount = Factory.BankAccount.WithTestData()
                                 .WithBankAccountTypeID(20)
                                 .WithCurrencyID(10)
                                 .Build();
        // Act
        var result = await services.BuildServiceProvider().GetRequiredService<IBankAccountValidator>().ValidateAsync(bankAccount);

        // Assert
        result.IsValid.ShouldBeTrue();
    }

    [Fact]
    [Trait("Category", "Unit")]
    public async Task Return_ValidationError_GivenABankAccountWithAnEmptyAccountName()
    {
        // Arrange
        var services = new ServiceCollection();
        services.AddAdminAssistantClientSideDomainModel();

        var bankAccount = Factory.BankAccount.WithTestData()
                                             .WithAccountName(string.Empty)
                                             .Build();
        // Act
        var result = await services.BuildServiceProvider().GetRequiredService<IBankAccountValidator>().ValidateAsync(bankAccount);

        // Assert
        result.IsValid.ShouldBeFalse();
        result.Errors.ShouldContain(x => x.Severity == Severity.Error && x.ErrorCode == "NotEmptyValidator" && x.PropertyName == nameof(BankAccount.AccountName));
    }


    [Fact]
    [Trait("Category", "Unit")]
    public async Task Return_ValidationError_GivenABankAccount_WithAnAccountName_LongerThanMaxLength()
    {
        // Arrange
        var services = new ServiceCollection();
        services.AddAdminAssistantClientSideDomainModel();

        var bankAccount = Factory.BankAccount.WithTestData()
                                             .WithAccountName(new string('x', BankAccount.AccountNameMaxLength + 1))
                                             .Build();
        // Act
        var result = await services.BuildServiceProvider().GetRequiredService<IBankAccountValidator>().ValidateAsync(bankAccount);

        // Assert
        result.IsValid.ShouldBeFalse();
        result.Errors.ShouldContain(x => x.Severity == Severity.Error && x.ErrorCode == "MaximumLengthValidator" && x.PropertyName == nameof(BankAccount.AccountName));
    }

    [Fact]
    [Trait("Category", "Unit")]
    public async Task Return_ValidationError_GivenABankAccountWithAMissingBankAccountTypeID()
    {
        // Arrange
        var services = new ServiceCollection();
        services.AddAdminAssistantClientSideDomainModel();

        var bankAccount = Factory.BankAccount.WithTestData()
                                             .WithBankAccountTypeID(Constants.UnknownRecordID)
                                             .Build();
        // Act
        var result = await services.BuildServiceProvider().GetRequiredService<IBankAccountValidator>().ValidateAsync(bankAccount);

        // Assert
        result.IsValid.ShouldBeFalse();
        result.Errors.ShouldContain(x => x.Severity == Severity.Error && x.ErrorCode == "NotEqualValidator" && x.PropertyName == nameof(BankAccount.BankAccountTypeID));
    }

    [Fact]
    [Trait("Category", "Unit")]
    public async Task Return_ValidationError_GivenABankAccountWithAMissingCurrencyID()
    {
        // Arrange
        var services = new ServiceCollection();
        services.AddAdminAssistantClientSideDomainModel();

        var bankAccount = Factory.BankAccount.WithTestData()
                                             .WithCurrencyID(Constants.UnknownRecordID)
                                             .Build();
        // Act
        var result = await services.BuildServiceProvider().GetRequiredService<IBankAccountValidator>().ValidateAsync(bankAccount);

        // Assert
        result.IsValid.ShouldBeFalse();
        result.Errors.ShouldContain(x => x.Severity == Severity.Error && x.ErrorCode == "NotEqualValidator" && x.PropertyName == nameof(BankAccount.CurrencyID));
    }
}
#pragma warning restore CA1707 // Identifiers should not contain underscores
