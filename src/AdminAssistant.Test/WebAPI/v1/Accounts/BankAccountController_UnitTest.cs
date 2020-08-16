#pragma warning disable CA1707 // Identifiers should not contain underscores
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
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

namespace AdminAssistant.WebAPI.v1.Accounts
{
    public class BankAccountController_Put_Should
    {
    }

    public class BankAccountController_Post_Should
    {
        [Fact]
        [Trait("Category", "Unit")]
        public async Task Return_Status422UnprocessableEntity_Given_AnInvalidBankAccount()
        {
            // Arrange
            var validationErrors = new Dictionary<string, string>()
            {
                { "ExampleErrorCode", "ExampleErrorMessage" },
                { "ExampleErrorCode2", "ExampleErrorMessage2" }
            };
            var bankAccount = Factory.BankAccount.WithTestData(10).Build();

            var services = new ServiceCollection();
            services.AddMocksOfExternalServerSideDependencies();

            var mockMediator = new Mock<IMediator>();
            mockMediator.Setup(x => x.Send(It.IsAny<BankAccountCreateCommand>(), It.IsAny<CancellationToken>()))
                        .Returns(Task.FromResult(Result<BankAccount>.Invalid(validationErrors)));

            services.AddTransient((sp) => mockMediator.Object);
            services.AddTransient<BankAccountController>();

            var container = services.BuildServiceProvider();

            var mapper = container.GetRequiredService<IMapper>();
            var bankAccountRequest = mapper.Map<BankAccountCreateRequestDto>(bankAccount);

            // Act
            var response = await container.GetRequiredService<BankAccountController>().Post(bankAccountRequest).ConfigureAwait(false);

            // Assert
            response.Result.Should().BeOfType<UnprocessableEntityObjectResult>();
            response.Value.Should().BeNull();

            var result = (UnprocessableEntityObjectResult)response.Result;

            var value = (SerializableError)result.Value;
            value.Keys.Should().BeEquivalentTo(validationErrors.Keys);

            //var moo = (value as Dictionary<string, object>);
            //var moo2 = moo as Dictionary<string, string>;
            //value.Values.Should().BeEquivalentTo(validationErrors.Values);
            //value.Should().HaveCount(banks.Count);

            //var expected = banks.ToArray();
            //for (int i = 0; i < expected.Length; i++)
            //{
            //    value[i].BankID.Should().Be(expected[i].BankID);
            //    value[i].BankName.Should().Be(expected[i].BankName);
            //}
        }
    }

    public class BankAccountController_GetBankAccountById_Should
    {
        [Fact]
        [Trait("Category", "Unit")]
        public async Task Return_Status200Ok_With_ABankAccount_Given_AnExistingBankAccountID()
        {
            // Arrange
            var bankAccount = Factory.BankAccount.WithTestData(10).Build();

            var services = new ServiceCollection();
            services.AddMockServerSideLogging();
            services.AddAutoMapper(typeof(MappingProfile));

            var mockMediator = new Mock<IMediator>();
            mockMediator.Setup(x => x.Send(It.IsAny<BankAccountByIDQuery>(), It.IsAny<CancellationToken>()))
                        .Returns(Task.FromResult(Result<BankAccount>.Success(bankAccount)));

            services.AddTransient((sp) => mockMediator.Object);
            services.AddTransient<BankAccountController>();

            // Act
            var response = await services.BuildServiceProvider().GetRequiredService<BankAccountController>().GetBankAccountById(bankAccount.BankAccountID).ConfigureAwait(false);

            // Assert
            response.Result.Should().BeOfType<OkObjectResult>();
            response.Value.Should().BeNull();

            var result = (OkObjectResult)response.Result;
            result.Value.Should().BeAssignableTo<BankAccountResponseDto>();

            var value = ((BankAccountResponseDto)result.Value);
            value.BankAccountID.Should().Be(bankAccount.BankAccountID);
            value.AccountName.Should().Be(bankAccount.AccountName);
        }

