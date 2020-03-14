#pragma warning disable CA1707 // Identifiers should not contain underscores
using FluentAssertions;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using Xunit;

namespace AdminAssistant.Accounts.DomainModel.Validation
{
    public class BankAccountValidator_Should
    {
        [Fact]
        [Trait("Category", "Unit")]
        public async Task Return_IsValid_GivenAValidBankAccount()
        {
            // Arrange
            var services = new ServiceCollection();
            services.AddTransient<ICurrencyValidator, CurrencyValidator>();
            services.AddTransient<IBankAccountValidator, BankAccountValidator>();

            var bankAccount = TestData.BankAccountBuilder.WithTestData().Build();

            // Act
            var result = await services.BuildServiceProvider().GetRequiredService<IBankAccountValidator>().ValidateAsync(bankAccount).ConfigureAwait(false);

            // Assert
            result.IsValid.Should().BeTrue();
        }

        [Fact]
        [Trait("Category", "Unit")]
        public async Task Return_ValidationError_GivenABankAccountWithAnEmptyAccountName()
        {
            // Arrange
            var services = new ServiceCollection();
            services.AddTransient<ICurrencyValidator, CurrencyValidator>();
            services.AddTransient<IBankAccountValidator, BankAccountValidator>();

            var bankAccount = TestData.BankAccountBuilder.WithTestData().WithAccountName(string.Empty);

            // Act
            var result = await services.BuildServiceProvider().GetRequiredService<IBankAccountValidator>().ValidateAsync(bankAccount).ConfigureAwait(false);

            // Assert
            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(x => x.Severity == Severity.Error && x.ErrorCode == "NotEmptyValidator" && x.PropertyName == nameof(BankAccount.AccountName));
        }
    }
}
#pragma warning restore CA1707 // Identifiers should not contain underscores
