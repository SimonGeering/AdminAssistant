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
using AdminAssistant.UI.Shared.WebAPIClient.v1;

namespace AdminAssistant.Test.UI.Shared.WebAPIClient.v1;

public class WebApiClientMappingProfile_Should
{
    private readonly MapperConfiguration _configuration;
    private readonly IMapper _mapper;

    public WebApiClientMappingProfile_Should()
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
    [InlineData(typeof(BankResponseDto), typeof(Bank))]
    [InlineData(typeof(BankAccountResponseDto), typeof(BankAccount))]
    [InlineData(typeof(BankAccountInfoResponseDto), typeof(BankAccountInfo))]
    [InlineData(typeof(BankAccountTypeResponseDto), typeof(BankAccountType))]
    [InlineData(typeof(BankAccountTransactionResponseDto), typeof(BankAccountTransaction))]
    [InlineData(typeof(BankAccount), typeof(BankAccountCreateRequestDto))]
    [InlineData(typeof(BankAccount), typeof(BankAccountUpdateRequestDto))]
    // Asset Module
    [InlineData(typeof(AssetResponseDto), typeof(Asset))]
    // Budget Module
    [InlineData(typeof(BudgetResponseDto), typeof(Budget))]
    // Calendar Module
    [InlineData(typeof(ReminderResponseDto), typeof(Reminder))]
    // Contact Module
    [InlineData(typeof(ContactResponseDto), typeof(Contact))]
    // Core Module
    [InlineData(typeof(CurrencyResponseDto), typeof(Currency))]
    // Documents Module
    [InlineData(typeof(DocumentResponseDto), typeof(Document))]
    // Mail Module
    [InlineData(typeof(MailMessageResponseDto), typeof(MailMessage))]
    // Task Module
    [InlineData(typeof(TaskListResponseDto), typeof(TaskList))]
    //  Module
    //  Module
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
