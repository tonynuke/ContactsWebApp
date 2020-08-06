using AutoMapper;
using Employee.WebService.DTO;

namespace Employee.WebService
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Domain.Contacts.Contact, ContactDTO>();

            CreateMap<Domain.Employee, EmployeeDTO>()
                .ForMember(nameof(EmployeeDTO.Name), opt => opt.MapFrom(c => c.Name.Value));
        }
    }
}