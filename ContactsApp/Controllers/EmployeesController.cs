using System;
using System.Linq;
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

        private readonly OrganisationDbContext dbContext;

        private readonly IMapper mapper;

        [HttpPost]
        public async Task<long> CreateEmployee([FromBody] CreateEmployeeDTO dto)
        {
            var organisation = this.dbContext.Organisations
                .SingleOrDefault(org => org.Id == dto.OrganisationId);
            if (organisation == null)
                throw new Exception($"Организация с id {dto.OrganisationId} не найдена");

            var newEmployee = organisation.CreateEmployee(dto.Name, dto.Position);
            await this.dbContext.SaveChangesAsync();
            return newEmployee.Id;
        }

        [HttpDelete]
        public async Task DeleteEmployee([FromBody] DeleteEmployeeDTO dto)
        {
            var organisation = this.dbContext.Organisations.SingleOrDefault(org => org.Id == dto.OrganisationId);
            if (organisation == null)
                throw new Exception($"Организация с id {dto.OrganisationId} не найдена");

            organisation.RemoveEmployee(dto.EmployeeId);
            await this.dbContext.SaveChangesAsync();
        }

        public EmployeesController(ILogger<EmployeesController> logger, OrganisationDbContext dbContext, IMapper mapper)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
    }
}
