using Microsoft.AspNetCore.Http;
using Shared.DTOs;
using Shared.Requests;
using Shared.Responses;
using TrainReservation.Client.Interfaces;

namespace TrainReservation.Client.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IGatewayClient gatewayClient;

        private readonly IHttpContextAccessor httpContextAccessor;

        public AuthenticationService(
            IGatewayClient gatewayClient,
            IHttpContextAccessor httpContextAccessor)
        {
            this.gatewayClient = gatewayClient;
            this.httpContextAccessor = httpContextAccessor;
        }

        public async Task<LoginResponse?> LoginAsync(
            LoginRequest request)
        {
            var response =
                await gatewayClient.PostAsync<LoginRequest, LoginResponse>(
                    "/users/api/users/login",
                    request);

            if (response != null && response.Success)
            {
                httpContextAccessor.HttpContext!.Session.SetString(
                    "NIC",
                    response.User!.NIC);

                httpContextAccessor.HttpContext!.Session.SetString(
                    "UserName",
                    $"{response.User.FirstName} {response.User.LastName}");
            }

            return response;
        }

        public async Task<UserDto?> RegisterAsync(
            RegisterRequest request)
        {
            return await gatewayClient.PostAsync<RegisterRequest, UserDto>(
                "/users/api/users",
                request);
        }

        public void Logout()
        {
            httpContextAccessor.HttpContext!.Session.Clear();
        }

        public bool IsLoggedIn()
        {
            return httpContextAccessor.HttpContext!.Session
                .GetString("NIC") != null;
        }

        public string? GetLoggedInNic()
        {
            return httpContextAccessor.HttpContext!.Session
                .GetString("NIC");
        }
    }
}