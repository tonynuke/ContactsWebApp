using Employee.Domain;
using Microsoft.EntityFrameworkCore;

namespace Employee.Persistence
{
    public class OrganizationDbContext : DbContext
    {
        public DbSet<Organization> Organizations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new EmployeeConfiguration());
            modelBuilder.ApplyConfiguration(new OrganizationConfiguration());

            base.OnModelCreating(modelBuilder);
        }

        public OrganizationDbContext(DbContextOptions<OrganizationDbContext> options)
            : base(options)
        { }
    }
}
