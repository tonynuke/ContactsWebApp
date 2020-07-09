﻿using System;
using System.Linq;
using System.Threading.Tasks;
using ContactsApp.DTO;
using Employee.Persistence;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ContactsApp.Controllers
{
    [Route("[controller]")]
    public class EmployeesController : ControllerBase
    {
        private readonly ILogger<EmployeesController> logger;

        private readonly OrganisationDbContext dbContext;

        [HttpGet]
        [EnableQuery]
        public IQueryable<Employee.Domain.Employee> Get()
        {
            return this.dbContext.Organisations.AsNoTracking()
                .SelectMany(org => org.Employees);
        }

        [HttpPost]
        [Route("employee")]
        public async Task<long> CreateEmployee([FromBody] CreateEmployeeDTO dto)
        {
            var organisation = this.dbContext.Organisations
                .SingleOrDefault(org => org.Id == dto.OrganisationId);
            if (organisation == null)
                throw new Exception($"Организация с id {dto.OrganisationId} не найдена");

            var newEmployee = organisation.CreateEmployee(dto.Name, dto.Position);
            await this.dbContext.SaveChangesAsync();
            return newEmployee.Id;
        }

        public EmployeesController(ILogger<EmployeesController> logger, OrganisationDbContext dbContext)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }
    }
}
