using AutoMapper;
using Employees.WebService.DTO;

namespace Employees.WebService
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<global::Employee.Domain.Contacts.Contact, ContactDTO>();

            CreateMap<global::Employee.Domain.Employee, EmployeeDTO>()
                .ForMember(nameof(EmployeeDTO.Name), opt => opt.MapFrom(c => c.Name.Value));
        }
    }
}