using FluentValidation;
using Microsoft.Extensions.Logging;
using UserManaging.Domain;
using UserManaging.CQRS.Commands.Create;

namespace UserManaging.CQRS.Validations
{
    public class CreateUserAccountValidator : AbstractValidator<CreateAccountCommand>
    {
        public CreateUserAccountValidator(ILogger<CreateAccountCommand> logger)
        {
            RuleFor(command => command.UserId).NotNull().NotEmpty();
            RuleFor(commnad => commnad.Type).IsEnumName(typeof(AccountType), caseSensitive: false)
                .WithMessage("This account type is not supported.Supported values are 'ZipPay' and 'ZipMoney'");
            RuleFor(command => command.Balance).GreaterThanOrEqualTo(1000).WithMessage("Balance should be greater or equal to 1000");

            logger.LogTrace("----- Account Creation - {ClassName}", GetType().Name);
        }
    }
}
