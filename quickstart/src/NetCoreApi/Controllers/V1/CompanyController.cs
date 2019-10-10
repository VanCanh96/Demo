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
using MediatR;
using NetCoreApi.Command;
using Microsoft.AspNetCore.Authorization;
using NetCoreApi.Authentication;

namespace NetCoreApi.Controllers.V1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [AuthorizeUser]
    public class CompanyController : ControllerBase
    {
        private readonly IRepository<Company> _repository;
        private readonly IMediator _mediatR;

        public CompanyController(IRepository<Company> repository, IMediator mediator)
        {
            _repository = repository;
            _mediatR = mediator;
        }

        [HttpGet("get-all")]
        public async Task<IEnumerable<Company>> GetListCompany()
        {
            var data = await _repository.GetAllAsync();
            return data;
        }

        //[HttpPost("insert-Company")]
        //public async Task<IActionResult> InsertCompany([FromBody]CreateCompanyCommand command)
        //{
        //    var media = await _mediatR.Send(command);
        //    return this.Ok(new OkObjectResult(media));
        //}
    }
}