using MediatR;
using ServiceStack.FluentValidation.Attributes;
using TestingMediatR.Models;

namespace TestingMediatR.Commands.CreateStudent
{
    public record Command1(string studentName, string studentEmail, string studentAddress, int studentAge, string phoneNumber, string serialNumber,int GradeId, StudentGrade StudentGrade) : IRequest<StudentDetails>;
    
    
}

