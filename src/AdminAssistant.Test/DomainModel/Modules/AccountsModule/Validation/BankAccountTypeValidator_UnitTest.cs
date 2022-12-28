#pragma warning disable CA1707 // Identifiers should not contain underscores

using AdminAssistant.DomainModel;
using AdminAssistant.DomainModel.Modules.AccountsModule;
using AdminAssistant.DomainModel.Modules.AccountsModule.Validation;

namespace AdminAssistant.Test.DomainModel.Modules.AccountsModule.Validation;

public sealed class BankAccountTypeValidator_Should
{
    [Fact]
    [Trait("Category", "Unit")]
    public async Task Return_IsValid_GivenAValidBankAccountType()
    {
        // Arrange
        var services = new ServiceCollection();
        services.AddAdminAssistantClientSideDomainModel();

        var bankAccountType = Factory.BankAccountType.WithTestData(20)
                                                     .Build();
        // Act
        var result = await services.BuildServiceProvider().GetRequiredService<IBankAccountTypeValidator>().ValidateAsync(bankAccountType).ConfigureAwait(false);

        // Assert
        result.IsValid.Should().BeTrue();
    }

    [Fact]
    [Trait("Category", "Unit")]
    public async Task Return_ValidationError_GivenABankAccountTypeWithAnEmptyDescription()
    {
        // Arrange
        var services = new ServiceCollection();
        services.AddAdminAssistantClientSideDomainModel();

        var bankAccountType = Factory.BankAccountType.WithTestData(20)
                                                     .WithDescription(string.Empty)
                                                     .Build();
        // Act
        var result = await services.BuildServiceProvider().GetRequiredService<IBankAccountTypeValidator>().ValidateAsync(bankAccountType).ConfigureAwait(false);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(x => x.Severity == Severity.Error && x.ErrorCode == "NotEmptyValidator" && x.PropertyName == nameof(BankAccountType.Description));
    }

    [Fact]
    [Trait("Category", "Unit")]
    public async Task Return_ValidationError_GivenABank_WithABankName_LongerThanMaxLength()
    {
        // Arrange
        var services = new ServiceCollection();
        services.AddAdminAssistantClientSideDomainModel();

        var bankAccountType = Factory.BankAccountType.WithTestData(20)
                                                     .WithDescription(new string('x', BankAccountType.DescriptionMaxLength + 1))
                                                     .Build();
        // Act
        var result = await services.BuildServiceProvider().GetRequiredService<IBankAccountTypeValidator>().ValidateAsync(bankAccountType).ConfigureAwait(false);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(x => x.Severity == Severity.Error && x.ErrorCode == "MaximumLengthValidator" && x.PropertyName == nameof(BankAccountType.Description));
    }
}
#pragma warning restore CA1707 // Identifiers should not contain underscores
