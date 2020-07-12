using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.AspNet.OData;
using Contacts.WebService.DTO.Employee;
using Contacts.WebService.Services;
using Employee.Domain;
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
            var employee = new Employee.Domain.Employee(dto.Name, dto.Position);
            await this.dbContext.Employees.AddAsync(employee);

            foreach (var link in dto.Links)
            {
                employee.AddLink(link.Value, link.LinkType);
            }

            await this.dbContext.SaveChangesAsync();
            return employee.Id;
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

        public EmployeesController(ILogger<EmployeesController> logger, EmployeeDbContext dbContext, IMapper mapper)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
    }
}
