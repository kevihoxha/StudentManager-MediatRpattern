using MediatR;
using TestingMediatR.Models;
using TestingMediatR.Queries;
using TestingMediatR.Queries.GetStudentByIdQuery;
using TestingMediatR.Repositories;
using TestingMediatR.Services;

namespace TestingMediatR.Handlers
{
    public class QueryHandler : IRequestHandler< Queries.GetStudentByIdQuery.Query, StudentQueryResponse> 
    {
        private readonly IStudentService _studentService;
        public QueryHandler( IStudentRepository studentRepository, IStudentService studentService)
        {
            _studentService = studentService;

        }
        public async Task <StudentQueryResponse> Handle(Queries.GetStudentByIdQuery.Query query,CancellationToken cancellationToken)
        {
            var studentDetails = await _studentService.GetStudentByIdAsync(query.Id);

            // Map the necessary properties to the new response class
            var queryResponse = new StudentQueryResponse
            {
                StudentName = studentDetails.StudentName,
                Grade = studentDetails.Grade.Grade,
                Description = studentDetails.Grade.Description,
            };

            return queryResponse;
        }
    }
}
