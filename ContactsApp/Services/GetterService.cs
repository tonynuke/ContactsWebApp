using System;
using System.Linq;
using CSharpFunctionalExtensions;
using Employee.Domain;
using Employee.Persistence;

namespace ContactsApp.Services
{
    public class GetterService
    {
        private readonly EmployeeDbContext dbContext;

        private Result<Link> GetLink(Employee.Domain.Employee employee, long linkId)
        {
            var link = employee.Links.SingleOrDefault(link => link.Id == linkId);
            return link == null
                ? Result.Failure<Link>($"Link with id {linkId} not found")
                : Result.Ok(link);
        }

        public Result<Link> GetLink(long employeeId, long linkId)
        {
            var employee = GetEmployee(employeeId);
            var link = employee.IsSuccess ?
                GetLink(employee.Value, linkId) :
                Result.Failure<Link>(employee.Error);

            return link;
        }

        public Result<Employee.Domain.Employee> GetEmployee(long employeeId)
        {
            var employee = this.dbContext.Employees.SingleOrDefault(e => e.Id == employeeId);
            return employee == null
                ? Result.Failure<Employee.Domain.Employee>($"Employee with id {employeeId} not found")
                : Result.Ok(employee);
        }

        public GetterService(EmployeeDbContext dbContext)
        {
            this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }
    }
}