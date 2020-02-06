using FluentValidation;
using FluentValidation.Validators;
using Microsoft.Extensions.Logging;
using UserManaging.CQRS.Commands.Create;

namespace UserManaging.CQRS.Validations
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(command=>command.Email).EmailAddress(EmailValidationMode.AspNetCoreCompatible).WithMessage("Email is not in the right format");
            RuleFor(command => command.FirstName).NotEmpty().WithName("First Name").WithMessage("First name can not be null or empty");
            RuleFor(command=>command.LastName).NotEmpty().WithName("Last Name").WithMessage("Last name can not be null or empty");
            RuleFor(command => command.Salary).GreaterThan(0).WithMessage("Salary must be a positive number");
            RuleFor(command => command.Expenses).GreaterThan(0).WithMessage("Expenses must be a positive number");
        }
    }
}
