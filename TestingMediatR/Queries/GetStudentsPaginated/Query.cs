using MediatR;
using TestingMediatR.Models;

namespace TestingMediatR.Queries.GetStudentsPaginated
{
        public record Query(int PageNumber) : IRequest<QueryResponse>;
}
