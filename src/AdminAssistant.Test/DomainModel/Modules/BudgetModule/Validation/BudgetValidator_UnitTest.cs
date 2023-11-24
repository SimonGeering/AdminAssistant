#pragma warning disable CA1707 // Identifiers should not contain underscores

using AdminAssistant.DomainModel;
using AdminAssistant.DomainModel.Modules.BudgetModule;
using AdminAssistant.DomainModel.Modules.BudgetModule.Validation;

namespace AdminAssistant.Test.DomainModel.Modules.BudgetModule.Validation;

public sealed class BudgetValidator_Should
{
    [Fact]
    [Trait("Category", "Unit")]
    public async Task Return_IsValid_GivenAValidBudget()
    {
        // Arrange
        var services = new ServiceCollection();
        services.AddAdminAssistantClientSideDomainModel();

        var budget = Factory.Budget.WithTestData().Build();

        // Act
        var result = await services.BuildServiceProvider().GetRequiredService<IBudgetValidator>().ValidateAsync(budget);

        // Assert
        result.IsValid.Should().BeTrue();
    }

    [Fact]
    [Trait("Category", "Unit")]
    public async Task Return_ValidationError_GivenABudgetWithAnEmptyBudgetName()
    {
        // Arrange
        var services = new ServiceCollection();
        services.AddAdminAssistantClientSideDomainModel();

        var budget = Factory.Budget.WithTestData()
                                   .WithBudgetName(string.Empty)
                                   .Build();
        // Act
        var result = await services.BuildServiceProvider().GetRequiredService<IBudgetValidator>().ValidateAsync(budget);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(x => x.Severity == Severity.Error && x.ErrorCode == "NotEmptyValidator" && x.PropertyName == nameof(Budget.BudgetName));
    }

    [Fact]
    [Trait("Category", "Unit")]
    public async Task Return_ValidationError_GivenACurrency_WithADecimalFormat_LongerThanMaxLength()
    {
        // Arrange
        var services = new ServiceCollection();
        services.AddAdminAssistantClientSideDomainModel();

        var budget = Factory.Budget.WithTestData()
                                   .WithBudgetName(new string('x', Budget.BudgetNameMaxLength + 1))
                                   .Build();
        // Act
        var result = await services.BuildServiceProvider().GetRequiredService<IBudgetValidator>().ValidateAsync(budget);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(x => x.Severity == Severity.Error && x.ErrorCode == "MaximumLengthValidator" && x.PropertyName == nameof(Budget.BudgetName));
    }
}
#pragma warning restore CA1707 // Identifiers should not contain underscores
