using Employee.Domain;
using Microsoft.EntityFrameworkCore;

namespace Employee.Persistence
{
    public class OrganisationDbContext : DbContext
    {
        public DbSet<Organisation> Organisations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new PersonConfiguration());
            modelBuilder.ApplyConfiguration(new EmployeeConfiguration());
            modelBuilder.ApplyConfiguration(new OrganisationConfiguration());

            base.OnModelCreating(modelBuilder);
        }

        public OrganisationDbContext(DbContextOptions<OrganisationDbContext> options)
            : base(options)
        { }
    }
}
