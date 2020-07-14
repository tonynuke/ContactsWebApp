using Employee.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Employee.Persistence
{
    public class ContactConfiguration : IEntityTypeConfiguration<Contact>
    {
        public void Configure(EntityTypeBuilder<Contact> builder)
        {
            builder.HasKey(contact => contact.Id);
            builder.Property(contact => contact.Value).IsRequired();
            builder.Property(contact => contact.Type).IsRequired();
        }
    }
}
