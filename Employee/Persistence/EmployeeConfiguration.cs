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

            builder.OwnsOne(person => person.Name)
                .Property(name => name.Value)
                .HasColumnName(nameof(Domain.Employee.Name));
            builder.OwnsOne(person => person.Surname)
                .Property(name => name.Value)
                .HasColumnName(nameof(Domain.Employee.Surname));
            builder.OwnsOne(person => person.Patronymic)
                .Property(name => name.Value)
                .HasColumnName(nameof(Domain.Employee.Patronymic));
            builder.Property(person => person.BirthDate);

            builder.OwnsOne(employee => employee.Position)
                .Property(name => name.Value)
                .HasColumnName(nameof(Domain.Employee.Position));

            var links = builder.Metadata.FindNavigation(nameof(Domain.Employee.Links));
            links.SetPropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}
