using MediatR;
using TestingMediatR.Repositories;
using TestingMediatR.Services;

namespace TestingMediatR.Commands.DeleteCommand
{
    public class CoomandHandler : IRequestHandler<Command, bool>
    {
        private readonly IStudentService _studentService;
        public CoomandHandler(IStudentService studentService)
        {
            _studentService = studentService;
        }
        public async Task<bool> Handle(Command command, CancellationToken cancellationToken)
        {
            var studentDetails = await _studentService.GetStudentByIdAsync(command.Id);
            if (studentDetails == null)
                return false;
            await _studentService.DeleteStudentAsync(studentDetails.Id);
            return true;
        }
    }
}
