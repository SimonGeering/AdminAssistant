using AdminAssistant.DomainModel.Modules.AccountsModule;
using AdminAssistant.Framework.TypeMapping;
using Swashbuckle.AspNetCore.Annotations;

namespace AdminAssistant.WebAPI.v1.AccountsModule;

[SwaggerSchema(Required = new[] { "BankName" })]
public sealed record BankCreateRequestDto : IMapTo<Bank>
{
    public string BankName { get; init; } = string.Empty;

    public void MapTo(AutoMapper.Profile profile)
        => profile.CreateMap<BankCreateRequestDto, Bank>()
                  .ForMember(x => x.BankID, opt => opt.Ignore());
}    
