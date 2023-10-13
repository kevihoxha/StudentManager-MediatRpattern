using MediatR;
using TestingMediatR.Models;
using TestingMediatR.Repositories;
using TestingMediatR.Services;

namespace TestingMediatR.Queries.GetStudentListQuery
{
    public class QueyHandler : IRequestHandler<Queries.GetStudentListQuery.Query, IQueryable<StudentDetails>>
    {
        private readonly IStudentService _studentService;
        public QueyHandler(IStudentService studentService)
        {
            _studentService = studentService;
        }
        public async Task<IQueryable<StudentDetails>> Handle(Queries.GetStudentListQuery.Query query, CancellationToken cancellationToken)
        {
            return await _studentService.GetStudentListAsync();
        }
    }
}
