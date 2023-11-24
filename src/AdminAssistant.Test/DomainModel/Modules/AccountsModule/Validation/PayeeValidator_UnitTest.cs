#pragma warning disable CA1707 // Identifiers should not contain underscores

using AdminAssistant.DomainModel;
using AdminAssistant.DomainModel.Modules.AccountsModule;
using AdminAssistant.DomainModel.Modules.AccountsModule.Validation;

namespace AdminAssistant.Test.DomainModel.Modules.AccountsModule.Validation;

public sealed class PayeeValidator_Should
{
    [Fact]
    [Trait("Category", "Unit")]
    public async Task Return_IsValid_GivenAValidPayee()
    {
        // Arrange
        var services = new ServiceCollection();
        services.AddAdminAssistantClientSideDomainModel();

        var payee = Factory.Payee.WithTestData(20)
                          .Build();
        // Act
        var result = await services.BuildServiceProvider().GetRequiredService<IPayeeValidator>().ValidateAsync(payee);

        // Assert
        result.IsValid.Should().BeTrue();
    }

    [Fact]
    [Trait("Category", "Unit")]
    public async Task Return_IsNotValid_GivenAPayeeWithAnEmptyName()
    {
        // Arrange
        var services = new ServiceCollection();
        services.AddAdminAssistantClientSideDomainModel();

        var payee = Factory.Payee.WithTestData(20)
                          .WithName(string.Empty)
                          .Build();
        // Act
        var result = await services.BuildServiceProvider().GetRequiredService<IPayeeValidator>().ValidateAsync(payee);

        // Assert
        result.IsValid.Should().BeFalse();
    }

    [Fact]
    [Trait("Category", "Unit")]
    public async Task Return_IsNotValid_GivenAPayeeWithANameThatIsTooLong()
    {
        // Arrange
        var services = new ServiceCollection();
        services.AddAdminAssistantClientSideDomainModel();

        var payee = Factory.Payee.WithTestData(20)
                          .WithName(new string('X', Payee.NameMaxLength + 1))
                          .Build();
        // Act
        var result = await services.BuildServiceProvider().GetRequiredService<IPayeeValidator>().ValidateAsync(payee);

        // Assert
        result.IsValid.Should().BeFalse();
    }
}
#pragma warning restore CA1707 // Identifiers should not contain underscores
