using MediatR;
using System.Windows.Input;

namespace TestingMediatR.Login;

public class LoginCommand : IRequest<string>
{
    public string StudentEmail { get; set; }

    public LoginCommand(string studentEmail)
    {
        StudentEmail = studentEmail;
    }
}
