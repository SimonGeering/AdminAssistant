namespace AdminAssistant.WebAPI.v1.ContactsModule;

/// <summary>
/// Request DTO for updating an existing contact.
/// </summary>
public sealed record ContactUpdateRequestDto
{
    /// <summary>
    /// The Contact identifier. Required.
    /// </summary>
    [Required]
    public int ContactID { get; init; }

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
