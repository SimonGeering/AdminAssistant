using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;

namespace AdminAssistant.Framework.Providers
{
    public interface IHttpClientProvider
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1054:Uri parameters should not be strings", Justification = "TODO: Don't want to get into URI formats when strings work for now")]
        Task<TValue> GetFromJsonAsync<TValue>(string httpClientName, string? requestUri, CancellationToken cancellationToken = default);

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1054:Uri parameters should not be strings", Justification = "TODO: Don't want to get into URI formats when strings work for now")]
        Task<HttpResponseMessage> PutAsJsonAsync<TValue>(string httpClientName, string? requestUri, TValue value, CancellationToken cancellationToken = default);

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1054:Uri parameters should not be strings", Justification = "TODO: Don't want to get into URI formats when strings work for now")]
        Task<HttpResponseMessage> PostAsJsonAsync<TValue>(string httpClientName, string? requestUri, TValue value, CancellationToken cancellationToken = default);

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1054:Uri parameters should not be strings", Justification = "TODO: Don't want to get into URI formats when strings work for now")]
        Task<HttpResponseMessage> Delete(string httpClientName, string requestUri);

    }
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Build", "CA1812", Justification = "Compiler dosen't understand dependency injection")]
    internal class HttpClientProvider : IHttpClientProvider
    {
        private readonly IHttpClientFactory httpClientFactory;

        public HttpClientProvider(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1054:Uri parameters should not be strings", Justification = "TODO: Don't want to get into URI formats when strings work for now")]
        public Task<TValue> GetFromJsonAsync<TValue>(string httpClientName, string? requestUri, CancellationToken cancellationToken = default)
        {
            using (var httpClient = this.httpClientFactory.CreateClient(httpClientName))
                return httpClient.GetFromJsonAsync<TValue>(requestUri, null, cancellationToken);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1054:Uri parameters should not be strings", Justification = "TODO: Don't want to get into URI formats when strings work for now")]
        public Task<HttpResponseMessage> PutAsJsonAsync<TValue>(string httpClientName, string? requestUri, TValue value, CancellationToken cancellationToken = default)
        {
            using (var httpClient = this.httpClientFactory.CreateClient(httpClientName))
                return httpClient.PutAsJsonAsync(requestUri, value, cancellationToken);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1054:Uri parameters should not be strings", Justification = "TODO: Don't want to get into URI formats when strings work for now")]
        public Task<HttpResponseMessage> PostAsJsonAsync<TValue>(string httpClientName, string? requestUri, TValue value, CancellationToken cancellationToken = default)
        {
            using (var httpClient = this.httpClientFactory.CreateClient(httpClientName))
                return httpClient.PostAsJsonAsync(requestUri, value, cancellationToken);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1054:Uri parameters should not be strings", Justification = "TODO: Don't want to get into URI formats when strings work for now")]
        public Task<HttpResponseMessage> Delete(string httpClientName, string requestUri)
        {
            using (var httpClient = this.httpClientFactory.CreateClient(httpClientName))
                return httpClient.DeleteAsync(requestUri);
        }
    }
}
