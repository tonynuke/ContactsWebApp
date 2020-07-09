using Employee.DTO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Employee.Persistence
{
    public class OrganisationDTOConfiguration : IEntityTypeConfiguration<OrganisationDTO>
    {
        public void Configure(EntityTypeBuilder<OrganisationDTO> builder)
        {
            builder.HasKey(dto => dto.Id);
            builder.Property(dto => dto.Name);
        }
    }
}
