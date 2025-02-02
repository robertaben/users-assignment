using BlazorUserApp.Client.DTOs;
using System.Net.Http.Json;

namespace BlazorUserApp.Client.HttpClients
{
    public class UsersHttpClient
    {
        private readonly HttpClient httpClient;

        public UsersHttpClient(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<List<UserReadModel>> GetUsersAsync()
        {
            return await httpClient.GetFromJsonAsync<List<UserReadModel>>("api/users");
        }

        public async Task<UserReadModel> GetUserByIdAsync(int userId)
        {
            return await httpClient.GetFromJsonAsync<UserReadModel>($"api/users/{userId}");
        }

        public async Task AddUserAsync(UserWriteModel user)
        {
            await httpClient.PostAsJsonAsync("api/users", user);
        }

        public async Task UpdateUserAsync(int userId, UserWriteModel user)
        {
            await httpClient.PutAsJsonAsync($"api/users/{userId}", user);
        }

        public async Task DeleteUserAsync(int userId)
        {
            await httpClient.DeleteAsync($"api/users/{userId}");
        }
    }
}
