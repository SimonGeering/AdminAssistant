namespace AdminAssistant.Infra.Providers;

public interface IPdfFileProvider
{
    Task<IEnumerable<string>> ReadAllLinesAsync(byte[] fileContent);
}