        [Fact]
        [Trait("Category", "Unit")]
        public async Task Return_Status404NotFound_Given_ANonExistentBankAccountID()
        {
            // Arrange
            var services = new ServiceCollection();
            services.AddMocksOfExternalServerSideDependencies();

            var mockMediator = new Mock<IMediator>();
            mockMediator.Setup(x => x.Send(It.IsAny<BankAccountByIDQuery>(), It.IsAny<CancellationToken>()))
                        .Returns(Task.FromResult(Result<BankAccount>.NotFound()));

            services.AddTransient((sp) => mockMediator.Object);
            services.AddTransient<BankAccountController>();

            // Act
            var response = await services.BuildServiceProvider().GetRequiredService<BankAccountController>().GetBankAccountById(10).ConfigureAwait(false);

            // Assert
            response.Result.Should().BeOfType<NotFoundResult>();
            response.Value.Should().BeNull();
        }
    }

    public class BankAccountController_GetBankAccountTransactionList_Should
    {
        [Fact(Skip="WIP")]
        [Trait("Category", "Unit")]
        public async Task Return_Status200Ok_With_AListOfBankAccountTransaction_Given_AnExistingBankAccountID()
        {
            // Arrange
            var bankAccountTransactionList = new List<BankAccountTransaction>()
            {
                Factory.BankAccountTransaction.WithTestData(10).Build(),
                Factory.BankAccountTransaction.WithTestData(20).Build(),
                Factory.BankAccountTransaction.WithTestData(30).Build()
            };

            var services = new ServiceCollection();
            services.AddMockServerSideLogging();
            services.AddAutoMapper(typeof(MappingProfile));

            var mockMediator = new Mock<IMediator>();
            mockMediator.Setup(x => x.Send(It.IsAny<BankAccountTransactionsByBankAccountIDQuery>(), It.IsAny<CancellationToken>()))
                        .Returns(Task.FromResult(Result<IEnumerable<BankAccountTransaction>>.Success(bankAccountTransactionList)));

            services.AddTransient((sp) => mockMediator.Object);
            services.AddTransient<BankAccountController>();

            var container = services.BuildServiceProvider();

            // Act
            var response = await container.GetRequiredService<BankAccountController>().GetBankAccountTransactionListAsync(bankAccountTransactionList.First().BankAccountID).ConfigureAwait(false);

            // Assert
            var result = (OkObjectResult)response.Result;
            result.Value.Should().BeAssignableTo<IEnumerable<BankAccountTransactionResponseDto>>();

            var value = ((IEnumerable<BankAccountTransactionResponseDto>)result.Value).ToArray();
            value.Should().HaveCount(bankAccountTransactionList.Count);

            var expected = bankAccountTransactionList.ToArray();
            for (int i = 0; i < expected.Length; i++)
            {
                // TODO: Switch this to use AutoMapper.
                value[i].BankAccountTransactionID.Should().Be(expected[i].BankAccountTransactionID);
                value[i].BankAccountID.Should().Be(expected[i].BankAccountID);
            }
        }

        [Fact]
        [Trait("Category", "Unit")]
        public async Task Return_Status404NotFound_Given_ANonExistentBankAccountID()
        {
            // Arrange
            var services = new ServiceCollection();
            services.AddMocksOfExternalServerSideDependencies();

            var mockMediator = new Mock<IMediator>();
            mockMediator.Setup(x => x.Send(It.IsAny<BankAccountTransactionsByBankAccountIDQuery>(), It.IsAny<CancellationToken>()))
                        .Returns(Task.FromResult(Result<IEnumerable<BankAccountTransaction>>.NotFound()));

            services.AddTransient((sp) => mockMediator.Object);
            services.AddTransient<BankAccountController>();

            // Act
            var response = await services.BuildServiceProvider().GetRequiredService<BankAccountController>().GetBankAccountTransactionListAsync(Constants.UnknownRecordID).ConfigureAwait(false);

            // Assert
            response.Result.Should().BeOfType<NotFoundResult>();
            response.Value.Should().BeNull();
        }
    }
}
#pragma warning restore CA1707 // Identifiers should not contain underscores
