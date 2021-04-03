using AdminAssistant.DomainModel.Modules.AccountsModule;
using AdminAssistant.Framework.TypeMapping;
using Swashbuckle.AspNetCore.Annotations;

namespace AdminAssistant.WebAPI.v1.AccountsModule
{
    [SwaggerSchema(Required = new[] { "BankName" })]
    public class BankCreateRequestDto : IMapTo<Bank>
    {
        [SwaggerSchema("The Bank identifier.", ReadOnly = true)]
        public int BankID { get; set; }
        public string BankName { get; set; } = string.Empty;

        public void MapTo(AutoMapper.Profile profile)
            => profile.CreateMap<BankCreateRequestDto, Bank>()
                      .ForMember(x => x.BankID, opt => opt.Ignore());
    }    
}
