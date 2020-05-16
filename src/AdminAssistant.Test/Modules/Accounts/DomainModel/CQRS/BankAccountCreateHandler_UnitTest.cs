#pragma warning disable CA1707 // Identifiers should not contain underscores
using AdminAssistant.DAL.Modules.Accounts;
using Ardalis.Result;
using FluentAssertions;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;
using Microsoft.Extensions.Logging;
using System;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace AdminAssistant.DomainModel.Modules.Accounts.CQRS
{
    public class BankAccountCreateHandler_Should
    {
        private readonly ITestOutputHelper _testOutputHelper;

        public BankAccountCreateHandler_Should(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        [Fact]
        [Trait("Category", "Unit")]
        public async Task Return_ABankAccount_GivenAValidBankAccountCreateCommand()
        {
            // Arrange
            var services = new ServiceCollection();
            services.AddMockLogging();
            services.AddAdminAssistantServerSideDomainModel();
            services.AddTransient((sp) => new Mock<IBankAccountRepository>().Object);

            var bankAccount = TestData.BankAccountBuilder.WithTestData().Build();

            // Act
            var result = await services.BuildServiceProvider().GetRequiredService<IMediator>().Send(new BankAccountCreateCommand(bankAccount)).ConfigureAwait(false);

            // Assert
            result.Should().NotBeNull();
            result.Status.Should().Be(ResultStatus.Ok);
            //result.ValidationErrors.Should().NotBeEmpty();
        }

        [Fact]
        [Trait("Category", "Unit")]
        public async Task Return_ValidationError_GivenAnInvalidBankAccountCreateCommand()
        {
            Console.WriteLine("Moo");

            // Arrange
            var services = new ServiceCollection();
            services.AddMockLogging();
            services.AddAdminAssistantServerSideDomainModel();
            services.AddTransient((sp) => new Mock<IBankAccountRepository>().Object);

            var bankAccount = TestData.BankAccountBuilder.WithTestData().WithAccountName(string.Empty).Build();

            // Act
            var result = await services.BuildServiceProvider().GetRequiredService<IMediator>().Send(new BankAccountCreateCommand(bankAccount)).ConfigureAwait(false);

            // Assert
            result.Should().NotBeNull();
            result.Status.Should().Be(ResultStatus.Invalid);
            result.ValidationErrors.Should().NotBeEmpty();
        }
    }
}
#pragma warning restore CA1707 // Identifiers should not contain underscores
