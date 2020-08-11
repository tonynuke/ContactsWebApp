using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Employee.Persistence
{
    public class EmployeeConfiguration : IEntityTypeConfiguration<Domain.Employee>
    {
        public void Configure(EntityTypeBuilder<Domain.Employee> builder)
        {
            builder.HasKey(employee => employee.Id);

            // Unoptimized queries using OwnsOne https://github.com/dotnet/efcore/issues/18299. Fixed in framework version 5.
            builder.OwnsOne(person => person.Name)
                .Property(name => name.Value)
                .HasColumnName(nameof(Domain.Employee.Name))
                .IsRequired();

            builder.Property(person => person.Surname);
            builder.Property(person => person.Patronymic);
            builder.Property(person => person.BirthDate);

            builder.Property(person => person.Organization);
            builder.Property(employee => employee.Position);

            builder.OwnsMany(employee => employee.Contacts,
                navigationBuilder =>
                {
                    navigationBuilder.Property(contact => contact.Type).IsRequired();
                    navigationBuilder.Property(contact => contact.Value).IsRequired();
                });

            builder.OwnsMany(employee => employee.Contacts)
                .WithOwner()
                .Metadata.PrincipalToDependent
                .SetPropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}
