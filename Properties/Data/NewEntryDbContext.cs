using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using webapi.Models;
using webapi.Models.NewEntry;


namespace webapi.Properties.Data
{
    public class NewEntryDbContext : DbContext
    {
        internal readonly object UserRegistrations;

        public NewEntryDbContext(DbContextOptions<NewEntryDbContext> options) : base(options)
        {
        }
        public DbSet<NewEntries> NewEntries { get; set; }
    
    }
}
