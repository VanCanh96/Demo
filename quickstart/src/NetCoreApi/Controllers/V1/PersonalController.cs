using MediatR;
using Microsoft.AspNetCore.Mvc;
using NetCoreApi.Command;
using NetCoreApi.Models;
using NetCoreApi.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NetCoreApi.Controllers.V1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]

    public class PersonalController : ControllerBase
    {
        IRepository<Personal> _personRepo;
        IMediator _mediator;

        public PersonalController(IRepository<Personal> personRepo, IMediator mediator)
        {
            _personRepo = personRepo;
            _mediator = mediator;
        }

        // GET: api/Personal
        [HttpGet]
        public async  Task<IEnumerable<Personal>> GetAll()
        {
            var data = await _personRepo.GetAllAsync();
            return data;
        }


        // POST: api/Personal
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]PersonalCommand command)
        {
            var media = await _mediator.Send(command);
            return this.Ok(new OkObjectResult(media));
        }

        
    }
}