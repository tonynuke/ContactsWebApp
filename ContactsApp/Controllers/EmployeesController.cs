using System;
using System.Threading.Tasks;
using AutoMapper;
using ContactsApp.DTO;
using Employee.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ContactsApp.Controllers
{
    [Route("[controller]")]
    public class EmployeesController : ControllerBase
    {
        private readonly ILogger<EmployeesController> logger;

        private readonly OrganizationDbContext dbContext;

        private readonly IMapper mapper;

        private readonly OrganizationService service;

        [HttpPost]
        public async Task<long> CreateEmployee([FromBody] CreateEmployeeDTO dto)
        {
            var organization = this.service.GetOrganization(dto.OrganizationId);
            if (organization.IsFailure)
                throw new Exception(organization.Error);

            var newEmployee = organization.Value.CreateEmployee(dto.Name, dto.Position);
            await this.dbContext.SaveChangesAsync();
            return newEmployee.Id;
        }

        [HttpPatch]
        public async Task CreateEmployee([FromBody] PatchEmployeeDTO dto)
        {
            var employee = this.service.GetEmployee(dto.OrganizationId, dto.EmployeeId);
            if (employee.IsFailure)
                throw new Exception(employee.Error);

            employee.Value.Name = dto.Name;
            employee.Value.Surname = dto.Surname;
            employee.Value.Position = dto.Position;
            await this.dbContext.SaveChangesAsync();
        }

        [HttpDelete]
        public async Task DeleteEmployee([FromBody] DeleteEmployeeDTO dto)
        {
            var organization = this.service.GetOrganization(dto.OrganizationId);
            if (organization.IsFailure)
                throw new Exception(organization.Error);

            organization.Value.RemoveEmployee(dto.EmployeeId);
            await this.dbContext.SaveChangesAsync();
        }

        public EmployeesController(ILogger<EmployeesController> logger, OrganizationDbContext dbContext, IMapper mapper)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
    }
}
