#pragma warning disable CA1707 // Identifiers should not contain underscores
using AdminAssistant.Domain;
using AdminAssistant.Modules.CoreModule;
using AdminAssistant.Modules.CoreModule.Commands;
using AdminAssistant.Modules.CoreModule.Queries;
using AdminAssistant.WebAPI.v1.CoreModule;
using Microsoft.AspNetCore.Mvc;
using MappingProfile = AdminAssistant.WebAPI.v1.MappingProfile;

namespace AdminAssistant.Test.WebAPI.v1.CoreModule;

public sealed class CurrencyController_Put_Should
{
    [Fact]
    [Trait("Category", "Unit")]
    public async Task Return_Status200OK_Given_AValidCurrency()
    {
        // Arrange
        var currency = Factory.Currency.WithTestData(10).Build();

        var services = new ServiceCollection();
        services.AddMockServerSideLogging();
        services.AddAutoMapper(typeof(MappingProfile));

        var mockMediator = new Mock<IMediator>();
        mockMediator.Setup(x => x.Send(It.IsAny<CurrencyUpdateCommand>(), It.IsAny<CancellationToken>()))
                    .Returns(ValueTask.FromResult(Result<Currency>.Success(currency)));

        services.AddTransient((sp) => mockMediator.Object);
        services.AddTransient<CurrencyController>();

        var container = services.BuildServiceProvider();
        var currencyRequest = new CurrencyUpdateRequestDto()
        {
            CurrencyID = currency.CurrencyID.Value,
            DecimalFormat = currency.DecimalFormat,
            Symbol = currency.Symbol
        };

        // Act
        var response = await container.GetRequiredService<CurrencyController>().CurrencyPut(currencyRequest, default);

        // Assert
        response.Value.ShouldBeNull();
        response.Result.ShouldNotBeNull();
        response.Result.ShouldBeOfType<OkObjectResult>();

        var result = (OkObjectResult)response.Result!;
        result.Value.ShouldNotBeNull();
        result.Value.ShouldBeAssignableTo<CurrencyResponseDto>();

        var value = (CurrencyResponseDto)result.Value!;
        value.CurrencyID.ShouldBe(currency.CurrencyID.Value);
        value.Symbol.ShouldBe(currency.Symbol);
        value.DecimalFormat.ShouldBe(currency.DecimalFormat);
    }

    [Fact]
    [Trait("Category", "Unit")]
    public async Task Return_Status404NotFound_Given_ANonExistentCurrencyID()
    {
        // Arrange
        var currency = Factory.Currency.WithTestData(10).Build();

        var services = new ServiceCollection();
        services.AddMocksOfExternalServerSideDependencies();

        var mockMediator = new Mock<IMediator>();
        mockMediator.Setup(x => x.Send(It.IsAny<CurrencyUpdateCommand>(), It.IsAny<CancellationToken>()))
                    .Returns(ValueTask.FromResult(Result<Currency>.NotFound()));

        services.AddTransient((sp) => mockMediator.Object);
        services.AddTransient<CurrencyController>();

        var container = services.BuildServiceProvider();

        var mapper = container.GetRequiredService<IMapper>();
        var currencyRequest = mapper.Map<CurrencyUpdateRequestDto>(currency);

        // Act
        var response = await services.BuildServiceProvider().GetRequiredService<CurrencyController>().CurrencyPut(currencyRequest, default);

        // Assert
        response.Result.ShouldBeOfType<NotFoundObjectResult>();
        response.Value.ShouldBeNull();
    }

    [Fact]
    [Trait("Category", "Unit")]
    public async Task Return_Status422UnprocessableEntity_Given_AnInvalidCurrency()
    {
        // Arrange
        var validationErrors = new List<ValidationError>()
            {
                new ValidationError() { Identifier="ExampleErrorCode", ErrorMessage="ExampleErrorMessage", Severity=ValidationSeverity.Error },
                new ValidationError() { Identifier="ExampleErrorCode2", ErrorMessage="ExampleErrorMessage2", Severity=ValidationSeverity.Error }
            };
        var currency = Factory.Currency.WithTestData(10).Build();

        var services = new ServiceCollection();
        services.AddMocksOfExternalServerSideDependencies();

        var mockMediator = new Mock<IMediator>();
        mockMediator.Setup(x => x.Send(It.IsAny<CurrencyUpdateCommand>(), It.IsAny<CancellationToken>()))
                    .Returns(ValueTask.FromResult(Result<Currency>.Invalid(validationErrors)));

        services.AddTransient((sp) => mockMediator.Object);
        services.AddTransient<CurrencyController>();

        var container = services.BuildServiceProvider();

        var mapper = container.GetRequiredService<IMapper>();
        var currencyRequest = mapper.Map<CurrencyUpdateRequestDto>(currency);

        // Act
        var response = await container.GetRequiredService<CurrencyController>().CurrencyPut(currencyRequest, default);

        // Assert
        response.Value.ShouldBeNull();
        response.Result.ShouldNotBeNull();
        response.Result.ShouldBeOfType<UnprocessableEntityObjectResult>();

        var result = (UnprocessableEntityObjectResult)response.Result!;
        result.Value.ShouldNotBeNull();
        var errors = (SerializableError)result.Value!;

        foreach (var expectedErrorDetails in validationErrors)
        {
            var messages = (string[])errors[expectedErrorDetails.Identifier];
            messages.ShouldContain(expectedErrorDetails.ErrorMessage);
        }
    }
}

