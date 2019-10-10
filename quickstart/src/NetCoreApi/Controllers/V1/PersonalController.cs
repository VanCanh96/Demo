using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using NetCoreApi.Command;
using NetCoreApi.Models;
using NetCoreApi.Repositoties;
using NetCoreApi.Repositoties.Interface;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NetCoreApi.Controllers.V1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]

    public class PersonalController : ControllerBase
    {
        IRepositoryBase<Personal> _repositoryBase;
        IMediator _mediator;

        public PersonalController(IRepositoryBase<Personal> repositoryBase, IMediator mediator)
        {
            _repositoryBase = repositoryBase;
            _mediator = mediator;
        }

        // GET: api/Personal
        [HttpGet]
        public async  Task<IEnumerable<Personal>> GetAll()
        {
            var data = await _repositoryBase.GetAll();
            return data;
        }

        // GET: api/Personal/5
        [HttpGet("{id}", Name = "Get")]
        public void Get(int? id)
        {
            _repositoryBase.GetById(id.Value);
        }

        // POST: api/Personal
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]PersonalCommand command)
        {
            var media = await _mediator.Send(command);
            return this.Ok(new OkObjectResult(media));
        }

        // PUT: api/Personal/5
        [HttpPut("{id}")]
        public void Put(int? id)
        {
            _repositoryBase.GetById(id.Value);
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int? id)
        {
            _repositoryBase.Delete(id.Value);
        }
    }
}