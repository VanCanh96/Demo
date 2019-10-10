using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper.Contrib.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using NetCoreApi.Models;
using NetCoreApi.Repository;
using Npgsql;
using System.Linq;
using MediatR;
using NetCoreApi.Command;
using Microsoft.AspNetCore.Authorization;

namespace NetCoreApi.Controllers.V1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize]
    public class EmployeeController : ControllerBase
    {
        private readonly IRepository<Employee> _repository;
        private readonly IMediator _mediatR;

        public EmployeeController(IRepository<Employee> repository, IMediator mediator)
        {
            _repository = repository;
            _mediatR = mediator;
        }

        [HttpGet("get-all")]
        public async Task<IEnumerable<Employee>> GetListEmployee()
        {
            var data = await _repository.GetAllAsync();
            return data;
        }

        [HttpPost("insert-employee")]
        public async Task<IActionResult> InsertEmployee([FromBody]CreateEmployeeCommand command)
        {
            var media = await _mediatR.Send(command);
            return this.Ok(new OkObjectResult(media));
        }
    }
}