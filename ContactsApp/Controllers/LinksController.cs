using System;
using System.Threading.Tasks;
using Contacts.WebService.DTO.Link;
using Contacts.WebService.Services;
using Employee.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Contacts.WebService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LinksController : ControllerBase
    {
        private readonly ILogger<LinksController> logger;

        private readonly EmployeeDbContext dbContext;

        private readonly GetterService service;

        [HttpPost]
        public async Task<long> CreateLink([FromBody] CreateLinkDTO dto)
        {
            var employee = this.service.GetEmployee(dto.EmployeeId);
            if (employee.IsFailure)
                throw new Exception(employee.Error);

            var newLink = employee.Value.AddLink(dto.Value, dto.LinkType);
            await this.dbContext.SaveChangesAsync();
            return newLink.Id;
        }


        [HttpPut]
        public async Task CreateOrUpdateLink([FromBody] PutLinkDTO dto)
        {
            //var employee = this.service.GetEmployee(dto.EmployeeId);
            //if (employee.IsFailure)
            //    throw new Exception(employee.Error);

            //var newLink = employee.Value.AddLink(dto.Value, dto.LinkType);
            //await this.dbContext.SaveChangesAsync();
            //return newLink.Id;
        }

        [HttpDelete]
        public async Task DeleteLink([FromBody] DeleteLinkDTO dto)
        {
            var employee = this.service.GetEmployee(dto.EmployeeId);
            if (employee.IsFailure)
                throw new Exception(employee.Error);

            foreach(var id in dto.LinkIds)
            {
                employee.Value.RemoveLink(id);
            }
            await this.dbContext.SaveChangesAsync();
        }

        public LinksController(ILogger<LinksController> logger, EmployeeDbContext dbContext)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }
    }
}