public sealed class CurrencyController_CurrencyPost_Should
{
    [Fact]
    [Trait("Category", "Unit")]
    public async Task Return_Status201CreatedAtRoute_Given_AValidCurrency()
    {
        // Arrange
        var currency = Factory.Currency.WithTestData(10).Build();

        var services = new ServiceCollection();
        services.AddMockServerSideLogging();
        services.AddAutoMapper(typeof(MappingProfile));

        var mockMediator = new Mock<IMediator>();
        mockMediator.Setup(x => x.Send(It.IsAny<CurrencyCreateCommand>(), It.IsAny<CancellationToken>()))
                    .Returns(ValueTask.FromResult(Result<Currency>.Success(currency)));

        services.AddTransient((sp) => mockMediator.Object);
        services.AddTransient<CurrencyController>();

        var container = services.BuildServiceProvider();
        var currencyRequest = new CurrencyCreateRequestDto()
        {
            DecimalFormat = currency.DecimalFormat,
            Symbol = currency.Symbol
        };

        // Act
        var response = await container.GetRequiredService<CurrencyController>().CurrencyPost(currencyRequest, default);

        // Assert
        response.Value.ShouldBeNull();
        response.Result.ShouldNotBeNull();
        response.Result.ShouldBeOfType<CreatedAtRouteResult>();

        var result = (CreatedAtRouteResult)response.Result!;
        result.Value.ShouldNotBeNull();
        result.Value.ShouldBeAssignableTo<CurrencyResponseDto>();

        var value = (CurrencyResponseDto)result.Value!;
        value.CurrencyID.ShouldBe(currency.CurrencyID.Value);
        value.Symbol.ShouldBe(currency.Symbol);
        value.DecimalFormat.ShouldBe(currency.DecimalFormat);
    }

    [Fact]
    [Trait("Category", "Unit")]
    public async Task Return_Status422UnprocessableEntity_Given_AnInvalidCurrency()
    {
        // Arrange
        var validationErrors = new List<ValidationError>()
            {
                new ValidationError() { Identifier="ExampleErrorCode", ErrorMessage="ExampleErrorMessage", Severity=ValidationSeverity.Error },
                new ValidationError() { Identifier="ExampleErrorCode2", ErrorMessage="ExampleErrorMessage2", Severity=ValidationSeverity.Error }
            };
        var currency = Factory.Currency.WithTestData(10).Build();

        var services = new ServiceCollection();
        services.AddMocksOfExternalServerSideDependencies();

        var mockMediator = new Mock<IMediator>();
        mockMediator.Setup(x => x.Send(It.IsAny<CurrencyCreateCommand>(), It.IsAny<CancellationToken>()))
                    .Returns(ValueTask.FromResult(Result<Currency>.Invalid(validationErrors)));

        services.AddTransient((sp) => mockMediator.Object);
        services.AddTransient<CurrencyController>();

        var container = services.BuildServiceProvider();

        var mapper = container.GetRequiredService<IMapper>();
        var currencyRequest = mapper.Map<CurrencyCreateRequestDto>(currency);

        // Act
        var response = await container.GetRequiredService<CurrencyController>().CurrencyPost(currencyRequest, default);

        // Assert
        response.Value.ShouldBeNull();
        response.Result.ShouldNotBeNull();
        response.Result.ShouldBeOfType<UnprocessableEntityObjectResult>();

        var result = (UnprocessableEntityObjectResult)response.Result!;

        result.Value.ShouldNotBeNull();
        var errors = (SerializableError)result.Value!;

        foreach (var expectedErrorDetails in validationErrors)
        {
            var messages = (string[])errors[expectedErrorDetails.Identifier];
            messages.ShouldContain(expectedErrorDetails.ErrorMessage);
        }
    }
}

