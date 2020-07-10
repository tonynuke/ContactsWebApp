using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Employee.Persistence
{
    public class EmployeeConfiguration : IEntityTypeConfiguration<Domain.Employee>
    {
        public void Configure(EntityTypeBuilder<Domain.Employee> builder)
        {
            builder.HasKey(employee => employee.Id);

            // Баг с неоптимальными запросами для OwnsOne https://github.com/dotnet/efcore/issues/18299. Говорят исправили в 5 версии.
            builder.OwnsOne(person => person.Name)
                .Property(name => name.Value)
                .HasColumnName(nameof(Domain.Employee.Name))
                .IsRequired();
            builder.OwnsOne(person => person.Surname)
                .Property(name => name.Value)
                .HasColumnName(nameof(Domain.Employee.Surname));
            builder.OwnsOne(person => person.Patronymic)
                .Property(name => name.Value)
                .HasColumnName(nameof(Domain.Employee.Patronymic));
            builder.OwnsOne(person => person.Organisation)
                .Property(name => name.Value)
                .HasColumnName(nameof(Domain.Employee.Organisation));
            builder.Property(person => person.BirthDate);

            builder.OwnsOne(employee => employee.Position)
                .Property(name => name.Value)
                .HasColumnName(nameof(Domain.Employee.Position))
                .IsRequired();

            var links = builder.Metadata.FindNavigation(nameof(Domain.Employee.Links));
            links.SetPropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}
