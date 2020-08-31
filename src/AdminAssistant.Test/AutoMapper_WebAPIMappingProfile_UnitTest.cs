#pragma warning disable CA1707 // Identifiers should not contain underscores
using System;
using System.Diagnostics.CodeAnalysis;
using AdminAssistant.DomainModel.Modules.AccountsModule;
using AdminAssistant.DomainModel.Modules.BudgetModule;
using AdminAssistant.DomainModel.Modules.CoreModule;
using AdminAssistant.DomainModel.Modules.DocumentsModule;
using AdminAssistant.DomainModel.Modules.TasksModule;
using AdminAssistant.WebAPI.v1.AccountsModule;
using AdminAssistant.WebAPI.v1.BudgetModule;
using AdminAssistant.WebAPI.v1.CoreModule;
using AdminAssistant.WebAPI.v1.DocumentsModule;
using AdminAssistant.WebAPI.v1.TasksModule;
using AutoMapper;
using FluentAssertions;
using Xunit;

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
        [InlineData(typeof(Currency), typeof(CurrencyResponseDto))]
        [InlineData(typeof(Bank), typeof(BankResponseDto))]
        [InlineData(typeof(BankAccount), typeof(BankAccountResponseDto))]
        [InlineData(typeof(BankAccountInfo), typeof(BankAccountInfoResponseDto))]
        [InlineData(typeof(BankAccountType), typeof(BankAccountTypeResponseDto))]
        [InlineData(typeof(BankAccountTransaction), typeof(BankAccountTransactionResponseDto))]
        [InlineData(typeof(Budget), typeof(BudgetResponseDto))]
        [InlineData(typeof(Document), typeof(DocumentResponseDto))]
        [InlineData(typeof(TaskList), typeof(TaskListResponseDto))]
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
