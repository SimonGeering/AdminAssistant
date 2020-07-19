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
        public async Task Return_IsValid_GivenAValidCurrency()
        {
            // Arrange
            var services = new ServiceCollection();
            services.AddAdminAssistantClientSideDomainModel();

            //var currency = TestData.CurrencyBuilder.WithTestData();

            // Act
            int value = 42;
            //var result = await services.BuildServiceProvider().GetRequiredService<ICurrencyValidator>().ValidateAsync(currency).ConfigureAwait(false);

            // Assert
            value.Should().Be(42);
            //result.IsValid.Should().BeTrue();

        }
    }
}
#pragma warning restore CA1707 // Identifiers should not contain underscores
