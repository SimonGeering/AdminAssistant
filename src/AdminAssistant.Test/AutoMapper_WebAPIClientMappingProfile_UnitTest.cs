#pragma warning disable CA1707 // Identifiers should not contain underscores
using System;
using System.Diagnostics.CodeAnalysis;
using AdminAssistant.DomainModel.Modules.AccountsModule;
using AutoMapper;
using FluentAssertions;
using Xunit;

namespace AdminAssistant.UI.Shared.WebAPIClient.v1
{
    public class WebAPIClientMappingProfile_Should
    {
        private readonly IConfigurationProvider _configuration;
        private readonly IMapper _mapper;

        public WebAPIClientMappingProfile_Should()
        {
            _configuration = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>());

            _mapper = _configuration.CreateMapper();
        }

        [Fact]
        [Trait("Category", "Unit")]
        [SuppressMessage("Style", "IDE0022:Use expression body for methods", Justification = "One line test")]
        public void HaveValidConfiguration()
        {
            // Arrange

            // Act
            _configuration.AssertConfigurationIsValid();

            // Assert
        }

        [Theory]
        [Trait("Category", "Unit")]
        [InlineData(typeof(CurrencyResponseDto), typeof(Currency))]
        [InlineData(typeof(BankResponseDto), typeof(Bank))]
        [InlineData(typeof(BankAccountResponseDto), typeof(BankAccount))]
        [InlineData(typeof(BankAccountInfoResponseDto), typeof(BankAccountInfo))]
        [InlineData(typeof(BankAccountTypeResponseDto), typeof(BankAccountType))]
        [InlineData(typeof(BankAccountTransactionResponseDto), typeof(BankAccountTransaction))]
        [InlineData(typeof(BankAccount), typeof(BankAccountCreateRequestDto))]
        [InlineData(typeof(BankAccount), typeof(BankAccountUpdateRequestDto))]
        public void ShouldSupportMappingFromSourceToDestination(Type source, Type destination)
        {
            // Arrange
            var instance = Activator.CreateInstance(source);

            // Act
            var result = _mapper.Map(instance, source, destination);

            // Assert
            result.Should().NotBeNull();
        }
    }
}
#pragma warning restore CA1707 // Identifiers should not contain underscores
