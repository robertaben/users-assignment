using BlazorUserApp.Client.Components;
using BlazorUserApp.Client.DTOs;
using BlazorUserApp.Client.HttpClients;
using Microsoft.AspNetCore.Components;

namespace BlazorUserApp.Client.Pages
{
    public partial class Users
    {
        [Inject]
        private UsersHttpClient UsersHttpClient { get; set; }

        private List<UserReadModel> users;
        private NewEditUserForm newEditUserForm;

        protected override async Task OnInitializedAsync()
        {
            await LoadUsers();
        }

        private async Task LoadUsers()
        {
            users = await UsersHttpClient.GetUsersAsync();
        }

        private async Task DeleteUser(int userId)
        {
            await UsersHttpClient.DeleteUserAsync(userId);
            users = await UsersHttpClient.GetUsersAsync();
        }
    }
}
