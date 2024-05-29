using UserRegistrationwebapi.Models;
using Microsoft.EntityFrameworkCore;
using webapi.Models.NewEntry;

namespace UserRegwebapi.Data
{
    public class UserRegDbContext : DbContext
    {
        

        public UserRegDbContext(DbContextOptions<UserRegDbContext> options) : base(options)
        {
        }
        public DbSet<UserRegistrations> UserRegistrations { get; set; }
        public DbSet<NewEntries> NewEntries { get; set; }
    }
}
