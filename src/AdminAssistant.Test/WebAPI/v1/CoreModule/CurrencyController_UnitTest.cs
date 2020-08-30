#pragma warning disable CA1707 // Identifiers should not contain underscores
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AdminAssistant.DomainModel;
using AdminAssistant.DomainModel.Modules.AccountsModule;
using AdminAssistant.DomainModel.Modules.AccountsModule.CQRS;
using Ardalis.Result;
using AutoMapper;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Xunit;

namespace AdminAssistant.WebAPI.v1.Core
{
    public class CurrencyController_GetCurrency_Should
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
