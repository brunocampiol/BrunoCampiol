using BrunoCampiol.Infra.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace BrunoCampiol.Infra.Data.Context
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }

        public DbSet<LOGS> LOGS { get; set; }

        public DbSet<POSTS> POSTS { get; set; }

        public DbSet<VISITORS> VISITORS { get; set; }
    }
}
