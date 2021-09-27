#pragma warning disable CA1707 // Identifiers should not contain underscores
using AdminAssistant.DomainModel;
using AdminAssistant.DomainModel.Modules.CoreModule;
using AdminAssistant.DomainModel.Modules.CoreModule.CQRS;
using Microsoft.AspNetCore.Mvc;

namespace AdminAssistant.WebAPI.v1.CoreModule;

public class CurrencyController_GetCurrency_Should
{
    public class CurrencyController_Put_Should
    {
        [Fact()]
        [Trait("Category", "Unit")]
        public async Task Return_Status404NotFound_Given_ANonExistentCurrencyID()
        {
            // Arrange
            var currency = Factory.Currency.WithTestData(10).Build();

            var services = new ServiceCollection();
            services.AddMocksOfExternalServerSideDependencies();

            var mockMediator = new Mock<IMediator>();
            mockMediator.Setup(x => x.Send(It.IsAny<CurrencyUpdateCommand>(), It.IsAny<CancellationToken>()))
                        .Returns(Task.FromResult(Result<Currency>.NotFound()));

            services.AddTransient((sp) => mockMediator.Object);
            services.AddTransient<CurrencyController>();

            var container = services.BuildServiceProvider();

            var mapper = container.GetRequiredService<IMapper>();
            var currencyRequest = mapper.Map<CurrencyUpdateRequestDto>(currency);

            // Act
            var response = await services.BuildServiceProvider().GetRequiredService<CurrencyController>().CurrencyPut(currencyRequest).ConfigureAwait(false);

            // Assert
            response.Result.Should().BeOfType<NotFoundObjectResult>();
            response.Value.Should().BeNull();
        }

        // TODO: CurrencyController_Put UnitTests
        [Fact()]
        [Trait("Category", "Unit")]
        public async Task Return_Status404NotFound_Given_AnInvalidCurrency()
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
                        .Returns(Task.FromResult(Result<Currency>.Invalid(validationErrors)));

            services.AddTransient((sp) => mockMediator.Object);
            services.AddTransient<CurrencyController>();

            var container = services.BuildServiceProvider();

            var mapper = container.GetRequiredService<IMapper>();
            var currencyRequest = mapper.Map<CurrencyUpdateRequestDto>(currency);

            // Act
            var response = await container.GetRequiredService<CurrencyController>().CurrencyPut(currencyRequest).ConfigureAwait(false);

            // Assert
            response.Value.Should().BeNull();
            response.Result.Should().NotBeNull();
            response.Result.Should().BeOfType<UnprocessableEntityObjectResult>();

            var result = (UnprocessableEntityObjectResult)response.Result!;
            result.Value.Should().NotBeNull();
            var errors = (SerializableError)result.Value!;

            foreach (var expectedErrorDetails in validationErrors)
            {
                var messages = (string[])errors[expectedErrorDetails.Identifier];
                messages.Should().Contain(expectedErrorDetails.ErrorMessage);
            }
        }
    }

    public class CurrencyController_CurrencyPost_Should
    {
        [Fact()]
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
                        .Returns(Task.FromResult(Result<Currency>.Invalid(validationErrors)));

            services.AddTransient((sp) => mockMediator.Object);
            services.AddTransient<CurrencyController>();

            var container = services.BuildServiceProvider();

            var mapper = container.GetRequiredService<IMapper>();
            var currencyRequest = mapper.Map<CurrencyCreateRequestDto>(currency);

            // Act
            var response = await container.GetRequiredService<CurrencyController>().CurrencyPost(currencyRequest).ConfigureAwait(false);

            // Assert
            response.Value.Should().BeNull();
            response.Result.Should().NotBeNull();
            response.Result.Should().BeOfType<UnprocessableEntityObjectResult>();

            var result = (UnprocessableEntityObjectResult)response.Result!;

            result.Value.Should().NotBeNull();
            var errors = (SerializableError)result.Value!;

            foreach (var expectedErrorDetails in validationErrors)
            {
                var messages = (string[])errors[expectedErrorDetails.Identifier];
                messages.Should().Contain(expectedErrorDetails.ErrorMessage);
            }
        }
    }

    public class CurrencyController_CurrencyGetById_Should
    {
        [Fact()]
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
                        .Returns(Task.FromResult(Result<Currency>.Success(currency)));

            services.AddTransient((sp) => mockMediator.Object);
            services.AddTransient<CurrencyController>();

            // Act
            var response = await services.BuildServiceProvider().GetRequiredService<CurrencyController>().CurrencyGetById(currency.CurrencyID).ConfigureAwait(false);

            // Assert
            response.Value.Should().BeNull();
            response.Result.Should().NotBeNull();
            response.Result.Should().BeOfType<OkObjectResult>();

            var result = (OkObjectResult)response.Result!;
            result.Value.Should().NotBeNull();
            result.Value.Should().BeAssignableTo<CurrencyResponseDto>();

            var value = (CurrencyResponseDto)result.Value!;
            value.CurrencyID.Should().Be(currency.CurrencyID);
            value.Symbol.Should().Be(currency.Symbol);
            value.DecimalFormat.Should().Be(currency.DecimalFormat);
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
                        .Returns(Task.FromResult(Result<Currency>.NotFound()));

            services.AddTransient((sp) => mockMediator.Object);
            services.AddTransient<CurrencyController>();

            // Act
            var response = await services.BuildServiceProvider().GetRequiredService<CurrencyController>().CurrencyGetById(10).ConfigureAwait(false);

            // Assert
            response.Result.Should().BeOfType<NotFoundResult>();
            response.Value.Should().BeNull();
        }
    }

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
                    .Returns(Task.FromResult(Result<IEnumerable<Currency>>.Success(currencies)));

        services.AddTransient((sp) => mockMediator.Object);
        services.AddTransient<CurrencyController>();

        // Act
        var response = await services.BuildServiceProvider().GetRequiredService<CurrencyController>().GetCurrency().ConfigureAwait(false);

        // Assert
        response.Value.Should().BeNull();
        response.Result.Should().NotBeNull();
        response.Result.Should().BeOfType<OkObjectResult>();

        var result = (OkObjectResult)response.Result!;
        result.Value.Should().BeAssignableTo<IEnumerable<CurrencyResponseDto>>();

        result.Value.Should().NotBeNull();
        var value = ((IEnumerable<CurrencyResponseDto>)result.Value!).ToArray();
        value.Should().HaveCount(currencies.Count);

        var expected = currencies.ToArray();
        for (var index = 0; index < expected.Length; index++)
        {
            value[index].CurrencyID.Should().Be(expected[index].CurrencyID);
            value[index].Symbol.Should().Be(expected[index].Symbol);
            value[index].DecimalFormat.Should().Be(expected[index].DecimalFormat);
        }
    }
}
#pragma warning restore CA1707 // Identifiers should not contain underscores
