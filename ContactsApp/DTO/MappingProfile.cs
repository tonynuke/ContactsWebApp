using AutoMapper;
using Employee.Domain;

namespace ContactsApp.DTO
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Link, LinkDTO>();

            CreateMap<Organisation, OrganisationDTO>()
                .ForMember(nameof(OrganisationDTO.Name), opt => opt.MapFrom(c => c.Name.Value))
                .ForMember(nameof(OrganisationDTO.Name), opt => opt.MapFrom(c => c.Name.Value));

            CreateMap<Employee.Domain.Employee, EmployeeDTO>()
                .ForMember(nameof(EmployeeDTO.Name), opt => opt.MapFrom(c => c.Name.Value))
                .ForMember(nameof(EmployeeDTO.Surname), opt => opt.MapFrom(c => c.Surname.Value))
                .ForMember(nameof(EmployeeDTO.Patronymic), opt => opt.MapFrom(c => c.Patronymic.Value))
                .ForMember(nameof(EmployeeDTO.Position), opt => opt.MapFrom(c => c.Position.Value));
        }
    }
}