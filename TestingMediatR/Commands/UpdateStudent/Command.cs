using MediatR;
using TestingMediatR.Models;

namespace TestingMediatR.Commands.UpdateStudent
{
    public record Command(int Id, string StudentName, string StudentEmail, string StudentAddress, int StudentAge, string PhoneNumber, StudentGrade studentGrade) : IRequest<bool>;

}
