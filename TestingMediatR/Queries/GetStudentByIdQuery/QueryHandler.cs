using AutoMapper;
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
        private readonly IMapper _mapper;
        public QueryHandler( IStudentRepository studentRepository, IStudentService studentService, IMapper mapper)
        {
            _studentService = studentService;
            _mapper = mapper;

        }
        public async Task <StudentQueryResponse> Handle(Queries.GetStudentByIdQuery.Query query,CancellationToken cancellationToken)
        {
            var studentDetails = await _studentService.GetStudentByIdAsync(query.Id);

           var response = _mapper.Map<StudentQueryResponse>(studentDetails);

            return response;
        }
    }
}
