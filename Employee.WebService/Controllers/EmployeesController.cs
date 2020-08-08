using System;
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

        [ProducesResponseType(typeof(IQueryable<EmployeeDTO>), (int)HttpStatusCode.OK)]
        [HttpGet]
        public Task<IQueryable<EmployeeDTO>> Get(ODataQueryOptions<EmployeeDTO> options)
        {
            var employees = this.employeesService.GetAll().AsNoTracking();
            return employees.GetQueryAsync(this.mapper, options);
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
        public async Task<IActionResult> UpdateEmployee([FromBody] PutEmployeeDTO dto)
        {
            var result = await this.employeesService.UpdateEmployee(dto);
            return result.IsSuccess ? Ok() : Error(result.Error);
        }

        [ProducesResponseType(typeof(Envelope), (int)HttpStatusCode.OK)]
        [HttpDelete]
        public async Task<IActionResult> DeleteEmployee([FromBody] DeleteEmployeeDTO dto)
        {
            await this.employeesService.DeleteEmployee(dto);
            return Ok();
        }

        public EmployeesController(EmployeesService employeesService, IMapper mapper)
        {
            this.employeesService = employeesService ?? throw new ArgumentNullException(nameof(employeesService));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
    }
}
