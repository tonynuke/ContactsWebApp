﻿using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.AspNet.OData;
using Employee.WebService.DTO;
using Microsoft.AspNet.OData.Query;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Employee.WebService.Controllers
{
    [Produces("application/json")]
    [ApiController]
    [Route("[controller]")]
    public class EmployeesController : EnvelopController
    {
        private readonly EmployeesService employeesService;

        private readonly IMapper mapper;

        [ProducesResponseType(typeof(Envelope<IQueryable<EmployeeDTO>>), (int)HttpStatusCode.OK)]
        [HttpGet]
        public async Task<IActionResult> Get(ODataQueryOptions<EmployeeDTO> options)
        {
            var employees = this.employeesService.GetAll().AsNoTracking();
            var result = await employees.GetQueryAsync(this.mapper, options);
            return Ok(result);
        }

        [ProducesResponseType(typeof(Envelope<long>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(Envelope), (int)HttpStatusCode.BadRequest)]
        [HttpPost]
        public async Task<IActionResult> CreateEmployee([FromBody] CreateEmployeeDTO dto)
        {
            var result = await this.employeesService.CreateEmployee(dto);
            return result.IsSuccess ? Ok(result.Value) : Error(result.Error);
        }

        [ProducesResponseType(typeof(Envelope), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(Envelope), (int)HttpStatusCode.BadRequest)]
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateEmployee(long id, [FromBody] PutEmployeeDTO dto)
        {
            var result = await this.employeesService.UpdateEmployee(id, dto);
            return result.IsSuccess ? Ok() : Error(result.Error);
        }

        [ProducesResponseType(typeof(Envelope), (int)HttpStatusCode.OK)]
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteEmployee(long id)
        {
            await this.employeesService.DeleteEmployee(id);
            return Ok();
        }

        public EmployeesController(EmployeesService employeesService, IMapper mapper)
        {
            this.employeesService = employeesService ?? throw new ArgumentNullException(nameof(employeesService));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
    }
}
