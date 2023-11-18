namespace AdminAssistant.DomainModel.Shared;

public sealed record ConfigurationSettings
{
    public string WebApiClientBaseAddress { get; set; } = "https://localhost:5001";
    public string DatabaseProvider { get; set; } = string.Empty;
    public string ConnectionString { get; set; } = string.Empty;
    public string Auth0Authority { get; set; } = string.Empty;
    public string Auth0ApiIdentifier { get; set; } = string.Empty;
    public string Auth0AuthorizeEndpoint { get; set; } = string.Empty;
    public string Auth0TokenEndpoint { get; set; } = string.Empty;
    public string Auth0ClientId { get; set; } = string.Empty;
    public string Auth0ClientSecret { get; set; } = string.Empty;
    public string Auth0AppName { get; set; } = string.Empty;
}
