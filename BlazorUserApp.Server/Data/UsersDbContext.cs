using BlazorUserApp.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace BlazorUserApp.Server.Data
{
    public class UsersDbContext : DbContext
    {
        public UsersDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
    }
}