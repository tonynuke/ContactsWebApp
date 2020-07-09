using System;
using System.Linq;
using CSharpFunctionalExtensions;
using Employee.Domain;
using Employee.Persistence;

namespace ContactsApp
{
    public class OrganizationService
    {
        private readonly OrganizationDbContext dbContext;

        private Result<Employee.Domain.Employee> GetEmployee(Organization organization, long employeeId)
        {
            var employee = organization.Employees.SingleOrDefault(employee => employee.Id == employeeId);
            return employee == null
                ? Result.Failure<Employee.Domain.Employee>($"Employee with id {employeeId} not found")
                : Result.Ok(employee);
        }

        private Result<Link> GetLink(Employee.Domain.Employee employee, long linkId)
        {
            var link = employee.Links.SingleOrDefault(link => link.Id == linkId);
            return link == null
                ? Result.Failure<Link>($"Link with id {linkId} not found")
                : Result.Ok(link);
        }

        public Result<Organization> GetOrganization(long id)
        {
            var organization = this.dbContext.Organizations
                .SingleOrDefault(org => org.Id == id);
            return organization == null
                ? Result.Failure<Organization>($"Organization with id {id} not found")
                : Result.Ok(organization);
        }

        public Result<Employee.Domain.Employee> GetEmployee(long organizationId, long employeeId)
        {
            var organization = GetOrganization(organizationId);
            var employee = organization.IsSuccess ?
                GetEmployee(organization.Value, employeeId) :
                Result.Failure<Employee.Domain.Employee>(organization.Error);

            return employee;
        }

        public Result<Link> GetLink(long organizationId, long employeeId, long linkId)
        {
            var employee = GetEmployee(organizationId, employeeId);
            var link = employee.IsSuccess ?
                GetLink(employee.Value, linkId) :
                Result.Failure<Link>(employee.Error);

            return link;
        }

        public OrganizationService(OrganizationDbContext dbContext)
        {
            this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }
    }
}