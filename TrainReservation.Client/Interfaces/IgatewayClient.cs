using System.Net.Http.Headers;

namespace TrainReservation.Client.Interfaces
{
    public interface IGatewayClient
    {
        Task<T?> GetAsync<T>(string url);

        Task<TResponse?> PostAsync<TRequest, TResponse>(
            string url,
            TRequest request);

        Task PutAsync<TRequest>(
            string url,
            TRequest request);

        Task DeleteAsync(
            string url);

        void SetBearerToken(string token);
    }
}