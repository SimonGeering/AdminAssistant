#pragma warning disable CA1707 // Identifiers should not contain underscores
using AdminAssistant.Modules.AccountsModule;
using AdminAssistant.Modules.AssetRegisterModule;
using AdminAssistant.Modules.BudgetModule;
using AdminAssistant.Modules.CalendarModule;
using AdminAssistant.Modules.ContactsModule;
using AdminAssistant.Modules.CoreModule;
using AdminAssistant.Modules.DocumentsModule;
using AdminAssistant.Modules.MailModule;
using AdminAssistant.Modules.TasksModule;
using AdminAssistant.WebAPI.v1;
using AdminAssistant.WebAPI.v1.AccountsModule;
using AdminAssistant.WebAPI.v1.AssetRegisterModule;
using AdminAssistant.WebAPI.v1.BudgetModule;
using AdminAssistant.WebAPI.v1.CalendarModule;
using AdminAssistant.WebAPI.v1.ContactsModule;
using AdminAssistant.WebAPI.v1.CoreModule;
using AdminAssistant.WebAPI.v1.DocumentsModule;
using AdminAssistant.WebAPI.v1.MailModule;
using AdminAssistant.WebAPI.v1.TasksModule;

namespace AdminAssistant.Test.WebAPI.v1;

public class WebApiMappingProfile_Should
{
    private readonly MapperConfiguration _configuration;
    private readonly IMapper _mapper;

    public WebApiMappingProfile_Should()
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
    // Accounts Module
    [InlineData(typeof(Bank), typeof(BankResponseDto))]
    [InlineData(typeof(BankCreateRequestDto), typeof(Bank))]
    [InlineData(typeof(BankUpdateRequestDto), typeof(Bank))]
    [InlineData(typeof(BankAccount), typeof(BankAccountResponseDto))]
    [InlineData(typeof(BankAccountInfo), typeof(BankAccountInfoResponseDto))]
    [InlineData(typeof(BankAccountType), typeof(BankAccountTypeResponseDto))]
    [InlineData(typeof(BankAccountTransaction), typeof(BankAccountTransactionResponseDto))]
    [InlineData(typeof(BankAccountCreateRequestDto), typeof(BankAccount))]
    [InlineData(typeof(BankAccountUpdateRequestDto), typeof(BankAccount))]
    // Asset Module
    [InlineData(typeof(Asset), typeof(AssetResponseDto))]
    // Budget Module
    [InlineData(typeof(Budget), typeof(BudgetResponseDto))]
    // Calendar Module
    [InlineData(typeof(Reminder), typeof(ReminderResponseDto))]
    // Contacts Module
    [InlineData(typeof(Contact), typeof(ContactResponseDto))]
    [InlineData(typeof(ContactCreateRequestDto), typeof(Contact))]
    [InlineData(typeof(ContactUpdateRequestDto), typeof(Contact))]
    // Core Module
    [InlineData(typeof(Currency), typeof(CurrencyResponseDto))]
    [InlineData(typeof(CurrencyCreateRequestDto), typeof(Currency))]
    [InlineData(typeof(CurrencyUpdateRequestDto), typeof(Currency))]
    // Documents Module
    [InlineData(typeof(Document), typeof(DocumentResponseDto))]
    // Mail Module
    [InlineData(typeof(MailMessage), typeof(MailMessageResponseDto))]
    // Task Module
    [InlineData(typeof(TaskList), typeof(TaskListResponseDto))]
    public void ShouldSupportMappingFromSourceToDestination(Type source, Type destination)
    {
        // Arrange
        var instance = Activator.CreateInstance(source);

        // Act
        var result = _mapper.Map(instance, source, destination);

        // Assert
        result.ShouldNotBeNull();
    }
}
#pragma warning restore CA1707 // Identifiers should not contain underscores
