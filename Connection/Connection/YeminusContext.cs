using Entities;
using Microsoft.EntityFrameworkCore;

namespace Connection.Connection
{
    public class YeminusContext : DbContext
    {
        public YeminusContext (DbContextOptions options) : base (options)
        {

        }
        public DbSet<Producto> Producto { get; set; }
        public DbSet<Precio> Precio { get; set; }
    }
}
