using Employee.DTO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Employee.Persistence
{
    public class EmployeeDTOConfiguration : IEntityTypeConfiguration<PlainEmployeeDTO>
    {
        public void Configure(EntityTypeBuilder<PlainEmployeeDTO> builder)
        {
            builder.HasKey(dto => dto.Id);
            builder.ToView("PlainEmployeeLinks");
        }
    }
}
