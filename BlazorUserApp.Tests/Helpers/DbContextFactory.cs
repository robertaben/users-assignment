using BlazorUserApp.Server.Data;
using BlazorUserApp.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace BlazorUserApp.Tests.Helpers
{
    public static class DbContextFactory
    {
        public static UsersDbContext CreateDbContext(bool seedData = false)
        {
            var options = new DbContextOptionsBuilder<UsersDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var dbContext = new UsersDbContext(options);
            dbContext.Database.EnsureCreated();

            if (seedData)
            {
                SeedUsers(dbContext);
            }

            return dbContext;
        }

        private static void SeedUsers(UsersDbContext context)
        {
            var users = new List<User>
            {
                new User { Id = 1, FirstName = "Alice", LastName = "Smith", PhoneNumber = "+37069999999", Email = "alice@example.com" },
                new User { Id = 2, FirstName = "Bob", LastName = "Johnson", PhoneNumber = "0037061111111", Email = "bob@example.com" },
                new User { Id = 3, FirstName = "Charlie", LastName = "Brown", PhoneNumber = "861234567", Email = "charlie@example.com" }
            };

            context.Users.AddRange(users);
            context.SaveChanges();
        }
    }
}
