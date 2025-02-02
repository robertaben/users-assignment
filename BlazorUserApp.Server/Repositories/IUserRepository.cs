using BlazorUserApp.Server.DTOs;

namespace BlazorUserApp.Server.Repositories
{
    public interface IUserRepository
    {
        Task<List<UserReadModel>> GetUsersAsync();
        Task<UserReadModel?> GetUserByIdAsync(int userId);
        Task AddUserAsync(UserWriteModel userModel);
        Task UpdateUserAsync(int userId, UserWriteModel userModel);
        Task DeleteUserAsync(int userId);
    }
}