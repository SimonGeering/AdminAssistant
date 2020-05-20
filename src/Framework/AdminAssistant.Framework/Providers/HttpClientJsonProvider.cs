using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;

namespace AdminAssistant.Framework.Providers
{
    public interface IHttpClientJsonProvider
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
    public class HttpClientJsonProvider : IHttpClientJsonProvider
    {
        private readonly HttpClient client;

        public HttpClientJsonProvider(HttpClient client)
        {
            this.client = client;
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1054:Uri parameters should not be strings", Justification = "TODO: Don't want to get into URI formats when strings work for now")]
        public Task<TValue> GetFromJsonAsync<TValue>(string? requestUri, CancellationToken cancellationToken = default)
            => client.GetFromJsonAsync<TValue>(requestUri, null, cancellationToken);

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1054:Uri parameters should not be strings", Justification = "TODO: Don't want to get into URI formats when strings work for now")]
        public Task<HttpResponseMessage> PutAsJsonAsync<TValue>(string? requestUri, TValue value, CancellationToken cancellationToken = default)
            => client.PutAsJsonAsync(requestUri, value, cancellationToken);

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1054:Uri parameters should not be strings", Justification = "TODO: Don't want to get into URI formats when strings work for now")]
        public Task<HttpResponseMessage> PostAsJsonAsync<TValue>(string? requestUri, TValue value, CancellationToken cancellationToken = default)
            => client.PostAsJsonAsync(requestUri, value, cancellationToken);

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1054:Uri parameters should not be strings", Justification = "TODO: Don't want to get into URI formats when strings work for now")]
        public Task<HttpResponseMessage> Delete(string requestUri)
            => client.DeleteAsync(requestUri);
    }
}
