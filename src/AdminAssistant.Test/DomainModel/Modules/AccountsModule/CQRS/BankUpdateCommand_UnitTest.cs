#pragma warning disable CA1707 // Identifiers should not contain underscores
using System.Collections.Generic;
using System.Threading.Tasks;
using AdminAssistant.Infra.DAL.Modules.AccountsModule;
using Ardalis.Result;
using FluentAssertions;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using ObjectCloner.Extensions;
using Xunit;

namespace AdminAssistant.DomainModel.Modules.AccountsModule.CQRS
{
    public class BankUpdateCommand_Should
    {
        [Fact]
        [Trait("Category", "Unit")]
        public async Task SaveAndReturn_APersistedBank_GivenAValidBank()
        {
            // Arrange
            var bank = Factory.Bank.WithTestData(10).Build();

            var services = new ServiceCollection();
            services.AddMockServerSideLogging();
            services.AddAdminAssistantServerSideDomainModel();

            var mockBankRepository = new Mock<IBankRepository>();
            mockBankRepository.Setup(x => x.SaveAsync(bank))
                              .Returns(Task.FromResult(bank));

            services.AddTransient((sp) => mockBankRepository.Object);

            // Act
            var result = await services.BuildServiceProvider().GetRequiredService<IMediator>().Send(new BankUpdateCommand(bank)).ConfigureAwait(false);

            // Assert
            result.Should().NotBeNull();
            result.Status.Should().Be(ResultStatus.Ok);
            result.ValidationErrors.Should().BeEmpty();
            result.Value.Should().NotBeNull();
            result.Value.BankID.Should().BeGreaterThan(Constants.NewRecordID);
        }

        [Fact]
        [Trait("Category", "Unit")]
        public async Task Return_ValidationError_GivenAnInvalidBank()
        {
            // Arrange
            var services = new ServiceCollection();
            services.AddMockServerSideLogging();
            services.AddAdminAssistantServerSideDomainModel();
            services.AddTransient((sp) => new Mock<IBankRepository>().Object);

            var bank = Factory.Bank.WithTestData()
                                   .WithBankName(string.Empty)
                                   .Build();
            // Act
            var result = await services.BuildServiceProvider().GetRequiredService<IMediator>().Send(new BankUpdateCommand(bank)).ConfigureAwait(false);

            // Assert
            result.Should().NotBeNull();
            result.Status.Should().Be(ResultStatus.Invalid);
            result.ValidationErrors.Should().NotBeEmpty();
        }
        // TODO: Add test for BankUpdateCommand where BankID not in IBankRepository
    }
}
#pragma warning restore CA1707 // Identifiers should not contain underscores
