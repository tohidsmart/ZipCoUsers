using System;
using MediatR;

namespace UserManaging.CQRS.Commands.Create
{
    public class CreateAccountCommand : IRequest<CreateAccountResponse>
    {
        public Guid UserId { get; set; }
        public string Type { get; set; }
        public decimal Balance { get; set; } = 1000;
    }
}
