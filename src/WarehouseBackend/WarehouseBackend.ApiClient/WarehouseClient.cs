using System;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Flurl;
using Newtonsoft.Json;
using WarehouseBackend.Models;
using WarehouseBackend.Models.ApiRequests;
using WarehouseBackend.Models.ApiResponses;

namespace WarehouseBackend.ApiClient
{
    public class WarehouseClient : IWarehouseClient, IDisposable
    {
        public WarehouseClient(string baseAddress)
        {
            _baseAddress = baseAddress;
            _httpClient = new HttpClient();
        }

        public async Task<Container> AddContainerAsync(AddContainerRequest request,
            CancellationToken cancellationToken = default)
        {
            return await PostForResultAsync<Container>(
                    "api/containers", request, cancellationToken)
                .ConfigureAwait(false);
        }

        public async Task<Container> GetContainerByIdAsync(string id,
            CancellationToken cancellationToken = default)
        {
            return await GetForResultAsync<Container>(
                    $"api/containers/{id}", cancellationToken)
                .ConfigureAwait(false);
        }

        public async Task<Container> MarkContainerShippedAsync(string id,
            CancellationToken cancellationToken = default)
        {
            return await PutForResultAsync<Container>($"api/containers/{id}","", cancellationToken)
                .ConfigureAwait(false);
        }

        public async Task<ContainerResults> GetContainersAsync(GetContainersRequest request,
            CancellationToken cancellationToken = default)
        {
            return await GetForResultAsync<ContainerResults>(
                    $"api/containers?page={request.Page}&pageSize={request.PageSize}", cancellationToken)
                .ConfigureAwait(false);
        }

        public async Task<ContainerResults> GetContainersByClientIdAndShipmentDatesAsync(
            GetContainersByClientAndShippingDatesRequest request,
            CancellationToken cancellationToken = default)
        {
            var url = $"api/containers/shipped?page={request.Page}&pageSize={request.PageSize}&shipmentDateLow={request.ShipmentDateLow:s}&shipmentDateHigh={request.ShipmentDateHigh:s}";
            if (!string.IsNullOrEmpty(request.ClientId))
                url += $"&clientId={request.ClientId}";

            return await GetForResultAsync<ContainerResults>(
                    url, cancellationToken)
                .ConfigureAwait(false);
        }

        public async Task DeleteContainerAsync(string id,
            CancellationToken cancellationToken = default)
        {
            await DeleteAsync($"api/containers/{id}", cancellationToken)
                .ConfigureAwait(false);
        }

        public void Dispose()
        {
            _httpClient?.Dispose();
        }

        private string BuildUrl(string path)
        {
            var url = Url.Combine(_baseAddress, path);
            return url;
        }
        private HttpRequestMessage PrepareGetRequestMessage(string url)
        {
            var requestMessage = new HttpRequestMessage(
                HttpMethod.Get, BuildUrl(url));
            
            return requestMessage;
        }

        protected HttpRequestMessage PreparePostRequestMessage(string url, object body)
        {
            var bodyString = JsonConvert.SerializeObject(body);
            var requestUrl = BuildUrl(url);
            
            var requestMessage = new HttpRequestMessage(HttpMethod.Post, requestUrl);
            
            requestMessage.Content = new StringContent(bodyString, Encoding.UTF8, "application/json");

            return requestMessage;
        }

        protected HttpRequestMessage PreparePutRequestMessage(string url, object body)
        {
            var bodyString = JsonConvert.SerializeObject(body);
            var requestUrl = BuildUrl(url);

            var requestMessage = new HttpRequestMessage(HttpMethod.Put, requestUrl);
            
            requestMessage.Content = new StringContent(bodyString, Encoding.UTF8, "application/json");

            return requestMessage;
        }

        protected HttpRequestMessage PrepareDeleteRequestMessage(string url)
        {
            var requestMessage = new HttpRequestMessage(HttpMethod.Delete, BuildUrl(url));

            return requestMessage;
        }

        protected async Task RequestAsync(HttpRequestMessage requestMessage,
            CancellationToken cancellationToken = default)
        {
            var response = await _httpClient
                .SendAsync(requestMessage, cancellationToken)
                .ConfigureAwait(false);

            if (!response.IsSuccessStatusCode)
            {
                var body = await response.Content
                    .ReadAsStringAsync(cancellationToken)
                    .ConfigureAwait(false);
                throw new WarehouseApiClientException(body);
            }
        }

        protected async Task<T> RequestForResultAsync<T>(HttpRequestMessage requestMessage,
            CancellationToken cancellationToken = default)
        {
            var response = await _httpClient
                .SendAsync(requestMessage, cancellationToken)
                .ConfigureAwait(false);
            if (!response.IsSuccessStatusCode)
            {
                var body = await response.Content
                    .ReadAsStringAsync(cancellationToken)
                    .ConfigureAwait(false);
                throw new WarehouseApiClientException(body);
            }

            var resultBody = await response.Content
                .ReadAsStringAsync(cancellationToken)
                .ConfigureAwait(false);
            return JsonConvert.DeserializeObject<T>(resultBody) ?? throw new InvalidOperationException();
        }

        protected async Task<T> GetForResultAsync<T>(string url,
            CancellationToken cancellationToken = default)
        {
            var requestMessage = PrepareGetRequestMessage(url);

            return await RequestForResultAsync<T>(requestMessage, cancellationToken)
                .ConfigureAwait(false);
        }

        protected async Task<T> PostForResultAsync<T>(string url, object body,
            CancellationToken cancellationToken = default)
        {
            var requestMessage = PreparePostRequestMessage(url, body);

            return await RequestForResultAsync<T>(requestMessage, cancellationToken)
                .ConfigureAwait(false);
        }

        protected async Task<T> PutForResultAsync<T>(string url, object body,
            CancellationToken cancellationToken = default)
        {
            var requestMessage = PreparePutRequestMessage(url, body);

            return await RequestForResultAsync<T>(requestMessage, cancellationToken)
                .ConfigureAwait(false);
        }

        protected async Task DeleteAsync(string url,
            CancellationToken cancellationToken = default)
        {
            var requestMessage = PrepareDeleteRequestMessage(url);

            await RequestAsync(requestMessage, cancellationToken)
                .ConfigureAwait(false);
        }

        private readonly string _baseAddress;
        private readonly HttpClient _httpClient;
    }
}
