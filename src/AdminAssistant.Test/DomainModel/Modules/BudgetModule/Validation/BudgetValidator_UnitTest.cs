#pragma warning disable CA1707 // Identifiers should not contain underscores

using AdminAssistant.Domain;
using AdminAssistant.Modules.BudgetModule;
using AdminAssistant.Modules.BudgetModule.Validation;

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
        result.IsValid.ShouldBeTrue();
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
        result.IsValid.ShouldBeFalse();
        result.Errors.ShouldContain(x => x.Severity == Severity.Error && x.ErrorCode == "NotEmptyValidator" && x.PropertyName == nameof(Budget.BudgetName));
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
        result.IsValid.ShouldBeFalse();
        result.Errors.ShouldContain(x => x.Severity == Severity.Error && x.ErrorCode == "MaximumLengthValidator" && x.PropertyName == nameof(Budget.BudgetName));
    }
}
#pragma warning restore CA1707 // Identifiers should not contain underscores
