namespace AdminAssistant.WebAPI.v1.ContactsModule;

[SwaggerSchema(Required = ["FirstName", "LastName"])]
public sealed record ContactCreateRequestDto
{
    public string FirstName { get; init; } = string.Empty;
    public string LastName { get; init; } = string.Empty;
}
