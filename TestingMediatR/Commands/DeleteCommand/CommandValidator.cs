using FluentValidation;

namespace TestingMediatR.Commands.DeleteCommand
{
    public class CommandValidator : AbstractValidator<Command>
    {
        public CommandValidator()
        {
            RuleFor(command => command.Id).GreaterThan(0).WithMessage("Invalid student ID");
        }
    }
}
