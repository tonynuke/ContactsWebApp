using Microsoft.EntityFrameworkCore;

namespace Employee.Persistence
{
    public class EmployeeDbContext : DbContext
    {
        public DbSet<Domain.Employee> Employees { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new EmployeeConfiguration());
            modelBuilder.ApplyConfiguration(new LinkConfiguration());

            base.OnModelCreating(modelBuilder);
        }

        public EmployeeDbContext(DbContextOptions<EmployeeDbContext> options)
            : base(options)
        { }
    }
}
