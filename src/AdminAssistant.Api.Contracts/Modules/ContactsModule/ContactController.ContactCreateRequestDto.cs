namespace AdminAssistant.WebAPI.v1.ContactsModule;

/// <summary>
/// Request DTO for creating a new contact.
/// </summary>
public sealed record ContactCreateRequestDto
{
    /// <summary>
    /// The first name of the contact. Required.
    /// </summary>
    [Required]
    public string FirstName { get; init; } = string.Empty;

    /// <summary>
    /// The last name of the contact. Required.
    /// </summary>
    [Required]
    public string LastName { get; init; } = string.Empty;
}
