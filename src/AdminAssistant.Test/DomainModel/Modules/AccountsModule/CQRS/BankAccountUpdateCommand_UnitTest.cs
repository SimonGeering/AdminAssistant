#pragma warning disable CA1707 // Identifiers should not contain underscores
using System.Collections.Generic;
using System.Threading.Tasks;
using AdminAssistant.DAL.Modules.AccountsModule;
using Ardalis.Result;
using FluentAssertions;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Xunit;

namespace AdminAssistant.DomainModel.Modules.AccountsModule.CQRS
{
    public class BankAccountUpdateCommand_Should
    {
        [Fact(Skip = "TODO")]
        [Trait("Category", "Unit")]
        public async Task SaveAndReturn_AnUpdateBankAccount()
        {
            //// Arrange
            //var bankList = new List<Bank>() { Factory.Bank.WithTestData(20).Build() };

            var services = new ServiceCollection();
            //services.AddMockLogging();
            //services.AddAdminAssistantServerSideDomainModel();

            //var mockBankRepository = new Mock<IBankRepository>();
            //mockBankRepository.Setup(x => x.GetListAsync()).Returns(Task.FromResult<List<Bank>>(bankList));

            //services.AddTransient((sp) => mockBankRepository.Object);

            // Act
            var result = await services.BuildServiceProvider().GetRequiredService<IMediator>().Send(null).ConfigureAwait(false);

            //// Assert
            //result.Status.Should().Be(ResultStatus.Ok);
            //result.Value.Should().BeEquivalentTo(bankList);            
        }

        [Fact(Skip = "TODO")]
        [Trait("Category", "Unit")]
        public async Task Return_ValidationError_GivenAnInvalidBankAccountCreateCommand()
        {
            // Arrange
            var services = new ServiceCollection();
            services.AddMockLogging();
            services.AddAdminAssistantServerSideDomainModel();
            services.AddTransient((sp) => new Mock<IBankAccountRepository>().Object);

            var bankAccount = Factory.BankAccount.WithTestData()
                                                 .WithAccountName(string.Empty)
                                                 .Build();
            // Act
            var result = await services.BuildServiceProvider().GetRequiredService<IMediator>().Send(new BankAccountUpdateCommand(bankAccount)).ConfigureAwait(false);

            // Assert
            result.Should().NotBeNull();
            result.Status.Should().Be(ResultStatus.Invalid);
            result.ValidationErrors.Should().NotBeEmpty();
        }
    }
}
#pragma warning restore CA1707 // Identifiers should not contain underscores
