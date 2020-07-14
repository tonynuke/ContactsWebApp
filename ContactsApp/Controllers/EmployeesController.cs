using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.AspNet.OData;
using Contacts.WebService.DTO.Employee;
using Employee.Persistence;
using Microsoft.AspNet.OData.Query;
using Microsoft.AspNet.OData.Routing;
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

        [ODataRoute("a")]
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

            foreach (var contact in dto.Contacts)
            {
                employee.AddContact(contact.Value, contact.Type);
            }

            await this.dbContext.SaveChangesAsync();
            return employee.Id;
        }

        [HttpPut]
        public async Task UpdateEmployee([FromBody] PutEmployeeDTO dto)
        {
            var employee = await this.dbContext.Employees
                .Where(e => e.Id == dto.Id)
                .Include(e => e.Contacts).SingleOrDefaultAsync();

            if (employee == null)
                throw new Exception($"Employee with id {dto.Id} not found");

            employee.Name = dto.Name;
            employee.Surname = dto.Surname;
            employee.Patronymic = dto.Patronymic;
            employee.BirthDate = dto.BirthDate;
            employee.Organization = dto.Organization;
            employee.Position = dto.Position;

            var contactsToCreate = dto.Contacts.Where(contact=> contact.Id < 0).ToList();
            var contactsIdsToDelete = employee.Contacts.Select(contact => contact.Id)
                .Except(dto.Contacts.Select(contact => contact.Id)).ToList();
            var contactsIdsToChange = employee.Contacts.Select(contact => contact.Id)
                .Intersect(dto.Contacts.Select(contact => contact.Id)).ToList();

            foreach (var contact in contactsToCreate)
                employee.AddContact(contact.Value, contact.Type);

            foreach (var contactId in contactsIdsToDelete)
                employee.RemoveContact(contactId);

            foreach (var contactId in contactsIdsToChange)
            {
                var dtoContact = dto.Contacts.Single(contact => contact.Id == contactId);
                var contactToChange = employee.Contacts.Single(contact => contact.Id == contactId);
                contactToChange.Value = dtoContact.Value;
                contactToChange.Type = dtoContact.Type;
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

        public EmployeesController(ILogger<EmployeesController> logger, EmployeeDbContext dbContext, IMapper mapper)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
    }
}
