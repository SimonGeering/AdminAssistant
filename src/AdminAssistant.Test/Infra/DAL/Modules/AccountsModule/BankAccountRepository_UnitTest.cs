#pragma warning disable CA1707 // Identifiers should not contain underscores
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdminAssistant.DomainModel;
using AdminAssistant.DomainModel.Modules.AccountsModule;
using AdminAssistant.DomainModel.Shared;
using AdminAssistant.Infra.DAL.EntityFramework;
using AdminAssistant.Infra.DAL.EntityFramework.Model.Accounts;
using AdminAssistant.Infra.Providers;
using AutoMapper;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using MockQueryable.Moq;
using Moq;
using Xunit;

namespace AdminAssistant.Infra.DAL.Modules.AccountsModule
{
    public class BankAccountRepository_UnitTest
    {
        [Fact]
        [Trait("Category", "Unit")]
        public async Task Returns_PopulatedBankAccountList_WhenDatabaseHasData()
        {
            // Arrange
            var mapper = new ServiceCollection().AddAutoMapper(typeof(MappingProfile)).BuildServiceProvider().GetRequiredService<IMapper>();
            var bankAccountList = new List<BankAccount>()
            {
                Factory.BankAccount.WithTestData(10).Build(),
                Factory.BankAccount.WithTestData(20).Build()
            };
            var data = mapper.Map<IList<BankAccountEntity>>(bankAccountList);

            var mockDbContext = new Mock<IApplicationDbContext>();
            mockDbContext.Setup(x => x.BankAccounts)
                .Returns(data.AsQueryable().BuildMockDbSet().Object);

            var services = new ServiceCollection();
            services.AddAutoMapper(typeof(MappingProfile));
            services.AddTransient((sp) => new Mock<IDateTimeProvider>().Object);
            services.AddTransient((sp) => new Mock<IUserContextProvider>().Object);
            services.AddAdminAssistantServerSideInfra(new ConfigurationSettings() { ConnectionString = "FakeConnectionString", DatabaseProvider = "SQLServerLocalDB" });
            services.AddTransient((sp) => mockDbContext.Object);

            // Act
            var result = await services.BuildServiceProvider().GetRequiredService<IBankAccountRepository>().GetListAsync().ConfigureAwait(false);

            // Assert
            result.Should().HaveCount(bankAccountList.Count);
            result.Should().BeEquivalentTo(bankAccountList);
        }

        [Fact]
        [Trait("Category", "Unit")]
        public async Task Returns_PopulatedBankAccountList_WhenDatabaseHasData()
        {
            // Arrange
            var mapper = new ServiceCollection().AddAutoMapper(typeof(MappingProfile)).BuildServiceProvider().GetRequiredService<IMapper>();
            var bankAccountList = new List<BankAccount>()
            {
                Factory.BankAccount.WithTestData(10).Build(),
                Factory.BankAccount.WithTestData(20).Build()
            };
            var data = mapper.Map<IList<BankAccountEntity>>(bankAccountList);

            var mockDbContext = new Mock<IApplicationDbContext>();
            mockDbContext.Setup(x => x.BankAccounts)
                .Returns(data.AsQueryable().BuildMockDbSet().Object);

            var services = new ServiceCollection();
            services.AddAutoMapper(typeof(MappingProfile));
            services.AddTransient((sp) => new Mock<IDateTimeProvider>().Object);
            services.AddTransient((sp) => new Mock<IUserContextProvider>().Object);
            services.AddAdminAssistantServerSideInfra(new ConfigurationSettings() { ConnectionString = "FakeConnectionString", DatabaseProvider = "SQLServerLocalDB" });
            services.AddTransient((sp) => mockDbContext.Object);

            // Act
            var result = await services.BuildServiceProvider().GetRequiredService<IBankAccountRepository>().GetListAsync().ConfigureAwait(false);

            // Assert
            result.Should().HaveCount(bankAccountList.Count);
            result.Should().BeEquivalentTo(bankAccountList);
        }

        [Fact]
        [Trait("Category", "Unit")]
        public async Task Returns_ABankAccount_WhenDatabaseContainseAnItemWithTheGivenID()
        {
            // Arrange
            var mapper = new ServiceCollection().AddAutoMapper(typeof(MappingProfile)).BuildServiceProvider().GetRequiredService<IMapper>();

            var bankAccountList = new List<BankAccount>()
            {
                Factory.BankAccount.WithTestData(10).Build(),
                Factory.BankAccount.WithTestData(20).Build()
            };
            var data = mapper.Map<IList<BankAccountEntity>>(bankAccountList);

            var mockDbContext = new Mock<IApplicationDbContext>();
            mockDbContext.Setup(x => x.BankAccounts)
                .Returns(data.AsQueryable().BuildMockDbSet().Object);

            var services = new ServiceCollection();
            services.AddAutoMapper(typeof(MappingProfile));
            services.AddTransient((sp) => new Mock<IDateTimeProvider>().Object);
            services.AddTransient((sp) => new Mock<IUserContextProvider>().Object);
            services.AddAdminAssistantServerSideInfra(new ConfigurationSettings() { ConnectionString = "FakeConnectionString", DatabaseProvider = "SQLServerLocalDB" });
            services.AddTransient((sp) => mockDbContext.Object);

            // Act
            var result = await services.BuildServiceProvider().GetRequiredService<IBankAccountRepository>().GetAsync(bankAccountList.First().BankAccountID).ConfigureAwait(false);

            // Assert
            result.Should().BeEquivalentTo(bankAccountList.First());
        }
    }
}
#pragma warning restore CA1707 // Identifiers should not contain underscores
