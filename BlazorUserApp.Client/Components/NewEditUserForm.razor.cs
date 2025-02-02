using BlazorUserApp.Client.DTOs;
using BlazorUserApp.Client.HttpClients;
using Microsoft.AspNetCore.Components;

namespace BlazorUserApp.Client.Components
{
    public partial class NewEditUserForm
    {
        [Parameter] 
        public EventCallback OnUserUpdated { get; set; }

        [Parameter] 
        public EventCallback OnUserAdded { get; set; }

        [Inject]
        private UsersHttpClient UsersHttpClient { get; set; }

        private UserWriteModel userModel = new();
        private int? editingUserId;
        private bool showModal = false;

        public void OpenForNew()
        {
            userModel = new();
            editingUserId = null;
            showModal = true;
        }

        public void OpenForEdit(UserReadModel user)
        {
            userModel = new UserWriteModel
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber
            };
            editingUserId = user.Id;
            showModal = true;
        }

        private async Task SaveUser()
        {
            if (editingUserId == null)
            {
                await UsersHttpClient.AddUserAsync(userModel);
                await OnUserAdded.InvokeAsync();
            }
            else
            {
                await UsersHttpClient.UpdateUserAsync(editingUserId.Value, userModel);
                await OnUserUpdated.InvokeAsync();
            }

            userModel = new();
            showModal = false;
        }
    }
}