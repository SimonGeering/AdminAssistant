using AdminAssistant.DomainModel.Modules.AccountsModule;
using AdminAssistant.Framework.TypeMapping;
using Swashbuckle.AspNetCore.Annotations;

namespace AdminAssistant.WebAPI.v1.AccountsModule;

[SwaggerSchema(Required = new[] { "BankID", "BankName" })]
public sealed record BankUpdateRequestDto : IMapTo<Bank>
{
    [SwaggerSchema("The Bank identifier.", ReadOnly = true)]
    public int BankID { get; init; }
    public string BankName { get; init; } = string.Empty;
}
