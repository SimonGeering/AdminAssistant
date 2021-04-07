#pragma warning disable CA1707 // Identifiers should not contain underscores
using System.Collections.Generic;
using System.Threading.Tasks;
using AdminAssistant.Infra.DAL.Modules.CoreModule;
using Ardalis.Result;
using FluentAssertions;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using ObjectCloner.Extensions;
using Xunit;

namespace AdminAssistant.DomainModel.Modules.CoreModule.CQRS
{
    public class BankUpdateCommand_Should
    {
        [Fact]
        [Trait("Category", "Unit")]
        public async Task SaveAndReturn_APersistedBank_GivenAValidBank()
        {
            // Arrange
            var currency = Factory.Currency.WithTestData(10).Build();

            var services = new ServiceCollection();
            services.AddMockServerSideLogging();
            services.AddAdminAssistantServerSideDomainModel();

            var mockRepository = new Mock<ICurrencyRepository>();
            mockRepository.Setup(x => x.SaveAsync(currency))
                          .Returns(Task.FromResult(currency));

            services.AddTransient((sp) => mockRepository.Object);

            // Act
            var result = await services.BuildServiceProvider().GetRequiredService<IMediator>().Send(new CurrencyUpdateCommand(currency)).ConfigureAwait(false);

            // Assert
            result.Should().NotBeNull();
            result.Status.Should().Be(ResultStatus.Ok);
            result.ValidationErrors.Should().BeEmpty();
            result.Value.Should().NotBeNull();
            result.Value.CurrencyID.Should().BeGreaterThan(Constants.NewRecordID);
        }

        [Fact]
        [Trait("Category", "Unit")]
        public async Task Return_ValidationError_GivenAnInvalidBank()
        {
            // Arrange
            var services = new ServiceCollection();
            services.AddMockServerSideLogging();
            services.AddAdminAssistantServerSideDomainModel();
            services.AddTransient((sp) => new Mock<ICurrencyRepository>().Object);

            var currency = Factory.Currency.WithTestData()
                                           .WithoutASymbol()
                                           .Build();
            // Act
            var result = await services.BuildServiceProvider().GetRequiredService<IMediator>().Send(new CurrencyUpdateCommand(currency)).ConfigureAwait(false);

            // Assert
            result.Should().NotBeNull();
            result.Status.Should().Be(ResultStatus.Invalid);
            result.ValidationErrors.Should().NotBeEmpty();
        }
        // TODO: Add test for CurrencyUpdateCommand where CurrencyID not in ICurrencyRepository
    }
}
#pragma warning restore CA1707 // Identifiers should not contain underscores
