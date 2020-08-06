using System;
using CSharpFunctionalExtensions;
using Employee.Domain.Contacts;
using Employee.Persistence;
using Employee.WebService.DTO;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Employee.WebService
{
    public class EmployeesService
    {
        private readonly EmployeeDbContext dbContext;

        private Result<IEnumerable<Contact>> GetContactsFromDTO(IEnumerable<ContactDTO> contacts)
        {
            return contacts.Select(contact => Contact.Create(contact.Type, contact.Value)).Combine();
        }

        public IQueryable<Domain.Employee> GetAll()
        {
            return this.dbContext.Employees;
        }

        public async Task<Result<long>> CreateEmployee(CreateEmployeeDTO dto)
        {
            var employee = new Domain.Employee(dto.Name)
            {
                Surname = dto.Surname,
                Patronymic = dto.Patronymic,
                BirthDate = dto.BirthDate,
                Position = dto.Position,
                Organization = dto.Organization,
            };

            var contacts = this.GetContactsFromDTO(dto.Contacts);
            if (contacts.IsFailure)
            {
                return Result.Failure<long>($"Can't update contacts due to errors: {contacts.Error}");
            }
            employee.AddContacts(contacts.Value);

            await this.dbContext.Employees.AddAsync(employee);
            await this.dbContext.SaveChangesAsync();

            return Result.Success(employee.Id);
        }

        public async Task<Result> UpdateEmployee(PutEmployeeDTO dto)
        {
            var employee = await this.dbContext.Employees
                .Where(e => e.Id == dto.Id)
                .Include(e => e.Contacts).SingleOrDefaultAsync();

            if (employee == null)
            {
                return Result.Failure($"Employee with id {dto.Id} not found");
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
                return Result.Failure($"Can't update contacts due to errors: {contacts.Error}");
            }

            employee.ClearContacts();
            employee.AddContacts(contacts.Value);
            await this.dbContext.SaveChangesAsync();

            return Result.Success();
        }

        public async Task DeleteEmployee(DeleteEmployeeDTO dto)
        {
            var employee = await this.dbContext.Employees.SingleOrDefaultAsync(org => org.Id == dto.Id);
            if (employee != null)
            {
                this.dbContext.Employees.Remove(employee);
                await this.dbContext.SaveChangesAsync();
            }
        }

        public EmployeesService(EmployeeDbContext dbContext)
        {
            this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }
    }
}
