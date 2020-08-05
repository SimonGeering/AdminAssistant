#pragma warning disable CA1707 // Identifiers should not contain underscores
using AutoMapper;
using System;
using FluentAssertions;
using Xunit;
using AdminAssistant.DomainModel.Modules.AccountsModule;
using AdminAssistant.DAL.EntityFramework.Model.Accounts;
using AdminAssistant.DAL.EntityFramework.Model.Core;

namespace AdminAssistant.DAL
{
    public class DALMappingProfile_Should
    {
        private readonly IConfigurationProvider configuration;
        private readonly IMapper mapper;

        public DALMappingProfile_Should()
        {
            this.configuration = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>());

            this.mapper = this.configuration.CreateMapper();
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void HaveValidConfiguration()
        {
            // Arrange

            // Act
            this.configuration.AssertConfigurationIsValid();

            // Assert
        }

        [Theory]
        [Trait("Category", "Unit")]
        [InlineData(typeof(BankEntity), typeof(Bank))]
        [InlineData(typeof(BankAccountTypeEntity), typeof(BankAccountType))]
        [InlineData(typeof(CurrencyEntity), typeof(Currency))]
        [InlineData(typeof(BankAccountTransactionEntity), typeof(BankAccountTransaction))]
        [InlineData(typeof(Bank), typeof(BankEntity))]
        [InlineData(typeof(BankAccountTransaction), typeof(BankAccountTransactionEntity))]
        public void ShouldSupportMappingFromSourceToDestination(Type source, Type destination)
        {
            // Arrange
            var instance = Activator.CreateInstance(source);

            // Act
            var result = this.mapper.Map(instance, source, destination);

            // Assert
            result.Should().NotBeNull();
        }
    }
}
#pragma warning restore CA1707 // Identifiers should not contain underscores
