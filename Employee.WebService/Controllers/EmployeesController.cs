using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.AspNet.OData;
using CSharpFunctionalExtensions;
using Employee.Domain.Contacts;
using Employee.Persistence;
using Employees.WebService.DTO;
using Microsoft.AspNet.OData.Query;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Employees.WebService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmployeesController : ControllerBase
    {
        private readonly EmployeeDbContext dbContext;

        private readonly IMapper mapper;

        [HttpGet]
        public Task<IQueryable<EmployeeDTO>> Get(ODataQueryOptions<EmployeeDTO> options)
        {
            var request = this.dbContext.Employees.AsNoTracking();
            return request.GetQueryAsync(this.mapper, options);
        }

        [HttpPost]
        public async Task<IActionResult> CreateEmployee([FromBody] CreateEmployeeDTO dto)
        {
            var employee = new Employee.Domain.Employee(dto.Name)
            {
                Surname = dto.Surname,
                Patronymic = dto.Patronymic,
                BirthDate = dto.BirthDate,
                Position = dto.Position,
                Organization = dto.Organization,
            };

            await this.dbContext.Employees.AddAsync(employee);

            var contacts = this.GetContactsFromDTO(dto.Contacts);
            if (contacts.IsFailure)
            {
                return BadRequest($"Can't update contacts due to errors: {contacts.Error}");
            }

            employee.AddContacts(contacts.Value);

            await this.dbContext.SaveChangesAsync();

            return Ok(employee.Id);
        }

        private Result<IEnumerable<Contact>> GetContactsFromDTO(IEnumerable<ContactDTO> contacts)
        {
            return contacts.Select(contact => Contact.Create(contact.Type, contact.Value)).Combine();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateEmployee([FromBody] PutEmployeeDTO dto)
        {
            var employee = await this.dbContext.Employees
                .Where(e => e.Id == dto.Id)
                .Include(e => e.Contacts).SingleOrDefaultAsync();

            if (employee == null)
            {
                return BadRequest($"Employee with id {dto.Id} not found");
            }

            employee.Name = dto.Name;
            employee.Surname = dto.Surname;
            employee.Patronymic = dto.Patronymic;
            employee.BirthDate = dto.BirthDate;
            employee.Organization = dto.Organization;
            employee.Position = dto.Position;

            var contacts = this.GetContactsFromDTO(dto.Contacts);
            if (contacts.IsFailure)
            {
                return BadRequest($"Can't update contacts due to errors: {contacts.Error}");
            }

            employee.ClearContacts();
            employee.AddContacts(contacts.Value);

            await this.dbContext.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete]
        public async Task DeleteEmployee([FromBody] DeleteEmployeeDTO dto)
        {
            var employee = await this.dbContext.Employees.SingleOrDefaultAsync(org => org.Id == dto.Id);
            if (employee != null)
            {
                this.dbContext.Employees.Remove(employee);
                await this.dbContext.SaveChangesAsync();
            }
        }

        public EmployeesController(EmployeeDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
    }
}
