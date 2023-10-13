using MediatR;
using TestingMediatR.Models;
using TestingMediatR.Repositories;
using TestingMediatR.Services;

namespace TestingMediatR.Commands.UpdateStudent
{
    public class CommandHandler : IRequestHandler<Command, bool>
    {
        private readonly IStudentService _studentService;
        public CommandHandler(IStudentService studentService)
        {
            _studentService = studentService;
        }
        public async Task<bool> Handle(Command command, CancellationToken cancellationToken)
        {
            var studentDetails = await _studentService.GetStudentByIdAsync(command.Id);
            if (studentDetails == null)
                return default;
            studentDetails.StudentName = command.StudentName;
            studentDetails.StudentEmail = command.StudentEmail;
            studentDetails.StudentAddress = command.StudentAddress;
            studentDetails.StudentAge = command.StudentAge;
            studentDetails.PhoneNumber = command.PhoneNumber;
            studentDetails.Grade = command.studentGrade;
            return await _studentService.UpdateStudentAsync(studentDetails);
        }
    }
}
