using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.AspNet.OData;
using Contacts.WebService.DTO.Employee;
using Contacts.WebService.Services;
using Employee.Persistence;
using Microsoft.AspNet.OData.Query;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Contacts.WebService.Controllers
{
    [Route("[controller]")]
    public class EmployeesController : ControllerBase
    {
        private readonly ILogger<EmployeesController> logger;

        private readonly EmployeeDbContext dbContext;

        private readonly IMapper mapper;

        private readonly GetterService service;

        [HttpGet]
        public Task<IQueryable<EmployeeDTO>> Get(ODataQueryOptions<EmployeeDTO> options)
        {
            var request = this.dbContext.Employees.AsNoTracking();
            return request.GetQueryAsync(this.mapper, options);
        }

        [HttpPost]
        public async Task<long> CreateEmployee([FromBody] CreateEmployeeDTO dto)
        {
            var employee = new Employee.Domain.Employee(dto.Name, dto.Position)
            {
                Surname = dto.Surname,
                Patronymic = dto.Patronymic,
                BirthDate = dto.BirthDate,
                Position = dto.Position,
                Organization = dto.Organization
            };

            await this.dbContext.Employees.AddAsync(employee);

            foreach (var link in dto.Links)
            {
                employee.AddLink(link.Value, link.LinkType);
            }

            await this.dbContext.SaveChangesAsync();
            return employee.Id;
        }

        [HttpPut]
        public async Task UpdateEmployee([FromBody] PutEmployeeDTO dto)
        {
            var employee = this.service.GetEmployee(dto.Id);
            if (employee.IsFailure)
                throw new Exception(employee.Error);

            employee.Value.Name = dto.Name;
            employee.Value.Surname = dto.Surname;
            employee.Value.Patronymic = dto.Patronymic;
            employee.Value.BirthDate = dto.BirthDate;
            employee.Value.Organization = dto.Organization;
            employee.Value.Position = dto.Position;

            var linksToCreate = dto.Links.Where(link => link.Id < 0).ToList();
            var linksIdsToDelete = employee.Value.Links.Select(link => link.Id)
                .Except(dto.Links.Select(link => link.Id)).ToList();
            var linksIdsToChange = employee.Value.Links.Select(link => link.Id)
                .Intersect(dto.Links.Select(link => link.Id)).ToList();

            foreach (var link in linksToCreate)
                employee.Value.AddLink(link.Value, link.LinkType);

            foreach (var linkId in linksIdsToDelete)
                employee.Value.RemoveLink(linkId);

            foreach (var linkId in linksIdsToChange)
            {
                var dtoLink = dto.Links.Single(link => link.Id == linkId);
                var linkToChange = employee.Value.Links.Single(link => link.Id == linkId);
                linkToChange.Value = dtoLink.Value;
                linkToChange.Type = dtoLink.LinkType;
            }

            await this.dbContext.SaveChangesAsync();
        }

        [HttpDelete]
        public async Task DeleteEmployee([FromBody] DeleteEmployeeDTO dto)
        {
            foreach (var id in dto.Ids)
            {
                var employee = await this.dbContext.Employees.SingleOrDefaultAsync(org => org.Id == id);
                if (employee != null)
                {
                    this.dbContext.Employees.Remove(employee);
                }
            }

            await this.dbContext.SaveChangesAsync();
        }

        public EmployeesController(ILogger<EmployeesController> logger, EmployeeDbContext dbContext, IMapper mapper,
            GetterService service)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            this.service = service ?? throw new ArgumentNullException(nameof(service));
        }
    }
}
