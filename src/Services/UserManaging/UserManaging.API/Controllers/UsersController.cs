using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using UserManaging.Domain;
using UserManaging.CQRS.Commands.Create;
using UserManaging.CQRS.Queries;

namespace UserManaging.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class UsersController : ControllerBase
    {

        private readonly IMediator mediator;

        public UsersController(IMediator mediator)
        {
            this.mediator = mediator;
        }


        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateUserCommand command)
        {
            var response = await mediator.Send(command);
            return Created("api/v1/user", response);
        }


        [HttpGet]
        [Route("list")]

        public async Task<IActionResult> ListUsers()
        {
            var response = await mediator.Send(new QueryUsersRequest());
            return Ok(response);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetUser([FromRoute]Guid id)
        {
            var response = await mediator.Send(new QueryUserRequest { UserId = id });
            return Ok(response);
        }

    }
}