public sealed class CurrencyController_CurrencyGetById_Should
{
    [Fact]
    [Trait("Category", "Unit")]
    public async Task Return_Ok200Currency_With_ACurrency_Given_AnExistingCurrencyID()
    {
        // Arrange
        var currency = Factory.Currency.WithTestData(10).Build();

        var services = new ServiceCollection();
        services.AddMockServerSideLogging();
        services.AddAutoMapper(typeof(MappingProfile));

        var mockMediator = new Mock<IMediator>();
        mockMediator.Setup(x => x.Send(It.IsAny<CurrencyByIDQuery>(), It.IsAny<CancellationToken>()))
                    .Returns(ValueTask.FromResult(Result<Currency>.Success(currency)));

        services.AddTransient((sp) => mockMediator.Object);
        services.AddTransient<CurrencyController>();

        // Act
        var response = await services.BuildServiceProvider().GetRequiredService<CurrencyController>().CurrencyGetById(currency.CurrencyID.Value, default);

        // Assert
        response.Value.ShouldBeNull();
        response.Result.ShouldNotBeNull();
        response.Result.ShouldBeOfType<OkObjectResult>();

        var result = (OkObjectResult)response.Result!;
        result.Value.ShouldNotBeNull();
        result.Value.ShouldBeAssignableTo<CurrencyResponseDto>();

        var value = (CurrencyResponseDto)result.Value!;
        value.CurrencyID.ShouldBe(currency.CurrencyID.Value);
        value.Symbol.ShouldBe(currency.Symbol);
        value.DecimalFormat.ShouldBe(currency.DecimalFormat);
    }

    [Fact]
    [Trait("Category", "Unit")]
    public async Task Return_Status404NotFound_Given_ANonExistentCurrencyID()
    {
        // Arrange
        var services = new ServiceCollection();
        services.AddMocksOfExternalServerSideDependencies();

        var mockMediator = new Mock<IMediator>();
        mockMediator.Setup(x => x.Send(It.IsAny<CurrencyByIDQuery>(), It.IsAny<CancellationToken>()))
                    .Returns(ValueTask.FromResult(Result<Currency>.NotFound()));

        services.AddTransient((sp) => mockMediator.Object);
        services.AddTransient<CurrencyController>();

        // Act
        var response = await services.BuildServiceProvider().GetRequiredService<CurrencyController>().CurrencyGetById(10, default);

        // Assert
        response.Result.ShouldBeOfType<NotFoundResult>();
        response.Value.ShouldBeNull();
    }
}

public sealed class CurrencyController_GetCurrency_Should
{
    [Fact]
    [Trait("Category", "Unit")]
    public async Task Return_Status200OK_With_AListOfCurrency_Given_NoArguments()
    {
        // Arrange
        var currencies = new List<Currency>()
            {
                Factory.Currency.WithTestData(10).Build(),
                Factory.Currency.WithTestData(20).Build()
            };

        var services = new ServiceCollection();
        services.AddMockServerSideLogging();
        services.AddAutoMapper(typeof(MappingProfile));

        var mockMediator = new Mock<IMediator>();
        mockMediator.Setup(x => x.Send(It.IsAny<CurrenciesQuery>(), It.IsAny<CancellationToken>()))
                    .Returns(ValueTask.FromResult(Result<IEnumerable<Currency>>.Success(currencies)));

        services.AddTransient((sp) => mockMediator.Object);
        services.AddTransient<CurrencyController>();

        // Act
        var response = await services.BuildServiceProvider().GetRequiredService<CurrencyController>().GetCurrency(default);

        // Assert
        response.Value.ShouldBeNull();
        response.Result.ShouldNotBeNull();
        response.Result.ShouldBeOfType<OkObjectResult>();

        var result = (OkObjectResult)response.Result!;
        result.Value.ShouldBeAssignableTo<IEnumerable<CurrencyResponseDto>>();

        result.Value.ShouldNotBeNull();
        var value = ((IEnumerable<CurrencyResponseDto>)result.Value!).ToArray();
        value.Length.ShouldBe(currencies.Count);

        var expected = currencies.ToArray();
        for (var index = 0; index < expected.Length; index++)
        {
            value[index].CurrencyID.ShouldBe(expected[index].CurrencyID.Value);
            value[index].Symbol.ShouldBe(expected[index].Symbol);
            value[index].DecimalFormat.ShouldBe(expected[index].DecimalFormat);
        }
    }
}
#pragma warning restore CA1707 // Identifiers should not contain underscores
