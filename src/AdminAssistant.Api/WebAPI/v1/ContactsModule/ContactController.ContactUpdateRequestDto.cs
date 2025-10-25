namespace AdminAssistant.WebAPI.v1.ContactsModule;

[SwaggerSchema(Required = ["ContactID", "FirstName", "LastName"])]
public sealed record ContactUpdateRequestDto
{
    [SwaggerSchema("The Contact identifier.")]
    public int ContactID { get; init; }
    public string FirstName { get; init; } = string.Empty;
    public string LastName { get; init; } = string.Empty;
}
