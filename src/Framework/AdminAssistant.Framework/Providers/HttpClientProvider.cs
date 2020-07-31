using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;

namespace AdminAssistant.Framework.Providers
{
    public interface IHttpClientProvider
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1054:Uri parameters should not be strings", Justification = "TODO: Don't want to get into URI formats when strings work for now")]
        Task<TValue> GetFromJsonAsync<TValue>(string? requestUri, CancellationToken cancellationToken = default);

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1054:Uri parameters should not be strings", Justification = "TODO: Don't want to get into URI formats when strings work for now")]
        Task<HttpResponseMessage> PutAsJsonAsync<TValue>(string? requestUri, TValue value, CancellationToken cancellationToken = default);

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1054:Uri parameters should not be strings", Justification = "TODO: Don't want to get into URI formats when strings work for now")]
        Task<HttpResponseMessage> PostAsJsonAsync<TValue>(string? requestUri, TValue value, CancellationToken cancellationToken = default);

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1054:Uri parameters should not be strings", Justification = "TODO: Don't want to get into URI formats when strings work for now")]
        Task<HttpResponseMessage> Delete(string requestUri);

    }
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Build", "CA1812", Justification = "Compiler dosen't understand dependency injection")]
    internal class HttpClientProvider : IHttpClientProvider
    {
        private readonly HttpClient httpClient;

        public HttpClientProvider(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1054:Uri parameters should not be strings", Justification = "TODO: Don't want to get into URI formats when strings work for now")]
        public Task<TValue> GetFromJsonAsync<TValue>(string? requestUri, CancellationToken cancellationToken = default)
            => httpClient.GetFromJsonAsync<TValue>(requestUri, null, cancellationToken);

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1054:Uri parameters should not be strings", Justification = "TODO: Don't want to get into URI formats when strings work for now")]
        public Task<HttpResponseMessage> PutAsJsonAsync<TValue>(string? requestUri, TValue value, CancellationToken cancellationToken = default)
            => httpClient.PutAsJsonAsync(requestUri, value, cancellationToken);

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1054:Uri parameters should not be strings", Justification = "TODO: Don't want to get into URI formats when strings work for now")]
        public Task<HttpResponseMessage> PostAsJsonAsync<TValue>(string? requestUri, TValue value, CancellationToken cancellationToken = default)
            => httpClient.PostAsJsonAsync(requestUri, value, cancellationToken);

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1054:Uri parameters should not be strings", Justification = "TODO: Don't want to get into URI formats when strings work for now")]
        public Task<HttpResponseMessage> Delete(string requestUri)
            => httpClient.DeleteAsync(requestUri);
    }
}
