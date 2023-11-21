using TestingMediatR.Models;

namespace TestingMediatR.JWT
{
    public interface IJwtProvider
    {
        string Generate(StudentDetails student);
    }
}
