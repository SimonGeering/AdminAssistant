#pragma warning disable CA1707 // Identifiers should not contain underscores

using AdminAssistant.Domain;
using AdminAssistant.Modules.AccountsModule;
using AdminAssistant.Modules.AccountsModule.Validation;

namespace AdminAssistant.Test.DomainModel.Modules.AccountsModule.Validation;

public sealed class BankValidator_Should
{
    const string BankName_Value = $"{nameof(Bank.BankName)}.{nameof(Bank.BankName.Value)}";

    [Fact]
    [Trait("Category", "Unit")]
    public async Task Return_IsValid_GivenAValidBank()
    {
        // Arrange
        var services = new ServiceCollection();
        services.AddAdminAssistantClientSideDomainModel();

        var bank = Factory.Bank.WithTestData(20)
                          .Build();
        // Act
        var result = await services.BuildServiceProvider().GetRequiredService<IBankValidator>().ValidateAsync(bank);

        // Assert
        result.IsValid.ShouldBeTrue();
    }

    [Fact]
    [Trait("Category", "Unit")]
    public async Task Return_ValidationError_GivenABankWithAnEmptyBankName()
    {
        // Arrange
        var services = new ServiceCollection();
        services.AddAdminAssistantClientSideDomainModel();

        var bank = Factory.Bank.WithTestData(20)
                          .WithBankName(string.Empty)
                          .Build();
        // Act
        var result = await services.BuildServiceProvider().GetRequiredService<IBankValidator>().ValidateAsync(bank);

        // Assert
        result.IsValid.ShouldBeFalse();
        result.Errors.ShouldContain(x => x.Severity == Severity.Error && x.ErrorCode == "NotEmptyValidator" && x.PropertyName == BankName_Value);
    }

    [Fact]
    [Trait("Category", "Unit")]
    public async Task Return_ValidationError_GivenABank_WithABankName_LongerThanMaxLength()
    {
        // Arrange
        var services = new ServiceCollection();
        services.AddAdminAssistantClientSideDomainModel();

        var bank = Factory.Bank.WithTestData(20)
                          .WithBankName(new string('x', Bank.BankNameMaxLength + 1))
                          .Build();
        // Act
        var result = await services.BuildServiceProvider().GetRequiredService<IBankValidator>().ValidateAsync(bank);

        // Assert
        result.IsValid.ShouldBeFalse();
        result.Errors.ShouldContain(x => x.Severity == Severity.Error && x.ErrorCode == "MaximumLengthValidator" && x.PropertyName == BankName_Value);
    }
}
#pragma warning restore CA1707 // Identifiers should not contain underscores
