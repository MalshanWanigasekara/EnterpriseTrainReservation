using System.Net.Http.Headers;
using System.Text;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using TrainReservation.Client.Configurations;
using TrainReservation.Client.Interfaces;

namespace TrainReservation.Client.Services
{
    public class GatewayClient : IGatewayClient
    {
        private readonly HttpClient httpClient;

        public GatewayClient(
            HttpClient httpClient,
            IOptions<GatewaySettings> options)
        {
            this.httpClient = httpClient;

            httpClient.BaseAddress =
                new Uri(options.Value.BaseUrl);
        }

        public void SetBearerToken(string token)
        {
            httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue(
                    "Bearer",
                    token);
        }

        public async Task<T?> GetAsync<T>(string url)
        {
            var response =
                await httpClient.GetAsync(url);

            response.EnsureSuccessStatusCode();

            var json =
                await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<T>(json);
        }

        public async Task<TResponse?> PostAsync<TRequest, TResponse>(
            string url,
            TRequest request)
        {
            var json =
                JsonConvert.SerializeObject(request);

            var content =
                new StringContent(
                    json,
                    Encoding.UTF8,
                    "application/json");

            var response =
                await httpClient.PostAsync(url, content);

            response.EnsureSuccessStatusCode();

            json =
                await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<TResponse>(json);
        }

        public async Task PutAsync<TRequest>(
            string url,
            TRequest request)
        {
            var json =
                JsonConvert.SerializeObject(request);

            var content =
                new StringContent(
                    json,
                    Encoding.UTF8,
                    "application/json");

            var response =
                await httpClient.PutAsync(url, content);

            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteAsync(string url)
        {
            var response =
                await httpClient.DeleteAsync(url);

            response.EnsureSuccessStatusCode();
        }
    }
}