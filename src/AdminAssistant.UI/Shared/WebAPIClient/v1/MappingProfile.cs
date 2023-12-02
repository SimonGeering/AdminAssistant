using AdminAssistant.Modules.AccountsModule;
using AdminAssistant.Modules.AssetRegisterModule;
using AdminAssistant.Modules.BudgetModule;
using AdminAssistant.Modules.CalendarModule;
using AdminAssistant.Modules.ContactsModule;
using AdminAssistant.Modules.CoreModule;
using AdminAssistant.Modules.DocumentsModule;
using AdminAssistant.Modules.MailModule;
using AdminAssistant.Modules.TasksModule;
using SimonGeering.Framework.TypeMapping;

namespace AdminAssistant.UI.Shared.WebAPIClient.v1;

public sealed class MappingProfile : MappingProfileBase
{
    public MappingProfile()
        : base(typeof(MappingProfile).Assembly)
    {
        // Accounts Module ...
        CreateMap<BankResponseDto, Bank>();
        CreateMap<BankAccountResponseDto, BankAccount>()
            .ForMember(x => x.OwnerID, opt => opt.Ignore());
        CreateMap<BankAccountInfoResponseDto, BankAccountInfo>();
        CreateMap<BankAccountTypeResponseDto, BankAccountType>();
        CreateMap<BankAccountTransactionResponseDto, BankAccountTransaction>();
        CreateMap<BankAccount, BankAccountCreateRequestDto>()
            .ForMember(x => x.Balance, opt => opt.Ignore());
        CreateMap<BankAccount, BankAccountUpdateRequestDto>();

        // Asset Register Module ...
        CreateMap<AssetResponseDto, Asset>();

        // Budget Module ...
        CreateMap<BudgetResponseDto, Budget>();

        // Calendar Module ...
        CreateMap<ReminderResponseDto, Reminder>();

        // Contact Module ...
        CreateMap<ContactResponseDto, Contact>()
            .ForMember(x => x.OwnerID, opt => opt.Ignore())
            .ForMember(x => x.TitleID, opt => opt.Ignore())
            .ForMember(x => x.DateOfBirth, opt => opt.Ignore());

        // Core Module ...
        CreateMap<CurrencyResponseDto, Currency>();

        // Documents Module ...
        CreateMap<DocumentResponseDto, Document>();

        // Mail Module ...
        CreateMap<MailMessageResponseDto, MailMessage>();

        // Tasks Module ...
        CreateMap<TaskListResponseDto, TaskList>();
    }
}
