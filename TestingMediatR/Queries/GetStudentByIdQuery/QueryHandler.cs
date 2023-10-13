using MediatR;
using TestingMediatR.Models;
using TestingMediatR.Queries;
using TestingMediatR.Repositories;
using TestingMediatR.Services;

namespace TestingMediatR.Handlers
{
    public class QueryHandler : IRequestHandler< Queries.GetStudentByIdQuery.Query, StudentDetails> 
    {
        private readonly IStudentService _studentService;
        public QueryHandler( IStudentRepository studentRepository, IStudentService studentService)
        {
            _studentService = studentService;

        }
        public async Task <StudentDetails>Handle(Queries.GetStudentByIdQuery.Query query,CancellationToken cancellationToken)
        {
            return await _studentService.GetStudentByIdAsync(query.Id);
        }
    }
}
