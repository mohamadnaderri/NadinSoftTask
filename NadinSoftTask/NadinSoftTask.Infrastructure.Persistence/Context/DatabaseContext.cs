using Microsoft.EntityFrameworkCore;
using NadinSoftTask.DomainModel.Product;
using NadinSoftTask.Infrastructure.Persistence.Mappings;
using System.Reflection;

namespace NadinSoftTask.Infrastructure.Persistence.Context
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {

        }

        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetAssembly(typeof(ProductMappings)));
        }
    }
}
