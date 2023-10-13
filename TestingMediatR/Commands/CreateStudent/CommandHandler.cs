using MediatR;
using TestingMediatR.Models;
using TestingMediatR.Repositories;
using TestingMediatR.Services;

namespace TestingMediatR.Commands.CreateStudent
{
    public class CommandHandler : IRequestHandler<Command1, StudentDetails>
    {
        private readonly IStudentService _studentService;
        public CommandHandler(IStudentService studentService)

        {
            _studentService = studentService;
        }
        public async Task<StudentDetails> Handle(Command1 command, CancellationToken cancellationToken)
        {
            var studentDetails = new StudentDetails()
            {
                StudentAddress = command.studentAddress,
                StudentEmail = command.studentEmail,
                StudentName = command.studentName,
                StudentAge = command.studentAge,
                PhoneNumber=command.phoneNumber,
                SerialNumber = command.serialNumber,
                Grade=command.StudentGrade

                
            };
            return await _studentService.AddStudentAsync(studentDetails);
        }
    }
}
