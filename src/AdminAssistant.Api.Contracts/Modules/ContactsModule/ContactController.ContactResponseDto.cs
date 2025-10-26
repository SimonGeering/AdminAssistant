namespace AdminAssistant.WebAPI.v1.ContactsModule;

public sealed record ContactResponseDto
{
    public int ContactID { get; init; }
    public string FirstName { get; init; } = string.Empty;
    public string LastName { get; init; } = string.Empty;
}
