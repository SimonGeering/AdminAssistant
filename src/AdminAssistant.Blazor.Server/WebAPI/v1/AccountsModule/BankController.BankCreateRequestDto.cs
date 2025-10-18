namespace AdminAssistant.WebAPI.v1.AccountsModule;

[SwaggerSchema(Required = ["BankName"])]
public sealed record BankCreateRequestDto
{
    public string BankName { get; init; } = string.Empty;
}
