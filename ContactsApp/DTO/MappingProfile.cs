using AutoMapper;
using Contacts.WebService.DTO.Employee;
using Contacts.WebService.DTO.Link;

namespace Contacts.WebService.DTO
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<global::Employee.Domain.Link, LinkDTO>();

            CreateMap<global::Employee.Domain.Employee, EmployeeDTO>()
                .ForMember(nameof(EmployeeDTO.Name), opt => opt.MapFrom(c => c.Name.Value))
                .ForMember(nameof(EmployeeDTO.Surname), opt => opt.MapFrom(c => c.Surname.Value))
                .ForMember(nameof(EmployeeDTO.Patronymic), opt => opt.MapFrom(c => c.Patronymic.Value))
                .ForMember(nameof(EmployeeDTO.Position), opt => opt.MapFrom(c => c.Position.Value))
                .ForMember(nameof(EmployeeDTO.Organization), opt => opt.MapFrom(c => c.Organization.Value));
        }
    }
}