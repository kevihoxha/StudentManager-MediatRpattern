using MediatR;

namespace TestingMediatR.Commands.DeleteCommand
{
    public record Command(int Id) : IRequest<bool>;
}
