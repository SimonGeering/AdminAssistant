using AdminAssistant.Framework.TypeMapping;

namespace AdminAssistant.UI.Shared.WebAPIClient.v1
{
    public class MappingProfile : MappingProfileBase
    {
        public MappingProfile()
            : base(typeof(MappingProfile).Assembly)
        {
            this.CreateMap<CurrencyResponseDto, DomainModel.Modules.AccountsModule.Currency>();
            this.CreateMap<BankAccountTypeResponseDto, DomainModel.Modules.AccountsModule.BankAccountType>();
        }
    }
}
