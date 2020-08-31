using AdminAssistant.DomainModel.Modules.AccountsModule;
using AdminAssistant.DomainModel.Modules.CoreModule;
using AdminAssistant.Framework.TypeMapping;

namespace AdminAssistant.UI.Shared.WebAPIClient.v1
{
    public class MappingProfile : MappingProfileBase
    {
        public MappingProfile()
            : base(typeof(MappingProfile).Assembly)
        {
            CreateMap<CurrencyResponseDto, Currency>();
            CreateMap<BankResponseDto, Bank>();
            CreateMap<BankAccountResponseDto, BankAccount>();
            CreateMap<BankAccountInfoResponseDto, BankAccountInfo>();
            CreateMap<BankAccountTypeResponseDto, BankAccountType>();
            CreateMap<BankAccountTransactionResponseDto, BankAccountTransaction>();
            CreateMap<BankAccount, BankAccountCreateRequestDto>()
                .ForMember(x => x.Balance, opt => opt.Ignore());
            CreateMap<BankAccount, BankAccountUpdateRequestDto>();
        }
    }
}
