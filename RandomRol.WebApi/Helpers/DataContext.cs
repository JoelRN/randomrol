using Microsoft.EntityFrameworkCore;
using RandomRol.WebApi.Entities;

namespace RandomRol.WebApi.Helpers
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Usuarios> Usuarios { get; set; }
    }
}