using AdminAssistant.WebAPI.v1.ContactsModule;

namespace AdminAssistant.WebAPIClient.v1.ContactsModule;

public interface IContactApiClient
{
    [Put("/api/v1/contacts-module/Contact")]
    Task<ContactResponseDto> UpdateContactAsync([Body] ContactUpdateRequestDto request, CancellationToken cancellationToken = default);

    [Post("/api/v1/contacts-module/Contact")]
    Task<ContactResponseDto> CreateContactAsync([Body] ContactCreateRequestDto request, CancellationToken cancellationToken = default);

    [Get("/api/v1/contacts-module/Contact/{contactID}")]
    Task<ContactResponseDto> GetContactByIdAsync(int contactID, CancellationToken cancellationToken = default);

    [Get("/api/v1/contacts-module/Contact")]
    Task<IEnumerable<ContactResponseDto>> GetContactsAsync(CancellationToken cancellationToken = default);
}
