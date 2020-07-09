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

        private readonly OrganizationDbContext dbContext;

        private readonly OrganizationService service;

        [HttpPost]
        public async Task<long> CreateLink([FromBody] CreateLinkDTO dto)
        {
            var employee = this.service.GetEmployee(dto.OrganizationId, dto.EmployeeId);
            if (employee.IsFailure)
                throw new Exception(employee.Error);

            var newLink = employee.Value.AddLink(dto.Value, dto.LinkType);
            await this.dbContext.SaveChangesAsync();
            return newLink.Id;
        }

        [HttpDelete]
        public async Task<long> CreateLink([FromBody] DeleteLinkDTO dto)
        {
            var employee = this.service.GetEmployee(dto.OrganizationId, dto.EmployeeId);
            if (employee.IsFailure)
                throw new Exception(employee.Error);

            employee.Value.RemoveLink(dto.LinkId);
            await this.dbContext.SaveChangesAsync();
        }

        public LinksController(ILogger<LinksController> logger, OrganizationDbContext dbContext)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }
    }
}
