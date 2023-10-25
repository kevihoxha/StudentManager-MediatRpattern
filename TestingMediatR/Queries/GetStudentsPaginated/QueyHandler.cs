using MediatR;
using TestingMediatR.Models;
using TestingMediatR.Repositories;
using TestingMediatR.Services;

namespace TestingMediatR.Queries.GetStudentsPaginated
{
    public class QueryHandler : IRequestHandler<Queries.GetStudentsPaginated.Query, QueryResponse>
    {
        private readonly IStudentService _studentService;
        public QueryHandler(IStudentService studentService)
        {
            _studentService = studentService;
        }
        public async Task<QueryResponse> Handle(Query request, CancellationToken cancellationToken)
        {
           var pageResults = 2f;
            var studentList = await _studentService.GetStudentListAsync();
            var pageCount = Math.Ceiling(studentList.Count() / pageResults);

            var students = studentList
                           .Skip((request.PageNumber -1) * (int)pageResults)
                           .Take((int)pageResults)
                           .ToList();

            var response = new QueryResponse
            {
                Students = students,
                CurrentPage = request.PageNumber,
                Pages = (int)pageCount
            };
            return  response;
        }
    }
}
