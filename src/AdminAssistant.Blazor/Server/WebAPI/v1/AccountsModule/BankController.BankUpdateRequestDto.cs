using AdminAssistant.DomainModel.Modules.AccountsModule;
using AdminAssistant.Framework.TypeMapping;
using Swashbuckle.AspNetCore.Annotations;

namespace AdminAssistant.WebAPI.v1.AccountsModule
{ 
    [SwaggerSchema(Required = new[] { "BankID", "BankName" })]
    public record BankUpdateRequestDto : IMapTo<Bank>
    {
        [SwaggerSchema("The Bank identifier.", ReadOnly = true)]
        public int BankID { get; init; }
        public string BankName { get; init; } = string.Empty;

        public void MapTo(AutoMapper.Profile profile)
            => profile.CreateMap<BankUpdateRequestDto, Bank>()
                      .ForMember(x => x.BankID, opt => opt.Ignore());
    }
}
