using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.AspNet.OData;
using ContactsApp.DTO.Employee;
using ContactsApp.Services;
using Employee.Persistence;
using Microsoft.AspNet.OData.Query;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ContactsApp.Controllers
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
            await this.dbContext.SaveChangesAsync();
            return employee.Id;
        }

        [HttpPut]
        public async Task CreateEmployee([FromBody] PutEmployeeDTO dto)
        {
            var employee = this.service.GetEmployee(dto.Id);
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
