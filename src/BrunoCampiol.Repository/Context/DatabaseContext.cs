using BrunoCampiol.Repository.Models;
using Microsoft.EntityFrameworkCore;

namespace BrunoCampiol.Repository.Context
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }

        public DbSet<LOGS> LOGS { get; set; }

        public DbSet<POSTS> POSTS { get; set; }

        public DbSet<VISITORS> VISITORS { get; set; }
    }
}
