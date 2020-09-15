#pragma warning disable CA1707 // Identifiers should not contain underscores
using System;
using System.Diagnostics.CodeAnalysis;
using AdminAssistant.DomainModel.Modules.AccountsModule;
using AdminAssistant.DomainModel.Modules.AssetRegisterModule;
using AdminAssistant.DomainModel.Modules.BudgetModule;
using AdminAssistant.DomainModel.Modules.CalendarModule;
using AdminAssistant.DomainModel.Modules.ContactsModule;
using AdminAssistant.DomainModel.Modules.CoreModule;
using AdminAssistant.DomainModel.Modules.DocumentsModule;
using AdminAssistant.DomainModel.Modules.MailModule;
using AdminAssistant.DomainModel.Modules.TasksModule;
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
        [InlineData(typeof(BankResponseDto), typeof(Bank))]
        [InlineData(typeof(BankAccountResponseDto), typeof(BankAccount))]
        [InlineData(typeof(BankAccountInfoResponseDto), typeof(BankAccountInfo))]
        [InlineData(typeof(BankAccountTypeResponseDto), typeof(BankAccountType))]
        [InlineData(typeof(BankAccountTransactionResponseDto), typeof(BankAccountTransaction))]
        [InlineData(typeof(BankAccount), typeof(BankAccountCreateRequestDto))]
        [InlineData(typeof(BankAccount), typeof(BankAccountUpdateRequestDto))]
        public void ShouldSupportAccoountsModuleMappingFromSourceToDestination(Type source, Type destination)
        {
            // Arrange
            var instance = Activator.CreateInstance(source);

            // Act
            var result = _mapper.Map(instance, source, destination);

            // Assert
            result.Should().NotBeNull();
        }

        [Theory]
        [Trait("Category", "Unit")]
        [InlineData(typeof(AssetResponseDto), typeof(Asset))]
        public void ShouldSupportAssetModuleMappingFromSourceToDestination(Type source, Type destination)
        {
            // Arrange
            var instance = Activator.CreateInstance(source);

            // Act
            var result = _mapper.Map(instance, source, destination);

            // Assert
            result.Should().NotBeNull();
        }

        [Theory]
        [Trait("Category", "Unit")]
        [InlineData(typeof(BudgetResponseDto), typeof(Budget))]
        public void ShouldSupportBudgetModuleMappingFromSourceToDestination(Type source, Type destination)
        {
            // Arrange
            var instance = Activator.CreateInstance(source);

            // Act
            var result = _mapper.Map(instance, source, destination);

            // Assert
            result.Should().NotBeNull();
        }

        [Theory]
        [Trait("Category", "Unit")]
        [InlineData(typeof(ReminderResponseDto), typeof(Reminder))]
        public void ShouldSupportCalendarModuleMappingFromSourceToDestination(Type source, Type destination)
        {
            // Arrange
            var instance = Activator.CreateInstance(source);

            // Act
            var result = _mapper.Map(instance, source, destination);

            // Assert
            result.Should().NotBeNull();
        }

        [Theory]
        [Trait("Category", "Unit")]
        [InlineData(typeof(ContactResponseDto), typeof(Contact))]
        public void ShouldSupportContactsModuleMappingFromSourceToDestination(Type source, Type destination)
        {
            // Arrange
            var instance = Activator.CreateInstance(source);

            // Act
            var result = _mapper.Map(instance, source, destination);

            // Assert
            result.Should().NotBeNull();
        }

        [Theory]
        [Trait("Category", "Unit")]
        [InlineData(typeof(CurrencyResponseDto), typeof(Currency))]
        public void ShouldSupportCoreModuleMappingFromSourceToDestination(Type source, Type destination)
        {
            // Arrange
            var instance = Activator.CreateInstance(source);

            // Act
            var result = _mapper.Map(instance, source, destination);

            // Assert
            result.Should().NotBeNull();
        }

        [Theory]
        [Trait("Category", "Unit")]
        [InlineData(typeof(DocumentResponseDto), typeof(Document))]
        public void ShouldSupportDocumentsModuleMappingFromSourceToDestination(Type source, Type destination)
        {
            // Arrange
            var instance = Activator.CreateInstance(source);

            // Act
            var result = _mapper.Map(instance, source, destination);

            // Assert
            result.Should().NotBeNull();
        }

        [Theory]
        [Trait("Category", "Unit")]
        [InlineData(typeof(MailMessageResponseDto), typeof(MailMessage))]
        public void ShouldSupportMailModuleMappingFromSourceToDestination(Type source, Type destination)
        {
            // Arrange
            var instance = Activator.CreateInstance(source);

            // Act
            var result = _mapper.Map(instance, source, destination);

            // Assert
            result.Should().NotBeNull();
        }

        [Theory]
        [Trait("Category", "Unit")]
        [InlineData(typeof(TaskListResponseDto), typeof(TaskList))]
        public void ShouldSupportTasksModuleMappingFromSourceToDestination(Type source, Type destination)
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
