using System;
using System.Linq;
using System.Threading.Tasks;
using ContactsApp.DTO;
using Employee.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ContactsApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LinksController : ControllerBase
    {
        private readonly ILogger<LinksController> logger;

        private readonly OrganisationDbContext dbContext;

        [HttpPost]
        public async Task<long> CreateLink([FromBody] CreateLinkDTO dto)
        {
            var organisation = this.dbContext.Organisations
                .SingleOrDefault(org => org.Id == dto.OrganisationId);
            if (organisation == null)
                throw new Exception($"Организация с id {dto.OrganisationId} не найдена");

            var employee = organisation.Employees.SingleOrDefault(e => e.Id == dto.EmployeeId);
            if (employee == null)
                throw new Exception($"Сотрудник с id {dto.EmployeeId} не найден");

            var newLink = employee.AddLink(dto.Value, dto.LinkType);
            await this.dbContext.SaveChangesAsync();
            return newLink.Id;
        }

        public LinksController(ILogger<LinksController> logger, OrganisationDbContext dbContext)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }
    }
}
