#pragma warning disable CA1707 // Identifiers should not contain underscores
using AutoMapper;
using System;
using FluentAssertions;
using Xunit;
using AdminAssistant.DomainModel.Modules.AccountsModule;
using System.Diagnostics.CodeAnalysis;

namespace AdminAssistant.WebAPI.v1
{
    public class WebAPIMappingProfile_Should
    {
        private readonly IConfigurationProvider _configuration;
        private readonly IMapper _mapper;

        public WebAPIMappingProfile_Should()
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
            var result = _mapper.Map(instance, source, destination);

            // Assert
            result.Should().NotBeNull();
        }
    }
}
#pragma warning restore CA1707 // Identifiers should not contain underscores
