using BlazorUserApp.Server.Data;
using BlazorUserApp.Server.DTOs;
using BlazorUserApp.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace BlazorUserApp.Server.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UsersDbContext usersDbContext;

        public UserRepository(UsersDbContext usersDbContext)
        {
            this.usersDbContext = usersDbContext;
        }

        public async Task AddUserAsync(UserWriteModel userModel)
        {
            var user = new User
            {
                FirstName = userModel.FirstName,
                LastName = userModel.LastName,
                PhoneNumber = userModel.PhoneNumber,
                Email = userModel.Email
            };
            usersDbContext.Users.Add(user);
            await usersDbContext.SaveChangesAsync();
        }

        public async Task DeleteUserAsync(int userId)
        {
            var user = await usersDbContext.Users.FindAsync(userId);
            if (user != null) { usersDbContext.Users.Remove(user); await usersDbContext.SaveChangesAsync(); }
        }

        public async Task<UserReadModel?> GetUserByIdAsync(int userId)
        {
            return await usersDbContext.Users.Where(u => u.Id == userId)
            .Select(u => new UserReadModel
            {
                Id = u.Id,
                FirstName = u.FirstName,
                LastName = u.LastName,
                PhoneNumber = u.PhoneNumber,
                Email = u.Email
            }).FirstOrDefaultAsync();
        }

        public async Task<List<UserReadModel>> GetUsersAsync()
        {
            return await usersDbContext.Users.Select(u => new UserReadModel
            {
                Id = u.Id,
                FirstName = u.FirstName,
                LastName = u.LastName,
                PhoneNumber = u.PhoneNumber,
                Email = u.Email
            }).ToListAsync();
        }

        public async Task UpdateUserAsync(int userId, UserWriteModel userModel)
        {
            var user = await usersDbContext.Users.FindAsync(userId);
            if (user == null)
            {
                return;
            }

            user.FirstName = userModel.FirstName;
            user.LastName = userModel.LastName;
            user.PhoneNumber = userModel.PhoneNumber;
            user.Email = userModel.Email;
            await usersDbContext.SaveChangesAsync();
        }
    }
}
