using Employee.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Employee.Persistence
{
    public class OrganizationConfiguration : IEntityTypeConfiguration<Organization>
    {
        public void Configure(EntityTypeBuilder<Organization> builder)
        {
            builder.HasKey(organization => organization.Id);

            // Баг с неоптимальными запросами для OwnsOne https://github.com/dotnet/efcore/issues/18299. Говорят исправили в 5 версии.
            builder.OwnsOne(organization => organization.Name)
                .Property(name => name.Value)
                .HasColumnName(nameof(Organization.Name))
                .IsRequired();

            var employees = builder.Metadata.FindNavigation(nameof(Organization.Employees));
            employees.SetPropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}
