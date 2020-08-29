using AdminAssistant.Framework.TypeMapping;

namespace AdminAssistant.UI.Shared.WebAPIClient.v1
{
    public class MappingProfile : MappingProfileBase
    {
        public MappingProfile()
            : base(typeof(MappingProfile).Assembly)
        {
            CreateMap<CurrencyResponseDto, DomainModel.Modules.AccountsModule.Currency>();
            CreateMap<BankAccountTypeResponseDto, DomainModel.Modules.AccountsModule.BankAccountType>();
        }
    }
}
