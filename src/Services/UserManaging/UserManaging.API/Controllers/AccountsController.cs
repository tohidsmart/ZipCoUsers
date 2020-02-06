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
    public class AccountsController : ControllerBase
    {

        private readonly ILogger<UsersController> logger;
        private readonly IMediator mediator;

        public AccountsController(ILogger<UsersController> logger, IMediator mediator)
        {
            this.mediator = mediator;
            this.logger = logger;
        }


        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateAccountCommand command)
        {
            var response = await mediator.Send(command);
            return Created("api/v1/accounts", response);
        }

        [HttpGet]
        [Route("list")]
        public async Task<IActionResult> ListAccounts()
        {
            var response = await mediator.Send(new QueryAccountsRequest());
            return Ok(response);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetAccount([FromRoute]Guid id)
        {
            var response = await mediator.Send(new QueryAccountRequest { AccountId = id });
            return Ok(response);
        }




    }
}
