using Employee.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Employee.Persistence
{
    public class OrganisationConfiguration : IEntityTypeConfiguration<Organisation>
    {
        public void Configure(EntityTypeBuilder<Organisation> builder)
        {
            builder.HasKey(organisation => organisation.Id);

            // Есть бага https://github.com/dotnet/efcore/issues/18299. Говорят исправили в 5 версии.
            builder.OwnsOne(organisation => organisation.Name)
                .Property(name => name.Value)
                .HasColumnName(nameof(Organisation.Name))
                .IsRequired();

            var employees = builder.Metadata.FindNavigation(nameof(Organisation.Employees));
            employees.SetPropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}
