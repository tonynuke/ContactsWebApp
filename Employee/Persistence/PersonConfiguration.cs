using Employee.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Employee.Persistence
{
    public class PersonConfiguration : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
        {
            builder.HasKey(person => person.Id);
            builder.OwnsOne(person => person.Name)
                .Property(name => name.Value)
                .HasColumnName(nameof(Person.Name));
            builder.OwnsOne(person => person.Surname)
                .Property(name => name.Value)
                .HasColumnName(nameof(Person.Surname));
            builder.OwnsOne(person => person.Patronymic)
                .Property(name => name.Value)
                .HasColumnName(nameof(Person.Patronymic));
            builder.Property(person => person.BirthDate);
        }
    }
}
