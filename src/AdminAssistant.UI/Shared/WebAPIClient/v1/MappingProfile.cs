using AdminAssistant.DomainModel.Modules.AccountsModule;
using AdminAssistant.DomainModel.Modules.AssetRegisterModule;
using AdminAssistant.DomainModel.Modules.BudgetModule;
using AdminAssistant.DomainModel.Modules.CalendarModule;
using AdminAssistant.DomainModel.Modules.ContactsModule;
using AdminAssistant.DomainModel.Modules.CoreModule;
using AdminAssistant.DomainModel.Modules.DocumentsModule;
using AdminAssistant.DomainModel.Modules.MailModule;
using AdminAssistant.DomainModel.Modules.TasksModule;
using AdminAssistant.Framework.TypeMapping;

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
