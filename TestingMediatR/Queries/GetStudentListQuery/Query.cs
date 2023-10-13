using MediatR;
using TestingMediatR.Models;

namespace TestingMediatR.Queries.GetStudentListQuery
{
    public record Query : IRequest<IQueryable<StudentDetails>>
    {
    }
}
