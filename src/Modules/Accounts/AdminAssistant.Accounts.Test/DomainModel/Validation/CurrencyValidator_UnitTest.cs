#pragma warning disable CA1707 // Identifiers should not contain underscores
using System.Threading.Tasks;
using FluentAssertions;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace AdminAssistant.Accounts.DomainModel.Validation
{
    public class CurrencyValidator_Should
    {
        [Fact]
        [Trait("Category", "Unit")]
        public async Task Return_IsValid_GivenAValidCurrency()
        {
            // Arrange
            var services = new ServiceCollection();
            services.AddTransient<ICurrencyValidator, CurrencyValidator>();
            var currency = TestData.CurrencyBuilder.WithTestData();

            // Act
            var result = await services.BuildServiceProvider().GetRequiredService<ICurrencyValidator>().ValidateAsync(currency).ConfigureAwait(false);

            // Assert
            result.IsValid.Should().BeTrue();
        }

        [Fact]
        [Trait("Category", "Unit")]
        public async Task Return_ValidationError_GivenACurrency_WithoutADecimalFormat()
        {
            // Arrange
            var services = new ServiceCollection();
            services.AddTransient<ICurrencyValidator, CurrencyValidator>();
            var currency = TestData.CurrencyBuilder.WithTestData().WithoutADecimalFormat();

            // Act
            var result = await services.BuildServiceProvider().GetRequiredService<ICurrencyValidator>().ValidateAsync(currency).ConfigureAwait(false);

            // Assert
            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(x => x.Severity == Severity.Error && x.ErrorCode == "NotEmptyValidator" && x.PropertyName == nameof(Currency.DecimalFormat));
        }

        [Fact]
        [Trait("Category", "Unit")]
        public async Task Return_ValidationError_GivenACurrency_WithADecimalFormat_LongerThanMaxLength()
        {
            // Arrange
            var services = new ServiceCollection();
            services.AddTransient<ICurrencyValidator, CurrencyValidator>();
            var currency = TestData.CurrencyBuilder.WithTestData().WithDecimalFormat(new string('x', Currency.DecimalFormatMaxLength + 1));

            // Act
            var result = await services.BuildServiceProvider().GetRequiredService<ICurrencyValidator>().ValidateAsync(currency).ConfigureAwait(false);

            // Assert
            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(x => x.Severity == Severity.Error && x.ErrorCode == "MaximumLengthValidator" && x.PropertyName == nameof(Currency.DecimalFormat));
        }

        [Fact]
        [Trait("Category", "Unit")]
        public async Task Return_ValidationError_GivenACurrency_WithoutASymbol()
        {
            // Arrange
            var services = new ServiceCollection();
            services.AddTransient<ICurrencyValidator, CurrencyValidator>();
            var currency = TestData.CurrencyBuilder.WithTestData().WithoutASymbol();

            // Act
            var result = await services.BuildServiceProvider().GetRequiredService<ICurrencyValidator>().ValidateAsync(currency).ConfigureAwait(false);

            // Assert
            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(x => x.Severity == Severity.Error && x.ErrorCode == "NotEmptyValidator" && x.PropertyName == nameof(Currency.Symbol));
        }

        [Fact]
        [Trait("Category", "Unit")]
        public async Task Return_ValidationError_GivenACurrency_WithASymbolLongerThanMaxLength()
        {
            // Arrange
            var services = new ServiceCollection();
            services.AddTransient<ICurrencyValidator, CurrencyValidator>();
            var currency = TestData.CurrencyBuilder.WithTestData().WithSymbol(new string('x', Currency.SymbolMaxLength + 1));

            // Act
            var result = await services.BuildServiceProvider().GetRequiredService<ICurrencyValidator>().ValidateAsync(currency).ConfigureAwait(false);

            // Assert
            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(x => x.Severity == Severity.Error && x.ErrorCode == "MaximumLengthValidator" && x.PropertyName == nameof(Currency.Symbol));
        }
    }
}
#pragma warning restore CA1707 // Identifiers should not contain underscores
