#pragma warning disable CA1707 // Identifiers should not contain underscores
using AutoMapper;
using System;
using FluentAssertions;
using Xunit;
using AdminAssistant.DomainModel.Modules.AccountsModule;

namespace AdminAssistant.WebAPI.v1
{
    public class WebAPIMappingProfile_Should
    {
        private readonly IConfigurationProvider configuration;
        private readonly IMapper mapper;

        public WebAPIMappingProfile_Should()
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
        [InlineData(typeof(Currency), typeof(v1.CurrencyResponseDto))]
        [InlineData(typeof(Bank), typeof(v1.BankResponseDto))]
        [InlineData(typeof(BankAccount), typeof(v1.BankAccountResponseDto))]
        [InlineData(typeof(BankAccountInfo), typeof(v1.BankAccountInfoResponseDto))]
        [InlineData(typeof(BankAccountType), typeof(v1.BankAccountTypeResponseDto))]
        [InlineData(typeof(BankAccountTransaction), typeof(v1.BankAccountTransactionResponseDto))]
        [InlineData(typeof(v1.CurrencyResponseDto), typeof(Currency))]
        [InlineData(typeof(v1.BankResponseDto), typeof(Bank))]
        [InlineData(typeof(v1.BankAccountResponseDto), typeof(BankAccount))]
        [InlineData(typeof(v1.BankAccountInfoResponseDto), typeof(BankAccountInfo))]
        [InlineData(typeof(v1.BankAccountTypeResponseDto), typeof(BankAccountType))]
        [InlineData(typeof(v1.BankAccountCreateRequestDto), typeof(BankAccount))]
        [InlineData(typeof(v1.BankAccountUpdateRequestDto), typeof(BankAccount))]
        [InlineData(typeof(v1.BankAccountTransactionResponseDto), typeof(BankAccountTransaction))]
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
