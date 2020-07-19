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
    }
}
#pragma warning restore CA1707 // Identifiers should not contain underscores
