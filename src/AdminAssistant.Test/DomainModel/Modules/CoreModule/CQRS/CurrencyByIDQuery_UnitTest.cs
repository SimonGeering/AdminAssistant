#pragma warning disable CA1707 // Identifiers should not contain underscores
using AdminAssistant.Infra.DAL.Modules.CoreModule;
using Ardalis.Result;
using FluentAssertions;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Xunit;

namespace AdminAssistant.DomainModel.Modules.CoreModule.CQRS
{
    public class CurrencyByIDQuery_Should
    {
        [Fact]
        [Trait("Category", "Unit")]
        public async Task Return_NotFound_GivenANonExistentCurrencyID()
        {
            // Arrange
            var nonExistentCurrencyID = Constants.UnknownRecordID;

            var services = new ServiceCollection();
            services.AddMockServerSideLogging();
            services.AddAdminAssistantServerSideDomainModel();

            var mockCurrencyRepository = new Mock<ICurrencyRepository>();
            mockCurrencyRepository.Setup(x => x.GetAsync(nonExistentCurrencyID)).Returns(Task.FromResult<Currency?>(null!));

            services.AddTransient((sp) => mockCurrencyRepository.Object);

            // Act
            var result = await services.BuildServiceProvider().GetRequiredService<IMediator>().Send(new CurrencyByIDQuery(nonExistentCurrencyID)).ConfigureAwait(false);

            // Assert
            result.Status.Should().Be(ResultStatus.NotFound);
        }

        [Fact]
        [Trait("Category", "Unit")]
        public async Task Return_OkCurrency_GivenAnExistingCurrencyID()
        {
            // Arrange
            var currency = Factory.Currency.WithTestData(10).Build();

            var services = new ServiceCollection();
            services.AddMockServerSideLogging();
            services.AddAdminAssistantServerSideDomainModel();

            var mockCurrencyRepository = new Mock<ICurrencyRepository>();
            mockCurrencyRepository.Setup(x => x.GetAsync(currency.CurrencyID)).Returns(Task.FromResult<Currency?>(currency));

            services.AddTransient((sp) => mockCurrencyRepository.Object);

            // Act
            var result = await services.BuildServiceProvider().GetRequiredService<IMediator>().Send(new CurrencyByIDQuery(currency.CurrencyID)).ConfigureAwait(false);

            // Assert
            result.Status.Should().Be(ResultStatus.Ok);
            result.Value.Should().Be(currency);
        }
    }
}
#pragma warning restore CA1707 // Identifiers should not contain underscores
