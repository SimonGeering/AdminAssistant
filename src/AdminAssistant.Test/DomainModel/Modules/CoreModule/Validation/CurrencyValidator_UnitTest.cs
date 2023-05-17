#pragma warning disable CA1707 // Identifiers should not contain underscores

using AdminAssistant.DomainModel;
using AdminAssistant.DomainModel.Modules.CoreModule;
using AdminAssistant.DomainModel.Modules.CoreModule.Validation;

namespace AdminAssistant.Test.DomainModel.Modules.CoreModule.Validation;

public sealed class CurrencyValidator_Should
{
    [Fact]
    [Trait("Category", "Unit")]
    public async Task Return_IsValid_GivenAValidCurrency()
    {
        // Arrange
        var services = new ServiceCollection();
        services.AddAdminAssistantClientSideDomainModel();

        var currency = Factory.Currency.WithTestData().Build();

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
        services.AddAdminAssistantClientSideDomainModel();

        var currency = Factory.Currency.WithTestData()
                                       .WithoutADecimalFormat()
                                       .Build();
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
        services.AddAdminAssistantClientSideDomainModel();

        var currency = Factory.Currency.WithTestData()
                                       .WithDecimalFormat(new string('x', Currency.DecimalFormatMaxLength + 1))
                                       .Build();
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
        services.AddAdminAssistantClientSideDomainModel();

        var currency = Factory.Currency.WithTestData()
                                       .WithoutASymbol()
                                       .Build();
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
        services.AddAdminAssistantClientSideDomainModel();

        var currency = Factory.Currency.WithTestData()
                                       .WithSymbol(new string('x', Currency.SymbolMaxLength + 1))
                                       .Build();
        // Act
        var result = await services.BuildServiceProvider().GetRequiredService<ICurrencyValidator>().ValidateAsync(currency).ConfigureAwait(false);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(x => x.Severity == Severity.Error && x.ErrorCode == "MaximumLengthValidator" && x.PropertyName == nameof(Currency.Symbol));
    }
}
#pragma warning restore CA1707 // Identifiers should not contain underscores
