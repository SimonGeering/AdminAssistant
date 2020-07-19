#pragma warning disable CA1707 // Identifiers should not contain underscores
using System.Threading.Tasks;
using FluentAssertions;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace AdminAssistant.DomainModel.Modules.BudgetModule.Validation
{
    public class BudgetValidator_Should
    {
        [Fact]
        [Trait("Category", "Unit")]
        public async Task Return_IsValid_GivenAValidBudget()
        {
            // Arrange
            var services = new ServiceCollection();
            services.AddAdminAssistantClientSideDomainModel();

            var budget = TestData.BudgetTestDataBuilder.WithTestData().Build();

            // Act
            var result = await services.BuildServiceProvider().GetRequiredService<IBudgetValidator>().ValidateAsync(budget).ConfigureAwait(false);

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

            var bankAccount = TestData.BudgetTestDataBuilder.WithTestData().WithBudgetName(string.Empty).Build();

            // Act
            var result = await services.BuildServiceProvider().GetRequiredService<IBudgetValidator>().ValidateAsync(bankAccount).ConfigureAwait(false);

            // Assert
            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(x => x.Severity == Severity.Error && x.ErrorCode == "NotEmptyValidator" && x.PropertyName == nameof(Budget.BudgetName));
        }
    }
}
#pragma warning restore CA1707 // Identifiers should not contain underscores
