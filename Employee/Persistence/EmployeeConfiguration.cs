using Employee.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Employee.Persistence
{
    public class EmployeeConfiguration : IEntityTypeConfiguration<Domain.Employee>
    {
        public void Configure(EntityTypeBuilder<Domain.Employee> builder)
        {
            builder.HasKey(employee => employee.Id);
            builder.OwnsOne(employee => employee.Position)
                .Property(name => name.Value)
                .HasColumnName(nameof(Domain.Employee.Position));
            builder.HasOne(employee => employee.Person)
                .WithOne().HasForeignKey<Person>("PersonFK");
        }
    }
}
