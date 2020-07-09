using Employee.DTO;
using Microsoft.EntityFrameworkCore;

namespace Employee.Persistence
{
    public class OrganisationQueryDbContext : DbContext
    {
        public DbSet<PlainEmployeeDTO> Employees { get; set; }
        public DbSet<OrganisationDTO> Organisations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new EmployeeDTOConfiguration());
            modelBuilder.ApplyConfiguration(new OrganisationDTOConfiguration());

            base.OnModelCreating(modelBuilder);
        }

        public OrganisationQueryDbContext(DbContextOptions<OrganisationQueryDbContext> options)
            : base(options)
        { }
    }
}
