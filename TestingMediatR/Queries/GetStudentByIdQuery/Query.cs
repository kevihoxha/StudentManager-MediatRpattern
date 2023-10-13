using MediatR;
using TestingMediatR.Models;

namespace TestingMediatR.Queries.GetStudentByIdQuery
{
    public record Query(int Id) : IRequest<StudentDetails>;

}
