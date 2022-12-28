#pragma warning disable CA1707 // Identifiers should not contain underscores
//using FluentValidation;

using AdminAssistant.DomainModel;
using AdminAssistant.DomainModel.Modules.AccountsModule;
using AdminAssistant.DomainModel.Modules.AccountsModule.Validation;

namespace AdminAssistant.Test.DomainModel.Modules.AccountsModule.Validation;

public sealed class BankAccountTransactionValidator_Should
{
    [Fact]
    [Trait("Category", "Unit")]
    public async Task Return_IsValid_GivenAValidBankAccountTransaction()
    {
        // Arrange
        var services = new ServiceCollection();
        services.AddAdminAssistantClientSideDomainModel();

        var bankAccountTransaction = Factory.BankAccountTransaction.WithTestData()
                                                                   .WithBankAccountID(20)
                                                                   .Build();
        // Act
        var result = await services.BuildServiceProvider().GetRequiredService<IBankAccountTransactionValidator>().ValidateAsync(bankAccountTransaction).ConfigureAwait(false);

        // Assert
        result.IsValid.Should().BeTrue();
    }

    [Fact]
    [Trait("Category", "Unit")]
    public async Task Return_ValidationError_GivenABankAccountWithAMissingCurrencyID()
    {
        // Arrange
        var services = new ServiceCollection();
        services.AddAdminAssistantClientSideDomainModel();

        var bankAccountTransaction = Factory.BankAccountTransaction.WithTestData()
                                                                   .WithBankAccountID(Constants.UnknownRecordID)
                                                                   .Build();
        // Act
        var result = await services.BuildServiceProvider().GetRequiredService<IBankAccountTransactionValidator>().ValidateAsync(bankAccountTransaction).ConfigureAwait(false);


        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(x => x.Severity == Severity.Error && x.ErrorCode == "NotEqualValidator" && x.PropertyName == nameof(BankAccountTransaction.BankAccountID));
    }

    [Fact]
    [Trait("Category", "Unit")]
    public async Task Return_ValidationError_GivenABankAccountTransactionWithAnEmptyDescription()
    {
        // Arrange
        var services = new ServiceCollection();
        services.AddAdminAssistantClientSideDomainModel();

        var bankAccountTransaction = Factory.BankAccountTransaction.WithTestData()
                                                                   .WithDescription(string.Empty)
                                                                   .Build();
        // Act
        var result = await services.BuildServiceProvider().GetRequiredService<IBankAccountTransactionValidator>().ValidateAsync(bankAccountTransaction).ConfigureAwait(false);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(x => x.Severity == Severity.Error && x.ErrorCode == "NotEmptyValidator" && x.PropertyName == nameof(BankAccountTransaction.Description));
    }


    [Fact]
    [Trait("Category", "Unit")]
    public async Task Return_ValidationError_GivenABankAccountTransaction_WithADescription_LongerThanMaxLength()
    {
        // Arrange
        var services = new ServiceCollection();
        services.AddAdminAssistantClientSideDomainModel();

        var bankAccountTransaction = Factory.BankAccountTransaction.WithTestData()
                                                                   .WithDescription(new string('x', BankAccountTransaction.DescriptionMaxLength + 1))
                                                                   .Build();
        // Act
        var result = await services.BuildServiceProvider().GetRequiredService<IBankAccountTransactionValidator>().ValidateAsync(bankAccountTransaction).ConfigureAwait(false);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(x => x.Severity == Severity.Error && x.ErrorCode == "MaximumLengthValidator" && x.PropertyName == nameof(BankAccountTransaction.Description));
    }
}
