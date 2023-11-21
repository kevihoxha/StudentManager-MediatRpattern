using MediatR;
using SendGrid.Helpers.Errors.Model;
using System.Windows.Input;
using TestingMediatR.JWT;
using TestingMediatR.Models;
using TestingMediatR.Repositories;
using TestingMediatR.Services;
using Xamarin.Essentials;

namespace TestingMediatR.Login
{
    internal sealed class LoginCommandHandler : IRequestHandler<LoginCommand, string>
    {
        private readonly IStudentService _studentService;
        private readonly IJwtProvider _jwtProvider;
        public LoginCommandHandler(IStudentService studentService , IJwtProvider jwtProvider)
        {
            _studentService = studentService;
            _jwtProvider = jwtProvider;
        }
        public async Task<string> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            string email = request.StudentEmail;
            StudentDetails? student = await _studentService.GetStudentByEmailAsync(email);
            if (student == null)
            {
                throw new NotFoundException("Student not found");
            }
            string token = _jwtProvider.Generate(student);
            return token;
        }
    }
}
