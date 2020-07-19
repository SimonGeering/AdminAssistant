#pragma warning disable CA1707 // Identifiers should not contain underscores
using FluentAssertions;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using Xunit;

namespace AdminAssistant.DomainModel.Modules.AccountsModule.Validation
{
    public class BankAccountValidator_Should
    {
        [Fact]
        [Trait("Category", "Unit")]
        public async Task Return_IsValid_GivenAValidBankAccount()
        {
            // Arrange
            var services = new ServiceCollection();
            services.AddAdminAssistantClientSideDomainModel();

            var bankAccount = TestData.BankAccountBuilder.WithTestData().WithBankAccountTypeID(20).WithCurrencyID(10).Build();

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
            services.AddAdminAssistantClientSideDomainModel();

            var bankAccount = TestData.BankAccountBuilder.WithTestData().WithAccountName(string.Empty).Build();

            // Act
            var result = await services.BuildServiceProvider().GetRequiredService<IBankAccountValidator>().ValidateAsync(bankAccount).ConfigureAwait(false);

            // Assert
            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(x => x.Severity == Severity.Error && x.ErrorCode == "NotEmptyValidator" && x.PropertyName == nameof(BankAccount.AccountName));
        }

        [Fact]
        [Trait("Category", "Unit")]
        public async Task Return_ValidationError_GivenABankAccountWithAMissingBankAccountTypeID()
        {
            // Arrange
            var services = new ServiceCollection();
            services.AddAdminAssistantClientSideDomainModel();

            var bankAccount = TestData.BankAccountBuilder.WithTestData().WithBankAccountTypeID(Constants.UnknownRecordID).Build();
            
            // Act
            var result = await services.BuildServiceProvider().GetRequiredService<IBankAccountValidator>().ValidateAsync(bankAccount).ConfigureAwait(false);

            // Assert
            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(x => x.Severity == Severity.Error && x.ErrorCode == "NotEqualValidator" && x.PropertyName == nameof(BankAccount.BankAccountTypeID));
        }
    }
}
#pragma warning restore CA1707 // Identifiers should not contain underscores
