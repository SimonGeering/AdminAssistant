#pragma warning disable CA1707 // Identifiers should not contain underscores
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AdminAssistant.DomainModel;
using AdminAssistant.DomainModel.Modules.CoreModule;
using AdminAssistant.DomainModel.Modules.CoreModule.CQRS;
using Ardalis.Result;
using AutoMapper;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Xunit;

namespace AdminAssistant.WebAPI.v1.CoreModule
{
    public class CurrencyController_GetCurrency_Should
    {
        public class CurrencyController_Put_Should
        {
            // TODO: CurrencyController_Put UnitTests
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
                response.Result.Should().BeOfType<UnprocessableEntityObjectResult>();
                response.Value.Should().BeNull();

                var result = (UnprocessableEntityObjectResult)response.Result;
                var errors = (SerializableError)result.Value;

                foreach (var expectedErrorDetails in validationErrors)
                {
                    var messages = (string[])errors[expectedErrorDetails.Identifier];
                    messages.Should().Contain(expectedErrorDetails.ErrorMessage);
                }
            }
        }

        public class BankController_BankGetById_Should
        {
            [Fact()]
            [Trait("Category", "Unit")]
            public async Task Return_Ok200Bank_With_ABank_Given_AnExistingBankID()
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
                response.Result.Should().BeOfType<OkObjectResult>();
                response.Value.Should().BeNull();

                var result = (OkObjectResult)response.Result;
                result.Value.Should().BeAssignableTo<CurrencyResponseDto>();

                var value = (CurrencyResponseDto)result.Value;
                value.CurrencyID.Should().Be(currency.CurrencyID);
                value.Symbol.Should().Be(currency.Symbol);
                value.DecimalFormat.Should().Be(currency.DecimalFormat);
            }

            [Fact]
            [Trait("Category", "Unit")]
            public async Task Return_Status404NotFound_Given_ANonExistentBankID()
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
            response.Result.Should().BeOfType<OkObjectResult>();
            response.Value.Should().BeNull();

            var result = (OkObjectResult)response.Result;
            result.Value.Should().BeAssignableTo<IEnumerable<CurrencyResponseDto>>();

            var value = ((IEnumerable<CurrencyResponseDto>)result.Value).ToArray();
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
}
#pragma warning restore CA1707 // Identifiers should not contain underscores
