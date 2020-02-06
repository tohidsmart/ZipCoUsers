using MediatR;

namespace UserManaging.CQRS.Commands.Create
{
    public class CreateUserCommand : IRequest<CreateUserResponse>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string Email { get; set; }

        public decimal Salary { get; set; }

        public decimal Expenses { get; set; }


    }
}
